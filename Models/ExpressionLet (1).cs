using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
//----------------------------------------------------------------------------------------------------------------------
namespace APPLICATION.Models
{


    public class ExpressionLet : TabTermes
    {

        //Empliquantfinale

        public short activeNum = 0; // si activelitterale = on active l'option de genre  les minterme on litterale
        public static short activeLet = 0; // si 1 on est dans le traitement numerique   
        private List<TabTermes> tabTermes = new List<TabTermes>();
        private List<TabTermes> tabTermesPremie = new List<TabTermes>();
        static List<string> Var = new List<string>();
        private int nbtermes = 0;
        static int nbVariables = 0;
        private int GroupeMAX = 0;
        const int Max = 32768;
        private int itiration = 0;
        void increment() { this.itiration++; }
        //================================================================================================
        int decimals;
        // char virgule;
        int groupe;
        string minterme;
        byte check;
        // int entiermax = 0;
        //  TabTermes term;
        //=================================================================================================
        // d'abord on classe les mintermes dans une liste pour parvenir  extraire le nombre de variables 
        //=================================================================================================
        public List<TabTermes> gettabTermes() { return this.tabTermes; }
        public ExpressionLet() { }
        public List<TabTermes> gettabTermesPremie() { return this.tabTermesPremie; }
        public List<String> getVar() { return Var; }
        public static string getVarPos(int i) { return Var.ElementAt(i); }
        public void settabTermes(List<TabTermes> tabtermes) { this.tabTermes = tabtermes; }
        public void SettabTermes(List<TabTermes> tab) { this.tabTermes = tab; }
        public void setGroupeMax(int newgroupemax) { this.GroupeMAX = newgroupemax; }
        public void setnbVariables(int var) { nbVariables = var; }
        public int getGroupeMAX() { return this.GroupeMAX; }

