using Microsoft.AspNetCore.Mvc;
using TicketMaster.Services;

namespace TicketMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //pl: api/film/list
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var result = _filmService.List();
            return Ok(result);
        }
    }
}
