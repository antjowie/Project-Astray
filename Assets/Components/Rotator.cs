using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float DeltaX = 90;
    [SerializeField] private float DeltaY = 45;
    [SerializeField] private float DeltaZ = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(
            DeltaX * Time.deltaTime,
            DeltaY * Time.deltaTime, 
            DeltaZ * Time.deltaTime);
    }
}
