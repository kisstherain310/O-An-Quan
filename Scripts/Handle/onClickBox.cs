// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Unity.Netcode;

// public class onClickBox : NetworkBehaviour
// {
//     public int index;
//     void OnMouseDown()
//     {
//         handleState();
//     }

//     void handleState()
//     {
//         if (IsClient)
//         {
//             SubmitIncreaseScoreServerRpc();
//         }
//         else
//         {
//             UpdateDirectStateClientRpc();
//         }
//     }
//     public void IncreaseScore()
//     {
//         if (IsClient)
//         {
//             SubmitIncreaseScoreServerRpc();
//         }
//         else
//         {
//             UpdateDirectStateClientRpc();
//         }
//     }

//     [ServerRpc(RequireOwnership = false)]
//     private void SubmitIncreaseScoreServerRpc()
//     {
//         UpdateDirectStateClientRpc();
//     }
//     [ClientRpc(RequireOwnership = false)]
//     private void UpdateDirectStateClientRpc()
//     {
//         if (PointModel.Ins.getValue(index) == 0) return;
//         StateManager.Ins.getCurIndex(index);
//         StateManager.Ins.updatePosHand(index);
//         StateManager.Ins.showDirect(index);
//     }
// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class onClickBox : MonoBehaviour
// {
//     public int index;
//     void OnMouseDown()
//     {
//         handleState();
//     }

//     void handleState()
//     {
//         Debug.Log(index);
//         if (PointModel.Ins.getValue(index) == 0) return;
//         StateManager.Ins.getCurIndex(index);
//         StateManager.Ins.updatePosHand(index);
//         StateManager.Ins.showDirect(index);
//     }
// }






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class onClickBox : NetworkBehaviour
{
    public int index;
    void OnMouseDown()
    {
        handleStateServerRpc();
    }


    [ServerRpc(RequireOwnership = false)]
    void handleStateServerRpc()
    {
        Debug.Log(index);
        if (PointModel.Ins.getValue(index) == 0) return;
        StateManager.Ins.getCurIndex(index);
        StateManager.Ins.updatePosHand(index);
        StateManager.Ins.showDirect(index);
    }
}