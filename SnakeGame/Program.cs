using GameManager;

List<Player> trailList = new List<Player>();

Display.VoidChar = '.';
Display.Create(10,10);
trailList.Add(new(7,5)); // trailList[0] означает голову персонажа
Apple.Add();
Display.Refresh();

System.Timers.Timer aTimer = new System.Timers.Timer(1000);

Directions direction = Directions.Up;



bool isOn = true;
aTimer.Enabled = true;
aTimer.Elapsed += TimerElapsed;
aTimer.AutoReset = true;
while (isOn == true)
{
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.W:
            direction = Directions.Up;
            break;
        case ConsoleKey.S:
            direction = Directions.Down;
            break;
        case ConsoleKey.A:
            direction = Directions.Left;
            break;
        case ConsoleKey.D:
            direction = Directions.Right;
            break;
        case ConsoleKey.E:
            aTimer.Enabled = false;
            aTimer.AutoReset = false;
            isOn = false;
            break;
        default:
            break;
    }
    
}
void MoveTrail(List<Player> tList) {
    for (int i = 1; i < tList.Count; i++)
    {
        Move(tList[i], tList[i - 1].pastDirection);
    }
}
void TimerElapsed(Object source, System.Timers.ElapsedEventArgs e) {
    Move(trailList[0], direction);
    if (trailList[0].obstacleOnThisCell)
    {
        aTimer.Enabled = false;
        isOn = false;
        aTimer.AutoReset = false;
        Console.Clear();
        Console.WriteLine("Game Over! :(");
        return;
    }
    MoveTrail(trailList);
    Apple.TryCollect(trailList[0]);
    if (Apple.collected) AddTrail(trailList);
    Display.Refresh();
}

void Move(Player player, Directions dir) {
    switch (dir)
    {
        case Directions.Up:
            player.Up();
            break;
        case Directions.Down:
            player.Down();
            break;
        case Directions.Left:
            player.Left();
            break;
        case Directions.Right:
            player.Right();
            break;
    }
}
void AddTrail(List<Player> tList) {
    switch (tList[tList.Count - 1].NowDirection) {
        case Directions.Up:
            tList.Add(new(tList[tList.Count - 1].PlayerY + 1, tList[tList.Count - 1].PlayerX));
            break;
        case Directions.Down:
            tList.Add(new(tList[tList.Count - 1].PlayerY - 1, tList[tList.Count - 1].PlayerX));
            break;
        case Directions.Left:
            tList.Add(new(tList[tList.Count - 1].PlayerY, tList[tList.Count - 1].PlayerX + 1));
            break;
        case Directions.Right:
            tList.Add(new(tList[tList.Count - 1].PlayerY, tList[tList.Count - 1].PlayerX - 1));
            break;
    }
}