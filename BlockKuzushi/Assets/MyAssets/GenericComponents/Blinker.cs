using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
	[SerializeField]
	float _interval;
	float _elapsed;

	Renderer _renderer;

	private void Awake()
	{
		_renderer = GetComponent<Renderer>();
	}

	void Update()
	{
		_elapsed += Time.deltaTime;
		if (_elapsed > _interval)
		{
			_renderer.enabled = !_renderer.enabled;
			_elapsed = 0f;
		}	
	}
}
