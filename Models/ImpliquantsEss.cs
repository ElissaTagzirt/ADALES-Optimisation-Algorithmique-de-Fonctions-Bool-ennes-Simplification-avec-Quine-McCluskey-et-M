using System;
using System.Collections.Generic;
using System.Linq;

namespace APPLICATION.Models

{
    public class ImpliquantsEss
    {





         static public List<int> mintermes = new List<int>();
         List<int> copy_min = new List<int>(mintermes);
         public List<ImpPrem> ImpPrems = new List<ImpPrem>();
         public List<List<byte>> Min_Imp = new List<List<byte>>();
         public List<struc> ImpEss = new List<struc>();
        //-----------------------------------------------------------------le resultat finale-------------------------------------------------------------------------------------
         List<string> Empliquantfinale = new List<string>();
        public List<string> getEmpliquantfinale() { return Empliquantfinale; }

        //------------------------------------------------------------------------------------------------------------------------------------------------------
         public string[] petrick = new string[mintermes.Count];
         public string VP = "";

        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        public struct ImpPrem
        {
            public string Imp;
            public List<int> combinaisons;
   


            public ImpPrem(string i, List<int> l)
            {
                Imp = i;
                combinaisons = l;
            }
            public string getImp() { return Imp; }
            public List<int> getcombinaisons() { return combinaisons; }
            public void affichecombinaisons()
            {
                Console.Write("[");
                foreach (int i in combinaisons) { Console.Write(i + ","); }
                Console.Write("]");
            }

        }

