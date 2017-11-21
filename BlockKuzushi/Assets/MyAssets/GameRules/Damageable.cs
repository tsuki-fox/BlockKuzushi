using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
	[SerializeField]
	float _maxDurability = 100;
	public float maxDurability
	{
		get { return _maxDurability; }
		set { _maxDurability = value; }
	}

	float _durability;
	public float durability { get { return _durability; } }

	private void Awake()
	{
		_durability = _maxDurability;
	}

	public void TakeDamage(float value)
	{
		value = Mathf.Clamp(value, 0, value);
		_durability -= value;
		if (_durability <= 0)
			Destroy(gameObject);
	}

	public void Recovery(float value)
	{
		value = Mathf.Clamp(value, 0, value);
		_durability += value;
		_durability = Mathf.Clamp(_durability, 0, _maxDurability);
	}
}
