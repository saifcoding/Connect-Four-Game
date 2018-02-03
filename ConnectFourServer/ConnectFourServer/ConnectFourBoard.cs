﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    public enum Color { None, Red, Black };

    public class ConnectFourBoard
    {
        public Color[,] GameBoard { get; private set; }

        public ConnectFourBoard(int rows, int cols)
        {
            // Instantiate an empty board
            GameBoard = new Color[rows, cols];
            for (int row = 0; row < this.GameBoard.GetLength(0); row++)
                for (int col = 0; col < this.GameBoard.GetLength(1); col++)
                    this.GameBoard[row, col] = Color.None;
        }

        // check first column, if first column is full then the board is full
        // then its tie
        public bool Tied()
        {
            for (int col = 0; col < this.GameBoard.GetLength(1); col++)
                if (GameBoard[0, col] == Color.None)
                    return false;
            return true;
        }

        public bool Winner(int row, int col)
        {
            return ((VerticalConnectFour(row, col) ||
                HorizontalConnectFour(row, col) ||
                ForwardDiagonalConnectFour(row, col) ||
                BackwardDiagonalConnectFour(row, col)));
        }

        #region Winner() function method helpers

        private bool VerticalConnectFour(int row, int col)
        {
            if (GameBoard[row, col] == Color.None)
                return false;
            int count = 1;
            int rowCursor = row - 1;
            rowCursor = row + 1;
            while (rowCursor < GameBoard.GetLength(0) && GameBoard[rowCursor, col] == GameBoard[row, col])
            {
                count++;
                rowCursor++;
            }
            if (count < 4)
                return false;
            return true;
        }

        private bool HorizontalConnectFour(int row, int col)
        {
            if (GameBoard[row, col] == Color.None)
                return false;
            int count = 1;
            //check left to inserted cell
            int colCursor = col - 1;
            while (colCursor >= 0 && GameBoard[row, colCursor] == GameBoard[row, col])
            {
                count++;
                colCursor--;
            }
            // check right to inserted cell
            colCursor = col + 1;
            while (colCursor < GameBoard.GetLength(1) && GameBoard[row, colCursor] == GameBoard[row, col])
            {
                count++;
                colCursor++;
            }
            if (count < 4)
                return false;
            return true;
        }

        private bool ForwardDiagonalConnectFour(int row, int col)
        {
            if (GameBoard[row, col] == Color.None)
                return false;
            int count = 1;
            int rowCursor = row - 1;
            int colCursor = col + 1;
            while (rowCursor >= 0 && colCursor < GameBoard.GetLength(1) && GameBoard[rowCursor, colCursor] == GameBoard[row, col])
            {
                count++;
                rowCursor--;
                colCursor++;
            }
            rowCursor = row + 1;
            colCursor = col - 1;
            while (rowCursor < GameBoard.GetLength(0) && colCursor >= 0 && GameBoard[rowCursor, colCursor] == GameBoard[row, col])
            {
                count++;
                rowCursor++;
                colCursor--;
            }
            if (count < 4)
                return false;
            return true;
        }

        private bool BackwardDiagonalConnectFour(int row, int col)
        {
            if (GameBoard[row, col] == Color.None)
                return false;
            int count = 1;
            int rowCursor = row + 1;
            int colCursor = col + 1;
            while (rowCursor < GameBoard.GetLength(0) && colCursor < GameBoard.GetLength(1) && GameBoard[rowCursor, colCursor] == GameBoard[row, col])
            {
                count++;
                rowCursor++;
                colCursor++;
            }
            rowCursor = row - 1;
            colCursor = col - 1;
            while (rowCursor >= 0 && colCursor >= 0 && GameBoard[rowCursor, colCursor] == GameBoard[row, col])
            {
                count++;
                rowCursor--;
                colCursor--;
            }
            if (count < 4)
                return false;
            return true;
        }
        #endregion

        public int Insert(Color cell, int column)
        {
            // Start from bottom, work up
            for (int row = GameBoard.GetLength(0) - 1; row >= 0; row--)
            {
                if (GameBoard[row, column] == Color.None)
                {
                    GameBoard[row, column] = cell;
                    return row;
                }
            }
            return -1;
        }
    }
}
