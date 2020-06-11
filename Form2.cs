using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze_of_mouse
{
    public partial class Form2 : Form
    {
        Map map = new Map();

        public Form2()
        {
            InitializeComponent();
        }

        /*點擊開始按鈕*/
        private void button1_Click(object sender, EventArgs e)
        {
            bool enter, next_step = false;
            int start_X = 0, start_Y = 0, end_X = 0, end_Y = 0;

            /*輸入起始x座標*/
            enter = int.TryParse(start_x.Text, out start_X);
            if (!enter)
                MessageBox.Show("請輸入 1 ~ " + map.GetRow() + "的整數!!!");

            /*判斷不超過地圖*/
            else if (start_X < 1 || start_X > map.GetRow())
            {
                MessageBox.Show("請輸入 1 ~ " + map.GetRow() + "的整數!!!");

            }
            else
                next_step = true;

            if (next_step)
            {
                next_step = false;

                /*輸入起始y座標*/
                enter = int.TryParse(start_y.Text, out start_Y);
                if (!enter)
                    MessageBox.Show("請輸入 1 ~ " + map.GetCol() + "的整數!!!");

                /*判斷不超出地圖*/
                else if (start_Y < 1 || start_Y > map.GetCol())
                {
                    MessageBox.Show("請輸入 1 ~ " + map.GetCol() + "的整數!!!");
                }
                else
                    next_step = true;
            }

            /*判斷不是牆壁*/
            if (next_step)
            {
                next_step = false;
                if (map.GetMaze()[start_X, start_Y] == 2)
                    MessageBox.Show("請輸入牆壁以外的位置");
                else
                    next_step = true;
            }

            if (next_step)
            {
                next_step = false;

                /*輸入終點x座標*/
                enter = int.TryParse(end_x.Text, out end_X);
                if (!enter)
                    MessageBox.Show("請輸入 1 ~ " + map.GetRow() + "的整數!!!");

                /*判斷不超出地圖*/
                else if (end_X < 1 || end_X > map.GetRow())
                {
                    MessageBox.Show("請輸入 1 ~ " + map.GetRow() + "的整數!!!");
                }
                else
                    next_step = true;
            }


            if (next_step)
            {
                next_step = false;

                /*輸入終點y座標*/
                enter = int.TryParse(end_y.Text, out end_Y);
                if (!enter)
                    MessageBox.Show("請輸入 1 ~ " + map.GetCol() + "的整數!!!");

                /*判斷不超出地圖*/
                else if (end_Y < 1 || end_Y > map.GetCol())
                {
                    MessageBox.Show("請輸入 1 ~ " + map.GetCol() + "的整數!!!");
                }
                else
                    next_step = true;
            }

            /*判斷不是牆壁*/
            if (next_step)
            {
                next_step = false;
                if (map.GetMaze()[end_X, end_Y] == 2)
                    MessageBox.Show("請輸入牆壁以外的位置");
                else
                    next_step = true;
            }

            /*判斷起始座標跟終點座標不同*/
            if (next_step)
            {
                next_step = false;
                if (start_X == end_X && start_Y == end_Y)
                    MessageBox.Show("請輸入不同的座標!!!");
                else
                    next_step = true;
            }

            if (next_step)
            {
                
                /*找尋路徑*/
                map.FindTheRoad(map.GetMaze(), start_X, start_Y, end_X, end_Y);

                /*隱藏不需要的物件*/
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                button1.Visible = false;
                start_x.Visible = false;
                end_x.Visible = false;
                start_y.Visible = false;
                end_y.Visible = false;

                /*顯示最終結果*/
                label1.Font = new Font("Arial", 25);
                label1.AutoSize = true;
                label1.Visible = true;
                label1.Text = map.PrintMaze();
                this.Width = 600;
                this.Height = 400;

                /*顯示步數*/
                if (map.GetStep() == 0)
                {
                    MessageBox.Show("找不到出口");
                }
                else
                {
                    MessageBox.Show("總共走了" + map.GetStep() + "步");
                }
            }

        }
    }
}
