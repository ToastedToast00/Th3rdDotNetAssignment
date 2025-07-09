using ConsoleApp1;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("This Program allows the user to play a card game");
        Console.WriteLine("Enter 5 to stop");
        GameSelect();

    }
    public static void Instructions()
    {
        Console.WriteLine("please select a game to play");
        Console.WriteLine();
        Console.WriteLine("  1. Black Jack");
        Console.WriteLine("  2. Poker");
        Console.WriteLine("  3. Go Fish");
        Console.WriteLine("  4. PlaceHolder");//war is a pretty bad game to play against bot, very boring.
        Console.WriteLine("  5. Exit Program");
    }
    public static void GameSelect()
    {
        Instructions();
        string? input = Console.ReadLine();
        switch (input)
        {
            case "1":
                BlackJack();
                break;
            case "2":
                Poker();
                break;
            case "3":
                GoFish();
                break;
            case "4":
                Console.WriteLine("War is not implemented yet");
                break;
            case "5":
                Console.WriteLine("Closing Program");
                return;
            default:
                Console.WriteLine("Invalid selection, please try again.");
                GameSelect();
                break;
        }
    }

    public static void PlayAgain()
    {
        string playAgain = Console.ReadLine();

        switch (playAgain)
        {
            case "y":
                GameSelect();
                break;
            case "n":
                Console.WriteLine("Closing Program");
                break;
            default:
                Console.WriteLine("Invalid input, please try again.");
                PlayAgain();
                break;
        }
    }

    public static void BlackJack()
    {
        var deck = CardDeck.GenerateDeck();
        int playerScore = 0;
        int dealerScore = 0;

        // Initial deal
        playerScore += DrawCardValue(deck);
        playerScore += DrawCardValue(deck);
        dealerScore += DrawCardValue(deck);

        Console.WriteLine($"Your score: {playerScore}");
        Console.WriteLine($"Dealer shows: {dealerScore}");

        // Player's turn
        while (playerScore < 21)
        {
            Console.Write("Hit or Stand? ");
            string? input = Console.ReadLine()?.ToLower();

            if (input == "hit")
            {
                int cardValue = DrawCardValue(deck);
                playerScore += cardValue;
                Console.WriteLine($"Drew card worth {cardValue}, total: {playerScore}");
            }
            else if (input == "stand")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        // Dealer's turn
        while (dealerScore < 17)
        {
            dealerScore += DrawCardValue(deck);
        }

        Console.WriteLine($"Your total: {playerScore}, Dealer total: {dealerScore}");
        //Win Handling
        if (playerScore > 21)
            Console.WriteLine("Bust! Dealer wins.");
        else if (dealerScore > 21 || playerScore > dealerScore)
            Console.WriteLine("You win!");
        else if (dealerScore > playerScore)
            Console.WriteLine("Dealer wins!");
        else
            Console.WriteLine("It's a tie.");

        Console.WriteLine();
        Console.WriteLine("Would you like to play again? y/n");
        PlayAgain();
    }

    public static int DrawCardValue(List<string> deck)
    {
        string card = deck[0];
        deck.RemoveAt(0);
        string rank = card.Split(' ')[0];

        return rank switch
        {
            "Jack" or "Queen" or "King" => 10,
            "Ace" => 11, //TODO: Handle Ace as 1 or 11 based on context
            _ => int.Parse(rank)
        };
    }

    public static void Poker()
    {
        var deck = CardDeck.GenerateDeck();
        var playerHand = new List<string>();
        var botHand = new List<string>();

        // Deal initial hands
        for (int i = 0; i < 5; i++)
        {
            playerHand.Add(deck[0]);
            deck.RemoveAt(0);
            botHand.Add(deck[0]);
            deck.RemoveAt(0);
        }

        Console.WriteLine("\nYour hand:");
        for (int i = 0; i < playerHand.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {playerHand[i]}");
        }

        Console.WriteLine("\nEnter the numbers of the cards you want to discard (space-separated, max 3), or press Enter to keep all:");
        string? discardInput = Console.ReadLine();
        var indicesToDiscard = new List<int>();

        if (!string.IsNullOrWhiteSpace(discardInput))
        {
            var entries = discardInput.Split(' ');
            foreach (var entry in entries)
            {
                if (int.TryParse(entry, out int index) && index >= 1 && index <= 5)
                {
                    indicesToDiscard.Add(index - 1);
                }
            }
        }

        // Replace discarded cards
        indicesToDiscard.Sort((a, b) => b.CompareTo(a)); // remove from highest to avoid shifting indexes
        foreach (int index in indicesToDiscard)
        {
            playerHand.RemoveAt(index);
        }

        for (int i = playerHand.Count; i < 5 && deck.Count > 0; i++)
        {
            playerHand.Add(deck[0]);
            deck.RemoveAt(0);
        }

        Console.WriteLine("\nYour final hand:");
        foreach (var card in playerHand)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine("\nBot's hand:");
        foreach (var card in botHand)
        {
            Console.WriteLine(card);
        }

        // Simple evaluation: count highest rank value
        int playerScore = PokerEvaluate(playerHand);
        int botScore = PokerEvaluate(botHand);

        Console.WriteLine($"\nScore — You: {playerScore} | Bot: {botScore}");

        if (playerScore > botScore)
            Console.WriteLine("You win!");
        else if (botScore > playerScore)
            Console.WriteLine("Bot wins!");
        else
            Console.WriteLine("It's a tie.");

        Console.WriteLine("\nPlay again? y/n");
        PlayAgain();

    }
    public static int PokerEvaluate(List<string> hand)
    {
        var rankValues = new Dictionary<string, int>
    {
        { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 }, { "6", 6 },
        { "7", 7 }, { "8", 8 }, { "9", 9 }, { "10", 10 },
        { "Jack", 11 }, { "Queen", 12 }, { "King", 13 }, { "Ace", 14 }
    };

        return hand.Select(card => rankValues[card.Split(' ')[0]]).Max();
    }

    public static void GoFish() 
    {
        var deck = CardDeck.GenerateDeck();
        var playerhand = new List<string>();
        var computerhand = new List<string>();

        // deal intial hand (5 cards each)
        for (int i = 0; i < 5; i++)
        {
            playerhand.Add(deck[0]);
            deck.RemoveAt(0);
            computerhand.Add(deck[0]);
            deck.RemoveAt(0);
        }

        while (deck.Count > 0 && playerhand.Count > 0 && computerhand.Count > 0)
        {
            Console.WriteLine("\nYour hand:");
            foreach (var card in playerhand)
            {
                Console.Write(" " + card);
            }
            Console.WriteLine();

            Console.Write("\nAsk for a rank (e.g. 'ace', '7', 'King')");
            string? rank = Console.ReadLine()?.ToLower();

            var matching = computerhand.Where(c => c.StartsWith(rank));
            if (matching.Count() > 0)
            {
                Console.WriteLine($"Bot gives you {matching.Count()} card(s) with rank {rank}");
                foreach (var card in matching)
                {
                    playerhand.Add(card);
                    computerhand.Remove(card);
                }
            }
            else
            {
                Console.WriteLine("Go Fish! Drawing a card from the deck.");
                if (deck.Count > 0)
                {
                    playerhand.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }

            //TODO: actually make the game work
        }
    }
    public static void CollectPairs(List<string> hand, List<string> pairs)
    {
        var grouped = hand.GroupBy(c => c.Split(' ')[0])
                          .Where(g => g.Count() >= 2);

        foreach (var group in grouped)
        {
            int count = group.Count();
            int pairsToRemove = count - (count % 2); // Remove pairs of cards

            for (int i = 0; i < pairsToRemove; i++)
            {
                var card = hand.First(c => c.StartsWith(group.Key));
                hand.Remove(card);
                pairs.Add(card); // Add the removed card to the pairs list
            }
        }
    }
}