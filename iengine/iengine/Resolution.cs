using System;
using System.Collections.Generic;

namespace iengine
{
    public class Resolution: SearchMethod
    {
        public PrintCNFSentences print = new PrintCNFSentences();
        public Resolution(string fileName) : base(fileName)
        {
            CNF cnf = new CNF(hornFormKB.KB);
            List<CNFSentences> KB = new List<CNFSentences>(cnf.CNFKB);
            CNFSentences alpha = new CNFSentences(hornFormKB.query.symbol, true);

            if(QueryNotSolvable(KB, alpha))
            {
                Console.WriteLine("NO");
            }
            else
            {
                PLResolution(KB, alpha);
            }

        }

        public bool PLResolution(List<CNFSentences> KB, CNFSentences alpha)
        {
            List<CNFSentences> addKB = new List<CNFSentences>();
            bool loop = false;

            loop = CheckSolved(KB, alpha);
            if (loop) { print.PrintResult(KB, alpha); }
            if (loop) { Console.WriteLine("YES"); return true; }
            addKB.Clear();

            // compare each CNF with every other CNF
            for (int i = 0; i < KB.Count - 1; i++)
            {
                for (int j = i + 1; j < KB.Count; j++)
                {
                    CompareCNF(KB[i], KB[j], addKB);
                }
            }

            // if no new CNF added then return NO
            int x = 0;
            foreach (CNFSentences c in addKB)
            {

                if (!ContainsKB(KB, c))
                {
                    x++;
                    KB.Add(c);
                }
            }
            if(x == 0) { Console.WriteLine("NO"); return false; }
            PLResolution(KB, alpha);
            return false;
        }


        // checks if KB  has d when alpha is ~d
        public bool CheckSolved(List<CNFSentences> KB, CNFSentences alpha)
        {
            foreach (CNFSentences c in KB)
            {
                if (c.symbol != null && c.symbol == alpha.symbol)
                {
                    return !(c.Negate == alpha.Negate);
                }
            }
            return false;
        }


        // compare each cnf with other
        public void CompareCNF(CNFSentences first, CNFSentences second, List<CNFSentences> addKB)
        {
            if (first.symbol == null && second.symbol == null)
            {
                foreach (CNFSentences f in first.children)
                {
                    foreach (CNFSentences s in second.children)
                    {
                        if (f.symbol == s.symbol && f.Negate != s.Negate)
                        {
                            addKB.Add(RetureNewStatement(first, second, f.symbol));

                        }
                    }
                }
            }
            else if (first.symbol != null && second.symbol == null)
            {
                foreach (CNFSentences s in second.children)
                {
                    if (s.symbol == first.symbol && s.Negate != first.Negate)
                    {
                        addKB.Add(RetureNewStatement2(first, second));
                    }
                }
            }

            else if(first.symbol == null && second.symbol != null)
            {
                CompareCNF(second, first, addKB);
            }
        }

        // joins 2 cnf sentenses both with children
        public CNFSentences RetureNewStatement(CNFSentences first, CNFSentences second, string common)
        {
            Dictionary<string, bool> temp = new Dictionary<string, bool>();
            temp.Clear();
            foreach (CNFSentences f in first.children)
            {
                if (f.symbol != common)
                {
                    temp.Add(f.symbol, f.Negate);
                }
            }
            foreach (CNFSentences s in second.children)
            {
                if (s.symbol != common && !temp.ContainsKey(s.symbol))
                {
                    temp.Add(s.symbol, s.Negate);
                }
            }

            if(temp.Count == 1)
            {
                foreach(KeyValuePair<string, bool> k in temp)
                {
                    return new CNFSentences(k.Key, k.Value);
                }
            }
            return new CNFSentences(temp);
        }

        // joins 2 cnf sentenses first atonomous and second with children
        public CNFSentences RetureNewStatement2(CNFSentences first, CNFSentences second)
        {
            Dictionary<string, bool> temp = new Dictionary<string, bool>();
            temp.Clear();
            foreach (CNFSentences s in second.children)
            {
                if (s.symbol != first.symbol && !temp.ContainsKey(s.symbol))
                {
                    temp.Add(s.symbol, s.Negate);
                }
            }
            if (temp.Count == 1)
            {
                foreach (KeyValuePair<string, bool> k in temp)
                {
                    return new CNFSentences(k.Key, k.Value);
                }
            }
            return new CNFSentences(temp);
        }


        // check if the newCNF is alredy in the list or not
        public bool ContainsKB(List<CNFSentences> KB, CNFSentences newCNF)
        {
            foreach (CNFSentences cnf in KB)
            {
                if (!NotSame(cnf, newCNF)) { return true; }
            }
            return false;
        }

        // 
        public bool NotSame(CNFSentences first, CNFSentences second)
        {

            if(first.symbol == null && second.symbol == null && first.children.Length != second.children.Length)
            {
                return true;
            }
            else if (first.symbol == null && second.symbol == null && first.children.Length == second.children.Length)
            {
                for (int i = 0; i < first.children.Length; i++)
                {
                    if (first.children[i].symbol != second.children[i].symbol)
                    {
                        return true;
                    }
                }
            }
            else if(first.symbol != null && second.symbol != null)
            {
                if(first.symbol != second.symbol) { return true; }
                else if (first.Negate != second.Negate) { return true; }
            }
            else if(first.symbol == null && second.symbol != null) { return true; }
            else if (first.symbol != null && second.symbol == null) { return true; }

            return false;
        }


        // if ASK symbol is not in KB
        public bool QueryNotSolvable(List<CNFSentences> KB, CNFSentences alpha)
        {
            foreach(CNFSentences c in KB)
            {
                if(c.symbol != null && c.symbol == alpha.symbol) { return false; }
                if(c.symbol == null)
                {
                    foreach (CNFSentences child in c.children)
                    {
                        if (child.symbol != null && child.symbol == alpha.symbol) { return false; }
                    }
                }
            }
            return true;
        }
    }
}
