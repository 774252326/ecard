using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

using System.IO;

namespace WindowsFormsApplication9
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            FileStream fs = new FileStream(@"bunny_pancake.jpg", FileMode.Open, FileAccess.ReadWrite);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((int)fs.Length);

            fs.Close();
            br.Close();



            //Class1 movie = new Class1
            //{
            //    Name = Encoding.GetEncoding("ISO-8859-1").GetString(bytes),
            //    Year = 1995
            //};

            Class1 c1 = new Class1();
            c1.imge = new Image();
            c1.imge.Data = bytes;

            //Image img = new Image();
            //img.Data = bytes;

            string str=@"movie.json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(str, JsonConvert.SerializeObject(c1));

            // serialize JSON directly to a file
            //using (StreamWriter file = File.CreateText(str))
            {
                //JsonSerializer serializer = new JsonSerializer();
                //serializer.Serialize(file, img);
            }




            // read file into a string and deserialize JSON to a type
            //Image movie1 = JsonConvert.DeserializeObject<Image>(File.ReadAllText(str));
            Class1 movie1 = JsonConvert.DeserializeObject<Class1>(File.ReadAllText(str));
            // deserialize JSON directly from a file
            //using (StreamReader file = File.OpenText(@"c:\movie.json"))
            {
                //JsonSerializer serializer = new JsonSerializer();
                //Movie movie2 = (Movie)serializer.Deserialize(file, typeof(Movie));
            }


            MemoryStream ms = new MemoryStream(movie1.imge.Data);
            System.Drawing.Image img1 = System.Drawing.Image.FromStream(ms, true);//生成图片
            ms.Close();

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img1;


        }
    }
}
