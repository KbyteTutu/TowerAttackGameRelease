using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour
{
    private Button spBtn;
    private GameObject spBlue;
    private GameObject spGreen;
    private GameObject spPurple;

    private int spawnerSelect;//选择哪个出生点

    public static bool spawning = false;

    private Button goBtn;//出发按钮

    private Text coins;

    public static int coinsVal = 0;

    public GameObject spawner;

    public GameObject noCredit;
    public GameObject needMore;

    public  List<int> queue;

    private GameObject helpPgObj;

    private Button helpBtn;
    private Button helpPage;
    private GameObject winObj;
    private Button winBtn;
    private GameObject loseObj;
    private Button loseBtn;

    private GameObject purpleStar;
    private GameObject blueStar;
    private GameObject greenStar;
    public static bool purpleWin;
    public static bool blueWin;
    public static bool greenWin;
    private GameObject winAll;

    private GameObject renewObj;


    void Start()
    {
        spawnerSelect = 1;

        spBlue = GameObject.Find("Canvas/Shop/StartSelect/blue");
        spGreen = GameObject.Find("Canvas/Shop/StartSelect/green");
        spPurple = GameObject.Find("Canvas/Shop/StartSelect/purple");
        spSetOff();

        GameObject textObj = GameObject.Find("Canvas/Shop/Coins");
        coins = (Text)textObj.GetComponent<Text>();


        GameObject btnObj = GameObject.Find("Canvas/Shop/StartSelect");
        spBtn = (Button)btnObj.GetComponent<Button>();
        spBtn.onClick.AddListener(onClick_Spawn);


        GameObject goBtnObj = GameObject.Find("Canvas/Shop/Go");
        goBtn = (Button)goBtnObj.GetComponent<Button>();
        goBtn.onClick.AddListener(onClick_Go);

        GameObject helpBtnObj = GameObject.Find("Canvas/HelpBtn");
        helpBtn = (Button)helpBtnObj.GetComponent<Button>();
        helpBtn.onClick.AddListener(onClick_HelpBtn);

        helpPgObj = GameObject.Find("Canvas/Help");
        helpPage = (Button)helpPgObj.GetComponent<Button>();
        helpPage.onClick.AddListener(onClick_HelpPg);
        helpPgObj.SetActive(false);

        winObj = GameObject.Find("Canvas/Win");
        winObj.SetActive(false);
        winBtn = (Button)winObj.GetComponent<Button>();
        winBtn.onClick.AddListener(OnClick_win);

        renewObj = GameObject.Find("Canvas/Shop/Renew");
        Button renewBtn = (Button)renewObj.GetComponent<Button>();
        renewBtn.onClick.AddListener(onClick_Renew);


        loseObj = GameObject.Find("Canvas/Lose");
        loseObj.SetActive(false);
        loseBtn = (Button)loseObj.GetComponent<Button>();
        loseBtn.onClick.AddListener(OnClick_Lose);

        purpleStar = GameObject.Find("Canvas/Stars/PurpleStar");
        purpleStar.SetActive(false);
        blueStar = GameObject.Find("Canvas/Stars/BlueStar");
        blueStar.SetActive(false);
        greenStar = GameObject.Find("Canvas/Stars/GreenStar");
        greenStar.SetActive(false);

        winAll= GameObject.Find("Canvas/WinAll");
        winAll.SetActive(false);
        Button winAllBtn = winAll.GetComponent<Button>();
        winAllBtn.onClick.AddListener(onClickWinall);

        purpleWin = false;
        blueWin = false;
        greenWin = false;

    }

    private void onClick_Renew()
    {
        GameObject minionsQue = GameObject.Find("Canvas/Shop/MinionQueue");

        MinionButton[] g = minionsQue.GetComponentsInChildren<MinionButton>();
        for (int i = 0; i < g.Length; i++)
        {
            g[i].renew();
        }
        coinsVal = 0;

    }

    private void onClickWinall()
    {
        winAll.SetActive(false);
    }

    public void winAllShow()
    {
        winObj.SetActive(false);
        winAll.SetActive(true);
    }

    public void winShow()
    {
        winObj.SetActive(true);
    }

    public void loseShow()
    {
        loseObj.SetActive(true);
    }

    private void OnClick_Lose()
    {
        loseObj.SetActive(false);
    }

    private void OnClick_win()
    {
        winObj.SetActive(false);
    }
    private void onClick_HelpPg()
    {
        helpPgObj.SetActive(false);
    }

    private void onClick_HelpBtn()
    {
        helpPgObj.SetActive(true);
    }

    void Update()
    {
        var v = 20 - coinsVal;
        if (v<0)
        {
            coins.color = Color.red;
        }
        else
        {
            coins.color = Color.yellow;
        }
        coins.text = v.ToString();
    }


    private void onClick_Go()
    {
        queue = new List<int> { };//清空队列
        queue.Add(spawnerSelect);

        GameObject minionsQue = GameObject.Find("Canvas/Shop/MinionQueue");

        Button[] a = minionsQue.GetComponentsInChildren<Button>();
        int exist = 0;
        for (int i = 0; i < a.Length; i++)
        {
            var temp = a[i].GetComponent<MinionButton>().minionVal;
            queue.Add(temp);
            if (temp>0)
            {
                exist++;
            }
        }

        if (exist <6 )
        {
            Instantiate(needMore, transform);
            return;
        }
        if (coinsVal>20)
        {
            Instantiate(noCredit,transform);
            return;
        }
        //查看是否有存活spawner
        if (spawning)
        {
            return;
        }
        

        Instantiate(spawner);
        spawning = true;
        MinionSpawner.arrivedMinions = 0;

        Debug.Log(showLog(queue));
    }


    private String showLog(List<int> a)
    {
        String re = "";
        foreach (var item in a)
        {
            re = re + item+",";
        }
        return re;
    }

    void onClick_Spawn()
    {
        
        switch (spawnerSelect)
        {
            case 1:
                spSetOff();
                spawnerSelect = 2;
                spBlue.SetActive(true);
                break;
            case 2:
                spSetOff();
                spawnerSelect = 3;
                spGreen.SetActive(true);
                break;
            case 3:
                spSetOff();
                spawnerSelect = 1;
                spPurple.SetActive(true);
                break;

        }
        Debug.Log("出生点："+spawnerSelect);

    }

    private void spSetOff()
    {
        spBlue.SetActive(false);
        spGreen.SetActive(false);
        spPurple.SetActive(false);
    }

}
