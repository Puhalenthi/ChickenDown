using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    public class PlayerBow
    {
        public GameObject bow;
        public GameObject player;
        public PlayerBow(GameObject p, GameObject go)
        {
            player = p;
            bow = go;
        }

        public void rotateBow(int val)
        {
            bow.transform.RotateAround(player.transform.position, new Vector3(0, 0, val), 50 * Time.deltaTime);
        }

        public void updatePos()
        {
            bow.transform.position = player.transform.position;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
