using APPLICATION.Commands;
using APPLICATION.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APPLICATION.ViewModels
{
    internal class InputViewModel : ViewModelBase ,INotifyPropertyChanged
    {
       // public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public ICommand DrawCommand { get; }
        public ICommand ResultCommand { get; }
        public ICommand TraceCommand { get; }
        public ICommand ReinitialiserCommand { get; }
        public StepsList Steps { get; set; }
        public ICommand NavigateAjouterFonctionCommand { get; set; }
        ImpliquantsEss imp = new ImpliquantsEss();


        public string Expression
        {
            get { return _expression; }
            set
            {
                var resultCommand = ResultCommand as ResultatCommand;

                if (string.IsNullOrWhiteSpace(value))
                {
                    if(resultCommand != null)
                    {
                        resultCommand.IsEnabled = false;
                    }

                    throw new Exception("Expression vide");
                }
                else if(validationNum(value) == false)
                {
                    if (resultCommand != null)
                    {
                        resultCommand.IsEnabled = false;
                    }

                    throw new Exception("Syntaxe non valide! veuillez respecter la forme suivante:  0,2,3,4");
                }

                _expression = value;
                NotifyPropertyChanged(nameof(Expression));

                if (resultCommand != null)
                {
                    resultCommand.IsEnabled = true;
                }
            }
        }

        public string ExpressionSimplifier
        {
            get { return _expressionSimplifier; }
            set
            {
                _expressionSimplifier = value;
                NotifyPropertyChanged(nameof(ExpressionSimplifier));
                bool isValid = !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);

                if (TraceCommand is ResultatCommand traceCommand)
                {
                    traceCommand.IsEnabled = isValid;
                }

                if(DrawCommand is ResultatCommand drawCommand)
                {
                    drawCommand.IsEnabled = isValid;
                }
            }
        }

        public InputViewModel(ICommand drawCommand,ICommand traceCommand, ICommand backCommand)
        {
            NavigateAjouterFonctionCommand = backCommand;
            ReinitialiserCommand = new ResultatCommand(Reinitialiser);
            DrawCommand = drawCommand;
            TraceCommand = traceCommand;
            ResultCommand = new ResultatCommand(Result, canExecute: false);
        }
        public void Reinitialiser()
        {
            ExpressionSimplifier = null;
        }

        private void Result()
        {
            this.Steps = Simplifier();
            this.ExpressionSimplifier = this.Steps.FinalResult;
        }

        /* public string Error { get { return null; } }

         public string this[string exp]
         {
             get
             {
                 string result = null;
                 switch (exp)
                 {
                     case "Expression":
                         if (string.IsNullOrWhiteSpace(Expression))
                             result = "Expression vide ";
                         else
                         {
                             if (validationNum(Expression) == false)
                                 result = " Erreur de Syntaxe";
                         }
                         break;
                 }
                 if (ErrorCollection.ContainsKey(exp))
                     ErrorCollection[exp] = result;
                 else if (result != null)
                     ErrorCollection.Add(exp, result);
                 OnPropertyChanged("ErrorCollection");

                 return result;
             }

         }*/
        bool validationNum(string expression)
        {
            var charsAutorisesNum = "0123456789, ";
            int i;
            for (i = 0; i < expression.Count(); i++)
            {
                if (!charsAutorisesNum.Contains(expression.ElementAt(i))) return false;
            }
            string[] s = expression.Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
            foreach (var v in s)
            {
                if (!Int32.TryParse(v, out int x)) return false;
            }

            return true;
        }

       

        public StepsList Simplifier()
        {
            Stopwatch minteur = Stopwatch.StartNew();
            var steps = new StepsList();

            //-------------------------------------------------------------------------------------------------------------------------
            /* Assembly assembly = Assembly.LoadFrom(@"C:\Users\laeticia hammi\Downloads\BCLRelease\Release\BooleanClassLibrary.dll");//on va changer son emplacement
                                                                                                                                       // var type = assembly.GetType("DNF");
                Type[] externDLLTypes = assembly.GetTypes();
                dynamic Formula = Activator.CreateInstance(externDLLTypes[14], "(a&b&c)+(b&c)");
                //dynamic ImplConverter = Activator.CreateInstance(externDLLTypes[11]); 
                //ImplConverter.ToTruthTable(Formula);
                // methode.PrintString = new StringBuilder("a+b+c");
                /*dynamic Truthtable = Activator.CreateInstance(externDLLTypes[10],3);
                ImpConverter.Initialize();
                //Console.WriteLine(test);
                methode.Printtree();
                Console.WriteLine("===================================================");
                foreach(Type item in externDLLTypes)
                 {
                     Console.WriteLine(item.ToString());

                 }

                Console.ReadKey();*/
            /*  Console.WriteLine("Entrez la fonction");
              string fonc = Console.ReadLine();
              Formula formule = new Formula(fonc);
              TruthTable tv = new TruthTable(15);
              tv = ImplConverter.ToTruthTable(formule);
              //IBooleanFunctionImpl dnf;
              //dnf = ImplConverter.ToDNF(tv);
              Console.WriteLine(ImplConverter.ToDNF(tv));
              /*   BDDTree graph = new BDDTree("&");
                 graph = ImplConverter.ToBDDTree(tv);
                 Console.WriteLine("====================");
                 Console.WriteLine(graph.PrintTree());*/





            //-------------------------------------------------------------------------------------------------------------------------

            /*  Fonctionnum ret = new Fonctionnum();

              result = ret.extraireMinterme();
              Console.WriteLine("en programme");
              foreach (int v in result) { Console.WriteLine("valu"+v); }
               /*  Boolean test = false;
                  List<TabTermes> tabTermesb = new List<TabTermes>();
                  Fonctionnum fonction = new Fonctionnum();
                  List<int> ls = fonction.getds();
                  ImpliquantsEss.importmintermes(ls);
                  fonction.getmin(ls);
                  Console.WriteLine("********************************************************************************");
                  Console.WriteLine("la liste initiale ");
                  fonction.afficherList(fonction.gettabTermes());
                  Console.WriteLine("********************************************************************************");
                  Console.WriteLine(" combine does it work :" + fonction.Combinetout(fonction.gettabTermes()));
                  Console.WriteLine("\n********************************************************************************");
                  Console.WriteLine("list finale d'impliquant premier");
                  fonction.afficherList(fonction.gettabTermesPremie());
                  Console.WriteLine("*******************************************************************************");
                  //============================ le test de la suite de qmc ==================================
                //  ImpliquantsEss.importpremiers(fonction);
                  Console.WriteLine("*******************************************************************************");
                  ImpliquantsEss.test(); // elle cree uniquement la matrice*/
            /* Console.WriteLine("ce n'est pas nécessaire de passe a  "+ImpliquantsEss.reduc());
             foreach(var v in ImpliquantsEss.mintermes){Console.WriteLine(v);}
             foreach (var v in ImpliquantsEss.ImpEss) { Console.WriteLine("impliquant essentiel:{0}", v.Imp.Imp); }

           //============================ le test de cout ==================================
            string expression = "0*2*4*7+0*3*2*6*5+0*3*2*6*7+0*3*5*6*4+0*3*5*6*2+0*4*2*7*6";

            string PE = "p0+p1*p0+p2*p1+p3+p4*p2+p5*p2+p6*p3+p7*p4+p8*p6+p8*p5+p7";
           ImpliquantsEss.affichematrice();

           Console.WriteLine("l'affiche de la matrice");

           //****************************************
           //pour le main 
         //  string PE = "p0+p1*p0+p2*p1+p3+p4*p2+p5*p2+p6*p3+p7*p4+p8*p6+p8*p5+p7";

           /*  List<string> resultat = new List<string>();
             resultat = ImpliquantsEss.ImpiquantF(expression);
             Console.WriteLine(" nombre d'element dans la liste finale resultat " + resultat.Count);
             foreach (string val in resultat) { Console.WriteLine("minterme " + val); }
           List<variables> res = new List<variables>();
           ImpliquantsEss Ass = new ImpliquantsEss(res);
         //  Ass = ImpliquantsEss.RecuperVariables(PE);


           List<string> resultat = new List<string>();
            resultat = ImpliquantsEss.ImpiquantF(expression);
             Console.WriteLine(" nombre d'element dans la liste finale resultat " + resultat.Count);
             foreach (string val in resultat) { Console.WriteLine("minterme " + val); }*/
            /* List<variables> res = new List<variables>();
             ImpliquantsEss Ass = new ImpliquantsEss(res);
             Ass = ImpliquantsEss.RecuperVariables(PE);
            // ImpliquantsEss.affichagePetrickliste();
             //============================ temps d'execusion ==================================
             Console.WriteLine("***************************************************");*/
            // Console.WriteLine("***************************************************");
            // Console.WriteLine("***************************************************");
            //  testpetrik.partieIPertik(PE);
            /*  List<string> variblA = new List<string>() { "p0", "p2" };
              List<string> variblB= new List<string>() { "p3", "p2" };
              variables variablesA = new variables(variblA);
              variables variablesB = new variables(variblB);
              List<variables> B = new List<variables>() { variablesA, variablesB };
              Petrik petrik = new Petrik();
                      Console.WriteLine("le resulata"+ petrik.GetExpressionFromMaxtermes(B));


              Stopwatch minteur = Stopwatch.StartNew();
              Boolean test = false;
              List<TabTermes> tabTermesb = new List<TabTermes>();
              Fonctionnum fonction = new Fonctionnum();
              List<int> ls = fonction.getds();
              ImpliquantsEss.importmintermes(ls);
              fonction.getmin(ls);
              Console.WriteLine("la liste initiale ");
              Console.WriteLine("***************************************************");
              fonction.afficherList(fonction.gettabTermes());
              /*  Console.WriteLine("\n\n***************************************************");
               // tabTermesb = fonction.combine(fonction.gettabTermes());

                Console.WriteLine("new liste d'impliquant apres combine");
                fonction.afficherList(tabTermesb);
                Console.WriteLine("list impliquant premier apres combine");
                fonction.afficherList(fonction.gettabTermesPremie()); */
            /*  Console.WriteLine("\n\n ***************************************************");
              Console.WriteLine(" combine tout ");
              Console.WriteLine("\n\n combine does it work :" + fonction.Combinetout(fonction.gettabTermes()));

              Console.WriteLine("***************************************************");
              Console.WriteLine("***************************************************");
              Console.WriteLine("list finale d'impliquant premier");
              fonction.afficherList(fonction.gettabTermesPremie());
              ImpliquantsEss.importpremiers(fonction);
              ImpliquantsEss.test();

              string expression = "0*2*4*7+0*3*2*6*5+0*3*2*6*7+0*3*5*6*4+0*3*5*6*2+0*4*2*7*6";
              List<string> resultat = new List<string>();

              resultat = ImpliquantsEss.ImpiquantF(expression);
              Console.WriteLine(" nombre d'element dans resultat " + resultat.Count);
              foreach (string val in resultat) { Console.WriteLine("minterme " + val); }





              //*/
            /* Console.WriteLine("le minterme : ");
             string tttt = Console.ReadLine();
             Console.WriteLine("le resultat " + ImpliquantsEss.ConvertesseurBinToString(tttt));*/



            //-------------------------------------------------------------------------------------------
            Boolean test = false;
            List<TabTermes> tabTermesb = new List<TabTermes>();
            Fonctionnum fonction = new Fonctionnum(Expression);

            //ImpliquantsEss.importmintermes(ls);
            //--------------------numerique -----------------------------------------------------------------

            List<int> ls = fonction.getds();
            imp = new ImpliquantsEss();
            imp.importmintermes(ls);
            fonction.getmin(ls);

            //-------------------------------------------------------------------------------------------
            /*  ImpliquantsEss.importmintermes(ls);
             fonction.getmin(ls);*/

            /*ImpliquantsEss.importmintermes(result);
            fonction.getmin(result);*/
            //-----------------------------------------------------------------------------------------
            Console.WriteLine("la liste initiale ");
            Console.WriteLine("***************************************************");


            var s1 = fonction.gettabTermes();
            steps.Add(new Step(s1));

            fonction.afficherList(s1); //this is the list we should display it contains binary representation of some decimals  that are also shown and the number of '1' in each binary represantation 


            Console.WriteLine("\n\n***************************************************");
            var y = fonction.gettabTermes().ToList();
            tabTermesb = fonction.combine(y);

            Console.WriteLine("new liste d'impliquant apres combine");

            steps.Add(new Step(tabTermesb));

            fonction.afficherList(tabTermesb);   // Second Step
            Console.WriteLine("list impliquant premier apres combine");

            var s2 = fonction.gettabTermesPremie();
            steps.Add(new Step(s2));

            fonction.afficherList(s2); // Is this the end of step2 
            Console.WriteLine("\n\n ***************************************************");
            Console.WriteLine(" combine tout ");
            fonction.Combinetout(tabTermesb);
            Console.WriteLine("list finale d'impliquant premier");

            var a = fonction.gettabTermesPremie().ToList();
            steps.Add(new Step(a));

            fonction.afficherList(a);//this is the third one
            imp.importpremiers(fonction);

            var result = imp.ReductionTotaleQMCandPETRIK(); // The last one
            steps.FinalResult = result;

            //foreach(var v in ImpliquantsEss.mintermes){Console.WriteLine(v);}
            // ImpliquantsEss.affichematrice((List<List<bool>>)ImpliquantsEss.Min_Imp, (List<ImpliquantsEss.ImpPrem>)ImpliquantsEss.ImpPrems);
            /*foreach (var v in ImpliquantsEss.ImpPrems) { Console.WriteLine("impliquant essentiel:{0}", v.Imp); }//*/

            Console.WriteLine("le temps d'execussion est : " + minteur.ElapsedMilliseconds / 1000 + "seconds");
            Console.WriteLine("***************************************************");

            //this.ExpressionSimplifier = imp.ReductionTotaleQMCandPETRIK();
            return steps;
        }


        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _expressionSimplifier;
        private string _expression;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
