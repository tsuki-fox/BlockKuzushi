using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
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

		GetComponent<CollisionObserver>().Subscribe();
		GetComponent<CollisionObserver>().AddHandlerCollisionEnter2DAll(col =>
		{
			MessageVisualizer.Write("hit!", col.transform.position);
			col.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
			col.gameObject.GetComponentInChildren<TrailRenderer>().startColor = Color.white;
			col.gameObject.GetComponentInChildren<TrailRenderer>().endColor = Color.white;
			col.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
			foreach (Transform item in col.gameObject.transform)
				item.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");


			var particle = Instantiate(_particle);
			particle.transform.position = col.contacts[0].point;

			Observable.Timer(System.TimeSpan.FromSeconds(3)).Subscribe(t =>
			{
				Destroy(particle.gameObject);
			}).AddTo(particle);

			Camera.main.transform.DOComplete();
			Camera.main.transform.DOShakePosition(0.1f, 0.2f);
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		float delta = 0f;
		if (MyInput.leftRot)
			delta += 10f;
		if (MyInput.rightRot)
			delta -= 10f;
		_blockers.transform.localEulerAngles += new Vector3(0f, 0f, delta);
	}
}
