using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace FourInRow
{
    internal class MatrixPanel : Panel
    {
        private PictureBox[,] pictureBoxBoard;
        private Region m_OriginalShape;
        private GraphicsPath m_SpecialShapeForEmptyCell;
        private bool m_isBoardSizeChanged = false;
        private int m_NumOfRows;
        private int m_NumOfCols;

        private void initForStart(byte i_NumOfRows, byte i_NumOfCols, GraphicsPath i_CirclePathForCoinPictureBox)
        {
            m_OriginalShape = new Region();
            Width = 67 * i_NumOfCols;
            Height = 67 * (i_NumOfRows + 1);
            Location = new Point(20, 40);
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.Thistle;
            m_NumOfRows = i_NumOfRows;
            m_NumOfCols = i_NumOfCols;

            pictureBoxBoard = new PictureBox[i_NumOfRows, i_NumOfCols];
            m_SpecialShapeForEmptyCell = GameForm.QuickCalculateGraphicsPath(Properties.Resources.EmptyCell);

            for (int rowIndex = 0; rowIndex < i_NumOfRows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_NumOfCols; colIndex++)
                {
                    pictureBoxBoard[rowIndex, colIndex] = new PictureBox();
                    pictureBoxBoard[rowIndex, colIndex].Image = Properties.Resources.EmptyCell;
                    m_OriginalShape = pictureBoxBoard[rowIndex, colIndex].Region;
                    pictureBoxBoard[rowIndex, colIndex].Region = new Region(m_SpecialShapeForEmptyCell);
                    pictureBoxBoard[rowIndex, colIndex].Name = GameForm.ImagesNames.k_EmptyCell;
                    pictureBoxBoard[rowIndex, colIndex].Size = new Size(67, 67);
                    pictureBoxBoard[rowIndex, colIndex].Location = new Point(colIndex * 67, 67 + (rowIndex * 67));
                    Controls.Add(pictureBoxBoard[rowIndex, colIndex]);
                }
            }
        }

        public MatrixPanel(byte i_NumOfRows, byte i_NumOfCols, GraphicsPath i_CirclePathForCoinPictureBox)
        {
            initForStart(i_NumOfRows, i_NumOfCols, i_CirclePathForCoinPictureBox);
        }

        internal void InitAfterBoardSizeChanged(int i_NumOfRowsChangedByProperties, int i_NumOfColsChangedByProperties)
        {
            for (int rowIndex = 0; rowIndex < m_NumOfRows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < m_NumOfCols; colIndex++)
                {
                    Controls.Remove(pictureBoxBoard[rowIndex, colIndex]);
                }
            }

            pictureBoxBoard = new PictureBox[i_NumOfRowsChangedByProperties, i_NumOfColsChangedByProperties];

            for (int rowIndex = 0; rowIndex < i_NumOfRowsChangedByProperties; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_NumOfColsChangedByProperties; colIndex++)
                {
                    pictureBoxBoard[rowIndex, colIndex] = new PictureBox();
                    pictureBoxBoard[rowIndex, colIndex].Image = Properties.Resources.EmptyCell;
                    pictureBoxBoard[rowIndex, colIndex].Region = new Region(m_SpecialShapeForEmptyCell);
                    pictureBoxBoard[rowIndex, colIndex].Name = GameForm.ImagesNames.k_EmptyCell;
                    pictureBoxBoard[rowIndex, colIndex].Size = new Size(67, 67);
                    pictureBoxBoard[rowIndex, colIndex].Location = new Point(colIndex * 67, 67 + (rowIndex * 67));
                    Controls.Add(pictureBoxBoard[rowIndex, colIndex]);
                }
            }

            m_NumOfRows = i_NumOfRowsChangedByProperties;
            m_NumOfCols = i_NumOfColsChangedByProperties;
            Width = 67 * i_NumOfColsChangedByProperties;
            Height = 67 * (i_NumOfRowsChangedByProperties + 1);
            m_isBoardSizeChanged = false;
        }

        internal void InitForNextRound(int i_NumOfRows, int i_NumOfCols)
        {
            for (int rowIndex = 0; rowIndex < i_NumOfRows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_NumOfCols; colIndex++)
                {
                    pictureBoxBoard[rowIndex, colIndex].Image = Properties.Resources.EmptyCell;
                    pictureBoxBoard[rowIndex, colIndex].Region = new Region(m_SpecialShapeForEmptyCell);
                    pictureBoxBoard[rowIndex, colIndex].Name = GameForm.ImagesNames.k_EmptyCell;
                }
            }

            m_isBoardSizeChanged = false;
        }

        internal void ChangePictureBoxBoard(byte i_RowToInsert, byte i_ChosenCol, char i_DiscSign)
        {
            pictureBoxBoard[i_RowToInsert, i_ChosenCol].Region = m_OriginalShape;

            if (i_DiscSign == (char)Player.eSignOfPlayer.SignOfPlayer1)
            {
                pictureBoxBoard[i_RowToInsert, i_ChosenCol].Image = Properties.Resources.FullCellRed;
                pictureBoxBoard[i_RowToInsert, i_ChosenCol].Name = GameForm.ImagesNames.k_FullCellRed;
            }
            else if (i_DiscSign == (char)Player.eSignOfPlayer.SignOfPlayer2)
            {
                pictureBoxBoard[i_RowToInsert, i_ChosenCol].Image = Properties.Resources.FullCellYellow;
                pictureBoxBoard[i_RowToInsert, i_ChosenCol].Name = GameForm.ImagesNames.k_FullCellYellow;
            }
        }

        public PictureBox[,] Board
        {
            get
            {
                return pictureBoxBoard;
            }
        }

        public bool BoardSizeChanged
        {
            get
            {
                return m_isBoardSizeChanged;
            }

            set
            {
                m_isBoardSizeChanged = value;
            }
        }
    }
}
