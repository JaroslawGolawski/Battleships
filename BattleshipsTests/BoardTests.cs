using Battleships;

namespace BattleshipsTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void TestPlaceShipsRandomly()
        {
            Board board = new Board();
            List<Ship> ships = new List<Ship>()
        {
            new Ship("Battleship", 5),
            new Ship("Destroyer 1", 4),
            new Ship("Destroyer 1", 4)
        };

            board.PlaceShipsRandomly(ships);

            // Check that all ships are placed on the board
            foreach (Ship ship in ships)
            {
                var sumSizes = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (board.board[i, j] == 'S')
                        {
                            sumSizes++;
                        }
                    }
                }

                Assert.AreEqual(13, sumSizes);
            }
        }

        [TestMethod]
        public void TestTakeShot_Hit()
        {
            Board board = new Board();
            Ship ship = new Ship("Destroyer 1", 4);
            board.PlaceShip(ship, 0, 0, true);

            ShotResult result = board.TakeShot(0, 0);

            Assert.AreEqual(ShotResult.Hit, result);
            Assert.AreEqual('H', board.board[0, 0]);
        }

        [TestMethod]
        public void TestTakeShot_Miss()
        {
            Board board = new Board();
            Ship ship = new Ship("Destroyer 1", 4);
            board.PlaceShip(ship, 0, 0, true);

            ShotResult result = board.TakeShot(1, 1);

            Assert.AreEqual(ShotResult.Miss, result);
            Assert.AreEqual('O', board.board[1, 1]);
        }

        [TestMethod]
        public void TestTakeShot_Sink()
        {
            Board board = new Board();
            Ship ship = new Ship("Destroyer 1", 4);
            board.PlaceShip(ship, 0, 0, true);

            board.TakeShot(0, 0);
            board.TakeShot(0, 1);
            board.TakeShot(0, 2);
            ShotResult result = board.TakeShot(0, 3);

            Assert.AreEqual(ShotResult.Sink, result);
            Assert.AreEqual('H', board.board[0, 0]);
            Assert.AreEqual('H', board.board[0, 1]);
            Assert.AreEqual('H', board.board[0, 2]);
            Assert.AreEqual('H', board.board[0, 3]);
        }

        
        [TestMethod]
        public void TestAllShipsSunk_False()
        {
            Board board = new Board();
            Ship ship = new Ship("Destroyer 1", 4);
            board.PlaceShip(ship, 0, 0, true);

            bool allSunk = board.AllShipsSunk();

            Assert.IsFalse(allSunk);
        }

        [TestMethod]
        public void TestAllShipsSunk_True()
        {
            Board board = new Board();
            Ship ship = new Ship("Destroyer 1", 4);
            board.PlaceShip(ship, 0, 0, true);
            board.TakeShot(0, 0);
            board.TakeShot(0, 1);
            board.TakeShot(0, 2);
            board.TakeShot(0, 3);

            bool allSunk = board.AllShipsSunk();

            Assert.IsTrue(allSunk);
        }
    }
}

