using System;
namespace Poker
{
    public class Card
    {
        public enum SUIT
		{
            C, D, H, S
        }

        public enum VALUE
        {
            TWO = 2, THREE = 3, FOUR = 4, FIVE = 5, SIX = 6, SEVEN = 7, EIGHT = 8, NINE = 9, T, J, Q, K, A
        }

        public SUIT Suit { get; set; }
        public VALUE Value { get; set; }
    }
}
