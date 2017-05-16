using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public class Plansza_Tetris : Plansza
    {
        private int x_figury, y_figury;
        private Figura klocek;

        public Plansza_Tetris()
        {
            x_figury = y_figury = 0;
        }

        public Plansza_Tetris(int a, int b)
            : base(a, b)
        {
            x_figury = y_figury = 0;
        }

        public bool rysujfigure(int x, int y, Figura f)
        {
            if (!sprawdzwymiary(x, y)) return false;
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (f.siatka[i, j])
                    {
                        if (!sprawdzwymiary(x + i, y + j)) return false;
                        if (siatka[x + i, y + j].czyzajeta()) return false;
                    }
            x_figury = x;
            y_figury = y;
            if (klocek != f) klocek = f;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (f.siatka[i, j])
                    {
                        siatka[x + i, y + j].ustawkolor(f.kolor);
                        rysujkratke(x + i, y + j);
                    }
            return true;
        }

        public bool wdol()
        {
            if (klocek == null) return false;
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    if (klocek.siatka[i, j] && (j == 3 || !klocek.siatka[i, j + 1]))
                    {
                        if (!sprawdzwymiary(x_figury + i, y_figury + j + 1)) return false;
                        if (siatka[x_figury + i, y_figury + j + 1].czyzajeta()) return false;
                    }
                }

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) siatka[x_figury + i, y_figury + j].kasuj(g, BackColor);
            rysujfigure(x_figury, y_figury + 1, klocek);
            return true;
        }

        public bool wlewo()
        {
            if (klocek == null) return false;
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    if (klocek.siatka[i, j] && (i == 0 || !klocek.siatka[i - 1, j]))
                    {
                        if (!sprawdzwymiary(x_figury + i - 1, y_figury + j)) return false;
                        if (siatka[x_figury + i - 1, y_figury + j].czyzajeta()) return false;
                    }
                }

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) siatka[x_figury + i, y_figury + j].kasuj(g, BackColor);
            rysujfigure(x_figury - 1, y_figury, klocek);
            return true;
        }

        public bool wprawo()
        {
            if (klocek == null) return false;
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    if (klocek.siatka[i, j] && (i == 3 || !klocek.siatka[i + 1, j]))
                    {
                        if (!sprawdzwymiary(x_figury + i + 1, y_figury + j)) return false;
                        if (siatka[x_figury + i + 1, y_figury + j].czyzajeta()) return false;
                    }
                }

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) siatka[x_figury + i, y_figury + j].kasuj(g, BackColor);
            rysujfigure(x_figury + 1, y_figury, klocek);
            return true;
        }

        public void obroc_w_lewo()
        {
            if (klocek.kat == 0) klocek.kat = 270;
            else klocek.kat -= 90;
            Figura nowa = klocek.odwroc(klocek.kat);
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    if (nowa.siatka[i, j] && !sprawdzwymiary(x_figury + i, y_figury + j)) return;
                    if (nowa.siatka[i, j] &&
                    !klocek.siatka[i, j] &&
                    siatka[x_figury + i, y_figury + j].czyzajeta())
                        return;
                }

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) siatka[x_figury + i, y_figury + j].kasuj(g, BackColor);
            rysujfigure(x_figury, y_figury, nowa);
        }

        public void obroc_w_prawo()
        {
            if (klocek.kat == 270) klocek.kat = 0;
            else klocek.kat += 90;
            Figura nowa = klocek.odwroc(klocek.kat);
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    if (nowa.siatka[i, j] && !sprawdzwymiary(x_figury + i, y_figury + j)) return;
                    if (nowa.siatka[i, j] &&
                    !klocek.siatka[i, j] &&
                    siatka[x_figury + i, y_figury + j].czyzajeta())
                        return;
                }

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) siatka[x_figury + i, y_figury + j].kasuj(g, BackColor);

            rysujfigure(x_figury, y_figury, nowa);
        }

        public void wyczysclinie(int n)
        {
            if (!sprawdzwymiary(1, n)) return;
            for (int i = 0; i < sz_ekranu; i++)
                siatka[i, n].kasuj(g, BackColor);
        }

        public void przesunlinie(int n)
        {
            if (n < 0 || n >= wys_ekranu - 1) return;
            int i;
            for (i = 0; i < sz_ekranu; i++)
            {
                if (!siatka[i, n].czyzajeta())
                {
                    siatka[i, n + 1].kasuj(g, BackColor);
                    continue;
                }
                siatka[i, n + 1].ustawkolor(siatka[i, n].getkolor());
                siatka[i, n + 1].rysuj(g);
                siatka[i, n].kasuj(g, BackColor);
            }
        }

        public int linie()
        {
            int n = wys_ekranu - 1;
            int i;
            int ile = 0;
            bool czylinia = true;
            while (n > 0 && ile < 4)
            {
                for (i = 0; i < sz_ekranu; i++)
                    if (!siatka[i, n].czyzajeta()) czylinia = false;
                if (czylinia)
                {
                    ile++;
                    for (int m = n - 1; m >= 0; m--)
                        przesunlinie(m);
                }
                else n--;
                czylinia = true;
            }
            return ile;
        }

        public Figura getfigura()
        {
            return klocek;
        }
    }
}
