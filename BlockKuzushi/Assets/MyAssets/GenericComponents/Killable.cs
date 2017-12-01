using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Generals/Killable")]
public class Killable : MonoBehaviour
{
	public delegate void OnDamageHandler(Killable self);
	public delegate void OnRecoveryHandler(Killable self);

	/// <summary>ダメージイベント</summary>
	public event OnDamageHandler onDamage = delegate { };
	/// <summary>回復イベント</summary>
	public event OnRecoveryHandler onRecovery = delegate { };

	[SerializeField, Header("最大へルス")]
	float _maxHealth = 100;
	float _health;

	/// <summary>最大ヘルス</summary>
	public float maxHealth
	{
		get { return _maxHealth; }
		set { _maxHealth = value; }
	}
	/// <summary>現在ヘルス</summary>
	public float health
	{
		get { return _health; }
	}

	void Awake()
	{
		_health = _maxHealth;	
	}

	/// <summary>ダメージを受ける</summary>
	/// <param name="value">受ける量 : value>0 </param>
	public void TakeDamage(float value)
	{
		value = Mathf.Max(0, value);
		_health -= value;
		onDamage(this);
		if (_health <= 0)
			Destroy(gameObject);
	}

	/// <summary>ヘルスを回復する</summary>
	/// <param name="value">回復する量 : value>0 </param>
	public void Recovery(float value)
	{
		value = Mathf.Max(0, value);
		_health += value;
		_health = Mathf.Clamp(_health, 0, _maxHealth);
		onRecovery(this);
	}
}