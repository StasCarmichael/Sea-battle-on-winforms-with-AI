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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        //2 бота
        private void button2Bots_Click(object sender, EventArgs e)
        {
            BotForms botForms = new BotForms();

            botForms.Show();
            this.Hide();
        }

        //Игрок и бот
        private void button1_Click(object sender, EventArgs e)
        {
            MenAndBotForm menAndBot = new MenAndBotForm();

            menAndBot.Show();
            this.Hide();
        }

        //Exit
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
