using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

/// <summary>
/// ゲームイベントバス
/// </summary>
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
			public TagName self;
			public TagName other;
		}
		public delegate void OnCollisionHandler(GameObject self, GameObject other, Collision2D collision);
		public delegate void OnDestroyHandler(GameObject self);
	}

	/// <summary>
	/// 衝突関係
	/// </summary>
	public static class Collisions
	{
		//ハンドラコンテナ
		static Dictionary<Declares.CollisionKey, Declares.OnCollisionHandler> _handlers 
			= new Dictionary<Declares.CollisionKey, Declares.OnCollisionHandler>();
		/// <summary>キーが安全であるか</summary>
		/// <param name="key">キー</param>
		static bool IsSafe(Declares.CollisionKey key)
		{
			if (!_handlers.ContainsKey(key))
			{
				Debug.LogWarningFormat("存在しないキー>>>timing:{0} self:{1} other:{2}", key.timing.ToString(), key.self.ToString(), key.other.ToString());
				return false;
			}
			return true;
		}
		/// <summary>キーを取得する</summary>
		/// <param name="timing">タイミング</param>
		/// <param name="selfName">名前</param>
		/// <param name="otherName">名前</param>
		/// <returns></returns>
		public static Declares.CollisionKey GetKey(Declares.CollisionTiming timing,TagName self,TagName other)
		{
			Declares.CollisionKey key = new Declares.CollisionKey();
			key.timing = timing;
			key.self = self;
			key.other = other;
			return key;
		}
		/// <summary>ハンドラを追加する</summary>
		/// <param name="timing">タイミング</param>
		/// <param name="selfName">名前</param>
		/// <param name="otherName">名前</param>
		/// <param name="handler">ハンドラ</param>
		/// <returns></returns>
		public static Declares.CollisionKey Subscribe(Declares.CollisionTiming timing,TagName self,TagName other,Declares.OnCollisionHandler handler)
		{
			var key = GetKey(timing, self, other);
			if (_handlers.ContainsKey(key))
				_handlers[key] += handler;
			else
				_handlers.Add(key, handler);
			return key;
		}
		/// <summary>ハンドラを削除する</summary>
		/// <param name="timing">タイミング</param>
		/// <param name="selfName">名前</param>
		/// <param name="otherName">名前</param>
		/// <param name="handler">ハンドラ</param>
		public static void Unsubscribe(Declares.CollisionTiming timing,TagName self,TagName other,Declares.OnCollisionHandler handler)
		{
			var key = GetKey(timing, self, other);
			if(IsSafe(key))
				_handlers[key] -= handler;
		}
		/// <summary>イベントを発火する</summary>
		/// <param name="timing">タイミング</param>
		/// <param name="selfName">名前</param>
		/// <param name="otherName">名前</param>
		/// <param name="self">オブジェクト</param>
		/// <param name="other">オブジェクト</param>
		/// <param name="collision">コリジョン情報</param>
		public static void Notify(Declares.CollisionTiming timing, TagName selfTag, TagName otherTag, GameObject self, GameObject other, Collision2D collision)
		{
			var key = GetKey(timing, selfTag, otherTag);
			if (_handlers.ContainsKey(key))
				_handlers[key](self, other, collision);
			else
				_handlers.Add(key, (v1, v2, v3) => { });
		}
	}

	public static class Destroies
	{
		static Dictionary<TagName, Declares.OnDestroyHandler> _handlers 
			= new Dictionary<TagName, Declares.OnDestroyHandler>();

		public static void Subscribe(TagName name,Declares.OnDestroyHandler handler)
		{
			if (_handlers.ContainsKey(name))
				_handlers[name] += handler;
			else
				_handlers.Add(name, handler);
		}
		public static void Unsubscribe(TagName name, Declares.OnDestroyHandler handler)
		{
			if (_handlers.ContainsKey(name))
				_handlers[name] -= handler;
		}
		public static void Notify(TagName name, GameObject self)
		{
			if (_handlers.ContainsKey(name))
				_handlers[name](self);
		}
	}
}
