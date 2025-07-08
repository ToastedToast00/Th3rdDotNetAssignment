using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class CardDeck
    {
        private static readonly string[] Suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        private static readonly string[] Ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        public static List<string> GenerateDeck() 
        { 
            var deck = new List<string>();

            //Generate all 52 cards
            foreach (var suit in Suits)
            {
                foreach (var rank in Ranks)
                {
                    deck.Add($"{rank} of {suit}");
                }
            }

            //shuffle the deck
            //This is classified as a Fisher-Yates shuffle algorithm (whatever that means)
            var rng = new Random();
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = rng.Next(0, i + 1);
                //swap
                var temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }

            return deck;
        }
    }
}
