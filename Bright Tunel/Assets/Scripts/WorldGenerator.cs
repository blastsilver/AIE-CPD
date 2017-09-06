using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
	public GameObject prefabBase = null;

	public List<GameObject> Generate(Vector3 start, Vector3 offset, int amount)
	{
		// required variables
		Vector3 position = start;
		List<GameObject> list = new List<GameObject>();
		// iterate through spawn amount
		for (int i = 0; i < amount; i++)
		{
			// instantiate object & append to list
			list.Add(GameObject.Instantiate(prefabBase, position, new Quaternion(), transform));
			// update position
			position += offset;
		}
		// return object list
		return list;
	}
}