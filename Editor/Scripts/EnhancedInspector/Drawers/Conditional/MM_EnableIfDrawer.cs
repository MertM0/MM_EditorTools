using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_EnableIfAttribute.
    /// Enables/disables fields based on boolean conditions.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_EnableIfAttribute))]
    public class MM_EnableIfDrawer : PropertyDrawer
    {
        #region Fields
        
        private MM_EnableIfAttribute EnableIf => (MM_EnableIfAttribute)attribute;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the property with enabled state based on condition
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool shouldEnable = ShouldEnable(property);
            
            GUI.enabled = shouldEnable;
            EditorGUI.PropertyField(position, property, label, true);
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
        
        #region Private Methods
        
        /// <summary>
        /// Checks if the field should be enabled
        /// </summary>
        private bool ShouldEnable(SerializedProperty property)
        {
            return MM_EditorUtility.GetBooleanConditionValue(property, EnableIf.ConditionName, true, "MM_EnableIf");
        }
        
        #endregion
    }
}
