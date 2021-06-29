using System;
using System.Drawing;
using System.Windows.Forms;
using SeaBattleWinForms.Extension;
using SeaBattleWinForms.Logic;

namespace SeaBattleWinForms
{
    public partial class MenAndBotForm : Form
    {
        MainForm mainForm;

        const int FIELD_SIZE = 10;
        const int SQUARE_SIZE = 38;
        const int FREEZE_TIME = 400;


        const int START_X = 80;
        const int START_Y = 60;
        const int DIFFERENCE_BETWEEN_FIELD = 630;


        private static readonly Color DETECTION_COLOR = Color.Red;
        private static readonly Color BASE_COLOR = Color.Transparent;


        bool start = false;


        public MenAndBotForm(MainForm _mainForm)
        {
            InitializeComponent();

            mainForm = _mainForm;

            myFieldDelegate = new MyDelegat(UpdateFieldMyField);
            enemyFieldDelegate = new MyDelegat(UpdateFieldEnemyField);
        }


        delegate void MyDelegat();
        MyDelegat myFieldDelegate;
        MyDelegat enemyFieldDelegate;


        Button[,] myField;
        Button[,] enemyField;
        Button[,] placementField;


        void CreateField(out Button[,] thisField, EventHandler eventHandler, int startX, int startY)
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
                    thisField[i, j].Click += new EventHandler(eventHandler);
                    thisField[i, j].BackColor = BASE_COLOR;
                    this.Controls.Add(thisField[i, j]);
                }
            }

        }


        void ClearField(ref Button[,] thisField)
        {
            for (int i = 0; i < thisField.GetLength(0); i++)
            {
                for (int j = 0; j < thisField.GetLength(1); j++)
                {
                    thisField[i, j].Visible = true;
                    thisField[i, j].Text = string.Empty;
                    thisField[i, j].BackColor = BASE_COLOR;
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
                for (int j = 0; j < thisField.GetLength(1); j++)
                {
                    thisField[i, j].Enabled = true;
                }
            }
        }
        void VisibleField(ref Button[,] thisField)
        {
            for (int i = 0; i < thisField.GetLength(0); i++)
            {
                for (int j = 0; j < thisField.GetLength(1); j++)
                {
                    thisField[i, j].Visible = false;
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
        private void UpdateFieldEnemyField()
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
                    int x = ((pressedButton.Location.X - (START_X + DIFFERENCE_BETWEEN_FIELD)) / SQUARE_SIZE) - 1;
                    int y = ((pressedButton.Location.Y - START_Y) / SQUARE_SIZE) - 1;


                    int myResult = player.ShotMan(bot, x, y);


                    player.DrawEnemyField(bot, enemyField);
                    player.DrawMyField(bot, myField);


                    if (myResult == (int)eOptionShots.missed)
                    {
                        BlokField(ref enemyField);

                        //Вистрел бота
                        while (bot.ShotBot(player, FREEZE_TIME))
                        {
                            player.DrawEnemyField(bot, enemyField);
                            player.DrawMyField(bot, myField);

                            this.Invoke(myFieldDelegate);
                            this.Invoke(enemyFieldDelegate);
                        }


                        player.DrawEnemyField(bot, enemyField);
                        player.DrawMyField(bot, myField);

                    }


                    this.Invoke(myFieldDelegate);
                    this.Invoke(enemyFieldDelegate);

                    UnLockField(ref enemyField);

                }

                if (bot.Win == true)
                {
                    MessageBox.Show("YOU LOSE !!!");
                    BlokField(ref enemyField);
                    bot.DrawShips(enemyField);

                    buttonRestart.Visible = true;
                }
                else if (player.Win == true)
                {
                    MessageBox.Show("YOU WIN !!!");
                    BlokField(ref enemyField);
                    bot.DrawShips(enemyField);

                    buttonRestart.Visible = true;
                }

            }
        }
        public void PlacementShip(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            if (pressedButton.BackColor == BASE_COLOR)
            {
                pressedButton.BackColor = DETECTION_COLOR;
            }
            else
            {
                pressedButton.BackColor = BASE_COLOR;
            }
        }



        //Main Service Method
        private void FullPlacement()
        {
            if (bot == null || player == null)
            {
                CreateField(out myField, DetectShip, START_X, START_Y);
                CreateField(out enemyField, DetectShip, START_X + DIFFERENCE_BETWEEN_FIELD, START_Y);
            }
            if (bot == null) { bot = new BattleField(); }
            if (player == null) { player = new BattleField(); }


            bot.AutoPlacement();
            player.AutoPlacement();

            ClearField(ref myField);
            ClearField(ref enemyField);

            BlokField(ref myField);
            BlokField(ref enemyField);


            player.DrawShips(myField);


            button2.Visible = true;


            label1.Visible = true;
            label2.Visible = true;
        }


        private void buttonAutoPlacment_Click(object sender, EventArgs e)
        {
            if (placementField != null) { VisibleField(ref placementField); placementField = null; }
            buttonManPlacement.Visible = false;
            buttonClear.Visible = false;
            buttonStartManPlacment.Visible = false;

            FullPlacement();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (placementField != null) { VisibleField(ref placementField); placementField = null; }

            UnLockField(ref enemyField);

            buttonAutoPlacment.Visible = false;
            button2.Visible = false;

            start = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.Show();

            //Очистка форми
            Dispose();
            Close();
        }
        private void buttonExit_Click(object sender, EventArgs e) => Application.Exit();


        private void buttonRestart_Click(object sender, EventArgs e)
        {
            bot = new BattleField();
            player = new BattleField();


            buttonAutoPlacment.Visible = true;


            ClearField(ref myField);
            ClearField(ref enemyField);

            BlokField(ref myField);
            BlokField(ref enemyField);

            buttonRestart.Visible = false;
        }
        private void buttonManPlacement_Click(object sender, EventArgs e)
        {
            if (placementField == null)
            {
                CreateField(out placementField, PlacementShip, START_X, START_Y);
                ClearField(ref placementField);
            }

            buttonClear.Visible = true;
            buttonStartManPlacment.Visible = true;
            buttonManPlacement.Visible = false;
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearField(ref placementField);
        }
        private void buttonStartManPlacment_Click(object sender, EventArgs e)
        {
            player = new BattleField();

            if (player.HumanPlacementWinForms(ref placementField, DETECTION_COLOR))
            {
                buttonClear.Visible = false;
                buttonStartManPlacment.Visible = false;
                buttonAutoPlacment.Visible = false;


                if (myField == null) { CreateField(out myField, DetectShip, START_X, START_Y); }
                if (enemyField == null) { CreateField(out enemyField, DetectShip, START_X + DIFFERENCE_BETWEEN_FIELD, START_Y); }
                VisibleField(ref placementField);
                ClearField(ref myField);
                ClearField(ref enemyField);


                bot = new BattleField();
                bot.AutoPlacement();


                player.DrawShips(myField);
                BlokField(ref myField);

                label1.Visible = true;
                label2.Visible = true;

                start = true;
            }
            else
            {
                MessageBox.Show("Будь ласка !!! \n Розставте кораблі вірно.");
            }
        }




        Point point;
        private void MenAndBotForm_MouseDown(object sender, MouseEventArgs e) => point = new Point(e.X, e.Y);

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
