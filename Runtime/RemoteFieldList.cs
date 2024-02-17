//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using System.Collections.Generic;

namespace Connor.RemoteConfigHelper.Runtime
{
    [Serializable]
    public class RemoteFieldList
    {
        public List<string> RemoteFields;
        public List<string> FieldDeclaringTypeNames;
        public List<string> RemoteObjects;
    }
}
