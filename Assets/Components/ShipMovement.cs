using UnityEngine;
using System.Collections.Generic;

public class ShipMovement : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        print(Input.GetAxis("MouseX").ToString());
    }
}