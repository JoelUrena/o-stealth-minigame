using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerTransparency : MonoBehaviour {

    //public float a;
   // public Color color = Color.clear;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // 
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag ("TT") == true )
        {
            GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 0.5f);
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TT") == true)
        {
            GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 1f);
        }
    }
}
