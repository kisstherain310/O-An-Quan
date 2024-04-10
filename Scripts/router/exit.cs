using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    void OnMouseDown()
    {
        PointModel.Ins.resetPoint();
        PointManager.Ins.updateScore();
        StateManager.Ins.resetStage();
        StateManager.Ins.closeGamePlay();
        UIManager.Ins.OnOpen(0);
    }
}
