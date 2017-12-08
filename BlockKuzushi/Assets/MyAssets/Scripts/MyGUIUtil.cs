using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyGUIUtil
{
	static Color _guiColor;
	static Color _guiContentColor;

	public static void BeginColorChange(Color color)
	{
		_guiColor = GUI.color;
		GUI.color = color;
	}

	public static void EndColorChange()
	{
		GUI.color = _guiColor;
	}

	public static void BeginContentColorChange(Color color)
	{
		_guiContentColor = GUI.contentColor;
		GUI.contentColor = color;
	}

	public static void EndContentColorChange()
	{
		GUI.contentColor = _guiContentColor;
	}
}