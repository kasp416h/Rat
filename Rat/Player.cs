using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Rat
{
	public class Player
	{
		public string Name { get; set; }

        public string Password { get; set; }

		public int Money { get; set; }

		public List<Bet> Bets { get; set; }

		public Player(string name, string password, int money)
		{
			Name = name;
			Password = password;
			Money = money;
        }
	}
}

