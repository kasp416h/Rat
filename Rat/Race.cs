using System;
using System.Diagnostics;

namespace DLL
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
            bool raceIsRunning = true;

            while (raceIsRunning)
            {
                heat += 1;
                for (int ratIndex = 0; ratIndex < rats.Count; ratIndex++)
                {
                    Rat rat = rats[ratIndex];
                    rat.MoveRat();
                    logRace(rat, heat);

                    if (rat.Posistion >= trackLength)
                    {
                        raceIsRunning = false;
                        break;
                    }
                }
            }
        }
        public Rat GetWinner()
        {
            Rat rat = Rats.FirstOrDefault(rat => rat.Posistion >= RaceTrack.TrackLength);
            _winner = rat;
            return rat;
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

