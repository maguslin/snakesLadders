﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    class Player
    {
        protected string name;
        protected ConsoleColor colour;
        protected int position;

        public Player(string _name, ConsoleColor _colour)
        {
            this.name = _name;
            this.colour = _colour;
            this.position = 0;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public ConsoleColor Colour
        {
            get { return this.colour; }
            set { this.colour = value; }
        }

        public int Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
    }

    public class Square
    {
        protected string type;
        protected int action;
        protected ConsoleColor? playerColour;

        public string Type
        {
            get { return this.type; }
            set
            {
                if ((value == "S") || (value == "L") || (value == "N"))
                {
                    this.type = value;
                }
                else this.type = "N";
            }
        }


        public int Action
        {
            get { return this.action; }
            set { this.action = value; }
        }

        public ConsoleColor? PlayerColour
        {
            get { return this.playerColour; }
            set { this.playerColour = value; }
        }

        public Square()
        {
            this.Type = "N";
            this.Action = 0;
            this.PlayerColour = null;
        }
    }
    class Program
    { 
       
        
        static void Main(string[] args)
        {
            Player[] players = CollectData();
            Square[] squares = SetUpBoard();
            PlayGame(players, squares);
            Console.ReadKey();
        }
        
        public static void PlayGame(Player[] players, Square[] squares)
        {
            //Needs to be put to initiate the TakePlayerTurn -- win = ControlLoop(numOfPlayers, players, squares); //Returns win = true because someone has won the ga

            int PlayerNum = 0;
            do
            {
                Console.WriteLine("{0}, Press to roll the dice.", players[PlayerNum].Name);
                Console.ReadKey();
                TakePlayerTurn(players[PlayerNum], squares);
                DisplayBoard(squares);
                if (players[PlayerNum].Position < 99)
                {
                    PlayerNum = PlayerNum + 1;
                    if (PlayerNum == players.Length)
                    {
                        PlayerNum = 0;
                    }
                }
            } while (players[PlayerNum].Position < 99);

            Console.WriteLine($"{players[PlayerNum].Name} has won the game!");

        }

        public static void TakePlayerTurn(Player currentplayer, Square[] squares)
        {
            int tempPlayerRollVal = 0;  // ∴

            
                tempPlayerRollVal = GetDieValue();
                Console.WriteLine($"{currentplayer.Name}, you have rolled a {tempPlayerRollVal}");
            //Determines new player position
            Move(currentplayer, tempPlayerRollVal, squares);
            ApplyRules(currentplayer, squares);

            //Re-colours square of the player
            squares[currentplayer.Position].PlayerColour = currentplayer.Colour;
            Console.WriteLine($"Your current position is {currentplayer.Position}");
        }

        public static Player[] CollectData()
        {
            int numOfPlayers = 0;
            List<ConsoleColor?> colourOptions = new List<ConsoleColor?>(new ConsoleColor?[] { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Magenta });

            bool intCheck = false;
            while (intCheck == false)
            {
                Console.WriteLine("Please enter the number of players(between 2 - 5): ");
                intCheck = int.TryParse(Console.ReadLine(), out numOfPlayers);

                if (numOfPlayers < 2 || numOfPlayers > 5)
                    intCheck = false;

                if (intCheck == false)
                {
                    Console.WriteLine("Invalid entry! Please try again!");
                }
            }

            Player[] players = new Player[numOfPlayers];

            //Collects names of players and colour chosen.
            for (int i = 0; i < players.Length; i++)
            {
                Console.WriteLine("");
                string currentName = "";
                ConsoleColor? currentColour = null;

                Console.WriteLine("Player {0}, please enter your name: ", i + 1);
                currentName = Console.ReadLine();

                bool colourCheck = false;
                while (colourCheck == false)
                {
                    int colourChoice = 0;
                    Console.WriteLine("Which colour would you like to use?");
                    for (int x = 0; x < colourOptions.Count; x++)
                        Console.WriteLine("{0}. {1}", x + 1, colourOptions[x]);

                    colourCheck = int.TryParse(Console.ReadLine(), out colourChoice);
                    if (colourChoice < 1 || colourChoice > 5)
                        colourCheck = false;
                    if (colourCheck == false)
                        Console.WriteLine("Invalid entry! Please try again!");
                    else
                    {
                        currentColour = colourOptions[colourChoice - 1];
                        colourOptions.RemoveAt(colourChoice - 1);
                    }
                    players[i] = new Player(currentName, (ConsoleColor)currentColour);
                }                
            }

            return players;

        }

        //changes player location
        static void Move(Player player, int roll, Square[] squares)
        {
            int pos1 = player.Position;
            player.Position += roll;
            if (player.Position > 99)
            {
                player.Position = 99;
            }
            int pos2 = player.Position;
            squares[pos1].PlayerColour = null;
            squares[pos2].PlayerColour = player.Colour;
        }

        static Square[] LoadBoard()
        {
            Square[] Squares = new Square[100];

            for (int i = 0; i < Squares.Length; i++)
            {
                Squares[i] = new Square();
            }

            Squares[3].Action = 10;
            Squares[3].Type = "L";

            Squares[8].Action = 22;
            Squares[8].Type = "L";

            Squares[16].Action = -10;
            Squares[16].Type = "S";

            Squares[19].Action = 18;
            Squares[19].Type = "L";

            Squares[27].Action = 56;
            Squares[27].Type = "L";

            Squares[39].Action = 19;
            Squares[39].Type = "L";

            Squares[53].Action = -20;
            Squares[53].Type = "S";

            Squares[61].Action = -44;
            Squares[61].Type = "S";

            Squares[63].Action = -4;
            Squares[63].Type = "S";

            Squares[70].Action = 20;
            Squares[70].Type = "L";

            Squares[86].Action = -70;
            Squares[86].Type = "S";

            Squares[92].Action = -20;
            Squares[92].Type = "S";

            Squares[94].Action = -20;
            Squares[94].Type = "S";

            Squares[98].Action = -21;
            Squares[98].Type = "S";

            Squares[99].Type = "W";
            return Squares;
        }

        static void ApplyRules(Player CurrentPlayer, Square[] squares)
        {
            Square CurrentSquare = squares[CurrentPlayer.Position];
            Move(CurrentPlayer, CurrentSquare.Action, squares);
        }

        static int GetDieValue()
        {
            int roll1;
            int roll2;
            bool doubleTurn = false;
            int total = 0;

            //Rolls dice twice
            roll1 = RollDice();
            Thread.Sleep(15);
            roll2 = RollDice();

            Console.WriteLine(roll1);
            Console.WriteLine(roll2);

            //Checks if values the same. If so, returns bool true
            doubleTurn = Double(roll1, roll2);
            

            //Adds total of two rolls
            total = CalculateTotal(roll1, roll2) + total;

            //If first two rolls double, rolls third dice
            if (doubleTurn == true)
            {
                total = total + RollDice();
            }

            return total;
        }

        static int RollDice()
        {
            int random = 0;

            //Creates dice values
            Random randomNumber = new Random();
            return random = randomNumber.Next(1, 7);
        }

        static bool Double(int roll1, int roll2)
        {
            bool doubleTurn = false;

            //Checks to see if values the same
            if (roll1 == roll2)
            {
                Console.WriteLine("You got a double!!");
                doubleTurn = true;
            }

            return doubleTurn;
        }

        static int CalculateTotal(int roll1, int roll2)
        {
            //Creates total of rolls
            int total = roll1 + roll2;
            return total;
        }
                
        public static void DisplayBoard(Square[] Squares)
        {
            Console.Clear();
            ConsoleColor BackgroundColour = Console.BackgroundColor;

            //Loops through the rows
            for (int y = 0; y < 10; y++)
            {
                //goes through the top of a row
                for (int i = 0; i <= 100; i++)
                {
                    //checks to see if there is new column
                    if (i % 10 == 0)
                    {
                        //works out what square it is in
                        int temp = CalculateLocation(i, y);

                        //checks to see if the current square is occupied
                        if (Squares[temp - 1].PlayerColour != null)
                        {
                            //changes the colour
                            Console.BackgroundColor = (ConsoleColor)Squares[temp - 1].PlayerColour;
                        }
                        else
                        {
                            //puts the colour back
                            Console.BackgroundColor = BackgroundColour;
                        }
                        //prints the column divider
                        Console.Write("█");
                    }
                    else
                    {
                        //prints the row divider
                        Console.Write("▀");
                    }
                }
                //moves to the next line
                Console.Write("\n");


                //does the middle of the row
                for (int i = 0; i <= 100; i++)
                {
                    //checks to see if its at the number location
                    if (i % 10 == 2)
                    {
                        //finds the current square
                        int temp = CalculateLocation(i, y);

                        //prints the square number
                        Console.Write(temp);

                        //accounts for any extra characters
                        i += temp.ToString().Length - 1;
                    }
                    //checks to see if it is at a new column
                    else if (i % 10 == 0)
                    {
                        //finds the current square
                        int temp = CalculateLocation(i, y);

                        //checks to see if that square is occupied
                        if (Squares[temp - 1].PlayerColour != null)
                        {
                            Console.BackgroundColor = (ConsoleColor)Squares[temp - 1].PlayerColour;
                        }
                        else
                        {
                            Console.BackgroundColor = BackgroundColour;
                        }
                        //prints the new column
                        Console.Write("█");
                    }
                    //checks to see if its at a point for printing square details
                    else if (i % 10 == 6)
                    {
                        //finds the current square
                        int temp = CalculateLocation(i, y);

                        temp--;

                        //checks to see if that square has any actions
                        if (Squares[temp].Type != "N")
                        {
                            //Collects the details to print
                            string toPrint = Squares[temp].Type + Math.Sqrt(Math.Pow(Squares[temp].Action, 2)).ToString();

                            //writes the to the screen
                            Console.Write(toPrint);
                            i += toPrint.Length - 1;
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                //goes to a new line
                Console.Write("\n");

                //loops through the bottom of each row
                for (int i = 0; i <= 100; i++)
                {
                    //checks to see if it has hit a new column
                    if (i % 10 == 0)
                    {
                        //finds the current square
                        int temp = CalculateLocation(i, y);

                        //checks to see if this square is occupied
                        if (Squares[temp - 1].PlayerColour != null)
                        {
                            //changes the colour to the player colour
                            Console.BackgroundColor = (ConsoleColor)Squares[temp - 1].PlayerColour;
                        }
                        else
                        {
                            //changes the colour back to its original equation
                            Console.BackgroundColor = BackgroundColour;
                        }
                        //prints the column border
                        Console.Write("█");
                    }
                    else
                    {
                        //prints the row border
                        Console.Write("▄");
                    }
                }
                //prints the new line
                Console.Write("\n");
            }


            //goes back to the top of the screen.
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = BackgroundColour;




            //PrintSnakes(Squares);
            //PrintLadders(Squares);
            //JustText(Squares);
        }

        public static int CalculateLocation(int i, int y)
        {

            //complicated maths
            int temp = (((i - (i % 10)) / 10) + 1);
            int temp2 = temp;
            temp += y * 10;
            temp = temp + (y % 2) * (9 - (temp2 - 1) * 2);
            temp = 101 - temp;
            return temp;
        }

        public static void PrintLadders(Square[] Squares)
        {
            //goes through all of the squares
            for (int i = 0; i < Squares.Length; i++)
            {
                //checks to see if it found a ladder
                if (Squares[i].Type == "L")
                {
                    int startingPointX;
                    int startingPointY;
                    int endingPointX;
                    int endingPointY;
                    int newLocation = i + Squares[i].Action;
                    double gradient;

                    //checks to see wether or not it is on an odd line
                    if ((i / 10) % 2 == 0)
                    {
                        //Finds the starting location
                        startingPointX = (((i % 10)) * 10) + 6;
                        startingPointY = ((10 - (i / 10)) * 3) - 2;

                        //finds the finishing location
                        endingPointX = (((newLocation % 10)) * 10) + 6;
                        endingPointY = ((10 - (newLocation / 10)) * 3) - 2;
                    }
                    else
                    {
                        //Finds the starting location
                        startingPointX = ((9 - (i % 10)) * 10) + 6;
                        startingPointY = ((10 - (i / 10)) * 3) - 2;

                        //finds the finishing location
                        endingPointX = ((9 - (newLocation % 10)) * 10) + 6;
                        endingPointY = ((10 - (newLocation / 10)) * 3) - 2;
                    }

                    //finds out what the gradient is
                    gradient = (double)(startingPointY - endingPointY) / (double)(endingPointX - startingPointX);

                    //checks to see if it is going straight up
                    if (gradient == double.PositiveInfinity)
                    {
                        //stores the starting point
                        double x = startingPointX;

                        //loops through all the vertical values
                        for (int y = startingPointY; y >= endingPointY; y--)
                        {
                            //prints to that location
                            PrintSomething((int)x, (int)y);
                            PrintSomething((int)x + 1, (int)y);
                        }
                    }//checks for a negative gradient
                    else if (gradient < 0)
                    {
                        //stores the y starting point
                        double y = startingPointY;

                        //loops through all the x values
                        for (int x = startingPointX; x >= endingPointX; x--)
                        {
                            //prints something to the screen
                            PrintSomething((int)x, (int)y);
                            PrintSomething((int)x + 1, (int)y);

                            //checks to see if the gradient is even steeper
                            if (gradient < -1)
                            {
                                PrintSomething((int)x + 1, (int)y + 1);
                                PrintSomething((int)x, (int)y + 1);
                            }
                            //adjusts the y value
                            y -= (-gradient);
                        }
                    }
                    else
                    {
                        //sets the starting point
                        double y = startingPointY;

                        //loops through the x value
                        for (int x = startingPointX; x <= endingPointX; x++)
                        {
                            //prints the thing to the screen
                            PrintSomething((int)x, (int)y);
                            PrintSomething((int)x + 1, (int)y);

                            //checks to see if it needs to print more to the screen
                            if (gradient > 1)
                            {
                                PrintSomething((int)x + 1, (int)y + 1);
                                PrintSomething((int)x, (int)y + 1);
                            }

                            //adjusts y by the gradient
                            y -= (gradient);
                        }
                    }


                }
            }
        }

        //prints a block character to the screen at the given position in the given colour
        public static void PrintSomething(int x, int y, ConsoleColor Colour = ConsoleColor.Magenta)
        {
            Console.SetCursorPosition(x, y);
            ConsoleColor temp;
            temp = Console.ForegroundColor;
            Console.ForegroundColor = Colour;
            Console.Write("█");
            Console.ForegroundColor = temp;
        }

        private static void JustText(Square[] Squares)
        {
            ConsoleColor temp2 = Console.BackgroundColor;
            //Loops through the rows
            for (int y = 0; y < 10; y++)
            {

                //does the middle of the row
                for (int i = 0; i <= 100; i++)
                {
                    //checks to see if its at the number location
                    if (i % 10 == 2)
                    {
                        //finds the current square
                        int temp = CalculateLocation(i, y);

                        //goes to the cursor position
                        Console.SetCursorPosition(i, (y * 3) + 1);

                        //prints the square number
                        Console.Write(temp);

                        //accounts for any extra characters
                        i += temp.ToString().Length - 1;
                    }
                }
            }
        }

        //prints the snakes to the screen
        public static void PrintSnakes(Square[] Squares)
        {
            //loops through all the squares
            for (int i = 0; i < Squares.Length; i++)
            {
                //checks to see if it found a snake
                if (Squares[i].Type == "S")
                {
                    //declares some variables
                    int startingPointX;
                    int startingPointY;
                    int endingPointX;
                    int endingPointY;
                    int newLocation = i + Squares[i].Action;
                    double gradient;

                    //checks to see if its on an odd or even square
                    if ((i / 10) % 2 == 0)
                    {
                        //sets the starting locations
                        startingPointX = (((i % 10)) * 10) + 6;
                        startingPointY = ((10 - (i / 10)) * 3) - 2;

                        //sets the ending locations
                        endingPointX = (((newLocation % 10)) * 10) + 6;
                        endingPointY = ((10 - (newLocation / 10)) * 3) - 2;
                    }
                    else
                    {
                        //sets the starting locations
                        startingPointX = ((9 - (i % 10)) * 10) + 6;
                        startingPointY = ((10 - (i / 10)) * 3) - 2;

                        //sets the ending locations
                        endingPointX = ((9 - (newLocation % 10)) * 10) + 6;
                        endingPointY = ((10 - (newLocation / 10)) * 3) - 2;
                    }

                    //finds out the gradient between the points
                    gradient = (double)(startingPointY - endingPointY) / (double)(endingPointX - startingPointX);

                    //checks to see if its going straight down
                    if (gradient == double.NegativeInfinity)
                    {
                        //sets the x position
                        double x = startingPointX;

                        //Loops through all the relevant y values
                        for (int y = startingPointY; y <= endingPointY; y++)
                        {
                            //creates a variable to store the colour
                            ConsoleColor Colour;

                            //alternates the colour
                            if (y % 2 == 0)
                            {
                                Colour = ConsoleColor.DarkRed;
                            }
                            else
                            {
                                Colour = ConsoleColor.DarkYellow;
                            }

                            //prints the blocks to the screen
                            PrintSomething((int)x, (int)y, Colour);
                            PrintSomething((int)x + 1, (int)y, Colour);
                        }
                    }//checks to see if it has a negative gradient
                    else if (gradient < 0)
                    {
                        //stores the starting location
                        double y = startingPointY;

                        //loops through all the locations
                        for (int x = startingPointX; x <= endingPointX; x++)
                        {
                            //stores the colour
                            ConsoleColor Colour;

                            //alternates the colour
                            if (x % 2 == 0)
                            {
                                Colour = ConsoleColor.DarkRed;
                            }
                            else
                            {
                                Colour = ConsoleColor.DarkYellow;
                            }

                            //prints that part to the screen
                            PrintSomething((int)x, (int)y, Colour);
                            PrintSomething((int)x + 1, (int)y, Colour);

                            //if the gradient is even really steep it prints extra characters
                            if (gradient < -1)
                            {
                                PrintSomething((int)x + 1, (int)y + 1, Colour);
                                PrintSomething((int)x, (int)y + 1, Colour);
                            }

                            //adjusts the y value by the gradient
                            y -= (gradient);

                        }
                    }
                    else
                    {
                        //grabs the starting point
                        double y = startingPointY;

                        //loops through all the x values
                        for (int x = startingPointX; x >= endingPointX; x--)
                        {
                            //stores the colour
                            ConsoleColor Colour;

                            //alternates the colour
                            if (x % 2 == 0)
                            {
                                Colour = ConsoleColor.DarkRed;
                            }
                            else
                            {
                                Colour = ConsoleColor.DarkYellow;
                            }

                            //prints this part to the screen
                            PrintSomething((int)x, (int)y, Colour);
                            PrintSomething((int)x + 1, (int)y, Colour);

                            //prints extra characters if the gradient is steeper
                            if (gradient > 1)
                            {
                                PrintSomething((int)x + 1, (int)y + 1, Colour);
                                PrintSomething((int)x, (int)y + 1, Colour);
                            }

                            //adjusts the y value by the gradient
                            y -= (-gradient);
                        }
                    }


                }
            }
        }

        public static Square[] SetUpBoard(){
            Square[] temp = LoadBoard();
            DisplayBoard(temp);
            return temp;
        }

    }
}
