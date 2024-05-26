using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class onClickBox : NetworkBehaviour
{
    public int index;

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
        if (PointModel.Ins.getValue(index) == 0) return;
        StateManager.Ins.getCurIndex(index);
        StateManager.Ins.updatePosHand(index);
        StateManager.Ins.showDirect(index);
    }
}


