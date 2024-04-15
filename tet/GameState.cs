using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tet
{
    public class GameState
    {
        private Block currentBlock;
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);
                    if (BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public GameGrid Gamegrid { get; }
        public BlockQuene Blockquene { get; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public Block HooldBlock { get; private set; }
        public bool CanHold { get; private set; }

        public GameState()
        {
            Gamegrid = new GameGrid(22, 10);
            Blockquene = new BlockQuene();
            CurrentBlock = Blockquene.GetAndUpdate();
            CanHold = true;
        }

        private bool BlockFits()
        {
            foreach (PositionOfBlock p in CurrentBlock.TilePosition())
            {
                if (!Gamegrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }
        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }
            if (HooldBlock == null)
            {
                HooldBlock = CurrentBlock;
                CurrentBlock = Blockquene.GetAndUpdate();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = HooldBlock;
                HooldBlock = tmp;
            }
            CanHold = false;
        }
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }
        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }
        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }
        private bool IsGameOver()
        {
            return !(Gamegrid.IsRowEmpty(0) && Gamegrid.IsRowEmpty(1));
        }
        public void PlaceBlock()
        {
            foreach (PositionOfBlock p in CurrentBlock.TilePosition())
            {
                Gamegrid[p.Row, p.Column] = CurrentBlock.Id;
            }
            Score += Gamegrid.ClearFullRows();
            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = Blockquene.GetAndUpdate();
                CanHold = true;
            }
        }
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
    }
}
