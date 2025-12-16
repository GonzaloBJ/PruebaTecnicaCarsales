using System.Net;
using BFF.Web.Filters;
using BFF.Web.Interfaces;
using BFF.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BFF.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodiosController : ControllerBase
    {
        private readonly IItemInfoService<EpisodioDto> _episodiosInfoService;

        public EpisodiosController(IItemInfoService<EpisodioDto> episodiosInfoService)
        {
            _episodiosInfoService = episodiosInfoService;
        }


        [HttpGet(Name = "episodios")]
        public async Task<IActionResult> GetAsync([FromQuery] EpisodiosFilter filter)
        {
            var result = await _episodiosInfoService.GetItemsInfoAsync(filter);

            if (!result.Success)
            {
                if (result.Code == (int)HttpStatusCode.NotFound)
                    return NotFound(result.ErrorMessage);

                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}