using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This state class is derived from BlobState.
 * In this state, the blob will blink until player click them.*/
public class BlobStateBlinking : BlobState
{
   
    private GameController controller;
    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    // Call the function that start blinking 
    public override void Enter() 
    {
        controller = blob.GetComponentInParent<GameController>();
        blob.curstate = "blink";
        blob.blinkstart();
    }

    public override void Run() { }


    // plus one score when leave the blinking state
    public override void Leave()
    {
        controller.Score += 1;

    }


}
