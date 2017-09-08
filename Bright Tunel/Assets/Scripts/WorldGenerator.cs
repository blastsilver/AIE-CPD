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
            // spawn & append object to list
            list.Add(Spawn(position));
			// update position
			position += offset;
		}
		// return object list
		return list;
	}

    public GameObject Spawn(Vector3 position)
    {
        // instantiate new object
        return Instantiate(prefabBase, position, new Quaternion(), transform);
    }
}