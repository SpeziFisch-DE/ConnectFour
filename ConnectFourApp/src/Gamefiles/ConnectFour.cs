using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Game
{
    class Game
    {
        string[][] field;
        int winCount;
        bool AI = false;

        public Game(int x, int y, int winCount, bool ai)
        {
            field = new string[x][];
            for (int i = 0; i < x; i++)
            {
                field[i] = new string[y];
                for (int j = 0; j < y; j++)
                {
                    field[i][j] = "_";
                }

            }
            this.winCount = winCount;
            AI = ai;
        }

        public static Game CreateGameFromConsole()
        {
            int x = 3;
            int y = 3;
            int winCount = 3;
            bool ai = false;
            int formPosition = 0;
            do
            {
                string input = "";
                if (formPosition == 0)
                {
                    Console.WriteLine("set row quantity: ");
                    input = Console.ReadLine();
                    string pattern = "[0-9]";
                    Regex rg = new Regex(pattern);
                    if (rg.Match(input).Success)
                    {
                        int intInput = Convert.ToInt32(input);
                        if (intInput >= 3)
                        {
                            x = intInput;
                            formPosition = 1;
                        }
                        else
                        {
                            Console.WriteLine("Quantity can't be lower than 3!");
                        }
                    }
                }
                if (formPosition == 1)
                {
                    Console.WriteLine("set column quantity: ");
                    input = Console.ReadLine();
                    string pattern = "[0-9]";
                    Regex rg = new Regex(pattern);
                    if (rg.Match(input).Success)
                    {
                        int intInput = Convert.ToInt32(input);
                        if (intInput >= 3)
                        {
                            y = intInput;
                            formPosition = 2;
                        }
                        else
                        {
                            Console.WriteLine("Quantity can't be lower than 3!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input needs to be a number");
                    }
                }
                if (formPosition == 2)
                {
                    Console.WriteLine("set amount of connections needed to win: ");
                    input = Console.ReadLine();
                    string pattern = "[0-9]";
                    Regex rg = new Regex(pattern);
                    if (rg.Match(input).Success)
                    {
                        int intInput = Convert.ToInt32(input);
                        if ((intInput <= x) && (intInput <= y))
                        {
                            winCount = intInput;
                            formPosition = 3;
                        }
                        else
                        {
                            Console.WriteLine("Can't be higher than any of the field axes!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input needs to be a number");
                    }
                }
                if (formPosition == 3)
                {
                    Console.WriteLine("play against Computer?");
                    Console.WriteLine("(y/n)");
                    input = Console.ReadLine();
                    if (input == "y")
                    {
                        ai = true;
                        formPosition = 4;
                    }
                    else if (input == "n")
                    {
                        ai = false;
                        formPosition = 4;
                    }
                    else
                    {
                        Console.WriteLine("wrong input. Please use either y or n");
                    }
                }
            } while (formPosition < 4);
            return new Game(x, y, winCount, ai);
        }

        public void DrawField()
        {
            for (int i = 0; i < field.Length; i++)
            {
                string row = "";
                for (int j = 0; j < field[i].Length; j++)
                {
                    row += "[" + field[i][j] + "]";
                }
                Console.WriteLine(row);
            }
        }

        public static string DoGameLoop()
        {
            Game game = CreateGameFromConsole();
            string input = "";
            bool playerTurn = true;

            bool draw = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Play");
                Console.WriteLine("");
                game.DrawField();
                Console.WriteLine("");

                if (!game.AI)
                {
                    Console.WriteLine("player " + (playerTurn ? "X" : "O") + " choose a column:");
                }
                else
                {
                    Console.WriteLine("choose a column:");
                }
                string choices = "";
                for (int i = 0; i < game.field[0].Length; i++)
                {
                    choices = choices + "[" + i + "]";
                }
                Console.WriteLine(choices);
                if (game.AI)
                {
                    if (playerTurn)
                    {
                        input = Console.ReadLine();
                        string pattern = "[0-9]";
                        Regex rg = new Regex(pattern);
                        if (rg.Match(input).Success)
                        {
                            int intInput = Convert.ToInt32(input);
                            if ((intInput >= 0) && (intInput < game.field[0].Length))
                            {
                                if (game.SetColumn(intInput, "X"))
                                {
                                    playerTurn = !playerTurn;
                                }
                                else
                                {
                                    Console.WriteLine("Column is full. Choose another Column.");
                                    Console.WriteLine("[ENTER]");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Chosen column out of range!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("AI is choosing...");
                        Console.WriteLine("[ENTER]");
                        Console.ReadKey();
                        var rand = new Random();
                        int aiColumn = rand.Next(0, game.field[0].Length);
                        while (!game.SetColumn(aiColumn, "O"))
                        {
                            aiColumn = rand.Next(0, game.field[0].Length);
                        }
                        playerTurn = !playerTurn;
                    }
                }
                else
                {
                    input = Console.ReadLine();
                    string pattern = "[0-9]";
                    Regex rg = new Regex(pattern);
                    if (rg.Match(input).Success)
                    {
                        int intInput = Convert.ToInt32(input);
                        if ((intInput >= 0) && (intInput < game.field[0].Length))
                        {
                            game.SetColumn(intInput, (playerTurn ? "X" : "O"));
                            playerTurn = !playerTurn;
                        }
                        else
                        {
                            Console.WriteLine("Chosen column out of range!");
                        }
                    }
                }

                int columnAtTopCount = 0;
                for (int i = 0; i < game.field[0].Length; i++)
                {
                    if (game.field[0][i] != "_")
                    {
                        columnAtTopCount = columnAtTopCount + 1;                        
                    }
                }
                draw = (columnAtTopCount.Equals(game.field[0].Length));
            } while (!(game.WinCheck("X") || game.WinCheck("O") || draw));
            Console.Clear();
            Console.WriteLine("Play");
            Console.WriteLine("");
            game.DrawField();
            Console.WriteLine("");

            string winner = game.WinCheck("X") ? "X" : (game.WinCheck("O") ? "O" : "_");

            if (winner != "_")
            {
                Console.WriteLine("Player " + winner + " won!");
            } else {
                Console.WriteLine("It's a draw!");
            }
            Console.WriteLine("[back]");
            return winner;
        }
        public bool SetColumn(int y, string player)
        {
            if (field[0][y] != "_")
            {
                return false;
            }
            else
            {
                for (int i = 1; i < field.Length; i++)
                {
                    bool placed = false;
                    if (field[i][y] != "_")
                    {
                        field[i - 1][y] = player;
                        placed = true;
                    }
                    else if (i + 1 == field.Length && field[i][y] == "_")
                    {
                        field[i][y] = player;
                        placed = true;
                    }
                    if (placed)
                    {
                        break;
                    }
                }
                return true;
            }
        }

        public bool WinCheck(string player)
        {
            return (Connections(player) >= winCount);
        }

        private int FieldConnectionsInDirection(string player, int x, int y, int way_x, int way_y)
        {
            if (field[x][y] != player)
            {
                return 0;
            }
            if ((x + way_x < field.Length && x + way_x >= 0) && (y + way_y < field.Length && y + way_y >= 0))
            {
                if (field[x + way_x][y + way_y] == player)
                {
                    return 1 + FieldConnectionsInDirection(player, x + way_x, y + way_y, way_x, way_y);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 1;
            }
        }

        private int FieldConnections(string player, int x, int y)
        {
            int maxWorth = 0;
            for (int i = -1; i <= 1; i += 1)
            {
                for (int j = -1; j <= 1; j += 1)
                {
                    if (!(i == 0 && j == 0))
                    {
                        int compare = FieldConnectionsInDirection(player, x, y, i, j);
                        if (compare > maxWorth)
                        {
                            maxWorth = compare;
                        }
                    }
                }
            }
            return maxWorth;
        }

        private int Connections(string player)
        {
            int maxWorth = 0;
            for (int i = 0; i < field.Length; i++)
            {
                for (int j = 0; j < field[i].Length; j++)
                {
                    int compare = FieldConnections(player, i, j);
                    if (compare > maxWorth)
                    {
                        maxWorth = compare;
                    }
                }
            }
            return maxWorth;
        }

    }
}