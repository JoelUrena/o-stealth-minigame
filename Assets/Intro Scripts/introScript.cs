using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class introScript : MonoBehaviour {

    public int textNum;
    public Text zeroText;
        public Text oneText;
        public Text twoText;
        public Text threeText;
        public Text fourText;
        public Text fiveText;
        public Text sixText;
        public Text sevenText;

    public GameObject paper;
    public GameObject folder;

    //pt 2
    public GameObject sgtPaper;
    public GameObject banHammer;
    public GameObject judge;
    public GameObject pt2Text1;
    public GameObject pt2Text2;
    public GameObject pt2Text3;

    //pt3-ish
    public GameObject judgeLast;
    public GameObject judgeLastText;

    public bool endOfScene;
    public bool m_SceneLoaded;
    //public GameObject[] text = new GameObject [8];


    // Use this for initialization
    void Start () {
        paper.SetActive(false);
        folder.SetActive(false);
        sgtPaper.SetActive(true);//make sure this is true

        banHammer.SetActive(false);
        judge.SetActive(false);
        pt2Text1.SetActive(false);
        pt2Text2.SetActive(false);
        pt2Text3.SetActive(false);

        judgeLast.SetActive(false);
        judgeLastText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
        if (Input.GetKeyDown(KeyCode.Space))
        {
            textNum += 1;
        }

        if (textNum == 0)
        {
            zeroText.enabled = true;
        }
        if (textNum == 1)
        {
            paper.SetActive(true);
            folder.SetActive(true);
            zeroText.enabled = false;
            oneText.enabled = true;
        }
        if (textNum == 2)
        {
            oneText.enabled = false;
            twoText.enabled = true;
        }
        if (textNum == 3)
        {
            twoText.enabled = false;
            threeText.enabled = true;
        }
        if (textNum == 4)
        {
            threeText.enabled = false;
            fourText.enabled = true;
        }
        if (textNum == 5)
        {
            fourText.enabled = false;
            fiveText.enabled = true;
        }
        if (textNum == 6)
        {
            fiveText.enabled = false;
            sixText.enabled = true;
        }
        if (textNum == 7)
        {
            sixText.enabled = false;
            sevenText.enabled = true;
        }
        if (textNum == 8)
        {
            
            sevenText.enabled = false;
            paper.SetActive(false);
            folder.SetActive(false);
        }
        if (textNum == 9)
        {
            sgtPaper.SetActive(false);
            banHammer.SetActive(true);
            //add judge hammer sound effect and crowded jury
        }
        if (textNum == 10)
        {
            judge.SetActive(true);
            pt2Text1.SetActive(true);
        }
        if (textNum == 11)
        {
            pt2Text1.SetActive(false);
            pt2Text2.SetActive(true);
        }
        if (textNum == 12)
        {
            pt2Text2.SetActive(false);
            pt2Text3.SetActive(true);
        }
        if (textNum == 13)
        {
            pt2Text3.SetActive(false);
            judge.SetActive(false);
            banHammer.SetActive(false);

            judgeLast.SetActive(true);
            judgeLastText.SetActive(true);
        }
        if (textNum == 14)
        {
            endOfScene = true;
            //mad screen fade to black
        }
        if (endOfScene == true)
        {

            SceneManager.LoadScene("Ocelot Stealth");
        }

    }
}
