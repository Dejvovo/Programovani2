using System;
using System.Collections.Generic;
using System.Linq;

public class Model
    {
        private Random random;

        public int Cas;
        public Vytah Vytah;
        public List<Oddeleni> VsechnaOddeleni = new List<Oddeleni>();
        public List<Zakaznik> VsichniZakaznici = new List<Zakaznik>();

        public int MaxPatro;
        private Kalendar kalendar;

        private string NazevSouboru;

        private int PocetVygenerovanychZakazniku;

        public Model(string nazevSouboru, int pozetZakazniku, Random random) {
            NazevSouboru = nazevSouboru;
            PocetVygenerovanychZakazniku = pozetZakazniku;
            this.random = random;
        }

        private void Init() {
            Cas = 0;
            kalendar = new Kalendar();
            VytvorProcesy();

        }

        public void Naplanuj(int kdy, Proces kdo, TypUdalosti co)
        {
            kalendar.Pridej(kdy, kdo, co);
        }
        public void Odplanuj(Proces kdo, TypUdalosti co)
        {
            kalendar.OdeberJednu(kdo, co);
        }
        public void VytvorProcesy()
        {
            var soubor = new System.IO.StreamReader(NazevSouboru);
            while (!soubor.EndOfStream)
            {
                string s = soubor.ReadLine();
                if (s != "")
                {
                    switch (s[0])
                    {
                        case 'O':
                            new Oddeleni(this, s.Substring(1));
                            break;
                        // case 'Z':
                        //     new Zakaznik(this, s.Substring(1));
                            // break;
                        case 'V':
                            Vytah = new Vytah(this, s.Substring(1));
                            break;
                    }
                }
            }
            soubor.Close();


            var zakaznikFactory = new ZakaznikFactory(this.random, this);
            var nazvyOddeleni = this.VsechnaOddeleni.Select(o => o.ID).ToArray();

            for(var i=0; i < PocetVygenerovanychZakazniku; i++) {
                var zakaznik = zakaznikFactory.VygenerujZakaznika(nazvyOddeleni);
                VsichniZakaznici.Add(zakaznik);
            }
        }
        public int Vypocet()
        {
            Init();

            for(Udalost udalost; (udalost = kalendar.VyberNejdrivejsi()) != null; ) {
                Cas = udalost.kdy;
                udalost.kdo.Zpracuj(udalost);
                
            }

            return Cas;
        }
    }
