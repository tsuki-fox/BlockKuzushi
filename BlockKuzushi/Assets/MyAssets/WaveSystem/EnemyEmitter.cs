using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TF;
using UniRx;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyEmitter : MonoBehaviour
{
	[SerializeField]
	GameObject _warningIcon;
	[SerializeField]
	GameObject _emitSource;

	public void EmitRandom(Vector2 minPos,Vector2 maxPos)
	{
		var pos = RandomEx.RangeVector2(minPos, maxPos);
		var warning = ObjectPool.Borrow(_warningIcon);
		warning.transform.position = pos;
		Observable.Timer(System.TimeSpan.FromSeconds(1)).Subscribe(t =>
		{
			var enemy = ObjectPool.Borrow(_emitSource);
			enemy.transform.position = pos;
		}).AddTo(this);
	}

	[CustomEditor(typeof(EnemyEmitter))]
	public class EnemyEmitterInspector:Editor
	{
		public override void OnInspectorGUI()
		{
			var self = target as EnemyEmitter;

			if (GUILayout.Button("Emit Random"))
				self.EmitRandom(new Vector2(-12, 10), new Vector2(12, -10));
			base.OnInspectorGUI();
		}
	}
}
