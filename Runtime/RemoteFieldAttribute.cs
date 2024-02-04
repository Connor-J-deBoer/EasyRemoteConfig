//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using UnityEngine;

namespace Connor.RemoteConfigHelper
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class RemoteFieldAttribute : PropertyAttribute { }
}