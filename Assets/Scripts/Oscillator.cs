using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    [SerializeField] float movementOffset = 1f;

    Vector3 startingPosition;
    const float tau = Mathf.PI * 2;//~6.283

    // Start is called before the first frame update
    void Start()
    {
        this.startingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure period is never 0!
        if(period <= Mathf.Epsilon) return;
        
        //Continuously grows over time
        float cycles = Time.time / period;
        //Goes from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);
        //Goes from 0 to 1
        float movementFactor = (rawSinWave + movementOffset) / 2f;

        //Move the object
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
