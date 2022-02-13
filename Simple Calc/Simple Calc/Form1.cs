using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



///!Accident within the program! textbox_result is actually the label whereas textBox1 is the textbox_result

namespace Simple_Calc
{
    public partial class Form1 : Form
    {
        Double resultValue = 0;
        string operationPerformed = "";
        bool isOperationPerformed = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void textbox_result_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonclick(object sender, EventArgs e)
        {
            /// Clearing the number 0 from textboxOne 
            if ((textBox1.Text == "0") || (isOperationPerformed))
                textBox1.Clear();

            ///Buttons will print their text, effectively being able to print every button with 3 lines of code instead of 30
            isOperationPerformed = false;
            Button button = (Button)sender;
            textBox1.Text = textBox1.Text + button.Text;
        }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operationPerformed = button.Text;
            resultValue = Double.Parse(textBox1.Text);
            textbox_result.Text = resultValue + " " + operationPerformed;
            isOperationPerformed = true;

        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            resultValue = 0;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ///multiplication, addition, subtraction, division all put together
            switch(operationPerformed)
            {
                case "+":
                    textBox1.Text=(resultValue + Double.Parse(textBox1.Text)).ToString();
                    break;
                case "-":
                    textBox1.Text = (resultValue - Double.Parse(textBox1.Text)).ToString();
                    break;
                case "*":
                    textBox1.Text = (resultValue * Double.Parse(textBox1.Text)).ToString();
                    break;
                case "/":
                    textBox1.Text = (resultValue / Double.Parse(textBox1.Text)).ToString();
                    break;
            }
        }
    }
}
