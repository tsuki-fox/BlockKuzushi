using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerController : MonoBehaviour
{
	public delegate void OnCollisionEnter2DHandler(Collision2D collision);
	public delegate void OnCollisionStay2DHandler(Collision2D collision);
	public event OnCollisionEnter2DHandler onCollisionEnter2D = delegate { };

	private void OnCollisionEnter2D(Collision2D collision)
	{
		onCollisionEnter2D(collision);
	}
	private void OnCollisionStay2D(Collision2D collision)
	{
		
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		
	}
}
