using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugger : MonoBehaviour
{
    BoxCollider playerCollider;

    void Start()
    {
        playerCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            //Toggle collision
            playerCollider.enabled = !playerCollider.enabled;
            if(playerCollider.enabled){
                Debug.Log("Player Collider Enabled");
            }
            else
            {
                Debug.Log("Player Collider Disabled");
            }
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Loading Next Level");
            CollisionHandler.LoadRelativeLevel(1);
        }
    }
}
