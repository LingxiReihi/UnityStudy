using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MyNetworkManager : NetworkManager
{
    public void StartSomething(int state)
    {
        if (state == 0)
            StartHost();
        else if (state == 1)
            StartServer();
        else if (state == 2)
            StartClient();
    }
}
