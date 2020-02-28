using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blobs are objects whose behaviour is controlled by a state machine. They are destroyed when the player clicks on them.
 * This class manages the blob state machine and provides a link to the master game controller,
   to allow the blob to interact with the game world.*/
public class Blob : MonoBehaviour
{
    private BlobState currentState; // Current blob state (unique to each blob)
    private GameController controller;  // Cached connection to game controller component
    public string curstate; // keep track of the current state
    private MeshRenderer mr;

    void Start()
    {
        ChangeState(new BlobStateMoving(this)); // Set initial state.
        controller = GetComponentInParent<GameController>();
        mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        currentState.Run(); // Run state update.
    }

    // Change state.
    public void ChangeState(BlobState newState)
    {
        if (currentState != null) currentState.Leave();
        currentState = newState;
        currentState.Enter();
    }

    // Change blobs to shrinking state when clicked.
    void OnMouseDown()
    {
        // if blobs is already in the shrinking state. It won't enter state again
        if (curstate != "shrink")
        {
            if (curstate == "blink")
            {
                controller.Score += 10;
            }
            else
            {
                controller.Score -= 5;
            }
            ChangeState(new BlobStateShrinking(this));
        }
           
    }

    // Destroy blob gameObject and remove it from master blob list.
    public void Kill()
    {
        controller.RemoveFromList(this);
        Destroy(gameObject);
    }

    //blinking state start.
    public void blinkstart()
    {
        StartCoroutine(blink());
    }

    //keep blinking every 0.5 second as long as the state is blinking state
    IEnumerator blink()
    {
        while (curstate == "blink")
        {
            mr.enabled = false;
            yield return new WaitForSeconds(0.5f);
            mr.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
      
    }
}
