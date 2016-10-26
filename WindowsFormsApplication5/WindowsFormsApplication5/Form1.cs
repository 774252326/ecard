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

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        [DataContract]
        class Person
        {
            [DataMember]
            internal string name;

            [DataMember]
            internal int age;
        }

        [DataContract]
        class Addr
        {
            [DataMember]
            internal string Company1;

            [DataMember]
            internal string Company2;
        }

        [DataContract]
        class pn
        {
            [DataMember]
            internal string Company;

            [DataMember]
            internal string Mobile1;

            [DataMember]
            internal string Mobile2;

            [DataMember]
            internal string Directline;

            [DataMember]
            internal string Fax;
        }

        [DataContract]
        class email
        {
            [DataMember]
            internal string Email1;
            [DataMember]
            internal string Email2;
        }

        [DataContract]
        class im
        {
            [DataMember]
            internal string skype;
            [DataMember]
            internal string MSN;
        }



        [DataContract]
        class Card
        {
            [DataMember]
            internal string Prefix;

            [DataMember]
            internal string Name;

            [DataMember]
            internal string Suffix;

            [DataMember]
            internal string Title;

            [DataMember]
            internal string CompanyName;

            [DataMember]
            internal string CompanyWebsite;

            [DataMember]
            internal Addr Addr;

            [DataMember]
            internal pn PhoneNumber;

            [DataMember]
            internal email Email;

            [DataMember]
            internal im IM;
        }


        public Form1()
        {
            InitializeComponent();

            button1.Text = "打开";
            set1();
        }


        private void showcard(Card p3)
        {
            prefix.Text = p3.Prefix;
            name.Text = p3.Name;
            suffix.Text = p3.Suffix;
            title.Text = p3.Title;
            companyname.Text = p3.CompanyName;
            companywebsite.Text = p3.CompanyWebsite;
            addrcompany1.Text = p3.Addr.Company1;
            addrcompany2.Text = p3.Addr.Company2;
            pncompany.Text = p3.PhoneNumber.Company;
            pnmobile1.Text = p3.PhoneNumber.Mobile1;
            pnmobile2.Text = p3.PhoneNumber.Mobile2;
            pndirectline.Text = p3.PhoneNumber.Directline;
            pnfax.Text = p3.PhoneNumber.Fax;
            email1.Text = p3.Email.Email1;
            email2.Text = p3.Email.Email2;
            imskype.Text = p3.IM.skype;
            immsn.Text = p3.IM.MSN;
        }

        private void set1()
        {
            Card label = new Card();
            label.Addr = new Addr();

            label.Addr.Company1 = "Company1 address";
            label.Addr.Company2 = "Company2 address";
            label.CompanyName = "Company Name";
            label.CompanyWebsite = "Company Website";
            label.Email = new email();

            label.Email.Email1 = "Email1";
            label.Email.Email2 = "Email2";
            label.IM = new im();

            label.IM.MSN = "MSN";
            label.IM.skype = "skype";
            label.Name = "Name";
            label.PhoneNumber = new pn();

            label.PhoneNumber.Company = "Company Phone Number";
            label.PhoneNumber.Directline = "Directline";
            label.PhoneNumber.Fax = "Fax";
            label.PhoneNumber.Mobile1 = "Mobile Phone Number 1";
            label.PhoneNumber.Mobile2 = "Mobile Phone Number 2";

            label.Prefix = "Prefix";
            label.Suffix = "Suffix";
            label.Title = "Tilte";

            showcard(label);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Person p = new Person();
            //p.name = "John";
            //p.age = 42;

            //MemoryStream stream1 = new MemoryStream();

            //Serialize the Person object to a memory stream using DataContractJsonSerializer.
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
            //ser.WriteObject(stream1, p);

            //Show the JSON output.
            //stream1.Position = 0;
            //StreamReader sr = new StreamReader(stream1);

            //Console.Write("JSON form of Person object: ");
            //Console.WriteLine(sr.ReadToEnd());



            //Deserialize the JSON back into a new Person object.
            //stream1.Position = 0;
            //Person p2 = (Person)ser.ReadObject(stream1);
            //Person p2 = (Person)ser.ReadObject(sr.BaseStream);
            //byte[] by = new byte(str);


            //Show the results.
            //Console.Write("Deserialized back, got name=");
            //Console.Write(p2.name);
            //Console.Write(", age=");
            //Console.WriteLine(p2.age);


            //Console.WriteLine("Press <ENTER> to terminate the program.");
            //Console.ReadLine();
            //////////////////////////////////////////////////////////////////////////////////////

            //StreamReader sr = new StreamReader("j.txt",System.Text.Encoding.GetEncoding("gb2312"));
            //String str;
            //str = sr.ReadToEnd();
            //MessageBox.Show(str);
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
            //MemoryStream stream2 = new MemoryStream(Encoding.UTF8.GetBytes(str));
            //Person p2 = (Person)ser.ReadObject(stream2);

            //MessageBox.Show("Deserialized back, got name=" + p2.name + ", 年龄=" + p2.age);


            ///////////////////////////////////////////////////////////////


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
                        using (myStream)
                        {
                            // Insert code to read the stream here.

                            ///////////////////////////////////////////////////////////////////////////
                            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
                            //Person p2 = (Person)ser.ReadObject(myStream);
                            //MessageBox.Show("Deserialized back, got name=" + p2.name + ", age=" + p2.age1);
                            //////////////////////////////////////////////////////////////////////////////



                            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Card));
                            //Card p4 = (Card)ser.ReadObject(myStream);
                            //showcard(p4);
                        }


                        StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.GetEncoding("gb2312"));
                        //MessageBox.Show(sr.ReadToEnd());
                        MemoryStream stream2 = new MemoryStream(Encoding.UTF8.GetBytes(sr.ReadToEnd()));
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Card));
                        Card p4 = (Card)ser.ReadObject(stream2);
                        showcard(p4);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }



        }
    }
}
