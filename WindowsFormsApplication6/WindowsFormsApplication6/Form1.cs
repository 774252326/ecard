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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public int i;
        public int c = 1;
        ArrayList indexmemo;
        [DataContract]
        class Card
        {
            /// <summary>
            /// //string[0] for english, string[1] for chinese
            /// </summary>\
            [DataMember(Order = 0, IsRequired = true)]
            internal string[] Name;

            [DataMember(Order = 1)]
            internal string[] Prefix;

            [DataMember(Order = 2)]
            internal string[] Suffix;

            [DataMember(Order = 3)]
            internal string[] Title;

            [DataMember(Order = 4)]
            internal string[] CompanyName;

            /// <summary>
            /// //string[2n] for english, string[2n+1] for chinese
            /// </summary>
            [DataMember]
            internal string[] Addr;



            /// <summary>
            /// //enable multiple value for each entry
            /// </summary>
            [DataMember]
            internal string[] CompanyWebsite;

            [DataMember]
            internal string[] Email;

            [DataMember]
            internal string[] MSN;

            [DataMember]
            internal string[] Skype;

            [DataMember]
            internal string[] CompanyPhone;

            [DataMember]
            internal string[] Directline;

            [DataMember]
            internal string[] Fax;

            [DataMember]
            internal string[] Mobile;
        }


        public string filePath;
        Card pp;
        Card cardLabel;
        //public Encoding ecg = Encoding.GetEncoding("gb2312");
        public Encoding ecg = Encoding.UTF8;

        public Form1()
        {
            InitializeComponent();
            //listBox1.Items.Add("a");
            ColumnHeader colHead;
            colHead = new ColumnHeader();
            colHead.Text = "item";
            listView1.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "value";
            listView1.Columns.Add(colHead);

            button1.Text = "change";
            button2.Text = "open";
            button3.Text = "save";

            setCardLabel();
        }


        private void setCardLabel()
        {
            cardLabel = new Card();

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





            //cardLabel.Addr = new Addr();

            //cardLabel.Addr.Company1 = "Company1 address";
            //cardLabel.Addr.Company2 = "Company2 address";
            //cardLabel.CompanyName = "Company Name";
            //cardLabel.CompanyWebsite = "Company Website";
            //cardLabel.Email = new email();

            //cardLabel.Email.Email1 = "Email1";
            //cardLabel.Email.Email2 = "Email2";
            //cardLabel.IM = new im();

            //cardLabel.IM.MSN = "MSN";
            //cardLabel.IM.skype = "skype";
            //cardLabel.Name = "Name";
            //cardLabel.PhoneNumber = new pn();

            //cardLabel.PhoneNumber.Company = "Company Phone Number";
            //cardLabel.PhoneNumber.Directline = "Directline";
            //cardLabel.PhoneNumber.Fax = "Fax";
            //cardLabel.PhoneNumber.Mobile1 = "Mobile Phone Number 1";
            //cardLabel.PhoneNumber.Mobile2 = "Mobile Phone Number 2";

            //cardLabel.Prefix = "Prefix";
            //cardLabel.Suffix = "Suffix";
            //cardLabel.Title = "Tilte";

            //showcard(label);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem.ListViewSubItem lvsi;

            if (listView1.SelectedItems.Count > 0)
            {
                MessageBox.Show(listView1.SelectedIndices[0].ToString() + ',' + indexmemo[listView1.SelectedIndices[0]].ToString());
                Form2 frm2 = new Form2();

                //frm2.Value = listView1.SelectedItems[0].SubItems[1].ToString();
                lvsi = listView1.SelectedItems[0].SubItems[1];
                frm2.set(lvsi.Text);
                frm2.ShowDialog();

                if (frm2.Value != null)
                {
                    //MessageBox.Show(frm2.Value);
                    //int ff = findidx(listView1.SelectedItems[0], frm2.Value);
                    chgitem(listView1.SelectedItems[0], (int)indexmemo[listView1.SelectedIndices[0]], frm2.Value);
                    //MessageBox.Show(ff.ToString());
                    listView1.Items.Clear();
                    showcard(pp);
                }
                else
                {
                    //MessageBox.Show("取消");
                    //delitem(listView1.SelectedItems[0]);
                    listView1.Items.Clear();
                    showcard(pp);
                }



            }
            else
                MessageBox.Show("请先选择要修改的某一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);





        }


        //private int delelement(


        private int delitem(ListViewItem lvi, int i)
        {
            string itm = lvi.SubItems[0].Text;
            string val = lvi.SubItems[1].Text;

            

            if (itm.Equals(cardLabel.Addr[c].ToString()))
            {
                ArrayList al = new ArrayList(pp.Addr);
                al.RemoveAt(i);
                al.RemoveAt(i - 2 * c);
                pp.Addr = (string[])al.ToArray(typeof(string));

            }


            if (itm.Equals(cardLabel.CompanyPhone[c].ToString()))
            {

                        ArrayList al = new ArrayList(pp.Email);
                        al.RemoveAt(i);
                        pp.Email = (string[])al.ToArray(typeof(string));
          
            }


            if (itm.Equals(cardLabel.CompanyWebsite[c].ToString()))
            {
            
                        ArrayList al = new ArrayList(pp.Email);
                        al.RemoveAt(i);
                        pp.Email = (string[])al.ToArray(typeof(string));
              
            }


            if (itm.Equals(cardLabel.Directline[c].ToString()))
            {
          
                        ArrayList al = new ArrayList(pp.Directline);
                        al.RemoveAt(i);
                        pp.Directline = (string[])al.ToArray(typeof(string));
           
            }

            if (itm.Equals(cardLabel.Email[c].ToString()))
            {

            

                        ArrayList al = new ArrayList(pp.Email);
                        al.RemoveAt(i);
                        pp.Email = (string[])al.ToArray(typeof(string));

            }

            if (itm.Equals(cardLabel.Fax[c].ToString()))
            {
     
                        ArrayList al = new ArrayList(pp.Fax);
                        al.RemoveAt(i);
                        pp.Fax = (string[])al.ToArray(typeof(string));
        
            }

            if (itm.Equals(cardLabel.Mobile[c].ToString()))
            {
             
                        ArrayList al = new ArrayList(pp.Mobile);
                        al.RemoveAt(i);
                        pp.Mobile = (string[])al.ToArray(typeof(string));
                
            }


            if (itm.Equals(cardLabel.MSN[c].ToString()))
            {
            
                        ArrayList al = new ArrayList(pp.MSN);
                        al.RemoveAt(i);
                        pp.MSN = (string[])al.ToArray(typeof(string));
            
            }

            if (itm.Equals(cardLabel.Skype[c].ToString()))
            {
      
                        ArrayList al = new ArrayList(pp.Skype);
                        al.RemoveAt(i);
                        pp.Skype = (string[])al.ToArray(typeof(string));
           
            }


            return i;
        }


        private int chgitem(ListViewItem lvi, int i, string nv)
        {
            string itm = lvi.SubItems[0].Text;
            string val = lvi.SubItems[1].Text;


            if (itm.Equals(cardLabel.CompanyName[c].ToString()))
            {
                pp.CompanyName[i] = nv;
            }


            if (itm.Equals(cardLabel.Name[c].ToString()))
            {
                pp.Name[i] = nv;
            }

            if (itm.Equals(cardLabel.Prefix[c].ToString()))
            {
                pp.Prefix[i] = nv;

            }

            if (itm.Equals(cardLabel.Suffix[c].ToString()))
            {
                pp.Suffix[i] = nv;

            }

            if (itm.Equals(cardLabel.Title[c].ToString()))
            {
                pp.Title[i] = nv;
            }





            if (itm.Equals(cardLabel.Addr[c].ToString()))
            {

                pp.Addr[i] = nv;

            }


            if (itm.Equals(cardLabel.CompanyPhone[c].ToString()))
            {

                pp.CompanyPhone[i] = nv;

            }


            if (itm.Equals(cardLabel.CompanyWebsite[c].ToString()))
            {

                pp.CompanyWebsite[i] = nv;

            }


            if (itm.Equals(cardLabel.Directline[c].ToString()))
            {

                pp.Directline[i] = nv;

            }

            if (itm.Equals(cardLabel.Email[c].ToString()))
            {

                pp.Email[i] = nv;

            }

            if (itm.Equals(cardLabel.Fax[c].ToString()))
            {

                pp.Fax[i] = nv;

            }

            if (itm.Equals(cardLabel.Mobile[c].ToString()))
            {

                pp.Mobile[i] = nv;

            }


            if (itm.Equals(cardLabel.MSN[c].ToString()))
            {

                pp.MSN[i] = nv;

            }

            if (itm.Equals(cardLabel.Skype[c].ToString()))
            {

                pp.Skype[i] = nv;

            }


            return i;
        }

        private int findidx(ListViewItem lvi, string nv)
        {
            string itm = lvi.SubItems[0].Text;
            string val = lvi.SubItems[1].Text;


            if (itm.Equals(cardLabel.CompanyName[c].ToString())
                )
            {
                pp.CompanyName[c] = nv;
                return c;
            }


            if (itm.Equals(cardLabel.Name[c].ToString()))
            {
                pp.Name[c] = nv;
                return c;
            }

            if (itm.Equals(cardLabel.Prefix[c].ToString()))
            {
                pp.Prefix[c] = nv;
                return c;
            }

            if (itm.Equals(cardLabel.Suffix[c].ToString()))
            {
                pp.Suffix[c] = nv;
                return c;
            }

            if (itm.Equals(cardLabel.Title[c].ToString()))
            {
                pp.Title[c] = nv;
                return c;
            }



            int i;

            if (itm.Equals(cardLabel.Addr[c].ToString()))
            {
                for (i = c; i < pp.Addr.Length; i += 2)
                {
                    if (val.Equals(pp.Addr[i].ToString()))
                    {
                        pp.Addr[i] = nv;
                        return i;
                    }
                }

            }


            if (itm.Equals(cardLabel.CompanyPhone[c].ToString()))
            {
                for (i = 0; i < pp.CompanyPhone.Length; i++)
                {
                    if (val.Equals(pp.CompanyPhone[i].ToString()))
                    {
                        pp.CompanyPhone[i] = nv;
                        return i;
                    }
                }
            }


            if (itm.Equals(cardLabel.CompanyWebsite[c].ToString()))
            {
                for (i = 0; i < pp.CompanyWebsite.Length; i++)
                {
                    if (val.Equals(pp.CompanyWebsite[i].ToString()))
                    {
                        pp.CompanyWebsite[i] = nv;
                        return i;
                    }
                }
            }


            if (itm.Equals(cardLabel.Directline[c].ToString()))
            {
                for (i = 0; i < pp.Directline.Length; i++)
                {
                    if (val.Equals(pp.Directline[i].ToString()))
                    {
                        pp.Directline[i] = nv;
                        return i;
                    }
                }
            }

            if (itm.Equals(cardLabel.Email[c].ToString()))
            {
                for (i = 0; i < pp.Email.Length; i++)
                {
                    if (val.Equals(pp.Email[i].ToString()))
                    {
                        pp.Email[i] = nv;
                        return i;
                    }
                }
            }

            if (itm.Equals(cardLabel.Fax[c].ToString()))
            {
                for (i = 0; i < pp.Fax.Length; i++)
                {
                    if (val.Equals(pp.Fax[i].ToString()))
                    {
                        pp.Fax[i] = nv;
                        return i;
                    }
                }
            }

            if (itm.Equals(cardLabel.Mobile[c].ToString()))
            {
                for (i = 0; i < pp.Mobile.Length; i++)
                {
                    if (val.Equals(pp.Mobile[i].ToString()))
                    {
                        pp.Mobile[i] = nv;
                        return i;
                    }
                }
            }


            if (itm.Equals(cardLabel.MSN[c].ToString()))
            {
                for (i = 0; i < pp.MSN.Length; i++)
                {
                    if (val.Equals(pp.MSN[i].ToString()))
                    {
                        pp.MSN[i] = nv;
                        return i;
                    }
                }
            }

            if (itm.Equals(cardLabel.Skype[c].ToString()))
            {
                for (i = 0; i < pp.Skype.Length; i++)
                {
                    if (val.Equals(pp.Skype[i].ToString()))
                    {
                        pp.Skype[i] = nv;
                        return i;
                    }
                }
            }


            return -1;
        }

        private void showcard(Card p3)
        {
            if (indexmemo != null)
                indexmemo.Clear();

            indexmemo = new ArrayList();


            ListViewItem lvi;


            lvi = new ListViewItem();
            lvi.SubItems[0].Text = cardLabel.Title[c].ToString();
            lvi.SubItems.Add(p3.Title[c].ToString());
            listView1.Items.Add(lvi);
            indexmemo.Add(c);

            lvi = new ListViewItem();
            lvi.SubItems[0].Text = cardLabel.Prefix[c].ToString();
            lvi.SubItems.Add(p3.Prefix[c].ToString());
            listView1.Items.Add(lvi);
            indexmemo.Add(c);

            lvi = new ListViewItem();
            lvi.SubItems[0].Text = cardLabel.Name[c].ToString();
            lvi.SubItems.Add(p3.Name[c].ToString());
            listView1.Items.Add(lvi);
            indexmemo.Add(c);

            lvi = new ListViewItem();
            lvi.SubItems[0].Text = cardLabel.Suffix[c].ToString();
            lvi.SubItems.Add(p3.Suffix[c].ToString());
            listView1.Items.Add(lvi);
            indexmemo.Add(c);

            lvi = new ListViewItem();
            lvi.SubItems[0].Text = cardLabel.CompanyName[c].ToString();
            lvi.SubItems.Add(p3.CompanyName[c].ToString());
            listView1.Items.Add(lvi);
            indexmemo.Add(c);

            int i;

            for (i = c; i < p3.Addr.Length; i += 2)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.Addr[c].ToString();
                lvi.SubItems.Add(p3.Addr[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
            }


            for (i = 0; i < p3.CompanyWebsite.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.CompanyWebsite[c].ToString();
                lvi.SubItems.Add(p3.CompanyWebsite[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
            }


            for (i = 0; i < p3.CompanyPhone.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.CompanyPhone[c].ToString();
                lvi.SubItems.Add(p3.CompanyPhone[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
            }

            for (i = 0; i < p3.Directline.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.Directline[c].ToString();
                lvi.SubItems.Add(p3.Directline[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
            }


            for (i = 0; i < p3.Fax.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.Fax[c].ToString();
                lvi.SubItems.Add(p3.Fax[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
            }

            for (i = 0; i < p3.Mobile.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.Mobile[c].ToString();
                lvi.SubItems.Add(p3.Mobile[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
            }


            for (i = 0; i < p3.Email.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.Email[c].ToString();
                lvi.SubItems.Add(p3.Email[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
            }


            for (i = 0; i < p3.MSN.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.MSN[c].ToString();
                lvi.SubItems.Add(p3.MSN[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
            }

            for (i = 0; i < p3.Skype.Length; i++)
            {
                lvi = new ListViewItem();
                lvi.SubItems[0].Text = cardLabel.Skype[c].ToString();
                lvi.SubItems.Add(p3.Skype[i].ToString());
                listView1.Items.Add(lvi);
                indexmemo.Add(i);
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
                        //MessageBox.Show(sr.ReadToEnd());
                        MemoryStream stream2 = new MemoryStream(ecg.GetBytes(sr.ReadToEnd()));
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Card));
                        pp = (Card)ser.ReadObject(stream2);

                        listBox1.Items.Add(pp.Name[c].ToString());
                        showcard(pp);

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
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Card));
            ser.WriteObject(stream1, pp);

            //Show the JSON output.
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string str = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(filePath, false, ecg);
            //StreamWriter sw = new StreamWriter(filePath);
            sw.Write(str);
            sw.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListViewItem.ListViewSubItem lvsi;

            if (listView1.SelectedItems.Count > 0)
            {


                //foreach (ListViewItem li in listView1.SelectedItems)
                {
                    //MessageBox.Show("取消");
                    //delitem(li);
                }

                delitem(listView1.SelectedItems[0], (int)indexmemo[listView1.SelectedIndices[0]]);
                listView1.Items.Clear();
                showcard(pp);




            }
            else
                MessageBox.Show("请先选择要修改的某一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }




    }
}
