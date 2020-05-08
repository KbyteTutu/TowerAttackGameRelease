using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionButton : MonoBehaviour
{
    public int minionVal;
    private GameObject val_0;
    private GameObject val_1;
    private GameObject val_2;
    private GameObject val_4;
    private GameObject val_5;

    private Button btn;

    void Start()
    {
        minionVal = 0;
        val_0 = transform.GetChild(0).gameObject;
        val_1 = transform.GetChild(1).gameObject;
        val_2 = transform.GetChild(2).gameObject;
        val_4 = transform.GetChild(3).gameObject;
        val_5 = transform.GetChild(4).gameObject;
        setOff();
        val_0.SetActive(true);


        //获取按钮游戏对象
        GameObject btnObj = transform.gameObject;
        //获取按钮脚本组件
        btn = (Button)btnObj.GetComponent<Button>();
        //添加点击侦听
        btn.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        
        switch (minionVal)
        {
            case 0:
                setOff();
                minionVal = 1;
                val_1.SetActive(true);
                ShopButtons.coinsVal += 1;
                break;
            case 1:
                setOff();
                minionVal = 2;
                val_2.SetActive(true);
                ShopButtons.coinsVal += 2;
                break;
            case 2:
                setOff();
                minionVal = 4;
                val_4.SetActive(true);
                ShopButtons.coinsVal += 4;
                break;
            case 4:
                setOff();
                minionVal = 5;
                val_5.SetActive(true);
                ShopButtons.coinsVal += 5;
                break;
            case 5:
                setOff();
                minionVal = 0;
                val_0.SetActive(true);
                break;
        }

    }

    private void setOff()
    {
        ShopButtons.coinsVal -= minionVal;
        val_0.SetActive(false);
        val_1.SetActive(false);
        val_2.SetActive(false);
        val_4.SetActive(false);
        val_5.SetActive(false);
    }

    public void renew()
    {
        setOff();
        minionVal = 0;
        val_0.SetActive(true);

    }

}
