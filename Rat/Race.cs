using System;
using System.Diagnostics;

namespace Rat
{
    public class Race
    {
        public int RaceID { get; set; }
        public List<Rat> Rats { get; set; }
        public Track RaceTrack { get; set; }
        private Rat _winner { get; set; }
        private string _log { get; set; }
        public Race(int raceID, List<Rat> rats, Track raceTrack)
        {
            RaceID = raceID;
            Rats = rats;
            RaceTrack = raceTrack;
        }
        public void ConductRace()
        {
            List<Rat> rats = Rats;
            int trackLength = RaceTrack.TrackLength;

            int heat = 0;
            while (!rats.Any(rat => rat.Posistion >= trackLength))
            {
                heat += 1;
                for (int ratIndex = 0; ratIndex < rats.Count; ratIndex++)
                {
                    Rat rat = rats[ratIndex];
                    rat.MoveRat();
                    logRace(rat, heat);
                }
            }

            foreach (Rat rat in rats)
            {
                rat.ResetRat();
            }
        }
        public Rat GetWinner()
        {
            Rat rat = Rats.FirstOrDefault(rat => rat.Posistion == RaceTrack.TrackLength);
            _winner = rat;
            return _winner;
        }
        public string GetRaceReport()
        {
            return _log;
        }
        private void logRace(Rat rat, int heat)
        {
            _log += String.Format("Heat {0}.{1}-{2} ", heat, rat.Name, rat.Posistion);
        }
    }
}

