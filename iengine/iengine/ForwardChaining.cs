using System;
using System.Collections.Generic;

namespace iengine
{
    public class ForwardChaining: SearchMethod
    {
        public Queue<string> agenda = new Queue<string>();
        public Dictionary<string, bool> inferred = new Dictionary<string, bool>();
        public Dictionary<Sentences, int> clauses = new Dictionary<Sentences, int>();

        public ForwardChaining(string fileName) : base(fileName)
        {

            //inferred Dictionary conatains all symbol with value false 
            foreach (string s in hornFormKB.GetSymbols())
            {
                if (!inferred.ContainsKey(s)) { inferred.Add(s, false); }
            }

            // clauses Dictionary <sentences> with int(number of symbols in that sentance)
            foreach (Sentences s in hornFormKB.KB)
            {
                if(!clauses.ContainsKey(s))
                {
                    int count = GetSentenceCount(s);
                    clauses.Add(s, count);
                }
            }

            // adding true (autonomus) symbols to agenda
            foreach (Sentences s in hornFormKB.KB)
            {
                if (s.symbol != null)
                {
                    agenda.Enqueue(s.symbol);
                    //Console.WriteLine(kbString.symbol);
                }
            }
            PlFcEntails(hornFormKB.KB, hornFormKB.query);
        }

        public void PlFcEntails(List<Sentences> kb, Sentences alpha)
        {
            Dictionary<Sentences, int> Clauses = new Dictionary<Sentences, int>(clauses);
            Queue<string> printResult = new Queue<string>();
            bool end = false;
            while (agenda.Count != 0 && !end)
            {
                string currentAgenda = agenda.Dequeue();
                printResult.Enqueue(currentAgenda);
                if(currentAgenda == alpha.symbol) { end = true;  break; }
                if (inferred.ContainsKey(currentAgenda)) { inferred[currentAgenda] = true; }


                // if currentAgends (symbol) is present in PREMISE of sentence, -1 for its int
                foreach(KeyValuePair<Sentences, int> s in Clauses)
                {
                    PremiseContainAgenda(s.Key, currentAgenda);
                } 

                // when int is 0 change false to true in inferred
                foreach (KeyValuePair<Sentences, int> s in clauses)
                {
                    if(s.Value <= 0 && s.Key.symbol == null)
                    {
                        if (!agenda.Contains(s.Key.children[1].symbol) &&
                             !printResult.Contains(s.Key.children[1].symbol))
                        {
                            agenda.Enqueue(s.Key.children[1].symbol);
                        }
                    }
                }
            }

            //write answer - if found write path - else not found
            if(printResult.Contains(alpha.symbol))
            {
                Console.Write("YES:");
                foreach (string s in printResult)
                {
                    Console.Write(" {0}", s);
                }
            }
            else
            {
                Console.WriteLine("NO");
            }
        }


        // returns 0 for Atanomus sentence like p
        // returns 1 for sentence like p => e
        // returns 2 for sentence like p&g => e
        public int GetSentenceCount(Sentences sent)
        {
            int temp = 0;
            if(sent.symbol != null) 
            {
                //temp++;
                return temp; 
            }
            else
            {
                temp++;
                foreach(Sentences s in sent.children)
                {
                    temp += GetSentenceCount(s);
                }
                return temp;
            }
        }

        // returns each sentence KB in string
        public string GetKB(Sentences s)
        {
            if (s.symbol != null)
            {
                return s.symbol;
            }
            else
            {
                return GetKB(s.children[0]) + s.connective + GetKB(s.children[1]);
            }
        }

        // if currentAgends (symbol) is present in PREMISE of sentence, -1 for its int
        public void PremiseContainAgenda(Sentences  s, string currentAgenda)
        {
            if (s.symbol == null)
            {
                if (s.children[0].symbol != null)
                {
                    if (s.children[0].symbol == currentAgenda)
                    {
                        clauses[s]--;
                    }
                }
                else
                {
                    if (s.children[0].children[0].symbol == currentAgenda || s.children[0].children[1].symbol == currentAgenda)
                    {
                        clauses[s]--;
                    }
                }
            }
        }
    }
}
