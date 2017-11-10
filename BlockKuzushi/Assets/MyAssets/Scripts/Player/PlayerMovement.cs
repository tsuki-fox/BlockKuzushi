using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	float _moveSpeed;

	private void Update()
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
		transform.position += delta * Time.deltaTime;
	}
}
