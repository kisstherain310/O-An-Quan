using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class onClickArRight : NetworkBehaviour
{
    private void OnMouseDown()
    {
        if (IsClient)
        {
            handleStateServerRpc();
        }
        else
        {
            handleStateClientRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void handleStateServerRpc()
    {
        handleStateClientRpc();
    }

    [ClientRpc]
    private void handleStateClientRpc()
    {
        handleState();
    }

    private void handleState()
    {
        StateManager.Ins.setDirect("right");
    }
}