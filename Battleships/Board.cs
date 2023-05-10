namespace Battleships
{
    public class Board
    {
        public char[,] board { get; set; }

        public Board()
        {
            board = new char[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = '-';
                }
            }
        }

        public void PlaceShipsRandomly(List<Ship> ships)
        {
            Random rand = new Random();
            foreach (Ship ship in ships)
            {
                bool placed = false;
                while (!placed)
                {
                    int row = rand.Next(10);
                    int col = rand.Next(10);
                    bool horizontal = rand.Next(2) == 0;
                    if (CanPlaceShip(ship, row, col, horizontal))
                    {
                        PlaceShip(ship, row, col, horizontal);
                        placed = true;
                    }
                }
            }
        }

        // Check if a ship can be placed at a given location
        private bool CanPlaceShip(Ship ship, int row, int col, bool horizontal)
        {
            int len = ship.Size;
            if (horizontal)
            {
                if (col + len > 10)
                {
                    return false;
                }
                for (int j = col; j < col + len; j++)
                {
                    if (board[row, j] != '-')
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (row + len > 10)
                {
                    return false;
                }
                for (int i = row; i < row + len; i++)
                {
                    if (board[i, col] != '-')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Place a ship at a given location
        public void PlaceShip(Ship ship, int row, int col, bool horizontal)
        {
            int len = ship.Size;
            if (horizontal)
            {
                for (int j = col; j < col + len; j++)
                {
                    board[row, j] = 'S';
                }
            }
            else
            {
                for (int i = row; i < row + len; i++)
                {
                    board[i, col] = 'S';
                }
            }
        }

        // Take a shot at a given location
        public ShotResult TakeShot(int row, int col)
        {
            if (board[row, col] == 'S')
            {
                board[row, col] = 'H';
                if (AllShipsSunk())
                {
                    return ShotResult.Sink;
                }
                else
                {
                    return ShotResult.Hit;
                }
            }
            else
            {
                board[row, col] = 'O';
                return ShotResult.Miss;
            }
        }

        // Check if all ships are sunk
        public bool AllShipsSunk()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (board[i, j] == 'S')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Print the game board
        public void PrintBoard()
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < 10; j++)
                {
                    if (board[i, j] == 'S')
                    {
                        Console.Write("- ");
                    }
                    else if (board[i, j] == 'H')
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write(board[i, j] + " ");
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
