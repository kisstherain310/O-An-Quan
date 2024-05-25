using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class onClickArRight : NetworkBehaviour
{
    void OnMouseDown()
    {
        if (IsClient)
        {
            SubmitServerRpc();
        }
        else
        {
            UpdateClientRpc();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void SubmitServerRpc()
    {
        UpdateClientRpc();
    }

    [ClientRpc]
    private void UpdateClientRpc()
    {
        StateManager.Ins.setDirect("right");
    }
}
