using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MovementSequence : MonoBehaviour
{
	[System.Serializable]
	public class Waypoint
	{
		public Vector3 _point;
	}
	[System.Serializable]
	public class Link
	{
		public Waypoint _start;
		public Waypoint _end;
	}

	[SerializeField]
	List<Waypoint> _waypoints = new List<Waypoint>();
	[SerializeField]
	List<Link> _links = new List<Link>();

#if UNITY_EDITOR
	[CustomEditor(typeof(MovementSequence))]
	public class MovementSequenceInspector:Editor
	{
		void Awake()
		{
			EditorApplication.update += SceneView.RepaintAll;
		}

		Vector2 WorldToSceneViewPoint(Vector3 point)
		{
			var pos = SceneView.currentDrawingSceneView.camera.WorldToScreenPoint(point);
			return new Vector2(pos.x, SceneView.currentDrawingSceneView.position.height - pos.y);
		}

		void OnSceneGUI()
		{
			var self = target as MovementSequence;
			var sceneCamera = SceneView.currentDrawingSceneView.camera;

			Handles.BeginGUI();

			foreach(var point in self._waypoints)
			{
				var pos = sceneCamera.WorldToScreenPoint(point._point);
				var buttonRect = new Rect(pos.x, SceneView.currentDrawingSceneView.position.height - pos.y, 75, 25);
				GUI.Button(buttonRect, "point");
			}

			foreach(var link in self._links)
			{
				var startPoint = WorldToSceneViewPoint(link._start._point);
				var endPoint = WorldToSceneViewPoint(link._end._point);
				Handles.DrawLine(startPoint, endPoint);
			}

			Handles.EndGUI();
		}

		public override void OnInspectorGUI()
		{
			var self = target as MovementSequence;

			if (GUILayout.Button("Clear"))
			{
				self._waypoints.Clear();
			}

			if(GUILayout.Button("Add waypoint"))
			{
				var point = new Waypoint();
				point._point = RandomEx.RangeVector2(-Vector2.one, Vector2.one);
				self._waypoints.Add(point);
			}

			base.OnInspectorGUI();
		}
	}
#endif
}
