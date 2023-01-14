using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    public class PlayerBow
    {
        public GameObject bow;
        public GameObject player;
        public GameObject pivot;
        public PlayerBow(GameObject p, GameObject go, GameObject piv)
        {
            player = p;
            bow = go;
            pivot = piv;
        }

        public void rotateBow(int val, int dir)
        {
            bow.transform.RotateAround(player.transform.position, new Vector3(0, 0, dir * val), 100 * Time.deltaTime);
        }

        public void updatePos()
        {
            //bow.transform.position = player.transform.position;
            pivot.transform.position = player.transform.position;
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
