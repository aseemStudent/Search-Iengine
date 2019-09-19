using System;
using System.Collections.Generic;

namespace iengine
{
    public class TTModel
    {
        Dictionary<string, bool> ttModels;// = new Dictionary<string, bool>();


        public TTModel()
        {
            ttModels = new Dictionary<string, bool>();
        }

        public TTModel NewModel(string symbol, bool boolValue)
        {
            TTModel m = new TTModel();
            m.ttModels.Add(symbol, boolValue);
            foreach (KeyValuePair<string, bool> kv in this.ttModels)
            {
                if (!m.ModelDictionary.ContainsKey(kv.Key)) { m.ModelDictionary.Add(kv.Key, kv.Value); }
                Console.WriteLine("{0}, {1}", kv.Key, kv.Value);
            }
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            return m;
        }

        public Dictionary<string, bool> ModelDictionary
        {
            get
            {
                return ttModels;
            }
            set
            {
                ttModels = value;
            }
        }




    }
}