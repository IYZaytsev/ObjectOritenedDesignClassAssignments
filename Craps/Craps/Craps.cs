using System;
using System.IO;
// Ivan Zaytsev
// The purpose of this code is to 
// code the game of Craps in C#
namespace Craps
{
    class Craps
    {
        static void Main(string[] args)
        {   // rnd is used for dice rolls, 
            // isGameOver controls main game loop
            // chips stores number of chips player has
            // wager stores the player's wager
            Random rnd = new Random();
            bool isGameOver = false;
            int chips = 100;
            string titleScreen = File.ReadAllText(@".\TitleScreen.txt");
            Console.WriteLine(titleScreen);
            int wager = 0;
            string response = "";
            while (!isGameOver)
            {   

                // Ending the game if the player runs out of chips
                if (chips < 1)
                {
                    Console.WriteLine("Out of Chips");
                    break;
                }
                Console.WriteLine(String.Format("Chips left:{0}", chips));
                Console.WriteLine("Please Enter your wager or press x to leave the game");
                response = Console.ReadLine();
                if (response == "x")
                {
                    break;
                }
                wager = Convert.ToInt32(response);
                int round = 0;
                int point = 0;
                // Loop for the rounds in a game
                while (true)
                {   // Rolling the dice
                    int dice1 = rnd.Next(1,6);
                    int dice2 = rnd.Next(1,6);
                    int sum = dice1 + dice2;
                    // Output String being formatted
                    string output = String.Format("Dice 1:{0} Dice 2: {1} Sum:{2} \nWager:{3} Chips left: {4}", dice1, dice2, sum, wager, chips);
                    Console.WriteLine(DrawCubes(dice1, dice2));
                    Console.WriteLine(output);

                    // Displaying the users point after each roll 
                    if (point != 0)
                    {
                        Console.WriteLine(String.Format("Point is {0}\n", point));
                    }
                    
                    // Checking first round special cases
                    if (round == 0)
                    {   // If sum is equal to 7 or 11 on first round the player wins
                        if (sum == 7 || sum == 11)
                        {
                            chips += 2*wager;
                            Console.WriteLine("\n");
                            Console.WriteLine(DrawYouWon());
                            Console.WriteLine(String.Format("Chips Won: {0}", 2*wager));
                            break;
                        }
                        // If sum is equal to 2, 3 ,12 on the first round the player loses
                        if (sum == 2 || sum == 3 || sum == 12)
                        {
                            chips -= wager;
                            Console.WriteLine("\n");
                            Console.WriteLine(DrawYouLost());
                            Console.WriteLine(String.Format("Chips Lost: {0}", wager));
                            break;
                        }
                        // Setting the player's "point" to sum of dice
                        point = dice1 + dice2;
                        Console.WriteLine(String.Format("Your Point is: {0}", point));
                        round++;
                        continue;
                    }
                    round++;
                    // Player won the round
                    if (sum == point)
                    {
                        chips += 2 * wager;
                        Console.WriteLine("\n");
                        Console.WriteLine(DrawYouWon());
                        Console.WriteLine(String.Format("Chips Won: {0}", 2 * wager));
                        break;
                    }
                    // Player lost the round
                    if (sum == 7)
                    {
                        chips -= wager;
                        Console.WriteLine("\n");
                        Console.WriteLine(DrawYouLost());
                        Console.WriteLine(String.Format("Chips Lost: {0}", wager));
                        break;
                    }

                    // Getting user input to roll
                    Console.WriteLine("Press Enter to Roll...");
                    Console.ReadKey();

                }
                Console.WriteLine("Would you like to play again? Enter y/n");
                response = Console.ReadLine();
                if (response == "n")
                {
                    break;
                }


            }
            Console.WriteLine(String.Format("End of Game Chips: {0}", chips));
            Console.WriteLine("Thanks for playing");
            Console.Write("------------GAME OVER------------");

        }

         static string DrawCubes(int num1, int num2)
        {   // Reads ASCI art from file
            // and then modifies it
            // to represent the cubes in current round
            string cube = File.ReadAllText(@".\cube1.txt");
            string number1 = String.Format(@"/\  {0}\", num1);
            string number2 = String.Format(@"/ {0}  /\", num2);
            cube = cube.Replace(@"/\' .\", number1);
            cube = cube.Replace(@"/ .  /\", number2);

            return cube;
        }
        static string DrawYouWon()
        {   // Reads ASCI art from file
            string youWon = File.ReadAllText(@".\youWon.txt");
            return youWon;
        }
        static string DrawYouLost()
        {   // Reads ASCI art from file
            string youLost = File.ReadAllText(@".\youLost.txt");
            return youLost;
        }
    }
}
