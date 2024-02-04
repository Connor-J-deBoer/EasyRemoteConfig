//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Connor.RemoteConfigHelper
{
    [System.Serializable]
    public class PayloadValue
    {
        public string key;
        public string type;
        public dynamic value;
    }
}
