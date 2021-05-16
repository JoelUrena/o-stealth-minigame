using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowEnemy : MonoBehaviour {
    //public GameObject Obj;
    //public Camera mCamera;
    //public RectTransform rt;
    public GameObject nameLabel;//what we name the object
	// Use this for initialization
	void Start () {
		
	}
	//the way how i set this up was getting a speheres position and translating it to a txt
	// Update is called once per frame
	void Update () {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);//create a new vector 3 named named pos and make it equal to the main camera an then (takes a world point and converts it into a screen point ((canvas))) transform.position of this sphere
        nameLabel.transform.position = namePos;//thi ssets our text , name label, 's position equal to name postition 
	}
    //void OnGUI()
    //{
     //   var gotransform = Obj.GetComponent<Transform>();//new variable reated outta nowhere, named gotransform, is equal to the enemy's transform (dynamic movement)
     //   rt.position = mCamera.WorldToScreenPoint(gotransform.position);//rector transform.position is equal to the main camera.world to screen point (goto transform position) which we stated is the players dynamic position

    //}
}
