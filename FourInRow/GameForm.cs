using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace FourInRow
{
    internal class GameForm : Form
    {
        public struct ImagesNames
        {
            public const string k_EmptyCell = "Empty Cell";
            public const string k_FullCellRed = "FullCellRed";
            public const string k_FullCellYellow = "FullCellYellow";
        }

        private GameSettingsForm gameSettingsForm;
        private MatrixPanel matrixPanel;
        private Timer timerFall;
        private Timer timerBlink;
        private StatusBar statusBarPlayersInfo;
        private PictureBox pictureBoxFalling;
        private PictureBox pictureBoxHover;
        private Player m_RefToPlayer1;
        private Player m_RefToPlayer2;
        private Board m_RefToBoard;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem startANewGameToolStripMenuItem;
        private ToolStripMenuItem startANewTournirToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem howToPlayToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Label labelAbout;
        private int m_NumOfRowsChangedByProperties;
        private int m_NumOfColsChangedByProperties;
        private int m_Turn = 1;
        private int m_DelayOfMouseMove = 0;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            runGameSettingsForm();
        }

        public GameForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            gameSettingsForm = new GameSettingsForm();
        }

        private void initController()
        {
            if (matrixPanel.BoardSizeChanged)
            {
                initAfterBoardSizeChanged();
            }
            else
            {
                initForNextRound();
            }
        }

        private void initForStart(byte i_NumOfRows, byte i_NumOfCols)
        {
            statusBarPlayersInfo = new StatusBar();
            createStatusBar();
            Controls.Add(statusBarPlayersInfo);

            menuStrip1 = new System.Windows.Forms.MenuStrip();
            gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            startANewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            startANewTournirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            howToPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { gameToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(232, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.BackColor = this.BackColor;
            menuStrip1.BackgroundImage = FourInRow.Properties.Resources.MenuBar;
            menuStrip1.BackgroundImageLayout = ImageLayout.Stretch;
            menuStrip1.Text = "menuStrip1";

            gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { startANewGameToolStripMenuItem, startANewTournirToolStripMenuItem, propertiesToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            gameToolStripMenuItem.Text = "Game";

            startANewGameToolStripMenuItem.Name = "startANewGameToolStripMenuItem";
            startANewGameToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            startANewGameToolStripMenuItem.Text = "Start A New Game";
            startANewGameToolStripMenuItem.Click += StartANewGameToolStripMenuItem_Click;

            startANewTournirToolStripMenuItem.Name = "startANewTournirToolStripMenuItem";
            startANewTournirToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            startANewTournirToolStripMenuItem.Text = "Start A New Tournir";
            startANewTournirToolStripMenuItem.Click += StartANewTournirToolStripMenuItem_Click;

            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { howToPlayToolStripMenuItem, toolStripSeparator2, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            helpToolStripMenuItem.Text = "Help";

            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            propertiesToolStripMenuItem.Text = "Properties...";
            propertiesToolStripMenuItem.Click += PropertiesToolStripMenuItem_Click;

            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(182, 6);

            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;

            howToPlayToolStripMenuItem.Name = "howToPlayToolStripMenuItem";
            howToPlayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            howToPlayToolStripMenuItem.Text = "How To Play?";
            howToPlayToolStripMenuItem.Click += HowToPlayToolStripMenuItem_Click;

            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(149, 6);

            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;

            labelAbout = new Label();
            labelAbout.Size = new System.Drawing.Size(300, 300);
            labelAbout.BackColor = System.Drawing.Color.White;
            labelAbout.Text = "4 In A Row!! :)";
            labelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelAbout.Font = new System.Drawing.Font("Arial", 25);
            labelAbout.Click += LabelAbout_Click;
            labelAbout.Visible = false;
            Controls.Add(labelAbout);

            Controls.Add(this.menuStrip1);
            MainMenuStrip = this.menuStrip1;

            Text = "Four In A Row";
            Name = "GameForm";
            MaximizeBox = false;
            BackColor = Color.LightSkyBlue;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            SizeGripStyle = SizeGripStyle.Hide;
            ClientSize = new Size((i_NumOfCols * 67) + 40, 100 + ((i_NumOfRows * 67) + 50));
            timerFall = new Timer();
            timerFall.Interval = 1;
            timerFall.Tick += timerFall_Tick;
            timerBlink = new Timer();
            timerBlink.Interval = 200;
            timerBlink.Tick += timerBlink_Tick;

            GraphicsPath circlePathForCoinPictureBox = new GraphicsPath();
            circlePathForCoinPictureBox.AddEllipse(new Rectangle(0, 0, 60, 60));

            pictureBoxFalling = new PictureBox();
            pictureBoxFalling.Visible = false;
            pictureBoxFalling.Height = 67;
            pictureBoxFalling.Width = 67;
            pictureBoxFalling.Image = FourInRow.Properties.Resources.CoinRed;
            pictureBoxFalling.Region = new Region(circlePathForCoinPictureBox);

            pictureBoxHover = new PictureBox();
            pictureBoxHover.Image = FourInRow.Properties.Resources.CoinRed;
            pictureBoxHover.BackColor = System.Drawing.Color.Thistle;
            pictureBoxHover.Height = 67;
            pictureBoxHover.Width = 67;
            pictureBoxHover.Region = new Region(circlePathForCoinPictureBox);

            matrixPanel = new MatrixPanel(i_NumOfRows, i_NumOfCols, circlePathForCoinPictureBox);
            matrixPanel.SendToBack();
            matrixPanel.MouseMove += MatrixPanel_MouseMove;
            pictureBoxFalling.SendToBack();
            matrixPanel.Controls.Add(pictureBoxHover);
            matrixPanel.Controls.Add(pictureBoxFalling);

            foreach (Control ctrl in matrixPanel.Controls)
            {
                ctrl.MouseMove += MatrixPanel_MouseMove;
            }

            matrixPanel.Click += MatrixPanel_Click;

            Controls.Add(matrixPanel);
            pictureBoxHover.BringToFront();
        }

        private void initAfterBoardSizeChanged()
        {
            m_Turn = 1;

            m_RefToBoard.NumOfCols = (byte)m_NumOfColsChangedByProperties;
            m_RefToBoard.NumOfRows = (byte)m_NumOfRowsChangedByProperties;
            matrixPanel.InitAfterBoardSizeChanged(m_NumOfRowsChangedByProperties, m_NumOfColsChangedByProperties);

            foreach (Control ctrl in matrixPanel.Controls)
            {
                ctrl.MouseMove += MatrixPanel_MouseMove;
            }

            pictureBoxFalling.SendToBack();
            pictureBoxHover.Visible = true;
            ClientSize = new Size((m_NumOfColsChangedByProperties * 67) + 40, 105 + ((m_NumOfRowsChangedByProperties * 67) + 40));
            m_RefToBoard.InitAfterBoardSizeChanged();
            updateStatusBarScores();
            changePictureBoxHover();
            changePictureBoxFalling();
            CenterToScreen();
        }

        private void initForNextRound()
        {
            m_Turn = 1;
            matrixPanel.InitForNextRound(m_RefToBoard.NumOfRows, m_RefToBoard.NumOfCols);
            m_RefToBoard.InitBoardForNewGame();
            updateStatusBarScores();
            updateStatusBarCurrentPlayer();
            changePictureBoxHover();
            changePictureBoxFalling();
            pictureBoxHover.Visible = true;
        }

        private void createStatusBar()
        {
            string currentPlayer, scores;

            currentPlayer = string.Format("Current Player: {0}", m_RefToPlayer1.Name);
            scores = string.Format("{0}: {1}, {2}: {3}", m_RefToPlayer1.Name, m_RefToPlayer1.Score, m_RefToPlayer2.Name, m_RefToPlayer2.Score);

            statusBarPlayersInfo.Panels.Add(currentPlayer.ToString());
            statusBarPlayersInfo.Panels.Add(scores);
            statusBarPlayersInfo.Panels[0].AutoSize = StatusBarPanelAutoSize.Spring;
            statusBarPlayersInfo.Panels[1].AutoSize = StatusBarPanelAutoSize.Spring;
            statusBarPlayersInfo.Panels[0].BorderStyle = StatusBarPanelBorderStyle.None;
            statusBarPlayersInfo.Panels[1].BorderStyle = StatusBarPanelBorderStyle.None;
            statusBarPlayersInfo.ShowPanels = true;
        }

        private void runGameSettingsForm()
        {
            gameSettingsForm.ShowDialog();

            if (gameSettingsForm.DialogResult != DialogResult.Cancel)
            {
                GameManager.StartGame((byte)gameSettingsForm.NumOfRows, (byte)gameSettingsForm.NumOfCols, gameSettingsForm.Player1Name, gameSettingsForm.Player2Name, out m_RefToPlayer1, out m_RefToPlayer2, out m_RefToBoard);
                initForStart((byte)gameSettingsForm.NumOfRows, (byte)gameSettingsForm.NumOfCols);
                CenterToScreen();
            }
            else
            {
                Close();
            }
        }

        private void startFallingCoin(int i_ChosenCol, int i_PictureBoxFallingLeftIndex)
        {
            if (m_RefToBoard.GameBoard[0, i_ChosenCol].Sign == (char)Player.eSignOfPlayer.SignOfBlank)
            {
                pictureBoxFalling.Left = i_PictureBoxFallingLeftIndex + 3;
                pictureBoxFalling.Top = 0;
                pictureBoxFalling.Visible = true;
                pictureBoxHover.Visible = false;
                timerFall.Start();
            }
        }

        private void playTurn(byte i_ChosenCol)
        {
            byte rowToInsert;
            char discSign, signOfWinner;

            GameManager.PlaySpecificTurn(ref m_RefToPlayer1, ref m_RefToPlayer2, ref m_RefToBoard, m_Turn, (byte)i_ChosenCol, out rowToInsert, out discSign);
            matrixPanel.ChangePictureBoxBoard(rowToInsert, i_ChosenCol, discSign);
            m_Turn++;
            handleEndOfTurn(i_ChosenCol, out signOfWinner);
            changePictureBoxFalling();
            changePictureBoxHover();
            updateStatusBarCurrentPlayer();
        }

        private void updateStatusBarCurrentPlayer()
        {
            statusBarPlayersInfo.Panels[0].Text = string.Format("Current Player: {0}", m_Turn % 2 == 1 ? m_RefToPlayer1.Name : m_RefToPlayer2.Name);
        }

        private void updateStatusBarScores()
        {
            statusBarPlayersInfo.Panels[1].Text = string.Format("{0}: {1}, {2}: {3}", m_RefToPlayer1.Name, m_RefToPlayer1.Score, m_RefToPlayer2.Name, m_RefToPlayer2.Score);
        }

        private void changePictureBoxFalling()
        {
            if (m_Turn % 2 == 0)
            {
                pictureBoxFalling.Image = FourInRow.Properties.Resources.CoinYellow;
            }
            else
            {
                pictureBoxFalling.Image = FourInRow.Properties.Resources.CoinRed;
            }
        }

        private void changePictureBoxHover()
        {
            if (m_Turn % 2 == 1)
            {
                pictureBoxHover.Image = FourInRow.Properties.Resources.CoinRed;
            }
            else
            {
                pictureBoxHover.Image = FourInRow.Properties.Resources.CoinYellow;
            }
        }

        private void handleEndOfTurn(byte i_ChosenCol, out char o_SignOfWinner)
        {
            if (m_RefToBoard.CheckIfThereIsWinner(i_ChosenCol, out o_SignOfWinner))
            {
                handleWinSituation(o_SignOfWinner);
            }
            else if (m_RefToBoard.CheckIfFull())
            {
                handleTieSituation();
            }
        }

        private void handleTieSituation()
        {
            if (MessageBox.Show("Tie!!" + Environment.NewLine + "Another Round?", "A Tie!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                Close();
            }
            else
            {
                initController();
            }
        }

        private void handleWinSituation(char i_SignOfWinner)
        {
            string winner;

            GameManager.HandleWinSituation(ref m_RefToPlayer1, ref m_RefToPlayer2, i_SignOfWinner);
            blinkWinnerPath(i_SignOfWinner);
            timerBlink.Start();

            if (i_SignOfWinner == (char)Player.eSignOfPlayer.SignOfPlayer1)
            {
                winner = m_RefToPlayer1.Name;
            }
            else
            {
                winner = m_RefToPlayer2.Name;
            }

            pictureBoxHover.Visible = false;
            if (MessageBox.Show(winner + " Won!!" + Environment.NewLine + "Another Round?", "A Win!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                Close();
            }
            else
            {
                timerBlink.Stop();
                initController();
            }
        }

        private void blinkWinnerPath(char i_SignOfWinner)
        {
            foreach (Board.Square sq in m_RefToBoard.WinnerPath)
            {
                if (i_SignOfWinner == (char)Player.eSignOfPlayer.SignOfPlayer1)
                {
                    matrixPanel.Board[sq.RowNumber, sq.ColNumber].Image = FourInRow.Properties.Resources.CoinRed;
                    matrixPanel.Board[sq.RowNumber, sq.ColNumber].BackColor = Color.DeepPink;
                }
                else
                {
                    matrixPanel.Board[sq.RowNumber, sq.ColNumber].Image = FourInRow.Properties.Resources.CoinYellow;
                    matrixPanel.Board[sq.RowNumber, sq.ColNumber].BackColor = Color.DeepPink;
                }
            }
        }

        private void MatrixPanel_Click(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Y < 65 && (e as MouseEventArgs).Y >= 0)
            {
                int pictureBoxFallingLeftIndex = (int)(e as MouseEventArgs).X / 67 * 67;
                int colIndex = ((e as MouseEventArgs).X - 20) / 67;
                startFallingCoin(colIndex, pictureBoxFallingLeftIndex);
            }
        }

        private void MatrixPanel_MouseMove(object sender, MouseEventArgs e)
        {
            m_DelayOfMouseMove++;

            if (m_DelayOfMouseMove % 3 == 0)
            {
                if (sender is PictureBox)
                {
                    pictureBoxHover.Left = (sender as Control).Left + (e as MouseEventArgs).X - 33;
                    pictureBoxHover.Top = (sender as Control).Top + (e as MouseEventArgs).Y + 5;
                }
                else
                {
                    pictureBoxHover.Left = (e as MouseEventArgs).X - 33;
                    pictureBoxHover.Top = (e as MouseEventArgs).Y + 5;
                }
            }
        }

        private void HowToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HowToPlayForm form1 = new HowToPlayForm();
            form1.ShowDialog();
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameSettingsForm.ShowDialog();

            if (gameSettingsForm.DialogResult == DialogResult.OK)
            {
                m_RefToPlayer1.Name = gameSettingsForm.Player1Name;
                m_RefToPlayer2.Name = gameSettingsForm.Player2Name;
                updateStatusBarCurrentPlayer();
                updateStatusBarScores();
                if (gameSettingsForm.NumOfRows != m_RefToBoard.NumOfRows || gameSettingsForm.NumOfCols != m_RefToBoard.NumOfCols)
                {
                    m_NumOfColsChangedByProperties = (int)gameSettingsForm.NumOfCols;
                    m_NumOfRowsChangedByProperties = (int)gameSettingsForm.NumOfRows;
                    matrixPanel.BoardSizeChanged = true;
                    if (MessageBox.Show("Sart a New Game?", "4 In A Row", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        MessageBox.Show("New board size will take effect on the next game.", "4 In A Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        initAfterBoardSizeChanged();
                    }
                }
            }
        }

        private void LabelAbout_Click(object sender, EventArgs e)
        {
            labelAbout.Visible = false;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelAbout.Left = (ClientSize.Width - labelAbout.Width) / 2;
            labelAbout.Top = (ClientSize.Height - labelAbout.Height) / 2;
            labelAbout.Visible = true;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StartANewTournirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_RefToPlayer1.ResetScore();
            m_RefToPlayer2.ResetScore();
            initForNextRound();
        }

        private void StartANewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initForNextRound();
        }

        private void timerBlink_Tick(object sender, EventArgs e)
        {
            foreach (Board.Square sq in m_RefToBoard.WinnerPath)
            {
                if (matrixPanel.Board[sq.RowNumber, sq.ColNumber].Name != "blink1")
                {
                    matrixPanel.Board[sq.RowNumber, sq.ColNumber].BackColor = System.Drawing.Color.Purple;
                    matrixPanel.Board[sq.RowNumber, sq.ColNumber].Name = "blink1";
                }
                else
                {
                    matrixPanel.Board[sq.RowNumber, sq.ColNumber].BackColor = System.Drawing.Color.DeepPink;
                    matrixPanel.Board[sq.RowNumber, sq.ColNumber].Name = "blink2";
                }
            }
        }

        private void timerFall_Tick(object sender, EventArgs e)
        {
            int rowIndex = (pictureBoxFalling.Bottom - 70) / 67;
            int colIndex = pictureBoxFalling.Location.X / 67;

            if (pictureBoxFalling.Bottom + 80 >= this.ClientSize.Height || matrixPanel.Board[rowIndex, colIndex].Name != GameForm.ImagesNames.k_EmptyCell)
            {
                doWhenReachedBottom(colIndex);
            }
            else
            {
                pictureBoxFalling.Top += 10;
            }
        }

        private void doWhenReachedBottom(int i_ColIndex)
        {
            timerFall.Stop();
            pictureBoxHover.Visible = true;
            pictureBoxFalling.Visible = false;
            playTurn((byte)i_ColIndex);
        }

        internal static GraphicsPath QuickCalculateGraphicsPath(Bitmap i_Bitmap)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Color transparentColor = i_Bitmap.GetPixel(4, 40);
            int startRegionArea = -1;
            Color pixelColor = Color.Empty;
            BitmapData bitmapData = i_Bitmap.LockBits(new Rectangle(0, 0, i_Bitmap.Width, i_Bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr scanL = bitmapData.Scan0;
            int yOffset = bitmapData.Stride - (i_Bitmap.Width * 3);

            unsafe
            {
                int bitmapHeight = i_Bitmap.Height;
                int bitmapWidth = i_Bitmap.Width;

                byte* p = (byte*)(void*)scanL;
                for (int y = 0; y <= bitmapHeight - 1; y++)
                {
                    for (int x = 0; x <= bitmapWidth - 1; x++)
                    {
                        int B = (int)p[0];
                        int G = (int)p[1];
                        int R = (int)p[2];
                        pixelColor = Color.FromArgb(R, G, B);
                        if (pixelColor == transparentColor && startRegionArea != -1)
                        {
                            graphicsPath.AddRectangle(new Rectangle(startRegionArea, y, (x - 1) - startRegionArea, 1));
                            startRegionArea = -1;
                        }

                        if (pixelColor != transparentColor && startRegionArea == -1)
                        {
                            startRegionArea = x;
                        }

                        p += 3;
                    }

                    if (startRegionArea != -1)
                    {
                        graphicsPath.AddRectangle(new Rectangle(startRegionArea, y, i_Bitmap.Width - startRegionArea, 1));
                        startRegionArea = -1;
                    }

                    p += yOffset;
                }
            }

            i_Bitmap.UnlockBits(bitmapData);
            return graphicsPath;
        }
    }
}
