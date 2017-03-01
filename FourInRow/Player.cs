using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow
{
    internal class Player
    {
        public enum eTypeOfPlayer
        {
            HumanPlayer = 0,
            PcPlayer = 1
        }

        public enum eSignOfPlayer
        {
            SignOfPlayer1 = 'X',
            SignOfPlayer2 = 'O',
            SignOfBlank = ' '
        }

        private string r_PlayerName;
        private readonly byte r_TypeOfPlayer;
        private readonly char r_DiscSign;
        private int m_Score = 0;

        public Player(byte i_TypeOfPlayer, char i_DiscSign, string i_PlayerName)
        {
            r_TypeOfPlayer = i_TypeOfPlayer;
            r_DiscSign = i_DiscSign;
            r_PlayerName = i_PlayerName;
        }

        public void ResetScore()
        {
            m_Score = 0;
        }

        public byte TypeOfPlayer
        {
            get
            {
                return r_TypeOfPlayer;
            }
        }

        public string Name
        {
            get
            {
                return r_PlayerName;
            }

            set
            {
                r_PlayerName = value;
            }
        }

        public char DiscSign
        {
            get
            {
                return r_DiscSign;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }
    }
}
