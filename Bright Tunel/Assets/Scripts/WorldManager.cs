using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour
{
	public int amount = 5;
	public Vector3 direction = new Vector3(0, 0, 0);
	public Vector3 generationStart = new Vector3(0, 0, 0);
	public Vector3 generationOffset = new Vector3(0,-1, 0);

	private WorldGenerator m_WorldGenerator;
	private List<GameObject> m_WorldObjects = new List<GameObject>();



	void Start()
	{
		// fetch data
		m_WorldGenerator = GetComponent<WorldGenerator> ();
		// generate new world
		List<GameObject> list = m_WorldGenerator.Generate(generationStart, generationOffset, amount);
		// update generated objects
		AppendObjects(list);
	}

	void Update ()
	{
		Vector3 dir = direction * Time.deltaTime;

		foreach (GameObject obj in m_WorldObjects)
		{
			if (obj.transform.position.y < 2)
			{
				obj.transform.Translate(dir);
			}
			else
			{
				Destroy(obj);
				m_WorldObjects.Remove(obj);
			}
		}
	}

	public void AppendObjects(GameObject obj) { m_WorldObjects.Add (obj); }
	public void AppendObjects(List<GameObject> list) { foreach (GameObject obj in list) AppendObjects (obj); }
}