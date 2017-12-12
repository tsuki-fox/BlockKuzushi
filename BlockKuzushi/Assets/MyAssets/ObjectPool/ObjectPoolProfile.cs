using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewProfile")]
public class ObjectPoolProfile : ScriptableObject
{
	[System.Serializable]
	public class ObjectProfile
	{
		public Texture2D _icon;
		public GameObject _prefab;
		public int _reserveAmount;

#if UNITY_EDITOR
		[CustomPropertyDrawer(typeof(ObjectProfile))]
		public class ObjectProfileDrawer : PropertyDrawer
		{
			public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUI.BeginProperty(position, label, property);

				var prefabProp = property.FindPropertyRelative("_prefab");
				var amountProp = property.FindPropertyRelative("_reserveAmount");
				var icon = (Texture2D)property.FindPropertyRelative("_icon").objectReferenceValue;

				var iconRect = new Rect(position)
				{
					width = 32,
					height = 32
				};
				var prefabRect = new Rect(position)
				{
					x = position.x + iconRect.width,
					width = position.width - iconRect.width,
					height = position.height * 0.5f
				};
				var amountRect = new Rect(prefabRect)
				{
					x = position.x + iconRect.width,
					width = prefabRect.width,
					y = prefabRect.y + prefabRect.height
				};

				if (icon != null)
					EditorGUI.DrawTextureTransparent(iconRect, icon);
				else
					EditorGUI.LabelField(iconRect, "NULL");
				prefabProp.objectReferenceValue = EditorGUI.ObjectField(prefabRect, "Prefab", prefabProp.objectReferenceValue, typeof(GameObject), false);
				EditorGUI.BeginDisabledGroup(prefabProp.objectReferenceValue == null);
				EditorGUI.PropertyField(amountRect, amountProp);
				EditorGUI.EndDisabledGroup();

				EditorGUI.EndProperty();
				if (EditorGUI.EndChangeCheck())
				{
					if (prefabProp.objectReferenceValue != null)
						property.FindPropertyRelative("_icon").objectReferenceValue = AssetPreview.GetMiniThumbnail(prefabProp.objectReferenceValue);
					else
						property.FindPropertyRelative("_icon").objectReferenceValue = null;
				}
			}

			public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
			{
				return base.GetPropertyHeight(property, label) * 2f;
			}
		}
#endif
	}

	static List<ObjectPoolProfile> _entities = new List<ObjectPoolProfile>();
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static void ReserveOnLoad()
	{
		foreach (var entity in _entities)
		{
			foreach (var prof in entity._profiles)
			{
				if (prof._prefab != null)
					ObjectPool.Reserve(prof._prefab, prof._reserveAmount);
			}
		}
	}


	[SerializeField]
	List<ObjectProfile> _profiles = new List<ObjectProfile>();

	void OnEnable()
	{
		_entities.Add(this);
	}

	public void AddProfile()
	{
		_profiles.Add(new ObjectProfile());
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(ObjectPoolProfile))]
	public class ObjectPoolProfileInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			var self = target as ObjectPoolProfile;
			serializedObject.Update();

			//追加ボタン
			if (GUILayout.Button("Add Profile"))
				self.AddProfile();

			//profile表示
			var remIndexes = new List<int>();
			var profiles = serializedObject.FindProperty("_profiles");
			for (var i = 0; i < profiles.arraySize; i++)
			{
				var prof = profiles.GetArrayElementAtIndex(i);

				EditorGUILayout.BeginHorizontal("box");
				EditorGUILayout.PropertyField(prof);
				MyGUIUtil.BeginBackgroundColorChange(Color.red);
				if (GUILayout.Button("X", GUILayout.Width(16), GUILayout.Height(32)))
					remIndexes.Add(i);
				MyGUIUtil.EndBackgroundColorChange();
				EditorGUILayout.EndHorizontal();
			}

			foreach (var index in remIndexes)
				profiles.DeleteArrayElementAtIndex(index);

			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}