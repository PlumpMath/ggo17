using System.Collections.Generic;
using UnityEngine;

public class PoolingFactory : MonoBehaviour
{

	[SerializeField]
	private Transform poolPrefab;
	
	private static Transform factory;
	private static Transform poolPrefabStatic;
	private static Dictionary<Transform, List<Transform>> objectsByPrefab = new Dictionary<Transform, List<Transform>>();
	private static Dictionary<Transform, Transform> pools = new Dictionary<Transform, Transform>();
	private static Dictionary<Transform, int> lastObjectReused = new Dictionary<Transform, int>();

	public static Transform SpawnOrRecycle(Transform prefab, Vector3 position)
	{
		if (!objectsByPrefab.ContainsKey(prefab))
		{
			AddNewPrefab(prefab);
		}

		var recyclee = GetUnusedObject(prefab);
		if (recyclee == null)
		{
			DoubleObjects(prefab);
			recyclee = GetUnusedObject(prefab);
		}

		ActivateObjectAndChildren(recyclee); // Activate first, then recycle, else you don't find child IRecyclables
		RecycleObject(recyclee);
		recyclee.position = position;
		return recyclee;
	}

	private static void ActivateObjectAndChildren(Transform recyclee)
	{
		recyclee.gameObject.SetActive(true);

		for (var i = 0; i < recyclee.gameObject.transform.childCount; i++)
		{
			recyclee.gameObject.transform.GetChild(i).gameObject.SetActive(true);
		}
	}

	private static void AddNewPrefab(Transform prefab)
	{
		objectsByPrefab.Add(prefab, new List<Transform>());
		lastObjectReused.Add(prefab, -1);

		if (factory != null && poolPrefabStatic != null)
		{
			var pool = Instantiate(poolPrefabStatic, Vector3.zero, Quaternion.identity, factory);
			pool.name = prefab.name + "s (0)";
			pools.Add(prefab, pool);
		}
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
	
	private static void DoubleObjects(Transform prefab)
	{
		var objects = objectsByPrefab[prefab];
		var initialCount = objects.Count;
		var targetCount = initialCount * 2 + 1;
		var parent = pools[prefab];
		
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

		foreach (var recyclable in recyclee.GetComponentsInChildren<IRecyclable>())
		{
			recyclable.Recycle();
		}
	}

	void Start ()
	{
		factory = transform;
		poolPrefabStatic = poolPrefab;
	}
	
	void Update () {
	}
}