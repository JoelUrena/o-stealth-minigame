using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazzardScript : MonoBehaviour {
    public EnemyBehavior enemyBehavior;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag ("human") == true)
        {
            enemyBehavior.enemyGoneHayWire = true;

        }
    }
}
