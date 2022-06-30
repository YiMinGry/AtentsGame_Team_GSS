using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Enemy : MonoBehaviour
{
    Rigidbody rigid;
    public float moveSpeed = 10.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        rigid.MovePosition(transform.position - Vector3.right * moveSpeed * this.transform.localScale.z * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "MG2_Fish_Border")
        {
            Destroy(this.gameObject);
        }
        else if (other.transform.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}



