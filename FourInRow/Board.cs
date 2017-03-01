using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow
{
    internal class Board
    {
        public struct Square
        {
            private readonly byte r_RowNumber;
            private readonly byte r_ColNumber;
            private char m_DiscSign;

            public Square(byte i_RowNumber, byte i_ColNumber)
            {
                r_RowNumber = i_RowNumber;
                r_ColNumber = i_ColNumber;
                m_DiscSign = (char)Player.eSignOfPlayer.SignOfBlank;
            }

            public byte RowNumber
            {
                get
                {
                    return r_RowNumber;
                }
            }

            public byte ColNumber
            {
                get
                {
                    return r_ColNumber;
                }
            }

            public char Sign
            {
                get { return m_DiscSign; }
                set { m_DiscSign = value; }
            }
        }

        private List<Square> m_ListOfWinnerPath;
        private byte r_NumOfRows;
        private byte r_NumOfCols;
        private Square[,] m_GameBoard;
        private int m_NumOfDiscs = 0;

        public Board(byte i_NumOfRows, byte i_NumOfCols)
        {
            r_NumOfRows = i_NumOfRows;
            r_NumOfCols = i_NumOfCols;
            m_GameBoard = new Square[r_NumOfRows, r_NumOfCols];
            m_ListOfWinnerPath = new List<Square>();
            initBoard();
        }

        public List<Square> WinnerPath
        {
            get
            {
                return m_ListOfWinnerPath;
            }
        }

        public byte NumOfRows
        {
            get { return r_NumOfRows; }
            set { r_NumOfRows = value; }
        }

        public byte NumOfCols
        {
            get { return r_NumOfCols; }
            set { r_NumOfCols = value; }
        }

        public Square[,] GameBoard
        {
            get { return m_GameBoard; }
        }

        private void initBoard()
        {
            for (byte rowIndex = 0; rowIndex < r_NumOfRows; rowIndex++)
            {
                for (byte colIndex = 0; colIndex < r_NumOfCols; colIndex++)
                {
                    m_GameBoard[rowIndex, colIndex] = new Square(rowIndex, colIndex);
                }
            }
        }

        public void InitBoardForNewGame()
        {
            for (byte rowIndex = 0; rowIndex < r_NumOfRows; rowIndex++)
            {
                for (byte colIndex = 0; colIndex < r_NumOfCols; colIndex++)
                {
                    m_GameBoard[rowIndex, colIndex].Sign = (char)Player.eSignOfPlayer.SignOfBlank;
                }
            }

            m_NumOfDiscs = 0;
        }

        public void InsertNewDisc(byte i_ColNum, char i_DiscSign, out byte o_RowToInsert)
        {
            o_RowToInsert = findRowNumberToInsertNewDisc(i_ColNum);

            m_GameBoard[o_RowToInsert, i_ColNum].Sign = i_DiscSign;
            m_NumOfDiscs++;
        }

        private byte findRowNumberToInsertNewDisc(byte i_ColNum)
        {
            byte rowNum = r_NumOfRows;

            for (int rowIndex = r_NumOfRows - 1; rowIndex >= 0; rowIndex--)
            {
                if (m_GameBoard[rowIndex, i_ColNum].Sign == (char)Player.eSignOfPlayer.SignOfBlank)
                {
                    rowNum = (byte)rowIndex;
                    break;
                }
            }

            return rowNum;
        }

        public bool CheckIfThereIsWinner(byte i_ChosenCol, out char o_SignOfWinner)
        {
            bool thereIsWinner = true;
            byte rowToFind;

            findLastInsertedRow(out rowToFind, i_ChosenCol);
            thereIsWinner = checkIfThereIs4(rowToFind, i_ChosenCol, out o_SignOfWinner);

            return thereIsWinner;
        }

        private void findLastInsertedRow(out byte o_RowToFind, byte i_ChosenCol)
        {
            o_RowToFind = r_NumOfRows;

            if (i_ChosenCol < r_NumOfCols)
            {
                for (int rowIndex = 0; rowIndex < r_NumOfRows; rowIndex++)
                {
                    if (m_GameBoard[rowIndex, i_ChosenCol].Sign != (char)Player.eSignOfPlayer.SignOfBlank)
                    {
                        o_RowToFind = (byte)rowIndex;
                        break;
                    }
                }
            }
        }

        private bool checkIfThereIs4(byte i_Row, byte i_Col, out char o_SignOfWinner)
        {
            char signOfSpecificSquare;
            bool isWinner = false;

            if (i_Row == r_NumOfRows)
            {
                isWinner = false;
                o_SignOfWinner = (char)Player.eSignOfPlayer.SignOfBlank;
            }
            else
            {
                signOfSpecificSquare = m_GameBoard[i_Row, i_Col].Sign;
                o_SignOfWinner = signOfSpecificSquare;

                checkHorizontal(i_Row, i_Col, signOfSpecificSquare, ref isWinner);

                if (isWinner == false)
                {
                    checkVertical(i_Row, i_Col, signOfSpecificSquare, ref isWinner);
                }

                if (isWinner == false)
                {
                    checkUpLeft(i_Row, i_Col, signOfSpecificSquare, ref isWinner);
                }

                if (isWinner == false)
                {
                    checkUpRight(i_Row, i_Col, signOfSpecificSquare, ref isWinner);
                }
            }

            return isWinner;
        }

        private void checkHorizontal(byte i_Row, byte i_Col, char i_SignOfSpecificSquare, ref bool io_Winner)
        {
            int counter = 0;
            byte colIndex = 0;

            while (i_Col + colIndex < r_NumOfCols && m_GameBoard[i_Row, i_Col + colIndex].Sign == i_SignOfSpecificSquare)
            {
                m_ListOfWinnerPath.Add(new Square(i_Row, (byte)(i_Col + colIndex)));
                counter++;
                colIndex++;
            }

            colIndex = 1;

            while (i_Col - colIndex >= 0 && m_GameBoard[i_Row, i_Col - colIndex].Sign == i_SignOfSpecificSquare)
            {
                m_ListOfWinnerPath.Add(new Square(i_Row, (byte)(i_Col - colIndex)));
                counter++;
                colIndex++;
            }

            if (counter >= 4)
            {
                io_Winner = true;
            }
            else
            {
                m_ListOfWinnerPath.Clear();
            }
        }

        private void checkVertical(byte i_Row, byte i_Col, char i_SignOfSpecificSquare, ref bool io_Winner)
        {
            int counter = 0;
            byte rowIndex = 0;

            while (i_Row + rowIndex < r_NumOfRows && m_GameBoard[i_Row + rowIndex, i_Col].Sign == i_SignOfSpecificSquare)
            {
                m_ListOfWinnerPath.Add(new Square((byte)(i_Row + rowIndex), i_Col));
                counter++;
                rowIndex++;
            }

            rowIndex = 1;

            while (i_Row - rowIndex >= 0 && m_GameBoard[i_Row - rowIndex, i_Col].Sign == i_SignOfSpecificSquare)
            {
                m_ListOfWinnerPath.Add(new Square((byte)(i_Row - rowIndex), i_Col));
                counter++;
                rowIndex++;
            }

            if (counter >= 4)
            {
                io_Winner = true;
            }
            else
            {
                m_ListOfWinnerPath.Clear();
            }
        }

        private void checkUpLeft(byte i_Row, byte i_Col, char i_SignOfSpecificSquare, ref bool io_Winner)
        {
            byte rowIndex = 0, colIndex = 0;
            int counter = 0;

            while (i_Row - rowIndex >= 0 && i_Col - colIndex >= 0 && m_GameBoard[i_Row - rowIndex, i_Col - colIndex].Sign == i_SignOfSpecificSquare)
            {
                m_ListOfWinnerPath.Add(new Square((byte)(i_Row - rowIndex), (byte)(i_Col - colIndex)));
                counter++;
                rowIndex++;
                colIndex++;
            }

            rowIndex = 1;
            colIndex = 1;

            while (i_Row + rowIndex < r_NumOfRows && i_Col + colIndex < r_NumOfCols && m_GameBoard[i_Row + rowIndex, i_Col + colIndex].Sign == i_SignOfSpecificSquare)
            {
                m_ListOfWinnerPath.Add(new Square((byte)(i_Row + rowIndex), (byte)(i_Col + colIndex)));
                counter++;
                rowIndex++;
                colIndex++;
            }

            if (counter >= 4)
            {
                io_Winner = true;
            }
            else
            {
                m_ListOfWinnerPath.Clear();
            }
        }

        private void checkUpRight(byte i_Row, byte i_Col, char i_SignOfSpecificSquare, ref bool io_Winner)
        {
            byte rowIndex = 0, colIndex = 0;
            int counter = 0;

            while (i_Row - rowIndex >= 0 && i_Col + colIndex < r_NumOfCols && m_GameBoard[i_Row - rowIndex, i_Col + colIndex].Sign == i_SignOfSpecificSquare)
            {
                m_ListOfWinnerPath.Add(new Square((byte)(i_Row - rowIndex), (byte)(i_Col + colIndex)));
                counter++;
                rowIndex++;
                colIndex++;
            }

            rowIndex = 1;
            colIndex = 1;

            while (i_Row + rowIndex < r_NumOfRows && i_Col - colIndex >= 0 && m_GameBoard[i_Row + rowIndex, i_Col - colIndex].Sign == i_SignOfSpecificSquare)
            {
                m_ListOfWinnerPath.Add(new Square((byte)(i_Row + rowIndex), (byte)(i_Col - colIndex)));
                counter++;
                rowIndex++;
                colIndex++;
            }

            if (counter >= 4)
            {
                io_Winner = true;
            }
            else
            {
                m_ListOfWinnerPath.Clear();
            }
        }

        public bool CheckIfFull()
        {
            return m_NumOfDiscs == r_NumOfRows * r_NumOfCols;
        }

        public void InitAfterBoardSizeChanged()
        {
            m_GameBoard = new Square[r_NumOfRows, r_NumOfCols];
            initBoard();
        }
    }
}
