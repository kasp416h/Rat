using Rat;
using Rat.repository;

namespace Interface;

class Program
{
    static void Main(string[] args)
    {
        SaveCenter save = new SaveCenter();
        SaveCenter saveData = save.LoadData();

        RaceManager raceManager = new RaceManager();

        foreach (Race race in saveData.Races)
        {
            raceManager.CreateRace(race.RaceID, race.Rats, race.RaceTrack);
        }

        foreach (Track track in saveData.Tracks)
        {
            raceManager.CreateTrack(track.Name, track.TrackLength);
        }

        foreach (Rat.Rat rat in saveData.Rats)
        {
            raceManager.CreateRat(rat.Name, rat.Upper, rat.Lower);
        }

        foreach (Player player in saveData.Players)
        {
            raceManager.CreatePlayer(player.Name, player.Password, player.Money);
        }
    }
}

