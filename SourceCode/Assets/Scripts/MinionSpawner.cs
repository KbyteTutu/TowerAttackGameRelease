using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MinionSpawner : MonoBehaviour
{

    //public Wave[] minionWaves;
    public GameObject minionPrefab_1;
    public GameObject minionPrefab_2;
    public GameObject minionPrefab_4;
    public GameObject minionPrefab_5;

    public float spawnInterval = 0.5f;//生成的间隔时间
    private float countDown = 0;

    public Transform spawnPositionGreen;
    public Transform spawnPositionBlue;
    public Transform spawnPositionPurple;

    private Transform currentSpawn;//当前生成位置

    public static int minionAlive;//存活单位数，
    public static int arrivedMinions;//成功的单位数
    private int countNum;//总单位数，包括空值
    private int currentNum = 1;//当前生成的单位序号
    public static int waySelect = 0;

    private bool spawnOver;

    private List<int> minionQueue;//进攻序列
    private Text t;

    //成就对象
    private GameObject purpleStar;
    private GameObject blueStar;
    private GameObject greenStar;

    private GameObject winAll;




    // Start is called before the first frame update
    void Start()
    {
        purpleStar = GameObject.Find("Canvas/Stars/PurpleStar");
        blueStar = GameObject.Find("Canvas/Stars/BlueStar");
        greenStar = GameObject.Find("Canvas/Stars/GreenStar");

        winAll = GameObject.Find("Canvas/WinAll");


        GameObject shopObj = GameObject.Find("Canvas");
        Canvas shop = (Canvas)shopObj.GetComponent<Canvas>();
        minionQueue = shop.GetComponent<ShopButtons>().queue;

        spawnOver = false;

        countNum = getMinionNum();
        minionAlive = 0;
        //设定起始位置
        if (minionQueue[0] == 1)
        {
            currentSpawn = spawnPositionPurple;
            waySelect = 1;
        }
        else if (minionQueue[0] == 2)
        {
            currentSpawn = spawnPositionBlue;
            waySelect = 2;
        }
        else if (minionQueue[0] == 3)
        {
            currentSpawn = spawnPositionGreen;
            waySelect = 3;
        }
        GameObject countText = GameObject.Find("Canvas/Arrived");
        t = countText.GetComponent<Text>();

    }




    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            countDown = spawnInterval;//重置倒计时
            if (spawnOver == false)
            {
                SpawnMinion(getMinionType(currentNum));
                currentNum++;
            }
            if (currentNum > countNum)
            {
                spawnOver = true;
            }


        }
        //判断到终点的球数量
        if (arrivedMinions > 0)
        {
            t.text = (4 - arrivedMinions).ToString();
        }
        //设置胜利条件
        //在全部生成完毕后如果存活数0则结束
        if (spawnOver)
        {
            if (arrivedMinions == 4)
            {
                Debug.Log("成功");
                GameObject.Find("Canvas").GetComponent<ShopButtons>().winShow();
                lightStar();
                reset();
            }
            else if (minionAlive == 0)
            {
                Debug.Log("全灭");
                GameObject.Find("Canvas").GetComponent<ShopButtons>().loseShow();
                reset();
            }
        }
    }

    private void lightStar()
    {
        switch (waySelect)
        {
            case 1:
                purpleStar.SetActive(true);
                ShopButtons.purpleWin = true;
                break;
            case 2:
                blueStar.SetActive(true);
                ShopButtons.blueWin = true;
                break;
            case 3:
                greenStar.SetActive(true);
                ShopButtons.greenWin = true;
                break;
        }
         
        if (ShopButtons.greenWin && ShopButtons.blueWin && ShopButtons.purpleWin)
        {
            GameObject.Find("Canvas").GetComponent<ShopButtons>().winAllShow();
        }


    }



    private void reset()
    {
        t.text = "4";
        ShopButtons.spawning = false;

        Destroy(gameObject);
    }

    private int getMinionType(int currentNum)
    {
        if (minionQueue[currentNum] > 0)
        {
            minionAlive++;//每生成一个非空类型的，存活数加一
        }
        return minionQueue[currentNum];
    }

    private void SpawnMinion(int type)
    {
        //StartCoroutine(MinionGo());

        switch (type)
        {
            case 0:
                //空过
                break;
            case 1:
                //一血
                Instantiate(minionPrefab_1, currentSpawn.position, currentSpawn.rotation);
                break;
            case 2:
                //二血
                Instantiate(minionPrefab_2, currentSpawn.position, currentSpawn.rotation);
                break;
            case 4:
                //四血
                Instantiate(minionPrefab_4, currentSpawn.position, currentSpawn.rotation);
                break;
            case 5:
                //五血
                Instantiate(minionPrefab_5, currentSpawn.position, currentSpawn.rotation);
                break;
        }


    }


    private int getMinionNum()
    {
        int cnt = 1;
        bool flag = false;
        for (int i = 9; i >= 1; i--)
        {
            if (flag == false)
            {
                if (minionQueue[i] != 0)
                {
                    flag = true;
                }
            }
            else
            {
                cnt++;
            }

        }
        return cnt;
    }

    //IEnumerator waveMinion()
    //{
    //    if (waveIndex>=minionWaves.Length)
    //    {
    //        yield break;
    //    }
    //    Wave wave = minionWaves[waveIndex];
    //    minionAlive = wave.count;
    //    for (int i = 0; i < wave.count; i++)
    //    {
    //        waySelect = waveIndex;
    //        Instantiate(wave.minionPrefab, spawnPosition.position, spawnPosition.rotation);
    //        yield return new WaitForSeconds(1/wave.rate);
    //    }
    //    waveIndex++;
    //}

}
