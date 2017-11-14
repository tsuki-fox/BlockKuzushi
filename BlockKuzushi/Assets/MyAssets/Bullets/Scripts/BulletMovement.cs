using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletMovement : MonoBehaviour
{
	[SerializeField]
	ParticleSystem _particle;

	Rigidbody2D _body;

	public Vector2 velocity
	{
		get { return _body.velocity; }
		set { _body.velocity = value; }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
		{
			if(collision.gameObject.layer==LayerMask.NameToLayer("Enemy"))
			{
				var particle=Instantiate(_particle);
				particle.transform.position = transform.position;
				Destroy(particle.gameObject, 3f);
				Destroy(gameObject);
				MessageVisualizer.Write("hit!", this);
			}
		}
	}

	private void Awake()
	{
		_body = GetComponent<Rigidbody2D>();

		Observable.Timer(System.TimeSpan.FromSeconds(4)).Subscribe(_ =>
		{
			MessageVisualizer.Write("anni", transform.position);
			Destroy(gameObject);
		}).AddTo(this);
	}
}
