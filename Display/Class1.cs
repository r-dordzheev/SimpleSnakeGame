namespace GameManager
{
    public static class Display
    {
        public static char[,] DisArr { get; internal set; } = null;
        static int sizeX = 5;
        static int sizeY = 5;
        static char voidChar = '#';
        public static char VoidChar {
            get {
                return voidChar;
            }
            set {
                for (int i = 0; i < SizeY; i++)
                {
                    for (int j = 0; j < SizeX; j++)
                    {
                        if (DisArr[i, j] == voidChar) {
                            DisArr[i, j] = value;
                        }
                    }
                }
                voidChar = value;
            }
        }

        public static int SizeX {
            get {
                return sizeX;
            }
            private set {
                if (value >= 2)
                {
                    sizeX = value;
                }
                else {
                    throw new DisplaySizeException();
                }
            }
        }
        public static int SizeY {
            get
            {
                return sizeY;
            }
            private set
            {
                if (value >= 2)
                {
                    sizeY = value;
                }
                else
                {
                    throw new DisplaySizeException();
                }
            }
        }

        public static void Create() {
            if (DisArr != null) {
                throw new DisplayIsCreatedException();
            }
            DisArr = new char[sizeY,sizeX];
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    DisArr[i, j] = voidChar;
                }
            }
        }
        public static void Create(int X, int Y) {
            if (DisArr != null)
            {
                throw new DisplayIsCreatedException();
            }
            SizeX = X;
            SizeY = Y;
            DisArr = new char[sizeY, sizeX];
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    DisArr[i, j] = voidChar;
                }
            }

        }
        public static void Refresh() {
            if (DisArr == null) {
                throw new DisplayIsNullException();
            }
            Console.Clear();
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    Console.Write(DisArr[i, j]);
                }
                Console.WriteLine();
            }
        }

    }
    public class Player
    {
        int playerX = 2;
        int playerY = 2;
        public int predX;
        public int predY;
        static char playerChar = '@';
        public bool obstacleOnThisCell = false;
        public Directions NowDirection { get { return nowDirection; } private set {
                pastDirection = nowDirection;
                nowDirection = value;
            } 
        }
        Directions nowDirection = Directions.Up;
        public Directions pastDirection { get; private set; } = new Directions();

        /*
        public static char PlayerChar {
            get {
                return playerChar;
            }
            set {
                Display.DisArr[b.playerY - 1, playerX - 1] = value;
                playerChar = value;
            }
        }*/
        

        public int PlayerX
        {
            get
            {
                return playerX;
            }
            set
            {
                if (value > Display.SizeX || value <= 0)
                {
                    throw new DisplaySizeException("X игрока находится вне дисплея");
                }
                else
                {
                    predX = playerX;
                    Display.DisArr[playerY - 1, playerX - 1] = Display.VoidChar;
                    Display.DisArr[playerY - 1, value - 1] = playerChar;
                    playerX = value;
                }
            }
        }
        public int PlayerY
        {
            get
            {
                return playerY;
            }
            set
            {
                if (value > Display.SizeY || value <= 0)
                {
                    throw new DisplaySizeException("Y игрока находится вне дисплея");
                }
                else
                {
                    predY = playerY;
                    Display.DisArr[playerY - 1, playerX - 1] = Display.VoidChar;
                    Display.DisArr[value - 1, playerX - 1] = playerChar;
                    playerY = value;
                }
            }
        }
        public Player(int Y, int X)
        {
            if (Display.DisArr == null)
            {
                throw new DisplayIsNullException("Нельзя обратиться к игроку без создания дисплея");
            }
            PlayerY = Y;
            PlayerX = X;
            predY = Y;
            predX = X;
            Display.DisArr[playerY - 1, PlayerX - 1] = playerChar;
        }

        public void Up()
        {
            if (PlayerY - 2 >= 0)
            {
                if (Display.DisArr[playerY - 2, PlayerX - 1] != playerChar)
                {
                    PlayerY--;
                    NowDirection = Directions.Up;
                    Display.Refresh();
                }
                else
                {
                    obstacleOnThisCell = true;
                }
            }
            else
            {
                obstacleOnThisCell = true;
            }

        }
        public void Down()
        {
            if (PlayerY + 1 <= Display.SizeY)
            {
                if (Display.DisArr[playerY, PlayerX - 1] != playerChar)
                {
                    PlayerY++;
                    NowDirection = Directions.Down;
                    Display.Refresh();
                }
                else
                {
                    obstacleOnThisCell = true;
                }
            }
            else
            {
                obstacleOnThisCell = true;
            }

        }
        public void Left()
        {
            if (PlayerX - 2 >= 0)
            {
                if (Display.DisArr[playerY - 1, PlayerX - 2] != playerChar)
                {
                    PlayerX--;
                    NowDirection = Directions.Left;
                    Display.Refresh();
                }
                else
                {
                    obstacleOnThisCell = true;
                }
            }
            else
            {
                obstacleOnThisCell = true;
            }

        }
        public void Right()
        {
            if (PlayerX + 1 <= Display.SizeY)
            {
                if (Display.DisArr[playerY - 1, PlayerX] != playerChar)
                {
                    PlayerX++;
                    NowDirection = Directions.Right;
                    Display.Refresh();
                }
                else
                {
                    obstacleOnThisCell = true;
                }
            }
            else
            {
                obstacleOnThisCell = true;
            }

        }
    }

    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    class DisplaySizeException : Exception {
        public DisplaySizeException() : this("Невалидный размер дисплея (Дисплей должен быть минимум 2X2)") { }
        public DisplaySizeException(string message) : base(message) { }
    }
    class DisplayIsCreatedException : Exception {
        public DisplayIsCreatedException() : this("Попытка создать дисплей после его создания"){ }
        public DisplayIsCreatedException(string message) : base(message) { }
    }
    class DisplayIsNullException : Exception {
        public DisplayIsNullException() : this("Массив дисплея является null. Забыли создать?") { }
        public DisplayIsNullException(string message) : base(message) { }
    }
    /*
    class PlayerIsCreatedException : Exception {
        public PlayerIsCreatedException() : this("Попытка создать игрока при его существовании") { }
        public PlayerIsCreatedException(string message) : base(message) { }
    }*/
}