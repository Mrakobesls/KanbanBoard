using AutoMapper;
using BoardApp.BLL.Services;
using BoardApp.Common.Models;
using BoardApp.WebApi.Jwt;
using BoardApp.WebApi.Models.RequestModels.Column;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BoardApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : Controller
    {
        private readonly IColumnService _columnService;
        private readonly IBoardAccessService _boardAccessService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public ColumnsController(IColumnService columnService, IBoardAccessService boardAccessService, ITokenService tokenService, IMapper mapper)
        {
            _columnService = columnService;
            _boardAccessService = boardAccessService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddColumn(AddColumnRequest column)
        {
            if (!_boardAccessService.IsUpdatePermission(column.BoardId, _tokenService.GetUserId(HttpContext)))
                return Forbid();

            try
            {
                var columnId = _columnService.Add(_mapper.Map<ColumnDto>(column)).Id;
                return Ok(columnId);
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
        public IActionResult EditColumn(int id, EditColumnRequest column)
        {
            if (!_boardAccessService.IsUpdatePermission(_columnService.Read(id).BoardId, _tokenService.GetUserId(HttpContext)))
                return Forbid();

            try
            {
                var columnDto = _mapper.Map<ColumnDto>(column);
                columnDto.Id = id;
                _columnService.Update(columnDto);

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
        public IActionResult DeleteColumn(int id)
        {
            if (!_boardAccessService.IsUpdatePermission(_columnService.Read(id).BoardId, _tokenService.GetUserId(HttpContext)))
                return Forbid();

            try
            {
                _columnService.Delete(id);

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
