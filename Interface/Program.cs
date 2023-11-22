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

        raceManager.Players = saveData.Players;
        raceManager.Races = saveData.Races;

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

        Console.WriteLine("Hvad er dit navn");
        string name = Console.ReadLine();

        Console.WriteLine("Hvad er dit password?");
        string password = Console.ReadLine();

        Player playerName = raceManager.Players.FirstOrDefault(player => player.Name == name);
        Player playerPassword = raceManager.Players.FirstOrDefault(player => player.Password == password);

        if (playerName.Name == playerPassword.Name && playerName.Password == playerPassword.Password) 
        {
            string response = playerName.Login(name, password);
            Console.WriteLine(response);
        }

        Console.WriteLine("Pick a race");

        for (int raceIndex = 1; raceIndex < raceManager.Races.Count; raceIndex++)
        {
            Race race = raceManager.Races[raceIndex];
            Console.WriteLine("Race " + raceIndex + "." + " " + race.RaceID);
        }

        int racePick = Int32.Parse(Console.ReadLine());

        Console.WriteLine("Race Started");

        raceManager.ConductRace(raceManager.Races[racePick - 1]);

        Rat.Rat winner = raceManager.Races[racePick - 1].GetWinner();
        Console.WriteLine(winner);

        string raceReport = raceManager.ViewRaceReport(raceManager.Races[racePick - 1]);
        Console.WriteLine(raceReport);
        Console.ReadLine();

    }
}

