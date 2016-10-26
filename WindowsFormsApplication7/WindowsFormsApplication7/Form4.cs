using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication7
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        //private int ii=0;

        private ArrayList returnal;

        public string ins;

        public ArrayList Value
        {
            set { returnal = value; }
            get { return returnal; }
        }

        public void setwin(int cc)
        {
            switch (cc)
            {
                case 0:
                    button1.Text = "OK";
                    button2.Text = "Cancel";
                    ins = "please enter ";
                    textBox1.ImeMode = ImeMode.Off;
                    textBox2.ImeMode = ImeMode.Off;
                    textBox3.ImeMode = ImeMode.Off;
                    textBox4.ImeMode = ImeMode.Off;
                    textBox5.ImeMode = ImeMode.Off;
                    break;
                case 1:
                    button1.Text = "确定";
                    button2.Text = "取消";
                    ins = "请输入";
                    textBox1.ImeMode = ImeMode.On;
                    textBox2.ImeMode = ImeMode.On;
                    textBox3.ImeMode = ImeMode.On;
                    textBox4.ImeMode = ImeMode.On;
                    textBox5.ImeMode = ImeMode.On;
                    break;
                default:
                    break;
            }
        }

        public void settext(ArrayList cardlabel, int c)
        {
            textBox1.Text = ins + ((ArrayList)cardlabel[0])[c].ToString();
            textBox2.Text = ins + ((ArrayList)cardlabel[1])[c].ToString();
            textBox3.Text = ins + ((ArrayList)cardlabel[2])[c].ToString();
            textBox4.Text = ins + ((ArrayList)cardlabel[3])[c].ToString();
            textBox5.Text = ins + ((ArrayList)cardlabel[4])[c].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            returnal = new ArrayList();
            ArrayList itmal = new ArrayList();
            itmal.Add(textBox1.Text);
            itmal.Add(textBox1.Text);
            returnal.Add(itmal);
            itmal = new ArrayList();
            itmal.Add(textBox2.Text);
            itmal.Add(textBox2.Text);
            returnal.Add(itmal);
            itmal = new ArrayList();
            itmal.Add(textBox3.Text);
            itmal.Add(textBox3.Text);
            returnal.Add(itmal);
            itmal = new ArrayList();
            itmal.Add(textBox4.Text);
            itmal.Add(textBox4.Text);
            returnal.Add(itmal);
            itmal = new ArrayList();
            itmal.Add(textBox5.Text);
            itmal.Add(textBox5.Text);
            returnal.Add(itmal);
            for (int i = 6; i <= 14; i++)
            {
                itmal = new ArrayList();
                returnal.Add(itmal);
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            returnal = null;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //ii++;
            //if (ii > 1) 
            //MessageBox.Show("L");
        }


    }
}
