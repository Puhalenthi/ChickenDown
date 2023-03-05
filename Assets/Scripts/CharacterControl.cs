using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using Unity.Netcode;

public class CharacterControl : NetworkBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D characterRB;
    public GameObject characterGO;
    public GameObject massBorder;
    public GameObject bow;
    public GameObject pivot;
    public GameObject center;
    public GameObject arrowPrefab;
    public bool isHoldingL = true;
    public PlayerController.Controller playerController;
    public Bow.PlayerBow playerBow;
    public Arrow.PlayerArrow playerArrow;
    public SpriteRenderer _renderer;
    Vector3 moveDirection;

    private int rotationDirection;

    void Start()
    {
        if (!IsOwner) return;
        playerController = new PlayerController.Controller(characterRB, massBorder);
        playerBow = new Bow.PlayerBow(center, bow, pivot);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        playerBow.updatePos();

        if (playerBow.bow.transform.rotation.z >= -180 && playerBow.bow.transform.rotation.z < 180)
        {
            rotationDirection = -1;
        }
        else
        {
            rotationDirection = 1;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            playerBow.rotateBow(-1, rotationDirection);
            //Debug.Log("rotating LEFT");
        }
        else if (Input.GetKey(KeyCode.E))
        {
            playerBow.rotateBow(1, rotationDirection);
            //Debug.Log("rotating RIGHT");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerController.moveUp();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            playerController.moveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            playerController.moveRight();
        }
    }

    void OnTriggerEnter(Collider attack)
    {
        if (attack.gameObject.tag == "Player")
        {
            moveDirection = characterRB.transform.position - attack.transform.position;
            playerController.Knockback(moveDirection, attack);
        }
    }
}