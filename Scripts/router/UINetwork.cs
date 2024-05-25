using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UINetwork : MonoBehaviour
{
    private static string typeGame = "easy";
    void navigate()
    {
        UIManager.Ins.OnClose(1);
        StateManager.Ins.setTypeGame(typeGame);
        StateManager.Ins.openGamePlay();
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }

        GUILayout.EndArea();
    }
    static void StartButtons()
    {
        if (GUILayout.Button("Tạo phòng"))
        {
            NetworkManager.Singleton.StartHost();
            UIManager.Ins.OnClose(1);
            StateManager.Ins.setTypeGame(typeGame);
            StateManager.Ins.openGamePlay();
        }

        if (GUILayout.Button("Vào phòng"))
        {
            NetworkManager.Singleton.StartClient();
            UIManager.Ins.OnClose(1);
            StateManager.Ins.setTypeGame(typeGame);
            StateManager.Ins.openGamePlay();
        }
    }
}