        public struct struc
        {
            public ImpPrem Imp;
            public int min;   //
            public struc(ImpPrem i, int m) { Imp = i; min = m; }
            public ImpPrem getImpp() { return Imp; }
        }
        /*********************importer liste mintermes***************/
         public void importmintermes(List<int> ls)
        {
            mintermes = ls;
            Console.WriteLine(mintermes);
            //Console.WriteLine(ImpPrem) ;
        }
        /******************fin importer liste mintermes**************/
        /******************importer impliquants premiers***************/
        /***************fin importer impliquants premiers*************/
         public void importpremiers(Fonctionnum f)
        {
            Dictionary<ImpPrem, char> d = new Dictionary<ImpPrem, char>();
            List<TabTermes> l = new List<TabTermes>(f.gettabTermesPremie());
            ImpPrem s = new ImpPrem();
            foreach (var v in l)
            {
                s = new ImpPrem(v.getminterme(), v.getdecimals());
                if (!ImpPrems.Exists(x => x.Imp == s.Imp)) { ImpPrems.Add(s); }
            }
        }
        //--------------------------------------------------------------------------
      /*  static public void importpremiers(ExpressionLet f)
        {
            Dictionary<ImpPrem, char> d = new Dictionary<ImpPrem, char>();
            List<TabTermes> l = new List<TabTermes>(f.gettabTermesPremie());
            ImpPrem s = new ImpPrem();
            foreach (var v in l)
            {
                s = new ImpPrem(v.getminterme(), v.getdecimals());
                if (!ImpPrems.Exists(x => x.Imp == s.Imp)) { ImpPrems.Add(s); }
            }
        }*/
        /******************************methode generer matrice*******************************/
         List<List<byte>> GenerTabEss(List<int> m, List<ImpPrem> i)
        {
            List<List<byte>> mimp = new List<List<byte>>();
            List<byte> listbool = new List<byte>();
            int a, t;
            /******initialiser le "tableau"(liste mimp) avec la liste de chaque minterme(contient autant d'elements que d'impliquants premiers) et toutes les valeurs à "false"****/

            for (a = 0; a < m.Count; a++)
            {
                listbool = new List<byte>();
                for (t = 0; t < i.Count; t++) { listbool.Add(0); }
                mimp.Add(listbool);
            }
            /***********************************************************************/
                for (a = 0; a < i.Count; a++)
                {//poour chaque impliquant premier
                    foreach (var b in i[a].combinaisons)
                    {//pour chaque combinaison de cet impliquant
                        Console.WriteLine("{0} index:{1} ", b, m.IndexOf(b));
                        mimp[m.IndexOf(b)][a] = 1;

                        /*1. on se positionne dans la liste numero (m.IndexOf(i[a].combinaisons[b]) càd l'ordre du minterme alias la combinaison
                        2. puis on se positionne dans la case dont l'ordre est celui de l'impliquant premier
                        3. maintenant que nous sommes dans la case qui represente la combinaison impliquée par notre impliquant premier nous lui affectons un "vrai"
                        */
                    }
                }         
            return mimp;
        }
        /*****************************fin methode generer matrice**********************************************/
        /*****************extrait impliquants essentiels********************************/
         List<struc> extraitImpEss()
        {
            int i, j, k = 0;
            int plusUn = 0;
            struc s;
            List<struc> l = new List<struc>();
            for (i = 0; i < Min_Imp.Count; i++)
            {
                j = 0; plusUn = 0;
                while ((plusUn < 2) && (j < Min_Imp[i].Count))
                {
                    if (Min_Imp[i][j] == 1) { plusUn++; k = j; }
                    j++;
                }
                if (plusUn == 1) { s = new struc(ImpPrems[k], mintermes[i]); l.Add(s); }

            }
            return l;
        }
        /*********************fin extrait impliquants essentiels*********************/
        /********************mintermes non couverts****************************************/
         List<int> NonCouverts(List<struc> l)
        {
            List<int> mn = new List<int>(); bool existe; int i = 0;
            foreach (var v in mintermes)
            {/*revoir implementation en utilisant direcrement la liste precedente des non couverts*/
                existe = false;
                i = 0;
                while ((!existe) && (i < l.Count))
                {
                    existe = (l[i].Imp.combinaisons.Contains(v));
                    i++;
                }
                if (!existe) { mn.Add(v); }
            }

            return mn;
        }
        /***********************fin mintermes non couverts**********************************/
        /***********************************supprime essentiels de la matrice**************************************/
         void SupEss()
        {
            int i, j;
            foreach (var v in ImpEss)
            {/*parcourir la liste des essentiels*/

                /********suppression des colonnes*****/
                foreach (var x in v.Imp.combinaisons)
                {/*pour chaque essentiel parcourir sa liste de combinaisons de mintermes*/
                    i = mintermes.IndexOf(x);/*position du minterme a supprimer*/
                    if ((i > -1))
                    {/*le minterme n'a pas encore ete supprime dans une liste de combinaisons precedente*/
                        Min_Imp.RemoveAt(i);/**supprimer le minterme de la matrice*/

                        mintermes.RemoveAt(i);/*supprimer le minterme de la liste de mintermes*/
                    }
                }
                /********fin suppression des colonnes****/

                /**********suppression des lignes******/
                j = ImpPrems.IndexOf(v.Imp);/**position de l'impliquant essentiel courant*/
                if ((j > -1))
                {/**si il n'a pas encore ete supprime auparvant (c-a-d qu'il est essentiel pour 2 mintermes par ex.)*/
                    foreach (var w in Min_Imp)
                    {/*pour chaque minterme de la matrice*/
                        w.RemoveAt(j);/*on lui supprime la case correspondante a l'impliquant essentiel courant*/
                    }
                    ImpPrems.RemoveAt(j);
                }
                /*******fin suppression des lignes*****/
            }
        }
        /*********************************fin supprime essentiels de la matrice********************************/
        /********************************supprime colonnes dominantes***********************************/
         public bool supColDom()
        {
            bool contient = true, contenue = true, trouv = false;
            int i = 0, j = 0, k = 0;
            while (i < mintermes.Count - 1)
            {
                j = i + 1;
                while (j < mintermes.Count)
                {
                    k = 0; contient = true; contenue = true;
                    while ((contient || contenue) && (k < Min_Imp[0].Count))
                    {
                        if (Min_Imp[i][k] == 1 && Min_Imp[j][k] == 0)
                        {
                            contenue = false;
                        }
                        else if (Min_Imp[i][k] == 0 && Min_Imp[j][k] == 1)
                        {
                            contient = false;
                        }
                        k++;
                    }
                    if (contenue)
                    {
                        Min_Imp.RemoveAt(j); mintermes.RemoveAt(j); trouv = true;
                    }
                    else if (contient)
                    {
                        Min_Imp.RemoveAt(i); mintermes.RemoveAt(i); trouv = true; j = i + 1;
                    }
                    else { j++; }
                }
                i += 1;
            }
            return (trouv);
        }
        /*****************************fin supprime colonnes dominantes******************************/
        /*****************************supprime lignes dominees*******************************/
         public bool supligDom()
        {
            bool contient = true, contenue = true, trouv = false;
            int i = 0, j = 0, k = 0;
            while (i < ImpPrems.Count - 1)
            {
                j = i + 1;
                while (j < ImpPrems.Count)
                {
                    k = 0; contient = true; contenue = true;
                    while ((contient || contenue) && (k < Min_Imp.Count))
                    {
                        if (Min_Imp[k][i] == 1 && Min_Imp[k][j] == 0) { contenue = false; }
                        else if (Min_Imp[k][i] == 0 && Min_Imp[k][j] == 1) { contient = false; }
                        k++;
                    }
                    if (contenue)
                    {
                        foreach (var v in Min_Imp)
                        {
                            v.RemoveAt(i);
                        }
                        ImpPrems.RemoveAt(i); trouv = true; j = i + 1;
                    }
                    else if (contient)
                    {
                        foreach (var v in Min_Imp)
                        {
                            v.RemoveAt(j);
                        }
                        ImpPrems.RemoveAt(j); trouv = true;
                    }
                    else { j++; }
                }
                i += 1;
            }
            return (trouv);
        }
        /****************************fin supprime lignes dominees*****************************/
        /********************************procedure generale de reduction de la matrice******************************/

