using GameManager;

Display.Create(10,10);
Player.Create(5, 5);
Display.Refresh();
bool isOn = true;
while (isOn == true)
{
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.W:
            PlayerMovement.Up();
            break;
        case ConsoleKey.S:
            PlayerMovement.Down();
            break;
        case ConsoleKey.A:
            PlayerMovement.Left();
            break;
        case ConsoleKey.D:
            PlayerMovement.Right();
            break;
        case ConsoleKey.E:
            isOn = false;
            break;
        default:
            break;
    }
}