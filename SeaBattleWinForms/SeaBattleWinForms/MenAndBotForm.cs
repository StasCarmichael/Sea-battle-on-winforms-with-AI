using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattleWinForms
{
    public partial class MenAndBotForm : Form
    {
        const int SIZE = 10;
        const int squareSize = 38;

        const int FREEZETIME = 10;


        bool start = false;


        public MenAndBotForm()
        {
            InitializeComponent();
        }



        delegate void CustomDelegate(BattleField bot, Button[,] WindEnemyField);



        Button[,] myField;
        Button[,] enemyField;

        void CreateField(ref Button[,] thisField, int startX, int startY)
        {
            thisField = new Button[SIZE, SIZE];

            //Верхняя линия
            for (int i = 0; i < SIZE + 1; i++)
            {
                string str = "АБВГДЕЄЖЗИ";

                Button button = new Button();
                if (i != 0)
                {
                    button.Text = Convert.ToString(str[i - 1]);
                    button.Font = new Font(button.Font.FontFamily, 12, FontStyle.Bold);
                }

                button.Location = new Point(startX + (squareSize * i), startY);
                button.Size = new Size(squareSize, squareSize);
                button.BackColor = Color.Gray;
                button.Enabled = false;
                this.Controls.Add(button);
            }


            //Нижняя линия
            for (int i = 0; i < SIZE; i++)
            {
                Button button = new Button();

                button.Text = Convert.ToString(i + 1);
                button.Font = new Font(button.Font.FontFamily, 12, FontStyle.Bold);

                button.Location = new Point(startX, startY + (squareSize * (i + 1)));
                button.Size = new Size(squareSize, squareSize);
                button.BackColor = Color.Gray;
                button.Enabled = false;
                this.Controls.Add(button);
            }


            //создания матрици
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    thisField[i, j] = new Button();

                    thisField[i, j].Location = new Point(startX + (squareSize * (i + 1)), startY + (squareSize * (j + 1)));
                    thisField[i, j].Size = new Size(squareSize, squareSize);
                    thisField[i, j].Click += new EventHandler(DetectShip);
                    this.Controls.Add(thisField[i, j]);
                }
            }
        }
        void ClearField(ref Button[,] thisField)
        {
            for (int i = 0; i < thisField.GetLength(0); i++)
            {
                for (int j = 0; j < thisField.GetLength(0); j++)
                {
                    thisField[i, j].Enabled = true;
                    thisField[i, j].Visible = true;
                    thisField[i, j].Text = string.Empty;
                    thisField[i, j].BackColor = Color.Transparent;
                }
            }
        }
        void BlokField(ref Button[,] thisField)
        {
            for (int i = 0; i < thisField.GetLength(0); i++)
            {
                for (int j = 0; j < thisField.GetLength(0); j++)
                {
                    thisField[i, j].Enabled = false;
                }
            }
        }
        void UnLockField(ref Button[,] thisField)
        {
            for (int i = 0; i < thisField.GetLength(0); i++)
            {
                for (int j = 0; j < thisField.GetLength(0); j++)
                {
                    thisField[i, j].Enabled = true;
                }
            }
        }




        BattleField bot;
        BattleField player;

        private void button1_Click(object sender, EventArgs e)
        {
            if (bot == null && player == null)
            {
                bot = new BattleField();
                player = new BattleField();

                CreateField(ref myField, 80, 60);
                CreateField(ref enemyField, 710, 60);
            }

            bot.Placement();
            player.Placement();

            ClearField(ref myField);
            ClearField(ref enemyField);

            BlokField(ref myField);
            BlokField(ref enemyField);

            player.DrawShips(myField);


            button2.Visible = true;

            label1.Visible = true;
            label2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UnLockField(ref enemyField);

            button1.Visible = false;

            start = true;

            button2.Visible = false;
        }



        //Обработчик собитий на кнопку
        public void DetectShip(object sender, EventArgs e)
        {

            Button pressedButton = sender as Button;
            if (start)
            {
                if (!(bot.Win || player.Win))
                {
                    int x = ((pressedButton.Location.X - 710) / squareSize) - 1;
                    int y = ((pressedButton.Location.Y - 60) / squareSize) - 1;


                    int myResult = player.ShotMan(bot, x, y);


                    player.DrawEnemyField(bot, enemyField);
                    player.DrawMyField(bot, myField);


                    if (myResult == (int)eOptionShots.missed)
                    {
                        //Вистрел бота
                        while (bot.ShotBot(player, FREEZETIME))
                        {
                            player.DrawEnemyField(bot, enemyField);
                            player.DrawMyField(bot, myField);
                        }
                    }
                }


                player.DrawEnemyField(bot, enemyField);
                player.DrawMyField(bot, myField);


                if (bot.Win == true)
                {
                    MessageBox.Show("YOU LOSE !!!");
                    BlokField(ref enemyField);
                    bot.DrawShips(enemyField);

                    button5.Visible = true;
                }
                else if (player.Win == true)
                {
                    MessageBox.Show("YOU WIN !!!");
                    BlokField(ref enemyField);
                    bot.DrawShips(enemyField);

                    button5.Visible = true;
                }
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            mainForm form = new mainForm();
            form.Show();
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        Point point;
        private void MenAndBotForm_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }
        private void MenAndBotForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bot = new BattleField();
            player = new BattleField();

            button1.Visible = true;


            ClearField(ref myField);
            ClearField(ref enemyField);

            BlokField(ref myField);
            BlokField(ref enemyField);

            button5.Visible = false;
        }
    }
}
