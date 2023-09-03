using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab03
{
    public partial class MainWindow : Window
    {
        private int animationCounter = 0;
        private int treeDepth = 0;
        private int depthLimit = 10;  // Define your depth limit

        private void StartAnimation(object sender, EventArgs e)
        {
            animationCounter += 1;
            if (animationCounter % 30 == 0)
            {
                DrawBinaryTree(
                    canvas1,
                    treeDepth,
                    new Point(canvas1.Width / 2, 0.83 * canvas1.Height),
                    0.2 * canvas1.Width,
                    -Math.PI / 2
                );
                string str = "Binary Tree - Depth = " +
                treeDepth.ToString();
                tblabe.Text = str;
                treeDepth += 1;
                if (treeDepth > depthLimit)
                {
                    tblabe.Text = "Binary Tree - Depth = 10. Finished";
                    CompositionTarget.Rendering -= StartAnimation;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += StartAnimation; // Start the animation when the window is created
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            canvas1.Children.Clear();
            tblabe.Text = "";
            animationCounter = 0;
            treeDepth = 1;
            CompositionTarget.Rendering += StartAnimation;
        }


        private double lengthScale = 0.75;
        private double deltaTheta = Math.PI / 5;
        private void DrawBinaryTree(Canvas canvas, int depth, Point pt,
                                        double length, double theta)
        {
            double x1 = pt.X + length * Math.Cos(theta);
            double y1 = pt.Y + length * Math.Sin(theta);
            Line line = new Line();
            line.Stroke = Brushes.Blue;
            line.X1 = pt.X;
            line.Y1 = pt.Y;
            line.X2 = x1;
            line.Y2 = y1;
            canvas.Children.Add(line);
            if (depth < depthLimit)
            {
                DrawBinaryTree(canvas, depth - 1,
                                 new Point(x1, y1),
                                  length * lengthScale, theta + deltaTheta);
                DrawBinaryTree(canvas, depth - 1,
                                  new Point(x1, y1),
                                  length * lengthScale, theta - deltaTheta);
            }
            else
                return;
        }
    }
}
