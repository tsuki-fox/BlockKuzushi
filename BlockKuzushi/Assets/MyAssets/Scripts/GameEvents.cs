using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

/*
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

	public void IgnitionCollisionEvent(string v1,v2)
	public void OnCollision


	any.onEvent+=(int value){};
	any.onEvent+=(int value,string name){};



*/

namespace GameEvents
{
	/// <summary>
	/// 宣言
	/// </summary>
	public sealed class Declares
	{
		public enum CollisionTiming
		{
			Enter,
			Stay,
			Exit
		}
		public struct CollisionKey
		{
			public CollisionTiming timing;
			public string name;
			public string otherName;
		}
		public delegate void OnCollisionEnterHandler(GameObject self, GameObject other, Collision2D collision);
	}

	/// <summary>
	/// 衝突関係
	/// </summary>
	public static class Collisions
	{
		static Dictionary<Declares.CollisionKey, Declares.OnCollisionEnterHandler> _handlers 
			= new Dictionary<Declares.CollisionKey, Declares.OnCollisionEnterHandler>();

		static bool IsSafe(Declares.CollisionKey key)
		{
			if (!_handlers.ContainsKey(key))
			{
				Debug.LogWarningFormat("存在しないキー>>>timing:{0} self:{1} other:{2}", key.timing.ToString(), key.name, key.otherName);
				return false;
			}
			return true;
		}

		public static Declares.CollisionKey GetKey(Declares.CollisionTiming timing,string name,string otherName)
		{
			Declares.CollisionKey key = new Declares.CollisionKey();
			key.timing = timing;
			key.name = name;
			key.otherName = otherName;
			return key;
		}

		public static Declares.CollisionKey AddHandler(Declares.CollisionTiming timing, string name, string otherName, Declares.OnCollisionEnterHandler handler)
		{
			var key = GetKey(timing, name, otherName);
			if (_handlers.ContainsKey(key))
				_handlers[key] += handler;
			else
				_handlers.Add(key, handler);
			return key;
		}

		public static void RemoveHandler(Declares.CollisionTiming timing,string name,string otherName,Declares.OnCollisionEnterHandler handler)
		{
			var key = GetKey(timing, name, otherName);
			if(IsSafe(key))
				_handlers[key] -= handler;
		}

		public static void Ignition(Declares.CollisionTiming timing, string name, string otherName, GameObject self, GameObject other, Collision2D collision)
		{
			var key = GetKey(timing, name, otherName);
			if (IsSafe(key))
				_handlers[key](self, other, collision);
		}

		public static void Ignition(Declares.CollisionTiming timing,GameObject self,GameObject other,Collision2D collision)
		{
			var key = GetKey(timing, self.tag, other.tag);
			if (IsSafe(key))
				_handlers[key](self, other, collision);
		}
	}


	namespace Destroies
	{
	}
}
