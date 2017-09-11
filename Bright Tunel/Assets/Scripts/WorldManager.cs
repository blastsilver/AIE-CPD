using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour
{
	public bool _testSPAWN = false;
	public bool _testCLEANUP = false;
	public bool _testTRANSLATE = false;
	public GenerationSettings generationSettings = new GenerationSettings();


	private List<GameObject> m_WorldObjects = new List<GameObject>();

	void Start()
	{
		GenerateWorld (generationSettings);
	}

	void Update ()
	{
		if (_testSPAWN)
		{
			GenerateChunk (generationSettings);
			_testSPAWN = false;
		}
		if (_testCLEANUP)
		{
			for (int i = 0; i < m_WorldObjects.Count; i++)
			{
				if (m_WorldObjects[i].transform.position.y < -Camera.main.farClipPlane)
				{
					Destroy(m_WorldObjects[i]);
					m_WorldObjects.RemoveAt (i--);
				}
			}
			_testCLEANUP = false;
		}
		if (_testTRANSLATE)
		{
			foreach (GameObject obj in m_WorldObjects)
			{
				obj.transform.Translate (Vector3.down * generationSettings.offset);
			}
			_testTRANSLATE = false;
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