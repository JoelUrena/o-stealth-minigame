using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Script : MonoBehaviour
{
    [MenuItem("Tools/Assign Tile Material")]//puts it on the menu (tools category)
    public static void AssignTileMaterial()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Tile");
        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = material;
        }
    }
    [MenuItem("Tools/Assign Tile Script")]
    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject t in tiles)
        {
            t.AddComponent<TileScript>();//this line in sequence makes aevey object with the tage "tile" obtail tile scripts in the inspector menu //fast way to addd stuff on a bunch of game objects
        }
    }
}
