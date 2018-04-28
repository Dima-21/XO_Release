using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Stats
    {
        string file = "stats.bat";
        public int countgame { get; set; }
        public int winX { get; set; }   
        public int winO { get; set; }
        public int DH { get; set; }
        public void SaveToFile(char[,] c, int focus)
        {
            
            FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
            for (int i = 0; i < 3; i++)
                for (int y = 0; y < 3; y++)
                    fs.WriteByte((byte)c[i, y]);
            fs.WriteByte((byte)countgame);
            fs.WriteByte((byte)focus);
            fs.WriteByte((byte)winX);
            fs.WriteByte((byte)winO);
            fs.WriteByte((byte)DH);
            fs.Close();
        }
        public char[,] LoadFromFile(ref int focus)
        {
            char[,] tmp = new char[3, 3];
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                for (int i = 0; i < 3; i++)
                    for (int y = 0; y < 3; y++)
                        tmp[i, y] = (char)fs.ReadByte();
                countgame = (int)fs.ReadByte();
                focus = (int)fs.ReadByte();
                winX = (int)fs.ReadByte();
                winO = (int)fs.ReadByte();
                DH = (int)fs.ReadByte();
                fs.Close();
            }
            catch(Exception e)
            {}
            return tmp;
        }

       public void PrintStats()
        {
            Console.WriteLine($"Количество игр: {countgame}");
            Console.WriteLine($"Количество побед Х: {winX}");
            Console.WriteLine($"Количество побед O: {winO}");
            Console.WriteLine($"Количество ничьих: {DH}");
        }
    }
}
