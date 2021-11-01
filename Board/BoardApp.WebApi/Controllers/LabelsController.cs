using System.Collections.Generic;
using AutoMapper;
using BoardApp.BLL.Services;
using BoardApp.WebApi.Models;
using BoardApp.WebApi.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _labelService;
        private readonly IMapper _mapper;
        public LabelsController(ILabelService labelService, IMapper mapper)
        {
            _labelService = labelService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetLabels()
        {
            var labels = _labelService.ReadAll();
            var response = new GetLabelsResponse
            {
                Labels = _mapper.Map<IList<LabelModel>>(labels)
            };
            return Ok(response);
        }
    }
}
