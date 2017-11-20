using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameObjectEx
{
	public static void SetChildrenLayer(this GameObject src, int layer, bool containSelf = true)
	{
		if (containSelf)
			src.layer = layer;
		foreach (Transform item in src.transform)
			item.gameObject.layer = layer;
	}

	public static void SetChildrenTag(this GameObject src,TagName tag,bool containSelf=true)
	{
		if (containSelf)
			src.tag = tag.ToString();
		foreach (Transform item in src.transform)
			item.gameObject.tag = tag.ToString();
	}

	public static TagName GetEnumTagName(this GameObject src)
	{
		return (TagName)Enum.Parse(typeof(TagName), src.tag);
	}
}