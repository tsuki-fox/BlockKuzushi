using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using DG.Tweening;

public class PlayerTest : MonoBehaviour
{
	[SerializeField]
	GameObject _blockers;
	[SerializeField]
	ParticleSystem _particle;

	// Use this for initialization
	void Start ()
	{
		var emitter = GetComponent<BlockerEmitter>();
		
		for(int i=0;i<4;i++)
		{
			emitter.Emit(i / 4f * 360f, 30f, 1f, 0.3f);
		}

		_blockers = transform.Find("Blockers").gameObject;
	}
}
