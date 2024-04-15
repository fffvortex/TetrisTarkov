using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tet
{
    public abstract class Block
    {
        protected abstract PositionOfBlock[][] Tiles { get; }
        protected abstract PositionOfBlock StartOffset { get; } // стартовая позиция блока в сетке
        public abstract int Id { get; } // id для различия разных блоков

        private int rotationState; // состояние положения при вращении блока
        private PositionOfBlock offset; // текущее состояние положения блока

        protected Block()
        {
            offset = new PositionOfBlock(StartOffset.Row, StartOffset.Column); // установка смещения
                                                                               // равного изначальному смещению
        }
        public IEnumerable<PositionOfBlock> TilePosition() // возвращает позиции сетки занятые блоком,
                                                           // учитывая текущее положение и смещение
        {
            foreach (PositionOfBlock position in Tiles[rotationState]) // перебирает позиции блока
                                                                       // в текущем состоянии вращения,
                                                                       // и добавляет смещение строки и столбца
            {
                yield return new PositionOfBlock(position.Row + offset.Row, position.Column + offset.Column);
            }
        }
        public void RotateCW() // метод поворота блока по часовой стрелке
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }
        public void RotateCCW() // метод поворота блока против часовой стрелки
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }
        public void Move(int rows, int columns) // метод перемещения блоков в сетке
        {
            offset.Row += rows;
            offset.Column += columns;
        }
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    } 
}
