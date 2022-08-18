using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage =10;
    Transform turret;
    Transform turretTag;
    private void Awake()
    {
        turret = transform.parent.parent.parent;
        turretTag = turret.parent;
    }
    
    private void OnCollisionEnter(Collision other)
    {

        if ((other.gameObject.CompareTag("Unit") || other.gameObject.CompareTag("Enemy")) && (!turretTag.CompareTag(other.gameObject.tag)))
        {
            Unit unitOther = other.gameObject.GetComponent<Unit>();
            unitOther.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
