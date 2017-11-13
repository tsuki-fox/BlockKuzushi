using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	float _moveSpeed;

	Rigidbody2D _body;

	private void Awake()
	{
		_body = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		//移動の処理
		Vector3 delta = new Vector3();
		if(MyInput.up)
			delta.y += _moveSpeed;
		if(MyInput.down)
			delta.y -= _moveSpeed;
		if(MyInput.left)
			delta.x -= _moveSpeed;
		if(MyInput.right)
			delta.x += _moveSpeed;

		_body.velocity = delta;
	}
}
