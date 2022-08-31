using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_Projectile : MonoBehaviour
{
    public int damage =10;
    Transform turret;
    Transform turretTag;
    
    
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Unit") || other.gameObject.CompareTag("Enemy"))
        {
            MG3_Unit unitOther = other.gameObject.GetComponent<MG3_Unit>();
            unitOther.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
