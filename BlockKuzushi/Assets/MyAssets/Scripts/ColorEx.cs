using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorEx
{
	public static void SetRed(this Color self,float r)
	{
		self = new Color(r, self.g, self.b, self.a);
	}

	public static void SetBlue(this Color self,float b)
	{
		self = new Color(self.r, self.g, b, self.a);
	}
}
