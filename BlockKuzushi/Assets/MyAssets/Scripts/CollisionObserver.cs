using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CollisionObserver : MonoBehaviour
{
	List<CollisionSubscriber> _subscribers = new List<CollisionSubscriber>();

	/// <summary>
	/// 子のサブスクライバをすべて登録する
	/// </summary>
	public void Subscribe()
	{
		_subscribers = GetComponentsInChildren<CollisionSubscriber>().ToList();
	}

	private void Awake()
	{
		Subscribe();
	}

	/// <summary>
	/// イベントハンドラを追加する
	/// </summary>
	/// <param name="handler">ハンドラ</param>
	public void AddHandlerCollisionEnter2DAll(CollisionSubscriber.onCollision2DHandler handler)
	{
		foreach (var item in _subscribers)
			item.onCollisionEnter2D += handler;
	}

	/// <summary>
	/// イベントハンドラを追加する
	/// </summary>
	/// <param name="handler">ハンドラ</param>
	public void AddHandlerCollisionStay2DAll(CollisionSubscriber.onCollision2DHandler handler)
	{
		foreach (var item in _subscribers)
			item.onCollisionStay2D += handler;
	}

	/// <summary>
	/// イベントハンドラを追加する
	/// </summary>
	/// <param name="handler">ハンドラ</param>
	public void AddHandlerCollisionExit2DAll(CollisionSubscriber.onCollision2DHandler handler)
	{
		foreach (var item in _subscribers)
			item.onCollisionExit2D += handler;
	}

	/// <summary>
	/// イベントハンドラを削除する
	/// </summary>
	/// <param name="handler">ハンドラ</param>
	public void RemoveHandlerCollision2DAll(CollisionSubscriber.onCollision2DHandler handler)
	{
		foreach (var item in _subscribers)
		{
			item.onCollisionEnter2D -= handler;
			item.onCollisionStay2D -= handler;
			item.onCollisionExit2D -= handler;
		}
	}

	/// <summary>
	/// オブジェクトの名前からイベントハンドラを追加する
	/// </summary>
	/// <param name="name">名前</param>
	/// <param name="handler">ハンドラ</param>
	public void AddHandlerCollisionEnter2DByName(string name, CollisionSubscriber.onCollision2DHandler handler)
	{
		foreach (var item in _subscribers.Where(item => item.name == name))
			item.onCollisionEnter2D += handler;
	}

	/// <summary>
	/// オブジェクトの名前からイベントハンドラを追加する
	/// </summary>
	/// <param name="name">名前</param>
	/// <param name="handler">ハンドラ</param>
	public void AddHandlerCollisionStay2DByName(string name, CollisionSubscriber.onCollision2DHandler handler)
	{
		foreach (var item in _subscribers.Where(item => item.name == name))
			item.onCollisionStay2D += handler;
	}

	/// <summary>
	/// オブジェクトの名前からイベントハンドラを追加する
	/// </summary>
	/// <param name="name">名前</param>
	/// <param name="handler">ハンドラ</param>
	public void AddHandlerCollisionExit2DByName(string name, CollisionSubscriber.onCollision2DHandler handler)
	{
		foreach (var item in _subscribers.Where(item => item.name == name))
			item.onCollisionExit2D += handler;
	}

	/// <summary>
	/// オブジェクトの名前からイベントハンドラを削除する
	/// </summary>
	/// <param name="name">名前</param>
	/// <param name="handler">ハンドラ</param>
	public void RemoveHandlerCollision2DByName(string name, CollisionSubscriber.onCollision2DHandler handler)
	{
		foreach (var item in _subscribers.Where(item => item.name == name))
		{
			item.onCollisionEnter2D += handler;
			item.onCollisionStay2D += handler;
			item.onCollisionExit2D += handler;
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(CollisionObserver))]
	[CanEditMultipleObjects]
	public class CollisionObserverInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var real = target as CollisionObserver;
			EditorGUILayout.LabelField("Count : "+real._subscribers.Count.ToString());
		}
	}
#endif
}
