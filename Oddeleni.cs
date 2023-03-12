using System;
using System.Collections.Generic;

public class Oddeleni : Proces
    {
        private int rychlost;
        private List<Zakaznik> fronta;
        private bool obsluhuje;

        public Oddeleni(Model model, string popis)
        {
            this.model = model;
            string[] popisy = popis.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            this.ID = popisy[0];
            this.patro = int.Parse(popisy[1]);
            if (this.patro > model.MaxPatro)
                model.MaxPatro = this.patro;
            this.rychlost = int.Parse(popisy[2]);
            obsluhuje = false;
            fronta = new List<Zakaznik>();
            model.VsechnaOddeleni.Add(this);
        }
        public void ZaradDoFronty(Zakaznik zakaznik)
        {
            fronta.Add(zakaznik);
            Log("do fronty " + zakaznik.ID);

            if (obsluhuje) {} // nic
            else
            {
                obsluhuje = true;
                model.Naplanuj(model.Cas, this, TypUdalosti.Start);
            }
        }
        public void VyradZFronty(Zakaznik koho)
        {
            fronta.Remove(koho);
        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:
                    if (fronta.Count == 0)
                        obsluhuje = false; // a dal neni naplanovana a probudi se tim, ze se nekdo zaradi do fronty
                    else
                    {
                        Zakaznik zak = fronta[0];
                        fronta.RemoveAt(0);
                        model.Odplanuj(zak, TypUdalosti.Trpelivost);
                        model.Naplanuj(model.Cas + rychlost, zak, TypUdalosti.Obslouzen);
                        model.Naplanuj(model.Cas + rychlost, this, TypUdalosti.Start);
                    }
                    break;
            }
        }
    }
