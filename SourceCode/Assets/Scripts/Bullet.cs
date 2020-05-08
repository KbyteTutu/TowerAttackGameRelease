using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform m_Target;
    public float bulletSpeed = 20;
    public float attackDmg = 1;//攻击力

    public void SetTarget(Transform target)
    {
        m_Target = target;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = m_Target.position - transform.position;
        if (Vector3.Distance(m_Target.position,transform.position)< bulletSpeed * Time.deltaTime)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * bulletSpeed * Time.deltaTime, Space.World);
        transform.LookAt(m_Target);
    }

    private void HitTarget()
    {
        //杀敌
        MinionHit();
        //销毁
        Destroy(gameObject);
    }

    private void MinionHit()
    {
        MinionHP m_HP = m_Target.GetComponent<MinionHP>();
        if (m_HP!=null)
        {
            m_HP.Damage(attackDmg);
        }
    }
}
