using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Game
{
    class Game
    {
        string[][] field;

        public Game(int x, int y)
        {
            field = new string[x][];
            for (int i = 0; i < x; i++)
            {
                field[i] = new string [y];
                for (int j = 0; j < x; j++)
                {
                    field[i][j] = "_";
                }

            }
        }

        public void DrawField() {
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

    }
}