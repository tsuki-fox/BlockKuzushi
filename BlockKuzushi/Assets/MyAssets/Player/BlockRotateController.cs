using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotateController : MonoBehaviour
{
	[SerializeField,Header("ブロックのルート(auto)")]
	Transform _root;
	[SerializeField,Header("回転速度")]
	float _rotSpeed;

	void Start()
	{
		//子からブロックのルートを取得
		_root = transform.Find("Blockers");
	}

	void Update()
	{
		if (!_root)
			return;

		if(MyInput.leftRot)
			_root.AddEulerAnglesZ(_rotSpeed * Time.deltaTime);
		if (MyInput.rightRot)
			_root.AddEulerAnglesZ(-_rotSpeed * Time.deltaTime);
	}
}
