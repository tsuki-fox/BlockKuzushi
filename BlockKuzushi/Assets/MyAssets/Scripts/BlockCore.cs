using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening;

public class BlockCore : MonoBehaviour
{
	[SerializeField]
	Color _color;
	[SerializeField]
	ParticleSystem _particle;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		/*
		MessageVisualizer.Write("HIT", transform.position);

		if(collision.gameObject.tag=="Ball")
		{
			Camera.main.transform.DOComplete();
			Camera.main.transform.DOShakePosition(0.1f);

			var ps = Instantiate(_particle);
			ps.transform.position = transform.position;
			Destroy(ps.gameObject, 5f);
			Destroy(gameObject);
		}
		*/
	}

	// Use this for initialization
	void Awake ()
	{
		Observable.Interval(TimeSpan.FromMilliseconds(16)).Subscribe(cnt =>
		{
			GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(new Color(_color.r, _color.g, _color.b, 0f), _color, (cnt+1) / 100.0f);
		}).AddTo(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
