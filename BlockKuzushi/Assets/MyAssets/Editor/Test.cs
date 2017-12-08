using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class Test
{
	public bool FormatCheck(string text)
	{
		const int Proc = 0;
		const int Ready = 1;

		int state = Ready;
		string num = "";
		int prev = -1;
		foreach (var c in text)
		{
			if (state == Ready)
			{
				if (c == '{')
					state = Proc;
				else if (c == '}')
					return false;
			}
			else if (state == Proc)
			{
				if (c == '{')
					return false;
				if (c == '}')
				{
					int v = 0;
					if (num == "")
						return false;
					if (!int.TryParse(num, out v))
						return false;
					if (v != prev + 1)
					{
						Debug.LogFormat("v:{0} prev+1:{1}", v, prev + 1);
						return false;
					}
					prev = v;
					state = Ready;
					num = "";
				}
				else
					num += c;
			}
		}

		return true;
	}

	[Test]
	public void FormatCheckTest()
	{
		List<KeyValuePair<bool, string>> check = new List<KeyValuePair<bool, string>>();
		
		check.Add(new KeyValuePair<bool, string>(true, "{0}{1}{2}"));
		check.Add(new KeyValuePair<bool, string>(true, "{00}{01}{02}"));
		check.Add(new KeyValuePair<bool, string>(false,"{0}{{1}{2}"));
		check.Add(new KeyValuePair<bool, string>(false, "{0}{a}{3}"));
		check.Add(new KeyValuePair<bool,string>(false, "{{0}}{1}{2}"));
		check.Add(new KeyValuePair<bool, string>(false,"}{1}{2}"));
		check.Add(new KeyValuePair<bool, string>(false, "{ } {2}"));

		foreach(var item in check)
		{
			Assert.That(item.Key == FormatCheck(item.Value), "{0}", item.Value);
		}
	}
}
