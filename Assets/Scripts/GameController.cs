﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Main game controller class  - controls game logic.
 */
public class GameController : MonoBehaviour
{
    // These links must be set in the Inspector.
    public Blob blobPrefab; 
    public Text scoreText; // Link to UI element to display score.

    // Control where the blobs spawn.
    public float spawnInterval = 1.0f;
    public float spawnDistanceMax = 10.0f;
    public float blobStartY = 1.0f;

    // How often do blobs spawn?
    private float spawnTimer;

    // Score is added on destroying blobs
    private int _score; // baking variabl

    // List of all the blobs in the game.
    private List<Blob> blobList = new List<Blob>();

    void Start()
    {
        
    }

    
    void Update()
    {
        // On pressing space bar, remove the the half of the list that is highest up in the y-axis.
        if (Input.GetKeyDown("space"))
        {
            RemoveHighestBlobs();
        }


        // Spawn blobs on timer and add to master list.
        spawnTimer += Time.deltaTime;

        while (spawnTimer > spawnInterval)
        {
            spawnTimer -= spawnInterval;
            Vector3 startPosOffset = new Vector3(Random.Range(-spawnDistanceMax, spawnDistanceMax),
                                                 blobStartY,
                                                 Random.Range(-spawnDistanceMax, spawnDistanceMax));
            // Instantiate with radom rotation
            Blob newBlob = Instantiate<Blob>(blobPrefab, transform.position + startPosOffset, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            newBlob.transform.parent = transform; // Set parent to be this gameObject so that the blobs can find the game controller.
            blobList.Add(newBlob);
        }
    }

    // public property of the score. Add and display score.
    public int Score 
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            scoreText.text = _score.ToString();
           
        }
    }

    // Remove blob from blob list.
    public void RemoveFromList(Blob blob)
    {
        Debug.Log(blobList.Remove(blob) ? "Blob removed from list" : "Blob error: not removed from list");
    }

    // Remove the blobs with the highest y values. 
    public void RemoveHighestBlobs()
    {
        // Selection sort the list of blobs by y
        for (int i = 0; i < blobList.Count; i++)
        {
            int curMinIndex = i;
            float curMin = blobList[i].transform.position.y;

            for (int j = i + 1; j < blobList.Count; j++)
            {
                if (curMin > blobList[j].transform.position.y)
                {
                    curMinIndex =j;
                    curMin = blobList[j].transform.position.y;
                }
            }

            //swap
            if (curMinIndex != i)
            {
                Blob temp = blobList[i];
                blobList[i] = blobList[curMinIndex];
                blobList[curMinIndex] = temp;
            }
        }

        // Remove the 50% of the list with the highest y value.
        int toKill = blobList.Count / 2;

        // Iterate backwards through the list to avoid invalidating index after removing blob.
        for (int i = blobList.Count - 1; i >= toKill; i--) 
        {
            blobList[i].Kill();
        }
        
    }


}
