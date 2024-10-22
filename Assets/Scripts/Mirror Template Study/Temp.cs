using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Temp : NetworkManager
{
    void OnEnable()
    {
        transport = GetComponent<Transport>();
    }
}
