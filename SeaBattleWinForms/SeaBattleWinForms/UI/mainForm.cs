using System;
using System.Windows.Forms;


namespace SeaBattleWinForms
{
    public partial class MainForm : Form
    {
        BotForms botForms;
        MenAndBotForm menAndBot;

        public MainForm()
        {
            InitializeComponent();
        }


        //2 бота
        private void button2Bots_Click(object sender, EventArgs e)
        {
            botForms = new BotForms(this);
            botForms.Show();

            Hide();
        }

        //Игрок и бот
        private void buttonBotAndPlayer_Click(object sender, EventArgs e)
        {
            menAndBot = new MenAndBotForm(this);
            menAndBot.Show();

            Hide();
        }

        //Exit
        private void buttonExit_Click(object sender, EventArgs e) => Application.Exit();


    }
}
