using ConsoleApp1;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Program
{
    public static void Main(string[] args)
    {
        GameSelect();
        Console.WriteLine("This Program allows the user to play a card game");
        Console.WriteLine("Enter 5 to stop");
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
        //TODO: using CardGenerator() play a game of poker
    }
    public static void GoFish() 
    {
        //TODO: using CardGenerator() play a game of Go Fish
    }
}