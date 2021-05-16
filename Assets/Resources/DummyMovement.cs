using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion position = gameObject.transform.rotation;
		if (position.y == -90.007)
        {
            Debug.Log("quternion");
            position.y = 0;
        }
	}
}
