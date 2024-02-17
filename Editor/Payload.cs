//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Connor.RemoteConfigHelper.Editor
{
    [System.Serializable]
    public class Payload
    {
        public string type = "settings";
        public List<PayloadValue> value;
    }
}
