using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tet
{
    public class GameGrid
    {
        private readonly int[,] grid;
        public int Rows { get; }
        public int Columns { get; }

        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }
        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }
        public bool IsInside(int r, int c) // внутри ли сетки объект
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }
        public bool IsEmpty(int r, int c) // пуста ли ячейка
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }
        public bool IsRowFull(int r)  // заполнена ли строка

        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0) // если какой либо столбец в ряду r == 0, то ряд не полный
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsRowEmpty(int r) // пуста ли строка
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0) // если ячейка в строке r и каком либо столбце с не равна 0, то ряд не пустой
                {
                    return false;
                }
            }
            return true;
        }
        private void ClearRow(int r) // очистка строки
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }
        private void MoveDownRow(int r, int numRows) // сдвиг строки вниз на указанное колличество строк
        {
            for (int c = 0; c < Columns; ++c)
            {
                grid[r + numRows, c] = grid[r, c]; // каждой ячейке со смещенной строкой присваивается исходная ячейка
                grid[r, c] = 0; // исходная строка очищается
            };
        }
        public int ClearFullRows() // очистка заполненных строк
        {
            int cleared = 0; // счетчик колличества очищенных строк, для перемещения вниз не полных строк
            for (int r = Rows - 1; r >= 0; r--)
            {
                if (IsRowFull(r)) // если строка полная
                {
                    ClearRow(r); // очищается
                    cleared++; // счетчик инкрементится
                }
                else if (cleared > 0) // если очистилась хотя бы одна строка 
                {
                    MoveDownRow(r, cleared); // строка перемещается вниз на колличество очищенных строк
                }
            }
            return cleared; // возвращает счетчик очищенных строк 
        }
    }
}
