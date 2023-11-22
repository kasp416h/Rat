using System;
namespace Rat
{
	public class Player
	{
		public string Name { get; set; }
		private string _password;
		public string Password { get { return _password; } }
		public bool LoggedIn { get; set; }
		public int Money { get; set; }
		public List<Bet> Bets { get; set; }
		public Player(string name, string password, int money)
		{
			Name = name;
			_password = password;
			Money = money;
			LoggedIn = false;
		}
	}
}

