using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPSnamkes
{
    public partial class Form1 : Form
    {

        Game game = new Game();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap background = DrawBoard();

            PictureBox gameBoardDisplay = new PictureBox();
            gameBoardDisplay.Height = this.Height;
            gameBoardDisplay.Width = this.Width * (2 / 3);
            gameBoardDisplay.Location = new Point(0, 0);
            gameBoardDisplay.Image = background;

            TextBox txtbx = new TextBox();
            txtbx.Text = "";
            txtbx.Name = "txtbx1";
            txtbx.Location = new Point(10, 10);
            txtbx.Height = 20;
            txtbx.Width = 50;
            Controls.Add(txtbx);

            this.Text = "help";

            this.Controls.Add(gameBoardDisplay);
            





            game.PlayGame();
        }


        private Bitmap DrawBoard()
        {
            Bitmap board = new Bitmap(Properties.Resources.snlboard001, Properties.Resources.snlboard001.Height, Properties.Resources.snlboard001.Width);
            using(Graphics g = Graphics.FromImage(board))
            {
                g.DrawImage(Properties.Resources.s_l, new Point[] { new Point(0, 0), new Point(board.Width, 0), new Point(0, board.Height) });
            }

            return board;
        }
    }
}
