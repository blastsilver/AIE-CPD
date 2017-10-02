using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : UIActionListener
{
    public GameObject player = null;

	public WorldSettings worldSettings = new WorldSettings();

    private void Start()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
    }

    void Update ()
	{
		// update world
		UpdateWorld ();
    }

	void CreateWorld() { CreateWorld (worldSettings); }
	void DeleteWorld() { DeleteWorld (worldSettings); }
	void UpdateWorld() { UpdateWorld (worldSettings); }
	void CreateWorldChunk() { CreateWorldChunk (worldSettings.chunkSettings); }
	void CreateWorldChunks() { CreateWorldChunks (worldSettings.chunkSettings); }
	void DeleteWorldChunks() { DeleteWorldChunks (worldSettings.chunkSettings); }
	void UpdateWorldChunks() { UpdateWorldChunks (worldSettings.chunkSettings); }

	public void CreateWorld(WorldSettings settings)
	{
		// create new chunks
		CreateWorldChunks (settings.chunkSettings);
	}

	public void DeleteWorld(WorldSettings settings)
	{
		// delete chunks
		DeleteWorldChunks(settings.chunkSettings);
        //deletes player
        Destroy(player);
        player = null;
	}

	public void UpdateWorld(WorldSettings settings)
	{
		// check if paused
		if (settings.paused != true)
		{
			// update chunks
			UpdateWorldChunks (settings.chunkSettings);
		}
	}

	public void CreateWorldChunk(WorldChunkSettings settings)
	{
		// initialize variables
		int index = settings.chunks.Count;
		Vector3 position = new Vector3(0, 0, 0);
		if (settings.chunks.Count > 0) position = settings.chunks [settings.chunks.Count - 1].transform.position;
		// instantiate new object
		settings.chunks.Add(Instantiate(settings.prefabs[0], position + Vector3.up * settings.offset, new Quaternion(), settings.container));
    }

	public void DeleteWorldChunk(WorldChunkSettings settings, int index)
	{
		// check index range
		if (index > -1)
		{
			// destroy gameobject
			Destroy (settings.chunks [index]);
			// remove node
			settings.chunks.RemoveAt (index);
		}
	}

	public void DeleteWorldChunk(WorldChunkSettings settings, GameObject obj)
	{
		// check if null
		if (obj != null)
		{
			// destroy gameobject
			Destroy (obj);
			// remove node
			settings.chunks.Remove(obj);
		}
	}

	public void DeleteWorldChunks(WorldChunkSettings settings)
	{
		// while not empty
		while (settings.chunks.Count != 0)
		{
			// destroy gameobject
			Destroy (settings.chunks [0]);
			// delete node
			settings.chunks.RemoveAt (0);
		}
	}

	public void CreateWorldChunks(WorldChunkSettings settings)
	{
		// iterate through generation amount
		for (int i = 0; i < settings.amount; i++)
		{
			// create new chunk
			CreateWorldChunk (settings);
		}
	}

	public void UpdateWorldChunks(WorldChunkSettings settings)
	{
		// iterate through chunks
		foreach (GameObject obj in settings.chunks)
		{
			// update chunk transform
			obj.transform.Translate (Vector3.down * settings.offset * settings.speed * Time.deltaTime);
		}

		while (settings.chunks[settings.chunks.Count - 1].transform.position.y < Camera.main.transform.position.y)
		{
			// iterate through chunks
			for (int i = 0; i < settings.chunks.Count; i++)
			{
				// check if out of range
				if (settings.chunks [i].transform.position.y < -Camera.main.farClipPlane)
				{
					// delete chunk
					DeleteWorldChunk(settings, settings.chunks[i--]);
				}
			}
			CreateWorldChunk (settings);
		}
	}

    private void SpawnPlayer()
    {
        //get the first object on the list wich is the bottom most one
        GameObject holder = worldSettings.chunkSettings.chunks[0];
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

        player = Instantiate(worldSettings.playerSettings.player, new Vector3(0, 0, 0), Quaternion.identity);

        //assign the players start point to the first object in the list of available points
        //dose not matter wich one we start off with
        player.GetComponent<JumpTo>().StartPoint = transformHolder[0];
    }

    override public void HandleEvent(UIActionEvent e)
	{
		switch (e)
		{
		case UIActionEvent.GAME_PAUSE:
			worldSettings.paused = true;
			break;
		case UIActionEvent.GAME_EXIT:
		case UIActionEvent.GAME_BACK:
		case UIActionEvent.GAME_MENU:
			worldSettings.chunkSettings.speed = 0;
			worldSettings.paused = true;
			DeleteWorld ();
			break;
		case UIActionEvent.GAME_FINISH:
			worldSettings.paused = true;
			break;
		case UIActionEvent.GAME_START:
		case UIActionEvent.GAME_RESTART:
			DeleteWorld ();
			CreateWorld ();
			worldSettings.paused = false;
			worldSettings.chunkSettings.speed = 0.5f;
            SpawnPlayer();
			break;
		case UIActionEvent.GAME_CONTINUE:
			worldSettings.paused = false;
			break;
		}
	}

	[System.Serializable]
	public class WorldSettings
	{
		public bool paused = true;
		public WorldChunkSettings chunkSettings;
		public WorldPlayerSettings playerSettings;
	}

	[System.Serializable]
	public class WorldChunkSettings
	{
		public int amount = 5;
		public float offset = 1;
		[Range(0.0f, 5.0f)] public float speed = 0;
		public Transform container = null;
		[HideInInspector]
		public List<GameObject> chunks = new List<GameObject>();
		public List<GameObject> prefabs = new List<GameObject>();
	}

	[System.Serializable]
	public class WorldPlayerSettings
	{
		public bool isPrefab = false;
		public GameObject player = null;
	}
}