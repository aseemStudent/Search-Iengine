using System;
using System.Collections.Generic;

namespace iengine
{
    public class CNF
    {
        List<CNFSentences> cnfKB = new List<CNFSentences>();

        public CNF(List<Sentences> KB)
        {
            foreach(Sentences s in KB)
            {
                if(s.symbol != null)
                {
                    //Console.WriteLine(s.symbol);
                    cnfKB.Add(new CNFSentences(s.symbol, false));
                }
                else if (s.children[0].symbol != null)
                {
                    Dictionary<string, bool> temp = new Dictionary<string, bool>();
                    temp.Add(s.children[0].symbol, true);
                    temp.Add(s.children[1].symbol, false);
                    cnfKB.Add(new CNFSentences(temp));
                    //Console.WriteLine("{0}{1}{2}", s.children[0].symbol, s.connective, s.children[1].symbol);
                }
                else
                {
                    Dictionary<string, bool> temp = new Dictionary<string, bool>();
                    temp.Add(s.children[0].children[0].symbol, true);
                    temp.Add(s.children[0].children[1].symbol, true);
                    temp.Add(s.children[1].symbol, false);
                    cnfKB.Add(new CNFSentences(temp));
                    //Console.WriteLine("{0}{1}{2}{3}{4}", s.children[0].children[0].symbol, s.children[0].connective, s.children[0].children[1].symbol,
                    //                                        s.connective, s.children[1].symbol);
                }
            }
        }

        public List<CNFSentences> CNFKB
        {
            get
            {
                return cnfKB;
            }
            set
            {
                cnfKB = value;
            }
        }
    }
}
