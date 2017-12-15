using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[AddComponentMenu("Generals/PlayerControlledMover")]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlledMover : MonoBehaviour
{
	[SerializeField, Header("移動速度")]
	float _moveSpeed;

	Rigidbody2D _rigidbody;
	Transform _spriteObject;

	Vector3 _pos;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteObject = transform.Find("Sprite");	
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

		if (Input.anyKey)
		{
			var rot = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f;
			_spriteObject.transform.DORotate(new Vector3(0, 0, rot), 0.1f);
		}

		_pos = transform.position;
	}
}
