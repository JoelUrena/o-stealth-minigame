using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;

    public float horizMove = 45;
    public float verticMove = 15;

    public void moveHorizontal(bool left)
    {
        float dir = 1;
        if (!left)
            dir *= -1;
        transform.RotateAround(target.position, Vector3.up, horizMove * dir );
    }

    public void moveVertical(bool up)
    {
        float dir = 1;
        if (!up)
            dir *= -1;
        transform.RotateAround(target.position, transform.TransformDirection(Vector3.right), verticMove * dir);
    }

}