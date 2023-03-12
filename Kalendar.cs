
using System;
using System.Collections.Generic;

public class Kalendar
    {
        private List<Udalost> udalosti = new List<Udalost>();
        public void Pridej(int kdy, Proces proces, TypUdalosti typUdalosti)
        {
            // Console.WriteLine($"PLAN: {proces.ID} {typUdalosti} v case: {kdy}");
            udalosti.Add(new Udalost(kdy, proces, typUdalosti));
        }
        public void OdeberJednu(Proces proces, TypUdalosti co)
        {
            var udalost = udalosti.Find((u) => u.kdo == proces && u.co == co);
            udalosti.Remove(udalost);
        }
        public Udalost VyberNejdrivejsi()
        {
            Udalost nejdrivejsi = null;
            udalosti.ForEach(u => {
                if(nejdrivejsi == null) nejdrivejsi = u;
                if(u.kdy < nejdrivejsi.kdy) nejdrivejsi = u;
            });
            udalosti.Remove(nejdrivejsi);
            return nejdrivejsi;
        }
    }

 