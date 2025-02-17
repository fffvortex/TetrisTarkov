﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tet
{
    public class IBlock : Block
    {
        private readonly PositionOfBlock[][] tiles = new PositionOfBlock[][] // прописаны координаты
                                                                                  // для каждой позиции
                                                                                  // смещенного блока
        {
            new PositionOfBlock[] { new(1, 0), new(1, 1), new(1, 2), new(1, 3) },
            new PositionOfBlock[] { new(0, 2), new(1, 2), new(2, 2), new(3, 2) },
            new PositionOfBlock[] { new(2, 0), new(2, 1), new(2, 2), new(2, 3) },
            new PositionOfBlock[] { new(0, 1), new(1, 1), new(2, 1), new(3, 1) },
        };
        public override int Id => 1;
        protected override PositionOfBlock StartOffset => new PositionOfBlock(-1, 3);
        protected override PositionOfBlock[][] Tiles => tiles;
    }
}
