using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Models
{


    public class TabTermes
    {
        public List<int> Decimals { get; } = new List<int>();
        public byte Check { get; private set; }
        public int Groupe { get; }

        private string minterme;
        //====================================
        //nt decimalss;
        int nbVariables;
        int nbtermes;
        int entiermax = 0;
        const int Max = 32768;
        //****************************************************************************************
        //============== pour teste l'adjacence ==============//
        static int saveit = 0;
        public static Boolean testadjacence = false;
        public Boolean gettestadjacence() { return testadjacence; }

        //===================================================================//
        //Constructeur
        public TabTermes() { }
        public TabTermes(List<int> decimals, byte check, string minterme, int groupe)
        {
            this.Decimals = decimals;
            this.Check = check;
            this.minterme = minterme;
            this.Groupe = groupe;
        }
        // ======================
        public string Adjacence(string termA, string termB)
        {
            //toujours initialisé ce champs a false avant de commence le test
            testadjacence = false;
            saveit = 0;
            char v = 'x';
            int cpt = 0;
            for (int i = 0; i < termA.Length; i++)
            {
                if (termA[i] != termB[i])
                {
                    cpt++; saveit = i;
                    // Console.Write("| value " + saveit);

                }
            }

            // apres le parcour des deux chaine
            if (cpt == 1)
            {
                testadjacence = true;
                return changeChar(termB, v, saveit + 1);
            }
            else
            {
                testadjacence = false;
                return termB;
            }
        }

        //------------------------------------------------------------------//
        public string changeChar(string str, char ch, int position)
        {
            return str.Substring(0, position - 1) + ch + str.Substring(position);
        }
        public string addCharBegginig(string str, char ch)
        {
            return ch + str.Substring(0, str.Length);
        }
        //------------------------------------------------------------------//
        public void afficherlistdecimals()
        {
            Console.Write("[");
            for (int i = 0; i < Decimals.Count; i++)
            {
                Console.Write(Decimals[i]);
                Console.Write(",");
            }
            Console.Write("]");
        }
        public void afficher()
        {
            afficherlistdecimals();
            Console.Write("|" + Check + "|" + minterme + "|" + Groupe);
        }
        //******* les getters *****************
        public List<int> getdecimals()
        { return Decimals; }
        public int getint(int i)
        { return Decimals[i]; }
        public byte getcheck()
        { return Check; }
        public void setcheck(byte a)
        { this.Check = a; }
        public int getgroupe()
        { return Groupe; }
        public String getminterme()
        { return minterme; }
        //****************combinerMinterm************

    }
}
