using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Generals/PlayerControlledBlockRotator")]
public class PlayerControlledBlockRotator : MonoBehaviour
{
	[SerializeField, Header("回転速度")]
	float _rotateSpeed = 1;

	[SerializeField]
	Transform _blockRoot;

	public void Retarget()
	{
		_blockRoot = transform.Find("Blocks");
	}

	void Start()
	{
		Retarget();
	}

	void Update()
	{
		if (MyInput.leftRot)
			_blockRoot.Rotate(new Vector3(0, 0, _rotateSpeed));
		if (MyInput.rightRot)
			_blockRoot.Rotate(new Vector3(0, 0, -_rotateSpeed));
	}
}
