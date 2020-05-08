using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionHP : MonoBehaviour
{
    public float initHP = 100;
    public float HP;
    private float currentHP;

    public Transform CanvasCenter;
    public Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = initHP;
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("toCamera", 0, 0.1f);
    }

    private void toCamera()
    {
        Vector3 dir =transform.position - Camera.main.transform.position ;
        Quaternion toward = Quaternion.LookRotation(dir);
        CanvasCenter.rotation = Quaternion.Euler(new Vector3(toward.eulerAngles.x, 0,0));
    }

    public void Damage(float dmgAmount)
    { 
        currentHP -= dmgAmount;
        hpBar.fillAmount = currentHP / initHP;
        HP = currentHP;
        if (currentHP <= 0)
        {
            MinionSpawner.minionAlive--;
            Debug.Log(MinionSpawner.minionAlive);
            Destroy(gameObject);
        }
    }
}
