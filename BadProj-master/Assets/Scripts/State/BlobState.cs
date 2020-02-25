using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Abstract base class for State machine.
 * Abstract classes cannot be instantiated with new.*/
public abstract class BlobState
{
    protected Blob blob; // The manager that contains the state machine.

    public abstract void Run(); // This is abstract so it MUST be implemented in derived classes.
    public virtual void Enter() { } // Virtual so can be overriden in derived classes.
    public virtual void Leave() { }

    public BlobState(Blob theBlob) // Constructor that takes an argument.
    {
        blob = theBlob;
    }

}
