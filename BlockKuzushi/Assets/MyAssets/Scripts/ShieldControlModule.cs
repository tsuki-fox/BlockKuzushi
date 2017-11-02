using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldControlModule : MonoBehaviour
{
	[SerializeField]
	float _distance = 1.0f;

	[SerializeField]
	GameObject _target;

	private void Update()
	{
		if (_target == null)
			return;

		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var dir = mousePos - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x);
		var angleDeg = angle * Mathf.Rad2Deg;

		var pos = new Vector3();
		pos.x = Mathf.Cos(angle) * _distance;
		pos.y = Mathf.Sin(angle) * _distance;

		_target.transform.position = pos;
		_target.transform.SetRotZ(angleDeg - 90f);
	}
}
