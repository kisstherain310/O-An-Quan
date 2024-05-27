using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class route1toHard : MonoBehaviour
{
    private string typeGame = "hard";
    void OnMouseDown()
    {
        UIManager.Ins.OnClose(1);
        StateManager.Ins.setTypeGame(typeGame);
        StateManager.Ins.openGamePlay();
        NetworkManager.Singleton.StartHost();
    }
}
