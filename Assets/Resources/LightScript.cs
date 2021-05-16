using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {
    public bool downL;
    public bool upL;
    public Vector3 upMovement;
    public Vector3 downMovement;
    public bool playerSpotted;

	// Use this for initialization
	void Start () {
        downL = true;
        upL = false;
        playerSpotted = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 position = gameObject.transform.position;

        //if (playerSpotted == true)
       // {
            
       // }

        //DownLight();
        //UpLight();

        if (position.z <= -8.1)
        {
            
            downL = false;
            upL = true;
        }
        if (position.z >= -0.80)
        {
            downL = true;
            upL = false;
        }
	}

    public void DownLight()
    {
        if (downL == true)
        {
            GetComponent<Transform>().Translate(downMovement);

        }
    }
    public void UpLight()
    {
        if (upL == true)
        {
            GetComponent<Transform>().Translate(upMovement);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player") == true)
        {
            playerSpotted = true;
        }
       
    }
    //public void OnTriggerExit(Collider other)
   // {
      //  if (other.CompareTag("Player") == true)
       // {
       //     playerSpotted = false;
       // }
   // }


}
