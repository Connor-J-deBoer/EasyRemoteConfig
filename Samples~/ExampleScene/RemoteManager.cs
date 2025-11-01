//=================================================================\\
//======Copyright (C) 2025 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using MQG.EasyRemoteConfig.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class RemoteManager : MonoBehaviour
{
    private void OnTap(InputValue inputValue)
    {
        Remote.ApplyRemoteValues();
    }
}
