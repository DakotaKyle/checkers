using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers.Services
{
    internal class board_builder
    {
        public static BindingList<board_square> board_Squares = new BindingList<board_square>();
        public static BindingList<piece> pieces = new BindingList<piece>();

        public static void add_square(board_square sq)
        {
            board_Squares.Add(sq);
        }

        public static void add_piece(piece piece)
        {
            pieces.Add(piece);
        }

        public static void remove_piece(piece piece)
        {
            pieces.Remove(piece);
        }
    }
}
