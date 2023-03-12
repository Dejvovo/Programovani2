using System.Globalization;
using System;
using System.Collections.Generic;

public class Zakaznik : Proces
    {
        private int trpelivost;
        private int prichod;
        private List<string> Nakupy;

        public int CasKteryStravilVObchodnimDome;

        public Zakaznik(Model model, string ID, int prichod, int trpelivost, List<string> nakupy) {
            this.model = model;
            this.ID = ID;
            this.prichod = prichod;
            this.trpelivost = trpelivost;
            this.Nakupy = nakupy;

            model.Naplanuj(prichod, this, TypUdalosti.Start);
        }

        public Zakaznik(Model model, string popis)
        {
            this.model = model;
            string[] popisy = popis.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
           
            this.ID = popisy[0];
            this.prichod = int.Parse(popisy[1]);
            this.trpelivost = int.Parse(popisy[2]);
            Nakupy = new List<string>();
            for (int i = 3; i < popisy.Length; i++)
            {
                Nakupy.Add(popisy[i]);
            }
            this.patro = 0;
            // Console.WriteLine("Init Zakaznik: {0}", ID);
            model.Naplanuj(prichod, this, TypUdalosti.Start);
        }
        public override void Zpracuj(Udalost udalost)
        {
            switch (udalost.co)
            {
                case TypUdalosti.Start:
                    if (Nakupy.Count == 0)
                    // ma nakoupeno
                    {
                        if (patro == 0) {
                            Log("-------------- odchází"); // nic, konci
                            CasKteryStravilVObchodnimDome = udalost.kdy;// - prichod;
                        }
                        else
                            model.Vytah.PridejDoFronty(patro, 0, this);
                    }
                    else
                    {
                        var oddeleni = ZiskejOddeleni(Nakupy[0]);
                        int pat = oddeleni.patro;
                        if (pat == patro) // to oddeleni je v patre, kde prave jsem
                        {
                            if (Nakupy.Count > 1)
                                model.Naplanuj(model.Cas + trpelivost, this, TypUdalosti.Trpelivost);
                            oddeleni.ZaradDoFronty(this);
                        }
                        else
                            model.Vytah.PridejDoFronty(patro, pat, this);
                    }
                    break;
                case TypUdalosti.Obslouzen:
                    Log("Nakoupeno: " + Nakupy[0]);
                    Nakupy.RemoveAt(0);
                    // ...a budu hledat dalsi nakup -->> Start
                    model.Naplanuj(model.Cas, this, TypUdalosti.Start);
                    break;
                case TypUdalosti.Trpelivost:
                    Log("!!! Trpelivost: " + Nakupy[0]);
                    // vyradit z fronty:
                    {
                        var odd = ZiskejOddeleni(Nakupy[0]);
                        odd.VyradZFronty(this);
                    }

                    // prehodit tenhle nakup na konec:
                    string nesplneny = Nakupy[0];
                    Nakupy.RemoveAt(0);
                    Nakupy.Add(nesplneny);

                    // ...a budu hledat dalsi nakup -->> Start
                    model.Naplanuj(model.Cas, this, TypUdalosti.Start);
                    break;
            }
        }

        private Oddeleni ZiskejOddeleni(string jmeno)
        {
            return model.VsechnaOddeleni.Find(o => o.ID == jmeno);
        }
    }
