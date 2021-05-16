using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove {
    GameObject target;
    public float delayTimer;
    public LightScript lightM;
    public GameManager gm;


	// Use this for initialization
	void Start () 
    {
        Init();

	}
	
	// Update is called once per frame
	void Update () 
    {
        
        delayTimer +=  Time.deltaTime; 
        if (!turn)//if its not the players turn (bool found in tactics move)
        {
            delayTimer = 0f;//when it's not their turn timer is zero
            return;//dont execute the update function


        }
       
        if (!moving)
        {
            if (delayTimer >= 1.5f)//1 second delay before the npc is allowed to think
            {

                FindNearestTarget();
                CalculatePath();
                FindSelectableTiles();
                actualTargetTile.target = true;//the npc will click on the player 
            }
        }
        else if (lightM.playerSpotted == true)
        {
            Debug.Log("We gottem boyz");
            Move();
        }
        else
        {
            //if (lightM.playerSpotted == true)
           // {
             //   Debug.Log("We gottem boyz");
                //Move();
            //}
            lightM.DownLight();
            lightM.UpLight();

            



            TurnManager.EndTurn();

           
               
           


        }
        if (moving)
        {
            lightM.DownLight();
            lightM.UpLight();
        }


	}
    void CalculatePath()//where the npc is going to move to
    {
        TileScript targetTile = GetTargetTile(target);//function from tile script
        FindPath(targetTile);
    }
    void FindNearestTarget ()//this is how the ai function ( the brain ) 
    //todo:make more advanced
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");//find all game objects that have the tage "player" (crates an array of all player objects)

        GameObject nearest = null;
        float distance = Mathf.Infinity;//distance is set infinity so everything else is smallr (you'll see)

        foreach (GameObject obj in targets)//for each object in our array
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);//we calculate the distance using vector 3... 
            //...distance (from the position of "ths unit" (whatever the unit is) ((transform.position)), to the position of the object ((obj.transform.position)) in the target array)

            if (d < distance)//if d is less than the distance...
            {
                distance = d;//the object less than infinity is the closest so we set the float distance equal to d wich is the... 
                //...distance from the position of ths unit to the position of the object in the target array
                nearest = obj;
            }
        }//end of funcion NOTE: a quick and dirty way to finding the closest "something" based on distance

        target = nearest;
    }



}
