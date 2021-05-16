using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public bool walkable = true;
    public bool current = false;
    public bool target = false;//where we ae moving to
    public bool selectable = false;

    public List<TileScript> adjacencyList = new List<TileScript>();


    //neededd BFS (Breath First Search)

    public bool visited = false;
    public TileScript parent = null;
    public int distance = 0;

    //For A*
    public float f = 0;// f is g + h 
    public float g = 0;//g is the cost from the parent to the current tile
    public float h = 0;//h is the cost from the proccesed tile to the destination
    //the cost from the parent to the current tile, g, plus the cost from the proccesed tile to the destination, h, = f (yay for algebra II)
    //used for finding the best path in a sort amount of time
    //the leat amount of distance to the start + the distance to the end (keep in mind) (h + g = f)


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;

        }
        else 
        {
            GetComponent<Renderer>().material.color = Color.white;//if nothing is selectabl, then tyurn target back to white

        }
	}
    public void Reset()
    {
        adjacencyList.Clear();

        //walkable = true;

        current = false;
        target = false;//where we ae moving to
        selectable = false;


        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;//all of em == 0 CIRCLE OF LIFE, NOTHIN PERSONAL KID DONT DO DRUGS AND DIVIDE BY ZERO
    }
    public void FindNeighbors(float jumpHeight, TileScript target)
    {
        Reset();
        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);


    }
    public void CheckTile(Vector3 direction, float jumpHeight, TileScript target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight ) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            TileScript tile = item.GetComponent<TileScript>();
            if (tile != null && walkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) ||(tile == target))//|| is NOT []
                    //this raycas is making sure nothing is ontop of the tile, howver, we waant it to include the tile (tile == targett)
                {
                    adjacencyList.Add(tile);

                }
            }
        }
    }
}
