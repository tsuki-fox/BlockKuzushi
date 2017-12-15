using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TF;
using UniRx;

public class AutoRepay : MonoBehaviour
{
	[SerializeField]
	float _repayTime=1f;

	void OnBorrow()
	{
		Observable.Timer(System.TimeSpan.FromSeconds(_repayTime)).Subscribe(t =>
		{
			ObjectPool.Repay(gameObject);
		}).AddTo(this);
	}
}
