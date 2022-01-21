using GameManager;

Display.Create(10,10);
Display.VoidChar = '.';
Player mainPlayer = new(5,5);
Display.Refresh();

System.Timers.Timer aTimer = new System.Timers.Timer(1000);

Directions Direction = Directions.Up;

bool isOn = true;
aTimer.Enabled = true;
aTimer.Elapsed += Move;
aTimer.AutoReset = true;
while (isOn == true)
{
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.W:
            Direction = Directions.Up;
            break;
        case ConsoleKey.S:
            Direction = Directions.Down;
            break;
        case ConsoleKey.A:
            Direction = Directions.Left;
            break;
        case ConsoleKey.D:
            Direction = Directions.Right;
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
void Move(Object source, System.Timers.ElapsedEventArgs e) {
    switch (Direction) {
        case Directions.Up:
            mainPlayer.Up();
            break;
        case Directions.Down:
            mainPlayer.Down();
            break;
        case Directions.Left:
            mainPlayer.Left();
            break;
        case Directions.Right:
            mainPlayer.Right();
            break;
    }
    if (Player.gameOver)
    {
        aTimer.Enabled = false;
        isOn = false;
        aTimer.AutoReset = false;
        Console.Clear();
        Console.WriteLine("Game Over! Вы врезались в стену :(");
    }
}

enum Directions {
    Up,
    Down,
    Left,
    Right
}