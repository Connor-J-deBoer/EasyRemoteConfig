using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RemoteConfigHelper
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class RemoteField : Attribute { }
}

