using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class route1toEasy : MonoBehaviour
{
    void OnMouseDown()
    {
        UIManager.Ins.OnClose(1);
        StateManager.Ins.openGamePlay();
    }
}
