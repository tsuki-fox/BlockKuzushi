using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectEx
{
	public static void SetChildrenLayer(this GameObject src, int layer, bool containSelf = true)
	{
		if (containSelf)
			src.layer = layer;
		foreach (Transform item in src.transform)
			item.gameObject.layer = layer;
	}
}