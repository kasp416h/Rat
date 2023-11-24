using System;
namespace DLL
{
	public class Bet
	{
		private int _money { get; set; }
		public int Money { get { return _money; } }
		private Player _player { get; set; }
		private Race _race { get; set; }
		private Rat _rat { get; set; }
		public Bet(int money, Player player, Race race, Rat rat)
		{
			_money = money;
			_player = player;
			_race = race;
			_rat = rat;
		}
		public void PayWinnings(Rat winner, Race race)
		{
			if (winner.Name == _rat.Name && _race.RaceID == race.RaceID)
			{
				_player.Money += _money * 2;
			}
		}
	}
}

