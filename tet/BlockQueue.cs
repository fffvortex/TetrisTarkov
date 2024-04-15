using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tet
{
    // класс очередь блоков
    public class BlockQuene
    {
        private readonly Block[] blocks = new Block[]
        {
            new OBlock(),
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };
        public BlockQuene()
        {
            NextBlock = RandomBlock();
        }
        private readonly Random random = new Random();

        public Block NextBlock { get; private set; }
        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }
        public Block GetAndUpdate() 
        {
            Block block = NextBlock;
            do
            {
                NextBlock = RandomBlock();
            }
            while (block.Id == NextBlock.Id);
            return block;
        }
    }
}
