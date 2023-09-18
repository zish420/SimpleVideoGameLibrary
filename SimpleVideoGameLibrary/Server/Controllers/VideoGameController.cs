using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SimpleVideoGameLibrary.Server.Data;
using SimpleVideoGameLibrary.Shared;

namespace SimpleVideoGameLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly DataContext _context;

        public VideoGameController(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<VideoGame>>> GetAllVideoGames()
        {
            var list = await _context.VideoGames.OrderBy(g => g.ReleaseYear).ToListAsync();


            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<List<VideoGame>>> CreateVideoGames(VideoGame videoGame)
        {
            _context.VideoGames.Add(videoGame);
            await _context.SaveChangesAsync();
            
            return await GetAllVideoGames;
        }
    }
}
