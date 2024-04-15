using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tet
{
    public class PositionOfBlock
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public PositionOfBlock(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
