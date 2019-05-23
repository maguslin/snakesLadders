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
        Bitmap BoardBackground;
        Bitmap BoardBackgroundSized;



        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBox boardDisplay = new PictureBox();

            BoardBackground = SetUpBoard();

            SetUpPictureBox(boardDisplay);
            boardDisplay.Name = "boardDisplay";

            this.BackgroundImage = BoardBackground;

            this.Controls.Add(boardDisplay);

            this.Click += Form1_Click;

            RefreshBoard(40);

            //game.PlayGame();
        }


        private Bitmap SetUpBoard()
        {
            Bitmap board = new Bitmap(Properties.Resources.snlboard001, Properties.Resources.snlboard001.Height, Properties.Resources.snlboard001.Width);
            using(Graphics g = Graphics.FromImage(board))
            {
                g.DrawImage(Properties.Resources.s_l, new Point[] { new Point(0, 0), new Point(board.Width, 0), new Point(0, board.Height) });
            }

            return board;
        }

        private Bitmap DrawBoard(int Width, int Height)
        {
            Bitmap temporay = new Bitmap(Width, Height);

            using(Graphics g = Graphics.FromImage(temporay))
            {
                g.DrawImage(BoardBackground, new Point[] { new Point(0, 0), new Point(Width, 0), new Point(0, Height) });
            }

            return temporay;
        }

        private void SetUpPictureBox(PictureBox BoardDisplay)
        {
            BoardDisplay.Height = this.ClientRectangle.Height;
            BoardDisplay.Width = (int)((this.ClientRectangle.Width) * (double)9 / (double)12);
            BoardBackgroundSized = DrawBoard(BoardDisplay.Width, BoardDisplay.Height);
            BoardDisplay.Image = (Bitmap)BoardBackgroundSized.Clone();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            PictureBox boardDisplay = (PictureBox)this.Controls.Find("boardDisplay",false)[0];
            boardDisplay.Image.Dispose();
            SetUpPictureBox(boardDisplay);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            for(int i = 1; i <= 100; i++)
            {
                RefreshBoard(i);
                
            }
        }

        private void RefreshBoard(int square)
        {
            PictureBox boardDisplay = (PictureBox)this.Controls.Find("boardDisplay", false)[0];
            Bitmap temp = (Bitmap)BoardBackgroundSized.Clone();
            int xCoord;
            int yCoord;

            if (null != boardDisplay.Image)
            {
              boardDisplay.Image.Dispose();
            }

            using (Graphics g = Graphics.FromImage(temp))
            {
                yCoord = (10 -((square - 1) / 10) - 1) * (temp.Height/10);
                if(((square -1)/10) % 2 == 1)
                {
                    xCoord = (9 -((square -1)%10)) * (boardDisplay.Width/10);
                }
                else
                {
                    xCoord = ((square -1)%10) * (boardDisplay.Width/10);
                }

                g.DrawImage(Properties.Resources.blueCounter, new Point[] {new Point(xCoord, yCoord), new Point(xCoord +boardDisplay.Width/10, yCoord), new Point(xCoord, yCoord + boardDisplay.Height / 10)});
            }

            boardDisplay.Image = temp;
        }
    }
}
