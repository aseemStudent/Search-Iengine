using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace iengine
{
    public class Sentences
    {
        public string symbol;
        public string connective;
        public Sentences[] children;

        public Sentences()
        {
        }

        public Sentences(string aClause)
        {
            symbol = aClause;
        }

        public Sentences(string aConnective, string[] aChildren)
        {
            connective = aConnective;
            children = new Sentences[aChildren.Length];

            for (int i = 0; i < aChildren.Length; i++)
            {
                this.children[i] = new Sentences(aChildren[i]);
            }
        }

        public Sentences(string aConnective, Sentences[] aChildren)
        {
            connective = aConnective;
            children = aChildren;
  
        }

        public bool AtomicSentence(string aChar)
        {
            return aChar.All(char.IsLetterOrDigit);
        }
    }
}
