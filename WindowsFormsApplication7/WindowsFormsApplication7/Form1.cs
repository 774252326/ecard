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



namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {


        [DataContract]
        class Card
        {
            /// <summary>
            /// //string[0] for english, string[1] for chinese           
            /// </summary>
            [DataMember(Order = 0, IsRequired = true)]
            internal string[] Name;

            [DataMember(Order = 1, IsRequired = true)]
            internal string[] Prefix;

            [DataMember(Order = 2)]
            internal string[] Suffix;

            [DataMember(Order = 3, IsRequired = true)]
            internal string[] Title;

            [DataMember(Order = 4, IsRequired = true)]
            internal string[] CompanyName;

            /// <summary>
            /// //string[2n] for english, string[2n+1] for chinese
            /// </summary>
            [DataMember(Order = 5)]
            internal string[] Addr;



            /// <summary>
            /// //enable multiple value for each entry
            /// </summary>
            [DataMember(Order = 6)]
            internal string[] CompanyWebsite;

            [DataMember(Order = 7)]
            internal string[] Email;

            [DataMember(Order = 8)]
            internal string[] MSN;

            [DataMember(Order = 9)]
            internal string[] Skype;

            [DataMember(Order = 10)]
            internal string[] CompanyPhone;

            [DataMember(Order = 11)]
            internal string[] Directline;

            [DataMember(Order = 12)]
            internal string[] Fax;

            [DataMember(Order = 13)]
            internal string[] Mobile;
        }

        [DataContract]
        class CardBook
        {
            [DataMember]
            internal Card[] C;
        }

        public const int lang = 1;

        public string filePath;
        //CardBook pcp;
        //Card cardLabel;
        //public Encoding ecg = Encoding.GetEncoding("gb2312");
        public Encoding ecg = Encoding.UTF8;
        //ArrayList buf;

        ArrayList clabel;
        ArrayList cardmemo;

        //ArrayList fplist;


        private int cursel = 0;

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
        }

        private void Card2Al(ref Card k, ref ArrayList al)
        {
            ArrayList itmal;

            itmal = new ArrayList();
            foreach (string val in k.Name)
            {
                itmal.Add(val);
            }
            al.Add(itmal);


            itmal = new ArrayList();
            foreach (string val in k.Prefix)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Suffix)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Title)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.CompanyName)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Addr)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.CompanyWebsite)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Email)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.MSN)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Skype)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.CompanyPhone)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Directline)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Fax)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Mobile)
            {
                itmal.Add(val);
            }
            al.Add(itmal);
            //itmal = new ArrayList();


        }


        private void Card2Al(Card k, ref ArrayList al)
        {
            ArrayList itmal;

            itmal = new ArrayList();
            foreach (string val in k.Name)
            {
                itmal.Add(val);
            }
            al.Add(itmal);


            itmal = new ArrayList();
            foreach (string val in k.Prefix)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Suffix)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Title)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.CompanyName)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Addr)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.CompanyWebsite)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Email)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.MSN)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Skype)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.CompanyPhone)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Directline)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Fax)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in k.Mobile)
            {
                itmal.Add(val);
            }
            al.Add(itmal);
            //itmal = new ArrayList();


        }
        private void CardBook2Al(ref CardBook cb, ref ArrayList al)
        {
            foreach (Card k in cb.C)
            {
                ArrayList cardal = new ArrayList();
                Card2Al(k, ref cardal);
                al.Add(cardal);
            }

        }

        private void Al2Card(ref Card k, ref ArrayList al)
        {
            //ArrayList itmal = (ArrayList)al[0];
            k.Name = (string[])((ArrayList)al[0]).ToArray(typeof(string));
            k.Prefix = (string[])((ArrayList)al[1]).ToArray(typeof(string));
            k.Suffix = (string[])((ArrayList)al[2]).ToArray(typeof(string));
            k.Title = (string[])((ArrayList)al[3]).ToArray(typeof(string));
            k.CompanyName = (string[])((ArrayList)al[4]).ToArray(typeof(string));
            k.Addr = (string[])((ArrayList)al[5]).ToArray(typeof(string));
            k.CompanyWebsite = (string[])((ArrayList)al[6]).ToArray(typeof(string));
            k.Email = (string[])((ArrayList)al[7]).ToArray(typeof(string));
            k.MSN = (string[])((ArrayList)al[8]).ToArray(typeof(string));
            k.Skype = (string[])((ArrayList)al[9]).ToArray(typeof(string));
            k.CompanyPhone = (string[])((ArrayList)al[10]).ToArray(typeof(string));
            k.Directline = (string[])((ArrayList)al[11]).ToArray(typeof(string));
            k.Fax = (string[])((ArrayList)al[12]).ToArray(typeof(string));
            k.Mobile = (string[])((ArrayList)al[13]).ToArray(typeof(string));
            //k.Name = (string[])((ArrayList)al[0]).ToArray(typeof(string));
        }


        private void Al2Card(ref Card k, ArrayList al)
        {
            //ArrayList itmal = (ArrayList)al[0];
            k.Name = (string[])((ArrayList)al[0]).ToArray(typeof(string));
            k.Prefix = (string[])((ArrayList)al[1]).ToArray(typeof(string));
            k.Suffix = (string[])((ArrayList)al[2]).ToArray(typeof(string));
            k.Title = (string[])((ArrayList)al[3]).ToArray(typeof(string));
            k.CompanyName = (string[])((ArrayList)al[4]).ToArray(typeof(string));
            k.Addr = (string[])((ArrayList)al[5]).ToArray(typeof(string));
            k.CompanyWebsite = (string[])((ArrayList)al[6]).ToArray(typeof(string));
            k.Email = (string[])((ArrayList)al[7]).ToArray(typeof(string));
            k.MSN = (string[])((ArrayList)al[8]).ToArray(typeof(string));
            k.Skype = (string[])((ArrayList)al[9]).ToArray(typeof(string));
            k.CompanyPhone = (string[])((ArrayList)al[10]).ToArray(typeof(string));
            k.Directline = (string[])((ArrayList)al[11]).ToArray(typeof(string));
            k.Fax = (string[])((ArrayList)al[12]).ToArray(typeof(string));
            k.Mobile = (string[])((ArrayList)al[13]).ToArray(typeof(string));
            //k.Name = (string[])((ArrayList)al[0]).ToArray(typeof(string));
        }

        private void Al2CardBook(ref CardBook cb, ArrayList al)
        {
            //int nc=al.Count;
            //int i;
            Card ca;
            //ArrayList cardal;
            //ArrayList q=new ArrayList();
            List<Card> w = new List<Card>();
            //w.Add(ca);
            //CardBook cb=new CardBook();


            foreach (ArrayList cardal in al)
            //for (i = 0; i < nc; i++)
            {
                //cardal = (ArrayList)al[i];
                ca = new Card();
                Al2Card(ref ca, cardal);
                w.Add(ca);

            }

            cb.C = (Card[])(w.ToArray());
            //return cb;
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
                    button4.Text = "remove";
                    button5.Text = "add item";
                    button6.Text = "change";
                    button7.Text = "delete";
                    break;
                case 1:
                    listView1.Columns[0].Text = "项";
                    listView1.Columns[1].Text = "值";
                    button1.Text = "添加";
                    button2.Text = "打开";
                    button3.Text = "保存";
                    button4.Text = "移除";
                    button5.Text = "添加项";
                    button6.Text = "修改";
                    button7.Text = "删除";
                    break;
                default:
                    break;
            }
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

            Card2Al(ref cardLabel, ref labelAl);
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

                        StreamReader sr = new StreamReader(filePath, ecg);
                        MemoryStream stream2 = new MemoryStream(ecg.GetBytes(sr.ReadToEnd()));
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(CardBook));
                        CardBook pcp = (CardBook)ser.ReadObject(stream2);

                        CardBook2Al(ref pcp, ref cardmemo);
                        cursel = cardmemo.Count - 1;

                        updbox();

                        sr.Close();
                        stream2.Close();
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
            Al2CardBook(ref ppp, cardmemo);


            MemoryStream stream1 = new MemoryStream();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(CardBook));
            ser.WriteObject(stream1, ppp);


            //Show the JSON output.
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string str = sr.ReadToEnd();
            sr.Close();

            StreamWriter sw = new StreamWriter(filePath, false, ecg);
            sw.Write(str);
            sw.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ArrayList buf = (ArrayList)cardmemo[cursel];
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
                delitm(ref al, ind - i);
                i++;
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
                ArrayList buf = (ArrayList)cardmemo[cursel];
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
                    ArrayList buf = (ArrayList)cardmemo[cursel];
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
                cursel = cardmemo.Count - 1;
                updbox();
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cursel = listBox1.SelectedIndex;
            updview();
        }

        public int showname(ref ArrayList cm)
        {
            foreach (ArrayList onecard in cm)
            {
                string cname = ((ArrayList)onecard[0])[lang].ToString();
                listBox1.Items.Add(cname);
            }
            if (cursel < listBox1.Items.Count)
                listBox1.SelectedIndex = cursel;

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

                delcards(ref cardmemo, listBox1.SelectedIndices);
                cursel = 0;

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
            if (cursel >= 0)
            {
                ArrayList buf = (ArrayList)cardmemo[cursel];
                showcard(buf);
            }
        }

        public void updbox()
        {
            listBox1.Items.Clear();
            cursel = showname(ref cardmemo);
            updview();
        }

    }
}
