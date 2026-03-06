using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_MinValueAttribute.
    /// Clamps numeric values to minimum value.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_MinValueAttribute))]
    public class MM_MinValueDrawer : PropertyDrawer
    {
        #region Fields
        
        private MM_MinValueAttribute MinValue => (MM_MinValueAttribute)attribute;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the property and clamps to minimum value
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
            MM_EditorUtility.ClampNumericProperty(property, MinValue.MinValue);
        }
        
        #endregion
    }
}
