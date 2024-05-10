// Copyright © Connor deBoer 2024, All Rights Reserved

using Connor.RemoteConfigHelper.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _exampleText;

    [RemoteField]
    [SerializeField] 
    private string _exampleName = "This super duper long name that is just way way too long";

    [RemoteField]
    [Range(0, 1000)]
    [SerializeField] 
    private int exampleInt = 123;

    [RemoteField]
    public bool ExampleBool = false;

    [RemoteField]
    [SerializeField]
    private List<int> ExampleList = new List<int>()
    {
        10,
        20,
        30
    };

    [ContextMenu("Update Remote")]
    public void CallRemote()
    {
        HandleRemoteFields.Service.UpdateFields();
    }

    private void Update()
    {
        _exampleText.text = _exampleName;
    }
}
