using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class Turret : MonoBehaviour
{
    public float range = 5;
    public string targetTag = "Minion";
    public Transform target;
    public Transform rotateCenter;//炮台旋转中心
    public Transform muzzle;
    public GameObject bulletPf;

    public float rotSpeed = 0.01f;
    public float shootRate = 2;
    private float countDown = 0;

    public int N;   //描点个数
    public float width;  //圆的粗细
    public Material CircleMaterial; //材质


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("searchTarget", 0, 0.5f);
        countDown = 1 / shootRate;
    }



    // Update is called once per frame
    void Update()
    {

        if (target == null) return;
        aimTarget();
        //倒计时
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            
            GameObject bulletObj = Instantiate(bulletPf, muzzle.position, muzzle.rotation);

            Bullet bullet = bulletObj.GetComponent<Bullet>();

            if (bullet == null)
            {
                bullet = bulletObj.AddComponent<Bullet>();
            }

            bullet.SetTarget(target);

            countDown = 1 / shootRate;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    private void searchTarget()
    {
        if (target!=null)
        {
            float tempTargetDis = Vector3.Distance(target.position, transform.position);
            if (tempTargetDis<range)
            {
                return;
            }
        }

        GameObject[] Minions =  GameObject.FindGameObjectsWithTag(targetTag);
        float minDis = Mathf.Infinity;
        Transform nearest = null;
        foreach (GameObject minion in Minions)
        {
            float tempDis = Vector3.Distance(minion.transform.position, transform.position);
            if (tempDis<minDis)
            {
                minDis = tempDis;
                nearest = minion.transform;
            }
        }
        if (minDis < range)
        {
            target = nearest;
        }
        else
        {
            target = null;
        }

    }

    private void aimTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        Quaternion temp = Quaternion.Lerp(rotateCenter.rotation, rotation, Time.deltaTime * rotSpeed);
        rotateCenter.rotation = Quaternion.Euler(new Vector3(0, temp.eulerAngles.y, 0));
    }

}
