using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PoolingFactory : MonoBehaviour
{

	private static Dictionary<Transform, List<Transform>> objectsByPrefab = new Dictionary<Transform, List<Transform>>();
	private static Dictionary<Transform, int> lastObjectReused = new Dictionary<Transform, int>();

	public static Transform SpawnOrRecycle(Transform prefab, Vector3 position)
	{
		return SpawnOrRecycle(prefab, position, null);
	}

	public static Transform SpawnOrRecycle(Transform prefab, Vector3 position, Transform parent)
	{
		if (!objectsByPrefab.ContainsKey(prefab))
		{
			AddNewPrefab(prefab);
		}

		var recyclee = GetUnusedObject(prefab);
		if (recyclee == null)
		{
			DoubleObjects(prefab, parent);
			recyclee = GetUnusedObject(prefab);
		}

		RecycleObject(recyclee);
		recyclee.position = position;
		recyclee.gameObject.SetActive(true);
		return recyclee;
	}

	private static void AddNewPrefab(Transform prefab)
	{
		objectsByPrefab.Add(prefab, new List<Transform>());
		lastObjectReused.Add(prefab, -1);
	}
	
	private static Transform GetUnusedObject(Transform prefab)
	{
		var objects = objectsByPrefab[prefab];
		
		if (objects.Count == 0) return null;
		
		var startIndex = (lastObjectReused[prefab] + 1) % objects.Count;
		var index = startIndex;
		Transform recyclee = null;

		do
		{
			if (!objects[index].gameObject.activeSelf && index < objects.Count)
			{
				recyclee = objects[index];
				lastObjectReused[prefab] = index;
			}
			
			index = (index + 1) % objects.Count;
		} while (recyclee == null && index != startIndex);

		return recyclee;
	}
	
	private static void DoubleObjects(Transform prefab, Transform parent)
	{
		var objects = objectsByPrefab[prefab];
		var initialCount = objects.Count;
		var targetCount = initialCount * 2 + 1;
		
		for (var i = initialCount; i < targetCount; i++)
		{
			Transform newRecyclee;
			if (parent != null)
			{
				newRecyclee = Instantiate(prefab, new Vector3(100, 100, 0), Quaternion.identity, parent);
			}
			else
			{
				newRecyclee = Instantiate(prefab, new Vector3(100, 100, 0), Quaternion.identity);
			}
			
			newRecyclee.gameObject.SetActive(false);
			objects.Add(newRecyclee);
		}

		if (parent != null)
		{
			var baseName = parent.gameObject.name.Split(' ')[0];
			var newName = baseName + " (" + objects.Count + ")";
			parent.gameObject.name = newName;
		}
	}
	
	private static void RecycleObject(Transform recyclee)
	{
		foreach (var recyclable in recyclee.GetComponents<IRecyclable>())
		{
			recyclable.Recycle();
		}
	}

	void Start () {
	}
	
	void Update () {
	}
}