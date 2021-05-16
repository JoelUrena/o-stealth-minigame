using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCube : MonoBehaviour {
    public Vector3 move;
    public OcelotFunction script;
    public Vector3 lookPos;

    //public float speed;
    //public Rigidbody rigidBody;
   // public float moveSpeed;
    //public Vector3 PlayerRot;

	// Use this for initialization
	void Start () {
       // rigidBody = GetComponent<Rigidbody>();
        //speed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        GetComponent<Transform>().Translate(move);
    }

   // void OnCollisionEnter(Collision collision)
   // {
         
   // }
}
