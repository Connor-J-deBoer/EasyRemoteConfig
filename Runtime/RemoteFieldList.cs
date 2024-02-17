using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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
