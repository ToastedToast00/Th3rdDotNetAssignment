using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Program
{
    public static void Main(string[] args)
    {
        GameSelect();
    }
    public static void Instructions() 
    {
        Console.WriteLine("This Program allows the user to play a card game");
        Console.WriteLine("Enter 5 to stop");
        Console.WriteLine("please select a game to play");
        Console.WriteLine();
        Console.WriteLine("  1. Black Jack");
        Console.WriteLine("  2. Poker");
        Console.WriteLine("  3. Go Fish");
        Console.WriteLine("  4. War");//war is a pretty bad game to play against bot, very boring.
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
                War();
                break;
            default:
                Console.WriteLine("Invalid selection, please try again.");
                GameSelect();
                break;
        }
    }
    public static void BlackJack()
    {
        //TODO: using CardGenerator() play a game of blackjack
    }
    public static void Poker()
    {
        //TODO: using CardGenerator() play a game of poker
    }
    public static void GoFish() 
    {
        //TODO: using CardGenerator() play a game of Go Fish
    }
    public static void War() 
    {
        //TODO: using CardGenerator() play a game of War
    }
}