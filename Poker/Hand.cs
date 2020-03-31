using System;
namespace Poker
{
    public enum Hand
    {
        HighCard,
        Pair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind
    }

    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }

    class HandEvaluator : Card
    {
        private int heartsSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private Card[] cards;
        private HandValue handValue;

        public HandEvaluator(Card[] sortedhand)
        {
            heartsSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            cards = new Card[5];
            Cards = sortedhand;
            handValue = new HandValue();
        }

        public HandValue HandValues
        {
            get { return handValue; }
            set { handValue = value; }
        }

        public Card[] Cards
        {
            get { return cards; }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];
            }
        }

        public Hand EvaluateHand()
        {
            getNumberOfSuit();
            if (FourOfKind())
                return Hand.FourKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (ThreeOfKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (Pair())
                return Hand.Pair;

            handValue.HighCard = (int)cards[4].Value;
            return Hand.HighCard;
        }

        private void getNumberOfSuit()
        {
            foreach (var element in Cards)
            {
                if (element.Suit == Card.SUIT.H)
                    heartsSum++;
                else if (element.Suit == Card.SUIT.D)
                    diamondSum++;
                else if (element.Suit == Card.SUIT.C)
                    clubSum++;
                else if (element.Suit == Card.SUIT.S)
                    spadesSum++;

            }
        }

        private bool FourOfKind()
		{
            if(cards[0].Value == cards[1].Value &&
                cards[0].Value == cards[2].Value &&
                cards[0].Value == cards[3].Value)
			{
                handValue.Total = (int)cards[1].Value * 4;
                handValue.HighCard = (int)cards[4].Value;
                return true;
			}
            else if(cards[1].Value == cards[2].Value &&
                cards[1].Value == cards[3].Value &&
                cards[1].Value == cards[4].Value)
			{
                handValue.Total = (int)cards[1].Value * 4;
                handValue.HighCard = (int)cards[0].Value;
                return true;
			}

            return false;
		}

        private bool FullHouse()
		{
            if((cards[0].Value == cards[1].Value &&
                cards[0].Value == cards[2].Value &&
                cards[3].Value == cards[4].Value) ||
                    (cards[0].Value == cards[1].Value &&
                cards[2].Value == cards[3].Value &&
                cards[2].Value == cards[4].Value))
			{
                handValue.Total = (int)(cards[0].Value) +
                    (int)(cards[1].Value) + (int)(cards[2].Value) +
                    (int)(cards[3].Value) + (int)(cards[4].Value);
                return true;
            }

            return false;
		}

        private bool Flush()
		{
            if(heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
			{
                handValue.Total = (int)cards[4].Value;
                return true;
			}

            return false;
		}

        private bool Straight()
		{
            if(cards[0].Value +1 == cards[1].Value &&
                cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value)
			{
                handValue.Total = (int)cards[4].Value;
                return true;
			}

            return false;
		}

        private bool ThreeOfKind()
		{
            if((cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value) ||
                (cards[1].Value == cards[2].Value && cards[1].Value == cards[3].Value))
            {
                handValue.Total = (int)cards[2].Value * 3;
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if(cards[2].Value == cards[3].Value && cards[2].Value == cards[4].Value)
			{
                handValue.Total = (int)cards[2].Value * 3;
                handValue.HighCard = (int)cards[1].Value;
                return true;
			}

            return false;
		}

        private bool TwoPairs()
		{
            if(cards[0].Value == cards[1].Value && cards[2].Value == cards[3].Value)
			{
                handValue.Total = ((int)cards[1].Value * 2) + ((int)cards[3].Value * 2);
                handValue.HighCard = (int)cards[4].Value;
                return true;
			}
            else if (cards[0].Value == cards[1].Value && cards[3].Value == cards[4].Value)
            {
                handValue.Total = ((int)cards[1].Value * 2) + ((int)cards[3].Value * 2);
                handValue.HighCard = (int)cards[2].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value && cards[3].Value == cards[4].Value)
            {
                handValue.Total = ((int)cards[1].Value * 2) + ((int)cards[3].Value * 2);
                handValue.HighCard = (int)cards[0].Value;
                return true;
            }

            return false;
        }

        private bool Pair()
		{
            if(cards[0].Value == cards[1].Value)
			{
                handValue.Total = (int)cards[0].Value * 2;
                handValue.HighCard = (int)cards[4].Value;
                return true;
			}
            else if (cards[1].Value == cards[2].Value)
            {
                handValue.Total = (int)cards[1].Value * 2;
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if (cards[2].Value == cards[3].Value)
            {
                handValue.Total = (int)cards[2].Value * 2;
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if (cards[3].Value == cards[4].Value)
            {
                handValue.Total = (int)cards[3].Value * 2;
                handValue.HighCard = (int)cards[2].Value;
                return true;
            }

            return false;
        }
    }
}
