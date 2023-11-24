using System;
namespace DLL
{
	public static class RNG
	{
		private static Random _rng = new Random();
		public static int Range(int upper, int lower)
		{
			int randomNumber = _rng.Next(lower, upper);
			return randomNumber;
		}
	}
}

