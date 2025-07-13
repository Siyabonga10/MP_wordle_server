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
        [HttpPost]
        public ActionResult CreateGame()
        {
            var playerUsername = User.FindFirst("sub")?.Value;
            if (playerUsername == null)
                return BadRequest();
            var newGameID = GameService.NewGame();
            var newPlayer = new Player(playerUsername, ishost: true);
            GameService.AddPlayerToGame(newGameID, newPlayer);
            return CreatedAtAction(nameof(CreateGame), GameService.FindGame(newGameID), newGameID);
        }

        [HttpPut("{gameID}")]
        public ActionResult AddPlayer(int gameID)
        {
            var targetGame = GameService.FindGame(gameID);
            if (targetGame == null)
                return NotFound();

            var playerUsername = User.FindFirst("sub")?.Value;
            if (playerUsername == null)
                return BadRequest();
            var newPlayer = new Player(playerUsername, ishost: false);
            if (GameService.AddPlayerToGame(gameID, newPlayer))
                return NoContent();
            return BadRequest();
        }
    }
}