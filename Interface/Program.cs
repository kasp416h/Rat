using BLL;
using BLL.Repository;
using DLL;
using DLL.Modles;

namespace Interface;

class Program
{
    static void Main(string[] args)
    {
        SaveCenter saveData = new SaveCenter();

        RaceManager raceManager = new RaceManager();
        Bookmaker bookmaker = new Bookmaker();

        StartUp(raceManager, saveData);

        Player player = Login(raceManager);

        while (player.LoggedIn)
        {
            Console.WriteLine("Options");


            Console.WriteLine("1. Create Race");
            Console.WriteLine("2. Create Track");
            Console.WriteLine("3. Create DLL");
            Console.WriteLine("4. Save game");
            Console.WriteLine("5. Exit game");
            Console.WriteLine("6. Pick a race");

            int option = Int32.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    int raceID = 1;

                    if (raceManager.Races.Count > 0)
                    {
                        Race lastRace = raceManager.Races.Last();
                        raceID = lastRace.RaceID + 1;
                    }

                    bool isPlayerStillChoosing = true;
                    List<Rat> temporaryRats = new List<Rat>();

                    Console.WriteLine("Choose a rat");

                    while (isPlayerStillChoosing)
                    {
                        for (int ratIndex = 0; ratIndex < raceManager.Rats.Count; ratIndex++)
                        {
                            Rat rat = raceManager.Rats[ratIndex];
                            Console.WriteLine((ratIndex + 1) + ". " + rat.Name);
                        }

                        Console.WriteLine((raceManager.Rats.Count + 1) + ". exit");

                        int ratOptionRace = Int32.Parse(Console.ReadLine());

                        if ((ratOptionRace - 1) == raceManager.Rats.Count)
                        {
                            isPlayerStillChoosing = false;
                        }
                        else
                        {
                            temporaryRats.Add(raceManager.Rats[ratOptionRace - 1]);
                        }

                    }

                    Console.WriteLine("Choose a track");

                    for (int trackIndex = 0; trackIndex < raceManager.Tracks.Count; trackIndex++)
                    {
                        Track track = raceManager.Tracks[trackIndex];
                        Console.WriteLine((trackIndex + 1) + ". " + track.Name);
                    }

                    int trackOption = Int32.Parse(Console.ReadLine());

                    Track pickedTrack = raceManager.Tracks[trackOption - 1];

                    Race createdRace = raceManager.CreateRace(raceID, temporaryRats, pickedTrack);
                    raceManager.Races.Add(createdRace);
                    break;
                case 2:
                    Console.WriteLine("Give the track name: ");
                    string trackName = Console.ReadLine();

                    Console.WriteLine("Give the track a length");
                    int trackLength = Int32.Parse(Console.ReadLine());

                    Track createdTrack = raceManager.CreateTrack(trackName, trackLength);
                    raceManager.Tracks.Add(createdTrack);
                    break;
                case 3:
                    Console.WriteLine("Give the rat a name");
                    string ratName = Console.ReadLine();

                    Console.WriteLine("Give the rat a minimum speed");
                    int lower = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Give the rat a maximum speed");
                    int upper = Int32.Parse(Console.ReadLine());

                    Rat createdRat = raceManager.CreateRat(ratName, upper, lower);
                    raceManager.Rats.Add(createdRat);
                    break;
                case 4:
                    Maneger maneger = new Maneger();
                    maneger.callsave();
                    break;
                case 5:
                    player.LoggedIn = false;
                    break;
                case 6:
                    for (int raceIndex = 0; raceIndex < raceManager.Races.Count; raceIndex++)
                    {
                        Race race = raceManager.Races[raceIndex];
                        Console.WriteLine("Race " + (raceIndex + 1) + "." + " " + race.RaceID);
                    }

                    int racePick = Int32.Parse(Console.ReadLine());

                    Race pickedRace = raceManager.Races[racePick - 1];

                    bookmaker.SetRace(pickedRace);

                    for (int ratIndex = 0; ratIndex < pickedRace.Rats.Count; ratIndex++)
                    {
                        Rat rat = pickedRace.Rats[ratIndex];
                        Console.WriteLine((ratIndex + 1) + ". " + rat.Name);
                    }

                    int ratOption = Int32.Parse(Console.ReadLine());

                    Rat pickedRat = pickedRace.Rats[ratOption - 1];

                    bool isBetNotPlaced = true;

                    while (isBetNotPlaced)
                    {
                        Console.WriteLine("Bet amount: ");
                        int placedMoney = Int32.Parse(Console.ReadLine());
                        if (placedMoney > player.Money)
                        {
                            Console.WriteLine("You do not have that much money");
                        } else
                        {
                            Bet bet = bookmaker.PlaceBet(pickedRace, pickedRat, player, placedMoney);
                            bookmaker.Bets.Add(bet);
                            isBetNotPlaced = false;
                        }
                    }

                    Console.WriteLine("Race Started");

                    raceManager.ConductRace(pickedRace);

                    string raceReport = raceManager.ViewRaceReport(raceManager.Races[racePick - 1]);
                    Console.WriteLine(raceReport);

                    Rat winner = raceManager.Races[racePick - 1].GetWinner();
                    Console.WriteLine(winner);

                    Console.WriteLine(player.Money);

                    bookmaker.PayOutWinnings(winner);

                    Console.WriteLine(player.Money);


                    break;
            }
        }
        Console.ReadLine();
    }
    public static void StartUp(RaceManager raceManager, SaveCenter saveData)
    {
        raceManager.Races = saveData.Races;
        raceManager.Tracks = saveData.Tracks;
        raceManager.Rats = saveData.Rats;
        raceManager.Players = saveData.Players;

        foreach (Race race in saveData.Races)
        {
            raceManager.CreateRace(race.RaceID, race.Rats, race.RaceTrack);
        }

        foreach (Track track in saveData.Tracks)
        {
            raceManager.CreateTrack(track.Name, track.TrackLength);
        }

        foreach (Rat rat in saveData.Rats)
        {
            raceManager.CreateRat(rat.Name, rat.Upper, rat.Lower);
        }

        foreach (Player player in saveData.Players)
        {
            raceManager.CreatePlayer(player.Name, player.Password, player.Money);
        }
    }
    public static Player? Login(RaceManager raceManager)
    {
        Console.WriteLine("Hvad er dit navn");
        string name = Console.ReadLine();

        Console.WriteLine("Hvad er dit password?");
        string password = Console.ReadLine();

        foreach (Player player in raceManager.Players)
        {
            if (name.ToLower() != player.Name.ToLower() || password != player.Password)
            {
                Console.WriteLine("Enten navn eller password er forkert");
            }
            else
            {
                Console.WriteLine("Du er nu logget ind");
                player.LoggedIn = true;
                return player;
            }
        }

        return null;
    }
}

