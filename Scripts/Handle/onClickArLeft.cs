using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickArLeft : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("on left");
        StateManager.Ins.setDirect("left");
    }
}
