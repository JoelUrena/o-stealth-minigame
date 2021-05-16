using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcelotFunction : MonoBehaviour {
    Rigidbody rigidBody;
    public float speed;//we initialized it with 4 (that what the equals 4 means) ((that means in the inspector meanu, speed is already 4))
   public Vector3 lookPos;
    public GameObject prefab;
    public Vector3 prefabMove;
    public bool gunAble = false;
    public GameObject gunred;
    public Material red;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        speed = 0.5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");//create a float outta thin air
        float vertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(horizontal, 0, vertical);//y would be for jumping wich is why the z axis is used (We create a new vector 3 named movement outta thin air and make it equal to the floats we made previously outta thin air)
        //rigidBody.AddForce(movement * speed / Time.deltaTime);//we add force to the vector 3 movement and multiply it by speed which is divided byt time.delta time ( to keep it consistent with framerate)

      
    }
    void Update()
    {
        //Raycasting and Mouse Movement"
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//this creates a nw ray outta thin air and makes it equal to screen point to ray (input. Mouse position) its pretty neat in my opinion!
        RaycastHit hit; //creates a new raycast named hit outta nowhere ( thats all)

        if (Physics.Raycast(ray, out hit, 100))//(origin of mouse position, info from raycast direction, dont know what value is )
        {
            lookPos = hit.point;//whenever theres a raycast, we will update the look position
//            Debug.DrawLine(character.Position, hit.point);
        }

       

        Vector3 lookdir = lookPos - transform.position;//the vector3, llok dir, that we created is equal to the vector 3 look pos
        lookdir.y = 0;//used so he doesnt look up
        transform.LookAt(transform.position + lookdir, Vector3.up);

        //Movement of Player
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {    
            speed = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("down");
            speed = 0.3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Debug.Log("up");
            speed = 0.5f;
        }

        //prefab    
       
        if (gunAble == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(prefab, transform.position, transform.rotation);
            }
            gunred.GetComponent<MeshRenderer>().material = red;
        }

    }
    public void OnClick()
    {
        gunAble = true;
    }

}
