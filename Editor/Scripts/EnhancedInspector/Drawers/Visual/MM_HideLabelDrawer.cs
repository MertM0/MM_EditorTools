using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_HideLabelAttribute.
    /// Hides the field label.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_HideLabelAttribute))]
    public class MM_HideLabelDrawer : PropertyDrawer
    {
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the property without label
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }
        
        #endregion
    }
}
