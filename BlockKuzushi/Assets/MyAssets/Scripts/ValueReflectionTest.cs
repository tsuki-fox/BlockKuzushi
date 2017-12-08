using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.All)]
public class Attachable : Attribute
{
}

public class ValueReflectionTest : MonoBehaviour
{
	[SerializeField, Header("結果")]
	string _result;

	[SerializeField, Header("インスタンス")]
	GameObject _instance;

	[SerializeField, Header("コンポーネント")]
	List<Component> _components;

	[SerializeField, Header("コンポーネントインデックス")]
	int _componentIndex = 0;

	[SerializeField, Header("メンバー")]
	List<string> _members;

	void CorrectComponents()
	{
		_components = _instance.GetComponents<Component>().ToList();

		Debug.LogFormat("corrected {0} components", _components.Count());
	}
	void CorrectMembers()
	{
		
		_members = _components[_componentIndex].GetType().GetMembers(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance)
			.Where(item => Attribute.GetCustomAttribute(item,typeof(Attachable))!=null)
			.Select(item => item.Name).ToList();
	}

	void OnValidate()
	{
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(ValueReflectionTest))]
	public class ValueReflectionTestInspector : Editor
	{
		int _selectedCmpIndex = 0;
		int _selectedMemberIndex = 0;

		List<FieldInfo> _selectedMembers;

		GameObject _gameObject;
		Component _selectedComponent = null;
		object _selectedField = null;
		int _selectedFieldIndex = 0;
		Type _selectedType;

		public override void OnInspectorGUI()
		{
			_gameObject = (GameObject)EditorGUILayout.ObjectField("gameobject", _gameObject, typeof(GameObject), true);
			_selectedComponent = MyEditorGUILayout.ComponentsPopupFromGameObject(_selectedComponent, _gameObject);
			MyEditorGUILayout.FieldsPopupFromComponent(ref _selectedFieldIndex, ref _selectedField, _selectedComponent, null, typeof(Attachable));

			//EditorGUILayout.LabelField("value from selected field:" + ((float)_selectedField).ToString());

			/*
			var self = target as ValueReflectionTest;
			if (GUILayout.Button("Correct Components"))
				self.CorrectComponents();
			if (GUILayout.Button("Correct Members"))
				self.CorrectMembers();

			_selectedCmpIndex = EditorGUILayout.Popup(_selectedCmpIndex, self._components.Select(item => item.GetType().Name).ToArray());
			_selectedMembers = self._components[_selectedCmpIndex].GetType().GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(item => Attribute.GetCustomAttribute(item, typeof(Attachable)) != null).Cast<FieldInfo>().ToList();
			_selectedMemberIndex = EditorGUILayout.Popup(_selectedMemberIndex, _selectedMembers.Select(item => item.Name).ToArray());
			var fieldName = _selectedMembers[_selectedMemberIndex].Name;
			Debug.LogFormat("field name:{0}", fieldName);
			var selectedComponent = self._components[_selectedCmpIndex];
			var field = selectedComponent.GetType().GetField(fieldName,BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance);
			var value = (float)field.GetValue(selectedComponent);

			self._result = value.ToString();
			*/
			EditorGUILayout.Separator();
			base.OnInspectorGUI();
			
		}
	}
#endif
}
