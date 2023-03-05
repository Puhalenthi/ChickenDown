using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public class Controller
    {
        public bool isOnGround = true;
        private float jumpForce = 10;
        private float moveForce = 2;
        public float knockbackStrength = 50.0f;
        public Rigidbody2D player;
        public GameObject massBorder;


        public Controller(Rigidbody2D rb, GameObject go)
        {
            player = rb;
            massBorder = go;
        }

        public void moveUp()
        {
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        public void moveLeft()
        {
            player.AddForce(Vector2.left * moveForce, ForceMode2D.Force);
        }
        public void moveRight()
        {
            player.AddForce(Vector2.right * moveForce, ForceMode2D.Force);
        }
        public void Knockback(Vector3 moveDirection, Collider collision)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector2 awayFromPlayer = (collision.gameObject.transform.position - player.transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * knockbackStrength, ForceMode.Impulse);
        }
    }
}
