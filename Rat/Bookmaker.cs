using System;
using DLL.Modles;

namespace DLL
{
    public class Bookmaker
    {
        public List<Bet> Bets { get; set; }
        public Race Race { get; set; }
        public Bookmaker()
        {
            Bets = new List<Bet>();
        }
        public void SetRace(Race race)
        {
            Race = race;
        }
        public Bet PlaceBet(Race race, Rat rat, Player player, int money)
        {
            Bet bet = new Bet(money, player, race, rat);
            return bet;

        }
        public void PayOutWinnings(Rat winner)
        {
            foreach (Bet bet in Bets)
            {
                bet.PayWinnings(winner, Race);
            }
        }
    }
}

