using DLL.Modles;

namespace DLL;

public class RaceManager
{
    private List<Rat> rats;
    private List<Player> players;
    private List<Track> tracks;
    private List<Race> races;

    public RaceManager(List<Rat> rats, List<Player> players, List<Track> tracks, List<Race> races)
    {
        this.rats = rats;
        this.players = players;
        this.tracks = tracks;
        this.races = races;
    }

    public RaceManager()
    {

    }

    public List<Track> Tracks { get; set;}
    public List<Player> Players { get; set; }
    public List<Race> Races { get; set; }
    public List<Rat> Rats { get; set; }

    public Race CreateRace(int raceID, List<Rat> rats, Track track)
    {
        Race race = new Race(raceID, rats, track);
        return race;
    }
    public Track CreateTrack(string name, int trackLength)
    {
        Track track = new Track(name, trackLength);
        return track;
    }
    public void ConductRace(Race race)
    {
        race.ConductRace();
    }

    public string ViewRaceReport(Race race)
    {
        string raceReport = race.GetRaceReport();
        return raceReport;
    }
    public Rat CreateRat(string name, int upper, int lower)
    {
        Rat rat = new Rat(name, upper, lower);
        return rat;
    }
    public Player CreatePlayer(string name, string password, int money)
    {
        Player player = new Player(name, password, money);
        return player;
    }


}