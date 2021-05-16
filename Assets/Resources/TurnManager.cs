using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    static Queue<string> turnKey = new Queue<string>();//its the key for who evers turn it is
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();//its a queue for thecurrent team who's turn is is.

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)//if no one took a turn,
        {
            InitTeamTurnQueue();//its time to initalize team turn queue
        }

    }

    static void InitTeamTurnQueue()//all member functions  have to be static ( if you didnt notice) because it makes it easier o we dont have to get the turn manager (quick and dirty method)
    {
        List<TacticsMove> teamList = units[turnKey.Peek()];//turn key has the team thats is active; we are peeking into the queue tunKey to see whos next (get it...Queue?)

        foreach (TacticsMove unit in teamList)//we are grabing the tactics move list so we can add this to the team queue
        {
            turnTeam.Enqueue(unit);

        }
        StartTurn();
    }

    public static void StartTurn()//it public because we want units when their turn is over - we want them to end their own turn. They'll call the end turn funtion on themselves
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();//begin turn for the current team (BeginTurn function is in tactics move idky lol)
        }
    }
    public static void EndTurn()
    {
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();//the thing that grabs the tactics move ends the turn (this end turn is the other end turn located in tactics movement :p idky)

        if (turnTeam.Count > 0)//to check if anyone on the team ( who ever has the same matching tag) has left to go
        {
            StartTurn();
        }
        else //if everything is all good and set, (if we finished)
        {
            string team = turnKey.Dequeue();//we want to move to the next team, taking this team off the queue, now the que moves to the other team
            turnKey.Enqueue(team);//we the put that sam team that we just took off back into the queue, creating an infinite loop
            InitTeamTurnQueue();// the next team's turn is initiated
        }
    }

    public static void AddUnit(TacticsMove unit)//the units will add themselves instead of doing aa gameobject.findall(gameobjects) w/tag ("blahblahblah"0

    {
        //when a unit comes online, it's going to be responsible for adding itself
        List<TacticsMove> list; 

        if (!units.ContainsKey(unit.tag))//if the units "tag" has -->NOT<-- been added to dictionary ( forgot what dictionary does, but is probably self explanitory so im scared to ask someone)
        {
            list = new List<TacticsMove>();//create a new lsit 
            units[unit.tag] = list;//you add that tag to the llibrary, and assign to the list


            if (!turnKey.Contains(unit.tag))//if turnkey doesnt contain the unit tag(we look at the turn key for the tag )
            {
                turnKey.Enqueue(unit.tag);//if the tag is in there (which is probably not cuz if it is it'll duplicate - meaning same person will have two turns) THEN QUEUE THAT TAG TO THE BACK NO CHEATING

            }

            //in recap, we just added a new list, added it to the dictionary for the tag, weve added a "team", we then added that team to the turn que ALL INORDER FOR THE NPC TO MAKE ITS OWN TURN HUEHUEHUE HAHAHAHAHA dont judge me
            //but wait...theres more!

        }
        else //but what if it does successfuly find the tag in the dictionary?
        {
            list = units[unit.tag];//we grab the list for that tag and assign it
        }
        list.Add(unit);//we then add the unit that was passing as a parameter (tactics move unit) into the list of the dictionary
    }

}
