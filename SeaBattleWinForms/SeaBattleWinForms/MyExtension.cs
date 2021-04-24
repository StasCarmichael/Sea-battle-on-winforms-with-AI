using System;
using System.Collections.Generic;
using System.Text;
using SeaBattleWinForms;
using System.Windows.Forms;
using System.Drawing;


namespace SeaBattleExtention
{
    static class MyExtension
    {
        private static readonly Color FORE_ALIVE_COLOR = Color.Black;
        private static readonly Color BACK_ALIVE_COLOR = Color.Green;

        private static readonly Color FORE_DESTROYED_COLOR = Color.Black;
        private static readonly Color BACK_DESTROYED_COLOR = Color.Red;

        private static readonly Color FORE_SHOOTED_COLOR = Color.White;
        private static readonly Color BACK_SHOOTED_COLOR = Color.Blue;

        private static readonly Font MY_FONT = new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold);



        //Рисуем попадания по нам + наши все корабли включая вибитии
        public static void DrawMyField(this BattleField battleField, BattleField bot, Button[,] WindMyField)
        {
            //Записать дание в матрицу
            int[,] myField = battleField.GetMyField(bot);

            //Нарисовать корабли
            for (int x = 0; x < BattleField.SIZE_X; x++)
            {
                for (int y = 0; y < BattleField.SIZE_Y; y++)
                {
                    if (myField[x, y] == (int)eStatus.destroyed)
                    {
                        WindMyField[x, y].Text = "X";
                        WindMyField[x, y].ForeColor = FORE_DESTROYED_COLOR;
                        WindMyField[x, y].BackColor = BACK_DESTROYED_COLOR;
                        WindMyField[x, y].Enabled = false;
                    }
                    else if (myField[x, y] == (int)eStatus.reserved)
                    {
                        WindMyField[x, y].Text = "#";
                        WindMyField[x, y].ForeColor = FORE_ALIVE_COLOR;
                        WindMyField[x, y].BackColor = BACK_ALIVE_COLOR;
                    }
                    else if (myField[x, y] == (int)eStatus.shooted)
                    {
                        WindMyField[x, y].ForeColor = FORE_SHOOTED_COLOR;
                        WindMyField[x, y].BackColor = BACK_SHOOTED_COLOR;
                        WindMyField[x, y].Enabled = false;
                    }

                    WindMyField[x, y].Font = MY_FONT;
                }
            }
        }

        //Рисуем наши попадания и вибитие кораблики bota
        public static void DrawEnemyField(this BattleField battleField, BattleField bot, Button[,] WindEnemyField)
        {
            //Записать данние в матрицу
            int[,] enemyField = battleField.GetEnemyField(bot);

            //Нарисовать корабли
            for (int x = 0; x < BattleField.SIZE_X; x++)
            {
                for (int y = 0; y < BattleField.SIZE_Y; y++)
                {
                    if (enemyField[x, y] == (int)eStatus.destroyed)
                    {
                        WindEnemyField[x, y].Text = "X";
                        WindEnemyField[x, y].ForeColor = FORE_DESTROYED_COLOR;
                        WindEnemyField[x, y].BackColor = BACK_DESTROYED_COLOR;
                        WindEnemyField[x, y].Enabled = false;
                    }
                    else if (enemyField[x, y] == (int)eStatus.shooted)
                    {
                        WindEnemyField[x, y].ForeColor = FORE_SHOOTED_COLOR;
                        WindEnemyField[x, y].BackColor = BACK_SHOOTED_COLOR;
                        WindEnemyField[x, y].Enabled = false;
                    }
                    WindEnemyField[x, y].Font = MY_FONT;
                }
            }
        }

        //Рисуем только живие корабли
        public static void DrawShips(this BattleField battleField, Button[,] thisField)
        {
            bool[,] myShips = battleField.GetMyShips();


            // Живие коробли
            for (int x = 0; x < BattleField.SIZE_X; x++)
            {
                for (int y = 0; y < BattleField.SIZE_Y; y++)
                {
                    if (myShips[x, y] == true)
                    {
                        thisField[x, y].Text = "#";
                        thisField[x, y].ForeColor = FORE_ALIVE_COLOR;
                        thisField[x, y].BackColor = BACK_ALIVE_COLOR;
                        thisField[x, y].Font = MY_FONT;
                    }
                }
            }
        }
    }
}
