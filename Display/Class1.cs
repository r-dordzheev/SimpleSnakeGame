namespace GameManager
{
    public static class Display
    {
        public static char[,]? DisArr { get;  internal set; }
        static int sizeX = 5;
        static int sizeY = 5;
        static char voidChar = '#';

        /// <summary>
        /// Represent a symbol that use to show void in a display
        /// </summary>
        public static char VoidChar {
            get {
                return voidChar;
            }
            set {
                if (DisArr == null)
                {
                    voidChar = value;
                }
                else
                {
                    for (int i = 0; i < SizeY; i++)
                    {
                        for (int j = 0; j < SizeX; j++)
                        {
                            if (DisArr[i, j] == voidChar)
                            {
                                DisArr[i, j] = value;
                            }
                        }
                    }
                    voidChar = value;
                }
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

        /// <summary>
        /// Creates display and fill it with void chars
        /// </summary>
        /// <exception cref="DisplayIsCreatedException"></exception>
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
        /// <summary>
        /// Creates display and fill it with void chars
        /// </summary>
        /// <param name="X">Size of display in X</param>
        /// <param name="Y">Size of display in Y</param>
        /// <exception cref="DisplayIsCreatedException"></exception>
        public static void Create(int X, int Y)  {
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

        /// <summary>
        /// Clears console and write new DisArr instead
        /// </summary>
        /// <exception cref="DisplayIsNullException"></exception>
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
        /// <summary>
        /// Represent the player char that will be displayed
        /// </summary>
        public static char playerChar { get; private set; } = '@';
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
        
        /// <summary>
        /// Represent the player X in DisArr
        /// </summary>
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
                    throw new DisplaySizeException("Player's X is outside of Display");
                }
                else
                {
                    Display.DisArr[playerY - 1, playerX - 1] = Display.VoidChar;
                    Display.DisArr[playerY - 1, value - 1] = playerChar;
                    playerX = value;
                }
            }
        }
        /// <summary>
        /// Represent the Player Y in DisArr
        /// </summary>
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
                    throw new DisplaySizeException("Player's Y is outside of Display");
                }
                else
                {
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
                throw new DisplayIsNullException("You cant create player object without creating a display");
            }
            PlayerY = Y;
            PlayerX = X;
            Display.DisArr[playerY - 1, PlayerX - 1] = playerChar;
        }
        /// <summary>
        /// Moves player by 1 in up direction
        /// </summary>
        public void Up()
        {
            if (PlayerY - 2 >= 0)
            {
                if (Display.DisArr[playerY - 2, PlayerX - 1] != playerChar)
                {
                    PlayerY--;
                    NowDirection = Directions.Up;
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
        /// <summary>
        /// Moves player by 1 in down direction
        /// </summary>
        public void Down()
        {
            if (PlayerY + 1 <= Display.SizeY)
            {
                if (Display.DisArr[playerY, PlayerX - 1] != playerChar)
                {
                    PlayerY++;
                    NowDirection = Directions.Down;
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
        /// <summary>
        /// Moves player by 1 in left direction
        /// </summary>
        public void Left()
        {
            if (PlayerX - 2 >= 0)
            {
                if (Display.DisArr[playerY - 1, PlayerX - 2] != playerChar)
                {
                    PlayerX--;
                    NowDirection = Directions.Left;
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
        /// <summary>
        /// Moves player by 1 in right direction
        /// </summary>
        public void Right()
        {
            if (PlayerX + 1 <= Display.SizeY)
            {
                if (Display.DisArr[playerY - 1, PlayerX] != playerChar)
                {
                    PlayerX++;
                    NowDirection = Directions.Right;
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
    public static class Apple
    {
        public static List<int[]> appleList = new();
        public static int Points { get; private set; }
        public static char AppleChar { get; private set; } = '$';
        public static bool collected = false;

        static Apple()
        {
            if (Display.DisArr == null)
            {
                throw new DisplayIsNullException();
            }
            Points = 0;
        }
        /// <summary>
        /// Creates apple in DisArr
        /// </summary>
        /// <exception cref="DisplayIsFullException"></exception>
        public static void Add()
        {
            List<int[]> availableNums = new();
            Random rnd = new();
            for (int i = 0; i < Display.SizeY; i++)
            {
                for (int j = 0; j < Display.SizeX; j++)
                {
                    if (Display.DisArr[i, j] == Display.VoidChar)
                    {
                        availableNums.Add(new int[2] { i, j });
                    }
                }
            }
            if (availableNums.Count == 0) throw new DisplayIsFullException();
            int t = rnd.Next(1,availableNums.Count);
            Display.DisArr[availableNums[t - 1][0], availableNums[t - 1][1]] = AppleChar;
            appleList.Add(new int[2] { availableNums[t-1][0], availableNums[t-1][1]});
        }
        /*
        public static void Collect(int index, bool addPoints = true)
        {
            if (appleList.Count == 0)
            {
                throw new AppleException("appleList array is empty");
            }
            collected = false;
            if (index < 0 || index >= appleList.Count) {
                throw new AppleException($"appleList array doesnt have element with index {index}");
            }
            Display.DisArr[appleList[index][0], appleList[index][1]] = Display.VoidChar;
            appleList.RemoveAt(index);
            if (addPoints)
            {
                Points++;
                Add();
            }
            collected = true;
        }
        public static void Collect(int Y, int X, bool addPoints = true) {
            if (appleList.Count == 0)
            {
                throw new AppleException("appleList array is empty");
            }
            bool thereIs = false;
            collected = false;
            int removeIndex = 0;
            for (int i = 0; i < appleList.Count; i++)
            {
                if (appleList[i][0] == Y && appleList[i][1] == X) {
                    thereIs = true;
                    removeIndex = i;
                    break;
                }
            }
            if (thereIs == false)
            {
                throw new AppleException($"appleList array doesnt have element with Y {Y} X {X}");
            }
            Display.DisArr[appleList[removeIndex][0], appleList[removeIndex][1]] = Display.VoidChar;
            appleList.RemoveAt(removeIndex);
            if (addPoints)
            {
                Points++;
                Add();
            }
            collected = true;
        }*/
        /// <summary>
        /// Checks if player is on apple position
        /// </summary>
        /// <param name="player">Player position to check</param>
        /// <param name="addPoints">True if you need to score points</param>
        /// <exception cref="AppleException"></exception>
        public static void TryCollect(Player player, bool addPoints = true) {
            if (appleList.Count == 0)
            {
                throw new AppleException("appleList array is empty");
            }
            bool thereIs = false;
            collected = false;
            int removeIndex = 0;
            for (int i = 0; i < appleList.Count; i++)
            {
                if (appleList[i][0] == player.PlayerY - 1 && appleList[i][1] == player.PlayerX - 1)
                {
                    thereIs = true;
                    removeIndex = i;
                    break;
                }
            }
            if (thereIs == false)
            {
                return;
            }
            appleList.RemoveAt(removeIndex);
            if (addPoints)
            {
                Points++;
                Add();
            }
            collected = true;

        }
        public class AppleException : Exception {
            public AppleException() : this("Exception in apple class") { }
            public AppleException(string message) : base(message) { }
        }
    }
    /// <summary>
    /// Represent directions
    /// </summary>
        public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    class DisplayIsFullException : Exception {
        public DisplayIsFullException() : this("Display dont have enough free spaces to add new element") { }
        public DisplayIsFullException(string message) : base(message) { }
    }
    class DisplaySizeException : Exception {
        public DisplaySizeException() : this("Invalid display size (Display must be at least 2X2)") { }
        public DisplaySizeException(string message) : base(message) { }
    }
    class DisplayIsCreatedException : Exception {
        public DisplayIsCreatedException() : this("You can create display only once"){ }
        public DisplayIsCreatedException(string message) : base(message) { }
    }
    class DisplayIsNullException : Exception {
        public DisplayIsNullException() : this("Display's array is null") { }
        public DisplayIsNullException(string message) : base(message) { }
    }
}