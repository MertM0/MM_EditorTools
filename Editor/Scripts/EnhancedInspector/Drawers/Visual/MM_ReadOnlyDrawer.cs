using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_ReadOnlyAttribute.
    /// Displays the field as disabled (grayed out) in the Inspector.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_ReadOnlyAttribute))]
    public class MM_ReadOnlyDrawer : PropertyDrawer
    {
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the property in the Inspector
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Disable GUI
            GUI.enabled = false;
            
            // Draw the property
            EditorGUI.PropertyField(position, property, label, true);
            
            // Re-enable GUI
            GUI.enabled = true;
        }
        
        /// <summary>
        /// Get the height of the property
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        
        #endregion
    }
}
