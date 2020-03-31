using System;
using System.Linq;

namespace Poker
{
    public class DealCards : Deck
    {
        private Card[] firstPlayerHand;
        private Card[] secondPlayerHand;
        private Card[] sortedFirstPlayerHand;
        private Card[] sortedSecondPlayerHand;

        public DealCards()
        {
            firstPlayerHand = new Card[5];
            sortedFirstPlayerHand = new Card[5];
            secondPlayerHand = new Card[5];
            sortedSecondPlayerHand = new Card[5];
        }

        public void Deal()
		{
            setUpDeck();
            getHand();
            sortCards();
            displayCards();
            evaluateCards();
		}

        public void getHand()
		{
            for (int i = 0; i < 5; i++)
                firstPlayerHand[i] = getDeck[i];

            for (int i = 5; i < 10; i++)
                secondPlayerHand[i -5] = getDeck[i];
             
        }

        public void sortCards()
		{
            var queryFirstPlayer = from hand in firstPlayerHand
                                   orderby hand.Value
                              select hand;

            var querySecondPlayer = from hand in secondPlayerHand
                                    orderby hand.Value
                                    select hand;

            var index = 0;
            foreach(var element in queryFirstPlayer.ToList())
			{
                sortedFirstPlayerHand[index] = element;
                index++;
			}

            index = 0;
            foreach (var element in querySecondPlayer.ToList())
            {
                sortedSecondPlayerHand[index] = element;
                index++;
            }
        }

        public void displayCards()
		{
            Console.WriteLine("PLAYER ONE HAND");
            for (int i = 0; i < 5; i++)
            { 
                Console.WriteLine(sortedFirstPlayerHand[i].Value.ToString() +""+ sortedFirstPlayerHand[i].Suit.ToString());
            }
            
            Console.WriteLine("\nPLAYER TWO HAND");
            for (int i = 5; i < 10; i++)
            {
                Console.WriteLine(sortedSecondPlayerHand[i - 5].Value.ToString() + "" + sortedSecondPlayerHand[i - 5].Suit.ToString());
            }
        }

        public void evaluateCards()
		{
            HandEvaluator firstPlayerHandEvaluator = new HandEvaluator(sortedFirstPlayerHand);
            HandEvaluator secondPlayerHandEvaluator = new HandEvaluator(sortedSecondPlayerHand);

            Hand firstPlayerHand = firstPlayerHandEvaluator.EvaluateHand();
            Hand secondPlayerHand = secondPlayerHandEvaluator.EvaluateHand();

            Console.WriteLine("\nFirst Player Hand: " + firstPlayerHand);
            Console.WriteLine("\nSecond Player Hand: " + secondPlayerHand);

            if(firstPlayerHand > secondPlayerHand)
			{
                Console.WriteLine("\n--------------------");
                Console.WriteLine("\nFirst player WINS!");
                Console.WriteLine("\n--------------------");

            }
            else if(firstPlayerHand < secondPlayerHand)
			{
                Console.WriteLine("\n--------------------");
                Console.WriteLine("\nSecond player WINS!");
                Console.WriteLine("\n--------------------");
            }
            else
			{
                if(firstPlayerHandEvaluator.HandValues.Total > secondPlayerHandEvaluator.HandValues.Total)
                    Console.WriteLine("\n-------------------- \nFirst player WINS! \n--------------------");
                else if (firstPlayerHandEvaluator.HandValues.Total < secondPlayerHandEvaluator.HandValues.Total)
                    Console.WriteLine("\n-------------------- \nSecond player WINS! \n--------------------");
                else if(firstPlayerHandEvaluator.HandValues.HighCard > secondPlayerHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("\n-------------------- \nFirst player WINS! \n--------------------");
                else if (firstPlayerHandEvaluator.HandValues.HighCard < secondPlayerHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("\n-------------------- \nSecond player WINS! \n--------------------");
                else
                    Console.WriteLine("\n-------------------- \nTIE! \n--------------------");
            }
        }
    }
}
