using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            int j = 0; //j represents the y-axis
            int offset_x, offset_y;

            InitializeComponent();
            /*
             * Build the checkerboard
             */
            while (j <= 7)
            {
                int i = 0; //i represents the x-axis
                offset_y = j * 100;

                while (i <= 7)
                {
                    if (j % 2 == 0)
                    {
                        //Red square
                        offset_x = i * 100;
                        add_rectangle(255, 0, 0, i, offset_x, j, offset_y);

                        if (j < 3) //add a red piece
                        {
                            add_circle(255, 150, 0, i, offset_x, j, offset_y);
                        }
                        i++;

                        //Black square
                        offset_x = i * 100;
                        add_rectangle(0, 0, 0, i, offset_x, j, offset_y);

                        if (j >= 5)//add a black piece
                        {
                            add_circle(255, 255, 255, i, offset_x, j, offset_y);
                        }

                        i++;
                    }
                    else
                    {
                        //Black square
                        offset_x = i * 100;
                        add_rectangle(0, 0, 0, i, offset_x, j, offset_y);

                        if (j >= 5)//add a black piece
                        {
                            add_circle(255, 255, 255, i, offset_x, j, offset_y);
                        }

                        i++;

                        //Red square
                        offset_x = i * 100;
                        add_rectangle(255, 0, 0, i, offset_x, j, offset_y);

                        if (j < 3)//add a red piece
                        {
                            add_circle(255, 150, 0, i, offset_x, j, offset_y);
                        }
                        i++;
                    }
                }
                j++;
            }
        }

        private void add_rectangle(byte r, byte b, byte g, int x, int offset_x, int y, int offset_y) //Pass in 3 seperate bytes for color, x-axis, x-offset, y-axis, and y-offset
        {
            Brush sq_color = new SolidColorBrush(Color.FromRgb(r, b, g));

            Rectangle rec = new Rectangle
            {
                Width = 100,
                Height = 100,
                Fill = sq_color,
                StrokeThickness = 2,
                Stroke = Brushes.Black
            };

            Canvas.SetLeft(rec, x + offset_x);
            Canvas.SetTop(rec, y + offset_y);
            checker_board.Children.Add(rec);
        }

        private void add_circle(byte r, byte b, byte g, int x, int offset_x, int y, int offset_y) //Pass in 3 seperate bytes for RBG color, x-axis, x-offset, y-axis, and y-offset
        {
            Brush cc = new SolidColorBrush(Color.FromRgb(r, b, g));

            Ellipse circle = new Ellipse()
            {
                Width = 65,
                Height = 65,
                Fill = cc,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            checker_board.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)x + offset_x + 18);
            circle.SetValue(Canvas.TopProperty, (double)y + offset_y + 18);
        }
    }
}
