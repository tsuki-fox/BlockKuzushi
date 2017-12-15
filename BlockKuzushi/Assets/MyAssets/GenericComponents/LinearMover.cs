using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>直線移動コンポーネント</summary>
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Generals/LinearMover")]
public class LinearMover : MonoBehaviour
{
	[SerializeField, Header("速度")]
	float _speed;
	[SerializeField, Header("角度")]
	float _eulerAngle;

	Rigidbody2D _body;

	void Awake()
	{
		_body = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		float rad = Mathf.Deg2Rad * _eulerAngle;
		_body.velocity = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
	}


	void OnDrawGizmos()
	{
		float rad = Mathf.Deg2Rad * _eulerAngle;
		var dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, dir*100f);
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(LinearMover)),CanEditMultipleObjects()]
	public class LinearMoverInspector : Editor
	{
		//エディットモードか?
		bool _isEditMode = false;

		void OnSceneGUI()
		{
			if (!_isEditMode)
				return;
			var self = target as LinearMover;
			self._eulerAngle = Handles.RotationHandle(Quaternion.Euler(0, 0, self._eulerAngle), self.transform.position).eulerAngles.z;
			Undo.RecordObject(target, "LinearMover Handle Changed");
		}

		public override void OnInspectorGUI()
		{
			var self = target as LinearMover;

			_isEditMode = GUILayout.Toggle(_isEditMode, _isEditMode ? "Exit Edit" : "Edit", "button");

			EditorGUI.BeginChangeCheck();
			var speed = EditorGUILayout.Slider(
				label: "速度",
				value: self._speed,
				leftValue: 0f,
				rightValue: 100f);

			var eulerAngle = EditorGUILayout.Knob(
				knobSize: Vector2.one * 64,
				value: self._eulerAngle,
				minValue: 0f,
				maxValue: 360f,
				unit: "度",
				backgroundColor: Color.gray,
				activeColor: Color.red,
				showValue: true);
			if(EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "LinearMover Changed");
				self._speed = speed;
				self._eulerAngle = eulerAngle;
			}
		}
	}
#endif
}
