using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//goto public void move in order to move end turn to combat
public class TacticsMove : MonoBehaviour {

    public bool turn = false;

    List<TileScript> selectableTiles = new List<TileScript>();
    GameObject[] tiles; 

    Stack<TileScript> path = new Stack<TileScript>();
    TileScript currentTile;

    public bool moving = false;
    public int move = 5;
    public float jumpHeight = 2;
    public float moveSpeed = 2;//how fast the unit moves through the tiles
    public float jumpVelocity = 4.5f;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 1;

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    public TileScript actualTargetTile;

    //this is the light script and it's intended to move every turn
  //  public LightScript lightM;

    // Use this for initialization
    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;

        TurnManager.AddUnit(this);//were adding he current unit to the turn manager
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public TileScript GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        TileScript tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)) 
        {
            tile = hit.collider.GetComponent<TileScript>();

        }
        return tile;
    }
    public void ComputeAdjacencyList(float jumpHeight, TileScript target)
    {
        //tiles = GameObject.FindGameObjectsWithTag("Tile"); if you add or delete tile (new map)

        foreach (GameObject tile in tiles)
        {
            TileScript t = tile.GetComponent<TileScript>();//
            t.FindNeighbors(jumpHeight, target);

        }

    }
    public void FindSelectableTiles ()
    {
        ComputeAdjacencyList(jumpHeight, null);
        GetCurrentTile();

        Queue<TileScript> process = new Queue<TileScript>();

        process.Enqueue(currentTile);
        currentTile.visited = true; 
        //currentTile.parent = ?? leave as null 

        while (process.Count >0)
        {
            TileScript t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move)
            {
                
            
                foreach (TileScript tile in t.adjacencyList)
                {
                  if (!tile.visited)
                   {  
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }
    public void MoveToTile(TileScript tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        TileScript next = tile;
        while (next !=null)
        {
            path.Push(next);
            next = next.parent;
        }

    }

    public void Move()//to move the unit one tile to the next
    {
         if (path.Count > 0)
         {
            

                TileScript t = path.Peek();
                Vector3 target = t.transform.position;

                //calculate the units position
                target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

                if (Vector3.Distance(transform.position, target) >= 0.05f) 
                {
                    bool jump = transform.position.y != target.y;

                    if (jump)
                        {
                            Jump(target);
                        }

                    else
                        {
                    
                
                            CalculateHeading(target);
                            SetHorizontalVelocity();

                        
                        }
                    //locomotion (where we'd add animation)
                    transform.forward = heading;//going to fce the direction that we are going to go
                    transform.position += velocity * Time.deltaTime;

                }
                else
                {
                   //tile center reached

                    transform.position = target;
                    path.Pop();
                }
        }
        else 
        {

                RemoveSelectableTiles();
                moving = false;
            //so the light moves before the end of turn
            //lightM.DownLight();
           // lightM.UpLight();
            TurnManager.EndTurn();//initiates end turn from turn manager script //basicaly when moving is false, turn is over. This is done because we still havent added combat 
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (TileScript tile in selectableTiles)
        {
            tile.Reset();//resets all the tiles
        }
        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity ()
    {
        velocity = heading * moveSpeed;//heading is direction we are moving, so multiply it by move speed to move and set the velocity to that

    }
    void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump ( Vector3 target)
        {
            float targetY = target.y;
            target.y = transform.position.y;

            CalculateHeading(target);
            
            if (transform.position.y > targetY)
            {
                    fallingDown = false;
                    jumpingUp = false;
                    movingEdge = true;

                    jumpTarget = transform.position + (target - transform.position) / 2.0f;//finds the halfway position between the target and the player 

            }
            else
            {
                    fallingDown = false;
                    jumpingUp = true;
                    movingEdge = false;
            //23 minute mark if he did something you ddint notice

                    velocity = heading * moveSpeed / 3.0f;

                    float difference = targetY - transform.position.y;

                    velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
            }

        }
    void FallDownward(Vector3 target)
        {
        velocity += Physics.gravity * Time.deltaTime;

            if (transform.position.y <= target.y)
            {
                fallingDown = false;
                Vector3 p = transform.position;
                p.y = target.y;
                transform.position = p;

                velocity = new Vector3();
            }
        }
    void JumpUpward (Vector3 target)
        {
            velocity += Physics.gravity * Time.deltaTime;
            
        if (transform.position.y > target.y)
            {
                jumpingUp = false;
                fallingDown = true;
            }

        }
    void MoveToEdge ()
    {
        if (Vector3.Distance (transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 3.0f;
            velocity.y = 1.5f;

        }
    }

    protected TileScript FindLowestF(List<TileScript> list)
    {
        TileScript lowest = list[0];

        foreach (TileScript t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;//t becomes the next lowset
            }
        }

        list.Remove(lowest);//once we find it we remove it because we pull it off from the open list and add it to the closed list 

        return lowest;//then we process this one next
    }

    protected TileScript FindEndTile(TileScript t)
    {
        Stack<TileScript> tempPath = new Stack<TileScript>();

        TileScript next = t.parent;//unlike the player, the npc doesn't want to stad on the target, but next to them
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= move)
        {
            return t.parent;

        }

        TileScript endTile = null;
        for (int i = 0; i <= move; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

   protected void FindPath(TileScript target)//its protected or public so it could be accesable to NPCMove.cs
    {
        ComputeAdjacencyList(jumpHeight, target);//compute the red square to move and the jump height
        GetCurrentTile();


        List<TileScript> openList = new List<TileScript>();//for any tile that has not been processed
        List<TileScript> closedList = new List<TileScript>();//everytile that has been processed
        //we are not done unit the target tile has been added to the crosss list 
        //when the target tile has been added to the close list, we have found the best target tile

        openList.Add(currentTile);//we add the current tile to the open list because thats the fist tile we are going to process
        //currentTile.parent = ??
        //in the beggining, g is already zero because the current tile is zero, becuase the curent tile is the start tile
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);//h is the distance from the... 
        //...current tile's position to the target tile ( i know i explained this in tile script when we set up the float, but we had to actualy program h to function how we explained it)
        currentTile.f = currentTile.h;

        while (openList.Count > 0)//if we hit zero before finding the target, then, theres no path
        {
            TileScript t = FindLowestF(openList);//we need to find the one with the lowest f cost ( g + h)
            //the leat amount of distance to the start + the distance to the end (keep in mind)

            closedList.Add(t);//we add "t" to the close list AND NEVER PROCCESS IT AGAIN

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (TileScript tile in t.adjacencyList)//NOTE: THESE ARE THE THREE STEPS TO A*
            {
                if (closedList.Contains(tile))//if the tile that we want to process is in the close list,
                {
                    //do nothing, already processed
                }
                else if (openList.Contains(tile))//else if the tile that we want to process is in the open list,
                {
                    //todo find a quicker way to get to that tile


                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)//if temp g is lest than the current tile's g,
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else //if its not in the open list, or closed list
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);///estimated distance to the end 
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);//by sticking it in the open list, it can come through again to be processed
                }
            }
        }
        //todo - what do you do if there is no path to the target tile?//what if another enemy is on the tile?
        //find the closest open tile to that area
        Debug.Log("path Not found");
    }

    public void BeginTurn()
    {
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }



}
