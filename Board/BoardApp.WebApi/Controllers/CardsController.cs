using AutoMapper;
using BoardApp.BLL.Services;
using BoardApp.Common;
using BoardApp.Common.Models;
using BoardApp.WebApi.Exceptions;
using BoardApp.WebApi.Jwt;
using BoardApp.WebApi.Models;
using BoardApp.WebApi.Models.RequestModels;
using BoardApp.WebApi.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IBoardAccessService _boardAccessService;
        private readonly IColumnService _columnService;
        private readonly IBoardService _boardService;

        public CardsController(ICardService cardService, 
                               IMapper mapper, 
                               ITokenService tokenService, 
                               IBoardAccessService boardAccessService, 
                               IBoardService boardService,
                               IUserService userService,
                               IColumnService columnService)
        {
            _cardService = cardService;
            _mapper = mapper;
            _tokenService = tokenService;
            _boardAccessService = boardAccessService;
            _boardService = boardService;
            _userService = userService;
            _columnService = columnService;
        }

        [Authorize]
        [HttpPatch("{cardId}/members/{userId}")]
        public IActionResult AddMember(int cardId, int userId)
        {
            if (!HasAccess(cardId, Access.Owner))
            {
                return Unauthorized();
            }

            TryGetCard(cardId, out var cardDto);

            var userDto = _userService.Read(userId);

            if (userDto is null)
            {
                return StatusCode(404, new { Message = "User doesn't exist." });
            }

            var result = _cardService.AddMember(cardId, userId);
            var response = _mapper.Map<CardGetMembersResponse>(result);

            return StatusCode(201, response);
        }

        [Authorize]
        [HttpGet("{cardId}/members")]
        public IActionResult GetCardMembers(int cardId)
        {
            if (!HasAccess(cardId, Access.Owner, Access.Read))
                return Unauthorized();
            
            TryGetCard(cardId, out var cardDto);

            var usersResult = _cardService.GetAllMembers(cardDto);

            if (usersResult.Count < 1)
                return StatusCode(204);

            var users = _mapper.Map<IList<UserModel>>(usersResult);
            var response = _mapper.Map<CardGetMembersResponse>(cardDto);
            response.Users = users;

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{cardId}/members")]
        public IActionResult UpdateMembers(int cardId, List<CardUpdateMembersRequest> request)
        {
            if (!HasAccess(cardId, Access.Owner, Access.Update))
            {
                return Unauthorized();
            }

            TryGetCard(cardId, out var cardDto);

            var membersIds = request.Select(m => m.Id);
            cardDto = _cardService.UpdateMembers(cardDto, membersIds);
            var response = _mapper.Map<CardGetMembersResponse>(cardDto);

            return StatusCode(202, response);
        }

        private bool HasAccess(int cardId, params Access[] accesses)
        {
            try
            {
                var userId = _tokenService.GetUserId(HttpContext);

                var card = _cardService.Read(cardId);
                var column = _columnService.Read(card.ColumnId);
                var hasAccess = _boardAccessService.GetAccess(userId, column.BoardId);

                foreach (var access in accesses)
                {
                    if (hasAccess.HasFlag(access))
                        return true;
                }

                return false;
            }
            catch (AuthorizeException)
            {
                return false;
            }
        }

        private IActionResult TryGetCard(int id, out CardDto cardDto)
        {
            cardDto = _cardService.Read(id);

            if (cardDto is null)
            {
                return StatusCode(404, new { Message = "Card doesn't exist." });
            }

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCardRequest cardRequest, int boardId, int columnId)
        {
            var result = CheckCardAccess(boardId, columnId);
            if (result is not null)
            {
                return result;
            }
            var dto = _mapper.Map<CardDto>(cardRequest);
            dto.ColumnId = columnId;
            _cardService.Add(dto);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(UpdateCardRequest cardRequest, int boardId, int columnId)
        {
            var result = CheckCardAccess(boardId, columnId);
            if (result is not null)
            {
                return result;
            }
            var dto = _mapper.Map<CardDto>(cardRequest);
            dto.ColumnId = columnId;
            try
            {
                _cardService.Update(dto);
            }
            catch (ArgumentNullException ex) when(ex.Message.Contains("Card not found."))
            {
                return NotFound("Card does not exist");
            }
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int boardId, int columnId, int id)
        {
            var result = CheckCardAccess(boardId, columnId);
            if (result is not null)
            {
                return result;
            }
            try
            {
                _cardService.Delete(id);
            }
            catch (ArgumentNullException ex) when (ex.Message.Contains("Card not found."))
            {
                return NotFound("Card does not exist");
            }

            return Ok();
        }

        [Authorize]
        [HttpPatch("{cardId}/labels/{labelId}/add")]
        public IActionResult AddLabel(int boardId, int columnId, int cardId, int labelId)
        {
            var result = CheckCardAccess(boardId, columnId);
            if (result is not null)
            {
                return result;
            }

            try
            {
                _cardService.AddLabel(cardId, labelId);
            }
            catch (ArgumentNullException ex) when (ex.Message.Contains("Card not found."))
            {
                return NotFound("Card does not exist");
            }
            catch (ArgumentNullException ex) when (ex.Message.Contains("Label not found."))
            {
                return NotFound("Label does not exist");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [Authorize]
        [HttpPatch("{cardId}/labels/{labelId}/delete")]
        public IActionResult DeleteLabel(int boardId, int columnId, int cardId, int labelId)
        {
            var result = CheckCardAccess(boardId, columnId);
            if (result is not null)
            {
                return result;
            }

            try
            {
                _cardService.DeleteLabel(cardId, labelId);
            }
            catch (ArgumentNullException ex) when (ex.Message.Contains("Card not found."))
            {
                return NotFound("Card does not exist");
            }
            catch (ArgumentNullException ex) when (ex.Message.Contains("Label not found."))
            {
                return NotFound("Label does not exist");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        private IActionResult CheckCardAccess(int boardId, int columnId)
        {
            int userId;
            try
            {
                userId = _tokenService.GetUserId(HttpContext);
            }
            catch (AuthorizeException)
            {
                return Unauthorized();
            }

            var board = _boardService.Read(boardId);
            if (board is null)
            {
                return NotFound("Board does not exist");
            }

            var boardAccesses = _boardAccessService.GetNoneReadBoardAccessesByUserIdAndBoardId(boardId, userId);
            if (boardAccesses.Count == 0)
            {
                return NotFound("User has no access to this board");
            }

            if (board.Columns.All(x => x.Id != columnId))
            {
                return NotFound("Column does not exist");
            }

            return null;
        }
    }
}
