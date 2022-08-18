using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillProjectile : MonoBehaviour
{
    public int damage = 10;
    public AudioClip skillSound;
    

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            Unit unitOther = other.gameObject.GetComponent<Unit>();
            //SoundManager.instance.SFXPlay("meteo", skillSound);
            unitOther.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            //SoundManager.instance.SFXPlay("meteo", skillSound);
            Destroy(this.gameObject);
        }
    }
}
