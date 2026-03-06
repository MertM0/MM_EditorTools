using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_TagAttribute.
    /// Shows a tag dropdown for string fields.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_TagAttribute))]
    public class MM_TagDrawer : PropertyDrawer
    {
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the tag dropdown
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use MM_Tag with string fields only.");
            }
        }
        
        #endregion
    }
}
