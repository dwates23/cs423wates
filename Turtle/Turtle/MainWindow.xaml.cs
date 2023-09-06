using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace TurtleGraphicsApp
{
    public partial class MainWindow : Window
    {
        private Turtle turtle;
        private int gridSize = 50; // Default grid size
        private Canvas drawingCanvas;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTurtle();
        }

        private void InitializeTurtle()
        {
            turtle = new Turtle();
            drawingCanvas = new Canvas();
            canvas.Children.Add(drawingCanvas);
            DrawGrid();
        }

        private void DrawGrid()
        {
            // Draw a grid on the canvas
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = canvas.ActualWidth / gridSize;
                    rect.Height = canvas.ActualHeight / gridSize;

                    Canvas.SetLeft(rect, x * rect.Width);
                    Canvas.SetTop(rect, y * rect.Height);

                    rect.Stroke = Brushes.Gray;
                    rect.Fill = Brushes.White;

                    canvas.Children.Add(rect);
                }
            }
        }

        private void ExecuteCommandButton_Click(object sender, RoutedEventArgs e)
        {
            string command = commandTextBox.Text.Trim();
            ExecuteCommand(command);
            commandTextBox.Clear();
        }


        private void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "1": // Pen Up
                    turtle.PenDown = false;
                    break;

                case "2": // Pen Down
                    turtle.PenDown = true;
                    break;

                case "3": // Turn Right
                    turtle.TurnRight();
                    break;

                case "4": // Turn Left
                    turtle.TurnLeft();
                    break;

                case "6": // Clear the grid
                    InitializeTurtle();
                    break;

                case "9": // Terminate program
                    Application.Current.Shutdown();
                    break;

                default:
                    if (command.StartsWith("5,"))
                    {
                        // Move forward by x spaces
                        int steps;
                        if (int.TryParse(command.Substring(2), out steps))
                        {
                            if (turtle.PenDown)
                            {
                                DrawLine(turtle.PrevX, turtle.PrevY, turtle.X, turtle.Y);
                            }
                            turtle.MoveForward(steps);
                        }
                    }
                    break;
            }
        }

        private void DrawLine(int x1, int y1, int x2, int y2)
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;

            drawingCanvas.Children.Add(line);
        }

        public class Turtle
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public bool PenDown { get; set; }
            public int PrevX { get; private set; }
            public int PrevY { get; private set; }

            private int direction; // 0: Right, 1: Down, 2: Left, 3: Up

            public Turtle()
            {
                X = 0;
                Y = 0;
                PenDown = false;
                direction = 0; // Start facing right
            }

            public void TurnRight()
            {
                direction = (direction + 1) % 4;
            }

            public void TurnLeft()
            {
                direction = (direction - 1 + 4) % 4;
            }

            public void MoveForward(int steps)
            {
                PrevX = X;
                PrevY = Y;

                switch (direction)
                {
                    case 0: // Right
                        X += steps;
                        break;

                    case 1: // Down
                        Y += steps;
                        break;

                    case 2: // Left
                        X -= steps;
                        break;

                    case 3: // Up
                        Y -= steps;
                        break;
                }
            }
        }
    }
}