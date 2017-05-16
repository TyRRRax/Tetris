using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        private Plansza_Tetris Plansza_do_gry;
        private Plansza_Tetris nastepny;

        private Label Pkt, Poziom;
        private Label lblPkt, lblPoziom;
        private Label info;

        public int level;
        public int punkty;

        private bool szybka;
        private bool gra;
        private bool pauza;

        public Form1()
        {
            InitializeComponent();
            Inicjuj();
        }
        private void Inicjuj()
        {
            level = 1;
            punkty = 0;
            gra = false;
            pauza = false;

            licznik.Interval = 1000;
            szybka = false;

            this.Plansza_do_gry = new Plansza_Tetris(10, 18);
            this.nastepny = new Plansza_Tetris(4, 4);

            this.Plansza_do_gry.Location = new System.Drawing.Point(15, 15);
            this.Plansza_do_gry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Plansza_do_gry.Name = "Plansza_do_gry";
            this.Plansza_do_gry.TabIndex = 0;
            this.Plansza_do_gry.Paint += new PaintEventHandler(this.Plansza_Paint);

            this.nastepny.Location = new System.Drawing.Point(180, 15);
            this.nastepny.Name = "nastepny";
            this.nastepny.TabIndex = 0;
            this.nastepny.Paint += new PaintEventHandler(this.Plansza_Paint);

            this.Controls.Add(this.nastepny);
            this.Controls.Add(this.Plansza_do_gry);

            this.lblPkt = new Label();
            this.lblPoziom = new Label();
            this.Pkt = new Label();
            this.Poziom = new Label();
            this.info = new Label();

            this.lblPkt.AutoSize = true;
            this.lblPkt.Location = new System.Drawing.Point(180, 120);
            this.lblPkt.Name = "lblPkt";
            this.lblPkt.Size = new System.Drawing.Size(35, 13);
            this.lblPkt.TabIndex = 0;
            this.lblPkt.Text = "Punkty:";

            this.lblPoziom.AutoSize = true;
            this.lblPoziom.Location = new System.Drawing.Point(180, 100);
            this.lblPoziom.Name = "lblPoziom";
            this.lblPoziom.Size = new System.Drawing.Size(35, 13);
            this.lblPoziom.TabIndex = 0;
            this.lblPoziom.Text = "Poziom:";

            this.Pkt.AutoSize = true;
            this.Pkt.Location = new System.Drawing.Point(230, 120);
            this.Pkt.Name = "Pkt";
            this.Pkt.Size = new System.Drawing.Size(35, 13);
            this.Pkt.TabIndex = 0;
            this.Pkt.Text = "0";

            this.Poziom.AutoSize = true;
            this.Poziom.Location = new System.Drawing.Point(230, 100);
            this.Poziom.Name = "Poziom";
            this.Poziom.Size = new System.Drawing.Size(35, 13);
            this.Poziom.TabIndex = 0;
            this.Poziom.Text = "1";

            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(180, 140);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(50, 50);
            this.info.TabIndex = 0;
            this.info.Text = "Enter - Start\n"
                                + "Esc - Koniec\n"
                                + "Spacja - Pauza\n"
                                + "<- -> - lewo / prawo\n"
                                + "strzalka w gore - obrot w prawo\n\n"
                                + "strzalka w dol - przyspieszenie\n"
                                + " (nacisnac raz zeby przyspieszyc\n"
                                + " i drugi raz zeby zwolnic)\n\n"
                                + "W / Q - obrót w prawo / lewo";

            this.Controls.Add(this.lblPoziom);
            this.Controls.Add(this.lblPkt);
            this.Controls.Add(this.Pkt);
            this.Controls.Add(this.Poziom);
            this.Controls.Add(this.info);
        }

        private void licznik_Tick(object sender, EventArgs e)
        {
            if (!Plansza_do_gry.wdol())
            {
                if (szybka)
                {
                    szybka = false;
                    if (level < 11)
                        licznik.Interval = 1000 - 100 * (level - 1);
                    else if (level < 21)
                        licznik.Interval = 100 - 10 * (level - 1);
                    else licznik.Interval = 10;
                }
                int pkt = Plansza_do_gry.linie();
                punkty += (pkt * pkt);
                Pkt.Text = punkty.ToString();
                if (punkty >= level * 10)
                {
                    level++;
                    Poziom.Text = level.ToString();
                    if (level < 11)
                        licznik.Interval -= 100;
                    else if (level < 21)
                        licznik.Interval -= 10;
                    else licznik.Interval = 10;
                }
               Figura f = nastepny.getfigura();
                if (!Plansza_do_gry.rysujfigure(5, 0, f))
                {
                    licznik.Stop();
                    licznik.Interval = 1000;
                    MessageBox.Show("koniec");
                    gra = false;
                    f = null;
                    Plansza_do_gry.reset();
                    nastepny.reset();
                    level = 1;
                    punkty = 0;
                    Pkt.Text = "0";
                    Poziom.Text = "1";
                    return;
                }
                nastepny.reset();
                nastepny.rysujfigure(0, 0, new Figura());
            }
        }

        private void Plansza_Paint(object sender, PaintEventArgs e)
        {
            Plansza_Tetris p = (Plansza_Tetris)sender;
            p.odswiez();
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !gra)
            {
                gra = true;
                Plansza_do_gry.rysujfigure(5, 0, new Figura());
                nastepny.rysujfigure(0, 0, new Figura());
                licznik.Start();
            }
            else if (e.KeyCode == Keys.Left && gra && !pauza) Plansza_do_gry.wlewo();
            else if (e.KeyCode == Keys.Right && gra && !pauza) Plansza_do_gry.wprawo();
            else if (e.KeyCode == Keys.Q && gra && !pauza) Plansza_do_gry.obroc_w_lewo();
            else if ((e.KeyCode == Keys.W || e.KeyCode == Keys.Up) && gra && !pauza) Plansza_do_gry.obroc_w_prawo();
            else if (e.KeyCode == Keys.Down && gra && !pauza)
            {
                if (szybka)
                {
                    licznik.Interval = 1000 - 100 * (level - 1);
                    szybka = false;
                }
                else if (!szybka)
                {
                    licznik.Interval = 50;
                    szybka = true;
                }
            }
            else if (e.KeyCode == Keys.Escape && gra)
            {
                gra = false;
                Plansza_do_gry.reset();
                nastepny.reset();
                level = 1;
                punkty = 0;
                Poziom.Text = "1";
                Pkt.Text = "0";
                licznik.Stop();
                licznik.Interval = 1000;
            }
            else if (e.KeyCode == Keys.Space && gra)
            {
                if (pauza)
                {
                    licznik.Start();
                    pauza = false;
                }
                else
                {
                    licznik.Stop();
                    pauza = true;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


    }
