using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace iengine
{
    public class HornFormKB
    {
        public string clausesString;
        public Sentences query = new Sentences();
        public List<Sentences> KB = new List<Sentences>();
        public Queue<string> kbSymbols = new Queue<string>();

        public HornFormKB(string textFileName)
        {
            StreamReader textFile = new StreamReader(textFileName);

            if (textFile.ReadLine() == "TELL")
            {
                clausesString = textFile.ReadLine().TrimEnd(';');
            }
            clausesString = clausesString.Replace(" ", string.Empty);
            GetKnowledgeBase(clausesString);

            if (textFile.ReadLine() == "ASK")
            {
                query.symbol = textFile.ReadLine();
            }

            foreach(string s in GetSymbols())
            {
                if(!(kbSymbols.Contains(s)))
                {
                    kbSymbols.Enqueue(s);
                }
            }

        }

        public void GetKnowledgeBase(string clausesString)
        {
            string[] clauses = clausesString.Split(';');


            foreach(string s in clauses)
            {

                if (AtomicSentence(s))
                {
                    KB.Add(new Sentences(s));
                }
                else if(!s.Contains("&")) 
                {
                    string tempKB = s;
                    tempKB = tempKB.Replace("=>", ";");
                    string[] tempKBChildren = tempKB.Split(';');
                    KB.Add(new Sentences("=>", tempKBChildren));
                }
                else
                {
                    string tempKB = s;
                    tempKB = tempKB.Replace("=>", ";");
                    string[] tempKBChildrenA = tempKB.Split(';');
                    string[] tempKBChildrenB = tempKBChildrenA[0].Split('&');
                    Sentences tempSentence = new Sentences("&", tempKBChildrenB);
                    Sentences[] tempSentenceArray = { tempSentence, new Sentences(tempKBChildrenA[1]) };
                    KB.Add(new Sentences("=>", tempSentenceArray));
                }
            }
        }

        // ckecks for individual sentence like p, q (no connectives)
        public bool AtomicSentence(string aChar)
        {
            return aChar.All(char.IsLetterOrDigit);
        }

        // returns each sentence KB in string
        public string GetKB(Sentences s)
        {
            if(s.symbol != null)
            {
                return s.symbol;
            }
            else
            {
                return GetKB(s.children[0]) + s.connective + GetKB(s.children[1]);
            }
        }

        public List<string> GetSymbols()
        {
            List<string> temp = new List<string>();

            foreach(Sentences s in KB)
            {
                if(s.symbol != null)
                {
                    temp.Add(s.symbol);
                }
                else
                {
                    foreach (Sentences sC in s.children)
                    {
                        if (sC.symbol != null)
                        {
                            temp.Add(sC.symbol);
                        }
                        else
                        {
                            temp.Add(sC.children[0].symbol);
                            temp.Add(sC.children[1].symbol);
                        }
                    }
                }
            }

            return temp;
        }
    }
}
