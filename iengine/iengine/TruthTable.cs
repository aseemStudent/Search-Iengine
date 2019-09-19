using System;
using System.Collections.Generic;

namespace iengine
{
    public class TruthTable: SearchMethod
    {
        public TruthTable(string fileName): base(fileName)
        {
            Queue<string> symbols = new Queue<string>();
            foreach (string s in hornFormKB.GetSymbols())
            {   // gets every symbol of KB
                if (!symbols.Contains(s)) { symbols.Enqueue(s); }
            }

            TTCheckAll(hornFormKB.KB, hornFormKB.query, symbols, new Dictionary<string, bool>());

            if(b > 0 || a == 0)
            {
                Console.WriteLine("NO");
            }
            else
            {
                Console.WriteLine("YES: {0}", a);
            }
        }

        public bool TTCheckAll(List<Sentences> KB, Sentences alpha, Queue<string> symbolsQ, Dictionary<string, bool> model)
        {
            if(symbolsQ.Count == 0)
            {
                if (KBTrue(KB, model))
                {
                    if(EachSentence(alpha, model))
                    {
                        a++;
                    }
                    else
                    {
                        b++;
                    }
                }

            }
            else
            {
                string topSymbol = symbolsQ.Dequeue();

                return ((TTCheckAll(KB, alpha, new Queue<string>(symbolsQ), AddToDictionary(model, topSymbol, true)) 
                && (TTCheckAll(KB, alpha, new Queue<string>(symbolsQ), AddToDictionary(model, topSymbol, false)))));
            }

            return true;
        }


        public  Dictionary<string, bool> AddToDictionary(Dictionary<string, bool> model, string add, bool boolValue)
        {
            Dictionary<string, bool> extendDictionary = new Dictionary<string, bool>(model);
            extendDictionary.Add(add, boolValue);
            return extendDictionary;
        }

        public bool KBTrue(List<Sentences> KB, Dictionary<string, bool> model)
        {
            foreach(Sentences s in KB)
            {
                // if even 1 KB is false it returns false
                if(!EachSentence(s, model))
                {
                    return false;
                }
            }
            return true;
        }

        public bool EachSentence(Sentences s, Dictionary<string, bool> model)
        {
            if (s.symbol != null)
            {
                // checks single sentences like p1, a, b
                if(model.ContainsKey(s.symbol))
                {
                    return model[s.symbol];
                }

            }
            else
            {
                // if consequent is false it needs to checks antecedent
                if (!model[s.children[1].symbol])
                {
                    return Antecedent(s.children[0], model);
                }
                return true;
            }
            return false;
        }

        public bool Antecedent(Sentences s, Dictionary<string, bool> model)
        {
            // checks single antecedents like p1, a, b
            if (s.symbol != null)
            {
                return !model[s.symbol];
            }
            else
            {
                // // checks && sentences like a&b
                return !(model[s.children[0].symbol] && model[s.children[1].symbol]);
            }
        }


    }
}
