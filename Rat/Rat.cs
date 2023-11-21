using System;
namespace Rat
{
    public class Rat
    {
        public string Name;
        public int Posistion { get; set; }
        public int Upper { get; set; }
        public int Lower { get; set; }
        public Rat(string name, int upper, int lower)
        {
            Name = name;
            Upper = upper;
            Lower = lower;
            Posistion = 0;
        }
        public void ResetRat()
        {
            Posistion = 0;
        }
        public int MoveRat()
        {
            int randomNumber = RNG.Range(Upper, Lower);
            Posistion = randomNumber;
            return Posistion;
        }
    }
}

