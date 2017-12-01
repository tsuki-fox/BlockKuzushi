using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class ChangeColorFromDamages : MonoBehaviour
{
	[SerializeField,Header("最大時の色")]
	Color _startColor;
	[SerializeField,Header("最小値の色")]
	Color _endColor;

	void Start()
	{
		GetComponent<Damageable>().onDamage += OnDamage;
		GetComponent<Damageable>().onRecovery += OnRecovery;
	}

	void OnDamage(Damageable self)
	{
		Material mat = self.GetComponent<MeshRenderer>().material;
		mat.color = Color.Lerp(_startColor, _endColor, 1f - self.durability / self.maxDurability);
	}

	void OnRecovery(Damageable self)
	{
		Material mat = self.GetComponent<MeshRenderer>().material;
		mat.color = Color.Lerp(_startColor, _endColor, 1f - self.durability / self.maxDurability);
	}
}
