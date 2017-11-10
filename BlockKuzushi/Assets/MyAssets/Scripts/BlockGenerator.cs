using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BlockGenerator : MonoBehaviour
{
	[SerializeField]
	GameObject _source;

	[SerializeField]
	List<Transform> _spawnPoints;
	public List<Transform> spawnPoints
	{
		get { return _spawnPoints; }
		set { _spawnPoints = value; }
	}

	[SerializeField]
	float _interval;

	public void Generate()
	{
		foreach (var item in _spawnPoints)
		{
			var obj = Instantiate(_source);

			obj.transform.position = item.position;
		}
	}

	public void GraduallyGenerate(float interval)
	{
		int cnt = 0;
		foreach(var item in _spawnPoints)
		{
			Observable.Timer(TimeSpan.FromMilliseconds(cnt * interval)).Subscribe(e =>
			{
				var obj = Instantiate(_source);
				obj.transform.position = item.position;
			}).AddTo(this);
			cnt++;
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(BlockGenerator))]
	public class BlockGeneratorInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var param = target as BlockGenerator;

			if(GUILayout.Button("Generate"))
			{
				param.Generate();
			}
			if(GUILayout.Button("Generate2"))
			{
				param.GraduallyGenerate(param._interval);
			}
		}
	}
#endif
}
