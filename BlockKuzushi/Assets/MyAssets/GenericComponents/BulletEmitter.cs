using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AddComponentMenu("Generals/BulletEmitter")]
public class BulletEmitter : MonoBehaviour
{
	public enum SpecifyType
	{
		ConstantAngle,  //角度指定
		ByObject,		//オブジェクト指定
		Random,			//ランダム
	}

	[SerializeField,Header("生成する弾のプレハブ")]
	GameObject _sourceBullet;

	[SerializeField, Header("位置オフセット")]
	Vector2 _offset;
	[SerializeField, Header("発射速度")]
	float _shotSpeed;
	[SerializeField, Header("角度指定方法")]
	SpecifyType _specifyType;
	[SerializeField,Header("ターゲットオブジェクト")]
	GameObject _targetObject;
	[SerializeField,Header("発射角度(固定)")]
	float _constantAngle;
	[SerializeField]
	float _randomMin;
	[SerializeField]
	float _randomMax;

	[SerializeField, Header("生成個数を無制限にする")]
	bool _isUnlimitedEmitting = false;
	[SerializeField,Header("生成個数制限")]
	int _emittingLimit = 10;

	[SerializeField, Header("生成開始時間")]
	float _startTime = 0;
	[SerializeField, Header("生成間隔")]
	float _emitInterval = 1;

	//経過時間
	float _elapsedTime = 0;
	//現在の生成個数
	int _emitCount = 0;

	void Emit()
	{
		Vector2 direction = Vector2.zero;
		switch (_specifyType)
		{
			case SpecifyType.ConstantAngle:
				direction.x = Mathf.Cos(_constantAngle * Mathf.Deg2Rad);
				direction.y = Mathf.Sin(_constantAngle * Mathf.Deg2Rad);
				break;
			case SpecifyType.ByObject:
				direction = _targetObject.transform.position - transform.position;
				direction.Normalize();
				break;
			case SpecifyType.Random:
				var angle = UnityEngine.Random.Range(_randomMin, _randomMax);
				direction.x = Mathf.Cos(angle * Mathf.Deg2Rad);
				direction.y = Mathf.Sin(angle * Mathf.Deg2Rad);
				break;
		}

		//var bullet = Instantiate(_sourceBullet);
		var bullet = ObjectPool.Borrow(_sourceBullet);
		bullet.transform.position = transform.position + _offset.ToVector3();
		bullet.GetComponent<Rigidbody2D>().velocity = _shotSpeed * direction;
	}

	void Start()
	{
		Observable.Interval(TimeSpan.FromSeconds(_emitInterval)).Subscribe(_ =>
		{
			if (_elapsedTime < _startTime)
				return;
			if (_emitCount >= _emittingLimit && !_isUnlimitedEmitting)
				return;
			_emitCount++;
			Emit();
		}).AddTo(this);
	}

	void Update()
	{
		_elapsedTime += Time.deltaTime;
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(BulletEmitter))]
	[CanEditMultipleObjects]
	public class BulletEmitterInspector:Editor
	{
		public override void OnInspectorGUI()
		{
			var self = target as BulletEmitter;

			EditorGUI.BeginChangeCheck();
			var sourceBullet = (GameObject)EditorGUILayout.ObjectField("生成する弾のプレハブ", self._sourceBullet, typeof(GameObject), false);
			var offset = EditorGUILayout.Vector2Field("位置オフセット", self._offset);
			var shotSpeed = EditorGUILayout.FloatField("発射速度", self._shotSpeed);
			var specifyType = (SpecifyType)EditorGUILayout.EnumPopup("角度指定方法", self._specifyType);

			float constantAngle = self._constantAngle;
			GameObject targetObject = self._targetObject;
			float randomMin = self._randomMin;
			float randomMax = self._randomMax;

			EditorGUILayout.BeginVertical("box");
			switch (specifyType)
			{
				case SpecifyType.ConstantAngle:
					constantAngle = EditorGUILayout.FloatField("角度", self._constantAngle);
					break;
				case SpecifyType.ByObject:
					targetObject = (GameObject)EditorGUILayout.ObjectField("ターゲットオブジェクト", self._targetObject, typeof(GameObject), true);
					break;
				case SpecifyType.Random:
					randomMin = EditorGUILayout.FloatField("最小値", self._randomMin);
					randomMax = EditorGUILayout.FloatField("最大値", self._randomMax);
					EditorGUILayout.MinMaxSlider(ref randomMin, ref randomMax, 0, 360);
					break;
			}
			EditorGUILayout.EndVertical();

			var isUnlimitedEmitting = EditorGUILayout.Toggle("生成個数を無制限にする", self._isUnlimitedEmitting);
			var emittingLimit = self._emittingLimit;
			EditorGUI.BeginDisabledGroup(isUnlimitedEmitting);
			emittingLimit = EditorGUILayout.IntField("生成個数制限",self._emittingLimit);
			EditorGUI.EndDisabledGroup();

			var startTime = EditorGUILayout.FloatField("生成開始時間", self._startTime);
			var emitInterval = EditorGUILayout.FloatField("生成間隔", self._emitInterval);

			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Changed BulletEmitterInspector");
				self._sourceBullet = sourceBullet;
				self._offset = offset;
				self._shotSpeed = shotSpeed;
				self._specifyType = specifyType;
				self._constantAngle = constantAngle;
				self._targetObject = targetObject;
				self._randomMin = randomMin;
				self._randomMax = randomMax;
				self._isUnlimitedEmitting = isUnlimitedEmitting;
				self._emittingLimit = emittingLimit;
				self._startTime = startTime;
				self._emitInterval = emitInterval;
			}
			//base.OnInspectorGUI();
		}
	}
#endif
}
