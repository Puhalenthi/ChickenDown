using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor.Callbacks;
using UnityEngine.Diagnostics;
using JetBrains.Annotations;

public class CharacterControl : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D characterRB;
    public GameObject characterGO;
    public GameObject massBorder;
    public GameObject bow;
    public GameObject pivot;
    public GameObject arrowPrefab;
    public bool isHoldingL = false;
    public PlayerController.Controller playerController;
    public Bow.PlayerBow playerBow;
    public Arrow.PlayerArrow playerArrow;

    private int rotationDirection;

    void Start()
    {
        playerController = new PlayerController.Controller(characterRB, massBorder);
        playerBow = new Bow.PlayerBow(characterGO, bow, pivot);
        playerArrow = new Arrow.PlayerArrow(characterGO, bow, arrowPrefab);
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKey(KeyCode.L))
        {
            isHoldingL = true;

            if (Input.GetKeyDown(KeyCode.L))
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
                playerController.moveLeft();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerController.moveRight();
            }
        }


        if (Input.GetKey(KeyCode.Space))
        {
            playerController.increaseMass();
        }
        else
        {
            playerController.decreaseMass();
        }
        //if (Input.GetButton(KeyCode.))
        playerBow.updatePos();
        bow.SetActive(isHoldingL);
    }
}
