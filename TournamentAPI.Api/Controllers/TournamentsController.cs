using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TournamentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // CRUD operations using Unit of Work

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            var tournaments = await _unitOfWork.TournamentRepository.GetAllAsync();
            return Ok(tournaments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(tournament);
        }

        [HttpPost]
        public async Task<ActionResult<Tournament>> CreateTournament(Tournament tournament)
        {
            _unitOfWork.TournamentRepository.Add(tournament);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest("The provided id does not match the id of the tournament.");
            }

            var existingTournament = await _unitOfWork.TournamentRepository.GetAsync(id);
            if (existingTournament == null)
            {
                return NotFound("Tournament not found.");
            }

            // Update the existing tournament with the new data
            existingTournament.Title = tournament.Title;
            existingTournament.StartDate = tournament.StartDate;
            // Update other properties as needed

            try
            {
                _unitOfWork.TournamentRepository.Update(existingTournament);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while updating the tournament.");
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            _unitOfWork.TournamentRepository.Remove(tournament);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
