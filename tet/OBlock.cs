using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tet
{
    public class OBlock : Block
    {
        private readonly PositionOfBlock[][] tiles = new PositionOfBlock[][]
        {
            new PositionOfBlock[]{new(0,0),new(0,1),new(1,0),new(1,1)}
        };

        public override int Id => 4;
        protected override PositionOfBlock StartOffset => new PositionOfBlock(0, 4);

        protected override PositionOfBlock[][] Tiles => tiles;
    }
}
