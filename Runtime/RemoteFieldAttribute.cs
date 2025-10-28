//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using UnityEngine;

namespace MQG.EasyRemoteConfig.Runtime
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class RemoteFieldAttribute : PropertyAttribute { }
}