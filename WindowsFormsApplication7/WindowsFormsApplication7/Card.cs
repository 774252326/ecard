using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.IO;

namespace WindowsFormsApplication7
{
    class Card
    {
        public string[] Name { get; set; }

        public string[] Prefix { get; set; }

        public string[] Suffix { get; set; }

        public string[] Title { get; set; }

        public string[] CompanyName { get; set; }

        /// <summary>
        /// //string[2n] for english, string[2n+1] for chinese
        /// </summary>
        public string[] Addr { get; set; }



        /// <summary>
        /// //enable multiple value for each entry
        /// </summary>
        public string[] CompanyWebsite { get; set; }

        public string[] Email { get; set; }

        public string[] MSN { get; set; }

        public string[] Skype { get; set; }

        public string[] CompanyPhone { get; set; }

        public string[] Directline { get; set; }

        public string[] Fax { get; set; }

        public string[] Mobile { get; set; }

        public byte[] Logo { get; set; }

        public void Card2Al(ref ArrayList al)
        {
            ArrayList itmal;

            itmal = new ArrayList();
            foreach (string val in this.Name)
            {
                itmal.Add(val);
            }
            al.Add(itmal);


            itmal = new ArrayList();
            foreach (string val in this.Prefix)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.Suffix)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.Title)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.CompanyName)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.Addr)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.CompanyWebsite)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.Email)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.MSN)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.Skype)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.CompanyPhone)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.Directline)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.Fax)
            {
                itmal.Add(val);
            }
            al.Add(itmal);

            itmal = new ArrayList();
            foreach (string val in this.Mobile)
            {
                itmal.Add(val);
            }
            al.Add(itmal);
            //itmal = new ArrayList();


        }


        public void Al2Card(ArrayList al)
        {
            //ArrayList itmal = (ArrayList)al[0];
            this.Name = (string[])((ArrayList)al[0]).ToArray(typeof(string));
            this.Prefix = (string[])((ArrayList)al[1]).ToArray(typeof(string));
            this.Suffix = (string[])((ArrayList)al[2]).ToArray(typeof(string));
            this.Title = (string[])((ArrayList)al[3]).ToArray(typeof(string));
            this.CompanyName = (string[])((ArrayList)al[4]).ToArray(typeof(string));
            this.Addr = (string[])((ArrayList)al[5]).ToArray(typeof(string));
            this.CompanyWebsite = (string[])((ArrayList)al[6]).ToArray(typeof(string));
            this.Email = (string[])((ArrayList)al[7]).ToArray(typeof(string));
            this.MSN = (string[])((ArrayList)al[8]).ToArray(typeof(string));
            this.Skype = (string[])((ArrayList)al[9]).ToArray(typeof(string));
            this.CompanyPhone = (string[])((ArrayList)al[10]).ToArray(typeof(string));
            this.Directline = (string[])((ArrayList)al[11]).ToArray(typeof(string));
            this.Fax = (string[])((ArrayList)al[12]).ToArray(typeof(string));
            this.Mobile = (string[])((ArrayList)al[13]).ToArray(typeof(string));
            //k.Name = (string[])((ArrayList)al[0]).ToArray(typeof(string));
        }

        public void Logo2Img(ref Image img)
        {
            if (this.Logo == null)
            {
                img = null;
            }
            else
            {
                MemoryStream ms = new MemoryStream(this.Logo);
                img = Image.FromStream(ms, true);//生成图片
                ms.Close();
            }
        }

        public void Img2Logo(Image img)
        {
            if (img == null)
            {
                this.Logo = null;
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                Bitmap oBitmap = new Bitmap(img);
                oBitmap.Save(ms, img.RawFormat);
                ms.Position = 0;
                this.Logo = new byte[ms.Length];
                ms.Read(this.Logo, 0, Convert.ToInt32(ms.Length));
                ms.Flush();
                ms.Close();
            }
        }




    }
}
