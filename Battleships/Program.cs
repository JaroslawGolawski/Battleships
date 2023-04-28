using Battleships;

namespace BattleshipGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Battleship game!\n");
            Console.WriteLine("In this game, you will be playing against the computer. The computer has placed several ships on a 10x10 grid and your task is to sink all of them.\n");
            Console.WriteLine("Here are the rules:");
            Console.WriteLine("- There are 3 ships in total: 1 battleship (5 squares) and 2 destroyers (4 squares each).");
            Console.WriteLine("- You will enter the coordinates of a square to target (e.g. A5) and the computer will tell you if you hit a ship, missed, or sunk a ship.");
            Console.WriteLine("- The game ends when all ships are sunk.\n");

            // Create the game board and ships
            Board gameBoard = new Board();
            Ship battleship = new Ship("Battleship", 5);
            Ship destroyer1 = new Ship("Destroyer 1", 4);
            Ship destroyer2 = new Ship("Destroyer 2", 4);
            List<Ship> ships = new List<Ship>() { battleship, destroyer1, destroyer2 };
            gameBoard.PlaceShipsRandomly(ships);
            gameBoard.PrintBoard();

            // Play the game
            while (!gameBoard.AllShipsSunk())
            {
                Console.WriteLine("Enter a coordinate to target (e.g. A5):");
                string input = Console.ReadLine();
                if (!IsValidInput(input))
                {
                    Console.WriteLine("Invalid input. Please enter a coordinate of the form 'A5'.\n");
                    continue;
                }
                int row = input[0] - 'A';
                int col = int.Parse(input.Substring(1)) - 1;

                ShotResult result = gameBoard.TakeShot(row, col);
                switch (result)
                {
                    case ShotResult.Hit:
                        gameBoard.PrintBoard();
                        Console.WriteLine("You hit a ship!\n");
                        break;
                    case ShotResult.Miss:
                        gameBoard.PrintBoard();
                        Console.WriteLine("You missed.\n");
                        break;
                    case ShotResult.Sink:
                        gameBoard.PrintBoard();
                        Console.WriteLine("You sunk a ship!\n");
                        break;
                    default:
                        break;
                }
            }

            // Game over
            Console.WriteLine("Congratulations, you won the game!");
        }

        // Check if user input is valid
        static bool IsValidInput(string input)
        {
            if (input.Length != 2 && input.Length != 3)
            {
                return false;
            }
            if (input[0] < 'A' || input[0] > 'J')
            {
                return false;
            }
            if (!int.TryParse(input.Substring(1), out int result) || result < 1 || result > 10)
            {
                return false;
            }
            return true;
        }
    }
}


