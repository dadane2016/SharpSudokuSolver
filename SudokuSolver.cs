using System;

namespace SudokuSolver
{
    public class Sudoku
    {
        private int[,] fboard = new int[9,9];

        public Sudoku()
        {
            Clear();
        }

        public void Clear()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    fboard[i, j] = 0;
                }
            }
        }

        public int Length
        {
            get { return fboard.GetLength(0); }
        }

        public int[,] GameMatrix
        {
            get { return fboard; }
            set { fboard = value; }
        }

        public bool isSafe(int row, int col, int num)
        {

            // Recherche dans la ligne
            for (int d = 0; d < fboard.GetLength(0); d++)
            {
                if (fboard[row, d] == num)
                {
                    return false;
                }
            }

            // Recherche dans la colonne
            for (int r = 0; r < fboard.GetLength(0); r++)
            {
                if (fboard[r, col] == num)
                {
                    return false;
                }
            }

            // Recherche dans le carré
            int sqrt = (int)Math.Sqrt(fboard.GetLength(0));
            int boxRowStart = row - row % sqrt;
            int boxColStart = col - col % sqrt;

            for (int r = boxRowStart;
                 r < boxRowStart + sqrt; r++)
            {
                for (int d = boxColStart;
                     d < boxColStart + sqrt; d++)
                {
                    if (fboard[r, d] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void print()
        {
            for (int r = 0; r < Length; r++)
            {
                for (int d = 0; d < Length; d++)
                {
                    Console.Write(fboard[r, d]);
                    Console.Write(" ");

                    if ((d + 1) % (int)Math.Sqrt(Length) == 0)
                    {
                        Console.Write(" ");
                    }

                }
                Console.WriteLine("");

                if ((r + 1) % (int)Math.Sqrt(Length) == 0)
                {
                    Console.WriteLine("");
                }
            }
        }

        public bool Solve()
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (fboard[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            if (isEmpty)
            {
                return true;
            }

            for (int num = 1; num <= Length; num++)
            {
                if (isSafe(row, col, num))
                {
                    fboard[row, col] = num;
                    if (Solve())
                    {
                        return true;
                    }
                    else
                    {
                        fboard[row, col] = 0;
                    }
                }
            }
            return false;
        }
    }
}
