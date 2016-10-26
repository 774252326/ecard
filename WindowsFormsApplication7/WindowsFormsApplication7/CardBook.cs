using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;

namespace WindowsFormsApplication7
{
    class CardBook
    {
        public Card[] C { get; set; }


        public void CardBook2Al(ref ArrayList al)
        {
            foreach (Card k in this.C)
            {
                ArrayList cardal = new ArrayList();
                k.Card2Al(ref cardal);
                al.Add(cardal);
            }

        }


        public void Al2CardBook(ArrayList al)
        {
            Card ca;

            List<Card> w = new List<Card>();

            foreach (ArrayList cardal in al)
            {
                ca = new Card();
                ca.Al2Card(cardal);
                w.Add(ca);

            }

            this.C = (Card[])(w.ToArray());
        }


        public void CardBookLogo2Al(ref ArrayList al)
        {
            foreach (Card k in this.C)
            {
                Image img = null;
                k.Logo2Img(ref img);
                al.Add(img);
            }
        }

        public void Al2CardBookLogo(ArrayList al)
        {
            int i = 0;
            foreach (Image img in al)
            {
                this.C[i].Img2Logo(img);
                i++;
            }
        }

    }
}
