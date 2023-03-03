using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI playerName;
    public GameObject player;
    //public StartGame startingScript;

    // Unity program to display the name of the player in the game
    // Get the pName variable from the StartGame script

    // Start is called before the first frame update
    void Start()
    {
        //playerName.text = startingScript.pName.text;
        playerName.text = GameObject.Find("GameObject").GetComponent<StartGame>().pName.text;
        Debug.Log(playerName.text);
    }

    // Update is called once per frame
    void Update()
    {
        // Display the playerName above the player in the game by using the player's position. It should be 10 units above the player
        playerName.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
    }
}
