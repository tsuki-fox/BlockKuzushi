﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BusUploader : MonoBehaviour
{
	[SerializeField, Header("衝突開始時")]
	List<TagName> _enter;
	[SerializeField, Header("衝突中")]
	List<TagName> _stay;
	[SerializeField, Header("衝突終了時")]
	List<TagName> _exit;
	[SerializeField]
	bool deathNotifiable = true;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		foreach (var tag in _enter)
		{
			if (collision.gameObject.GetEnumTagName() == tag)
				GameEvents.Collisions.Notify(GameEvents.Declares.CollisionTiming.Enter, gameObject.GetEnumTagName(), tag, gameObject, collision.gameObject, collision);
		}
	}
	private void OnCollisionStay2D(Collision2D collision)
	{
		foreach (var tag in _stay)
		{
			if (collision.gameObject.GetEnumTagName() == tag)
				GameEvents.Collisions.Notify(GameEvents.Declares.CollisionTiming.Stay, gameObject.GetEnumTagName(), tag, gameObject, collision.gameObject, collision);
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		foreach (var tag in _exit)
		{
			if (collision.gameObject.GetEnumTagName() == tag)
				GameEvents.Collisions.Notify(GameEvents.Declares.CollisionTiming.Exit, gameObject.GetEnumTagName(), tag, gameObject, collision.gameObject, collision);
		}
	}

	private void OnDestroy()
	{
		if (deathNotifiable)
			GameEvents.Destroies.Notify(gameObject.GetEnumTagName(), gameObject);
	}
}
