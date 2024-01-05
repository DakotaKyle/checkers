using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers.Services
{
    internal class board_square
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }
        public string Color { get; set; }

        public board_square(int x, int y, string color)
        {
            X_Position = x;
            Y_Position = y;
            Color = color;
        }
    }
}
