using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Menu
    {
        X_O xo = new X_O();
        ConsoleKey key;
        public Menu()
        {
            xo.Load();
        }
        
        public void MainMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("1. Продолжить");
                Console.WriteLine("2. Новая игра");
                Console.WriteLine("3. Статистика игр");
                Console.WriteLine($"-------------{Environment.NewLine}Esc - Выход. {Environment.NewLine}Backspace - Выход в главное меню");
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.NumPad1:
                        Continue();
                        break;

                    case ConsoleKey.NumPad2:
                        NewGame();
                        break;

                    case ConsoleKey.NumPad3:
                        Stats();
                        break;
                }
            } while (key != ConsoleKey.Escape);
            xo.Save();
        }

        private void Continue()
        {

            do
            {
                Console.Clear();
                xo.Print();
                if (xo.isWinner())
                {
                    xo.NewGame();
                    Console.WriteLine("Для продолжения нажмите любую клавишу...");
                }
                xo.SetCursor();
                key = Console.ReadKey().Key;
                xo.Move(key);
            } while (key != ConsoleKey.Backspace);
        }

        private void NewGame()
        {
            xo.NewGame();
            Continue();
        }

        private void Stats()
        {
            Console.Clear();
            xo.PrintStats();
            Console.WriteLine("Для возврата в главное меню нажмите любую клавишу...");
            Console.ReadKey();
        }

    }
}
