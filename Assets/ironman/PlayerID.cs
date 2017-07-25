using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour
{
    [SyncVar] public string playerUniqueName;
    private NetworkInstanceId playerNetID;

    public override void OnStartLocalPlayer()
    {
        GetIdentity();
        SetIdentity();
    }

    void Update ()
    {
        if (name == "" || name == "ironmanPrefab 1(Clone)")
        {
            SetIdentity();
        }
	}

    [Client]
    private void GetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    private void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            transform.name = playerUniqueName;
        }
        else
        {
            transform.name = MakeUniqueIdentity();
        }
    }

    [Command]
    private void CmdTellServerMyIdentity(string identity)
    {
        playerUniqueName = name;
    }

    private string MakeUniqueIdentity()
    {
        return "Player" + playerNetID.ToString(); 
    }
}
