using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace APPLICATION.Models
{
    public class Fonctionnum : TabTermes
    {
         public short activeNum = 0; // si activelitterale = on active l'option de genre  les minterme on litterale
        private List<TabTermes> tabTermes = new List<TabTermes>();
        private List<TabTermes> tabTermesPremie = new List<TabTermes>();

        private int nbtermes = 0;
        static int nbVariables = 0;
        private int GroupeMAX = 0;
        const int Max = 32768;
        private int itiration = 0;
        void increment() { this.itiration++; }
        //================================================================================================
        int decimals;
        char virgule;
        int groupe;
        string minterme;
        byte check;
        int entiermax = 0;
        TabTermes term;
        //=================================================================================================
        // d'abord on classe les mintermes dans une liste pour parvenir  extraire le nombre de variables 
        //=================================================================================================
        public List<TabTermes> gettabTermes() { return this.tabTermes; }
        public string expression;
        public Fonctionnum() { }
        public Fonctionnum(string Exp)
        {

            expression = Exp; 

        }
        public List<TabTermes> gettabTermesPremie() { return this.tabTermesPremie; }
        public void settabTermes(List<TabTermes> tabtermes) { this.tabTermes = tabtermes; }
        public void SettabTermes(List<TabTermes> tab) { this.tabTermes = tab; }
        public void setGroupeMax(int newgroupemax) { this.GroupeMAX = newgroupemax; }
        public void setnbVariables(int var) { nbVariables = var; }
        public int getGroupeMAX() { return this.GroupeMAX; }

        //========================================================================


        //---------------------------------------------------------------------------------------------------------------------------
        public List<int> PassageLitteraleNumerique(string expression) 
        {
            List<int> resultat = new List<int>();
            String[] spearator = { "+" };
            String[] mintermslateralle = expression.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < mintermslateralle.Length; i++)
            {
                int mintermNumerique = ConverterMintermeLatEnNum(mintermslateralle[i]);
                resultat.Add(mintermNumerique);
            }
            return resultat;
        }

        //---------------------------------------------------------------------------------------------------------------------------
        public int ConverterMintermeLatEnNum(string mintermlateralle)
        {
            string resultat="" ;
            string st = "";
            String[] spearator = { "&" };
            String[] Variables = mintermlateralle.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
          /*  for(int i = 0; i < Variables.Length; i++)
            {
                Console.WriteLine("vr " + Variables[i]);
            }*/
           // Console.WriteLine("================================ ");
            for (int i = 0; i < Variables.Length; i++)
            {
                //Console.WriteLine("vr "+ Variables[i]+ ConverteVariableLatEnNum(Variables[i]));
               
                st = ConverteVariableLatEnNum(Variables[i]);
                
                    resultat = resultat + st;
                   
                
            }
            if (nbVariables< Variables.Length) { int var = (int)Variables.Length;
                setnbVariables( (int)var);
            }
          
            return Convert.ToInt32(resultat , 2 );
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public string ConverteVariableLatEnNum(string var)
        {
            string varStr = "'";
            if (var[0] == varStr[0]) { return "0"; }
            else { return "1"; }

        }

        //---------------------------------------------------------------------------------------------------------------------------
        public List<int> getds()
        {
            string min;
            List<int> result = new List<int>();
            List<int> minterms = new List<int>();
          //  minterms = extraireMinterme();
            //string expression;

            //--------------------------------------------------
            Console.Write("Entrez les mintermes un apres l'aure separes par une virgules : ");
            Console.Write("{");
          //  expression = "1,2,3,4,5,6,7,8,9,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,45,44,46,47,48,49,50,51,52,53,54,55,56,57,58,59,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,78,79,80,81,82,83,84,85,86,87,89,90,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,118";
            //expression = Console.ReadLine();
           
            String[] spearator = { "," };
            String[] minterm = expression.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < minterm.Length; i++)
            {

                decimals = Int32.Parse(minterm[i]);//jai changé ici
                if (decimals > entiermax)
                {
                    min = Convert.ToString(decimals, 2);
                    entiermax = decimals;
                    nbVariables = min.Length; //nombre de variables fixé 
                }
                result.Add(decimals);
            }
            //------------------------------------------------
          /*  foreach (int i in minterms) { Console.WriteLine(i); }
            for (int i = 0; i < minterms.Count(); i++)
            {
               
                decimals = minterms[i];
              /*  if (decimals > Max)
               {
                    Console.WriteLine("veuillez entrer un autre nombre ! ");
                }//
                list.Add(decimals);
                
                if (decimals > entiermax)
                {
                    min = Convert.ToString(decimals, 2);
                    entiermax = decimals;
                    nbVariables = min.Length; //nombre de variables fixé 
                }
            }*/
            Console.WriteLine("========================================================");
            Console.WriteLine("le nombre de variables est : " + nbVariables);
            Console.WriteLine("========================================================");
            
            return result;
        }
       
        //----------------------------------------------------------------------------------------
        public List<TabTermes> combine(List<TabTermes> tabTermesA)

        {

            List<TabTermes> tabTermesb = new List<TabTermes>(); // la liste de retour
            Boolean stop = false;
            int cpt = 0; // pour parcourir les element de groupeA 
            int ptr = 0; //parcourir les elements de groupeB 

            try
            {
                TabTermes tabTerA = tabTermesA[cpt];


                int newMAxGroupe = 0; // pour recupere le noveaux groupe max qu'on utiliserai comme conditions d'arret dans combine tout
                while ((int)tabTerA.getgroupe() < getGroupeMAX())
                {
                    ptr = cpt + 1;



                    TabTermes tabTerB =tabTermesA[ptr];

                    // recupere la position de prochaine groupe
                    while ((int)tabTerA.getgroupe() == (int)tabTerB.getgroupe())
                    {
                        ptr++; tabTerB = tabTermesA[ptr];

                    }

                    int GroupeB = (int)tabTerB.getgroupe(); //recuper la valure de prochaine groupe

                    stop = false;
                    while (stop != true)
                    {


                        // on passe par tout les minterms de groupeB , on test dabord sur eu l'adjacence
                        string termc;
                        termc = (string)Adjacence((string)tabTerA.getminterme(), (string)tabTerB.getminterme());
                        if (testadjacence == true)
                        {
                            List<int> dec = new List<int>();
                            foreach (int val in (List<int>)tabTerA.getdecimals()) { dec.Add(val); }
                            foreach (int val in (List<int>)tabTerB.getdecimals()) { dec.Add(val); }
                            int NbOne;
                            NbOne = CalculerNBun(termc);
                            if (newMAxGroupe < NbOne)
                            { newMAxGroupe = NbOne; }
                            TabTermes a = new TabTermes(dec, 0, termc, NbOne);
                            tabTermesb.Add(a);
                            // pour dire que ce terme est deja couvert 
                            tabTerA.setcheck(1);
                            tabTerB.setcheck(1);
                        }


                        /* on passe au test de prochaine minterme*/
                        if (ptr < (tabTermesA.Count) - 1)
                        {
                            ptr++;

                            TabTermes tabTermes1 = tabTermesA[ptr];
                            tabTerB = tabTermes1;
                        }
                        else { stop = true; }
                        if ((int)tabTerB.getgroupe() != GroupeB)
                        { stop = true; }

                    }
                    if ((byte)tabTerA.getcheck() == 0) { tabTermesPremie.Add(tabTerA); }
                    // puisque on termine avec le minterme tabTerA on le supprime
                    tabTermesA.Remove(tabTerA);

                    tabTerA = tabTermesA[cpt];// pour passe au prochaine minterm avec qui faire le test

                }
                // cette etape conserne le dernier groupe de la liste
                if (tabTermesA.Count() != 0)
                {
                    for (int cnt = 0; cnt < tabTermesA.Count(); cnt++)
                    {
                        TabTermes term = (TabTermes)tabTermesA[cnt];
                        if (term.getcheck() == 1) { tabTermesA.Remove(term); }
                        else { tabTermesPremie.Add(term); }
                    }
                }

                setGroupeMax(newMAxGroupe);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("liste A vide ");
                return tabTermesb;
            }
            Console.WriteLine("tabTermesPremie" + tabTermesPremie.Count());
            return tabTermesb;
        }
        //----------------------------------------------------------------------------------------
        public void Combinetout(List<TabTermes> tabTermesA)
        {
            List<TabTermes> tabTermesb = new List<TabTermes>();
            Boolean stop = false;
            while (stop != true)
            {
                
                
                tabTermesb = combine(tabTermesA);
                
                Console.WriteLine("tabTeB avant supprimer redandant " + tabTermesb.Count());
                tabTermesb = supprimeredandant(tabTermesb);
                Console.WriteLine("tabTeB apres supprimer redandant " + tabTermesb.Count());
                afficherList(tabTermesb);
                tabTermesA = tabTermesb;

                if (tabTermesb.Count == 0) { stop = true; }
            }
            if (gettabTermes().Count != 0)
            {
                Console.WriteLine("il y un probleme ");
                
            }
            Console.WriteLine("liste d'impliquant finale");
            tabTermesPremie = supprimeredandant(tabTermesPremie);
           
            afficherList(tabTermesPremie);


        }
        //----------------------------------------------------------------------------------------
        public List<TabTermes> supprimeredandant(List<TabTermes> tabTermesb)
        {
          
           short stop =0;
            int i =0;
           
               while ( (stop != 1)&&(i<tabTermesb.Count))
            {
                 
                for (int j = i+1; j < tabTermesb.Count; j++)
                {
                    if (equalsTerme(tabTermesb[i].getminterme(), tabTermesb[j].getminterme()) == 1)
                    {
                        
                        tabTermesb.Remove(tabTermesb[j]);
                        j--;
                    }
                    
                }
                i++;
            }
            return tabTermesb;
        }
        //----------------------------------------------------------------------------------------
        /*  public short equalsTerme(TabTermes mina , TabTermes minb)
          {
              string A = mina.getminterme();
              string B = minb.getminterme();
              for(int i = 0; i < A.Length; i++)
              {
                  if (A[i] != B[i]) { return -1; }
              }
              return 1;
          }*/
        //------------------------------------------------------------------------------------------
        public static short egalitedejaDansEmpliquantfinale(string A, List<string> B)
        {
            short result = 0;
            if (B.Count != 0)
            {
                foreach (string impl in B)
                {
                    result = equalsTerme(A, impl);
                    if (result == 1) { return 1; } //cette element existe deja
                }
                return -1; // y pas cette element
            }
            else { return -1; } //la list est vide
        }
        //----------------------------------------------------------------------------------------
        public static short equalsTerme(string A, string B)
        {

            if (A.Length != B.Length) { return -1; }
            else
            {
                for (int i = 0; i < A.Length; i++)
                {
                    if (A[i] != B[i]) { return -1; }
                }
                return 1;
            }

            return 0;

        }
        //----------------------------------------------------------------------------------------
        public int CalculerNBun(String minterme)
        {
            int nbun = 0;
            for (int i = 0; i < minterme.Length; i++)
            {
                if (minterme[i] == '1')
                {
                    nbun++;
                }

            }
            return nbun;
        }
        //----------------------------------------------------------------------------------------
      
     


        public void getmin(List<int> ls)
        {

            for (int i = 0; i < ls.Count; i++)
            {
                List<int> combinaison = new List<int>();
                decimals = ls[i];
                combinaison.Add(decimals);
               
                minterme = Convert.ToString(decimals, 2).PadLeft(nbVariables, '0');
                groupe = CalculerNBun(minterme);
                if (groupe > GroupeMAX) //pour recuper le groupe max
                {
                    this.GroupeMAX = groupe;
                }
                check = 0;
                tabTermes.Add(new TabTermes(combinaison, check, minterme, groupe));
            }
            tabTermes.Sort(new triliste());
        }
        //affichage de la liste
        //----------------------------------------------------------------------------------------
        public void afficherList(List<TabTermes> tabTerm)
        {
            foreach (TabTermes tabTermes in tabTerm)
            {
                Console.WriteLine();
                tabTermes.afficher();
            }
            Console.WriteLine();
            

        }



    }

}