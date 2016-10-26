using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;


namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {

        //1 for chinese 0 for english
        public const int lang = 1;

        public string filePath;
        //CardBook pcp;
        //Card cardLabel;
        //public Encoding ecg = Encoding.GetEncoding("gb2312");
        //public Encoding ecg = Encoding.UTF8;
        //ArrayList buf;

        ArrayList clabel;
        ArrayList cardmemo;

        //ArrayList fplist;
        ArrayList imglist;

        //private int cursel = 0;

        public Form1()
        {
            InitializeComponent();
            ColumnHeader colHead;

            colHead = new ColumnHeader();
            listView1.Columns.Add(colHead);
            colHead = new ColumnHeader();
            listView1.Columns.Add(colHead);

            setwin(lang);
            clabel = new ArrayList();
            setCardLabel(ref clabel);

            cardmemo = new ArrayList();
            //fplist = new ArrayList();
            imglist = new ArrayList();
        }



        private void setwin(int cc)
        {
            switch (cc)
            {
                case 0:
                    listView1.Columns[0].Text = "item";
                    listView1.Columns[1].Text = "value";
                    button1.Text = "add";
                    button2.Text = "open";
                    button3.Text = "save";
                    button4.Text = "remove item";
                    button5.Text = "add item";
                    button6.Text = "change item";
                    button7.Text = "delete";
                    button8.Text = "change logo";
                    button9.Text = "delete logo";
                    break;
                case 1:
                    listView1.Columns[0].Text = "项";
                    listView1.Columns[1].Text = "值";
                    button1.Text = "添加";
                    button2.Text = "打开";
                    button3.Text = "保存";
                    button4.Text = "移除项";
                    button5.Text = "添加项";
                    button6.Text = "修改项";
                    button7.Text = "删除";
                    button8.Text = "修改标志";
                    button9.Text = "删除标志";
                    break;
                default:
                    break;
            }
            updbtn();
        }


        private void setCardLabel(ref ArrayList labelAl)
        {
            Card cardLabel = new Card();

            cardLabel.Addr = new[] { "Address", "地址" };
            cardLabel.CompanyName = new[] { "Company Name", "公司名" };
            cardLabel.CompanyPhone = new[] { "Company Phone", "公司电话" };
            cardLabel.CompanyWebsite = new[] { "Comapny Website", "公司网址" };
            cardLabel.Directline = new[] { "Directline", "直线电话" };
            cardLabel.Email = new[] { "Email", "电子邮件" };
            cardLabel.Fax = new[] { "Fax", "传真" };
            cardLabel.Mobile = new[] { "Mobile", "手机" };
            cardLabel.MSN = new[] { "MSN", "MSN" };
            cardLabel.Name = new[] { "Name", "姓名" };
            cardLabel.Prefix = new[] { "Prefix", "尊称" };
            cardLabel.Skype = new[] { "Skype", "Skype" };
            cardLabel.Suffix = new[] { "Suffix", "后缀" };
            cardLabel.Title = new[] { "Title", "职位" };

            cardLabel.Card2Al(ref labelAl);
        }


        private void showcard(ArrayList al)
        {

            //if (indexmemo != null)
            //indexmemo.Clear();

            //indexmemo = new ArrayList();

            ListViewItem lvi;
            ArrayList itmal;
            ArrayList itmlabel;
            int i, j;
            for (i = 0; i < 6; i++)
            {
                if (i >= al.Count)
                    return;

                itmal = (ArrayList)al[i];
                itmlabel = (ArrayList)clabel[i];
                for (j = lang; j < itmal.Count; j += 2)
                {
                    lvi = new ListViewItem();
                    lvi.SubItems[0].Text = itmlabel[lang].ToString();
                    lvi.SubItems.Add(itmal[j].ToString());
                    listView1.Items.Add(lvi);
                }
            }

            for (i = 6; i < al.Count; i++)
            {
                itmal = (ArrayList)al[i];
                itmlabel = (ArrayList)clabel[i];
                //for (j = lang; j < itmal.Count; j += 2)
                foreach (string val in itmal)
                {
                    lvi = new ListViewItem();
                    lvi.SubItems[0].Text = itmlabel[lang].ToString();
                    lvi.SubItems.Add(val.ToString());
                    listView1.Items.Add(lvi);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {

                        filePath = openFileDialog1.FileName;

                        CardBook c1 = JsonConvert.DeserializeObject<CardBook>(File.ReadAllText(filePath));
                        c1.CardBook2Al(ref cardmemo);
                        c1.CardBookLogo2Al(ref imglist);
                        //cursel = cardmemo.Count - 1;

                        

                        updbox();
                        listBox1.SelectedIndex = cardmemo.Count - 1;
                        tabControl1.SelectedTab.Text = filePath;
                        myStream.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {



            if (String.IsNullOrEmpty(filePath))
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        // Code to write the stream goes here.
                        filePath = saveFileDialog1.FileName;
                        myStream.Close();


                    }
                }

            }

            CardBook ppp = new CardBook();
            ppp.Al2CardBook(cardmemo);
            ppp.Al2CardBookLogo(imglist);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(ppp));

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ArrayList buf = (ArrayList)cardmemo[listBox1.SelectedIndex];
                delitms(ref buf, listView1.SelectedIndices);
                updview();
            }
            else
                MessageBox.Show("请先选择要修改的某一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void delitms(ref ArrayList al, ListView.SelectedIndexCollection indice)
        {
            int i = 0;
            foreach (int ind in indice)
            {
                if (ind - i > 4)
                {
                    delitm(ref al, ind - i);
                    i++;
                }
            }
        }

        public void delitm(ref ArrayList al, int i)
        {
            ///foreach (ArrayList itmal in al)
            ///{
            ///    if (i >= itmal.Count)
            //    {
            //        i -= itmal.Count;
            //    }
            //    else
            //    {
            //        itmal.RemoveAt(i);
            //        return;
            //    }
            ///}



            int j;
            for (j = 0; j < 6; j++)
            {
                ArrayList itmal = (ArrayList)al[j];
                if (i >= (itmal.Count / 2))
                {
                    i -= itmal.Count / 2;
                }
                else
                {
                    itmal.RemoveAt(2 * i + 1);
                    //int ii = 1 - lang - lang;
                    itmal.RemoveAt(2 * i);
                    return;
                }
            }

            for (j = 6; j < al.Count; j++)
            {
                ArrayList itmal = (ArrayList)al[j];
                if (i >= (itmal.Count))
                {
                    i -= itmal.Count;
                }
                else
                {
                    itmal.RemoveAt(i);
                    return;
                }
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.setwin(lang);
            for (int i = 5; i < clabel.Count; i++)
            {
                frm3.setcombo(((ArrayList)clabel[i])[lang].ToString());
            }
            frm3.ShowDialog();

            if (frm3.Value != null)
            {
                ArrayList buf = (ArrayList)cardmemo[listBox1.SelectedIndex];
                additm(ref buf, frm3.Index + 5, frm3.Value);

                updview();
            }


        }


        public void additm(ref ArrayList al, int i, string val)
        {
            ArrayList itmal = (ArrayList)al[i];
            itmal.Add(val);
            if (i < 6)
                itmal.Add(val);
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                Form3 frm3 = new Form3();
                frm3.setwin(lang);
                frm3.setcombo(listView1.SelectedItems[0].SubItems[0].Text);
                frm3.settext(listView1.SelectedItems[0].SubItems[1].Text);
                frm3.ShowDialog();

                if (frm3.Value != null)
                {
                    ArrayList buf = (ArrayList)cardmemo[listBox1.SelectedIndex];
                    chgitm(ref buf, listView1.SelectedIndices[0], frm3.Value);

                    if (listView1.SelectedIndices[0] == 0)
                    {
                        updbox();
                    }
                    else
                    {
                        updview();
                    }
                }

            }
            else
                MessageBox.Show("请先选择要修改的某一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        public void chgitm(ref ArrayList al, int i, string val)
        {
            ///foreach (ArrayList itmal in al)
            ///{
            ///    if (i >= itmal.Count)
            //    {
            //        i -= itmal.Count;
            //    }
            //    else
            //    {
            //        itmal[i]=val;
            //        return;
            //    }
            ///}


            int j;
            for (j = 0; j < 6; j++)
            {
                ArrayList itmal = (ArrayList)al[j];
                if (i >= (itmal.Count / 2))
                {
                    i -= itmal.Count / 2;
                }
                else
                {
                    itmal[2 * i + lang] = val;
                    return;
                }
            }

            for (j = 6; j < al.Count; j++)
            {
                ArrayList itmal = (ArrayList)al[j];
                if (i >= (itmal.Count))
                {
                    i -= itmal.Count;
                }
                else
                {
                    itmal[i] = val;
                    return;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.setwin(lang);
            frm4.settext(clabel, lang);
            frm4.ShowDialog();

            if (frm4.Value != null)
            {
                cardmemo.Add(frm4.Value);
                imglist.Add(null);
                
                updbox();
                listBox1.SelectedIndex = cardmemo.Count - 1;
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cursel = listBox1.SelectedIndex;
            updview();
            updbtn();
        }

        public int showname(ref ArrayList cm)
        {
            foreach (ArrayList onecard in cm)
            {
                string cname = ((ArrayList)onecard[0])[lang].ToString();
                listBox1.Items.Add(cname);
            }
            //if (cursel < listBox1.Items.Count)
                //listBox1.SelectedIndex = cursel;

            return listBox1.SelectedIndex;

        }

        public string getname(int cs)
        {
            return ((ArrayList)((ArrayList)cardmemo[cs])[0])[lang].ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                ListBox.SelectedIndexCollection indx = listBox1.SelectedIndices;
                delcards(ref cardmemo, indx);
                delcards(ref imglist, indx);
                //cursel = 0;

                updbox();

            }
            else
                MessageBox.Show("请先选择要修改的某一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public void delcards(ref ArrayList al, ListBox.SelectedIndexCollection indice)
        {
            int i = 0;
            foreach (int ind in indice)
            {
                al.RemoveAt(ind - i);
                i++;
            }
        }


        public void updview()
        {
            listView1.Items.Clear();
            pictureBox1.Image = null;
            if (listBox1.SelectedIndex >= 0)
            {
                ArrayList buf = (ArrayList)cardmemo[listBox1.SelectedIndex];
                showcard(buf);
                updimg();
            }
        }

        public void updbox()
        {
            listBox1.Items.Clear();
            showname(ref cardmemo);
            updview();
            updbtn();
        }

        public void updimg()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = (Image)imglist[listBox1.SelectedIndex];
        }


        private void button8_Click(object sender, EventArgs e)
        {
            if (imglist.Count > 0)
            {
                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                //openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            imglist[listBox1.SelectedIndex] = Image.FromStream(myStream);
                            updimg();
                            myStream.Close();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (imglist.Count > 0)
            {
                imglist[listBox1.SelectedIndex] = null;
                updimg();
            }
        }

        public void updbtn()
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.Items.Count > 0)
            {
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }

        }

    }
}
