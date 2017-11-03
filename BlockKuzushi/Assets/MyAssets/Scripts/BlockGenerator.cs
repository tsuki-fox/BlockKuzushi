using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BlockGenerator : MonoBehaviour
{
	[SerializeField]
	GameObject _source;
	[SerializeField]
	Vector3 _pos;
	[SerializeField]
	float _initialAngle;
	[SerializeField]
	float _radius;
	[SerializeField]
	int count;
	[SerializeField,Range(0f,1f)]
	float missing;

	public static void GenerateInRing(GameObject source, Vector3 pos, float initialAngle,float radius, int count, float missing)
	{
		float angle = initialAngle;
		for (int f1 = 0; f1 < count; f1++)
		{
			if (Random.Range(0f, 1f) < missing)
			{
				angle += 360f / count;
				continue;
			}

			var obj = Instantiate(source);

			var objPos = pos;
			objPos.x += Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
			objPos.y += Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

			obj.transform.position = objPos;
			obj.transform.SetRotZ(angle - 90f);

			angle += 360f / count;
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
				BlockGenerator.GenerateInRing(param._source, param._pos, param._initialAngle, param._radius, param.count, param.missing);
			}
		}
	}
#endif
}
