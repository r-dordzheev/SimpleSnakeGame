using GameManager;

List<Player> trailList = new List<Player>();

Display.Create(10,10);
Display.VoidChar = '.';
trailList.Add(new(7,5)); // trailList[0] означает голову нашего персонажа
trailList.Add(new(8,5));
trailList.Add(new(9,5));
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
void MoveTrail() {
    for (int i = 1; i < trailList.Count; i++)
    {
        Move(trailList[i], trailList[i - 1].pastDirection);
    }
}
void TimerElapsed(Object source, System.Timers.ElapsedEventArgs e) {
    Move(trailList[0], direction);
    MoveTrail();
    if (Player.gameOver)
    {
        aTimer.Enabled = false;
        isOn = false;
        aTimer.AutoReset = false;
        Console.Clear();
        Console.WriteLine("Game Over! Вы врезались в стену :(");
    }
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