using System;
using System.Collections.Generic;

namespace iengine
{
    public class TT : SearchMethod
    {
        public Queue<string> symbols = new Queue<string>();
        public int x, y = 0;

        public TT(string fileName) : base(fileName)
        {
            foreach (string s in hornFormKB.GetSymbols())
            {   // gets every symbol of KB
                if (!symbols.Contains(s)) { symbols.Enqueue(s); }
            }

            TTCheckAll(hornFormKB.KB, hornFormKB.query.symbol, symbols, new TTModel());

            //            Console.WriteLine("x: {0}, y: {1}", x, y);
        }

        public bool TTCheckAll(List<Sentences> KB, string alpha, Queue<string> symbolsQ, TTModel model)
        {

            if (symbolsQ.Count == 0)
            {
                //if(KBTrue(KB, model))
                //{
                //    foreach (KeyValuePair<string, bool> s in model.ModelDictionary)
                //    {
                //        Console.WriteLine("47 {0}, {1}", s.Key, s.Value);
                //    }
                //    x++;
                //   AlphaTrue(alpha, model);
                //}
                //y++;
                //                KBSetUp(KB, model, alpha);
                if (KBTrue(KB, model, alpha))
                {
                    //Console.WriteLine("True");
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string topSymbol = symbolsQ.Dequeue();

                TTModel trueModel = model.NewModel(topSymbol, true);
                TTModel fasleModel = model.NewModel(topSymbol, false);

                return (TTCheckAll(KB, alpha, symbolsQ, trueModel)
                    && (TTCheckAll(KB, alpha, symbolsQ, fasleModel)));
            }
        }

        //check list of sentences with model
        public bool KBTrue(List<Sentences> KB, TTModel model, string alpha)
        {
            foreach (Sentences s in KB)
            {
                KBSetUp(s, model, alpha);
                //if(!EachKBTrue(s, model))
                //{
                //    return false;
                //}
            }
            return true;
        }

        public bool KBSetUp(Sentences KB, TTModel model, string alpha)
        {
            bool childZero;
            bool childOne = true;
            bool alphaBool = true;
            bool andChildOne = false;
            bool andChildZero = false;

            if (KB.connective == "=>")
            {
                if (KB.children[0].connective == null)
                {
                    foreach (KeyValuePair<string, bool> s in model.ModelDictionary)
                    {
                        if (KB.children[0].symbol == s.Key)
                        {
                            childZero = s.Value;
                        }
                        if (alpha == s.Key)
                        {
                            alphaBool = s.Value;
                            //Console.WriteLine(s.Key);
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, bool> s in model.ModelDictionary)
                    {
                        if (KB.children[0].children[0].symbol == s.Key)
                        {
                            andChildZero = s.Value;
                        }

                        if (KB.children[0].children[1].symbol == s.Key)
                        {
                            andChildOne = s.Value;
                        }

                    }
                }
                if (KB.children[1].connective == null)
                {
                    foreach (KeyValuePair<string, bool> s in model.ModelDictionary)
                    {
                        if (KB.children[1].symbol == s.Key)
                        {
                            childOne = s.Value;
                            //Console.WriteLine(s.Key);
                        }
                    }
                }
                if (KB.connective == null)
                {
                    if (KB.children == null)
                    {
                        foreach (KeyValuePair<string, bool> s in model.ModelDictionary)
                        {
                            if (KB.symbol == s.Key)
                            {
                                childOne = s.Value;
                            }
                        }/// ????????????????????????????????
                    }
                }
            }
            childZero = andLogic(andChildOne, andChildZero);

            if (Implication(childOne, childZero) && alphaBool)
            {
                Console.WriteLine("TRUE");

            }

            return true;
        }

        public bool andLogic(bool c0, bool c1)
        {
            return c0 && c1;
        }

        public bool Implication(bool c0, bool c1)
        {
            if (c1 == false)
            {
                if (c0 == true)
                {
                    return false;
                }
            }
            return true;
        }

        //public bool Alpha(bool implication, string alpha)
        //{
        //    if(alpha == true)
        //}
        //check each sentence with model
        public bool EachKBTrue(Sentences KB, TTModel model)
        {
            if (KB.symbol != null)
            {
                if (!EachKBTrueInModel(KB.symbol, model)) { return false; }
            }
            else if (KB.connective == "=>" && KB.children[0].symbol != null)
            {
                // Console.WriteLine(KB.children[0].symbol);
                //Console.WriteLine(KB.children[1].symbol);
                if (!EachKBTrueInModel(KB.children[1].symbol, model))
                {
                    return (!EachKBTrueInModel(KB.children[1].symbol, model));
                }
            }
            else if (KB.connective == "=>" && KB.children[0].symbol == null)
            {
                //Console.WriteLine("{0}{1}{2}{3}{4}", KB.children[0].children[0].symbol, KB.children[0].connective,
                //                  KB.children[0].children[1].symbol, KB.connective, KB.children[1].symbol);
                if (!EachKBTrueInModel(KB.children[1].symbol, model))
                {
                    return (!EachKBTrueInModel(KB.children[0].children[0].symbol, model)) && (!EachKBTrueInModel(KB.children[0].children[1].symbol, model));
                }
            }
            return true;
        }

        public bool EachKBTrueInModel(string KB, TTModel model)
        {
            foreach (KeyValuePair<string, bool> s in model.ModelDictionary)
            {
                //                Console.WriteLine("{0}, {1}", s.Key, s.Value);
                if (s.Key == KB)
                {
                    return s.Value;
                }
            }
            Console.WriteLine("#################################");
            return true;
        }


        //check alpha (query)  in model
        public bool AlphaTrue(string alpha, TTModel model)
        {
            //foreach (KeyValuePair<string, bool> s in model.ModelDictionary)
            //{
            //    if (s.Key == alpha)
            //    {
            //        return s.Value;
            //    }
            //}
            //foreach (TTSymbolBool s in model.ttSymbolBools)
            //{
            //    if (s.symbol == alpha.symbol)
            //    {
            //        Console.WriteLine("{0}, {1}", s.symbol, s.symbolBool);
            //        return true;
            //    }
            //}

            foreach (KeyValuePair<string, bool> s in model.ModelDictionary)
            {
                bool localAlpha;
                foreach (KeyValuePair<string, bool> a in model.ModelDictionary)
                {
                    if (a.Key == alpha)
                    {
                        localAlpha = a.Value;

                        if (s.Value == localAlpha)
                        {
                            //Console.WriteLine("{0}, {1}", alpha, s);
                            return true;
                        }
                    }
                }

            }
            return false;
        }
    }
}

