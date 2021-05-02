﻿using Monitor;
using UnityEngine;
using UnityEditor;

namespace Editor {

	
	[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
	public class NamedArrayDrawer : PropertyDrawer {
		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {
			try {
				int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
				EditorGUI.ObjectField(rect, property, new GUIContent(((NamedArrayAttribute) attribute).Names[pos]));
			}
			catch {
				EditorGUI.ObjectField(rect, property, label);
			}
		}
	}

}