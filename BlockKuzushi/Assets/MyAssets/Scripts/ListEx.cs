using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListEx
{
	public static void RemoveAtList<T>(this List<T> target,List<T> list)
	{
		foreach (var item in list)
			target.Remove(item);
	}
}
