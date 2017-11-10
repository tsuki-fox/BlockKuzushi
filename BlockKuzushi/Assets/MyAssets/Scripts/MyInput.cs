using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyInput
{
	public static bool left { get { return Input.GetKey(KeyCode.LeftArrow); } }
	public static bool right { get { return Input.GetKey(KeyCode.RightArrow); } }
	public static bool up { get { return Input.GetKey(KeyCode.UpArrow); } }
	public static bool down { get { return Input.GetKey(KeyCode.DownArrow); } }

	public static bool leftRot { get { return Input.GetKey(KeyCode.A); } }
	public static bool rightRot { get { return Input.GetKey(KeyCode.D); } }
}
