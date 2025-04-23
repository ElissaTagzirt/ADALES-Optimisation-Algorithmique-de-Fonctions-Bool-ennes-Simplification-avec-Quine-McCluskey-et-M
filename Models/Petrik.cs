using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Models
{

    //========================================================================================================================================================//
    //************************************************************* class  Maxterme *************************************************************************//
    //======================================================================================================================================================//
    public class Maxterme : variables
    {
        public List<variables> maxterme = new List<variables>();
        public variables termX; //X=X+X*Y
        public variables CombineVariablesSave;   //une variable de passage dans les traitement
        public List<variables> maxtermepassage = new List<variables>();

        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public void affichecombinevariablesave() { CombineVariablesSave.afficheListe(); }
        public void affichemaxtermepassage()
        {
            foreach (var val in maxtermepassage)
            {
                Console.WriteLine("---------"); val.afficheListe(); Console.WriteLine("---------");
            }
        }


        //============================================================ les methode ========================================================================//
        public Maxterme() { }
        public Maxterme(List<variables> Maxterme)
        { this.maxterme = Maxterme; }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public variables getCombineVariablessave() { return CombineVariablesSave; }
        public void setCombineVariablesSave(variables a) { this.CombineVariablesSave = a; }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public void RemoveMaxterme(variables varA) { this.maxterme.Remove(varA); }

        public List<variables> getMaxterme() { return maxterme; }

        public variables getMaxtermeVariablePos(int i) { return maxterme[i]; }
        public void SetMaxterme(List<variables> maxterme) { this.maxterme = maxterme; }
        public void AddMaxterme(variables Terme) { maxterme.Add(Terme); }
        public int MaxtermeCount() { return maxterme.Count(); }
        public variables getMaxtermePos(int i) { return maxterme[i]; }
        public void afficheMaxterme()
        {

            foreach (variables valure in maxterme)
            {
                Console.Write("||");
                valure.afficheListe();

            }
            Console.WriteLine("\n-----------------------------------------");
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public void termXClear() { termX.supp(); }
        public void settermX(variables termX) { this.termX = termX; }
        public variables gettermX() { return termX; }




        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public string GetExpressionFromMaxtermes(List<variables> maxt)
        {
            string resulat = "";
            foreach (variables val in maxt)
            {
                string Spassage = GetExpressionFromVariables(val.getvars());

                resulat = addCharBegginig(resulat, Spassage, '+');
            }
            return resulat.Substring(0, resulat.Length - 1);


        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public string GetExpressionFromMaxtermesForDidj(List<variables> maxt)
        {
            string resulat = "";
            foreach (variables val in maxt)
            {
                string Spassage = GetExpressionFromVariablesForDidj(val.getvars());

                resulat = addCharBegginig(resulat, Spassage, '+');
            }
            return resulat.Substring(0, resulat.Length - 1);


        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        // verifie l'existance d'une partie de terme // X et X*Y
        public short ExistePartie(List<string> Min, List<string> Max)
        {
            if ((Min.Count != 0) && (Max.Count != 0))
            {
                foreach (string valure in Min)
                {
                    if (Max.Contains(valure) == false) { return -1; }
                }
                return 1;
            }
            else { return -1; }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        // verifie l'egalite d'une chaine de caractere  0 : les deux liste vide 
        //                                            1 : pour l'egalite des deux liste en element 
        //                                            2 : pour l'existance d'une partie dans la liste // on recupere la partie dans termx
        //                                           -1 : y ne exalite ni existance 
        public short Egalite(List<string> A, List<string> B)

        {
            if ((A.Count != 0) && (B.Count != 0))
            {
                if (A.Count == B.Count) //teste l'egalite
                {
                    foreach (string valure in A)
                    {
                        if (B.Contains(valure) == false) { return -1; } // -1 c'est faut
                    }
                    return 1; // c'est vrai
                }
                else
                {
                    List<string> Min; List<string> Max;
                    if (A.Count < B.Count) { Min = A; Max = B; } else { Min = B; Max = A; }
                    if (ExistePartie(Min, Max) == 1)
                    {
                        variables passage = new variables(Min);
                        settermX(passage);
                        return 2; // existe une partie de A dans B
                    }
                    else { return -1; }
                }

            }
            else { return 0; }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        // verifie l'existance de deux terme simulaire X = X  0 : les deux liste vide 
        //                                            1 : deux terme simulaire X = X  
        //                                            2 : pour l'existance d'une partie de terme // on recupere la partie dans termx
        //                                           -1 :  ni exalite ni existance

        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public short TheresVariable(Maxterme A, Maxterme B)
        {
            short result = 0;
            byte tow = 0;
            if ((A.MaxtermeCount() != 0) && (B.MaxtermeCount() != 0))
            {
                for (int a = 0; a < A.MaxtermeCount(); a++)
                {
                    for (int b = 0; b < B.MaxtermeCount(); b++)
                    {

                        //TabTermes tabTerA = tabTermesA[cpt];
                        variables passageA = A.getMaxtermePos(a);
                        variables passageB = B.getMaxtermePos(b);
                        result = Egalite(passageA.getvars(), passageB.getvars());
                        if (result == 2) { tow = 1; } //vrai 
                        if (result == 1) { return 1; }

                    }
                }
                if (tow == 1) { return 2; } else { return -1; }

            }
            else { return 0; }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public short CombineVariables(variables A, variables B)
        {
            if ((A.getvars().Count() != 0) && (B.getvars().Count() != 0))
            {
                short v = Egalite(A.getvars(), B.getvars());
                if (v == 1) { this.CombineVariablesSave = A; return 1; }
                if (v == 2)
                {
                    if (A.Countvars() < B.Countvars()) { this.CombineVariablesSave = B; return 2; } else { this.CombineVariablesSave = A; return 2; }
                }

                if (v == -1)
                {
                    List<string> result = new List<string>();
                    foreach (string s in A.getvars()) { result.Add(s); }
                    foreach (string s in B.getvars())
                    {
                        if (result.Contains(s) != true) { result.Add(s); }
                    }
                    variables variables = new variables(result);
                    this.CombineVariablesSave = variables;
                    return -1;
                }
            }
            return 0;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public void CombineVariableWithMaxterme(variables A, List<variables> B)

        {
            short stop = 0; // il ocuppe moin de bit que un bool
            short v;
            int b = 0;

            while ((stop == 0) && (b < B.Count()) && (B.Count() != 0))
            {
                variables variableB = new variables(B[b].getvars());
                v = CombineVariables(A, variableB);
                if (v == 1)
                {
                    // this.maxtermepassage.Clear();
                    this.maxtermepassage.Add(A);
                    stop = 1;
                }
                if (v == 2) { this.maxtermepassage.Add(this.CombineVariablesSave); /**/ }
                if (v == -1) { this.maxtermepassage.Add(this.CombineVariablesSave); }
                b++;
            }

        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public void CombineMAxterme(List<variables> A, List<variables> B)
        {
            int a = 0;
            while (A.Count() != 0)
            {
                variables varA = A[0];
                CombineVariableWithMaxterme(varA, B);
                A.Remove(varA);
                Nettoyage(this.maxtermepassage);

            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//
        public void Nettoyage(List<variables> tab)
        {
            int T = 0;
            int i = 0;

            while (T < tab.Count())
            {
                variables varA = new variables();
                varA = tab[T];
                i = T + 1;
                short stop = 0;
                while ((i < tab.Count()) && (stop == 0))
                {
                    short v;
                    variables varB = new variables();
                    varB = tab[i];
                    v = Egalite(varA.getvars(), varB.getvars());
                    if (v == 1) { tab.Remove(varB); }
                    if (v == 2)
                    {
                        if (varA.Countvars() > varB.Countvars())
                        {
                            tab[T] = tab[i];
                            varA = tab[i];
                            stop = 1;
                            tab.Remove(varB);
                            i = T + 1;


                        }
                        else { tab.Remove(varB); }
                    }
                    if (v == -1) { i++; }
                }
                T++;
            }

        }


    }

    //=======================================================================================================================================================//
    //************************************************************* class  variables ***********************************************************************//
    //======================================================================================================================================================//
    public class variables
    {
        public List<string> vars = new List<string>();

        public variables(List<string> va) { this.vars = va; }
        public variables() { }
        //============================================================ les methode ========================================================================//
        public List<string> getvars() { return vars; }
        public void setvars(List<string> va) { this.vars = va; }
        public int Countvars() { return this.vars.Count(); }
        public void Addvars(List<string> va) { this.vars = va; }
        public void supp() { this.vars.Clear(); }
        //------------------------------------------------------------------------------------------------------------------------------------//
        public void afficheListe()
        {
            Console.Write("[");

            foreach (string val in vars)
            { Console.Write(val + ","); }
            Console.Write("]");
        }
        //------------------------------------------------------------------------------------------------------------------------------------//
        public string supprimeP(string str)
        {

            string[] charsToRemove = new string[] { "P", "p", };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            return str;
        }
        //------------------------------------------------------------------------------------------------------------------------------------//
        public string addCharBegginig(string str, string chA, char chB)
        {
            return chA + chB + str.Substring(0, str.Length);
        }
        //------------------------------------------------------------------------------------------------------------------------------------//
        public string addCharending(string str, string chA)
        {
            return str.Substring(0, str.Length) + chA;
        }
        //------------------------------------------------------------------------------------------------------------------------------------//
        public string GetExpressionFromVariables(List<string> varibles)
        {
            string resulat = "";
            foreach (string val in varibles)
            {
                string Spassage = supprimeP(val);

                resulat = addCharBegginig(resulat, Spassage, '*');
            }
            return resulat.Substring(0, resulat.Length - 1);


        }
        //------------------------------------------------------------------------------------------------------------------------------------//
        public string GetExpressionFromVariablesForDidj(List<string> varibles)
        {
            string resulat = "";
            foreach (string val in varibles)
            {
                resulat = addCharBegginig(resulat, val, '*');
            }
            return resulat.Substring(0, resulat.Length - 1);


        }


    }
    //======================================================================================================================================================//
    //**************************************************** class Petrik ************************************************************************************//
    //======================================================================================================================================================//
    public class Petrik : Maxterme
    {

        List<Maxterme> TermesPetrikII = new List<Maxterme>();
        List<Maxterme> TermesPetrik = new List<Maxterme>(); // la table principale
        public short v;
        public int pos;
        public short tow;
        public int a;
        //============================================================ les methode ========================================================================//

        public Petrik() { }
        public Petrik(List<Maxterme> TermesPetrik) { this.TermesPetrik = TermesPetrik; }
        public void AddTermesPetrik(Maxterme val) { this.TermesPetrik.Add(val); }
        public List<Maxterme> getpetrikone() { return this.TermesPetrik; }
        public List<Maxterme> getpetriktow() { return this.TermesPetrikII; }


        //------------------------------------------------------------------------------------------------------------------------------------//
        public void RecuperVariables(string expression)
        {
            // List<variables> resul = new List<variables>();
            // ImpliquantsEss resulte = new ImpliquantsEss(resul);
            String[] spearator = { "*" };
            String[] spearat = { "+" };
            String[] Sommes = expression.Split(spearator, StringSplitOptions.RemoveEmptyEntries);// K +J, k+ M
            foreach (var val in Sommes)
            {
                // Console.WriteLine("val" + val);
                String[] varProduit = val.Split(spearat, StringSplitOptions.RemoveEmptyEntries);   // K  J  

                Maxterme passageA = new Maxterme();
                for (int cpt = 0; cpt < varProduit.Length; cpt++)
                {
                    variables passage = new variables();

                    passage.getvars().Add(varProduit[cpt]); //addlistestring
                    passageA.AddMaxterme(passage);

                }
                AddTermesPetrik(passageA);

            }

        }
        //------------------------------------------------------------------------------------------------------------------------//
        public void affichagePetrickliste(List<Maxterme> tab)
        {
            //  Console.WriteLine("le nombre d'elemnt dans tab justepour verifie " + tab.Count());
            for (int i = 0; i < tab.Count(); i++)
            {

                tab[i].afficheMaxterme();
            }

        }
        //---------------------------------------------------------------------------------------------------------------------------//

        public void CombinedeuxlignePetrik()
        {

            Maxterme maxA = new Maxterme();
            Maxterme maxB = new Maxterme();
            Maxterme maxterme = new Maxterme();



            this.a = 1;
            this.pos = 0;
            short stop = 0; this.tow = 0;
            this.v = 0;


            if (this.TermesPetrik.Count() == 1)
            {
                maxA = this.TermesPetrik[0];

                foreach (variables val in maxA.getMaxterme()) { maxterme.getMaxterme().Add(val); }
                this.TermesPetrikII.Add(maxterme);

                this.TermesPetrik.Clear();

                stop = 1;
            }
            if (this.TermesPetrik.Count() != 0)

            {
                maxA = this.TermesPetrik[0];
                while ((this.a < this.TermesPetrik.Count()) && (stop != 1))
                {
                    maxB = this.TermesPetrik[a];
                    this.v = TheresVariable(maxA, maxB);
                    if (this.v == 1) { stop = 1; }
                    if (this.v == 2) { this.pos = a; this.tow = 1; }
                    this.a++; // bbb
                }


                if (this.v == 1)
                {
                    CombineMAxterme(maxA.getMaxterme(), maxB.getMaxterme());

                    foreach (variables val in this.maxtermepassage) { maxterme.getMaxterme().Add(val); }
                    Nettoyage(maxterme.getMaxterme());
                    this.TermesPetrikII.Add(maxterme);
                    this.TermesPetrik.Remove(maxB);
                    this.TermesPetrik.Remove(maxA);
                    this.maxtermepassage.Clear();


                }
                else
                {
                    if (this.tow == 1)
                    {
                        maxB = this.TermesPetrik[pos];
                        CombineMAxterme(maxA.getMaxterme(), maxB.getMaxterme());
                        foreach (variables val in this.maxtermepassage) { maxterme.getMaxterme().Add(val); }
                        Nettoyage(maxterme.getMaxterme());
                        this.TermesPetrikII.Add(maxterme);
                        this.TermesPetrik.Remove(maxB);
                        this.TermesPetrik.Remove(maxA);
                        this.maxtermepassage.Clear();

                    }
                    else
                    {
                        CombineMAxterme(maxA.getMaxterme(), maxB.getMaxterme());
                        foreach (variables val in this.maxtermepassage) { maxterme.getMaxterme().Add(val); }
                        Nettoyage(maxterme.getMaxterme());
                        this.TermesPetrikII.Add(maxterme);
                        this.TermesPetrik.Remove(maxB);
                        this.TermesPetrik.Remove(maxA);
                        this.maxtermepassage.Clear();

                    }
                }

            }
        }




        //----------------------------------------------------------------------------------------------------------------------------// 
        public void CombineTableauPetrik()
        {
            int cpt = 0;
            while (TermesPetrik.Count() != 0)
            {

                affichagePetrickliste(getpetrikone());
                CombinedeuxlignePetrik();

                affichagePetrickliste(getpetriktow());


            }
        }
        //----------------------------------------------------------------------------------------------------------------------------//
        public void CombineToutPetrik()
        {
            short stop = 0;
            while (stop != 1)
            {

                CombineTableauPetrik();

                if (this.TermesPetrikII.Count() == 1) { stop = 1; }
                else
                {

                    foreach (Maxterme VAR in TermesPetrikII) { TermesPetrik.Add(VAR); }

                    this.TermesPetrikII.Clear();
                }
            }


        }


        //----------------------------------------------------------------------------------------------------------------------------//
        // recupere l'expression disjonctive a utilise dans la deuxieme partie de petrik 
        public string getexpressionForPetrik(string expression)
        {
            RecuperVariables(expression);
            CombineToutPetrik();
            return GetExpressionFromMaxtermes(this.TermesPetrikII[0].getMaxterme());
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        // recupere l'expression disjonctive
        public string getExpressionDijonctive(string expression)
        {
            RecuperVariables(expression);
            CombineToutPetrik();
            return GetExpressionFromMaxtermesForDidj(this.TermesPetrikII[0].getMaxterme());
        }
        //----------------------------------------------------------------------------------------------------------------------------//
        //la fonction finale de petrik 
        ImpliquantsEss Imp = new ImpliquantsEss();
        public void fonctionFinalePetrik(string expression)
        {
            Imp.ImpiquantF(getexpressionForPetrik(expression));
            //return ImpliquantsEss.getEmpliquantfinale();

        }
        //---------------------------------------------------------------------------------------------------------------------------//
    }
}
