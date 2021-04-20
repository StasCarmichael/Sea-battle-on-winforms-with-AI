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
        const int FIELD_SIZE = 10;
        const int SQUARE_SIZE = 38;
        const int FREEZE_TIME = 700;


        bool start = false;


        public MenAndBotForm()
        {
            InitializeComponent();
        }


        Button[,] myField;
        Button[,] enemyField;


        void CreateField(ref Button[,] thisField, int startX, int startY)
        {
            thisField = new Button[FIELD_SIZE, FIELD_SIZE];

            //Верхняя линия
            for (int i = 0; i < FIELD_SIZE + 1; i++)
            {
                string str = "АБВГДЕЄЖЗИ";

                Button button = new Button();
                if (i != 0)
                {
                    button.Text = Convert.ToString(str[i - 1]);
                    button.Font = new Font(button.Font.FontFamily, 12, FontStyle.Bold);
                }

                button.Location = new Point(startX + (SQUARE_SIZE * i), startY);
                button.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
                button.BackColor = Color.Gray;
                button.Enabled = false;
                this.Controls.Add(button);
            }


            //Нижняя линия
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                Button button = new Button();

                button.Text = Convert.ToString(i + 1);
                button.Font = new Font(button.Font.FontFamily, 12, FontStyle.Bold);

                button.Location = new Point(startX, startY + (SQUARE_SIZE * (i + 1)));
                button.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
                button.BackColor = Color.Gray;
                button.Enabled = false;
                this.Controls.Add(button);
            }


            //создания матрици
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    thisField[i, j] = new Button();

                    thisField[i, j].Location = new Point(startX + (SQUARE_SIZE * (i + 1)), startY + (SQUARE_SIZE * (j + 1)));
                    thisField[i, j].Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
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


        //Service Method
        private void UpdateFieldMyField()
        {

            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    myField[i, j].Update();
                }
            }

        }
        private void UpdateFieldEnymyField()
        {
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    enemyField[i, j].Update();
                }
            }
        }


        BattleField bot;
        BattleField player;



        //Обработчик собитий на кнопку
        public void DetectShip(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;


            if (start)
            {
                if (!(bot.Win || player.Win))
                {
                    int x = ((pressedButton.Location.X - 710) / SQUARE_SIZE) - 1;
                    int y = ((pressedButton.Location.Y - 60) / SQUARE_SIZE) - 1;


                    int myResult = player.ShotMan(bot, x, y);


                    player.DrawEnemyField(bot, enemyField);
                    player.DrawMyField(bot, myField);


                    if (myResult == (int)eOptionShots.missed)
                    {
                        //Вистрел бота
                        while (bot.ShotBot(player, FREEZE_TIME))
                        {
                            player.DrawEnemyField(bot, enemyField);
                            player.DrawMyField(bot, myField);


                            UpdateFieldMyField();
                        }

                    }

                    player.DrawEnemyField(bot, enemyField);
                    player.DrawMyField(bot, myField);


                    UpdateFieldMyField();
                }
                else
                {
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
        }


        //Main Service Method
        private void FullPlacement()
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


        private void button1_Click(object sender, EventArgs e)
        {
            FullPlacement();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            UnLockField(ref enemyField);

            button1.Visible = false;
            button2.Visible = false;

            start = true;
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


    }
}
