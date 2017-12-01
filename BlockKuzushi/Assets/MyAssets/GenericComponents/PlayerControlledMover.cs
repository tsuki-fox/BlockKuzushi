using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Generals/PlayerControlledMover")]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlledMover : MonoBehaviour
{
	[SerializeField, Header("移動速度")]
	float _moveSpeed;

	Rigidbody2D _rigidbody;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();	
	}

	void Update()
	{
		var velocity = Vector2.zero;
		if (MyInput.left)
			velocity.x -= _moveSpeed;
		if (MyInput.right)
			velocity.x += _moveSpeed;
		if (MyInput.up)
			velocity.y += _moveSpeed;
		if (MyInput.down)
			velocity.y -= _moveSpeed;
		_rigidbody.velocity = velocity;

	}
}
