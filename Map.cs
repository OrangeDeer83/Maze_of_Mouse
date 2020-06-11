using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Maze_of_mouse
{
    class Map
    {
        private Point NO_MATCH_POINT = new Point(-1, -1);
        private const int XSTART = 17;
        private const int YSTART = 25;
        private const int HALF_BLOCK = 7;
        private const int X_DISTANCE = 30;
        private const int Y_DISTANCE = 39;
        private static int Row;
        private static int Col;
        private static int[,] maze;

        public Map() {}

        public Map(int row, int col) {
            generateMap(row, col);
            Row = row;
            Col = col;
        }

        public int GetRow()
        {
            return Row;
        }
        
        public int GetCol()
        {
            return Col;
        }

        /*判斷是否需要變換游標圖示*/
        public bool CanBeChange(int x, int y)
        {
            Point pos = new Point(x, y);
            pos = findTheCloestBlock(pos);

            if (pos == NO_MATCH_POINT)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*判斷最接近的方格*/
        private Point findTheCloestBlock(Point pos)
        {
            int remainder, quotient;

            /*判斷是否超出地圖*/
            if (pos.X < XSTART || pos.X > XSTART + (Col - 1) * Y_DISTANCE + HALF_BLOCK)
            {
                pos.X = -1;
            }
            else
            {
                pos.X -= XSTART;
                remainder = pos.X % X_DISTANCE;
                quotient = pos.X / X_DISTANCE;

                /*接近左方格*/
                if (remainder <= HALF_BLOCK)
                {
                    pos.X = quotient;
                }

                /*接近右方格*/
                else if (remainder >= X_DISTANCE - HALF_BLOCK)
                {
                    pos.X = quotient + 1;
                }

                /*在兩方格之間*/
                else
                {
                    pos.X = -1;
                }
            }
            
            /*判斷是否超出地圖*/
            if (pos.Y < YSTART || pos.Y > YSTART + (Row - 1) * Y_DISTANCE + HALF_BLOCK)
            {
                pos.Y = -1;
            }
            else
            {
                pos.Y -= YSTART;
                remainder = pos.Y % Y_DISTANCE;
                quotient = pos.Y / Y_DISTANCE;

                /*接近左方格*/
                if (remainder <= HALF_BLOCK)
                {
                    pos.Y = quotient;
                }

                /*接近右方格*/
                else if (remainder >= Y_DISTANCE - HALF_BLOCK)
                {
                    pos.Y = quotient + 1;
                }

                /*兩方格之間*/
                else
                {
                    pos.Y = -1;
                }

            }

            /*沒有在方格之內*/
            if (pos.X == -1 || pos.Y == -1)
            {
                return NO_MATCH_POINT;
            }

            /*回傳方格座標*/
            else
            {
                return pos;
            }
        }

        /*轉換座摽成陣列*/
        public Point ExchangeToArray(Point pos){
            pos = findTheCloestBlock(pos);
            return new Point(pos.Y + 1, pos.X + 1);
        }
        
        /*生成地圖*/
        private void generateMap(int row, int column)
        {
            maze = new int[row + 2, column + 2];
            for (int i = 0; i < row + 2; i++)
            {
                for (int j = 0; j < column + 2; j++)
                {

                    /*生成牆壁*/
                    if (i == 0 || i == row + 1 || j == 0 || j == column + 1)
                    {
                        maze[i, j] = 2;
                    }

                    /*生成路徑*/
                    else
                    {
                        maze[i, j] = 0;
                    }
                }
            }
        }

        public int[,] GetMaze()
        {
            return maze;
        }

        /*尋找路徑*/
        public int FindTheRoad(int[,] maze, int start_x, int start_y, int end_x, int end_y)
        {
            if (!Convert.ToBoolean(maze[start_x, start_y]))
            {
                maze[start_x, start_y] = 1;
                if (!Convert.ToBoolean(maze[end_x, end_y]) && !(Convert.ToBoolean(FindTheRoad(maze, start_x + 1, start_y + 1, end_x, end_y)) || Convert.ToBoolean(FindTheRoad(maze, start_x + 1, start_y, end_x, end_y)) || Convert.ToBoolean(FindTheRoad(maze, start_x, start_y + 1, end_x, end_y)) || Convert.ToBoolean(FindTheRoad(maze, start_x + 1, start_y - 1, end_x, end_y)) || Convert.ToBoolean(FindTheRoad(maze, start_x - 1, start_y + 1, end_x, end_y)) || Convert.ToBoolean(FindTheRoad(maze, start_x - 1, start_y, end_x, end_y)) || Convert.ToBoolean(FindTheRoad(maze, start_x, start_y - 1, end_x, end_y)) || Convert.ToBoolean(FindTheRoad(maze, start_x, start_y - 1, end_x - 1, end_y))))
                {
                    maze[start_x, start_y] = 0;
                }
            }

            return maze[end_x, end_y];
        }

        /*輸出地圖*/
        public String PrintMaze()
        {
            String map_now = "";

            for (int i = 1; i < Row + 1; i++)
            {
                for (int j = 1; j < Col + 1; j++)
                {
                    switch (maze[i, j])
                    {

                        /*沒走過的路以□表示*/
                        case 0:
                            map_now += "□ ";
                            break;

                        /*走過的路以🐭表示*/
                        case 1:
                            map_now += "🐭";
                            break;
                            /*牆壁以■表示*/
                        case 2:
                            map_now += "■ ";
                            break;
                        }
                    }
                    map_now += "\n";
                }

                return map_now;
            }

            /*計算步數*/
        public int GetStep()
        {
            int step = 0;

            for (int i = 1; i < Row + 1; i++)
            {
                for (int j = 1; j < Col + 1; j++)
                {
                    if (maze[i, j] == 1)
                    {
                        step++;
                    }
                }
            }

            return step;
        }
    }
}


