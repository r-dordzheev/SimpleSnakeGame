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
    public static class Player {
        internal static bool playerIsCreated { get; private set; } = false;
        static int playerX = 2;
        static int playerY = 2;
        private static char playerChar = '@';

        public static char PlayerChar {
            get {
                return playerChar;
            }
            set {
                Display.DisArr[playerY - 1, playerX - 1] = value;
                playerChar = value;
            }
        }


        public static int PlayerX {
            get {
                return playerX;
            }
            set {
                if (value > Display.SizeX || value <= 0)
                {
                    throw new DisplaySizeException("X игрока находится вне дисплея");
                }
                else if (playerIsCreated) {
                    Display.DisArr[playerY - 1, playerX - 1] = Display.VoidChar;
                    Display.DisArr[playerY - 1, value - 1] = playerChar;
                    playerX = value;
                }
                else
                {
                    playerX = value;
                }
            }
        }
        public static int PlayerY {
            get {
                return playerY;
            }
            set {
                if (value > Display.SizeY || value <= 0)
                {
                    throw new DisplaySizeException("Y игрока находится вне дисплея");
                }
                else if (playerIsCreated) {
                    Display.DisArr[playerY - 1, playerX - 1] = Display.VoidChar;
                    Display.DisArr[value - 1, playerX - 1] = playerChar;
                    playerY = value;
                }
                else
                {
                    playerY = value;
                }
            }
        }

        public static void Create() {
            if (playerIsCreated) {
                throw new PlayerIsCreatedException();
            }
            playerIsCreated = true;
            Display.DisArr[playerY - 1, playerX - 1] = playerChar;
        }
        public static void Create(int X, int Y) {
            if (playerIsCreated) {
                throw new PlayerIsCreatedException();
            }
            PlayerY = Y;
            PlayerX = X;
            playerIsCreated = true;
            Display.DisArr[playerX - 1, playerY - 1] = playerChar;
        }
        static Player() {
            if (Display.DisArr == null) {
                throw new DisplayIsNullException("Нельзя обратиться к игроку без создания дисплея");
            }
        }
    }
    public static class PlayerMovement {
        public static bool gameOver = false;
        public static void Up()
        {
            if (Player.PlayerY - 2 >= 0)
            {
                Player.PlayerY--;
                Display.Refresh();
            }
            else
            {
                gameOver = true;
                Display.Refresh();
            }

        }
        public static void Down()
        {
            if (Player.PlayerY + 1 <= Display.SizeY)
            {
                Player.PlayerY++;
                Display.Refresh();
            }
            else
            {
                gameOver = true;
                Display.Refresh();
            }

        }
        public static void Left()
        {
            if (Player.PlayerX - 2 >= 0)
            {
                Player.PlayerX--;
                Display.Refresh();
            }
            else
            {
                gameOver = true;
                Display.Refresh();
            }

        }
        public static void Right()
        {
            if (Player.PlayerX + 1 <= Display.SizeY)
            {
                Player.PlayerX++;
                Display.Refresh();
            }
            else
            {
                gameOver = true;
                Display.Refresh();
            }

        }
        static PlayerMovement()
        {
            if (!Player.playerIsCreated) {
                throw new PlayerIsCreatedException("Для обращения к классу PlayerMovement, необходимо создать Player");
            }
        }
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
    class PlayerIsCreatedException : Exception {
        public PlayerIsCreatedException() : this("Попытка создать игрока при его существовании") { }
        public PlayerIsCreatedException(string message) : base(message) { }
    }
}