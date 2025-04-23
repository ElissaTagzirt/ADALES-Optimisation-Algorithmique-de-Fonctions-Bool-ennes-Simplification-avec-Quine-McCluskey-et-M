using APPLICATION.Commands;
using APPLICATION.Models;
using APPLICATION.Service;
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
    internal class InputLitViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand DrawLitCommand { get; }
        public ICommand ResultCommand { get; }
        public ICommand TraceCommand { get; }
        public ICommand ReinitialiserCommand { get; }
        public ICommand NavigateAjouterFonctionCommand { get; set; }
        public StepsList Steps { get; private set; }

        public string _expression;
        public string _expressionSimplifier;
        ImpliquantsEssLitterale imp = new ImpliquantsEssLitterale();


        public string Expression
        {
            get { return _expression; }
            set
            {
                ResultatCommand rcmd = ResultCommand as ResultatCommand;

                if (string.IsNullOrWhiteSpace(value))
                {
                    if (rcmd != null)
                    {
                        rcmd.IsEnabled = false;
                    }

                    throw new Exception("Expression vide");
                }
                else
                {
                    if (!validationSyntax(value))
                    {
                        if (rcmd != null)
                        {
                            rcmd.IsEnabled = false;
                        }

                        throw new Exception("Expression incorrcte, Veuilliez respecter la forme suivante: a+b+!c&d");


                    }
                    if (!MissingOperateur(value))
                    {
                        if (rcmd != null)
                        {
                            rcmd.IsEnabled = false;
                        }
                        throw new Exception("Syntaxe erronée! Veuillez compléter votre expression! ");
                    }
                }

                _expression = value;
                NotifyPropertyChanged(nameof(Expression));

                if (rcmd != null)
                {
                    rcmd.IsEnabled = true;
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

                if (DrawLitCommand is ResultatCommand drawCommand)
                {
                    drawCommand.IsEnabled = isValid;
                }

                if(TraceCommand is ResultatCommand traceCommand)
                {
                    traceCommand.IsEnabled = isValid;
                }
            }
        }

        // public ICommand ResultCommand { get; }
        // public ICommand NavigateAjouterFonctionCommand { get; }
        public ICommand NavigateTraceCommand { get; }
        // public ICommand NavigateSyntheseCommand { get; }


        public InputLitViewModel(ICommand drawCommand, ICommand traceCommand, ICommand backCommand)
        {
            NavigateAjouterFonctionCommand = backCommand;
            TraceCommand = traceCommand;
            ReinitialiserCommand = new ResultatCommand(Reinitialiser);
            DrawLitCommand = drawCommand;
            ResultCommand = new ResultatCommand(SimplifierLitterale, canExecute: false);
        }
        public void Reinitialiser()
        {
            ExpressionSimplifier = null;
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
                             if (validationSyntax(Expression) == false)
                                 result = "Erreur de syntaxe";
                             if (MissingOperande(Expression) == false)
                                 result = "Operande Binaire ! ";
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
        bool MissingOperateur(string expression)
        {

            var operateur = "+&!";
            if (operateur.Contains(expression.Last())) return false;
            else return true;
        }

        bool validationSyntax(string expression)
        {
            var CharsAutorises = "!&+abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ() ";
            var lettres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var operateurs = "!+&";
            var opBinaires = "+&";
            int i;

            //caractères non autorisés:
            for (i = 0; i < expression.Count(); i++)
            {
                if (!CharsAutorises.Contains(expression.ElementAt(i))) { return false; }
            }
            Console.WriteLine("caractères non autorisés");
            //deux variables collées:
            for (i = 0; i < expression.Count(); i++)
            {
                if ((i < expression.Count() - 1) && (lettres.Contains(expression.ElementAt(i)))) { if (lettres.Contains(expression.ElementAt(i + 1))) return false; ; }
            }
            Console.WriteLine("deux variables collées:");
            //le signe négation précédé d'une variable:
            for (i = 0; i < expression.Count(); i++)
            {
                if ((i >0) && (expression.ElementAt(i) == '!')) { if (lettres.Contains(expression.ElementAt(i -1))) return false; ; }
            }
            Console.WriteLine("le signe négation précédé d'une variable");
            //supprimer les blancs:
            expression = expression.Replace(" ", "");
            Console.WriteLine(expression);
            //deux operateurs qui ne doivent pas se suivre:
            if (expression.Contains("&&") || expression.Contains("++") || expression.Contains("!!")
            || expression.Contains("+&") || expression.Contains("&+")||expression.Contains("!+")||expression.Contains("!&")) return false;
            Console.WriteLine("deux operateurs qui ne doivent pas se suivre");
            //positions et compatibilité des parenthèses:
            Stack<char> s = new Stack<char>();
            for (i = 0; i < expression.Count(); i++)
            {
                if (expression.ElementAt(i) == '(')
                {
                    s.Push('('); Console.WriteLine("repere");
                }
                else if (expression.ElementAt(i) == ')')
                {
                    if (s.Count == 0) { return false; } else s.Pop();
                }
            }
            if (!(s.Count == 0)) return false;
            Console.WriteLine("positions et compatibilité des parenthèses");
            //caractères avant et après les parenthèses:
            for (i = 0; i < expression.Count(); i++)
            {
                if (expression.ElementAt(i) == '(')
                {
                    if (i > 0) { if (!operateurs.Contains(expression.ElementAt(i - 1)) && expression.ElementAt(i - 1) != '(') return false; }
                }
                else if (expression.ElementAt(i) == ')')
                {
                    if (i < expression.Count() - 1) { if (!opBinaires.Contains(expression.ElementAt(i + 1)) && expression.ElementAt(i + 1) != ')') return false; }
                }
            }
            Console.WriteLine("caractères avant et après les parenthèses");
            //caractère de fin:
            // if (opBinaires.Contains(expression.Last())) return false;
            Console.WriteLine("caracteres de fin, expression juste");
            //expression vide
            if (expression.Contains("()")) return false;
            return true;



        }

        public void SimplifierLitterale()
        {
            imp = new ImpliquantsEssLitterale();
            var steps = new StepsList();

            //================================================================================================
            //------------------------------------litterlae-------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------------------------      Console.WriteLine("Entrez la fonction");
            ExpressionLet ex = new ExpressionLet();
            Console.WriteLine("l'expression originale" + Expression); //the input expression
            Formula formule = new Formula(ex.Reconstruction(Expression));
            TruthTable tv = new TruthTable(5);
            tv = ImplConverter.ToTruthTable(formule);
            Console.WriteLine(ImplConverter.ToDNF(tv)); //the new expression in DNF
            //---------------------------------------------//
            List<int> result = new List<int>();
            string expression = DNF.getresult();
            //---------------------------------------------//
            // Console.WriteLine("expression : " + expression);
            ExpressionLet ret = new ExpressionLet();

            result = ret.PassageLitteraleNumerique(expression);
            imp.importmintermes(result);
            ret.getmin(result);


            //---------------------------------------------//
            Console.WriteLine("la liste initiale "); //the first list 
            //Console.WriteLine("***************************************************");
            var firstStep = ret.gettabTermes();
            //steps.Add(new Step(firstStep));// you mean this and

            ret.afficherList(firstStep);// the medium steps (lists)
            steps.Add(new Step(firstStep)); //thid. Yeah exactly please can u remove the first one, sure!

            //   Console.WriteLine(" combine tout ");
            ret.Combinetout(ret.gettabTermes()); //this
            Console.WriteLine("list finale d'impliquant premier");
            var finalStep = ret.gettabTermesPremie();
            steps.Add(new Step(finalStep));
            ret.afficherList(finalStep);//the final list of implequents //or this? or both??


            imp.importpremiers(ret);
            imp.ReductionTotaleQMCandPETRIK();
            imp.importpremiers(ret);

            this.ExpressionSimplifier = imp.ReductionTotaleQMCandPETRIK();

            steps.FinalResult = this.ExpressionSimplifier;
            Console.WriteLine(" L'expression simplifier de mexpression originale :" + this.ExpressionSimplifier);// the result of simplification
            this.Steps = steps;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

}

