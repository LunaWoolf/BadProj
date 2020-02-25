using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Destroy blob on leaving arena
    void OnTriggerEnter(UnityEngine.Collider other)
    {
        Blob collideBlob = other.gameObject.GetComponent<Blob>();

        if (collideBlob != null)
        {
            collideBlob.Kill();
        }
    }
}
