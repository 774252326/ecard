using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            //textBox1.Text = ;
            button1.Text = "OK";
            button2.Text = "Cancel";
        }

        private string returnValue;
        public string Value
        {
            set { returnValue = value; }
            get { return returnValue; }
        }

        public void set(string v)
        {
            textBox1.Text = v;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            returnValue = textBox1.Text;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            returnValue = null;
            Close();
        }
    }
}
