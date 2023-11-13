using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuideSwitch : MonoBehaviour
{
    private string guideKey ="e";
    private static bool guideOn = false;

    public TextMeshProUGUI textGuide1; // TextMeshProテキストの参照
    public TextMeshProUGUI textGuide2; // TextMeshProテキストの参照

    private string multiLineText1 = "移動\nジャンプ\n箱の移動";
    private string multiLineText2 = "移動\nジャンプ\n箱の移動\n銃\n銃切替";
    private string multiLineText3 = "移動\nジャンプ\n箱の移動\n銃\n銃切替\n操作切替";
    private string multiLineText1_2 = ":  A D\n:  Space\n:  Enter+AD";
    private string multiLineText2_2 = ":  A D\n:  Space\n:  Enter+AD\n:  F\n:  W S";
    private string multiLineText3_2 = ":  A D\n:  Space\n:  Enter+AD\n:  F\n:  W S\n:  Shift";

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(guideKey))
        {
            guideOn = !guideOn;
        }

        if (PauseGame.IsPaused)
        {
            textGuide1.text = "";
            textGuide2.text = "";
        }
        else if (guideOn)
        {
            if (PlayerA.SpellNum > 2)
            {
                textGuide1.text = multiLineText3;
                textGuide2.text = multiLineText3_2;
                //textNum = 3;//説明部分全部表示                
            }
            else if (PlayerA.SpellNum == 0)
            {
                textGuide1.text = multiLineText1;
                textGuide2.text = multiLineText1_2;
                //textNum = 1;//説明部分基本操作表示
            }
            else
            {
                textGuide1.text = multiLineText2;
                textGuide2.text = multiLineText2_2;
                //textNum = 2;//説明部分4の魔法以外表示
            }


        }
        else
        {
            textGuide1.text = "";
            textGuide2.text = "";
        }


    }
}

