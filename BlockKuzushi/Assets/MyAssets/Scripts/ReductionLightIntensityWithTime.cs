using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(Light))]
public class ReductionLightIntensityWithTime : MonoBehaviour
{
	[SerializeField]
	float _duration;
	Light _light;

	void Awake()
	{
		_light = GetComponent<Light>();
		float sub = _light.intensity / (_duration + 0.00001f);

		Observable.IntervalFrame(1).Subscribe(t =>
		{
			_light.intensity -= sub * Time.deltaTime;
		}).AddTo(this);
	}
}
