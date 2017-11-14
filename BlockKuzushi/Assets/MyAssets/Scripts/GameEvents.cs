using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
	public delegate void OnConvertEnemyBulletHandler(GameObject playerBlocker, GameObject enemyBullet);
	public static event OnConvertEnemyBulletHandler onConvertEnemyBullet = delegate { };
	public static void TriggerConvertEnemyBullet(GameObject playerBlocker,GameObject enemyBullet)
	{
		onConvertEnemyBullet(playerBlocker, enemyBullet);
	}

	public delegate void OnDamageEnemyBlockerHandler(GameObject enemyBlocker, GameObject playerBullet);
	public static event OnDamageEnemyBlockerHandler onDamageEnemyBlockerHandler = delegate { };
}