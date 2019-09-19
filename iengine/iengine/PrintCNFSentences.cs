using System;
using System.Collections.Generic;

namespace iengine
{
    public class PrintCNFSentences
    {
        public PrintCNFSentences()
        {
        }

        // prints each CNFSentence
        public void Print(CNFSentences c)
        {
            if (c.symbol != null)
            {
                if (c.Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.symbol);
            }
            else if (c.children.Length == 2)
            {
                if (c.children[0].Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.children[0].symbol);
                Console.Write(" v ");
                Console.Write(c.children[1].symbol);
            }
            else if (c.children.Length == 3)
            {
                if (c.children[0].Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.children[0].symbol);
                Console.Write(" v ");
                if (c.children[1].Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.children[1].symbol);
                Console.Write(" v ");
                Console.Write(c.children[2].symbol);
            }
            else if (c.children.Length == 1)
            {
                if (c.children[0].Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.children[0].symbol);
            }
            else if (c.children.Length == 4)
            {
                if (c.children[0].Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.children[0].symbol);
                Console.Write(" v ");
                if (c.children[1].Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.children[1].symbol);
                Console.Write(" v ");
                if (c.children[2].Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.children[2].symbol);
                Console.Write(" v ");
                if (c.children[3].Negate)
                {
                    Console.Write("~");
                }
                Console.Write(c.children[3].symbol);
            }
            else if(c.children.Length > 4)
            {
                bool or = false;
                for(int i = 0; i < c.children.Length; i++)
                {
                    if(or)
                    {
                        Console.Write(" v ");
                    }
                    PrintEach(c.children[i]);
                    or = true;
                }
                //Console.ReadLine();
            }
            Console.WriteLine();
        }

        public void PrintEach(CNFSentences c)
        {
            if (c.Negate)
            {
                Console.Write("~");
            }
            if (c.symbol != null)
            {
                Console.Write(c.symbol);
            }
        }

        public void PrintResult(List<CNFSentences> KB, CNFSentences alpha)
        {
            Console.WriteLine("All CNF KBs\n");
            foreach(CNFSentences c in KB)
            {
                Print(c);
            }

            Console.WriteLine("\nKB and ~ alpha is unsatisfiable\n");
        }
    }
}
