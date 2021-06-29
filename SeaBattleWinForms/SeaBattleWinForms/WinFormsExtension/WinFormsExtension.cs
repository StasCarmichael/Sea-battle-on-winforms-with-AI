using System;
using System.Windows.Forms;
using System.Drawing;
using SeaBattleWinForms.Logic;


namespace SeaBattleWinForms.Extension
{
    static class WinFormsExtension
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
            eStatus[,] myField = battleField.GetMyField(bot);

            //Нарисовать корабли
            for (int x = 0; x < BattleField.SIZE_X; x++)
            {
                for (int y = 0; y < BattleField.SIZE_Y; y++)
                {
                    if (myField[x, y] == eStatus.destroyed)
                    {
                        WindMyField[x, y].Text = "X";
                        WindMyField[x, y].ForeColor = FORE_DESTROYED_COLOR;
                        WindMyField[x, y].BackColor = BACK_DESTROYED_COLOR;
                        WindMyField[x, y].Enabled = false;
                    }
                    else if (myField[x, y] == eStatus.reserved)
                    {
                        WindMyField[x, y].Text = "#";
                        WindMyField[x, y].ForeColor = FORE_ALIVE_COLOR;
                        WindMyField[x, y].BackColor = BACK_ALIVE_COLOR;
                    }
                    else if (myField[x, y] == eStatus.shooted)
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
            eStatus[,] enemyField = battleField.GetEnemyField(bot);

            //Нарисовать корабли
            for (int x = 0; x < BattleField.SIZE_X; x++)
            {
                for (int y = 0; y < BattleField.SIZE_Y; y++)
                {
                    if (enemyField[x, y] == eStatus.destroyed)
                    {
                        WindEnemyField[x, y].Text = "X";
                        WindEnemyField[x, y].ForeColor = FORE_DESTROYED_COLOR;
                        WindEnemyField[x, y].BackColor = BACK_DESTROYED_COLOR;
                        WindEnemyField[x, y].Enabled = false;
                    }
                    else if (enemyField[x, y] == eStatus.shooted)
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



        //Метод разширения на челевеческую ростановку
        public static bool HumanPlacementWinForms(this BattleField battleField, ref Button[,] thisField, Color definingColor)
        {
            bool[,] currField = new bool[thisField.GetLength(0), thisField.GetLength(1)];

            for (int x = 0; x < thisField.GetLength(0); x++)
            {
                for (int y = 0; y < thisField.GetLength(1); y++)
                {
                    if (thisField[x, y].BackColor == definingColor) { currField[x, y] = true; }
                }
            }

            return battleField.HumanPlacement(currField);
        }
    }
}
