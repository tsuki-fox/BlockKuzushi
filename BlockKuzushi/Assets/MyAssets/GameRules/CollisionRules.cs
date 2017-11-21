using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using DG.Tweening;

public class CollisionRules : MonoBehaviour
{
	[SerializeField]
	ParticleSystem _blockHitParticle;
	[SerializeField]
	GameObject _playerBulletSrc;
	[SerializeField]
	GameObject _enemy;
	[SerializeField]
	public AudioClip _sound;
	[SerializeField]
	public AudioClip _damage;
	[SerializeField]
	public AudioClip _reflection;
	[SerializeField]
	public SpriteRenderer _overlay;
	AudioSource _audioSource;

	void CreateParticle(Vector3 pos,float duration)
	{
		var particle = Instantiate(_blockHitParticle);
		particle.transform.position = pos;
		Observable.Timer(System.TimeSpan.FromSeconds(duration)).Subscribe(t =>
		{
			Destroy(particle.gameObject);
		}).AddTo(particle);
	}

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		Observable.Interval(System.TimeSpan.FromSeconds(2)).Subscribe(c =>
		{
			var enemy = Instantiate(_enemy);
			enemy.transform.position = Camera.main.ScreenToWorldPoint(RandomEx.RangeVector3(Vector3.zero, new Vector3(Screen.width, Screen.height, 0f)));
			enemy.transform.SetPositionZ(0);
		}).AddTo(this.gameObject);

		var go = gameObject;

		//プレイヤーダメージイベント
		GameEvents.Collisions.Subscribe(GameEvents.Declares.CollisionTiming.Enter, TagName.Player, TagName.EnemyBullet, (player, eBullet,collision) =>
		{
			Camera.main.DOComplete();
			Camera.main.DOShakePosition(0.4f, 1f, 5);
			_audioSource.PlayOneShot(_damage);
			_overlay.color = Color.red;
			Observable.IntervalFrame(1).Subscribe(c =>
			{
				_overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b,1-( c / 30f));
			}).AddTo(this.gameObject);
			Destroy(eBullet.gameObject);
		});

		//敵弾変換イベント
		GameEvents.Collisions.Subscribe(GameEvents.Declares.CollisionTiming.Enter, TagName.PlayerBlock, TagName.EnemyBullet, (playerBlocker, enemyBullet, collision) =>
		{
			MessageVisualizer.Write("hit!", enemyBullet.transform.position,Color.black);

			//PBullet生成
			var pBullet = Instantiate(_playerBulletSrc);
			pBullet.GetComponent<Rigidbody2D>().velocity = enemyBullet.GetComponent<Rigidbody2D>().velocity;
			pBullet.transform.position = enemyBullet.transform.position;

			//EBullet削除
			Destroy(enemyBullet);

			_audioSource.PlayOneShot(_reflection);

		});

		//敵ブロックダメージイベント
		GameEvents.Collisions.Subscribe(GameEvents.Declares.CollisionTiming.Enter, TagName.EnemyBlock, TagName.PlayerBullet, (eBlock, pBullet, collision) =>
		{
			//ダメージ処理
			//? 暫定処理
			MessageVisualizer.Write("damage!", eBlock,Color.red);
			var damageable = eBlock.GetComponent<Damageable>();
			if (damageable)
				damageable.TakeDamage(10);

			//エフェクト生成
			CreateParticle(pBullet.transform.position, 2f);

			//PBullet削除
			Destroy(pBullet);
		});

		//敵ダメージイベント
		GameEvents.Collisions.Subscribe(GameEvents.Declares.CollisionTiming.Enter, TagName.Enemy, TagName.PlayerBullet, (enemy, pBullet, collision) =>
		{

			//ダメージ処理
			var damageable = enemy.GetComponent<Damageable>();
			if (damageable)
				damageable.TakeDamage(10);

			//エフェクト生成
			CreateParticle(pBullet.transform.position, 2f);

			//PBullet削除
			Destroy(pBullet);
		});

		//死亡時
		GameEvents.Destroies.Subscribe(TagName.EnemyBlock, eBlock =>
		{
			//画面揺れ
			Camera.main.DOComplete();
			Camera.main.DOShakePosition(0.1f, 0.5f, 3);
			//パーティクル
			CreateParticle(eBlock.transform.position, 2);
			_audioSource.PlayOneShot(_sound);
		});

		GameEvents.Destroies.Subscribe(TagName.Enemy, enemy =>
		{
			//画面揺れ
			Camera.main.DOComplete();
			Camera.main.DOShakePosition(1f, 2f, 5);

			var pos = enemy.transform.position;

			for(int i=0;i<enemy.transform.Find("Blockers").childCount;i++)
			{
				Observable.Timer(System.TimeSpan.FromSeconds(i * 0.1f)).Subscribe(t =>
				{
					var delta = RandomEx.RangeVector3(new Vector3(-1, -1, -1), new Vector3(1, 1, 1));
					CreateParticle(pos + delta, 2);
					Camera.main.DOComplete();
					Camera.main.DOShakePosition(0.1f, 0.5f, 3);
					_audioSource.PlayOneShot(_sound);
				}).AddTo(this.gameObject);
			}
		});
	}
}
