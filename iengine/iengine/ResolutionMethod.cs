//using System;
//using System.Collections.Generic;

//namespace iengine
//{
//    public class ResolutionMethod : SearchMethod
//    {
//        public ResolutionMethod(string fileName) : base(fileName)
//        {
//            CNF cnf = new CNF(hornFormKB.KB);
//            List<CNFSentences> KB = new List<CNFSentences>(cnf.CNFKB);
//            CNFSentences alpha = new CNFSentences(hornFormKB.query.symbol, true);
//            PLResolution(KB, alpha);
//        }

//        public void PLResolution(List<CNFSentences> KB, CNFSentences alpha)
//        {
//            bool loop = false;
//            while(!loop)
//            {
//                loop = CheckSolved(KB, alpha);
//                if (loop) { Console.WriteLine("Solved"); Console.ReadLine(); }
//                List<CNFSentences> addKB = new List<CNFSentences>();
//                addKB.Clear();
//                for (int i = 0; i < KB.Count - 1; i++)
//                {
//                    for (int j = i + 1; j < KB.Count; j++)
//                    {
//                        CompareCNF(KB[i], KB[j], addKB);
//                    }
//                }
//                if (addKB.Count == 0) { loop = true; Console.WriteLine("Failed"); Console.ReadLine(); }
//                foreach (CNFSentences c in addKB)
//                {
//                    //Print(c);
//                    if (!ContainsKB(KB, c))
//                    {
//                        //Print(c);
//                        KB.Add(c);
//                    }
//                }
//            }
//        }

//        // compare each cnf with other
//        public void CompareCNF(CNFSentences first, CNFSentences second, List<CNFSentences> addKB)
//        {
//            if (first.symbol == null && second.symbol == null && NotSame(first, second))
//            {
//                foreach (CNFSentences f in first.children)
//                {
//                    foreach (CNFSentences s in second.children)
//                    {
//                        if (f.symbol == s.symbol && f.Negate != s.Negate)
//                        {
//                            //Print(first);
//                            //Print(second);
//                            addKB.Add(RetureNewStatement(first, second, f.symbol));
//                            //Print(RetureNewStatement(first, second, f.symbol));
//                        }
//                    }
//                }
//            }

//            if (first.symbol != null && second.symbol == null)
//            {
//                foreach (CNFSentences s in second.children)
//                {
//                    if (s.symbol == first.symbol && s.Negate != first.Negate)
//                    {
//                        //Print(first);
//                        //Print(second);
//                        //Print(RetureNewStatement2(first, second));
//                        //Console.ReadLine();
//                        addKB.Add(RetureNewStatement2(first, second));
//                    }
//                }
//            }

//            if (first.symbol == null && second.symbol != null)
//            {
//                CompareCNF(second, first, addKB);
//            }
//        }

//        // joins 2 cnf sentenses first atonomous and second with children
//        public CNFSentences RetureNewStatement2(CNFSentences first, CNFSentences second)
//        {
//            Dictionary<string, bool> temp = new Dictionary<string, bool>();
//            temp.Clear();
//            foreach (CNFSentences s in second.children)
//            {
//                if (s.symbol != first.symbol && !temp.ContainsKey(s.symbol))
//                {
//                    temp.Add(s.symbol, s.Negate);
//                }
//            }
//            return new CNFSentences(temp);
//        }

//        // joins 2 cnf sentenses both with children
//        public CNFSentences RetureNewStatement(CNFSentences first, CNFSentences second, string common)
//        {
//            Dictionary<string, bool> temp = new Dictionary<string, bool>();
//            temp.Clear();
//            foreach (CNFSentences f in first.children)
//            {
//                if (f.symbol != common)
//                {
//                    temp.Add(f.symbol, f.Negate);
//                }
//            }
//            foreach (CNFSentences s in second.children)
//            {
//                if (s.symbol != common && !temp.ContainsKey(s.symbol))
//                {
//                    temp.Add(s.symbol, s.Negate);
//                }
//            }
//            return new CNFSentences(temp);
//        }

//        // checks if KB  has d when alpha is ~d
//        public bool CheckSolved(List<CNFSentences> KB, CNFSentences alpha)
//        {
//            foreach (CNFSentences c in KB)
//            {
//                if (c.symbol != null && c.symbol == alpha.symbol)
//                {
//                    return !(c.Negate == alpha.Negate);
//                }
//            }
//            return false;
//        }

//        // check if the newCNF is alredy in the list or not
//        public bool ContainsKB(List<CNFSentences> KB, CNFSentences newCNF)
//        {
//            foreach (CNFSentences cnf in KB)
//            {
//                if (NotSame(cnf, newCNF))
//                {
//                    return false;
//                }
//            }
//            return true;
//        }

//        // 
//        public bool NotSame(CNFSentences first, CNFSentences second)
//        {
//            if (first.symbol != null && second.symbol != null && first.children.Length == second.children.Length)
//            {
//                for (int i = 0; i < first.children.Length; i++)
//                {
//                    if (first.children[i].symbol != second.children[i].symbol)
//                    {
//                        return true;
//                    }
//                }
//            }
//            return false;
//        }

//    }
//}
