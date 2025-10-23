//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Connor.EasyRemoteConfig.Runtime
{
    [Serializable]
    public struct RemoteFieldList<T>
    {
        public List<string> RemoteFields;
        public List<T> RemoteFieldsValue;
        public List<string> RemoteFieldsParentClass;
        public List<string> RemoteGameObjects;
    }
}
