using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour
{
    public float speed = 20;
    public Rigidbody2D rb;

    void Update()
    {
        rb.velocity = transform.right * speed;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}