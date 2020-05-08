using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAI : MonoBehaviour
{
    public float moveSpeed = 10;
    Transform target;
    private int pointIndex= 0;
    public int waySelect = 0;
    private Transform[] selected;

    public List<int> inputQueue;

    // Start is called before the first frame update
    void Start()
    {
        waySelect = MinionSpawner.waySelect;
        if (waySelect==1)
        {
            selected = WayPoints.way1;
        }
        else if (waySelect ==2)
        {
            selected = WayPoints.way2;
        }
        else
        {
            selected = WayPoints.way3;
        }
        target = selected[pointIndex];

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized* moveSpeed * Time.deltaTime, Space.World);
        if (Vector3.Distance(target.position,transform.position)<0.1f)
        {
            pointIndex++;
            if (pointIndex >= selected.Length)
            {
                minionDestroy();
                return;
            }
            target = selected[pointIndex];
        }
    }

    private void minionDestroy()
    {
        MinionSpawner.minionAlive--;
        MinionSpawner.arrivedMinions++;
        Destroy(gameObject);
    }
}
