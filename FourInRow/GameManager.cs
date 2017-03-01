using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow
{
    internal class GameManager
    {
        public enum eBoardTerms
        {
            MinRows = 4,
            MinCols = 4,
            MaxRows = 10,
            MaxCols = 10
        }

        public static int GetRandomValue(int i_Min, int i_Max)
        {
            Random rndCol = new Random();

            return rndCol.Next(i_Min, i_Max);
        }

        public static void StartGame(byte i_NumOfRows, byte i_NumOfCols, string i_Player1Name, string i_Player2Name, out Player o_Player1, out Player o_Player2, out Board o_Board)
        {
            Board board;
            Player player1, player2;

            board = new Board(i_NumOfRows, i_NumOfCols);
            player1 = new Player((byte)Player.eTypeOfPlayer.HumanPlayer, (char)Player.eSignOfPlayer.SignOfPlayer1, i_Player1Name);
            player2 = new Player((byte)Player.eTypeOfPlayer.HumanPlayer, (char)Player.eSignOfPlayer.SignOfPlayer2, i_Player2Name);

            o_Board = board;
            o_Player1 = player1;
            o_Player2 = player2;
        }

        public static void PlaySpecificTurn(ref Player io_Player1, ref Player io_Player2, ref Board io_Board, int i_Turn, byte i_ChosenCol, out byte o_RowToInsert, out char o_DiscSign)
        {
            if (i_Turn % 2 == 1)
            {
                o_DiscSign = io_Player1.DiscSign;
            }
            else
            {
                o_DiscSign = io_Player2.DiscSign;
            }

            io_Board.InsertNewDisc(i_ChosenCol, o_DiscSign, out o_RowToInsert);
        }

        public static void HandleWinSituation(ref Player io_Player1, ref Player io_Player2, char i_SignOfWinner)
        {
            if (i_SignOfWinner == (char)Player.eSignOfPlayer.SignOfPlayer1)
            {
                io_Player1.Score++;
            }
            else
            {
                io_Player2.Score++;
            }
        }
    }
}
