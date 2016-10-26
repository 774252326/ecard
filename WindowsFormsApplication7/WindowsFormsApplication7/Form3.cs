using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            //button1.Text = "OK";
            //button2.Text = "Cancel";
            
        }

        private string returnValue;
        private string returnItem;
        private int returnidx;

        public string Value
        {
            set { returnValue = value; }
            get { return returnValue; }
        }
        
        public string Item
        {
            set { returnItem = Item; }
            get { return returnItem; }
        }

        public int Index
        {
            set { returnidx = Index; }
            get { return returnidx; }
        }

        public void setcombo(string v)
        {
            //textBox1.Text = v;
            comboBox1.Items.Add(v);
            comboBox1.SelectedIndex = 0;
        }

        public void settext(string v)
        {
            textBox1.Text = v;
            //comboBox1.Items.Add(v);
            //comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = false;
        }

        public void setwin(int cc)
        {
            switch (cc)
            {
                case 0:
                    button1.Text = "OK";
                    button2.Text = "Cancel";
                    break;
                case 1:
                    button1.Text = "确定";
                    button2.Text = "取消";
                    break;
                default:
                    break;
            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                returnValue = textBox1.Text;
            }
            else
            {
                returnValue = null;
            }
            returnItem = comboBox1.SelectedItem.ToString();
            returnidx = comboBox1.SelectedIndex;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            returnValue = null;
            returnItem = null;
            returnidx = -1;
            Close();
        }


    }
}
