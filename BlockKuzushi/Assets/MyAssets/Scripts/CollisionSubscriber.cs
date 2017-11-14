using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSubscriber : MonoBehaviour
{
	public delegate void onCollision2DHandler(GameObject self,Collision2D collision);

	public event onCollision2DHandler onCollisionEnter2D = delegate { };
	public event onCollision2DHandler onCollisionStay2D = delegate { };
	public event onCollision2DHandler onCollisionExit2D = delegate { };

	private void OnCollisionEnter2D(Collision2D collision)
	{
		onCollisionEnter2D(this.gameObject,collision);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		onCollisionStay2D(this.gameObject,collision);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		onCollisionExit2D(this.gameObject,collision);
	}
}