using System;

namespace DLL.Modles
{
    public class Track
    {
        public string Name { get; set; }
        public int TrackLength { get; set; }
        public Track(string name, int trackLength)
        {
            Name = name;
            TrackLength = trackLength;
        }
    }
}

