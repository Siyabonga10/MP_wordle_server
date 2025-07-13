using System.ComponentModel.DataAnnotations;

namespace MP_WORDLE_SERVER.Models
{
    public class Player
    {
        public int Id { get; }
        public string Username { get; }
        public bool IsHost { get; }
        public DateTime CreatedAt { get; }
        public Player(string username, bool isHost)
        {
            Id = Id;
            Username = username;
            IsHost = isHost;
            CreatedAt = DateTime.UtcNow;
        }
    }
}