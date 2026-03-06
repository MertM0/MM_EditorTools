using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_LayerAttribute.
    /// Shows a layer dropdown for integer fields.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_LayerAttribute))]
    public class MM_LayerDrawer : PropertyDrawer
    {
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the layer dropdown
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUI.BeginProperty(position, label, property);
                property.intValue = EditorGUI.LayerField(position, label, property.intValue);
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use MM_Layer with int fields only.");
            }
        }
        
        #endregion
    }
}
