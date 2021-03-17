using System;
using System.Collections.Generic;
using System.Text;

//h ttp://kaj.uniwersytetradom.pl/cshn.html

namespace BackpackingProblem
{
    class Plecak{
        private List<Przedmiot> listaPrzedmiotow;
        private double cMax;
        private double c = 0;
        private double v = 0;
        private double cOpt = 0;
        private double vOpt = 0;

        public void Dane(int rozPl, List<Przedmiot> lista) {
            int n = lista.Count;
            listaPrzedmiotow = new List<Przedmiot>(lista);
            //listaPrzedmiotow = lista;
            cMax = rozPl;
        }

        public void Probuj(int k) {
            if (k < listaPrzedmiotow.Count - 1) Probuj(k + 1);
            c += listaPrzedmiotow[k].wagaPrz;
            if (c <= cMax) {
                v += listaPrzedmiotow[k].wartoscPrz;
                listaPrzedmiotow[k].s = true;
                if (v > vOpt) {
                    cOpt = c;
                    vOpt = v;
                    foreach(Przedmiot p in listaPrzedmiotow) {
                        p.sOpt = p.s;
                    }
                    if (k < listaPrzedmiotow.Count - 1) Probuj(k + 1);
                    listaPrzedmiotow[k].s = false;
                    v -= listaPrzedmiotow[k].wartoscPrz;
                }
                c -= listaPrzedmiotow[k].wagaPrz;
            }
        }

        public List<Przedmiot> wynik() {
            Probuj(0);
            List<Przedmiot> listaWynikowa = new List<Przedmiot>();
            int k = 0;
            foreach(Przedmiot p in listaPrzedmiotow) {
                k++;
                if (p.sOpt) {
                    listaWynikowa.Add(p);
                }
            }
            return listaWynikowa;
        }

    }
}