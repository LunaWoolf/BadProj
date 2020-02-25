using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This state class is derived from BlobState.
 * In this state, the blob shrinks to zero size and is destroyed.*/
public class BlobStateShrinking : BlobState
{
    // Time to destruction.
    private const float easeOutTime = 2.0f;
    private float elapsedTime;

    // Store initial scale.
    private Vector3 initialScale;

    public BlobStateShrinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run() // Overriden from base class.
    {
        elapsedTime -= Time.deltaTime;

        // Use an easing effect to give interesting shrinking effect.
        float scale = QuinticEaseOut(elapsedTime)/ easeOutTime;
        blob.transform.localScale = new Vector3(initialScale.x * scale, initialScale.y * scale, initialScale.z  * scale);

        if (elapsedTime < 0.0f)
        {
            blob.Kill(); // Destroy blob
        }
    }

    public override void Enter() // Overriden from base class.
    {
        elapsedTime = easeOutTime;
        initialScale = blob.transform.localScale;
    }

    /* 
    * Easing function taken from Tween.js - Licensed under the MIT license
    * at https://github.com/sole/tween.js
    */
    public float QuinticEaseOut(float k)
    {
        return 1f + ((k -= 1f) * k * k * k * k);
    }
}
