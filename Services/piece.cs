using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers.Services
{
    internal class piece
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }
        public string Color { get; set; }
        public bool isking { get; set; }
        public bool isalive { get; set; }

        public piece(int x, int y, string color, bool is_king, bool is_alive)
        {
            X_Position = x;
            Y_Position = y;
            Color = color;
            isking = is_king;
            isalive = is_alive;
        }
    }
}
