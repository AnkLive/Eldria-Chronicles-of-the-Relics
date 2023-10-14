// using UnityEditor;
// using UnityEngine;
//
// namespace Editor
// {
//     [CustomPropertyDrawer(typeof(GlobalStringVars))]
//     public class GlobalStringVarsDrawer : PropertyDrawer
//     {
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             EditorGUI.BeginProperty(position, label, property);
//
//             SerializedProperty itemDescriptions = property.FindPropertyRelative("StringVars");
//
//             if (itemDescriptions != null)
//             {
//                 Rect listRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight * itemDescriptions.arraySize);
//                 EditorGUI.PropertyField(listRect, itemDescriptions);
//             }
//
//             EditorGUI.EndProperty();
//         }
//
//         public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//         {
//             SerializedProperty itemDescriptions = property.FindPropertyRelative("StringVars");
//
//             if (itemDescriptions != null)
//             {
//                 return EditorGUIUtility.singleLineHeight * (itemDescriptions.arraySize + 1);
//             }
//
//             return EditorGUIUtility.singleLineHeight * 2; // Fallback height
//         }
//     }
// }