using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove {
    public LightScript lightM;

	// Use this for initialization
	void Start () 
    {
        Init();
	}

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)//if its not the players turn (bool found in tactics move)
        {
            return;//dont execute the update function
        }
        if (!moving)//if we're not moving
        {

            FindSelectableTiles();//perform selectionof tiles where to move
            CheckMouse();
        }
        else
        {
            lightM.DownLight();
            lightM.UpLight();
            Move();//move

        }
	}
    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    TileScript t = hit.collider.GetComponent<TileScript>();

                    if (t.selectable)
                    {
                        //todo: mov target
                        MoveToTile(t);

                    }

                }
            }
        }
    }
}
