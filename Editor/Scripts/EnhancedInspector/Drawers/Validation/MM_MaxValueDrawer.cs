using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_MaxValueAttribute.
    /// Clamps numeric values to maximum value.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_MaxValueAttribute))]
    public class MM_MaxValueDrawer : PropertyDrawer
    {
        #region Fields
        
        private MM_MaxValueAttribute MaxValue => (MM_MaxValueAttribute)attribute;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the property and clamps to maximum value
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
            MM_EditorUtility.ClampNumericProperty(property, null, MaxValue.MaxValue);
        }
        
        #endregion
    }
}