        public void AffichageDesResultatFinale()
        {
            Console.WriteLine("Affichage des minterme finale a convertire en exprission ");
            foreach (string val in Empliquantfinale)
            {
                Console.WriteLine("impliquant finale " + val);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//  
        public string ReductionTotaleQMCandPETRIK()
        {
            short testeur;

            testeur = reduc();

            if (testeur == 1)
            {


                foreach (var v in ImpEss)
                {



                    int max = v.Imp.combinaisons.Max();

                    // Impliquantfinale impliquantfinalepassage = new Impliquantfinale(v.Imp.Imp, max);
                    Empliquantfinale.Add(v.Imp.Imp);
                    //   Empliquantfinale.Add(impliquantfinalepassage);
                    //  Console.WriteLine("impliquant kan " + ConverterMinterme(Empliquantfinale));
                    //  Console.WriteLine("Le resultat finale " + ConverterMinterme(Empliquantfinale));
                }


            }
            else
            {

                Console.WriteLine("Affichage des resultat finale  avec QMC et petrik ");
                AffichageDesResultatFinale();
                // Console.WriteLine("Le resultat finale "+ ConverterMinterme(Empliquantfinale));
            }
            CleaninglisteFinale(Empliquantfinale);
            AffichageDesResultatFinale();
          //  Console.WriteLine("Le resultat finale " + ConverterMinterme(Empliquantfinale));
            return ConverterMinterme(Empliquantfinale);

        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public short reduc()
        {/*retourne vrai si simplification terminee, faux si passage a petrick necessaire*/
            Min_Imp = GenerTabEss(mintermes, ImpPrems);
            Console.WriteLine("affichage de la matrice avant reduction");
            affichematrice((List<List<byte>>)Min_Imp, (List<ImpPrem>)ImpPrems);
            List<struc> addEss = new List<struc>(); /*liste qui va ajouter les nluveaux impliquants essentiels a la liste d'origine*/
            bool continuer = true;/*booleen qui indique qu'il y'a encore possibilité de reduction*/
            do
            {
                addEss = extraitImpEss();

                if (addEss.Count != 0)
                {/*si il y'a de nouveaux essentiels*/
                    Console.WriteLine("il y a encore des essentiels");
                    ImpEss.AddRange(addEss);/*on les ajoute a la liste*/
                    if (NonCouverts(ImpEss).Count != 0)
                    {/*si il n'y a pas de couverture complete*/
                        Console.WriteLine("couverture non complete !");
                        SupEss();/*on supprime les essentiels (et les mintermes coverts) de la matrice*/
                    }
                    else
                    {/*il y'a couverture complete, la simplification est effectuee, on s'arrete*/
                        Console.WriteLine("coverture complete!");
                        return 1;
                    }
                }
                else
                {/*on n'a pas pu extraire des essentiels, on essaye de supprimer les colonnes\lignes dominantes\dominees*/

                    /**supprimer colonnes dominantes**/
                    /***supprimer lignes dominees**/
                    /**if(aucune colonne ou ligne supprimee){continuer=false};**/
                    continuer = (supColDom() || supligDom());
                }

            } while (continuer);
            /*on sort de la boucle si: il n'a pas couverture complete et il n'a plus d'essentiels a extraire ni de colonnes\lignes a supprimer ,
            c'est a dire passage a petrick*/
            //tabvarPetrick();
            Console.WriteLine(" affichage de la matrice apres reduction");
            affichematrice((List<List<byte>>)Min_Imp, (List<ImpPrem>)ImpPrems);
            Console.WriteLine("le nombre d'elemnet dans cette matrice est" + ImpPrems.Count());
            if (ImpPrems.Count() == 1) { Empliquantfinale.Add(ImpPrems[0].Imp); }
            if ((ImpPrems.Count() != 0) && (ImpPrems.Count() != 1))
            {
                int k = 0;
                petrick = new string[mintermes.Count];
                for (int i = 0; i < mintermes.Count; i++)
                {
                    for (int j = 0; j < ImpPrems.Count; j++)
                    {
                        if (Min_Imp[i][j] == 1)
                        {
                            petrick[k] = petrick[k] + "P" + j + "+";
                        }
                    }
                    petrick[k] = petrick[k].Remove(petrick[k].Length - 1);
                    Console.WriteLine(petrick[k]);
                    k++;
                }

                fonctionpetrick();
                Petrik petrik = new Petrik();
                // Empliquantfinale = petrik.fonctionFinalePetrik(VP);// ilaq ad ughal void, //////////////////////////////////////////////////////////
                petrik.fonctionFinalePetrik(VP);
                return -1;
            }
            else { return 1; }
        }
        //on genere la fonction de petrick 
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public void fonctionpetrick()
        {

            for (int cpt = 0; cpt < petrick.Length; cpt++)
            {
                VP = VP + petrick[cpt] + "*";
            }
            VP = VP.Remove(VP.Length - 1);
            Console.WriteLine(VP);
        }

        /*****************************fin procedure generale de reduction de la matrice*****************************/
        public void affichematrice(List<List<byte>> matriceBOOl, List<ImpPrem> combi)
        {
            Console.WriteLine("affichage de la matrice ");
            Console.WriteLine("_____________________________________________________________________________ ");

            for (int j = 0; j < combi.Count; j++)
            {


                for (int i = 0; i < matriceBOOl.Count; i++)
                {
                    Console.Write("|" + matriceBOOl[i][j]);
                    //  if (matriceBOOl[i][j] == 1) { Console.Write(" "); }

                }
                ImpPrem v = (ImpPrem)combi[j];
                Console.Write("|| " + v.Imp + "  ||");
                v.affichecombinaisons();

                Console.WriteLine();

            }
            Console.WriteLine("__________________________________________________________________________________ ");
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        //                                  les methodes qui calculent le cout minimale pour la partie petrik
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        // elle renvoi le cout d'un minterme premier essentiel
        //exemple : 10x1x0 son cout est 4
        public int coutTerm(string term)
        {
            int resultat = 0;
            for (int i = 0; i < term.Length; i++)
            {
                if (term[i] != 'x')
                {
                    resultat++;
                }
            }
            return resultat;
        }
        /*_______________________________________________________________________________________________________________________________________*/
        // le cout d'une expression d'une combinaison par exemple 01*02*03 et qui la  somme des cout de ces temres chaqu'un calcule avec la methode coutTerm
        public int cout(string Term)
        {
            int cout = 0;
            String[] spearator = { "*" };
            String[] minterm = Term.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < minterm.Length; i++)
            {

                int pos = Int16.Parse(minterm[i]);

                ImpPrem v = (ImpPrem)ImpPrems[pos];

                cout = cout + coutTerm((string)v.Imp); // ca position dans la matrice 

            }

            return cout;
        }
        /*_______________________________________________________________________________________________________________________________________*/
        // pour faire le test 
        // elle gener uniquement la matrice de qmc
        public void test()
        {
            Min_Imp = GenerTabEss(mintermes, ImpPrems);
            affichematrice((List<List<byte>>)Min_Imp, (List<ImpPrem>)ImpPrems);
        }
        /*_______________________________________________________________________________________________________________________________________*/
        // elle renvoi la liste des termes a cout min de la matrice apres traitement 
        public List<string> TermMin(string expression)

        {
            int COUTMIN = 0;
            List<string> Impliquants = new List<string>();


            String[] spearator = { "+" };
            String[] terms = expression.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                COUTMIN = (int)cout(terms[0]);
                Impliquants.Add(terms[0]);
                try
                {
                    for (int i = 1; i < terms.Length; i++)

                    {


                        if (COUTMIN > (int)cout(terms[i]))
                        {

                            Impliquants.Clear();
                            Impliquants.Add(terms[i]);
                            COUTMIN = (int)cout(terms[i]);
                        }
                        else
                        {

                            if (COUTMIN == (int)cout(terms[i]))
                            {

                                Impliquants.Add(terms[i]);
                            }
                        }

                    }
                }
                catch (ArgumentOutOfRangeException) { return Impliquants; }

            }
            catch (ArgumentOutOfRangeException) { return Impliquants; }
            // Console.WriteLine("le cout minimale est " + COUTMIN);


            return Impliquants;
        }
        /***********************************************************************************************************/
        // elle renvoie la liste d'impliquant finale : petrik + Qmc 
        public void ImpiquantF(string expression)
        {
            List<string> IMPFinale = new List<string>();
            List<string> ImpliquantsPetrik = TermMin(expression);

            try
            {
                for (int i = 0; i < ImpliquantsPetrik.Count; i++)
                {
                    String[] spearator = { "*" };
                    String[] minterms = ImpliquantsPetrik[i].Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < minterms.Length; j++)
                    {
                        //  IMPFinale.Add(ImpPrems[Int16.Parse(minterms[j])].Imp);
                        //  int max = ImpPrems[Int16.Parse(minterms[j])].combinaisons.Max();

                        // Impliquantfinale impliquantfinalepassage = new Impliquantfinale(ImpPrems[Int16.Parse(minterms[j])].Imp, max);
                        Empliquantfinale.Add(ImpPrems[Int16.Parse(minterms[j])].Imp);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e) { }
            // on ajoute les minter covert par la methode de qmc   


            foreach (var v in ImpEss)
            {
                //IMPFinale.Add(v.Imp.Imp);
                /*  if (egalitedejaDansEmpliquantfinale(v.Imp.Imp, Empliquantfinale)!=1) {
                      int max = v.Imp.combinaisons.Max();


                     // Impliquantfinale impliquantfinalepassage = new Impliquantfinale(v.Imp.Imp, max);
                       Empliquantfinale.Add(v.Imp.Imp); 
                    //  Empliquantfinale.Add(impliquantfinalepassage);
                  }*/
                int max = v.Imp.combinaisons.Max();


                // Impliquantfinale impliquantfinalepassage = new Impliquantfinale(v.Imp.Imp, max);
                Empliquantfinale.Add(v.Imp.Imp);
            }


            // return IMPFinale;
        }

        //______________________________________ les traitement d'affichage de resultat finale _____________________________________________//
        public string ConvertesseurBinToStringForX(string mintemeBin)
        {
            string resultat = "";
            byte stop = 0;
            while (stop != 1)
            {
                int ASCCI = 0;
                for (int i = 0; i < mintemeBin.Length; i++)
                {
                    char c = mintemeBin[i];
                    if (c != 'x')
                    {
                        string str = ConverteString(mintemeBin[i], ASCCI);
                        resultat = resultat + str;


                    }
                    ASCCI++;
                }
                stop = 1;
            }


            return resultat.Substring(0, resultat.Length - 1);

        }
        //_________________________________________________________________________________________________________________________________
        public string ConverteString(char var, int ascii)
        {

            if (var != 'x')
            {
                if (var == '0') { return "!b" + Convert.ToString(ascii, 10) + "."; }
                else
                {
                    if (var == '1') { return "b" + Convert.ToString(ascii, 10) + "."; }
                }


            }
            return "erreur";

        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public string ConverterMintermesansX(string impl, int tailleImpl)
        {



            if ((impl.Length - tailleImpl) != 0)
            {
                string Impliquant = impl.Substring((impl.Length - tailleImpl), (tailleImpl));

                return ConvertesseurBinToStringForX(Impliquant);

            }
            else
            {

                return ConvertesseurBinToStringForX(impl);
            }

        }
        //----------------------------------------------------------------------------------------------------------------------------------
        public string ConverterMinterme(List<string> impls)
        {
            string result = "";


            foreach (string impl in impls)

            {

                if (EstILessentielle(impl) == 1)
                {
                    result = result + " + " + ConvertesseurBinToStringForX(impl);
                }
                else
                {
                    if (EstILessentielle(impl) == -1)
                    {
                        //  int decimalvar = Int16.Parse(impl);

                        int mindec = Convert.ToInt32(impl, 2);
                        string minBIn = Convert.ToString(mindec, 2);
                        Console.WriteLine("min : " + minBIn);
                        result = result + " + " + ConverterMintermesansX(impl, minBIn.Length);
                    }
                }
            }


            return result.Substring(2, result.Length - 2);

        }
        //_________________________________________________________________________________________________________
        // retur 1 true : sont egale 
        // return -1 : sont pas egale 
        // return 0 si il y une erreur 
        public short egaliteMinterme(string A, string B)
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
        //_________________________________________________________________________________________________________
        // retur 1 true : il exite deja ce string 
        // return -1 :il n'exite pas deja ce string  
        // return 0 si il y une erreur 
        public short egalitedejaDansEmpliquantfinale(string A, List<string> B)
        {
            short result = 0;
            if (B.Count != 0)
            {
                foreach (string impl in B)
                {
                    result = egaliteMinterme(A, impl);
                    if (result == 1) { return 1; } //cette element existe deja
                }
                return -1; // y pas cette element
            }
            else { return -1; } //la list est vide
        }
        //_________________________________________________________________________________________________________
        // return 1 : il contient un X exp : 1x01
        // return -1 : si sy a pas de X exp : 1011
        public static short EstILessentielle(string A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == 'x') { return 1; }
            }
            return -1;
        }
        public void CleaninglisteFinale(List<string> A)
        {
            int i = 0;
            while ((i < A.Count) && (A.Count != 0))
            {
                int j = i + 1;
                while ((j < A.Count) && (A.Count != 0))
                {
                    short result = egaliteMinterme(A[i], A[j]);
                    if (result == 1) { A.Remove(A[j]); }
                    else { j++; }//cette element existe deja
                }
                i++;
            }
        }
    }
}
