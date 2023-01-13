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
    public PlayerController.Controller playerController;
    public Bow.PlayerBow playerBow;

    void Start()
    {
        playerController = new PlayerController.Controller(characterRB, massBorder);
        playerBow = new Bow.PlayerBow(characterGO, bow);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.L))
        {
            bow.SetActive(true);
            if (Input.GetKey(KeyCode.A))
            {
                playerBow.rotateBow(-1);
                Debug.Log("rotating LEFT");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerBow.rotateBow(1);
                Debug.Log("rotating RIGHT");
            }
        }
        else
        {
            bow.SetActive(false);

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
    }
}
