using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GamesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // CRUD operations using Unit of Work

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var games = await _unitOfWork.GameRepository.GetAllAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _unitOfWork.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame(Game game)
        {
            _unitOfWork.GameRepository.Add(game);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest("The provided id does not match the id of the game.");
            }

            var existingGame = await _unitOfWork.GameRepository.GetAsync(id);
            if (existingGame == null)
            {
                return NotFound("Game not found.");
            }

            // Update the existing game with the new data
            existingGame.Title = game.Title;
            existingGame.Time = game.Time;
            // Update other properties as needed

            try
            {
                _unitOfWork.GameRepository.Update(existingGame);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while updating the game.");
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _unitOfWork.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            _unitOfWork.GameRepository.Remove(game);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
