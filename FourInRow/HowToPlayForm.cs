using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace FourInRow
{
    internal class HowToPlayForm : Form
    {
        private TextBox textBoxHowToPlay;
        private Button buttonOK;

        public HowToPlayForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            initHowToPlayTextBox();
        }

        private void initHowToPlayTextBox()
        {
            textBoxHowToPlay.KeyPress += textBoxHowToPlay_KeyPress;
            textBoxHowToPlay.ScrollBars = ScrollBars.Vertical;

            try
            {
                textBoxHowToPlay.Lines = File.ReadAllLines("C:\\FourInArowHelp.txt");
            }
            catch (FileNotFoundException e)
            {
                textBoxHowToPlay.Text = string.Format("Sorry, The File Not Found.{0}Please continue playing and enjoy :)", Environment.NewLine);
                MessageBox.Show("Error" + Environment.NewLine + "The File Does Not Exist", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxHowToPlay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxHowToPlay = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(223, 273);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxHowToPlay
            // 
            this.textBoxHowToPlay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxHowToPlay.Location = new System.Drawing.Point(12, 12);
            this.textBoxHowToPlay.Multiline = true;
            this.textBoxHowToPlay.Name = "textBoxHowToPlay";
            this.textBoxHowToPlay.Size = new System.Drawing.Size(299, 255);
            this.textBoxHowToPlay.TabIndex = 1;
            // 
            // HowToPlayForm
            // 
            this.AcceptButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(323, 308);
            this.Controls.Add(this.textBoxHowToPlay);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HowToPlayForm";
            this.Text = "How to play?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
