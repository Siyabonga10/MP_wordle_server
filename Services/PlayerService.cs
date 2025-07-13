using System.Security.Cryptography;
using MP_WORDLE_SERVER.Models;

namespace MP_WORDLE_SERVER.Services
{
    internal static class PlayerService
    {
        readonly private static Random RNG;
        private static List<Player> AllPlayers { get; set; } = [];

        static PlayerService()
        {
            RNG = new Random();
        }
        public static Player NewPlayer(bool isHost)
        {
            var playerID = RNG.Next(1000, 9999);
            var newPlayer = new Player("User" + playerID.ToString(), isHost);
            AllPlayers.Add(newPlayer);
            return newPlayer;
        }
    }
}