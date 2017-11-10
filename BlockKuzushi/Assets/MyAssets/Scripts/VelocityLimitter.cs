﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocityLimitter : MonoBehaviour
{
	Rigidbody2D _body;

	[SerializeField]
	float _upper;
	[SerializeField]
	float _lower;

	private void Awake()
	{
		_body = GetComponent<Rigidbody2D>();
	}

	private void LateUpdate()
	{
		Vector2 vel = _body.velocity;
		Vector2 dir = vel.normalized;

		float mag = vel.magnitude;
		mag = Mathf.Clamp(mag, _lower, _upper);

		_body.velocity = mag * dir;
	}
}
