//=================================================================\\
//======Copyright (C) 2025 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using MQG.EasyRemoteConfig.Runtime;
using UnityEngine;


// This script contains all of the currently supported remotely configurable types and collections
// Feel free to log any of these variables to see them change!
public class ExampleScript : MonoBehaviour
{
    [RemoteField] private int _testInt = 1;
    [RemoteField] private uint _testUint = 2;
    [RemoteField] private float _testFloat = 2.69f;
    [RemoteField] private double _testDouble = 2.69;
    [RemoteField] private long _testLong = 123456789L;
    [RemoteField] private ulong _testUlong = 123456789UL;
    [RemoteField] private short _testShort = 32767;
    [RemoteField] private ushort _testUshort = 420;
    [RemoteField] private byte _testByte = 255;
    [RemoteField] private string _testString = "ass";
    [RemoteField] private char _testChar = 'A';
    [RemoteField] private bool _testBool = true;

    [RemoteField] private Vector2 _testVector2 = Vector2.one;
    [RemoteField] private Vector3 _testVector3 = Vector3.one;
    [RemoteField] private Quaternion _testQuaternion = Quaternion.identity;
    [RemoteField] private Color _testColor = new Color(1, 1, 1);

    [RemoteField] private List<int> _testIntList = new List<int>() { 1, 2, 3 };
    [RemoteField] private int[]  _testIntArray = new int[] { 1, 2, 3 };
    
    [RemoteField] private Dictionary<string, int> _testDict = new Dictionary<string, int>()
    {
        { "Test One", 1 },
        { "Test Two", 2 },
        { "Test Three", 3 },
    };

    // Update is called once per frame
    void Update()
    {
        foreach (var testInt in _testIntArray)
        {
            Debug.Log(testInt);
        }
    }
}