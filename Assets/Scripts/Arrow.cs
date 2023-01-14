using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Arrow : MonoBehaviour
{

    public class PlayerArrow
    {
        public GameObject player;
        public GameObject arrowPrefab;
        public GameObject bowPointer;
        public int projecticleSpeed = 10;

        public PlayerArrow(GameObject go, GameObject bp, GameObject ap)
        {
            player = go;
            bowPointer = bp;
            arrowPrefab = ap;
        }

        public void Launch(float degree, float power)
        {
            GameObject instantiatedArrow = Instantiate(arrowPrefab, bowPointer.transform.position, bowPointer.transform.rotation);
            Destroy(instantiatedArrow, 3);
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
