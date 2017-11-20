using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class CollisionRules : MonoBehaviour
{
	[SerializeField]
	ParticleSystem _blockHitParticle;
	[SerializeField]
	GameObject _playerBulletSrc;

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

		});

		//敵ブロックダメージイベント
		GameEvents.Collisions.Subscribe(GameEvents.Declares.CollisionTiming.Enter, TagName.EnemyBlock, TagName.PlayerBullet, (eBlock, pBullet, collision) =>
		{
			//ダメージ処理
			//? 暫定処理
			MessageVisualizer.Write("damage!", eBlock,Color.red);

			//エフェクト生成
			CreateParticle(pBullet.transform.position, 2f);

			//PBullet削除
			Destroy(pBullet);
		});

		//死亡時
		GameEvents.Destroies.Subscribe(TagName.EnemyBlock, eBlock =>
		{
			//画面揺れ
		});
	}
}
