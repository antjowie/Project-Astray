using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private bool randomizeRotation = false;

    public float deltaX = 90;
    public float deltaY = 45;
    public float deltaZ = 0;

    void Start()
    {
        if(randomizeRotation)
        {
            const float max = 180;
            deltaX = Random.Range(0, max);
            deltaY = Random.Range(0, max);
            deltaZ = Random.Range(0, max);
        }
    }

    void Update()
    {
        transform.Rotate(
            deltaX * Time.deltaTime,
            deltaY * Time.deltaTime, 
            deltaZ * Time.deltaTime);
    }
}
