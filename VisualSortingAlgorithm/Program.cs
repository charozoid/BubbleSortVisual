using SFML.Graphics;
using SFML.System;
using SFML.Window;
class Program 
{
    public const int WINDOW_WIDTH = 800;
    public const int WINDOW_HEIGHT = 600;
    public static RenderWindow window;
    public static Random random = new Random();
    public static Bar[] array = new Bar[40];
    public static bool started = false;
    public static void Main(string[] args)
    {
        VideoMode mode = new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT);
        window = new RenderWindow(mode, "Sorting algorithms");
        window.SetVerticalSyncEnabled(true);
        window.Closed += (sender, args) => window.Close();        
        window.SetFramerateLimit(30);

        CreateRandomIntArray();
        int count = array.Length;
        while (window.IsOpen)
        {
            window.Clear(Color.Black);
            if (started)
            {
                while (count > 0)
                {
                    for (int i = 1; i < count; i++)
                    {
                        if (array[i].value < array[i - 1].value)
                        {
                            Bar tempBar = array[i];
                            array[i] = array[i - 1];
                            array[i].shape.Position += new Vector2f(20, 0);
                            array[i - 1] = tempBar;
                            array[i - 1].shape.Position -= new Vector2f(20, 0);
                            window.DispatchEvents();
                            window.Clear(Color.Black);
                            DrawArrayGraphics();
                            window.Display();
                        }
                    }
                    count--;
                }
            }
            DrawArrayGraphics();

            KeyPressed();
            window.DispatchEvents();
            window.Display();
        }
    }
    public static void KeyPressed()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
        {
            started = true; 
        }
    }
    public static void DrawArrayGraphics()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i].Draw(window);
        }
    }
    public static void CreateRandomIntArray()
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rdm = random.Next(100);
            Bar bar = new Bar(new Vector2f(i * 20, WINDOW_HEIGHT - rdm * 5), rdm, new Color(125, (byte)(rdm*2), 225));
            array[i] = bar;
        }
    }
}
class Bar
{
    public Vector2f pos;
    public RectangleShape shape;
    public int value;
    public Bar (Vector2f pos, int value, Color color)
    {
        this.pos = pos;
        this.value = value;
        shape = new RectangleShape(new Vector2f(20, value * 5));
        shape.Position = pos;
        shape.FillColor = color;
    }
    public void Draw(RenderWindow window)
    {
        window.Draw(shape);
    }
}