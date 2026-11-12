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

        static int ScaleMapX = 15;  // 15   /25 Max  /4
        static int ScaleMapY = 30;   //30   /120 Max  /4
        static int TimeSpeed = 167;
        static int StaticTimeSpeed = 167;
        static bool SecondСohort;

        //static Vector Input;
        static int InputInt;



        static void Main(string[] args)
        {

            var t = new Thread(InputSystem)
            {
                IsBackground = true,
                Name = "MyChecker"
            };
            t.Start();
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetBufferSize(120, 300);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = true;
              //mim 20 /max 800
            //int void0 = 17;
            //string iqA1 = "Змейка";
            //Console.WriteLine(new string(' ', void0) + iqA1 + new string(' ', void0));
            //Console.WriteLine(new string('-', void0 *2+ iqA1.Length));
            MenuStart();
            return;


        }
        static void MenuStart()
        {
            Console.Clear();
            Console.Write("Быстрая игра? y/n: ");
            string start0 = Console.ReadLine();
            if (start0 == "y" || start0 == "Y")
            {
                MainCycle();
            }
            else if (start0 == "n" || start0 == "N")
            {
                MenuSetting();
            }
            else
            {
                MenuStart();
            }
        }
        static void MenuSetting()
        {
            Console.Clear();
            int void0 = 17;
            string iqA1 = "Настройки";
            Console.WriteLine(new string(' ', void0) + iqA1 + new string(' ', void0));
            Console.WriteLine(new string('-', void0 * 2 + iqA1.Length));
            Console.WriteLine("1. Вернутся в игру \n"+ "2. Выбор сложности \n" + "3. Настроить размер карты");
            Console.WriteLine("4. Настройки разработчка (В разработке,к  не доступоно)");
            string SettingInput = Console.ReadLine();
            if(SettingInput == "1")
            {
                Console.Clear();
                MainCycle();
                
            }
            else if (SettingInput == "2")
            {
                Console.Clear();
                Console.WriteLine("1.Легко \n" + "2.Нормально \n" + "3.Тяжело \n");
                string SettingInput1 = Console.ReadLine();
                if (SettingInput1 == "1")
                {
                    StaticTimeSpeed = 300;
                }
                else if(SettingInput1 == "2")
                {
                    StaticTimeSpeed = 167;
                }
                else if(SettingInput1 == "3")
                {
                    StaticTimeSpeed = 80;
                }
                MenuSetting();
            }
            else if (SettingInput == "3")
            {
                Console.Clear();
                Console.WriteLine("1.Маленькая \n" + "2.Нормальная  \n" + "3.Большая \n");
                string SettingInput1 = Console.ReadLine();
                if (SettingInput1 == "1")
                {
                    ScaleMapX = 8;
                    ScaleMapY = 15;
                }
                else if (SettingInput1 == "2")
                {
                    ScaleMapX = 15;
                    ScaleMapY = 30;
                }
                else if (SettingInput1 == "3")
                {
                    ScaleMapX = 25;
                    ScaleMapY = 120;
                }
                MenuSetting();
                MainCycle();
            }
            else
            {
                MainCycle();
                Console.WriteLine("Команда не расползнона");
            }
        }
        static void MainCycle()
        {
            SecondСohort = true;
            TimeSpeed = StaticTimeSpeed;
            Console.CursorVisible = false;
            Console.Clear();
            Console.Write("Загрузка");
            Console.CursorVisible = false;
            GameObgectManager MapObject = new GameObgectManager(ScaleMapX, ScaleMapY, TimeSpeed);
            Thread.Sleep(150);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("           ");
            MapObject._renderer.Displaying(MapObject._time, MapObject.Score,  MapObject._Scale);
            while (true)
            {
                MapObject.TechnicalStep(ref TimeSpeed);
                MapObject.MakeApple();
                MapObject.TurnPlayer(InputInt);
                if (MapObject.ISWall() == true)
                {
                    MapObject._renderer.Displaying(MapObject._time, MapObject.Score, MapObject._Scale);
                    break;
                }
                MapObject.MovePlayer();
                MapObject._renderer.Displaying(MapObject._time, MapObject.Score, MapObject._Scale);
                Thread.Sleep(TimeSpeed); //300 //150
            }
            Console.ForegroundColor = ConsoleColor.White;
            if(ScaleMapY<90)
            {
                Console.SetCursorPosition(ScaleMapX * 2 + 5, ScaleMapY / 4);// Самая магическая часть программы
            }
            SecondСohort = false;
            Console.WriteLine("Игра окончена");
            Thread.Sleep(2000);
            Console.SetCursorPosition(0, ScaleMapX+1);
            Console.CursorVisible = true;
            Console.WriteLine("Enter чтобы начать заново, s чтобы открыть настройки: ");
            string gameMenu =  Console.ReadLine();
            if (gameMenu == "s" || gameMenu == "S")
            {
                MenuSetting();
            }
            else 
            {
                MainCycle();
            }
        }
        static void InputSystem()
        {
            while (true)
            {
                if(SecondСohort)
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
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            InputInt = 0;
                            break;
                        case ConsoleKey.DownArrow:
                            InputInt = 1;
                            break;
                        case ConsoleKey.LeftArrow:
                            InputInt = 2;
                            break;
                        case ConsoleKey.RightArrow:
                            InputInt = 3;
                            break;
                    }
                }
            }
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
    class GameObgectManager
    {
        private GameObject[,] matrix;
        public Renderer _renderer { get; private set; }
        private Player _player;
        private static readonly Floor EmptyFloor = new Floor();
        private Random rnd = new Random();
        public Coordinates _Scale { get; private set; }
        public int _time { get; private set; }
        private bool _ateApple = false;
        private int _freeAplle = 0;
        private float _delayOnePercent =0;
        private float minSpeed = 0;
        public int Score { get; private set; }
        private int _scoreMem = 0;
        public GameObgectManager(int x, int y, int delay)
        {
            matrix = new GameObject[x, y];
            _Scale = new Coordinates(x, y);
            _renderer = new Renderer(this, _Scale, matrix);
            CreatingAScene();
            _time = 0;
            Score = 0;
            _delayOnePercent = delay / 100;
            minSpeed = _delayOnePercent * 48;
        }
        public void TurnPlayer(int index)
        {
            _player.Direction.ChangeMove(index);
            _player.HeadOrientation();
        }
        public bool ISWall()
        {
            if(_player.Tails.Count>0)
            {
                _player.Tails.Peek().IsLastTeil = true;
            }
            Coordinates coordinatesPlayer = _player.Coordinates;
            coordinatesPlayer = Coordinator(coordinatesPlayer);

            if (matrix[coordinatesPlayer.X, coordinatesPlayer.Y] is Woall w)
            {
                if (w._isCollision == true)
                    return true;
                else
                    return false;
            }
            else if(matrix[coordinatesPlayer.X, coordinatesPlayer.Y] is Tail q)
            {
                if(q.IsLastTeil == true && _player.Tails.Count>1)
                    return false;
                else
                    return true;
            }
            else if (matrix[coordinatesPlayer.X, coordinatesPlayer.Y] is Apple)
            {
                _freeAplle--;
                _ateApple = true;
                Score++;
                _scoreMem++;
                return false;
            }
            else
                return false;
        }
        public void MovePlayer()
        {
            Coordinates lastPlayer = _player.Coordinates;
            _player.Coordinates = Coordinator(_player.Coordinates);
            
            if(_ateApple == false && _player.Tails.Count>0)
            {
                
                Tail memTail = _player.Tails.Dequeue();
                Coordinates memCoordinaTeil = memTail.position;
                matrix[memCoordinaTeil.X, memCoordinaTeil.Y] = EmptyFloor;   //Финальный костыль 
                ChangeGameObject(lastPlayer, _player.Coordinates);
                if (memCoordinaTeil.X != _player.Coordinates.X && memCoordinaTeil.Y != _player.Coordinates.Y)
                {
                    matrix[lastPlayer.X, lastPlayer.Y] = memTail;
                    matrix[memCoordinaTeil.X, memCoordinaTeil.Y] = EmptyFloor;
                }
                else
                    matrix[lastPlayer.X, lastPlayer.Y] = memTail;
                memTail.position = lastPlayer;
                memTail.IsLastTeil = false;
                _player.Tails.Enqueue(memTail);
                _player.Tails.Peek().IsLastTeil= true;
            }
            else if(_ateApple == true)
            {
                ChangeGameObject(lastPlayer, _player.Coordinates);
                MakeTail(lastPlayer);
                _ateApple = false;
            }
            else /*(_player.Tails.Count == 0)*/
            {
                ChangeGameObject(lastPlayer, _player.Coordinates);
            }

            //Console.SetCursorPosition(_Scale.X * 2 + 5, 0);
            //Console.Write($"P X:{_player.Coordinates.X}  Y:{_player.Coordinates.Y}   ");
            //Console.SetCursorPosition(_Scale.X * 2 + 5, 1);
            //Console.Write($"Длина хвоста {_player.Tails.Count}");
            //if(_player.Tails.Count > 0)
            //{
            //    Console.SetCursorPosition(_Scale.X * 2 + 5, 2);
            //    Console.Write($"Последний хвост на координатах X:{_player.Tails.Peek().position.X}   Y:{_player.Tails.Peek().position.Y}   ");
            //}
            

        }
        public void MakeApple()
        {
            if (_time%25 == 0)
            {
                int maxiteration = ((_Scale.X - 2) * (_Scale.Y - 2)) - _player.Tails.Count - 1 - _freeAplle;
                if(maxiteration < 0)
                    maxiteration = 0;
                int index = 0;
                bool flag0 = true;
                while(flag0)
                {

                    if (index   >= maxiteration)
                        break;
                    int a = rnd.Next(1, _Scale.X - 1);
                    int b = rnd.Next(1, _Scale.Y - 1);
                    if (matrix[a,b] is Floor)
                    {
                        matrix[a, b] = new Apple();
                        index++;
                        _freeAplle++;
                        flag0 = false;
                    }
                }
                
            }
        }
        public void TechnicalStep(ref int _delay)
        {
            TimeAtTop();
            GrowingComplexity(ref _delay);

        }
        private void TimeAtTop()
        {
            _time++;
        }
        private void GrowingComplexity(ref int _delay0)
        {
            if (_scoreMem % 5 == 0 && _scoreMem > 0 && _delay0>minSpeed + 8)
            {
                _scoreMem = 0;
                _delay0 -= 8;
            }
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
                        matrix[i, j] = new Floor();
                    }
                }
            }
            _player = new Player(_Scale.X / 2, _Scale.Y / 2);
            matrix[_Scale.X / 2, _Scale.Y / 2] = _player;
        }
        private Coordinates Coordinator(Coordinates player)
        {
            int direct = _player.Direction.CheckingStatus();
            switch (direct)
            {
                case 0:
                    player.X--;
                    break;
                case 1:
                    player.X++;
                    break;
                case 2:
                    player.Y--;
                    break;
                case 3:
                    player.Y++;
                    break;
            }
            return player;
        }
        private void ChangeGameObject(Coordinates Chang1, Coordinates Chang2)
        {
             
            GameObject memory = matrix[Chang1.X, Chang1.Y];
            matrix[Chang1.X, Chang1.Y] = matrix[Chang2.X, Chang2.Y];
            matrix[Chang2.X, Chang2.Y] = memory;
        }
        private void MakeTail(Coordinates coordinates)
        {
            Tail tail = new Tail(coordinates);
            matrix[coordinates.X, coordinates.Y] = tail;
            _player.Tails.Enqueue(tail);



        }
    }
    class Renderer
    {
        private readonly GameObject[,] _matrix;
        private readonly Coordinates CasheScale;
        private readonly ConsoleColor[] consoleColors = { ConsoleColor.Magenta, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.White };
        private int indexColor = 0;
        private char[,] simbolMatrix;
        public Renderer(GameObgectManager matrix, Coordinates casheScale, GameObject[,] Matrix)
        {
            _matrix = Matrix;
            CasheScale = casheScale;
            simbolMatrix = new char[CasheScale.X, CasheScale.Y];
        }

        public void Displaying(int Time, int score, Coordinates scale)
        {

            if(Time % 8 == 0)
            {
                Console.ForegroundColor = consoleColors[indexColor];
                if (indexColor + 1 >= consoleColors.Length)
                    indexColor = 0;
                else
                    indexColor++;
            }
            
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    simbolMatrix[i, j] = _matrix[i, j]._sprite;
                }
            }
            var Window = new StringBuilder(scale.X*scale.Y);
            for (int i = 0; i < simbolMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < simbolMatrix.GetLength(1); j++)
                {
                    Window.Append(simbolMatrix[i, j]);
                }
                Window.Append('\n');
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(Window.ToString());
            Console.WriteLine($"Счёт {score}  ");
        }

    }

    abstract class GameObject
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
    class Floor : GameObject
    {
        public Floor()
        {
            _sprite = ' ';
        }
        public Floor(char Sprite)
        {
            _sprite = Sprite;
        }
    }
    class Player : GameObject
    {
        public Queue<Tail> Tails = new Queue<Tail>();
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
    }
    class Tail : GameObject
    {
        public Coordinates position = new Coordinates(); 
        public bool IsLastTeil = false;
        public Tail(Coordinates coordinates)
        {
            _sprite = 'O';
            position = coordinates;
        }
    }
    class Apple  : GameObject
    {
        public Apple()
        {
            _sprite = '@';
        }
    }
}
