using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickArRight : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("on right");
        StateManager.Ins.setDirect("right");
    }
}
