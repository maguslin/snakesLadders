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

        Game game;
        Bitmap BoardBackground;
        Bitmap BoardBackgroundSized;
        Dictionary<string, Bitmap> Counters;
        bool GameStarted;



        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            GameStarted = false;

            PictureBox boardDisplay = new PictureBox();
            game = new Game(this);

            BoardBackground = SetUpBoard();

            SetUpPictureBox(boardDisplay);
            boardDisplay.Name = "boardDisplay";

            Counters = new Dictionary<string, Bitmap>();
            Counters.Add("Red", Properties.Resources.redCounter);
            Counters.Add("Blue", Properties.Resources.blueCounter);
            Counters.Add("Yellow", Properties.Resources.yellowCounter);
            Counters.Add("Green", Properties.Resources.greenCounter);

            this.BackgroundImage = BoardBackground;

            PictureBox ShowDie = new PictureBox();
            ShowDie.Height = (int)(this.ClientRectangle.Height * (double)10/(double)12);
            ShowDie.Width = this.ClientRectangle.Width - boardDisplay.Width;
            ShowDie.Location = new Point(boardDisplay.Width, 0);
            ShowDie.Name = "ShowDie";
            ShowDie.BackColor = Color.Red;

            Button DoStuff = new Button();
            DoStuff.Height = this.ClientRectangle.Height - ShowDie.Height;
            DoStuff.Width = ShowDie.Width;
            DoStuff.Location = new Point(ShowDie.Location.X, ShowDie.Height);
            DoStuff.Click += Button_Click;
            DoStuff.Text = "Click For Magic";
            DoStuff.Name = "DoStuff";

            this.Controls.Add(ShowDie);
            this.Controls.Add(DoStuff);
            this.Controls.Add(boardDisplay);

            this.Click += Form1_Click;
        }


        private Bitmap SetUpBoard()
        {
            Bitmap board = new Bitmap(Properties.Resources.snlboard001, Properties.Resources.snlboard001.Height, Properties.Resources.snlboard001.Width);
            using (Graphics g = Graphics.FromImage(board))
            {
                g.DrawImage(Properties.Resources.s_l, new Point[] { new Point(0, 0), new Point(board.Width, 0), new Point(0, board.Height) });
            }

            return board;
        }

        private Bitmap DrawBoard(int Width, int Height)
        {
            Bitmap temporay = new Bitmap(Width, Height);

            using (Graphics g = Graphics.FromImage(temporay))
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
            if (null != BoardDisplay.Image)
            {
                BoardDisplay.Image.Dispose();
            }
            BoardDisplay.Image = (Bitmap)BoardBackgroundSized.Clone();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            PictureBox boardDisplay = (PictureBox)this.Controls.Find("boardDisplay", false)[0];
            PictureBox ShowDie = (PictureBox)this.Controls.Find("ShowDie", false)[0];
            Button DoStuff = (Button)this.Controls.Find("DoStuff", false)[0];
            SetUpPictureBox(boardDisplay);
            ShowDie.Height = (int)(this.ClientRectangle.Height * (double)10 / (double)12);
            ShowDie.Width = this.ClientRectangle.Width - boardDisplay.Width;
            ShowDie.Location = new Point(boardDisplay.Width, 0);
            DoStuff.Height = this.ClientRectangle.Height - ShowDie.Height;
            DoStuff.Width = ShowDie.Width;
            DoStuff.Location = new Point(ShowDie.Location.X, ShowDie.Height);

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            game.PlayGame();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            game.PlayGame();
        }

        public void DrawCounters(List<Square> Squares)
        {
            PictureBox boardDisplay = (PictureBox)this.Controls.Find("boardDisplay", false)[0];
            Bitmap temp = (Bitmap)BoardBackgroundSized.Clone();
            int xCoord;
            int yCoord;

            if (null != boardDisplay.Image)
            {
                boardDisplay.Image.Dispose();
            }

            foreach (Square i in Squares)
            {

                if (0 < i.Occupier.Count)
                {

                    yCoord = (10 - ((i.Number - 1) / 10) - 1) * (temp.Height / 10);
                    if (((i.Number - 1) / 10) % 2 == 1)
                    {
                        xCoord = (9 - ((i.Number - 1) % 10)) * (boardDisplay.Width / 10);
                    }
                    else
                    {
                        xCoord = ((i.Number - 1) % 10) * (boardDisplay.Width / 10);
                    }


                    using (Graphics g = Graphics.FromImage(temp))
                    {
                        if (1 == i.Occupier.Count)
                        {

                            g.DrawImage(Counters[i.Occupier[0].Colour], new Point[] { new Point(xCoord, yCoord), new Point(xCoord + boardDisplay.Width / 10, yCoord), new Point(xCoord, yCoord + boardDisplay.Height / 10) });
                        }
                    }
                }

            }


            boardDisplay.Image = temp;
        }

        private void UpdateDie()
        {

        }
    }
}
