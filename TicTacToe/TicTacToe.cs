using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class TicTacToe
    {
        private int currentPlayer;

        private string[,] board = new string[,]
        {
            { "1", "2", "3" },
            { "4", "5", "6" },
            { "7", "8", "9" } 
        };

        private bool GameHasWinner {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    // Horizontal check
                    if (this.board[i, 0] == this.board[i, 1] && this.board[i, 1] == this.board[i, 2])
                    {
                        return true;
                    }

                    // Vertical check
                    if (this.board[0, i] == this.board[1, i] && this.board[1, i] == this.board[2, i])
                    {
                        return true;
                    }
                }

                // Diagonal check starting from top left
                if (this.board[0, 0] == this.board[1, 1] && this.board[1, 1] == this.board[2, 2])
                {
                    return true;
                }

                // Diagonal check starting from top right
                if (this.board[0, 2] == this.board[1, 1] && this.board[1, 1] == this.board[2, 0])
                {
                    return true;
                }

                return false;
            }
        }

        private bool GameIsADraw
        {
            get
            {
                foreach (string item in this.board)
                {
                    if (item != "X" && item != "O")
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private string CurrentPlayerSymbol
        {
            get
            {
                return this.currentPlayer == 1 ? "O" : "X";
            }
        }

        public TicTacToe()
        {
            this.currentPlayer = 1;
        }

        public void Run()
        {
            while (!this.GameHasWinner && !this.GameIsADraw)
            {
                this.DisplayGameBoard();

                this.GetFieldSelectionForCurrentPlayer();

                if (!this.GameHasWinner && !this.GameIsADraw)
                {
                    this.TogglePlayer();
                }
            }

            this.DisplayGameBoard();

            Console.WriteLine();

            if (this.GameHasWinner)
                Console.WriteLine("Player {0} has won!", this.currentPlayer);
            else
                Console.WriteLine("The game has ended in a draw!");

            Console.WriteLine("Press any key to reset the game");
            Console.ReadKey();

            this.ResetGameBoard();
            this.Run();
        }

        private void TogglePlayer()
        {
            if (this.currentPlayer == 1)
            {
                this.currentPlayer = 2;
                return;
            }

            this.currentPlayer = 1;
        }

        private void DisplayGameBoard()
        {
            Console.Clear();

            int finalRowIndex = this.board.GetLength(0) - 1;

            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                bool displayBottomBorder = i == finalRowIndex ? false : true;

                Console.WriteLine("     |     |     ");
                Console.WriteLine(
                    "  {0}  |  {1}  |  {2}  ",
                    this.board[i, 0],
                    this.board[i, 1],
                    this.board[i, 2]
                );

                if (displayBottomBorder)
                {
                    Console.WriteLine("_____|_____|_____");
                }
                else
                {
                    Console.WriteLine("     |     |     ");
                }
            }
        }

        private void ResetGameBoard()
        {
            int currentTile = 1;

            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    this.board[i, j] = $"{currentTile}";
                    currentTile++;
                }
            }
        }

        private void GetFieldSelectionForCurrentPlayer()
        {
            string selection = "";

            while (true)
            {
                Console.WriteLine();
                Console.Write("Player {0}: Choose your field! ", this.currentPlayer);
                selection = Console.ReadLine() ?? "";

                int numericSelection = 0;
                if (!int.TryParse(selection, out numericSelection))
                {
                    Console.WriteLine("Please enter a number!");
                }

                if (!this.UpdateFieldForCurrentPlayer(selection))
                {
                    Console.WriteLine();
                    Console.WriteLine(" Incorrect input! Please select another field!");
                }
                else
                {
                    break;
                }
            }
        }

        private bool UpdateFieldForCurrentPlayer(string selection)
        {
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    if (this.board[i, j] == selection)
                    {
                        this.board[i, j] = this.CurrentPlayerSymbol;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
