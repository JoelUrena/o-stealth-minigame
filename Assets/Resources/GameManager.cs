using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public bool isWinTrue;
    public bool isLoseTrue;
    public GameObject Key;
    public GameObject doorSlide;
    public Vector3 up;
    public GameObject blackScreen;
    public float timeLVL;
    public GameObject timerText;
	// Use this for initialization
	void Start () {
        GetComponent<Animator>().enabled = false;
        timerText.SetActive(true);
        timeLVL = 30;
    }
	

   
	// Update is called once per frame
	void Update () {


        TimeRemaing();

        if (isWinTrue == true)
        {
            doorSlide.GetComponent<Transform>().Translate(up);

        }
       if (isLoseTrue == true)
        {
            GetComponent<Animator>().enabled = true;
            timerText.SetActive(false);
        }
       if (timeLVL <= 0)
        {
            isLoseTrue = true;
        }
	}

    public void TimeRemaing()
    {
        timeLVL -= Time.deltaTime;
        timerText.GetComponent<Text>().text = "Time: " + (timeLVL).ToString();//the text of the timer in the canvas is equal to the integer timer 
    }

   
}
 