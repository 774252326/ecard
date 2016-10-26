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


using Phychips.Rcp;
using Phychips.Helper;


using System.Threading;

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
        //private int cursel = 0;

        ArrayList clabel;
        ArrayList cardmemo;

        //ArrayList fplist;
        ArrayList imglist;
        string[] errstr1 = new[] { "the selected item cannot be removed", "此项不可删除" };
        string[] errstr2 = new[] { "no item selected", "未选中项" };
        string[] savstr1 = new[] { " saved", " 已保存" };
        //string[] qtitle = new[] { "name to query:", "输入要查找的姓名：" };
        string[] qtitle = new[] { "who:", "找谁：" };
        public static String recstr;

        private int discard;

        ArrayList flashmap;

        public Form5 f5;

        public Form1(Form5 nf)
        {
            InitializeComponent();
            InitialEnvironment();
            f5 = nf;
        }

        public Form1()
        {
            InitializeComponent();
            InitialEnvironment();            
        }

        private void InitialEnvironment()
        {
            ColumnHeader colHead;

            colHead = new ColumnHeader();
            listView1.Columns.Add(colHead);
            colHead = new ColumnHeader();
            listView1.Columns.Add(colHead);

            setwin(lang);
            updbtn();

            clabel = new ArrayList();
            setCardLabel(ref clabel);

            cardmemo = new ArrayList();
            //fplist = new ArrayList();
            imglist = new ArrayList();
            flashmap = new ArrayList();
        }

        //set button and list label
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
                    button10.Text = "import";
                    button11.Text = "import from card";
                    button12.Text = "clear card";
                    button14.Text = "find";
                    button18.Text = "export to card";
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
                    button10.Text = "导入";
                    button11.Text = "从卡导入";
                    button12.Text = "清除数据";
                    button14.Text = "查找";
                    button18.Text = "导出到卡";
                    break;
                default:
                    break;
            }
        }

        //setup item label in list 
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


        #region operation response
        //add name card
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
        //open file
        private void button2_Click(object sender, EventArgs e)
        {
            readfile(false);
        }
        //save file
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
                else
                {
                    return;
                }

            }

            CardBook ppp = new CardBook();
            ppp.Al2CardBook(cardmemo);
            ppp.Al2CardBookLogo(imglist);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(ppp));
            MessageBox.Show(filePath + savstr1[lang]);
            updbtn();
        }
        //delete items
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ArrayList buf = (ArrayList)cardmemo[listBox1.SelectedIndex];
                delitms(ref buf, listView1.SelectedIndices);
                updview();
            }
            else
                MessageBox.Show(errstr2[lang], "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //add item
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
        //change item
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
                MessageBox.Show(errstr2[lang], "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        //Delete name card
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
                MessageBox.Show(errstr2[lang], "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //add or change logo image
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
        //delete image logo
        private void button9_Click(object sender, EventArgs e)
        {
            if (imglist.Count > 0)
            {
                imglist[listBox1.SelectedIndex] = null;
                updimg();
            }
        }
        //import file
        private void button10_Click(object sender, EventArgs e)
        {
            readfile(true);
        }
        //change selection
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cursel = listBox1.SelectedIndex;
            updview();
            updbtn();
        }
        #endregion

        #region show data
        //update listview 
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
        //update listbox
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
        //get name string from arraylist
        public string getname(int cs)
        {
            return ((ArrayList)((ArrayList)cardmemo[cs])[0])[lang].ToString();
        }
        //update listview and picturebox
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
        //update whole windows
        public void updbox()
        {
            listBox1.Items.Clear();
            showname(ref cardmemo);
            updview();
            updbtn();
        }
        //update picturebox
        public void updimg()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = (Image)imglist[listBox1.SelectedIndex];
        }
        //update GUI button state
        public void updbtn()
        {
            tabControl1.SelectedTab.Text = filePath;
            if (listBox1.SelectedIndex >= 0 && listBox1.Items.Count > 0)
            {
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }

        }
        #endregion

        #region change data
        //add item
        public void additm(ref ArrayList al, int i, string val)
        {
            ArrayList itmal = (ArrayList)al[i];
            itmal.Add(val);
            if (i < 6)
                itmal.Add(val);
        }
        //delete items
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
                else
                {
                    MessageBox.Show(errstr1[lang]);
                }
            }
        }
        //delete one item
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
        //change item
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
        //Delete name card
        public void delcards(ref ArrayList al, ListBox.SelectedIndexCollection indice)
        {
            int i = 0;
            foreach (int ind in indice)
            {
                al.RemoveAt(ind - i);
                i++;
            }
        }
        //read data
        public void readfile(bool append)
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
                        CardBook c1 = JsonConvert.DeserializeObject<CardBook>(File.ReadAllText(openFileDialog1.FileName));
                        if (!append)
                        {
                            cardmemo.Clear();
                            imglist.Clear();
                            filePath = openFileDialog1.FileName;
                        }
                        c1.CardBook2Al(ref cardmemo);
                        c1.CardBookLogo2Al(ref imglist);

                        updbox();
                        listBox1.SelectedIndex = cardmemo.Count - 1;
                        myStream.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        #endregion




        //a = 1;
        //b = 0;
        //le = 7;
        //recstr = "";
        //f5.setReadCmd(a, b, Convert.ToUInt16(le));
        //f5.sendcmd();
        //recstr = f5.getReply();
        //MessageBox.Show(recstr);
        //button18.Enabled = false;


        //b = 2;
        //le = 247;
        //recstr = "";
        //f5.setReadCmd(a, b, Convert.ToUInt16(le));
        //f5.sendcmd();
        //recstr = f5.getReply();
        //MessageBox.Show(recstr);
        ////////////////////////////////////////
        //f5.setEraseCmd(1);
        //f5.button4.PerformClick();
        //////////////////////////////////////////////


        //ArrayList buf = (ArrayList)cardmemo[0];
        //Card c = new Card();
        //c.Al2Card(buf);
        //c.Img2Logo((Image)imglist[0]);
        //String jstr = JsonConvert.SerializeObject(c);


        //byte[] jbyte = Encoding.Default.GetBytes(jstr);
        //byte[] nbyte = Encoding.Default.GetBytes(c.Name[lang]);
        //byte nlen = Convert.ToByte(nbyte.Length);
        //ushort jlen = Convert.ToUInt16(jbyte.Length);

        //ByteBuilder bb = new ByteBuilder(f5.cardheader);
        //bb.Append(nlen);
        //bb.Append(jlen);
        //bb.Append(nbyte);



        //f5.setWriteCmd(1, 0, bb.GetByteArray());

        //f5.button4.PerformClick();
        ////////////////////////////////////////////////////////////////////

        //ArrayList buf = (ArrayList)cardmemo[0];
        //Card c = new Card();
        //c.Al2Card(buf);
        //c.Img2Logo((Image)imglist[0]);
        //String jstr = JsonConvert.SerializeObject(c);


        //byte[] jbyte = Encoding.Default.GetBytes(jstr);


        //ByteBuilder bb = new ByteBuilder(jbyte);
        //int idx = 0;
        //int maxl = 240;
        //while (bb.Length - idx > maxl)
        //{
        //    f5.setWriteCmd(1, Convert.ToUInt16(idx + 256), bb.GetByteArray(idx, maxl));
        //    f5.button4.PerformClick();
        //    idx += maxl;
        //}

        //f5.setWriteCmd(1, Convert.ToUInt16(idx + 256), bb.GetByteArray(idx, bb.Length - idx));
        //f5.button4.PerformClick();
        /////////////////////////////////////////////////////////////////////////////////////////////////


        private void button18_Click(object sender, EventArgs e)
        {

            Win32.HiPerfTimer pt = new Win32.HiPerfTimer();
            pt.Start();

            for (int i = 0; i < cardmemo.Count; i++)
            {
                ArrayList buf = (ArrayList)cardmemo[i];
                Card c = new Card();
                c.Al2Card(buf);
                c.Img2Logo((Image)imglist[i]);
                writeCardToBlock(c, Convert.ToByte(i));
            }

            pt.Stop();

            MessageBox.Show("all card file saved to card\nelapsed time = " + pt.Duration.ToString() + " s");


        }


        private void button11_Click(object sender, EventArgs e)
        {
            Win32.HiPerfTimer pt = new Win32.HiPerfTimer();     // 创建新的 HiPerfTimer 对象
            pt.Start();                             // 启动计时器

            for (byte ba = 0; ba < f5.BlockNumber; ba++)
            {
                readblock(ba);
            }

            updbox();
            listBox1.SelectedIndex = cardmemo.Count - 1;

            pt.Stop();                              // 停止计时器

            MessageBox.Show(cardmemo.Count.ToString() + " records imported!\nelapsed time = " + pt.Duration.ToString() + " s");

        }


        private byte[] decodeReadReply(String str)
        {
            byte[] rpb = HexEncoding.GetBytes(str, out discard);
            ByteBuilder bb = new ByteBuilder(rpb);
            return bb.GetByteArray(5, bb.Length - 8);
        }

        private byte[] readsth(byte blockaddr, ushort addr, ushort len)
        {
            ushort lenlimit = f5.MaxReadLength;
            ushort naddr = addr;
            ByteBuilder bb = new ByteBuilder();
            String tmp;
            while (len > lenlimit)
            {
                f5.setReadCmd2(blockaddr, addr, lenlimit);
                //f5.setReadCmd(blockaddr, addr, lenlimit);
                //f5.sendcmd();
                tmp = f5.getReply();
                bb.Append(decodeReadReply(tmp));

                naddr += lenlimit;
                if (naddr < addr)
                {
                    blockaddr++;
                }
                addr = naddr;
                len -= lenlimit;
            }

            f5.setReadCmd2(blockaddr, addr, len);
            //f5.setReadCmd(blockaddr, addr, len);
            //f5.sendcmd();
            tmp = f5.getReply();
            bb.Append(decodeReadReply(tmp));

            return bb.GetByteArray();
        }

        private bool readblock(byte ba)
        {
            ArrayList cardbuf;
            Image logobuf = null;

            byte[] rpb = readsth(ba, 0, 7);

            if (rpb[0] == f5.cardheader[0] && rpb[1] == f5.cardheader[1] && rpb[2] == f5.cardheader[2] && rpb[3] == f5.cardheader[3])
            {
                byte namelen = rpb[4];
                ushort jsonlen = rpb[5];
                jsonlen *= 256;
                jsonlen += rpb[6];

                byte[] namebyte = readsth(ba, 7, Convert.ToUInt16(namelen));
                byte[] jsonbyte = readsth(ba, 256, jsonlen);

                String strr = Encoding.Default.GetString(jsonbyte);
                Card c1 = JsonConvert.DeserializeObject<Card>(strr);

                cardbuf = new ArrayList();
                logobuf = null;
                c1.Card2Al(ref cardbuf);
                c1.Logo2Img(ref logobuf);
                cardmemo.Add(cardbuf);
                imglist.Add(logobuf);
                return true;
            }
            return false;

        }

        private void erasesth(byte blockaddr)
        {
            //do
            //{
            f5.setEraseCmd2(blockaddr);
            //f5.setEraseCmd(blockaddr);
            //f5.sendcmd();
            recstr = f5.getReply();
            //} while (recstr != "BB01860001007E");
        }

        private void writesth256(byte blockaddr, byte addr256, byte[] content)
        {
            ushort addr = Convert.ToUInt16(addr256);
            addr *= 256;

            int lenlimit = f5.MaxWriteLength;
            if (content.Length <= lenlimit)
            {
                f5.setWriteCmd2(blockaddr, addr, content);
                //f5.setWriteCmd(blockaddr, addr, content);
                //f5.sendcmd();
                recstr = f5.getReply();
            }
            else
            {
                ByteBuilder bb = new ByteBuilder(content);
                f5.setWriteCmd2(blockaddr, addr, bb.GetByteArray(0, lenlimit));
                //f5.setWriteCmd(blockaddr, addr, bb.GetByteArray(0, lenlimit));
                //f5.sendcmd();
                recstr = f5.getReply();

                addr += Convert.ToUInt16(lenlimit);

                int len2 = ( bb.Length > 256 )?(256  - lenlimit):(bb.Length - lenlimit);
                f5.setWriteCmd2(blockaddr, addr, bb.GetByteArray(lenlimit, len2));
                //f5.setWriteCmd(blockaddr, addr, bb.GetByteArray(lenlimit, len2));
                //f5.sendcmd();
                recstr = f5.getReply();

            }
        }


        private void writesth(byte blockaddr, byte addr256, byte[] content)
        {
            if (content.Length <= 256)
            {
                writesth256(blockaddr, addr256, content);
            }
            else
            {
                ByteBuilder bb = new ByteBuilder(content);
                byte nba, na256;
                int i;
                for (i = 0; i < bb.Length - 256; i += 256)
                {
                    writesth256(blockaddr, addr256, bb.GetByteArray(i, 256));
                    na256 = addr256;
                    addr256 += 1;
                    if (addr256 < na256)
                    {
                        blockaddr += 1;
                    }
                }

                writesth256(blockaddr, addr256, bb.GetByteArray(i, bb.Length - i));

            }

        }


        private bool writeCardToBlock(Card c, byte blockaddr)
        {
            String jstr = JsonConvert.SerializeObject(c);

            byte[] jbyte = Encoding.Default.GetBytes(jstr);
            byte[] nbyte = Encoding.Default.GetBytes(c.Name[lang]);

            if (nbyte.Length > f5.MaxNameLength)
            {
                MessageBox.Show("name too long");
                return false;
            }
            if (jbyte.Length > f5.MaxJsonLength)
            {
                MessageBox.Show("card file too big");
                return false;
            }

            byte nlen = Convert.ToByte(nbyte.Length);
            ushort jlen = Convert.ToUInt16(jbyte.Length);

            
            erasesth(blockaddr);
            ByteBuilder bb = new ByteBuilder(f5.cardheader);
            bb.Append(nlen);
            bb.Append(jlen);
            bb.Append(nbyte);

            writesth256(blockaddr, 0, bb.GetByteArray());
            writesth(blockaddr, 1, jbyte);
            return true;
        }


        private void button12_Click(object sender, EventArgs e)
        {
            Win32.HiPerfTimer pt = new Win32.HiPerfTimer();
            pt.Start();
            for (byte i = 0; i < f5.BlockNumber; i++)
            {
                erasesth(i);
            }
            pt.Stop();
            MessageBox.Show("all clear\nelapsed time = " + pt.Duration.ToString() + " s");

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Win32.HiPerfTimer pt = new Win32.HiPerfTimer();
            pt.Start();
            //int i = 0;
            //for (int i = 0; i < cardmemo.Count; i++)
            for (int i = 0; i < 128; i++)
            {


                ArrayList buf = (ArrayList)cardmemo[0];
                Card c = new Card();
                c.Al2Card(buf);
                c.Img2Logo((Image)imglist[0]);

                c.Name[0] += i.ToString();
                c.Name[1] += i.ToString();

                String jstr = JsonConvert.SerializeObject(c);

                byte[] jbyte = Encoding.Default.GetBytes(jstr);

                String tmpp = Encoding.Default.GetString(jbyte);

                bool aaa = jstr.Equals(tmpp);

                byte[] nbyte = Encoding.Default.GetBytes(c.Name[lang]);

                if (nbyte.Length > 249)
                {
                    MessageBox.Show("name too long");
                    continue;
                    //return;
                }
                if (jbyte.Length > 256 * 255)
                {
                    MessageBox.Show("card file too big");
                    continue;
                    //return;
                }

                byte nlen = Convert.ToByte(nbyte.Length);
                ushort jlen = Convert.ToUInt16(jbyte.Length);

                byte baddr = Convert.ToByte(i);
                erasesth(baddr);
                ByteBuilder bb = new ByteBuilder(f5.cardheader);
                bb.Append(nlen);
                bb.Append(jlen);
                bb.Append(nbyte);

                writesth256(baddr, 0, bb.GetByteArray());
                byte[] b1 = readsth(baddr, 0, Convert.ToUInt16(bb.Length));

                if (!cpb(b1, bb.GetByteArray()))
                {
                    MessageBox.Show("0");
                }

                ByteBuilder bbb = new ByteBuilder(jbyte);
                for (int j = 0; j < bbb.Length; j += 256)
                {
                    int lle = bbb.Length - j;
                    if (lle > 256)
                        lle = 256;
                    writesth256(baddr, Convert.ToByte(j / 256 + 1), bbb.GetByteArray(j, lle));
                    byte[] b2 = readsth(baddr, Convert.ToUInt16(j + 256), Convert.ToUInt16(lle));
                    if (!cpb(b2, bbb.GetByteArray(j, lle)))
                    {
                        MessageBox.Show(j.ToString());
                    }
                }

                //writesth(baddr, 1, jbyte);
            }

            pt.Stop();

            MessageBox.Show("all card file saved to card\nelapsed time = " + pt.Duration.ToString() + " s");


        }


        public bool cpb(byte[] b1, byte[] b2)
        {
            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i] != b2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.setwin(lang);
            frm3.setcombo(qtitle[lang]);
            frm3.settext("");
            frm3.ShowDialog();
            if (frm3.Value != null)
            {
                byte[] nbyte = Encoding.Default.GetBytes(frm3.Value);
                byte baddr = queryblock(nbyte);
                if (baddr < f5.BlockNumber)
                {
                    MessageBox.Show("block addresss is " + baddr.ToString());
                    readblock(baddr);
                    updbox();
                    listBox1.SelectedIndex = cardmemo.Count - 1;
                }
                else
                {
                    MessageBox.Show("404");
                }
            }

        }

        private byte queryblock(byte[] content)
        {
            if (content.Length > 0 && content.Length < f5.MaxNameLength)
            {
                f5.setQueryCmd2(content);
                //f5.setQueryCmd(content);
                //f5.sendcmd();
                String tmp = f5.getReply();
                byte[] rpb = HexEncoding.GetBytes(tmp, out discard);
                if (rpb[2] == f5.querycode && rpb[4] == 1)
                {
                    return rpb[5];
                }
            }
            return 0xFF;
        }


        private void queryspace()
        {
            f5.setSpaceCmd2();
            String tmp = f5.getReply();
            byte[] bt = decodeReadReply(tmp);
            ArrayList tmpmap = new ArrayList();
            for (int i = 0; i < bt.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    byte onebit = Convert.ToByte(2^j);
                    onebit &= bt[i];
                    if (onebit != 0)
                    {
                        tmpmap.Add(j + i * 8);
                    }

                }
            }

            flashmap=tmpmap;

        }


    }
}
