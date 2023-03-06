using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(NetProps.port);
        if(NetProps.mode == "host")
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                NetProps.ip,  // The IP address is a string
                NetProps.port, // The port number is an unsigned short
                "0.0.0.0"
            );
            NetworkManager.Singleton.StartHost();
        } else
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                NetProps.ip,  // The IP address is a string
                NetProps.port // The port number is an unsigned short
            );
            NetworkManager.Singleton.StartClient();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
