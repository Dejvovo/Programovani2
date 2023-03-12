using System;

public abstract class Proces
    {
        public string ID;

        public int patro;


        public abstract void Zpracuj(Udalost ud);

        public void Log(string zprava)
        {
            // Console.WriteLine($"{model.Cas}/{patro} {ID}: {zprava}");
        }
        
        protected Model model;
    }
