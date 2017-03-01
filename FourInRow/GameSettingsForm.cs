using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FourInRow
{
    internal class GameSettingsForm : Form
    {
        private Label labelPlayers;
        private Label labelPlayer1;
        private Label labelRows;
        private Label labelCols;
        private TextBox textBoxFirstName;
        private TextBox textBoxSecondName;
        private NumericUpDown numericRows;
        private NumericUpDown numericCols;
        private Label labelPlayer2;
        private Button buttonCancel;
        private Button buttonStart;

        public string Player1Name
        {
            get
            {
                return textBoxFirstName.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBoxSecondName.Text;
            }
        }

        public decimal NumOfRows
        {
            get
            {
                return numericRows.Value;
            }
        }

        public decimal NumOfCols
        {
            get
            {
                return numericCols.Value;
            }
        }

        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void InitializeComponent()
        {
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxSecondName = new System.Windows.Forms.TextBox();
            this.labelRows = new System.Windows.Forms.Label();
            this.numericRows = new System.Windows.Forms.NumericUpDown();
            this.labelCols = new System.Windows.Forms.Label();
            this.numericCols = new System.Windows.Forms.NumericUpDown();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCols)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPlayers
            // 
            this.labelPlayers.Location = new System.Drawing.Point(12, 9);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(50, 20);
            this.labelPlayers.TabIndex = 0;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1.Location = new System.Drawing.Point(35, 42);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(50, 20);
            this.labelPlayer1.TabIndex = 1;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(105, 39);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(115, 20);
            this.textBoxFirstName.TabIndex = 2;
            this.textBoxFirstName.Text = "Player1";
            // 
            // textBoxSecondName
            // 
            this.textBoxSecondName.Location = new System.Drawing.Point(105, 80);
            this.textBoxSecondName.Name = "textBoxSecondName";
            this.textBoxSecondName.Size = new System.Drawing.Size(115, 20);
            this.textBoxSecondName.TabIndex = 4;
            this.textBoxSecondName.Text = "Player2";
            // 
            // labelRows
            // 
            this.labelRows.Location = new System.Drawing.Point(35, 174);
            this.labelRows.Name = "labelRows";
            this.labelRows.Size = new System.Drawing.Size(119, 20);
            this.labelRows.TabIndex = 7;
            this.labelRows.Text = "Board Width (in cells):";
            // 
            // numericRows
            // 
            this.numericRows.Location = new System.Drawing.Point(160, 134);
            this.numericRows.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericRows.Name = "numericRows";
            this.numericRows.Size = new System.Drawing.Size(40, 20);
            this.numericRows.TabIndex = 8;
            this.numericRows.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // labelCols
            // 
            this.labelCols.Location = new System.Drawing.Point(35, 136);
            this.labelCols.Name = "labelCols";
            this.labelCols.Size = new System.Drawing.Size(119, 20);
            this.labelCols.TabIndex = 9;
            this.labelCols.Text = "Board Height (in cells):";
            // 
            // numericCols
            // 
            this.numericCols.Location = new System.Drawing.Point(160, 172);
            this.numericCols.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericCols.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericCols.Name = "numericCols";
            this.numericCols.Size = new System.Drawing.Size(40, 20);
            this.numericCols.TabIndex = 10;
            this.numericCols.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(12, 222);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(86, 30);
            this.buttonStart.TabIndex = 11;
            this.buttonStart.Text = "OK";
            this.buttonStart.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.Location = new System.Drawing.Point(35, 80);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(50, 20);
            this.labelPlayer2.TabIndex = 12;
            this.labelPlayer2.Text = "Player 2:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(134, 222);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 30);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // GameSettingsForm
            // 
            this.AcceptButton = this.buttonStart;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(232, 277);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.textBoxFirstName);
            this.Controls.Add(this.textBoxSecondName);
            this.Controls.Add(this.labelRows);
            this.Controls.Add(this.numericRows);
            this.Controls.Add(this.labelCols);
            this.Controls.Add(this.numericCols);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Properties";
            ((System.ComponentModel.ISupportInitialize)(this.numericRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
