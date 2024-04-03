using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static StateManager ins;
    public static StateManager Ins
    {
        get { return StateManager.ins; }
    }
    void Awake()
    {
        StateManager.ins = this;
    }
    private int curIndex;
    public Hand hand;
    public GameObject direct;
    public GameObject[] Stage;
    public GameObject[] StageAct;

    void Start()
    {
        hand.hide();
        this.hideDirect();
    }

    public void showDirect(int index)
    {
        direct.SetActive(true);
        direct.transform.position = StageAct[curIndex].transform.position;
    }
    private void hideDirect()
    {
        direct.SetActive(false);
    }

    public void updatePosHand(int index)
    {
        hand.show();
        hand.updatePos(Stage[index].transform.position);
    }

    public void getCurIndex(int index)
    {
        curIndex = index;
    }

}
