using checkers.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System;

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
            InitializeComponent();
            /*
             * Build the checkerboard
             */
            for (int j = 0; j <= 7; j++)
            {
                int offset_y = j * 100;
                for (int i = 0; i <= 7; i++)
                {
                    int offset_x = i * 100;
                    bool isEvenRow = j % 2 == 0;
                    bool isEvenColumn = i % 2 == 0;

                    // Determine the color of the square
                    bool isBlackSquare = isEvenRow ? isEvenColumn : !isEvenColumn;
                    string squareColor = isBlackSquare ? "black" : "red";
                    add_rectangle(isBlackSquare ? (byte)0 : (byte)255, 0, 0, i, offset_x, j, offset_y);

                    // Determine the color of the piece
                    string pieceColor = null;
                    if (isBlackSquare && j < 3) pieceColor = "Gold";
                    else if (isBlackSquare && j >= 5) pieceColor = "White";

                    if (pieceColor != null)
                    {
                        // Draw the piece
                        byte r = pieceColor == "Gold" ? (byte)255 : (byte)255;
                        byte g = pieceColor == "Gold" ? (byte)150 : (byte)255;
                        byte b = pieceColor == "Gold" ? (byte)0 : (byte)255;
                        add_circle(r, g, b, i, offset_x, j, offset_y);
                        pieceCount++;

                        // Add the piece to the Piece Binding List
                        piece piece = new(pieceCount, i, j, pieceColor, false, true);
                        board_builder.add_piece(piece);
                    }

                    // Add the square to the Board Binding List
                    board_square _Square = new(i, j, squareColor, pieceColor != null);
                    board_builder.add_square(_Square);
                }
            }
        }

        private void add_rectangle(byte r, byte g, byte b, int x, int offset_x, int y, int offset_y) //Pass in 3 seperate bytes for color, x-axis, x-offset, y-axis, and y-offset
        {
            Brush sq_color = new SolidColorBrush(Color.FromRgb(r, g, b));

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
                pointer.X = Mouse.GetPosition(this).X - checker_board.Margin.Left;
                pointer.Y = Mouse.GetPosition(this).Y - checker_board.Margin.Top;

                x_offset = (int)Math.Floor(pointer.X / 100.0 - 2);
                y_offset = (int)Math.Floor(pointer.Y / 100.0);


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
            else if (source.Equals("System.Windows.Shapes.Rectangle")) // Current logic is used for testing purposes.
            {
                pointer.X = Mouse.GetPosition(this).X - checker_board.Margin.Left;
                pointer.Y = Mouse.GetPosition(this).Y - checker_board.Margin.Top;

                x_offset = (int)Math.Floor(pointer.X / 100.0 - 1.9);
                y_offset = (int)Math.Floor(pointer.Y / 100.0 - 0.25);

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
                    pointer.X = Mouse.GetPosition(this).X - checker_board.Margin.Left;
                    pointer.Y = Mouse.GetPosition(this).Y - checker_board.Margin.Top;

                    x_offset = (int)Math.Floor(pointer.X / 100.0 - 2);
                    y_offset = (int)Math.Floor(pointer.Y / 100.0 - 0.25);


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
