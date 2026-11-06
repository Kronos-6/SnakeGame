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

        const int ScaleMapX = 15;  // Тетс Git
        const int ScaleMapY = 30;   //Должен быть меньше ScaleMapX 

        //static Vector Input;
        static int InputInt;



        static void Main(string[] args)
        {
            Console.WriteLine("Загрузка");
            Thread.Sleep(150);
            var t = new Thread(InputSystem)
            {
                IsBackground = true,
                Name = "MyChecker"
            };
            t.Start();
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetBufferSize(120, 300);
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            GameObgectMemory MapObject = new GameObgectMemory(ScaleMapX, ScaleMapY);
            while (true)
            {
                MapObject.TurnPlayer(InputInt);
                if (MapObject.ISWall() == true)
                {
                    break;
                }
                MapObject.MovePlayer();
                MapObject._renderer.Displaying();
                Thread.Sleep(150); //300 //150
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(ScaleMapX*2 +5, ScaleMapY/4);// Самая магическая часть программы
            Console.WriteLine("Игра окончена");
            Thread.Sleep(2000);
            Console.SetCursorPosition(0, ScaleMapX);
        }

        static void InputSystem()
        {
            while (true)
            {
                //Console.WriteLine("Привет из 2 потока");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        InputInt = 0;
                        break;
                    case ConsoleKey.S:
                        InputInt = 1;
                        break;
                    case ConsoleKey.A:
                        InputInt = 2;
                        break;
                    case ConsoleKey.D:
                        InputInt = 3;
                        break;
                }
            }
        }

    }
    public struct Vector
    {
        public bool _up { get; private set; }
        public bool _down { get; private set; }
        public bool _left { get; private set; }
        public bool _right { get; private set; }
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
    public struct VectorSimple
    {
        public bool? Vertical;
        public bool? Horizontal;
        public VectorSimple(int index)
        {
            Vertical = null;
            Horizontal = null;
            switch (index)
            {
                case 0:
                    ChangeUp(); break;
                case 1: ChangeDown(); break;
                case 2: ChangeLeft(); break;
                case 3: ChangeRight(); break;
                default: Console.WriteLine("Ошибка не может быть больше 3 VectorSimple Конструктор"); break;
            }
        }
        private void ChangeUp()
        {
            Vertical = true;
            Horizontal = null;
        }
        private void ChangeDown()
        {
            Vertical = false;
            Horizontal = null;
        }
        private void ChangeLeft()
        {
            Vertical = null;
            Horizontal = true;
        }
        private void ChangeRight()
        {
            Vertical = null;
            Horizontal = false;
        }
        public int CheckingStatus()
        {
            if (Vertical == true)
            {
                return 0;
            }
            else if (Vertical == false)
            {
                return 1;
            }
            else if (Horizontal == true)
            {
                return 2;
            }
            else if (Horizontal == false)
            {
                return 3;
            }
            else
            {
                Console.WriteLine("Ошибка,  Везде Null VectorSimple CheckingStatus");
                return 0;
            }
        }
        public void ChangeMove(int index)
        {
            switch (index)
            {
                case 0:
                    ChangeUp();
                    break;
                case 1:
                    ChangeDown();
                    break;
                case 2:
                    ChangeLeft();
                    break;
                case 3:
                    ChangeRight();
                    break;
                default:
                    Console.WriteLine("Ощибка VectorSimple ChangeMove index не может более 4 и менее 0");
                    break;


            }
        }

    }
    public struct Coordinates
    {
        public int X;
        public int Y;
        public Coordinates(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
    class GameObgectMemory
    {
        private GameObject[,] matrix;
        public Coordinates _Scale { get; private set; }
        public Renderer _renderer { get; private set; }
        private Player _player;
        public GameObgectMemory(int x, int y)
        {
            matrix = new GameObject[x, y];
            _Scale = new Coordinates(x, y);
            _renderer = new Renderer(this, _Scale, matrix);
            CreatingAScene();
        }
        private void CreatingAScene()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == matrix.GetLength(0) - 1 || j == matrix.GetLength(1) - 1)
                    {
                        matrix[i, j] = new Woall();
                    }
                    else
                    {
                        matrix[i, j] = new GameObject();
                    }
                }
            }
            _player = new Player(_Scale.X / 2, _Scale.Y / 2);
            matrix[_Scale.X / 2, _Scale.Y / 2] = _player;
        }
        public void TurnPlayer(int index)
        {
            _player.Direction.ChangeMove(index);
            _player.HeadOrientation();
        }
        public bool ISWall()
        {
            Coordinates coordinatesPlayer = new Coordinates();
            coordinatesPlayer = _player.Coordinates;
            int direct = _player.Direction.CheckingStatus();
            switch (direct)
            {
                case 0:
                    coordinatesPlayer.X--;
                    break;
                case 1:
                    coordinatesPlayer.X++;
                    break;
                case 2:
                    coordinatesPlayer.Y--;
                    break;
                case 3:
                    coordinatesPlayer.Y++;
                    break;
            }
            if (matrix[coordinatesPlayer.X, coordinatesPlayer.Y] is Woall w)
            {
                if (w._isCollision == true)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        public void MovePlayer()
        {

            Coordinates coordinates = new Coordinates();
            coordinates = _player.Coordinates;
            int direct = _player.Direction.CheckingStatus();
            switch (direct)
            {
                case 0:
                    _player.Coordinates.X--;
                    break;
                case 1:
                    _player.Coordinates.X++;
                    break;
                case 2:
                    _player.Coordinates.Y--;
                    break;
                case 3:
                    _player.Coordinates.Y++;
                    break;

            }
            //for(int i = _player.Ta)
            СhangeGameObject(coordinates, _player.Coordinates);
        }
        private void СhangeGameObject(Coordinates Chang1, Coordinates Chang2)
        {
            GameObject memory = new GameObject();
            memory = matrix[Chang1.X, Chang1.Y];
            matrix[Chang1.X, Chang1.Y] = matrix[Chang2.X, Chang2.Y];
            matrix[Chang2.X, Chang2.Y] = memory;
        }
    }
    class Renderer
    {
        private readonly GameObject[,] _matrix;
        private readonly Coordinates CasheScale;
        private readonly ConsoleColor[] consoleColors = { ConsoleColor.Magenta, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.White };
        private int indexColor = 0;
        private char[,] simbolMatrix;
        public Renderer(GameObgectMemory matrix, Coordinates casheScale, GameObject[,] Matrix)
        {
            _matrix = Matrix;
            CasheScale = casheScale;
            simbolMatrix = new char[CasheScale.X, CasheScale.Y];
        }

        public void Displaying()
        {

            Console.ForegroundColor = consoleColors[indexColor];
            if (indexColor + 1 >= consoleColors.Length)
                indexColor = 0;
            else
                indexColor++;
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    simbolMatrix[i, j] = _matrix[i, j]._sprite;
                }
            }
            var Window = new StringBuilder(25 * 25);
            for (int i = 0; i < simbolMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < simbolMatrix.GetLength(1); j++)
                {
                    Window.Append(simbolMatrix[i, j]);
                }
                Window.Append('\n');
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Window.ToString());

        }

    }

    class GameObject
    {
        public char _sprite { get; protected set; }
        public GameObject()
        {
            _sprite = ' ';
        }
    }
    class Woall : GameObject
    {
        public bool _isCollision { get; private set; }
        public Woall()
        {
            _sprite = '#';
            _isCollision = true;
        }
    }
    class Player : GameObject
    {
        private Queue<Tail> Tails = new Queue<Tail>();
        public VectorSimple Direction;
        public Coordinates Coordinates;
        public Player(int x, int y)
        {
            _sprite = '˄';
            Coordinates = new Coordinates(x, y);
            Direction = new VectorSimple(0);
            HeadOrientation();
        }
        public void HeadOrientation()
        {
            int direct = Direction.CheckingStatus();
            switch (direct)
            {
                case 0:
                    _sprite = '˄';
                    break;
                case 1:
                    _sprite = '˅';
                    break;
                case 2:
                    _sprite = '˂';
                    break;
                case 3:
                    _sprite = '˃';
                    break;
            }
        }
        public void AddTail(Coordinates coordinates)
        {
            Tails.Enqueue(new Tail(coordinates));
        }

    }
    class Tail : GameObject
    {
        public Coordinates position = new Coordinates(); 
        public Tail(Coordinates coordinates)
        {
            _sprite = 'O';
            position = coordinates;
        }
    }

}
