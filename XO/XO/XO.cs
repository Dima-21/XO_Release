using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class X_O
    {
        char[,] symbol = new char[3, 3];
        Position pos = new Position { X = 0, Y = 0 };
        Position cursorpos = new Position { X = 1, Y = 1 };
        char[] xo = new char[]{ 'X', 'O' };
        int focus_xo;
        Stats stats = new Stats();
        public X_O()
        {
            focus_xo = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    symbol[i, y] = ' ';
                }
            }

        }
        public void Move(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    cursorpos.X -= 4;
                    pos.Y--;
                    break;
                case ConsoleKey.RightArrow:
                    cursorpos.X += 4;
                    pos.Y++;
                    break;
                case ConsoleKey.DownArrow:
                    cursorpos.Y += 2;
                    pos.X++;
                    break;
                case ConsoleKey.UpArrow:
                    cursorpos.Y -= 2;
                    pos.X--;
                    break;
                case ConsoleKey.Spacebar:
                    if (symbol[pos.X, pos.Y] == ' ')
                    {
                        symbol[pos.X, pos.Y] = xo[focus_xo];
                        if (focus_xo == 0)
                            focus_xo++;
                        else
                            focus_xo--;
                    }
                    break;
            }
            if(pos.X > 2)
            {
                pos.X = 0;
                cursorpos.Y = 1;
            }
            else if (pos.X < 0)
            {
                pos.X = 2;
                cursorpos.Y = 5;
            }
            else if (pos.Y > 2)
            {
                pos.Y = 0;
                cursorpos.X = 1;
            }
            else if (pos.Y < 0)
            {
                pos.Y = 2;
                cursorpos.X = 9;
            }
            
        }

        public void NewGame()
        {
            focus_xo = 0;
            pos.X = pos.Y = 0;
            cursorpos.X = cursorpos.Y = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    symbol[i, y] = ' ';
                }
            }

        }
        public void Print()
        {
            Console.WriteLine($@"
  {symbol[0, 0]}| {symbol[0, 1]} |{symbol[0, 2]}
 - + - + -
  {symbol[1, 0]}| {symbol[1, 1]} |{symbol[1, 2]}
 - + - + -
  {symbol[2, 0]}| {symbol[2, 1]} |{symbol[2, 2]}
                                    ");
        }
        public void PrintStats()
        {
            stats.PrintStats();
        }

        public void SetCursor()
        {
            Console.SetCursorPosition(cursorpos.X, cursorpos.Y);
        }

        public bool isWinner()
        {
            if (isWinX())
            {
                stats.winX++;
                stats.countgame++;
                Console.WriteLine("Выиграл Х");
                return true;
            }
            if (isWinO())
            {
                stats.winO++;
                stats.countgame++;
                Console.WriteLine("Выиграл O");
                return true;
            }
            if (isDH())
            {
                stats.DH++;
                stats.countgame++;
                Console.WriteLine("Ничья");
                return true;
            }
            return false;
        }
       

        public void Save()
        {
            stats.SaveToFile(symbol, focus_xo);
        }
        public void Load()
        {
            symbol = stats.LoadFromFile(ref focus_xo);
        }

        private bool isWinX()
        {
            if (String.Join("", symbol[pos.X, 0], symbol[pos.X, 1], symbol[pos.X, 2]) == "XXX" ||
                String.Join("", symbol[0, pos.Y], symbol[1, pos.Y], symbol[2, pos.Y]) == "XXX" ||
                String.Join("", symbol[0, 0], symbol[1, 1], symbol[2, 2]) == "XXX" ||
                String.Join("", symbol[0, 2], symbol[1, 1], symbol[2, 0]) == "XXX")
            {
                return true;
            }
            return false;
        }

        private bool isWinO()
        {
            if (String.Join("", symbol[pos.X, 0], symbol[pos.X, 1], symbol[pos.X, 2]) == "OOO" ||
                String.Join("", symbol[0, pos.Y], symbol[1, pos.Y], symbol[2, pos.Y]) == "OOO" ||
                String.Join("", symbol[0, 0], symbol[1, 1], symbol[2, 2]) == "OOO" ||
                String.Join("", symbol[0, 2], symbol[1, 1], symbol[2, 0]) == "OOO")
            {
                return true;
            }
            return false;
        }

        private bool isDH() // DH - dead heat
        {
            for (int i = 0; i < 3; i++)
                for (int y = 0; y < 3; y++)
                    if (symbol[i, y] == ' ')
                        return false;
            return true;
        }
    }
}

        