﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class EnemyTest : MonoBehaviour
{
	[SerializeField]
	GameObject _bulletSrc;
	[SerializeField]
	GameObject _target;

	private void Start()
	{
		_target = GameObject.FindGameObjectWithTag("Player");

		Observable.Interval(System.TimeSpan.FromSeconds(0.5f)).Subscribe(t =>
		{
			var dir = (_target.transform.position - transform.position).normalized;
			var blt = Instantiate(_bulletSrc);
			dir += new Vector3(Random.Range(0f, 0.3f), Random.Range(0f, 0.3f));
			blt.transform.position = transform.position;
			blt.GetComponent<BulletMovement>().velocity = dir * 10f;
		}).AddTo(this);

		var emitter = GetComponent<BlockerEmitter>();

		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				var obj = emitter.Emit(i / 8f * 360f+j*15f, 30f, 1f+j*0.5f, 0.3f);
				obj.layer = LayerMask.NameToLayer("EnemyBlock");
			}
		}
	}

	private void Update()
	{
		transform.AddEulerAnglesZ(45f * Time.deltaTime);
	}
}
