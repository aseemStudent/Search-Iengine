using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace iengine
{
    public abstract class SearchMethod
    {
        public HornFormKB hornFormKB;
        public static int a = 0;
        public static int b = 0;
        public SearchMethod(string fileName)
        {
            hornFormKB = new HornFormKB(fileName);
        }

        // ckecks for individual sentence like p, q (no connectives)
        public bool AtomicSentence(string aChar)
        {
            return aChar.All(char.IsLetterOrDigit);
        }
    }
}
