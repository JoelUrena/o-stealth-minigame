using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {
    public int enemyHealth;
    public GameObject healthBar;
    public float healthTimer;
    public bool isHealthMeterDisplayed;
    public bool enemyGoneHayWire;
    GameObject human;
    //NavMeshAgent enemy;
	// Use this for initialization
	void Start () {
        enemyHealth = 5;
        healthBar.SetActive(false);
        //human = GameObject.FindGameObjectWithTag("human");
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth == 0)
        {
            Debug.Log("Doom");
            healthBar.SetActive(false);//
            Destroy(gameObject);


        }
        if (isHealthMeterDisplayed == true)
        {
            healthBar.SetActive(true);
            healthBar.GetComponent<Slider>().value = enemyHealth;
            healthTimer += Time.deltaTime;
            if (healthTimer >= 4)
            {
                healthBar.SetActive(false);
                isHealthMeterDisplayed = false;
                healthTimer = 0;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Bullet") == true)
        {
            isHealthMeterDisplayed = true;
            Debug.Log("hello");
            enemyHealth -= 1;


        }
    }
}
