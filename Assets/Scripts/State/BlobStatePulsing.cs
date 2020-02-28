using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This state class is derived from BlobState.
 * In this state, the blob remains motionless and pulses between two colors.*/
public class BlobStatePulsing : BlobState
{
    // Colors
    private Color initialColor;
    private Color targetColor;
    // Cached
    private Renderer renderer;

    // Time until state change
    private const float minTime = 1.0f;
    private const float maxTime = 5.0f;
    private float elapsedTime;
    private float endTime;


    public BlobStatePulsing(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run() // Overriden from base class.
    {
        // Ping/pong lerp between colors.
        Color lerpedColor = Color.Lerp(initialColor, targetColor, Mathf.PingPong(Time.time, 1.0f));
        renderer.material.SetColor("_Color", lerpedColor);

        // Return to Moving state after time has elapsed.
        elapsedTime += Time.deltaTime;

        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStateBlinking(blob));
            //blob.ChangeState(new BlobStateMoving(blob));
        }

    }

    public override void Enter() // Overriden from base class.
    {
        base.Enter(); // Call base class.

        renderer = blob.GetComponent<Renderer>(); // Cache the renderer.
        initialColor = renderer.material.GetColor("_Color"); // Store the current color.
        targetColor = Random.ColorHSV(); // Get a random color.
        endTime = Random.Range(minTime, maxTime);
        blob.curstate = "puls";
    }
}
