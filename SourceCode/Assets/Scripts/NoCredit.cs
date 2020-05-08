using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class NoCredit : MonoBehaviour
{
    private float count = 3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if (count<=0)
        {
            Destroy(gameObject);
        }
    }
}
