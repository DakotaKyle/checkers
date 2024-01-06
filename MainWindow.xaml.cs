using checkers.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Numerics;
using System.CodeDom;
using System.Configuration;
using System.Reflection;
using System.ComponentModel;

namespace checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool dragging = false;
        int pieceCount = 0;
        piece selectedPiece;
        object objectType;

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
                        //Draw Red square
                        offset_x = i * 100;
                        add_rectangle(255, 0, 0, i, offset_x, j, offset_y);

                        if (j < 3) //add a Gold piece
                        {
                            //Draw Gold piece
                            add_circle(255, 150, 0, i, offset_x, j, offset_y);
                            pieceCount++;

                            //Add piece to Piece Binding List
                            piece piece = new(pieceCount, i, j, "Gold" , false, true);
                            board_builder.add_piece(piece);

                            //Add square to Board Binding List
                            board_square _Square = new(i, j, "red", true);
                            board_builder.add_square(_Square);
                        }
                        else
                        {
                            //Add square to Board Binding List
                            board_square _Square = new(i, j, "red", false);
                            board_builder.add_square(_Square);
                        }
                        i++;

                        //Draw Black square
                        offset_x = i * 100;
                        add_rectangle(0, 0, 0, i, offset_x, j, offset_y);

                        if (j >= 5)
                        {
                            //Draw White piece
                            add_circle(255, 255, 255, i, offset_x, j, offset_y);
                            pieceCount++;

                            //Add piece to Piece Binding List
                            piece piece = new(pieceCount, i, j, "White", false, true);
                            board_builder.add_piece(piece);

                            //Add square to Board Binding List
                            board_square _Square1 = new(i, j, "black", true);
                            board_builder.add_square(_Square1);
                        }
                        else
                        {
                            //Add square to Board Binding List
                            board_square _Square1 = new(i, j, "black", false);
                            board_builder.add_square(_Square1);
                        }

                        i++;
                    }
                    else
                    {
                        //Black square
                        offset_x = i * 100;
                        add_rectangle(0, 0, 0, i, offset_x, j, offset_y);

                        if (j >= 5)
                        {
                            //Draw a White piece
                            add_circle(255, 255, 255, i, offset_x, j, offset_y);
                            pieceCount++;

                            //Add piece to Piece Bindling List
                            piece piece = new(pieceCount, i, j, "White", false, true);
                            board_builder.add_piece(piece);

                            //Add square to Board Binding List
                            board_square _Square2 = new(i, j, "black", true);
                            board_builder.add_square(_Square2);
                        }
                        else
                        {
                            //Add square to Board Binding List
                            board_square _Square2 = new(i, j, "black", false);
                            board_builder.add_square(_Square2);
                        }

                        i++;

                        //Red square
                        offset_x = i * 100;
                        add_rectangle(255, 0, 0, i, offset_x, j, offset_y);

                        if (j < 3)
                        {
                            //Draw a Gold piece
                            add_circle(255, 150, 0, i, offset_x, j, offset_y);
                            pieceCount++;

                            //Add piece to Piece Binding List
                            piece piece = new(pieceCount, i, j, "Gold", false, true);
                            board_builder.add_piece(piece);

                            //Add square to Binding List
                            board_square _Square3 = new(i, j, "red", true);
                            board_builder.add_square(_Square3);
                        }
                        else
                        {
                            //Add square to Binding List
                            board_square _Square3 = new(i, j, "red", false);
                            board_builder.add_square(_Square3);
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

        private void checker_board_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            objectType = e.Source;
            var source = objectType.ToString();
            double x_offset, y_offset;
            Point pointer;

            if (source.Equals("System.Windows.Shapes.Ellipse"))
            {
                pointer.X = Mouse.GetPosition(this).X;
                pointer.Y = Mouse.GetPosition(this).Y;

                x_offset = ((int)pointer.X / 100) - 2;
                y_offset = (int)pointer.Y / 100;

                foreach (piece piece in board_builder.pieces)
                {
                    int x, y;

                    x = piece.X_Position;
                    y = piece.Y_Position;

                    if ((x == x_offset) && (y == y_offset))
                    {
                        selectedPiece = piece;
                        dragging = true;
                        break;
                    }
                }
            }
            else if (source.Equals("System.Windows.Shapes.Rectangle"))
            {
                pointer.X = Mouse.GetPosition(this).X;
                pointer.Y = Mouse.GetPosition(this).Y;

                x_offset = ((int)pointer.X / 100) - 2;
                y_offset = (int)pointer.Y / 100;

                MessageBox.Show("X: " + x_offset + "Y: " + y_offset);
            }
        }

        private void checker_board_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dragging)
            {
                dragging = false;
                var anotherType = e.Source;
                var source = anotherType.ToString();
                int x_offset, y_offset;
                Point pointer;

                if (source.Equals("System.Windows.Shapes.Rectangle"))
                {
                    pointer.X = Mouse.GetPosition(this).X;
                    pointer.Y = Mouse.GetPosition(this).Y;

                    x_offset = ((int)pointer.X / 100) - 2;
                    y_offset = (int)pointer.Y / 100;

                    int id = selectedPiece.id;
                    int x = x_offset;
                    int y = y_offset;
                    string color = selectedPiece.Color;
                    bool isking = selectedPiece.isking;
                    bool isalive = selectedPiece.isalive;

                    byte r, b, g;
                    r = 255;
                    b = 255;
                    g = 255;

                    board_builder.remove_piece(selectedPiece);

                    Ellipse oldpoint = (Ellipse)objectType;

                    checker_board.Children.Remove(oldpoint);

                    piece newPiece = new(id, x, y, color, isking, isalive);
                    board_builder.add_piece(newPiece);

                    if (color.Equals("Gold"))
                    {
                        r = 255;
                        b = 150;
                        g = 0;
                    }

                    add_circle(r, b, g, x_offset, (x_offset * 100), y_offset, (y_offset * 100));
                }
                else
                {
                    MessageBox.Show("That space is occupied!");
                }
            }
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
