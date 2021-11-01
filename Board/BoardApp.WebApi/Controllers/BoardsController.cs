using AutoMapper;
using BoardApp.BLL.Services;
using BoardApp.Common.Models;
using BoardApp.WebApi.Jwt;
using BoardApp.WebApi.Models;
using BoardApp.WebApi.Models.RequestModels;
using BoardApp.WebApi.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BoardApp.Common;

namespace BoardApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController: ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IBoardAccessService _boardAccessService;
        private readonly IBoardService _boardService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public BoardsController(IBoardService boardService, IMapper mapper, IBoardAccessService boardAccessService, IPermissionService permissionService, IUserService userService, ITokenService tokenService)
        {
            _boardService = boardService;
            _mapper = mapper;
            _boardAccessService = boardAccessService;
            _permissionService = permissionService;
            _userService = userService;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("{boardId}")]
        public IActionResult GetBoardByIdWithMemberList(int boardId)
        {
            var response = new List<BoardsGetByIdResponse>();
            var id = _boardService.Read(boardId);
            response.Add(_mapper.Map<BoardsGetByIdResponse>(id));
            foreach (var infoAboutUsers in response)
            {
                infoAboutUsers.Members = new List<BoardsMemberListResponse>();
                var responseResult = _boardAccessService.ReadAll().Where(i => i.BoardId == boardId);
                foreach (var boardAccessId in responseResult)
                {
                        var result = _userService.Read(boardAccessId.UserId);
                        infoAboutUsers.Members.Add(_mapper.Map<BoardsMemberListResponse>(result));
                }
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetByUserId")]
        public IActionResult GetBoardsByUserIdWithPermissions()
        {
            var userId=_tokenService.GetUserId(HttpContext); //GET ID FROM TOKEN
            var boardAccess = _boardAccessService.ReadAll().Where(i => i.UserId == userId);
            var resultSearch = boardAccess.Select(infoBoard => _boardService.Read(infoBoard.BoardId)).Select(board => _mapper.Map<BoardsGetByUserIdResponse>(board)).ToList();
            foreach (var infoAboutPermission in resultSearch)
            {
                infoAboutPermission.Permission = new List<PermissionDto>();
                var responseResult = _boardAccessService.ReadAll().Where(x => x.BoardId == infoAboutPermission.Id && x.UserId == userId);
                foreach (var boardAccessId in responseResult)
                {
                    infoAboutPermission.Permission.Add(_permissionService.Read(boardAccessId.PermissionId));
                }
            }
            return Ok(resultSearch);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateBoard(BoardModel model)
        {
            var modelBoard = _mapper.Map<BoardDto>(model);
            var boardDto = _boardService.Add(modelBoard);
            var userId = _tokenService.GetUserId(HttpContext);
            _boardAccessService.Add(new BoardAccessDto() {BoardId = boardDto.Id, UserId = userId, PermissionId = 1});

            return Ok();
        }
        [Authorize]
        [HttpPost("addUsers")]
        public IActionResult AddUsersToBoard(AddUserRequest model)
        {
            var tokenId = _tokenService.GetUserId(HttpContext);
            var showAllUsers = _boardAccessService.ReadAll().Where(x => x.UserId == tokenId);
            foreach (var permissions in showAllUsers)
            {
                if (permissions.PermissionId != (int)Access.Owner)
                    return Unauthorized("You don't have permissions");
            }
            var board = _mapper.Map<BoardAccessDto>(model);
            _boardAccessService.Add(new BoardAccessDto() {BoardId = board.BoardId, UserId = board.UserId, PermissionId = board.PermissionId});
            return Ok();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult PathBoard(int id, BoardModel model)
        {
            var tokenId = _tokenService.GetUserId(HttpContext);
            var showAllUsers = _boardAccessService.ReadAll().Where(x => x.UserId == tokenId);
            foreach (var permissions in showAllUsers)
            {
                if (permissions.PermissionId != (int)Access.Owner)
                    return Unauthorized("You don't have permissions");
            }
            var board = _mapper.Map<BoardDto>(model);
            board.Id = id;
            try
            {
                _boardService.Update(board);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tokenId = _tokenService.GetUserId(HttpContext);
            var showAllUsers = _boardAccessService.ReadAll().Where(x => x.UserId == tokenId);
            foreach (var permissions in showAllUsers)
            {
                if (permissions.PermissionId != (int)Access.Owner)
                    return Unauthorized("You don't have permissions");
            }
            _boardService.Delete(id);
             return Ok();
        }
    }
}
