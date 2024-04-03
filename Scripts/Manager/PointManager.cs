using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public TextMeshProUGUI[] listScoreText;

    void Start()
    {
        initScore();
    }

    private void initScore(){
        for(int i = 0; i < 14; i++){
            listScoreText[i].text = PointModel.Ins.dsPoint[i].ToString();
        }
    }
}
