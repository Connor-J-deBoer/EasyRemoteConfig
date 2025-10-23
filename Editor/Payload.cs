//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;

namespace Connor.EasyRemoteConfig.Editor
{
    [System.Serializable]
    public class Payload
    {
        public string type = "settings";
        public List<PayloadValue> value;
    }
}
