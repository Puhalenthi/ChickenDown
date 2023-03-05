using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class ClientPlayerName : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>("Player0", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        networkPlayerName.Value = "Player" + (OwnerClientId + 1);
        playerName.text = networkPlayerName.Value.ToString();
    }
}
