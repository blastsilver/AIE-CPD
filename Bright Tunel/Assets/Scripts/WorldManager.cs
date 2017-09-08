using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour
{
	public int amount = 5;
	public Vector3 direction = new Vector3(0, 0, 0);
	public Vector3 generationStart = new Vector3(0, 0, 0);
	public Vector3 generationOffset = new Vector3(0,-1, 0);

    GameObject m_WorldLastObject;
	private WorldGenerator m_WorldGenerator;
	private List<GameObject> m_WorldObjects;

	void Start()
	{
        // fetch data
        m_WorldObjects = new List<GameObject>();
        m_WorldGenerator = GetComponent<WorldGenerator> ();
		// generate new world
        EraseAllObjects();
		List<GameObject> list = m_WorldGenerator.Generate(generationStart, generationOffset, amount);
        // update generated objects
		AppendObjects(list);
	}

	void Update ()
	{
        Vector3 dir = direction * Time.deltaTime;

        for (int i = 0; i < m_WorldObjects.Count; i++)
        {
            GameObject obj = m_WorldObjects[i];
            if (obj.transform.position.y < 2)
            {
                obj.transform.Translate(dir);
            }
            else
            {
                Destroy(obj);
                m_WorldObjects.Remove(obj);
                AppendObjects(m_WorldGenerator.Spawn(m_WorldLastObject.transform.position + generationOffset - dir));
            }
        }
    }

	public void AppendObjects(GameObject obj) { m_WorldObjects.Add (obj); m_WorldLastObject = obj; }
	public void AppendObjects(List<GameObject> list) { foreach (GameObject obj in list) AppendObjects (obj); }
    public void EraseAllObjects() { foreach (Transform child in transform) Destroy(child.gameObject); m_WorldObjects.Clear(); m_WorldLastObject = null; }
}