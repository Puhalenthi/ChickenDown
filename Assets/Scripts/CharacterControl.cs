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
    public bool isHoldingL = false;
    public PlayerController.Controller playerController;
    public Bow.PlayerBow playerBow;
    public Arrow.PlayerArrow playerArrow;
    public SpriteRenderer _renderer;

    private int rotationDirection;

    void Start()
    {
        bow.SetActive(isHoldingL);
        if (!IsOwner) return;
        playerController = new PlayerController.Controller(characterRB, massBorder);
        playerBow = new Bow.PlayerBow(center, bow, pivot);
        playerArrow = new Arrow.PlayerArrow(characterGO, bow, arrowPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        playerBow.updatePos();
        if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.Space))
        {
            isHoldingL = true;

            if (Input.GetKeyDown(KeyCode.L) || Input.GetKey(KeyCode.Space))
            {
                if (playerBow.bow.transform.rotation.z >= -180 && playerBow.bow.transform.rotation.z < 180)
                {
                    rotationDirection = -1;
                }
                else
                {
                    rotationDirection = 1;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerBow.rotateBow(-1, rotationDirection);
                //Debug.Log("rotating LEFT");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerBow.rotateBow(1, rotationDirection);
                //Debug.Log("rotating RIGHT");
            }
        }
        else
        {
            if (isHoldingL)
            {
                isHoldingL= false;
                playerArrow.Launch(12, 12);
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

        //if (Input.GetButton(KeyCode.))
        bow.SetActive(isHoldingL);
    }
}
