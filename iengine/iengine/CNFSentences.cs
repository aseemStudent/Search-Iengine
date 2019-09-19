using System;
using System.Collections.Generic;
namespace iengine
{
    public class CNFSentences
    {
        public string symbol;
        public CNFSentences[] children;
        public bool Negate = false;

        public CNFSentences(string aClause, bool b)
        {
            symbol = aClause;
            Negate = b;
        }

        public CNFSentences(Dictionary<string, bool> temp)
        {
            children = new CNFSentences[temp.Count];
            int i = 0;
            foreach (KeyValuePair<string, bool> kv in temp)
            {
                children[i++] = new CNFSentences(kv.Key, kv.Value);
            }

        }
    }
}
