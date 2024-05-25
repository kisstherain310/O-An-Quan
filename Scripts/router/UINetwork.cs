using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UINetwork : MonoBehaviour
{
    private static string typeGame = "duo";
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(130, 30, 300, 2000));

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
            UIManager.Ins.OnClose(1);
            StateManager.Ins.setTypeGame(typeGame);
            StateManager.Ins.openGamePlay();
            NetworkManager.Singleton.StartHost();
        }

        if (GUILayout.Button("Vào phòng"))
        {
            UIManager.Ins.OnClose(1);
            StateManager.Ins.setTypeGame(typeGame);
            StateManager.Ins.openGamePlay();
            NetworkManager.Singleton.StartClient();
        }

    }
}