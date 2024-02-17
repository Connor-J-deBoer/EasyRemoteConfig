//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;

namespace Connor.RemoteConfigHelper.Editor
{
    [System.Serializable]
    public class Payload
    {
        public string type = "settings";
        public List<PayloadValue> value;
    }
}
