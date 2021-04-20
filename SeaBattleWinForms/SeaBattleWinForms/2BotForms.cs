using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;



namespace SeaBattleWinForms
{
    public partial class BotForms : Form
    {
        const int SIZE = 10;
        const int squareSize = 38;

        public BotForms()
        {
            InitializeComponent();
        }


        Button[,] myField;
        Button[,] enemyField;


        Thread mainThread;
        Thread myThread1;
        Thread myThread2;


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
                    thisField[i, j].BackColor = Color.FromArgb(171, 171, 171);
                    this.Controls.Add(thisField[i, j]);
                }
            }
        }


        //Service Method
        private void UpdateFieldMyField()
        {
            while (true)
            {
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        myField[i, j].Update();
                    }
                }

                Thread.Sleep(100);
            }
        }
        private void UpdateFieldEnymyField()
        {
            while (true)
            {
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        enemyField[i, j].Update();
                    }
                }

                Thread.Sleep(100);
            }
        }



        BattleField chuck;
        BattleField morgan;


        //Battle Method
        private void BattleBot()
        {
            const int FREEZETIME = 750;

            myThread1 = new Thread(new ThreadStart(UpdateFieldMyField));
            myThread2 = new Thread(new ThreadStart(UpdateFieldEnymyField));

            myThread1.Start();
            myThread2.Start();


            #region Battle

            while (!(chuck.Win || morgan.Win))
            {

                //Отрисовка кораблей
                {
                    //корабли и вистрели чака
                    chuck.DrawMyField(morgan, myField);

                    //корабли и вистрели моргана
                    morgan.DrawMyField(chuck, enemyField);
                }


                //Вистрел Чака по Моргану
                while (chuck.ShotBot(morgan, FREEZETIME))
                {
                    //корабли и вистрели чака
                    chuck.DrawMyField(morgan, myField);

                    //корабли и вистрели моргана
                    morgan.DrawMyField(chuck, enemyField);
                }


                //Отрисовка кораблей
                {
                    //корабли и вистрели чака
                    chuck.DrawMyField(morgan, myField);

                    //корабли и вистрели моргана
                    morgan.DrawMyField(chuck, enemyField);
                }


                ////////Fix Bug
                //Проверка на бистрий виграш
                if (chuck.Win || morgan.Win) { goto Finihe; }


                //Вистрел Моргана по Чаку
                while (morgan.ShotBot(chuck, FREEZETIME))
                {
                    //корабли и вистрели чака
                    chuck.DrawMyField(morgan, myField);

                    //корабли и вистрели моргана
                    morgan.DrawMyField(chuck, enemyField);
                }


                //Отрисовка кораблей
                {
                    //корабли и вистрели чака
                    chuck.DrawMyField(morgan, myField);

                    //корабли и вистрели моргана
                    morgan.DrawMyField(chuck, enemyField);
                }
            }

            #endregion


            myThread1.Abort();
            myThread2.Abort();


        //Виводим результат стражения
        Finihe:
            if (chuck.Win == true) { button2.Visible = false; MessageBox.Show("Bot1 WINS"); }
            else if (morgan.Win == true) { button2.Visible = false; MessageBox.Show("Bot2 WINS"); }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            chuck = new BattleField();
            morgan = new BattleField();

            chuck.Placement();
            morgan.Placement();


            CreateField(ref myField, 80, 60);
            CreateField(ref enemyField, 710, 60);


            label1.Visible = true;
            label2.Visible = true;


            button1.Enabled = false;
            button1.Visible = false;

            button2.Visible = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;

            mainThread = new Thread(new ThreadStart(BattleBot));
            mainThread.Start();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (myThread1 != null) { if (myThread1.IsAlive) { myThread1.Abort(); } };
            if (myThread2 != null) { if (myThread2.IsAlive) { myThread2.Abort(); } };

            if (mainThread != null) { if (mainThread.IsAlive) { mainThread.Abort(); } };

            mainForm mainForm = new mainForm();
            mainForm.Show();
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (myThread1 != null) { if (myThread1.IsAlive) { myThread1.Abort(); } };
            if (myThread2 != null) { if (myThread2.IsAlive) { myThread2.Abort(); } };

            if (mainThread != null) { if (mainThread.IsAlive) { mainThread.Abort(); } };

            Application.Exit();
        }




        Point point;
        private void BotForms_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }
        private void BotForms_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }


    }
}
