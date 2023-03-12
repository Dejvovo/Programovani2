using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

public class ZakaznikFactory {
    private Random random;
    private Model model;

    private int pocetVygenerovanychZakazniku = 0;

    public ZakaznikFactory(Random random, Model model) {
        this.random = random;
        this.model = model;
    }

    private int VygenerujPrichod() {
        return random.Next(0, 601);
    }

    private int VygenerujTrpelivost() {
        return random.Next(0, 181);
    }

    private int VygenerujPocetNakupu() {
        return random.Next(1, 21);
    }

    private List<string> VygenerujNakupy(int pocetNakupu, string[] oddeleni) {
        var result = new List<string>();

        for(var i=0; i< pocetNakupu; i++) {
            result.Add(oddeleni[random.Next(0, oddeleni.Length-1)]);
        }

        return result;
    }

    public Zakaznik VygenerujZakaznika(string[] oddeleni) {
        var prichod = VygenerujPrichod();
        var trpelivost = VygenerujTrpelivost();
        var pocetNakupu = VygenerujPocetNakupu();
        var nakupy = VygenerujNakupy(pocetNakupu, oddeleni);
        var jmeno = $"$Z{pocetVygenerovanychZakazniku}";

        // Console.WriteLine($"Vygeneroval jsem zakaznika Jmeno:{jmeno}, prichod:{prichod}, trpelivost:{trpelivost}, nakupy: {string.Join(",", nakupy)} ");

        pocetVygenerovanychZakazniku++;
        var result = new Zakaznik(
            model,
            jmeno, 
            prichod,
            trpelivost,
            nakupy);

        return result;
    }
}
