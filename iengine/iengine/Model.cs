using System;

using System.Collections.Generic;
namespace iengine
{

    public class Model
    {
        Dictionary<string, bool> ttModels = new Dictionary<string, bool>();

        public Model()
        {
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
