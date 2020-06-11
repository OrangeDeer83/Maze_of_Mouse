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
    public partial class Form1 : Form
    {
        Form2 form2 = new Form2();
        int row, column;
        bool enter;
        Map map;

        public Form1()
        {
            InitializeComponent();
            this.Text = "走迷宮";
            label6.Visible = false;
        }

        /*當游標移動到格子上就變換游標圖示*/
        private void label6_MouseMove(object sender, MouseEventArgs e)
        {
            /*若是超出地圖游標不變*/
            if (e.X > (2 * column - 1) * 15 + 10 || e.Y > row * 15 + (row - 1) * 39 + 18)
            {
                this.Cursor = Cursors.Default;
            }

            /*若是在方格上游標變為手*/
            if (map.CanBeChange(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }

            /*若不在方格上則不變*/
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        /*點擊方格變換牆壁或是路*/
        private void label6_MouseClick(object sender, MouseEventArgs e)
        {
            Point pos = new Point(e.X, e.Y);

            /*座標轉換成陣列*/
            pos = map.ExchangeToArray(pos);

            /*判斷是否超出地圖*/
            if (e.X < (2 * column - 1) * 15 + 10 && e.Y < row * 15 + (row - 1) * 39 + 18)
            {
                if (map.CanBeChange(e.X, e.Y))
                {

                    /*若是為牆壁點擊後改為路*/
                    if (map.GetMaze()[pos.X, pos.Y] == 2)
                    {
                        map.GetMaze()[pos.X, pos.Y] = 0;
                    }

                    /*若是為路點擊後則改為牆壁*/
                    else
                    {
                        map.GetMaze()[pos.X, pos.Y] = 2;
                    }

                    /*輸出地圖*/
                    label6.Text = map.PrintMaze();
                }
            }
        }

        /*點擊開始按鈕*/
        private void Start_Click(object sender, EventArgs e)
        {
            bool row_error = true, column_error = true;

            /*輸入行數且不小於100*/
            enter = int.TryParse(Column_Enter.Text, out column);
            if (!enter)
                MessageBox.Show("請輸入大於100的整數!!!");
            else if (column < 100)
                MessageBox.Show("請輸入大於100的整數!!!");
            else
                column_error = false;

            /*輸入列數且不小於100*/
            enter = int.TryParse(Row_Enter.Text, out row);
            if (!column_error)
            {
                if (!enter)
                    MessageBox.Show("請輸入大於100的整數!!!");
                else if (row < 100)
                    MessageBox.Show("請輸入大於100的整數!!!");
                else
                    row_error = false;
            }

            /*成功輸入*/
            if (!row_error && !column_error)
            {
                /*隱藏不需要的物件*/
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                Column_Enter.Visible = false;
                Row_Enter.Visible = false;
                Start.Visible = false;
                
                /*生成地圖*/
                map = new Map(row, column);

                /*設定label6*/
                label6.Font = new Font("Arial", 25);
                label6.AutoSize = true;
                label6.Text = map.PrintMaze();
                label6.Visible = true;

                /*顯示form2*/
                form2.Show();
            }
        }
    }
}
