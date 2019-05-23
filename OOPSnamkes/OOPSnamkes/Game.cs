using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPSnamkes  //MLO
{
    public class Game
    {

        public bool Continue;
        private Queue<Player> players = new Queue<Player>();
        private Board gameboard;
        public int NumberOfPlayers { get { return players.Count; } }
        private Form1 Parent;

        public Game(Form1 Parent)
        {
            this.Parent = Parent;
            CreateBoard();
            GetPlayers();
        }

        public void CreatePlayerQueue(List<Player> newplayers)
        {
            foreach (var person in newplayers)
            {
                this.players.Enqueue(person);
                this.players.Last().SetSquare(gameboard.Squares[0]);
            }
        }

        private void GetPlayers()
        {
            AddPlayerForm addPlayerForm = new AddPlayerForm();
            addPlayerForm.ShowDialog();
            if (addPlayerForm.DialogResult == DialogResult.OK)
            {
                CreatePlayerQueue(addPlayerForm.Players);
            }
            else
            {
                Console.Beep();
            }
            addPlayerForm.Dispose();
        }

        private void CreateBoard()
        {
            this.gameboard = new Board();
        }

        public void PlayGame()
        {

            Player currentPlayer = new Player();

                currentPlayer = players.Dequeue();
                currentPlayer.TakeTurn(gameboard);
                players.Enqueue(currentPlayer);
                Parent.DrawCounters(gameboard.Squares);
            Parent.DrawCounters(gameboard.Squares);

            if(currentPlayer.winner){
                //do something spectacular
}
        }

    }
}
