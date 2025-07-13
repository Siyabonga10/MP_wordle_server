using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MP_WORDLE_SERVER.Services;
using MP_WORDLE_SERVER.Models;

namespace MP_WORDLE_SERVER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    internal class GameController : ControllerBase
    {
        [HttpGet("(gameId)")]
        public ActionResult<Game> GetGame(int gameId)
        {
            if (gameId <= 0)
                return BadRequest($"Cant have a negative game Id");

            var game = GameService.FindGame(gameId);
            return game == null ? NotFound($"Game with Game Id {gameId} not found") : game;
        }

        [HttpPost]
        public ActionResult CreateGame()
        {
            try
            {
                var player = GetAuthenticatedUsername();
                if (player.Value == null) return BadRequest("Player not authenticated");
                var game = GameService.CreateGame(hostUsername: player.Value);
                return CreatedAtAction(nameof(GetGame), new {gameId = game.Id}, game);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal error: Could not create new game");
            }
        }

        [HttpPut("{gameID}/join")]
        public ActionResult AddPlayer([FromRoute] int gameId)
        {
            try
            {
                var targetGame = GameService.FindGame(gameId);
                if (targetGame == null)
                    return NotFound();

                var player = GetAuthenticatedUsername();
                if (player.Value == null) return BadRequest("Player not authenticated");

                if (GameService.AddPlayerToGame(gameId, player.Value, isHost: false))
                    return NoContent();
                return BadRequest("could not add player to game");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal error: Could not add player to game");
            }
        }

        private ActionResult<string> GetAuthenticatedUsername()
        {
            var playerUsername = User.FindFirst("sub")?.Value;
            if (playerUsername == null)
                return BadRequest("Player authentication failed");
            return playerUsername;
        }
    }
}