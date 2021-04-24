
namespace SeaBattleWinForms
{
    partial class MenAndBotForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenAndBotForm));
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonManPlacement = new System.Windows.Forms.Button();
            this.buttonStartManPlacment = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button4.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(956, 556);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 96);
            this.button4.TabIndex = 7;
            this.button4.Text = "Вихід з програми";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button3.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(724, 556);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 96);
            this.button3.TabIndex = 6;
            this.button3.Text = "Вернутись на головну форму";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Lime;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(528, 556);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 96);
            this.button2.TabIndex = 5;
            this.button2.Text = "Старт";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(79, 556);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 96);
            this.button1.TabIndex = 4;
            this.button1.Text = "Авто Розстановка";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(823, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 47);
            this.label2.TabIndex = 9;
            this.label2.Text = "YOUR ENEMY";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(245, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 47);
            this.label1.TabIndex = 8;
            this.label1.Text = "YOU";
            this.label1.Visible = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Lime;
            this.button5.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(541, 548);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(131, 118);
            this.button5.TabIndex = 10;
            this.button5.Text = "Заново";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonManPlacement
            // 
            this.buttonManPlacement.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonManPlacement.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonManPlacement.Location = new System.Drawing.Point(302, 556);
            this.buttonManPlacement.Name = "buttonManPlacement";
            this.buttonManPlacement.Size = new System.Drawing.Size(160, 96);
            this.buttonManPlacement.TabIndex = 11;
            this.buttonManPlacement.Text = "Розстановка";
            this.buttonManPlacement.UseVisualStyleBackColor = false;
            this.buttonManPlacement.Click += new System.EventHandler(this.buttonManPlacement_Click);
            // 
            // buttonStartManPlacment
            // 
            this.buttonStartManPlacment.BackColor = System.Drawing.Color.LawnGreen;
            this.buttonStartManPlacment.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartManPlacment.Location = new System.Drawing.Point(520, 313);
            this.buttonStartManPlacment.Name = "buttonStartManPlacment";
            this.buttonStartManPlacment.Size = new System.Drawing.Size(180, 100);
            this.buttonStartManPlacment.TabIndex = 12;
            this.buttonStartManPlacment.Text = "Розставити";
            this.buttonStartManPlacment.UseVisualStyleBackColor = false;
            this.buttonStartManPlacment.Visible = false;
            this.buttonStartManPlacment.Click += new System.EventHandler(this.buttonStartManPlacment_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.Firebrick;
            this.buttonClear.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClear.Location = new System.Drawing.Point(520, 194);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(180, 100);
            this.buttonClear.TabIndex = 13;
            this.buttonClear.Text = "Очистити";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Visible = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // MenAndBotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonStartManPlacment);
            this.Controls.Add(this.buttonManPlacement);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MenAndBotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sea Battle";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenAndBotForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MenAndBotForm_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonManPlacement;
        private System.Windows.Forms.Button buttonStartManPlacment;
        private System.Windows.Forms.Button buttonClear;
    }
}