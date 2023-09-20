using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] bool dynamicMovement = true;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 5f;
    
    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        applySinMovement();
    }

    void applySinMovement()
    {
        if(period <= Mathf.Epsilon) return;

        const float tau = Mathf.PI * 2;

        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles*tau);

        if(dynamicMovement) movementFactor = (rawSinWave + 1f)/2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
