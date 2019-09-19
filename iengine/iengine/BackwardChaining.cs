using System;
using System.Collections.Generic;

namespace iengine
{
    public class BackwardChaining: SearchMethod
    {
        public Queue<string> queryAgenda = new Queue<string>();
        public List<string> inferredTrue = new List<string>();

        public BackwardChaining(string fileName) : base(fileName)
        {
            //inferred Dictionary conatains all symbol with value false 
            foreach (Sentences s in hornFormKB.KB)
            {
                if (s.symbol != null)
                {
                    if (!inferredTrue.Contains(s.symbol) && AtomicSentence(s.symbol))
                    {
                        inferredTrue.Add(s.symbol);
                    }
                }
            }

            Stack<string> printResult = new Stack<string>();
            printResult.Push(hornFormKB.query.symbol);

            if (BC(hornFormKB.KB, hornFormKB.query, printResult))
            {
                Console.Write("YES:");
                foreach (string s in printResult)
                {
                    Console.Write(" {0}", s);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("NO");
            }
        }


        public bool BC(List<Sentences> kb, Sentences alpha, Stack<string> printResult)
        {
            if(inferredTrue.Contains(alpha.symbol))
            {
                return true;
            }

            foreach (Sentences s in kb)
            {
                if (s.symbol != null)
                {
                    if (s.symbol == alpha.symbol)
                    {
                        return true;
                    }
                }
                else if(s.children[1].symbol == alpha.symbol)
                {
                    if (s.children[0].symbol != null)
                    {
                        if(!printResult.Contains(s.children[0].symbol))
                        {
                            printResult.Push(s.children[0].symbol);
                        }
                        return BC(kb, s.children[0], printResult);

                    }
                    else
                    {
                        if (!printResult.Contains(s.children[0].children[0].symbol))
                        {
                            printResult.Push(s.children[0].children[0].symbol);
                        }
                        if (!printResult.Contains(s.children[0].children[1].symbol))
                        {
                            printResult.Push(s.children[0].children[1].symbol);
                        }
                        return BC(kb, s.children[0].children[0], printResult) && BC(kb, s.children[0].children[1], printResult);
                    }
                }
            }
            return false;
        }
    }
}
