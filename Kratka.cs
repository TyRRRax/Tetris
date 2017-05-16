using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication4
{
    public class Kratka
    {
        private Rectangle kwadrat;
        private Color kolor;
        private int x, y;
        private bool zajeta;
        public const int wymiar = 15;

        public Kratka()
        {
            kwadrat = new Rectangle(x * wymiar + 2, y * wymiar + 2, wymiar - 2, wymiar - 2);
            zajeta = false;
            x = 0;
            y = 0;
            kolor = Color.Black;
        }
        public Kratka(int _x, int _y)
        {
            kwadrat = new Rectangle(x * wymiar + 2, y * wymiar + 2, wymiar - 2, wymiar - 2);
            zajeta = false;
            x = _x;
            y = _y;
            kolor = Color.Black;
        }
        public Kratka(int _x, int _y, Color k)
        {
            kwadrat = new Rectangle(x * wymiar + 2, y * wymiar + 2, wymiar - 2, wymiar - 2);
            zajeta = false;
            x = _x;
            y = _y;
            kolor = k;
        }
        public bool czyzajeta()
        {
            return zajeta;
        }
        public void ustawkolor(Color k)
        {
            kolor = k;
        }
        public Color getkolor()
        {
            return kolor;
        }
        public void rysuj(Graphics g)
        {
            SolidBrush p = new SolidBrush(kolor);
            g.FillRectangle(p, kwadrat);
            g.DrawRectangle(Pens.Black, kwadrat);
            zajeta = true;
        }

        public void kasuj(Graphics g, Color k)
        {
            SolidBrush p = new SolidBrush(k);
            g.FillRectangle(p, kwadrat);
            g.DrawRectangle(new Pen(p), kwadrat);
            zajeta = false;
        }
    }
}
