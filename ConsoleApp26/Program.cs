using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    
    internal class Program
    {
        const int ScaleMapX = 25;
        static int ScaleMapY = 25;

        static Vector Input;
        static void Main(string[] args)
        {
            Player player = new Player();
            char[,] Maps = new char[ScaleMapX, ScaleMapY];
            var t = new Thread(InputSystem)
            {
                IsBackground = true,
                Name = "MyChecker"
            };

            int a = 0;
            while (true)
            {
                for(int i = 0 ; i < Maps.GetLength(0); i++)
                {
                    for (int j = 0; j < Maps.GetLength(1); j++)
                    {
                        if(i == 0 || j == 0 || i == Maps.GetLength(1)-1 || j == Maps.GetLength(1)-1)
                        {
                            Maps[i, j] = '#';
                            Console.Write('#');
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    Console.WriteLine();
                }
                var Window = new StringBuilder(ScaleMapX, ScaleMapY);
                Thread.Sleep(1000);
                Console.Clear();
            }



        }
        static void InputSystem()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.W:
                        Input.ChangeUp();
                        break;
                    case ConsoleKey.S:
                        Input.ChangeDown();
                        break;
                    case ConsoleKey.A:
                        Input.ChangeLeft();
                        break;
                    case ConsoleKey.D:
                        Input.ChangeRight();
                        break;

                }
                
            }
        }
    }
    public struct Vector
    {
        private bool _up;
        private bool _down;
        private bool _left;
        private bool _right;
        public Vector(bool Up, bool Down, bool Left, bool Right)
        {
            _up = Up;
            _down = Down;
            _left = Left;
            _right = Right;
        }
        public void ChangeUp()
        {
            _up = true;
            _down = false;
            _left = false;
            _right = false;
        }
        public void ChangeDown()
        {
            _up = false;
            _down = true;
            _left = false;
            _right = false;
        }
        public void ChangeLeft()
        {
            _up = false;
            _down = false;
            _left = true;
            _right = false;
        }
        public void ChangeRight()
        {
            _up = false;
            _down = false;
            _left = false;
            _right = true;
        }

    }
    class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private Vector _vectorP;

        public Player()
        {
            X = 25 / 2;
            Y = 25 / 2;
            _vectorP = new Vector(true, false, false, false);
        }

    }
}
