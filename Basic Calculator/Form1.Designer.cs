using System.Collections.Generic;

namespace Interfata
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.Windows.Forms.TextBox textBox;
        private System.ComponentModel.IContainer components = null;

        

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Text = "Operations Here";
        }

        private void keyboarPresshandler(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                System.Data.DataTable dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(textBox.Text,"");
                textBox.Text = result.ToString();
            }
        }

        public void buttonHandler(object sender, System.EventArgs e)
        {
            var button = sender as System.Windows.Forms.Button;
            if(button.Text == "=")
            {
                System.Data.DataTable dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(textBox.Text,"");
                textBox.Text = result.ToString();
            }
            else
            {
                textBox.Text += button.Text;
            }
            
        }


        private void InitializeNumbers()
        {
            List<System.Windows.Forms.Button> buttons = new List<System.Windows.Forms.Button>();
            for (int i = 0; i < 10; i++)
            {
                System.Windows.Forms.Button numberedButton = new System.Windows.Forms.Button();
                numberedButton.Text = i.ToString();
                numberedButton.Location = new System.Drawing.Point(10,10 + (25*i));
                numberedButton.Click += buttonHandler;
                buttons.Add(numberedButton);
            }
            System.Windows.Forms.Button plusButton = new System.Windows.Forms.Button();
                plusButton.Text = "+";
                plusButton.Location = new System.Drawing.Point(120,10);
                plusButton.Click += buttonHandler;
                buttons.Add(plusButton);
            System.Windows.Forms.Button minusButton = new System.Windows.Forms.Button();
                minusButton.Text = "-";
                minusButton.Location = new System.Drawing.Point(120,10 + (25));
                minusButton.Click += buttonHandler;
                buttons.Add(minusButton);

            System.Windows.Forms.Button timesButton = new System.Windows.Forms.Button();
                timesButton.Text = "*";
                timesButton.Location = new System.Drawing.Point(120,10 + (25*2));
                timesButton.Click += buttonHandler;
                buttons.Add(timesButton);

            System.Windows.Forms.Button divisionButton = new System.Windows.Forms.Button();
                divisionButton.Text = "/";
                divisionButton.Location = new System.Drawing.Point(120,10 + (25*3));
                divisionButton.Click += buttonHandler;
                buttons.Add(divisionButton);
            
            System.Windows.Forms.Button leftParanthesisButton = new System.Windows.Forms.Button();
                leftParanthesisButton.Text = "(";
                leftParanthesisButton.Location = new System.Drawing.Point(120,10 + (25*4));
                leftParanthesisButton.Click += buttonHandler;
                buttons.Add(leftParanthesisButton);
            
            System.Windows.Forms.Button rightParanthesisButton = new System.Windows.Forms.Button();
                rightParanthesisButton.Text = ")";
                rightParanthesisButton.Location = new System.Drawing.Point(120,10 + (25*5));
                rightParanthesisButton.Click += buttonHandler;
                buttons.Add(rightParanthesisButton);
            
            System.Windows.Forms.Button equalsButton = new System.Windows.Forms.Button();
                equalsButton.Text = "=";
                equalsButton.Location = new System.Drawing.Point(120,10 + (25*6));
                equalsButton.Click += buttonHandler;
                buttons.Add(equalsButton);

            for (int i = 0; i < 17; i++)
            {
                Controls.Add(buttons[i]);    
            }

            textBox = new System.Windows.Forms.TextBox();
            textBox.Location = new System.Drawing.Point(120,10+25*7);
            Controls.Add(textBox);
            textBox.KeyUp += keyboarPresshandler;

        }


    
        #endregion
    }
}

