using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication4
{
    public class Figura
    {
        public enum kolory { niebieski, zolty, czerwony, brazowy, zielony };
        public enum figury { L, odw_L, palka, kwadrat, trojkat, piorun, odw_piorun };

        public bool[,] siatka;
        public figury jaka;
        public Color kolor;
        public int kat;
        private static Random losuj = new Random();

        public Figura()
        {
            kat = 0;
            siatka = new bool[4, 4];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    siatka[i, j] = false; // Wypelnienie znacznikiem nieodwiedzony.

            kolory nowy_kolor = (kolory)losuj.Next(4); // Wylosowanie jednego z 5 kolorow. Skorzystanie z Random losuj.
            switch (nowy_kolor)
            {
                case kolory.niebieski: kolor = Color.LightBlue; break;
                case kolory.zolty: kolor = Color.LemonChiffon; break;
                case kolory.czerwony: kolor = Color.Red; break;
                case kolory.brazowy: kolor = Color.Brown; break;
                case kolory.zielony: kolor = Color.Green; break;
                default: kolor = Color.Black; break;
            }

            figury nowa_figura = (figury)losuj.Next(6);
            switch (nowa_figura)
            {
                case figury.L: jaka = figury.L; siatka[0, 0] = siatka[0, 1] = siatka[0, 2] = siatka[1, 2] = true; break;
                case figury.odw_L: jaka = figury.odw_L; siatka[0, 0] = siatka[1, 1] = siatka[1, 2] = siatka[0, 2] = true; break;
                case figury.palka: jaka = figury.palka; siatka[0, 0] = siatka[0, 1] = siatka[0, 2] = siatka[0, 3] = true; break;
                case figury.piorun: jaka = figury.piorun; siatka[0, 0] = siatka[0, 1] = siatka[1, 1] = siatka[1, 2] = true; break;
                case figury.trojkat: jaka = figury.trojkat; siatka[1, 0] = siatka[0, 1] = siatka[1, 1] = siatka[2, 1] = true; break;
                case figury.odw_piorun: jaka = figury.odw_piorun; siatka[1, 0] = siatka[1, 1] = siatka[0, 1] = siatka[0, 2] = true; break;
                case figury.kwadrat: jaka = figury.kwadrat; siatka[0, 0] = siatka[1, 0] = siatka[0, 1] = siatka[1, 1] = true; break;
            }
        }
        public Figura odwroc(int f)
        {
            if (f != 0 && f != 90 && f != 180 && f != 270) return this;
            Figura nowa = new Figura();
            nowa.kat = f;
            nowa.kolor = kolor;
            nowa.jaka = jaka;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    siatka[i, j] = false;

            switch (jaka)
            {
                case figury.L:
                    switch (f)
                    {
                        case 0: nowa.siatka[0, 0] = nowa.siatka[0, 1] = nowa.siatka[0, 2] = nowa.siatka[1, 2] = true; break;
                        case 90: nowa.siatka[0, 0] = nowa.siatka[1, 0] = nowa.siatka[2, 0] = nowa.siatka[0, 1] = true; break;
                        case 180: nowa.siatka[0, 0] = nowa.siatka[1, 0] = nowa.siatka[1, 1] = nowa.siatka[1, 2] = true; break;
                        case 270: nowa.siatka[2, 0] = nowa.siatka[0, 1] = nowa.siatka[1, 1] = nowa.siatka[2, 1] = true; break;
                    }
                    break;
                case figury.odw_L:
                    switch (f)
                    {
                        case 0: nowa.siatka[1, 0] = nowa.siatka[1, 1] = nowa.siatka[1, 2] = nowa.siatka[0, 2] = true; break;
                        case 90: nowa.siatka[0, 0] = nowa.siatka[0, 1] = nowa.siatka[1, 1] = nowa.siatka[2, 1] = true; break;
                        case 180: nowa.siatka[0, 0] = nowa.siatka[0, 1] = nowa.siatka[0, 2] = nowa.siatka[1, 0] = true; break;
                        case 270: nowa.siatka[0, 0] = nowa.siatka[1, 0] = nowa.siatka[2, 0] = nowa.siatka[2, 1] = true; break;
                    }
                    break;
                case figury.palka:
                    switch (f)
                    {
                        case 0:
                        case 180:
                            nowa.siatka[0, 0] = nowa.siatka[0, 1] = nowa.siatka[0, 2] = nowa.siatka[0, 3] = true; break;
                        case 90:
                        case 270:
                            nowa.siatka[0, 0] = nowa.siatka[1, 0] = nowa.siatka[2, 0] = nowa.siatka[3, 0] = true; break;
                    }
                    break;
                case figury.piorun:
                    switch (f)
                    {
                        case 0:
                        case 180:
                            nowa.siatka[0, 0] = nowa.siatka[0, 1] = nowa.siatka[1, 1] = nowa.siatka[1, 2] = true; break;
                        case 90:
                        case 270:
                            nowa.siatka[1, 0] = nowa.siatka[2, 0] = nowa.siatka[0, 1] = nowa.siatka[1, 1] = true; break;
                    }
                    break;
                case figury.kwadrat:
                    nowa.siatka[0, 0] = nowa.siatka[1, 0] = nowa.siatka[0, 1] = nowa.siatka[1, 1] = true; break;
                case figury.trojkat:
                    switch (f)
                    {
                        case 0: nowa.siatka[1, 0] = nowa.siatka[0, 1] = nowa.siatka[1, 1] = nowa.siatka[2, 1] = true; break;
                        case 90: nowa.siatka[0, 0] = nowa.siatka[0, 1] = nowa.siatka[0, 2] = nowa.siatka[1, 1] = true; break;
                        case 180: nowa.siatka[0, 0] = nowa.siatka[1, 0] = nowa.siatka[2, 0] = nowa.siatka[1, 1] = true; break;
                        case 270: nowa.siatka[1, 0] = nowa.siatka[1, 1] = nowa.siatka[1, 2] = nowa.siatka[0, 1] = true; break;
                    }
                    break;
                case figury.odw_piorun:
                    switch (f)
                    {
                        case 0:
                        case 180:
                            nowa.siatka[1, 0] = nowa.siatka[1, 1] = nowa.siatka[0, 1] = nowa.siatka[0, 2] = true; break;
                        case 90:
                        case 270:
                            nowa.siatka[0, 0] = nowa.siatka[1, 0] = nowa.siatka[1, 1] = nowa.siatka[2, 1] = true; break;
                    }
                    break;

                }
            return nowa;
        }
    }
}
