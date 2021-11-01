using AutoMapper;
using BoardApp.BLL.Services;
using BoardApp.Common.Models;
using BoardApp.WebApi.Jwt;
using BoardApp.WebApi.Models.RequestModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BoardApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ICardService _cardService;
        private readonly IColumnService _columnService;
        private readonly IBoardAccessService _boardAccessService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IColumnService columnService, ICardService cardService, IBoardAccessService boardAccessService, ITokenService tokenService, IMapper mapper)
        {
            _commentService = commentService;
            _cardService = cardService;
            _boardAccessService = boardAccessService;
            _tokenService = tokenService;
            _mapper = mapper;
            _columnService = columnService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(AddCommentRequest comment)
        {
            var userId = _tokenService.GetUserId(HttpContext);
            if (!_boardAccessService.IsUpdatePermission(_columnService.Read(_cardService.Read(comment.CardId).ColumnId).BoardId, userId))
                return Forbid();

            try
            {
                var commentDto = _mapper.Map<CommentDto>(comment);
                commentDto.DateTime = DateTime.Now;
                commentDto.UserId = userId;
                var commentId = _commentService.Add(commentDto).Id;
                return Ok(commentId);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult EditComment(int id, EditCommentRequest comment)
        {
            var userId = _tokenService.GetUserId(HttpContext);
            if (!_boardAccessService.IsReadPermission(
                _columnService.Read(
                    _cardService.Read(
                        _commentService.Read(id).CardId
                    ).ColumnId
                ).BoardId, userId))
                return Forbid();

            if (_commentService.Read(comment.Id).UserId != userId)
                return Forbid();

            try
            {
                var commentDto = _mapper.Map<CommentDto>(comment);
                commentDto.Id = id;
                commentDto.UserId = userId;
                commentDto.DateTime = DateTime.Now;
                _commentService.Update(commentDto);

                return Ok("Ok");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            if (!_boardAccessService.IsUpdatePermission(
                _columnService.Read(
                    _cardService.Read(
                        _commentService.Read(id).CardId
                    ).ColumnId
                ).BoardId, _tokenService.GetUserId(HttpContext)))
                return Forbid();

            try
            {
                _commentService.Delete(id);

                return Ok("Ok");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
