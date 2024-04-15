using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tet
{
    public class SBlock : Block
    {
        private readonly PositionOfBlock[][] tiles = new PositionOfBlock[][]
        {
            new PositionOfBlock[] { new(0, 1), new(0, 2), new(1, 0), new(1, 1) },
            new PositionOfBlock[] { new(0, 1), new(1, 1), new(1, 2), new(2, 2) },
            new PositionOfBlock[] { new(1, 1), new(1, 2), new(2, 0), new(2, 1) },
            new PositionOfBlock[] { new(0, 0), new(1, 0), new(1, 1), new(2, 1) },
        };

        public override int Id => 5;

        protected override PositionOfBlock[][] Tiles => tiles;

        protected override PositionOfBlock StartOffset => new PositionOfBlock(0, 3);
    }
}
