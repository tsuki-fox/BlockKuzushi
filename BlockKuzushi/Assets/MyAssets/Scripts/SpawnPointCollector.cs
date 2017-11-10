using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
public static class IEnumerableExtensions
{
	/// <summary>
	/// 最小値を持つ要素を返します
	/// </summary>
	public static TSource FindMin<TSource, TResult>
	(
		this IEnumerable<TSource> self,
		Func<TSource, TResult> selector
	)
	{
		return self.First(c => selector(c).Equals(self.Min(selector)));
	}

	/// <summary>
	/// 最大値を持つ要素を返します
	/// </summary>
	public static TSource FindMax<TSource, TResult>
	(
		this IEnumerable<TSource> self,
		Func<TSource, TResult> selector
	)
	{
		return self.First(c => selector(c).Equals(self.Max(selector)));
	}
}

public class SpawnPointCollector : MonoBehaviour
{
	List<Transform> _points = new List<Transform>();

	/// <summary>
	/// シーンからスポーンポイントを収集する
	/// </summary>
	public void Collect()
	{
		_points = GameObject.FindGameObjectsWithTag("SpawnPoint")
			.Select(item => item.GetComponent<Transform>())
			.ToList();
	}

	/// <summary>
	/// ジェネレータにセットする
	/// </summary>
	public void SetToBlockGenerator()
	{
		var generator = FindObjectOfType<BlockGenerator>();
		generator.spawnPoints = _points;
	}

	private void Start()
	{
		var mesh = new Mesh();
		var collider = GetComponent<PolygonCollider2D>();

		MeshCreator.createSectorAndCollider(transform.position, 0f, 270f, 1f, 1.2f, 32, ref mesh, ref collider);

		var filter = GetComponent<MeshFilter>();
		filter.mesh = mesh;
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(SpawnPointCollector))]
	public class SpawnPointCollectorInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var param = target as SpawnPointCollector;

			if(GUILayout.Button("Set"))
			{
				param.Collect();
				param.SetToBlockGenerator();
			}
		}
	}
#endif
}
