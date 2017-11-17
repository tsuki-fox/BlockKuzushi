using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBusConnector : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
	}

	private void OnDestroy()
	{
		//GameEvents.Destroies.Notify(TagName.Player, this.gameObject);
	}
}
