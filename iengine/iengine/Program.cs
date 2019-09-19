// iengine 2
// aseem & peter

using System;
using System.Collections.Generic;

namespace iengine
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please make sure command is in format <method> <filename>");
            }

            switch (args[0].ToUpper())
            {
                case "TT":
                    Console.WriteLine("Truth Table");
                    TruthTable tt = new TruthTable(args[1]);
                    break;
                case "FC":
                    Console.WriteLine("Forward Chaining");
                    ForwardChaining fc = new ForwardChaining(args[1]);
                    break;
                case "BC":
                    Console.WriteLine("Backward Chaining");
                    BackwardChaining bc = new BackwardChaining(args[1]);
                    break;
                case "R":
                    Console.WriteLine("Resolution");
                    Resolution r = new Resolution(args[1]);
                    break;
                default:
                    Console.WriteLine("NO valid method selected");
                    break;
            }
            Console.WriteLine();
        }
    }
}
