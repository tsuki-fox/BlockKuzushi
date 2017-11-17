using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBusConnector : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//敵ブロックに接触時
		if (collision.gameObject.tag == TagName.EnemyBlock.ToString())
			GameEvents.Collisions.Notify(GameEvents.Declares.CollisionTiming.Enter, TagName.PlayerBullet, TagName.EnemyBlock, gameObject, collision.gameObject, collision);

		//敵に接触時
		if(collision.gameObject.tag==TagName.Enemy.ToString())
			GameEvents.Collisions.Notify(GameEvents.Declares.CollisionTiming.Enter, TagName.PlayerBullet, TagName.Enemy, gameObject, collision.gameObject, collision);
	}

	private void OnDestroy()
	{
		//GameEvents.Destroies.Notify(TagName.PlayerBullet, gameObject);
	}
}
