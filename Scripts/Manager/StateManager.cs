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
    private int turn = 2;
    public Hand hand;
    public GameObject direct;
    public GameObject top;
    public GameObject bot;
    public GameObject flagTop;
    public GameObject flagBot;
    public GameObject[] Stage;
    public GameObject[] StageAct;

    void Start()
    {

    }

    private void changeTurn()
    {
        if (turn == 1)
        {
            top.SetActive(false);
            flagTop.SetActive(false);
            bot.SetActive(true);
            flagBot.SetActive(true);
            turn = 2;
        }
        else if (turn == 2)
        {
            top.SetActive(true);
            flagTop.SetActive(true);
            bot.SetActive(false);
            flagBot.SetActive(false);
            turn = 1;
        }
    }
    public void getCurIndex(int index)
    {
        curIndex = index;
    }

    IEnumerator RepeatedAction(int times, int dir)
    {
        while (true)
        {
            for (int i = 0; i < times; i++)
            {
                curIndex += dir;
                if (curIndex == 1) curIndex = 13;
                if (curIndex == 14) curIndex = 2;
                handleAction(curIndex);
                yield return new WaitForSeconds(0.49f);
                hand.hide();
            }
            if (curIndex + dir == 7 || curIndex + dir == 13) break;
            if (PointModel.Ins.dsPoint[curIndex + dir] > 0)
            {
                curIndex += dir;
                times = PointModel.Ins.dsPoint[curIndex];
                handleHand(curIndex);
                yield return new WaitForSeconds(0.7f);
                updateState(curIndex, 0);
            }
            else if (PointModel.Ins.dsPoint[curIndex + dir] == 0)
            {
                int idxEat = curIndex + 2 * dir;
                if (idxEat == 1) idxEat = 13;
                if (idxEat == 14) idxEat = 2;
                if (PointModel.Ins.dsPoint[idxEat] > 0)
                {
                    handleHand(idxEat);
                    yield return new WaitForSeconds(0.5f);
                    updateState(idxEat, 0);
                    if (PointModel.Ins.dsPoint[idxEat + dir] == 0)
                    {
                        idxEat += 2 * dir;
                        if (idxEat == 1) idxEat = 13;
                        if (idxEat == 14) idxEat = 2;
                        if (PointModel.Ins.dsPoint[idxEat] > 0)
                        {
                            handleHand(idxEat);
                            yield return new WaitForSeconds(0.5f);
                            hand.hide();
                            updateState(idxEat, 0);
                        }
                    }
                    else hand.hide();
                }
                break;
            };
        }
    }

    private void handleHand(int index)
    {
        hand.show();
        hand.moveTo(Stage[index].transform.position);
    }

    private void handleAction(int index)
    {
        handleHand(index);
        updateState(index, PointModel.Ins.dsPoint[index] + 1);
    }
    public void setDirect(string dir)
    {
        hideDirect();
        int times = PointModel.Ins.dsPoint[curIndex];
        int isTop = 1;
        updateState(curIndex, 0);
        if (2 <= curIndex && curIndex <= 6) isTop = 1;
        else isTop = -1;
        if (dir == "left")
        {
            StartCoroutine(RepeatedAction(times, -1 * isTop));
        }
        else if (dir == "right")
        {
            StartCoroutine(RepeatedAction(times, 1 * isTop));
        }
        changeTurn();
    }

    private void updateState(int index, int value)
    {
        if (value > 0) Stage[index].SetActive(true);
        else if (value == 0) Stage[index].SetActive(false);
        PointModel.Ins.updatePoint(index, value);
        PointManager.Ins.updateScore();
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
}
