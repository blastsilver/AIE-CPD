﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour
{
	[Range(0.0f, 10.0f)] public float speed;

    private List<GameObject> m_WorldObjects = new List<GameObject>();

    public GameObject playerObject;

    public GenerationSettings generationSettings = new GenerationSettings();


	void Start()
	{
		GenerateWorld (generationSettings);

        //instantiate the player
        playerObject = Instantiate(generationSettings.prefabs[1], new Vector3(0, 0, 0), new Quaternion());

        //get the first object on the list wich is the bottom most one
        GameObject holder = m_WorldObjects[0];
        //create a list holding all the stating off jump points
        List<Transform> transformHolder = new List<Transform>();

        //get all the child objects in the chunk we are looking for
        foreach (Transform lv1 in holder.transform)
        {
            //get the child objects of those ones and fin the children there with the tag "JumpBlock" and add that
            //to our list
            foreach (Transform child in lv1)
            {
                if (child.tag == "JumpBlock")
                {
                    transformHolder.Add(child);
                }
            }
        }

        //assign the players start point to the first object in the list of available points
        //dose not matter wich one we start off with
        playerObject.GetComponent<JumpTo>().StartPoint = transformHolder[0];
	}

	void Update ()
	{
		foreach (GameObject obj in m_WorldObjects)
		{
			obj.transform.Translate (Vector3.down * generationSettings.offset * speed * Time.deltaTime);
		}

		while (m_WorldObjects [m_WorldObjects.Count - 1].transform.position.y < Camera.main.transform.position.y)
		{
			CleanupChunks();
			GenerateChunk(generationSettings);
		}
    }

	public void CleanupChunks()
	{
		// iterate through world objects
		for (int i = 0; i < m_WorldObjects.Count; i++)
		{
			// check if out of range
			if (m_WorldObjects[i].transform.position.y < -Camera.main.farClipPlane)
			{
				// delete game object
				Destroy(m_WorldObjects[i]);
				m_WorldObjects.RemoveAt (i--);
			}
		}
	}

	public void GenerateChunk(GenerationSettings settings)
	{
		// initialize variables
		Vector3 position = m_WorldObjects[m_WorldObjects.Count - 1].transform.position + Vector3.up * settings.offset;
		// instantiate new object
		m_WorldObjects.Add(Instantiate(settings.prefabs[0], position, new Quaternion(), settings.container));
	}

	public void GenerateWorld(GenerationSettings settings)
	{
		// initialize variables
		Vector3 position = new Vector3(0, 0, 0);
		Vector3 direction = new Vector3(0, settings.offset, 0);
		Quaternion rotation = new Quaternion(0, 0, 0, 1);
		// iterate through generation amount
		for (int i = 0; i < settings.amount; i++)
		{
			// instantiate new object
			m_WorldObjects.Add(Instantiate(settings.prefabs[0], position, rotation, settings.container));
			// update next object position
			position += direction;
		}
	}

	[System.Serializable]
	public class GenerationSettings
	{
		public int amount = 5;
		public float offset = 1;
		public Transform container;
		public List<GameObject> prefabs = new List<GameObject>();
	}
}