using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformEx
{
	public static void SetPosX(this Transform src,float x)
	{
		src.position = new Vector3(x, src.position.y, src.position.z);
	}
	public static void SetPosY(this Transform src, float y)
	{
		src.position = new Vector3(src.position.x, y, src.position.z);
	}
	public static void SetPosZ(this Transform src,float z)
	{
		src.position = new Vector3(src.position.x, src.position.y, z);
	}

	public static void SetRotX(this Transform src,float x)
	{
		src.eulerAngles = new Vector3(x, src.eulerAngles.y, src.eulerAngles.z);
	}
	public static void SetRotY(this Transform src,float y)
	{
		src.eulerAngles = new Vector3(src.eulerAngles.x, y, src.eulerAngles.z);
	}
	public static void SetRotZ(this Transform src,float z)
	{
		src.eulerAngles = new Vector3(src.eulerAngles.x, src.eulerAngles.y, z);
	}
}