        //========================================================================
        public string Reconstruction(string expression)
        {
            string pos = "'";
            string resultat = "";
            String[] spearator = { "+", "!", "&", "^", " " };
            String[] imp = expression.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '!')
                {
                    resultat = resultat + expression[i + 1] + pos[0];
                    i++;
                }
                else { resultat = resultat + expression[i]; }
            }
            Console.WriteLine(resultat);
            activeLet = 1;
            for (int a = 0; a < imp.Length; a++)
            {
                string c = imp[a];
                if (Var.Contains(c) == false)
                {
                    Var.Add(imp[a]);
                }
            }
            Var.Sort();
            return resultat;

        }
        //-----------------------------------------------------------------------------------------------------------------------------

        public short egaliteduxstring(string a, string b)
        {
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i]) { return -1; }  // abc acc
                }
                return 1; // element egale // il existe dans la list
            }
            else { return -1; }
        }

        public short existedansliste(List<string> ls, string a)
        {
            if (ls.Count != 0)
            {   // y pas cette element dans liste
                foreach (string v in ls)
                {
                    if (egaliteduxstring(a, v) == 1) { return 1; }//l'element exite dans liste
                }
                return -1;
            }
            else { return -1; }
        }





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
            string resultat = "";
            string st = "";
            String[] spearator = { "&" };
            String[] Variables = mintermlateralle.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Variables.Length; i++)
            {
                st = ConverteVariableLatEnNum(Variables[i]);
                resultat = resultat + st;
            }
            if (nbVariables < Variables.Length)
            {
                int var = (int)Variables.Length;
                setnbVariables((int)var);
            }
            return Convert.ToInt32(resultat, 2);
        }
        //---------------------------------------------------------------------------------------------------------------------------

        public string ConverteVariableLatEnNum(string var)
        {
            string varStr = "'";
            if (var.Length == 1) { return "1"; }
            else
            {

                if (var[var.Length - 1] == varStr[0]) { return "0"; }
                else { return "1"; }
            }
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



                    TabTermes tabTerB = tabTermesA[ptr];

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
                // Console.WriteLine("liste A vide ");
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

                //  Console.WriteLine("tabTeB avant supprimer redandant " + tabTermesb.Count());
                tabTermesb = supprimeredandant(tabTermesb);
                //  Console.WriteLine("tabTeB apres supprimer redandant " + tabTermesb.Count());
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

            //afficherList(tabTermesPremie);


        }
        //----------------------------------------------------------------------------------------

        public List<TabTermes> supprimeredandant(List<TabTermes> tabTermesb)
        {

            short stop = 0;
            int i = 0;

            while ((stop != 1) && (i < tabTermesb.Count))
            {
                for (int j = i + 1; j < tabTermesb.Count; j++)
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
    //----------------------------------------------------------- public class ANF : Formula ---------------------------------------------------------
    public class ANF : Formula
    {
        public ANF(string formula)
          : base(formula)
        {
            if (!this.isNotANF(formula))
                throw new Exception("Not ANF!");
        }

        private bool isNotANF(string formula)
        {
            bool flag = true;
            string[] source = formula.Split('^');
            for (int index = 0; index < source.Length && flag; ++index)
            {
                if (((IEnumerable<string>)source).Contains<string>('^'.ToString()) || ((IEnumerable<string>)source).Contains<string>('+'.ToString()) || ((IEnumerable<string>)source).Contains<string>('\''.ToString()))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }
    //------------------------------------------------------- public class BDDTree -------------------------------------------------------------
    /* public class BDDTree
     {
         private string myTT;
         private string[] list_of_Sub = new string[100];
         private Node[] list_of_del = new Node[100];
         private Node myRoot;
         private Node currentNode;
         private int myOrder;
         private int mySize;
         private int maxLevel;
         private int myDepth;
         private StringBuilder PrintString;
         private Dictionary<string, int> matrix;
         private string[][] lineMatrix;
         private int br;

         public Node MyRoot => this.myRoot;

         public int Size() => this.mySize;

         public int Depth => this.myDepth;

         public Dictionary<string, int> Matrix => this.matrix;

         public string[][] LineMatrix => this.lineMatrix;

         public BDDTree(string TT)
         {
             this.myTT = int.TryParse(Math.Log((double)TT.Length, 2.0).ToString(), out int _) ? TT : throw new ArgumentOutOfRangeException("vector", "Векторът не е с дължина отговаряща на таблично представена функция");
             this.list_of_Sub[0] = TT;
             for (int index = 1; index < 100; ++index)
             {
                 this.list_of_Sub[index] = "-1";
                 this.list_of_del = (Node[])null;
             }
             this.myRoot = new Node(); //du genre on lui alloue un noeud 
             this.myRoot.myValue = TT; // on initialise la valeur d la racine 
             this.myRoot.myLevel = 0; // niveau de la racine est a 0
             this.myRoot.traversed = false; // noeud non parcouru
             this.myRoot.child0 = (Node)null; //fils gauche a null
             this.myRoot.child1 = (Node)null; //fils droit a null
             this.myRoot.myParent = (Node)null; //le parent est a null
             this.myOrder = int.Parse((Math.Log((double)TT.Length) / Math.Log(2.0)).ToString());
             this.currentNode = this.myRoot; // le noeud courant est la racine 
             this.mySize = 0; //le size est a 0
             this.myDepth = 1;
             this.maxLevel = 0;
         }*/

    /* public bool deleted(Node child) //retourne vrai si le noeud existe dans la liste des noeuds supprimés si non on le supprime et on le place dans 
     {
         int index;
         for (index = 0; this.list_of_del[index] != null; ++index)
         {
             if (child == this.list_of_del[index])
                 return true;
         }
         this.list_of_del[index] = child;
         return false;
     }*/

    /*  public bool AddSub(string subF) // je pense c quand on ajoute une valeur 
      {
          int index = 0;
          while (this.list_of_Sub[index] != "-1" && !(subF == this.list_of_Sub[index]))
              ++index;
          if (subF == this.list_of_Sub[index])
              return false; // cest a dire il existe deja du coup on va pas lajouter 
          this.list_of_Sub[index] = subF; //on lajoute et on retourne vrai ajout par succes 
          return true;
      }*/

    /*  public Node AttachChild(string subF)
      {
          Node currentNode = this.currentNode;
          if (currentNode.myValue == subF)
          {
              this.currentNode = this.myRoot;
              return currentNode;
          }
          if (currentNode.child0 != null)
          {
              this.currentNode = currentNode.child0;
              Node node = this.AttachChild(subF);
              if (node != null)
                  return node;
          }
          if (currentNode.child1 != null)
          {
              this.currentNode = currentNode.child1;
              Node node = this.AttachChild(subF);
              if (node != null)
                  return node;
          }
          return (Node)null;
      }

      public void Initialize()
      {
          Node currentNode = this.currentNode; //treecode 
          string str = this.currentNode.myValue; //str prend la valeur du noeud en cours 
          ++this.mySize; //on incrémente le size 
          if (this.currentNode.myValue.Length > 1)
          {
              string subF1 = str.Substring(0, str.Length / 2);
              string subF2 = str.Substring(str.Length / 2, str.Length / 2);
              if (this.currentNode == this.myRoot)
              {
                  for (; subF1 == subF2 && subF1.Length > 0; subF2 = subF2.Substring(subF2.Length / 2, subF2.Length / 2))
                  {
                      this.AddSub(subF1);
                      this.myRoot.myLevel = int.Parse((Math.Log((double)(this.myTT.Length / subF1.Length)) / Math.Log(2.0)).ToString());
                      this.myRoot.myValue = subF1;
                      subF1 = subF1.Substring(0, subF1.Length / 2);
                  }
                  if (this.myRoot.myValue.Length == 1)
                      return;
              }
              for (; subF1.Length > 1 && subF1.Substring(0, subF1.Length / 2) == subF1.Substring(subF1.Length / 2, subF1.Length / 2); subF1 = subF1.Substring(0, subF1.Length / 2))
                  this.AddSub(subF1);
              for (; subF2.Length > 1 && subF2.Substring(0, subF2.Length / 2) == subF2.Substring(subF2.Length / 2, subF2.Length / 2); subF2 = subF2.Substring(0, subF2.Length / 2))
                  this.AddSub(subF2);
              if (this.AddSub(subF1))
              {
                  this.currentNode = currentNode;
                  this.newChild(this.currentNode, 0, subF1);
                  this.currentNode = currentNode.child0;
                  this.Initialize();
              }
              else
              {
                  this.currentNode = this.myRoot;
                  currentNode.child0 = this.AttachChild(subF1);
              }
              if (this.AddSub(subF2))
              {
                  this.currentNode = currentNode;
                  this.newChild(this.currentNode, 1, subF2);
                  this.currentNode = currentNode.child1;
                  this.Initialize();
              }
              else
              {
                  this.currentNode = this.myRoot;
                  currentNode.child1 = this.AttachChild(subF2);
              }
          }
          this.matrix = new Dictionary<string, int>();
          this.lineMatrix = new string[this.mySize * 2 - 2][];
          for (int index = 0; index < this.mySize * 2 - 2; ++index)
              this.lineMatrix[index] = new string[2];
          this.currentNode = this.myRoot;
      }

      public void newChild(Node parent, int childNum, string value)
      {
          Node node = new Node();
          node.myValue = value;
          node.myLevel = int.Parse((Math.Log((double)(this.myTT.Length / value.Length)) / Math.Log(2.0)).ToString());
          node.traversed = false;
          node.child0 = (Node)null;
          node.child1 = (Node)null;
          switch (childNum)
          {
              case 0:
                  parent.child0 = node;
                  break;
              case 1:
                  parent.child1 = node;
                  break;
          }
          if (node.myLevel <= this.maxLevel)
              return;
          this.maxLevel = node.myLevel;
          ++this.myDepth;
      }

      public string PrintTree()
      {
          if (this.PrintString == null)
              this.PrintString = new StringBuilder();
          Node currentNode = this.currentNode;
          if (currentNode.traversed)
              return "";
          currentNode.traversed = true;
          if (this.currentNode == this.myRoot)
              this.PrintString.Append("Tree depth: " + (object)this.myDepth + "\nTree size:" + (object)this.mySize + "\nTree level: Node Value:  0Child: 1Child\n");
          this.PrintString.Append(currentNode.myLevel.ToString() + "\t" + currentNode.myValue + "\t");
          this.matrix.Add(currentNode.myValue, currentNode.myLevel);
          if (currentNode.child0 != null)
          {
              this.lineMatrix[this.br][0] = currentNode.myValue;
              this.lineMatrix[this.br][1] = currentNode.child0.myValue;
              ++this.br;
              this.PrintString.Append(currentNode.child0.ToString() + "\t");
          }
          else
              this.PrintString.Append("null\t");
          if (currentNode.child1 != null)
          {
              this.lineMatrix[this.br][0] = currentNode.myValue;
              this.lineMatrix[this.br][1] = currentNode.child1.myValue;
              ++this.br;
              this.PrintString.Append((object)currentNode.child1);
              this.PrintString.Append("\n");
          }
          else
              this.PrintString.Append("null\n");
          if (currentNode.child0 != null)
          {
              this.currentNode = currentNode.child0;
              this.PrintTree();
          }
          this.currentNode = currentNode;
          if (currentNode.child1 != null)
          {
              this.currentNode = currentNode.child1;
              this.PrintTree();
          }
          this.currentNode = this.myRoot;
          return this.PrintString.ToString();
      }

      public string draw(Node node)
      {
          if (node == null)
              return "empty";
          if (node.child0 == null && node.child1 == null)
              return node.myValue;
          if (node.child0 != null && node.child1 == null)
              return node.myValue + "(" + this.draw(node.child0) + ", _)";
          if (node.child1 != null && node.child0 == null)
              return node.myValue + "(_, " + this.draw(node.child1) + ")";
          return node.myValue + "(" + this.draw(node.child0) + ", " + this.draw(node.child1) + ")";
      }
  }*/
    //------------------------------------------------------ public enum BooleanOperations --------------------------------------------------
    public enum BooleanOperations
    {
        Not,
        Or,
        And,
        Xor,
    }
    //----------------------------------------------------------- public class CNF : Formula-----------------------------------------------------
    public class CNF : Formula
    {
        public CNF(string formula)
          : base(formula)
        {
            if (!this.isCNF(formula))
                throw new Exception("Not CNF!");
        }

        private bool isCNF(string formula)
        {
            bool flag = true;
            string[] subString = formula.Split('&');
            for (int i = 0; i < subString.Length && flag; ++i)
            {
                if (((IEnumerable<string>)subString).Count<string>((Func<string, bool>)(s => s.Equals(subString[i]))) != 1)
                {
                    flag = false;
                    break;
                }
                foreach (char key in this.VariableNames.Keys)
                {
                    if (!subString[i].Contains<char>(key))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
    }
    //----------------------------------------------------- public class DNF : Formula ------------------------------------------------------------
    public class DNF : Formula
    {
        public static string result;

        public static string getresult() { return result; }
        public DNF(string formula)
          : base(formula)
        {
            Console.WriteLine(" val formula : " + formula);
            result = formula;
            if (!this.isDNF(formula))
                throw new Exception("Not DNF!");
        }

        private bool isDNF(string formula)
        {
            bool flag = true;
            string[] subString = formula.Split('+');
            for (int i = 0; i < subString.Length && flag; ++i)
            {
                if (((IEnumerable<string>)subString).Count<string>((Func<string, bool>)(s => s.Equals(subString[i]))) != 1)
                {
                    flag = false;
                    break;
                }
                foreach (char key in this.VariableNames.Keys)
                {
                    if (!subString[i].Contains<char>(key))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------
    public class Formula : IBooleanFunctionImpl
    {
        private const char LP = '(';
        private const char RP = ')';
        private const char ZERO = '0';
        private const char ONE = '1';
        private string normalized = "Not Given";
        private int numberOfVariables;
        private string formula;
        private Dictionary<char, int> variableNames;

        public Dictionary<char, int> VariableNames => this.variableNames;

        public Formula(string function)
        {
            char[] charArray = function.ToCharArray();
            SortedDictionary<char, int> sortedDictionary = new SortedDictionary<char, int>();
            int length = function.Trim().Length;
            int num1 = -1;
            for (int index = 0; index < length; ++index)
            {
                if (charArray[index] != '\'' && charArray[index] != '&' && charArray[index] != '+' && charArray[index] != '^' && charArray[index] != '(' && charArray[index] != ')' && charArray[index] != '0' && charArray[index] != '1' && charArray[index] != ' ' && !sortedDictionary.ContainsKey(charArray[index]))
                {
                    ++num1;
                    sortedDictionary.Add(charArray[index], num1);
                }
            }
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            int num2 = 0;
            foreach (char key in sortedDictionary.Keys)
                dictionary[key] = num2++;
            this.formula = function;
            this.variableNames = dictionary;
            this.numberOfVariables = dictionary.Count;
            StringBuilder stringBuilder = new StringBuilder(this.formula.Trim());
            for (int index = 0; index < stringBuilder.Length; ++index)
            {
                char c = stringBuilder[index];
                if (c == ' ')
                    stringBuilder.Remove(index--, 1);
                else if (!char.IsLetter(c) && c != '(' && c != ')' && c != '\'' && c != '&' && c != '+' && c != '^' && c != '0' && c != '1')
                    throw new ApplicationException(string.Format("Invalid character {0} in expression:\n {1} = AND\n  {2} = OR\n  {3} = XOR\n  {4} = NOT (postfix)\n Parentheses, 0, and 1 are okay too.", (object)c, (object)'&', (object)'+', (object)'^', (object)'\''));
            }
            for (int index = 0; index < stringBuilder.Length - 1; ++index)
            {
                char c1 = stringBuilder[index];
                char c2 = stringBuilder[index + 1];
                if (c1 == '(' && c2 == ')')
                {
                    stringBuilder.Insert(index + 1, '1');
                }
                else
                {
                    bool flag1 = char.IsLetter(c1) || c1 == '0' || c1 == '1';
                    bool flag2 = char.IsLetter(c2) || c2 == '0' || c2 == '1';
                    if (c1 == ')' && (c2 == '(' || flag2) || flag1 && (c2 == '(' || flag2) || c1 == '\'' && (c2 == '(' || flag2))
                        stringBuilder.Insert(index + 1, '&');
                }
            }
            int num3 = 0;
            for (int index = 0; index < stringBuilder.Length; ++index)
            {
                char ch = stringBuilder[index];
                if (ch == '(')
                    ++num3;
                if (ch == ')')
                    --num3;
                if (num3 < 0)
                    throw new ApplicationException("Badly nested parentheses");
            }
            if (num3 != 0)
                throw new ApplicationException("Unbalanced parentheses");
            stringBuilder.Insert(0, '('.ToString());
            stringBuilder.Insert(stringBuilder.Length, ')'.ToString());
            this.normalized = stringBuilder.ToString();
        }

        public Formula(bool value)
        {
            this.numberOfVariables = 0;
            this.formula = value ? "1" : "0";
            this.normalized = this.formula;
            this.variableNames = (Dictionary<char, int>)null;
        }
        public override string ToString() => this.formula;

        public int NumberOfVariables => this.numberOfVariables;

        public bool IsConstant => this.numberOfVariables == 0;

        public IEnumerable<char> Variables()
        {
            foreach (char ch in (IEnumerable<char>)this.VariableNames.Keys.OrderBy<char, string>((Func<char, string>)(x => x.ToString())))
                yield return ch;
        }

        public void ChangeVariableName(char PrevName, char Newname)
        {
            foreach (int key in this.variableNames.Keys)
            {
                if (key == (int)Newname)
                    throw new ArgumentException("Променлива с такова име вече същесвува.");
            }
            int num;
            this.variableNames.TryGetValue(PrevName, out num);
            this.variableNames.Remove(PrevName);
            this.variableNames.Add(Newname, num);
            char[] charArray = this.formula.ToCharArray();
            for (int index = 0; index < charArray.Length; ++index)
            {
                if ((int)charArray[index] == (int)PrevName)
                    charArray[index] = Newname;
            }
            this.formula = new string(charArray);
        }
        public int Eval(char[] varMask, bool[] valueMask)
        {
            StringBuilder stringBuilder = new StringBuilder(this.normalized);
            Stack<char> charStack = new Stack<char>();
            Stack<char> operands = new Stack<char>();
            for (int index1 = 0; index1 < varMask.Length; ++index1)
            {
                char newChar = valueMask[index1] ? '1' : '0';
                for (int index2 = 0; index2 < stringBuilder.Length; ++index2)
                {
                    if ((int)stringBuilder[index2] == (int)varMask[index1])
                        stringBuilder.Replace(stringBuilder[index2], newChar);
                }
            }
            for (int index = 0; index < stringBuilder.Length; ++index)
            {
                char ch1 = stringBuilder[index];
                switch (ch1)
                {
                    case '&':
                        if (charStack.Peek() == '\'')
                            this.EvalOperand(operands, charStack.Pop());
                        charStack.Push(ch1);
                        break;
                    case '\'':
                        char ch2 = operands.Count != 0 ? operands.Pop() : throw new ApplicationException("Syntax Error: Postfix NOT with no operand");
                        operands.Push(ch2 == '0' ? '1' : '0');
                        break;
                    case '(':
                        charStack.Push(ch1);
                        break;
                    case ')':
                        char oper;
                        while ((oper = charStack.Pop()) != '(')
                            this.EvalOperand(operands, oper);
                        break;
                    case '+':
                    case '^':
                        if (charStack.Peek() == '\'')
                            this.EvalOperand(operands, charStack.Pop());
                        while (charStack.Peek() == '&')
                            this.EvalOperand(operands, charStack.Pop());
                        charStack.Push(ch1);
                        break;
                    case '0':
                    case '1':
                        operands.Push(ch1);
                        break;
                    default:
                        throw new ApplicationException("Грешка: Невалиден оператор.");
                }
            }
            while (charStack.Count != 0)
                this.EvalOperand(operands, charStack.Pop());
            return operands.Pop() != '1' ? 0 : 1;
        }

        protected void EvalOperand(Stack<char> operands, char oper)
        {
            switch (oper)
            {
                case '&':
                    char ch1 = operands.Count != 0 ? operands.Pop() : throw new ApplicationException(string.Format("Синтактична грешка: оператор {0} няма операнди.", (object)'&'));
                    char ch2 = operands.Count != 0 ? operands.Pop() : throw new ApplicationException(string.Format("Синтактична грешка: {0} Липсващ десен операнд.", (object)'&'));
                    operands.Push(ch1 != '1' || ch2 != '1' ? '0' : '1');
                    break;
                case '+':
                    char ch3 = operands.Count != 0 ? operands.Pop() : throw new ApplicationException(string.Format("Синтактична грешка:оператор {0} няма операнди.", (object)'+'));
                    char ch4 = operands.Count != 0 ? operands.Pop() : throw new ApplicationException(string.Format("Синтактична грешка: {0} Липсващ десен операнд.", (object)'+'));
                    operands.Push(ch3 == '1' || ch4 == '1' ? '1' : '0');
                    break;
                case '^':
                    char ch5 = operands.Count != 0 ? operands.Pop() : throw new ApplicationException(string.Format("Синтактична грешка:оператор {0} няма операнди.", (object)'^'));
                    char ch6 = operands.Count != 0 ? operands.Pop() : throw new ApplicationException(string.Format("Синтактична грешка: {0} Липсващ десен операнд.", (object)'^'));
                    operands.Push(ch5 == '1' && ch6 == '0' || ch5 == '0' && ch6 == '1' ? '1' : '0');
                    break;
                default:
                    throw new ApplicationException("Грешка: " + (object)oper + " не е оператор.");
            }
        }

        public IBooleanFunctionImpl Restrict(char[] varMask, bool[] valueMask)
        {
            if (this.numberOfVariables == 0)
                return (IBooleanFunctionImpl)new Formula(this.Eval(new char[1]
                {
          'a'
                }, new bool[1]) == 1);
            if (varMask.Length != valueMask.Length)
                throw new ArgumentOutOfRangeException("Броят на променливите е различен от боря на стойностите.");
            if (varMask.Length > this.VariableNames.Count)
                throw new ArgumentOutOfRangeException("Променливите в маската са повече от променливите във фукцията.");
            char[] charArray = new StringBuilder(this.normalized).ToString().ToCharArray();
            StringBuilder stringBuilder = new StringBuilder();
            for (int index1 = 0; index1 < charArray.Length; ++index1)
            {
                char ch = charArray[index1];
                if (((IEnumerable<char>)varMask).Contains<char>(ch) && ch != '(' && ch != ')' && ch != '\'' && ch != '&' && ch != '+' && ch != '^' && ch != '0' && ch != '1')
                {
                    int index2 = -1;
                    for (int index3 = 0; index3 < varMask.Length; ++index3)
                    {
                        if ((int)charArray[index1] == (int)varMask[index3])
                        {
                            index2 = index3;
                            break;
                        }
                    }
                    if (valueMask[index2])
                        stringBuilder.Append("1");
                    else
                        stringBuilder.Append("0");
                }
                else
                    stringBuilder.Append(charArray[index1]);
            }
            Formula formula = new Formula(stringBuilder.ToString());
            if (formula.numberOfVariables != 0)
                return (IBooleanFunctionImpl)formula;
            return (IBooleanFunctionImpl)new Formula(formula.Eval(new char[1]
            {
        'a'
            }, new bool[1]) == 1);
        }

        public IBooleanFunctionImpl Apply(
          BooleanOperations operation,
          IBooleanFunctionImpl bfi)
        {
            Formula formula = (Formula)bfi;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            stringBuilder.Append(this.formula);
            stringBuilder.Append(") ");
            switch (operation)
            {
                case BooleanOperations.Or:
                    stringBuilder.Append('+');
                    break;
                case BooleanOperations.And:
                    stringBuilder.Append('&');
                    break;
                case BooleanOperations.Xor:
                    stringBuilder.Append('^');
                    break;
                default:
                    throw new ApplicationException("Невалидна операция.");
            }
            stringBuilder.Append(" (");
            stringBuilder.Append(formula.formula);
            stringBuilder.Append(")");
            return (IBooleanFunctionImpl)new Formula(stringBuilder.ToString());
        }

        public IBooleanFunctionImpl Compose(char varName, IBooleanFunctionImpl bfi)
        {
            Formula formula1 = (Formula)this.Restrict(new char[1]
            {
        varName
            }, new bool[1] { true });
            Formula formula2 = (Formula)this.Restrict(new char[1]
            {
        varName
            }, new bool[1]);
            return ((Formula)formula1.Apply(BooleanOperations.And, bfi)).Apply(BooleanOperations.Or, formula2.Apply(BooleanOperations.And, bfi.Inverse()));
        }

        public IBooleanFunctionImpl SwapVar(char varName1, char varName2)
        {
            int num1;
            this.variableNames.TryGetValue(varName1, out num1);
            int num2;
            this.variableNames.TryGetValue(varName2, out num2);
            char[] charArray = this.formula.ToCharArray();
            for (int index = 0; index < charArray.Length; ++index)
            {
                if ((int)charArray[index] == (int)varName1)
                    charArray[index] = varName2;
                else if ((int)charArray[index] == (int)varName2)
                    charArray[index] = varName1;
            }
            Formula formula = new Formula(new string(charArray));
            formula.variableNames.Remove(varName1);
            formula.variableNames.Add(varName1, num2);
            formula.variableNames.Remove(varName2);
            formula.variableNames.Add(varName2, num1);
            return (IBooleanFunctionImpl)formula;
        }

        public IBooleanFunctionImpl Inverse()
        {
            StringBuilder stringBuilder = new StringBuilder(this.normalized);
            int index = -1;
            do
            {
                ++index;
                char c = stringBuilder[index];
                if (char.IsLetter(c) && index == stringBuilder.Length - 1)
                {
                    stringBuilder.Insert(index + 1, '\'');
                    break;
                }
                if (index != stringBuilder.Length - 1)
                {
                    char ch = stringBuilder[index + 1];
                    if (char.IsLetter(c) && ch != '\'')
                        stringBuilder.Insert(index + 1, '\'');
                    else if (char.IsLetter(c) && ch == '\'' && index != stringBuilder.Length - 1)
                        stringBuilder.Remove(index + 1, 1);
                }
                else
                    break;
            }
            while (index != stringBuilder.Length - 1);
            return (IBooleanFunctionImpl)new Formula(stringBuilder.ToString());
        }

        public bool IsIsomorphicTo(IBooleanFunctionImpl bfi) => throw new NotImplementedException();

        public bool IsEssential(char varName) => this.numberOfVariables != 0 && this.ToTruthTable().IsEssential(varName);

        public bool Equals(IBooleanFunctionImpl bfi) => !(this.GetType() != bfi.GetType()) && this.formula == ((Formula)bfi).formula;
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------
    public interface IBooleanFunctionImpl
    {
        bool IsConstant { get; }

        int NumberOfVariables { get; }

        IEnumerable<char> Variables();

        void ChangeVariableName(char PrevName, char NewName);
        int Eval(char[] varMask, bool[] valueMask);

        IBooleanFunctionImpl Restrict(char[] varMask, bool[] valueMask);

        IBooleanFunctionImpl Apply(
         BooleanOperations operation,
         IBooleanFunctionImpl bfi);

        IBooleanFunctionImpl Compose(char varName, IBooleanFunctionImpl bfi);

        IBooleanFunctionImpl SwapVar(char varName1, char varName2);

        IBooleanFunctionImpl Inverse();

        bool IsIsomorphicTo(IBooleanFunctionImpl bfi);

        bool IsEssential(char varName);
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    public static class ImplConverter
    {

        public static IBooleanFunctionImpl ToDNF(this TruthTable tt)
        {
            List<int> intList = new List<int>();
            for (int index = 0; index < tt.Vector.Length; ++index)
            {
                if (tt.Vector[index] == 1)
                    intList.Add(index);
            }
            int numberOfVariables = tt.NumberOfVariables;
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            for (int index = 0; index < numberOfVariables; ++index)
                dictionary.Add(Convert.ToChar((int)Convert.ToInt16('a') + index), index);
            char[] chArray = new char[numberOfVariables];
            char[] array = dictionary.Keys.ToArray<char>();
            StringBuilder stringBuilder = new StringBuilder();
            int num1 = -1;
            foreach (int num2 in intList)
            {
                ++num1;
                string str1 = Convert.ToString(num2, 2);
                string str2 = str1;
                if (str1.Length < numberOfVariables)
                {
                    for (int index = 0; index < numberOfVariables - str1.Length; ++index)
                        str2 = "0" + str2;
                }
                int[] vector = tt.StringToVector(str2);
                for (int index = 0; index < vector.Length; ++index)
                {
                    if (vector[index] == 1)
                    {
                        stringBuilder.Append(array[index]);
                    }
                    else
                    {
                        stringBuilder.Append(array[index]);
                        stringBuilder.Append('\'');
                    }
                    if (index != vector.Length - 1)
                        stringBuilder.Append('&');
                }
                if (num1 < intList.Count - 1)
                    stringBuilder.Append('+');
            }
            return (IBooleanFunctionImpl)new DNF(stringBuilder.ToString());
        }

        public static IBooleanFunctionImpl ToCNF(this TruthTable tt)
        {
            List<int> source = new List<int>();
            for (int index = 0; index < tt.Vector.Length; ++index)
            {
                if (tt.Vector[index] == 0)
                    source.Add(index);
            }
            int numberOfVariables = tt.NumberOfVariables;
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            for (int index = 0; index < numberOfVariables; ++index)
                dictionary.Add(Convert.ToChar((int)Convert.ToInt16('a') + index), index);
            char[] chArray = new char[numberOfVariables];
            char[] array = dictionary.Keys.ToArray<char>();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (int num in source)
            {
                string str1 = Convert.ToString(num, 2);
                string str2 = str1;
                if (str1.Length < numberOfVariables)
                {
                    for (int index = 0; index < numberOfVariables - str1.Length; ++index)
                        str2 = "0" + str2;
                }
                int[] vector = tt.StringToVector(str2);
                stringBuilder.Append("(");
                for (int index = 0; index < vector.Length; ++index)
                {
                    if (vector[index] == 0)
                    {
                        stringBuilder.Append(array[index]);
                    }
                    else
                    {
                        stringBuilder.Append(array[index]);
                        stringBuilder.Append('\'');
                    }
                    if (index != vector.Length - 1)
                        stringBuilder.Append('+');
                }
                stringBuilder.Append(")");
                if (num != source.Last<int>())
                    stringBuilder.Append('&');
            }
            return (IBooleanFunctionImpl)new CNF(stringBuilder.ToString());
        }

        public static IBooleanFunctionImpl ToANF(this TruthTable tt)
        {
            int numberOfVariables = tt.NumberOfVariables;
            char[] chArray1 = new char[numberOfVariables];
            foreach (KeyValuePair<char, int> variableName in tt.VariableNames)
                chArray1[variableName.Value] = variableName.Key;
            string[][,] strArray1 = new string[numberOfVariables + 1][,];
            strArray1[0] = new string[int.Parse(Math.Pow(2.0, (double)numberOfVariables).ToString()), 1];
            for (int index = 0; (double)index < Math.Pow(2.0, (double)numberOfVariables); ++index)
                strArray1[0][index, 0] = tt.Vector[index].ToString();
            StringBuilder stringBuilder1 = new StringBuilder();
            for (int index1 = 0; index1 < numberOfVariables; ++index1)
            {
                int length = int.Parse(Math.Pow(2.0, (double)(numberOfVariables - index1 - 1)).ToString());
                strArray1[index1 + 1] = new string[length, 2];
                for (int index2 = 0; index2 < length; ++index2)
                {
                    if (index1 != 0)
                    {
                        strArray1[index1 + 1][index2, 0] = strArray1[index1][2 * index2, 0] + strArray1[index1][2 * index2, 1];
                        int[] array1 = ((IEnumerable<int>)ImplConverter.StringToVector(strArray1[index1][2 * index2 + 1, 0].ToString())).Concat<int>((IEnumerable<int>)ImplConverter.StringToVector(strArray1[index1][2 * index2 + 1, 1].ToString())).ToArray<int>();
                        int[] array2 = ((IEnumerable<int>)ImplConverter.StringToVector(strArray1[index1][2 * index2, 0].ToString())).Concat<int>((IEnumerable<int>)ImplConverter.StringToVector(strArray1[index1][2 * index2, 1].ToString())).ToArray<int>();
                        for (int index3 = 0; index3 < array1.Length; ++index3)
                        {
                            if (array1[index3] != array2[index3])
                            {
                                string[,] strArray2;
                                IntPtr index4;
                                (strArray2 = strArray1[index1 + 1])[(int)(index4 = (IntPtr)index2), 1] = strArray2[(int)index4, 1] + "1";
                            }
                            else
                            {
                                string[,] strArray3;
                                IntPtr index5;
                                (strArray3 = strArray1[index1 + 1])[(int)(index5 = (IntPtr)index2), 1] = strArray3[(int)index5, 1] + "0";
                            }
                        }
                    }
                    else
                    {
                        strArray1[index1 + 1][index2, 0] = strArray1[index1][2 * index2, 0];
                        int[] vector1 = ImplConverter.StringToVector(strArray1[index1][2 * index2 + 1, 0].ToString());
                        int[] vector2 = ImplConverter.StringToVector(strArray1[index1][2 * index2, 0].ToString());
                        for (int index6 = 0; index6 < vector1.Length; ++index6)
                            strArray1[index1 + 1][index2, 1] = vector1[index6] == vector2[index6] ? "0" : "1";
                    }
                }
            }
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append(strArray1[numberOfVariables][0, 0]);
            stringBuilder2.Append(strArray1[numberOfVariables][0, 1]);
            int[] vector3 = ImplConverter.StringToVector(stringBuilder2.ToString());
            List<int> intList = new List<int>();
            for (int index = 0; index < vector3.Length; ++index)
            {
                if (vector3[index] == 1)
                    intList.Add(index);
            }
            Dictionary<char, int> dictionary1 = new Dictionary<char, int>();
            for (int index = 0; index < numberOfVariables; ++index)
                dictionary1.Add(Convert.ToChar((int)Convert.ToInt16('a') + index), index);
            char[] chArray2 = new char[numberOfVariables];
            char[] array = dictionary1.Keys.ToArray<char>();
            StringBuilder stringBuilder3 = new StringBuilder();
            int num1 = -1;
            foreach (int num2 in intList)
            {
                ++num1;
                string str1 = Convert.ToString(num2, 2);
                string str2 = str1;
                if (str1.Length < numberOfVariables)
                {
                    for (int index = 0; index < numberOfVariables - str1.Length; ++index)
                        str2 = "0" + str2;
                }
                int[] vector4 = tt.StringToVector(str2);
                if (!((IEnumerable<int>)vector4).Contains<int>(1))
                    stringBuilder3.Append("1");
                for (int index = 0; index < vector4.Length; ++index)
                {
                    if (vector4[index] == 1)
                        stringBuilder3.Append(array[index]);
                }
                if (num1 < intList.Count - 1)
                    stringBuilder3.Append('^');
            }
            ANF anf = new ANF(stringBuilder3.ToString());
            anf.VariableNames.Clear();
            Dictionary<char, int> dictionary2 = new Dictionary<char, int>();
            for (int index = 0; index < chArray1.Length; ++index)
                anf.VariableNames.Add(chArray1[index], index);
            return (IBooleanFunctionImpl)anf;
        }

        public static int[] StringToVector(string s)
        {
            int[] vector = new int[s.Length];
            char[] charArray = s.ToCharArray();
            for (int index = 0; index < charArray.Length; ++index)
            {
                switch (charArray[index])
                {
                    case '0':
                        vector[index] = 0;
                        break;
                    case '1':
                        vector[index] = 1;
                        break;
                    default:
                        throw new ArgumentException("Невалидно инициализиране. Векторът съдържа символи различни от 0 и 1.", "vector");
                }
            }
            return vector;
        }

        public static TruthTable ToTruthTable(this Formula formula)
        {
            int numberOfVariables = formula.NumberOfVariables;
            if (numberOfVariables == 0)
                return new TruthTable(formula.Eval(new char[1]
                {
          'a'
                }, new bool[1]) == 1);
            int[] vector = new int[(int)Math.Pow(2.0, (double)numberOfVariables)];
            char[] varMask = new char[numberOfVariables];
            foreach (KeyValuePair<char, int> variableName in formula.VariableNames)
                varMask[variableName.Value] = variableName.Key;
            for (int index1 = 0; (double)index1 < Math.Pow(2.0, (double)numberOfVariables); ++index1)
            {
                string str1 = Convert.ToString(index1, 2);
                string str2 = str1;
                if (str1.Length < numberOfVariables)
                {
                    for (int index2 = 0; index2 < numberOfVariables - str1.Length; ++index2)
                        str2 = "0" + str2;
                }
                bool[] valueMask = new bool[numberOfVariables];
                char[] charArray = str2.ToCharArray();
                for (int index3 = 0; index3 < charArray.Length; ++index3)
                {
                    switch (charArray[index3])
                    {
                        case '0':
                            valueMask[index3] = false;
                            break;
                        case '1':
                            valueMask[index3] = true;
                            break;
                        default:
                            throw new ArgumentException("Невалидно инициализиране. Векторът съдържа символи различни от 0 и 1.", "vector");
                    }
                }
                vector[index1] = formula.Eval(varMask, valueMask);
            }
            TruthTable truthTable = new TruthTable(vector);
            truthTable.VariableNames.Clear();
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            for (int index = 0; index < varMask.Length; ++index)
                truthTable.VariableNames.Add(varMask[index], index);
            return truthTable;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------
    public class TruthTable : IBooleanFunctionImpl
    {
        public const int MaxNumberOfVariables = 15;
        private const uint AMASK = 32768;
        private int numberOfVariables;
        private Dictionary<char, int> variableNames;
        private int[] vector;

        public Dictionary<char, int> VariableNames => this.variableNames;

        public int[] Vector => this.vector;

        public bool IsConstant
        {
            get
            {
                if (this.vector.Length == 1)
                    return true;
                int num1 = 0;
                int num2 = 0;
                for (int index = 0; index < this.vector.Length; ++index)
                {
                    num1 += this.vector[index] == 0 ? 1 : 0;
                    num2 += this.vector[index] == 1 ? 1 : 0;
                }
                return num1 == 0 || num2 == 0;
            }
        }

        public TruthTable(int numberOfVariables)
        {
            if (numberOfVariables < 1)
                throw new ArgumentOutOfRangeException(nameof(numberOfVariables), "Броят на променливите трябва да бъде >0");
            this.numberOfVariables = numberOfVariables <= 15 ? numberOfVariables : throw new ArgumentOutOfRangeException(nameof(numberOfVariables), "Броят на променливите трябва да бъде < " + 8.ToString());
            this.CreateDictionary(numberOfVariables);
            int num1 = (int)Math.Pow(2.0, (double)numberOfVariables);
            StringBuilder stringBuilder = new StringBuilder();
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] data = new byte[1];
            Random random = new Random((DateTime.Now.Hour + DateTime.Now.Millisecond) % (DateTime.Now.Second + 1));
            for (int index = 0; index < num1; ++index)
            {
                cryptoServiceProvider.GetBytes(data);
                int num2 = DateTime.Now.Millisecond / 10 % 2;
                int num3 = random.Next(2) + Convert.ToInt32(Math.Floor(random.NextDouble())) % 2;
                stringBuilder.Append((num2 + num3 + (int)data[0]) % 2);
            }
            this.vector = this.StringToVector(stringBuilder.ToString());
        }

        public TruthTable(int[] vector)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector), "Параметърът vector е null");
            int result;
            if (!int.TryParse(Math.Log((double)vector.Length, 2.0).ToString(), out result))
                throw new ArgumentOutOfRangeException(nameof(vector), "Векторът не е с дължина отговаряща на таблично представена функция");
            if (result < 1)
                throw new ArgumentOutOfRangeException(nameof(numberOfVariables), "Броят на променливите трябва да бъде >0");
            this.vector = (int[])vector.Clone();
            this.numberOfVariables = result;
            this.CreateDictionary(result);
        }

        public TruthTable(TruthTable tt)
          : this(tt.vector)
        {
        }

        public TruthTable(string vector)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector), "Параметърът vector е null");
            string str = !(vector.Substring(0, 2) == "0x") ? vector : this.HexToBin(vector.Substring(1, vector.Length - 1));
            int result;
            if (!int.TryParse(Math.Log((double)str.Length, 2.0).ToString(), out result))
                throw new ArgumentOutOfRangeException(nameof(vector), "Векторът не е с дължина отговаряща на таблично представена функция");
            if (result < 1)
                throw new ArgumentOutOfRangeException(nameof(numberOfVariables), "Броят на променливите трябва да бъде >0");
            this.numberOfVariables = result <= 8 ? result : throw new ArgumentOutOfRangeException(nameof(numberOfVariables), "Броят на променливите трябва да бъде <= " + 8.ToString());
            this.CreateDictionary(result);
            this.vector = this.StringToVector(str);
        }

        public TruthTable(bool value)
        {
            this.numberOfVariables = 0;
            this.vector = new int[1];
            this.vector[0] = value ? 1 : 0;
            this.variableNames = (Dictionary<char, int>)null;
        }

        private void CreateDictionary(int n)
        {
            this.variableNames = new Dictionary<char, int>();
            for (int index = 0; index < n; ++index)
                this.variableNames.Add(Convert.ToChar((int)Convert.ToInt16('a') + index), index);
        }

        public int[] StringToVector(string str)
        {
            int[] vector = new int[str.Length];
            char[] charArray = str.ToCharArray();
            for (int index = 0; index < charArray.Length; ++index)
            {
                switch (charArray[index])
                {
                    case '0':
                        vector[index] = 0;
                        break;
                    case '1':
                        vector[index] = 1;
                        break;
                    default:
                        throw new ArgumentException("Невалидно инициализиране. Векторът съдържа символи различни от 0 и 1.", "vector");
                }
            }
            return vector;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < this.vector.Length - 1; ++index)
                stringBuilder.AppendFormat("{0}", (object)this.vector[index]);
            stringBuilder.AppendFormat("{0}", (object)this.vector[this.vector.Length - 1]);
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        public string HexString()
        {
            StringBuilder stringBuilder1 = new StringBuilder("0x");
            StringBuilder stringBuilder2 = new StringBuilder();
            for (int index = 0; index < this.vector.Length; index += 4)
            {
                stringBuilder2.AppendFormat("{0}{1}{2}{3}", (object)this.vector[index], (object)this.vector[index + 1], (object)this.vector[index + 2], (object)this.vector[index + 3]);
                switch (stringBuilder2.ToString())
                {
                    case "0000":
                        stringBuilder1.Append("0");
                        break;
                    case "0001":
                        stringBuilder1.Append("1");
                        break;
                    case "0010":
                        stringBuilder1.Append("2");
                        break;
                    case "0011":
                        stringBuilder1.Append("3");
                        break;
                    case "0100":
                        stringBuilder1.Append("4");
                        break;
                    case "0101":
                        stringBuilder1.Append("5");
                        break;
                    case "0110":
                        stringBuilder1.Append("6");
                        break;
                    case "0111":
                        stringBuilder1.Append("7");
                        break;
                    case "1000":
                        stringBuilder1.Append("8");
                        break;
                    case "1001":
                        stringBuilder1.Append("9");
                        break;
                    case "1010":
                        stringBuilder1.Append("A");
                        break;
                    case "1011":
                        stringBuilder1.Append("B");
                        break;
                    case "1100":
                        stringBuilder1.Append("C");
                        break;
                    case "1101":
                        stringBuilder1.Append("D");
                        break;
                    case "1110":
                        stringBuilder1.Append("E");
                        break;
                    case "1111":
                        stringBuilder1.Append("F");
                        break;
                }
                stringBuilder2.Clear();
            }
            return stringBuilder1.ToString();
        }

        private string HexToBin(string HexString)
        {
            string bin = "";
            for (int index = 0; index < HexString.Length; ++index)
            {
                switch (HexString[index])
                {
                    case '0':
                        bin += "0000";
                        break;
                    case '1':
                        bin += "0001";
                        break;
                    case '2':
                        bin += "0010";
                        break;
                    case '3':
                        bin += "0011";
                        break;
                    case '4':
                        bin += "0100";
                        break;
                    case '5':
                        bin += "0101";
                        break;
                    case '6':
                        bin += "0110";
                        break;
                    case '7':
                        bin += "0111";
                        break;
                    case '8':
                        bin += "1000";
                        break;
                    case '9':
                        bin += "1001";
                        break;
                    case 'A':
                    case 'a':
                        bin += "1010";
                        break;
                    case 'B':
                    case 'b':
                        bin += "1011";
                        break;
                    case 'C':
                    case 'c':
                        bin += "1100";
                        break;
                    case 'D':
                    case 'd':
                        bin += "1101";
                        break;
                    case 'E':
                    case 'e':
                        bin += "1110";
                        break;
                    case 'F':
                    case 'f':
                        bin += "1111";
                        break;
                }
            }
            return bin;
        }

        public int NumberOfVariables => this.numberOfVariables;

        public IEnumerable<char> Variables()
        {
            foreach (char ch in (IEnumerable<char>)this.VariableNames.Keys.OrderBy<char, string>((Func<char, string>)(x => x.ToString())))
                yield return ch;
        }

        public void ChangeVariableName(char PrevName, char NewName)
        {
            foreach (int key in this.variableNames.Keys)
            {
                if (key == (int)NewName)
                    throw new ArgumentException("Променлива с такова име вече същесвува.");
            }
            int num;
            this.variableNames.TryGetValue(PrevName, out num);
            this.variableNames.Remove(PrevName);
            this.variableNames.Add(NewName, num);
        }

        public IBooleanFunctionImpl Reduce()
        {
            if (this.variableNames == null)
                return (IBooleanFunctionImpl)new TruthTable(this.vector[0] == 1);
            List<char> charList = new List<char>();
            foreach (KeyValuePair<char, int> variableName in this.variableNames)
            {
                if (((TruthTable)this.Restrict(new char[1]
                {
          variableName.Key
                }, new bool[1] { true })).Equals((object)(TruthTable)this.Restrict(new char[1]
                {
          variableName.Key
                }, new bool[1])))
                    charList.Add(variableName.Key);
            }
            if (charList.Count != 0 && charList.Count != this.numberOfVariables)
            {
                char[] varMask = new char[charList.Count];
                bool[] valueMask = new bool[charList.Count];
                int index = 0;
                foreach (char ch in charList)
                {
                    varMask[index] = ch;
                    valueMask[index] = true;
                    ++index;
                }
                return this.Restrict(varMask, valueMask);
            }
            return charList.Count != 0 && charList.Count == this.numberOfVariables ? (IBooleanFunctionImpl)new TruthTable(this.vector[0] == 1) : (IBooleanFunctionImpl)this;
        }

        public int Eval(char[] varMask, bool[] valueMask)
        {
            if (varMask.Length != valueMask.Length)
                throw new ArgumentOutOfRangeException("Броят на променливите е различен от боря на стойностите.");
            char[] chArray = new char[this.variableNames.Count];
            bool[] flagArray = new bool[this.variableNames.Count];
            int index1 = 0;
            for (int index2 = 0; index2 < varMask.Length; ++index2)
            {
                if (this.VariableNames.ContainsKey(varMask[index2]))
                {
                    chArray[index1] = varMask[index2];
                    flagArray[index1] = valueMask[index2];
                    ++index1;
                }
            }
            char[] array = this.VariableNames.Keys.ToArray<char>();
            int index3 = 0;
            for (int index4 = 0; index4 < chArray.Length; ++index4)
            {
                if ((int)array[index4] != (int)chArray[index4])
                {
                    char key = chArray[index4];
                    chArray[index4] = array[index4];
                    this.VariableNames.TryGetValue(key, out index3);
                    chArray[index3] = key;
                    bool flag = flagArray[index4];
                    flagArray[index4] = flagArray[index3];
                    flagArray[index3] = flag;
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int index5 = 0; index5 < chArray.Length; ++index5)
            {
                int num = flagArray[index5] ? 1 : 0;
                stringBuilder.Append(num);
            }
            return this.vector[Convert.ToInt32(stringBuilder.ToString(), 2)];
        }


        public IBooleanFunctionImpl Restrict(char[] varMask, bool[] valueMask)
        {
            foreach (char key in varMask)
            {
                if (!this.VariableNames.ContainsKey(key))
                    throw new ArgumentException("Променлива с такова име няма в представянето на функцията.");
            }
            if (varMask.Length != valueMask.Length)
                throw new ArgumentOutOfRangeException("Броят на променливите е различен от боря на стойностите.");
            if (varMask.Length > this.VariableNames.Count)
                throw new ArgumentOutOfRangeException("Променливите в маската са повече от променливите във фукцията.");
            StringBuilder stringBuilder = new StringBuilder();
            for (int index1 = 0; index1 < this.vector.Length; ++index1)
            {
                string str1 = Convert.ToString(index1, 2);
                string str2 = str1;
                if (str1.Length < this.numberOfVariables)
                {
                    for (int index2 = 0; index2 < this.numberOfVariables - str1.Length; ++index2)
                        str2 = "0" + str2;
                }
                int[] vector = this.StringToVector(str2);
                bool flag = false;
                for (int index3 = 0; index3 < varMask.Length; ++index3)
                {
                    int index4;
                    this.variableNames.TryGetValue(varMask[index3], out index4);
                    if (vector[index4] != (valueMask[index3] ? 1 : 0))
                    {
                        flag = false;
                        break;
                    }
                    flag = true;
                }
                if (flag)
                    stringBuilder.Append(this.vector[index1]);
            }
            if (stringBuilder.Length == 1)
                return (IBooleanFunctionImpl)new TruthTable(stringBuilder.ToString() == "1");
            TruthTable truthTable = new TruthTable(stringBuilder.ToString());
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            int num1 = 0;
            foreach (KeyValuePair<char, int> variableName in this.VariableNames)
            {
                if (!((IEnumerable<char>)varMask).Contains<char>(variableName.Key) && !dictionary.ContainsKey(variableName.Key))
                {
                    int num2 = 0;
                    this.VariableNames.TryGetValue(variableName.Key, out num2);
                    dictionary.Add(variableName.Key, num1);
                    ++num1;
                }
            }
            truthTable.variableNames.Clear();
            truthTable.variableNames = dictionary;
            return (IBooleanFunctionImpl)truthTable;
        }

        public IBooleanFunctionImpl Apply(
          BooleanOperations operation,
          IBooleanFunctionImpl bfi)
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>((IDictionary<char, int>)this.variableNames);
            char[] array = this.variableNames.Keys.ToArray<char>();
            int num1 = this.variableNames.Values.Last<int>();
            int num2 = 1;
            foreach (char variable in bfi.Variables())
            {
                if (!((IEnumerable<char>)array).Contains<char>(variable))
                {
                    dictionary.Add(variable, num1 + num2);
                    ++num2;
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int index1 = 0; (double)index1 < Math.Pow(2.0, (double)dictionary.Count); ++index1)
            {
                string str1 = Convert.ToString(index1, 2);
                string str2 = str1;
                if (str1.Length < dictionary.Count)
                {
                    for (int index2 = 0; index2 < dictionary.Count - str1.Length; ++index2)
                        str2 = "0" + str2;
                }
                int[] vector = this.StringToVector(str2);
                bool[] valueMask1 = new bool[this.variableNames.Count];
                int index3 = 0;
                foreach (KeyValuePair<char, int> variableName in this.variableNames)
                {
                    int index4 = -1;
                    if (!dictionary.TryGetValue(variableName.Key, out index4))
                        throw new ApplicationException("Невъзможно е да се извлече име на променлива от индекса й.");
                    valueMask1[index3] = vector[index4] == 1;
                    ++index3;
                }
                int num3 = this.Eval(array, valueMask1);
                bool[] valueMask2 = new bool[bfi.NumberOfVariables];
                int index5 = 0;
                foreach (KeyValuePair<char, int> variableName in ((TruthTable)bfi).variableNames)
                {
                    int index6 = -1;
                    if (!dictionary.TryGetValue(variableName.Key, out index6))
                        throw new ApplicationException("Невъзможно е да се извлече име на променлива от индекса й.");
                    valueMask2[index5] = vector[index6] == 1;
                    ++index5;
                }
                int num4 = bfi.Eval(((TruthTable)bfi).variableNames.Keys.ToArray<char>(), valueMask2);
                bool flag1 = num3 == 1;
                bool flag2 = num4 == 1;
                bool flag3;
                switch (operation)
                {
                    case BooleanOperations.Or:
                        flag3 = flag1 || flag2;
                        break;
                    case BooleanOperations.And:
                        flag3 = flag1 & flag2;
                        break;
                    case BooleanOperations.Xor:
                        flag3 = flag1 != flag2;
                        break;
                    default:
                        throw new ApplicationException("Невалидна операция.");
                }
                if (flag3)
                    stringBuilder.Append("1");
                else
                    stringBuilder.Append("0");
            }
            TruthTable truthTable = new TruthTable(stringBuilder.ToString());
            truthTable.variableNames.Clear();
            truthTable.variableNames = dictionary;
            return (IBooleanFunctionImpl)truthTable;
        }

        public IBooleanFunctionImpl Compose(char varName, IBooleanFunctionImpl bfi)
        {
            TruthTable truthTable1 = (TruthTable)null;
            if (this.NumberOfVariables == 1)
            {
                int num1 = this.Eval(new char[1] { varName }, new bool[1]
                {
          true
                });
                int num2 = this.Eval(new char[1] { varName }, new bool[1]);
                if (num1 == 1 && num2 == 1)
                {
                    TruthTable truthTable2 = (TruthTable)bfi.Inverse();
                }
                else if (num1 == 0 && num2 == 0)
                {
                    truthTable1 = new TruthTable(new int[(int)Math.Pow(2.0, (double)bfi.NumberOfVariables)]);
                    truthTable1.variableNames.Clear();
                    int num3 = 0;
                    foreach (char variable in bfi.Variables())
                    {
                        truthTable1.variableNames.Add(variable, num3);
                        ++num3;
                    }
                }
                else if (num1 == 1 && num2 == 0)
                    truthTable1 = (TruthTable)bfi;
                else if (num1 == 0 && num2 == 1)
                    truthTable1 = (TruthTable)bfi.Inverse();
            }
            else
            {
                TruthTable truthTable3 = (TruthTable)this.Restrict(new char[1]
                {
          varName
                }, new bool[1] { true });
                TruthTable truthTable4 = (TruthTable)this.Restrict(new char[1]
                {
          varName
                }, new bool[1]);
                truthTable1 = (TruthTable)((TruthTable)truthTable3.Apply(BooleanOperations.And, bfi)).Apply(BooleanOperations.Or, truthTable4.Apply(BooleanOperations.And, bfi.Inverse()));
            }
            return (IBooleanFunctionImpl)truthTable1;
        }

        public IBooleanFunctionImpl SwapVar(char OldName, char NewName)
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            int index1;
            this.variableNames.TryGetValue(OldName, out index1);
            int index2;
            this.variableNames.TryGetValue(NewName, out index2);
            for (int index3 = 0; index3 < this.vector.Length; ++index3)
            {
                string str1 = Convert.ToString(index3, 2);
                string str2 = str1;
                if (str1.Length < this.numberOfVariables)
                {
                    for (int index4 = 0; index4 < this.numberOfVariables - str1.Length; ++index4)
                        str2 = "0" + str2;
                }
                int[] vector = this.StringToVector(str2);
                int num = vector[index1];
                vector[index1] = vector[index2];
                vector[index2] = num;
                StringBuilder stringBuilder2 = new StringBuilder();
                for (int index5 = 0; index5 < vector.Length; ++index5)
                    stringBuilder2.Append(vector[index5]);
                int int32 = Convert.ToInt32(stringBuilder2.ToString(), 2);
                stringBuilder1.Append(this.vector[int32]);
            }
            TruthTable truthTable = new TruthTable(stringBuilder1.ToString());
            truthTable.variableNames.Remove(OldName);
            truthTable.variableNames.Add(OldName, index2);
            truthTable.variableNames.Remove(NewName);
            truthTable.variableNames.Add(NewName, index1);
            return (IBooleanFunctionImpl)truthTable;
        }

        public IBooleanFunctionImpl Inverse()
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>((IDictionary<char, int>)this.variableNames);
            int[] vector = new int[this.vector.Length];
            for (int index = 0; index < this.vector.Length; ++index)
                vector[index] = this.vector[index] == 1 ? 0 : 1;
            TruthTable truthTable = new TruthTable(vector);
            truthTable.variableNames.Clear();
            truthTable.variableNames = dictionary;
            return (IBooleanFunctionImpl)truthTable;
        }

        public bool IsIsomorphicTo(IBooleanFunctionImpl bfi) => throw new NotImplementedException();

        public override bool Equals(object bfi)
        {
            if (this.GetType() != bfi.GetType())
                return false;
            bool flag = true;
            for (int index = 0; index < this.Vector.Length; ++index)
            {
                if (this.Vector[index] != ((TruthTable)bfi).Vector[index])
                    return false;
            }
            return flag;
        }

        public bool IsEssential(char varName) => !((TruthTable)this.Restrict(new char[1]
        {
      varName
        }, new bool[1] { true })).Equals((object)(TruthTable)this.Restrict(new char[1]
        {
      varName
        }, new bool[1]));

        public void CrossTablesWith(TruthTable truthTable1, TruthTable truthTable2)
        {
            if (this.vector.Length != truthTable1.vector.Length || this.vector.Length != truthTable2.vector.Length)
                throw new ArgumentException("truthTable1 или truthTable2 не е на същия брой променливи");
            int num = this.vector.Length / 2;
            for (int index = 0; index < this.vector.Length / 2; ++index)
                this.vector[index] = truthTable1.vector[index];
            for (int index = this.vector.Length / 2 + 1; index < this.vector.Length; ++index)
                this.vector[index] = truthTable2.vector[index];
        }

        public void Mutate(int numberOfMutations)
        {
            Random random = new Random((DateTime.Now.Hour + DateTime.Now.Millisecond) % (DateTime.Now.Second + 1));
            int num1 = DateTime.Now.Millisecond / 10 % 2;
            int num2 = random.Next((int)Math.Floor(Math.Pow(2.0, (double)this.numberOfVariables)));
            for (int index1 = 0; index1 < numberOfMutations; ++index1)
            {
                int num3 = random.Next(this.numberOfVariables);
                int index2 = (num2 + num3) % (int)Math.Floor(Math.Pow(2.0, (double)this.numberOfVariables));
                this.vector[index2] = (this.vector[index2] + 1) % 2;
            }
        }

        private uint Pack(uint NPA, uint Mask)
        {
            uint num1 = 32768;
            uint num2 = 0;
            for (int index = 0; index < 16; ++index)
            {
                uint num3 = num1 & NPA;
                if ((num1 & Mask) != 0U)
                    num2 = (uint)(((int)num2 << 1) + (num3 != 0U ? 1 : 0));
                num1 >>= 1;
            }
            return num2;
        }

        public void ReorderSubfunctions(uint Mask, out int[] f)
        {
            f = new int[this.vector.Length];
            for (uint index = 0; (long)index < (long)this.vector.Length; ++index)
            {
                uint num1 = this.Pack(Mask & index, Mask) * ((uint)this.vector.Length / 2U);
                Mask = ~Mask;
                uint num2 = this.Pack(Mask & index, Mask);
                f[(int)(IntPtr)(num1 + num2)] = this.vector[(int)(IntPtr)index];
                Mask = ~Mask;
            }
        }

        public void Subfunctions(char variable, out TruthTable var0sf, out TruthTable var1sf)
        {
            int[] f;
            this.ReorderSubfunctions((uint)(1 << this.VariableNames.Count<KeyValuePair<char, int>>() - this.VariableNames[variable] - 1), out f);
            int[] numArray1 = new int[f.Length / 2];
            int[] numArray2 = new int[f.Length / 2];
            for (int index = 0; index < numArray1.Length; ++index)
            {
                numArray1[index] = f[index];
                numArray2[index] = f[f.Length / 2 + index];
            }
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            int num1 = 0;
            foreach (KeyValuePair<char, int> variableName in this.VariableNames)
            {
                if ((int)variable != (int)variableName.Key && !dictionary.ContainsKey(variableName.Key))
                {
                    int num2 = 0;
                    this.VariableNames.TryGetValue(variableName.Key, out num2);
                    dictionary.Add(variableName.Key, num1);
                    ++num1;
                }
            }
            if (((IEnumerable<int>)numArray1).Count<int>() == 1)
            {
                var0sf = new TruthTable(numArray1[0] == 1);
            }
            else
            {
                var0sf = new TruthTable(numArray1);
                var0sf.variableNames.Clear();
                var0sf.variableNames = new Dictionary<char, int>((IDictionary<char, int>)dictionary);
            }
            if (((IEnumerable<int>)numArray2).Count<int>() == 1)
            {
                var1sf = new TruthTable(numArray2[0] == 1);
            }
            else
            {
                var1sf = new TruthTable(numArray2);
                var1sf.variableNames.Clear();
                var1sf.variableNames = new Dictionary<char, int>((IDictionary<char, int>)dictionary);
            }
        }
    }
}