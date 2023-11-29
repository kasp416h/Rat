using System;
namespace RatRaceWeb.Models
{
    public class Player : Rat.Player
    {
        public Player(string name, string password, int money) : base(name, password, money)
        {
            Name = name;
            Password = password;
            Money = money;
        }
    }
}

