using System.Collections.Generic;
using UnityEngine;

public class PoolingFactory : MonoBehaviour
{

	private static Dictionary<Transform, List<Transform>> objectsByPrefab = new Dictionary<Transform, List<Transform>>();
	private static Dictionary<Transform, int> lastObjectReused = new Dictionary<Transform, int>();

	public static Transform SpawnOrRecycle(Transform prefab, Vector3 position)
	{
		if (!objectsByPrefab.ContainsKey(prefab))
		{
			objectsByPrefab.Add(prefab, new List<Transform>());
			lastObjectReused.Add(prefab, -1);
		}

		var recyclee = GetUnusedObject(prefab);
		if (recyclee == null)
		{
			DoubleObjects(prefab);
			recyclee = GetUnusedObject(prefab);
		}

		RecycleObject(recyclee);
		recyclee.position = position;
		recyclee.gameObject.SetActive(true);
		return recyclee;
	}

	private static Transform GetUnusedObject(Transform prefab)
	{
		var objects = objectsByPrefab[prefab];
		var lastIndex = lastObjectReused[prefab];

		lastIndex = objects.Count == 0 ? 0 : (lastIndex + 1) % objects.Count;

		if (lastIndex >= objects.Count)
		{
			return null;
		}

		lastObjectReused[prefab] = lastIndex;
		var recyclee = objects[lastIndex];
		return recyclee.gameObject.activeSelf ? null : recyclee;
	}
	
	private static void DoubleObjects(Transform prefab)
	{
		var objects = objectsByPrefab[prefab];
		var initialCount = objects.Count;
		var targetCount = initialCount * 2 + 1;
		
		for (var i = initialCount; i <= targetCount; i++)
		{
			var newRecyclee = Instantiate(prefab, new Vector3(100, 100, 0), Quaternion.identity);
			newRecyclee.gameObject.SetActive(false);
			objects.Add(newRecyclee);
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