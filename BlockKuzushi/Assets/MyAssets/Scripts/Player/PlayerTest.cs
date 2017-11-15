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

		GetComponent<CollisionObserver>().Subscribe();

		GameEvents.Collisions.AddHandler(GameEvents.Declares.CollisionTiming.Enter, "pblocker", "ebullet", (playerBlocker, enemyBullet, collision) =>
		{
			MessageVisualizer.Write("hit!", enemyBullet.transform.position);
			enemyBullet.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
			enemyBullet.gameObject.GetComponentInChildren<TrailRenderer>().startColor = Color.white;
			enemyBullet.gameObject.GetComponentInChildren<TrailRenderer>().endColor = Color.white;
			enemyBullet.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
			enemyBullet.gameObject.SetChildrenLayer(LayerMask.NameToLayer("PlayerBullet"));
			if (collision.contacts.Count() > 0)
			{
				var particle = Instantiate(_particle);
				particle.transform.position = collision.contacts[0].point;

				Observable.Timer(System.TimeSpan.FromSeconds(3)).Subscribe(t =>
				{
					Destroy(particle.gameObject);
				}).AddTo(particle);
			}
		});

		GetComponent<CollisionObserver>().AddHandlerCollisionEnter2DAll((self, col) =>
		{
			GameEvents.Collisions.Ignition(GameEvents.Declares.CollisionTiming.Enter,"pblocker","ebullet", self, col.gameObject, col);
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
