using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWepon : MonoBehaviour {
    public int gunStrength = 5;//the most powereful weapon you have //Im also thinking you should aim to use this weapon
    public int gunAmmo = 3;

    public int strangle = 5;//you can only use this behind enemy (one block radius) (you now have body in your items that you must dispose of you you will walk slow)
    //if player is behid enemy, strangleAbility = true;


    public int punch = 1; //weak but it works. (enemy will use this against you) 

    public GameObject carboardBox;//can be applied //lowers enemy radius, has a meter for usage
    //mgs box troll

    //public OcelotFunction ocelotFunction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}


}
