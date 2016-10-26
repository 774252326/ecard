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
                    break;
                case 1:
                    button1.Text = "确定";
                    button2.Text = "取消";
                    ins = "请输入";
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
            for (int i = 0; i < 9; i++)
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


    }
}
