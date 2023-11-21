using System;
namespace Rat
{
    public class Bookmaker
    {
        public List<Bet> Bets { get; set; }
        public Race Race { get; set; }
        public Bookmaker()
        {
        }
        public void SetRace(Race race)
        {
            Race = race;
        }
        public Bet PlaceBet(Race race, Rat rat, Player player, int money)
        {
            Bet bet = new Bet(money, player, race, rat);
            Bets.Add(bet);
            return bet;

        }
        public void PayOutWinnings()
        {
            Rat winner = Race.GetWinner();
            foreach (Bet bet in Bets)
            {
                bet.PayWinnings(winner, Race);
            }
        }
    }
}

