using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_Calculator
{
    public partial class Form1 : Form
    {
        double result = 0;
        string op = "";
        bool xyz = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_click(object sender, EventArgs e)
        {
            if(txt_result.Text == "0" || (xyz)) 
            {
                txt_result.Clear();
            }
            xyz = false;
            Button b = (Button)sender;
            if(b.Text == ".")
            {
                if(!txt_result.Text.Contains("."))
                {
                    txt_result.Text = txt_result.Text + b.Text;
                }
                  
            }
            else
            txt_result.Text = txt_result.Text + b.Text;
        }

        private void operatior(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (result != 0)
            {
                button16.PerformClick();
                op = b.Text;
             
                lvl.Text = result + " " + op;
                xyz = true;
            }
            else
            {
                op = b.Text;
                result = double.Parse(txt_result.Text);
                lvl.Text = result + " " + op;
                xyz = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txt_result.Text = "0";
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            txt_result.Text = "0";
            result = 0;
        }

        private void button_999(object sender, EventArgs e)
        {
            switch(op)
            {
                case "+":
                    txt_result.Text = (result + double.Parse(txt_result.Text)).ToString();
                    break;
                case "-":
                    txt_result.Text = (result - double.Parse(txt_result.Text)).ToString();
                    break;
                case "*":
                    txt_result.Text = (result * double.Parse(txt_result.Text)).ToString();
                    break;
                case "/":
                    txt_result.Text = (result / double.Parse(txt_result.Text)).ToString();
                    break;
                default:
                    break;
            }
            result = double.Parse(txt_result.Text);
            lvl.Text = ""; 
        }
    }
}
