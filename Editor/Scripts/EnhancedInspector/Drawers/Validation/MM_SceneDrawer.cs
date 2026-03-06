using UnityEditor;
using UnityEngine;
using System.Linq;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom drawer for MM_Scene attribute.
    /// Provides a scene picker dropdown.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_SceneAttribute))]
    public class MM_SceneDrawer : PropertyDrawer
    {
        #region PropertyDrawer Override
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            // Get all scenes in build settings
            var scenes = EditorBuildSettings.scenes;
            string[] sceneNames = scenes.Select(s => System.IO.Path.GetFileNameWithoutExtension(s.path)).ToArray();
            
            if (property.propertyType == SerializedPropertyType.String)
            {
                // String field - show scene name
                int currentIndex = System.Array.IndexOf(sceneNames, property.stringValue);
                if (currentIndex == -1) currentIndex = 0;
                
                int newIndex = EditorGUI.Popup(position, label.text, currentIndex, sceneNames);
                
                if (newIndex >= 0 && newIndex < sceneNames.Length)
                {
                    property.stringValue = sceneNames[newIndex];
                }
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                // Int field - show scene index
                int currentIndex = property.intValue;
                if (currentIndex < 0 || currentIndex >= sceneNames.Length) currentIndex = 0;
                
                int newIndex = EditorGUI.Popup(position, label.text, currentIndex, sceneNames);
                property.intValue = newIndex;
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
            
            EditorGUI.EndProperty();
        }
        
        #endregion
    }
}
