using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
	//<Prefab名,GameObject>
	static Dictionary<string, List<GameObject>> _reserved = new Dictionary<string, List<GameObject>>();

	static GameObject _poolRoot = null;
	static Dictionary<string, GameObject> _groupRoots = new Dictionary<string, GameObject>();

	/// <summary>初期化関数</summary>
	static void Initialize()
	{
		_poolRoot = new GameObject("ObjectPool");
		GameObject.DontDestroyOnLoad(_poolRoot);
	}
	/// <summary>初期化チェック関数</summary>
	static void CheckInitialize()
	{
		if (_poolRoot == null)
			Initialize();
	}

	static GameObject CreateInstance(GameObject source)
	{
		var obj = GameObject.Instantiate(source);
		obj.name = source.name + _reserved[source.name].Count;
		return obj;
	}

	static void AddObjectGroup(string key)
	{
		var objRoot = new GameObject(key);
		objRoot.transform.SetParent(_poolRoot.transform);
		_groupRoots.Add(key, objRoot);

		_reserved.Add(key, new List<GameObject>());
	}

	public static void Reserve(GameObject prefab, int reserveCount)
	{
		CheckInitialize();

		if (!_groupRoots.ContainsKey(prefab.name))
			AddObjectGroup(prefab.name);

		for(int i=0;i<reserveCount;i++)
		{
			var obj = CreateInstance(prefab);
			obj.SetActive(false);
			obj.transform.SetParent(_groupRoots[prefab.name].transform);
			_reserved[prefab.name].Add(obj);
		}
	}

	public static GameObject Borrow(GameObject prefab)
	{
		CheckInitialize();

		if (!_reserved.ContainsKey(prefab.name))
		{
			Debug.LogWarningFormat("Object not reserved! name:{0}", prefab.name);
			return null;
		}

		//待機中のオブジェクトを探して返す
		foreach (var item in _reserved[prefab.name])
		{
			if (!item.activeSelf)
			{
				item.SetActive(true);
				return item;
			}
		}

		//全て使用中ならば新たに確保する
		Reserve(prefab, 1);
		return Borrow(prefab);
	}

	public static void Repay(GameObject obj)
	{
		obj.SetActive(false);
	}
}
