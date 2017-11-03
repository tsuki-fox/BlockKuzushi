using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class MessageVisualizer
{
	class Message
	{
		public string text;
		public Vector2 pos;
		public float elapsed;
	}

	static List<Message> _messages = new List<Message>();

	static float _extinctionDuration = 1f;

	static GameObject _instance = null;

	public static void CreateInstance()
	{
		_instance = new GameObject();
		_instance.AddComponent<MessageVisualizerInstance>();
		GameObject.DontDestroyOnLoad(_instance);
	}

	public static void Write(string text,Vector2 pos)
	{
		if (_instance == null)
			CreateInstance();

		Message msg = new Message();
		msg.text = text;
		msg.pos = Camera.main.WorldToScreenPoint(pos);
		msg.pos.y = Screen.height - msg.pos.y;
		msg.elapsed = 0f;

		_messages.Add(msg);
	}

	public static void Update(float delta)
	{
		_messages.ForEach(item => item.elapsed += delta);

		var rem = _messages.Where(item => item.elapsed > _extinctionDuration);
		_messages.RemoveAtList(rem.ToList());
	}

	public static void OnGUI()
	{
		foreach (var item in _messages)
		{
			Rect rect = new Rect(item.pos, new Vector2(100f, 25f));
			GUI.Label(rect, item.text);
		}
	}

	public class MessageVisualizerInstance : MonoBehaviour
	{
		private void Update()
		{
			MessageVisualizer.Update(Time.deltaTime);
		}

		private void OnGUI()
		{
			MessageVisualizer.OnGUI();
		}
	}
}
