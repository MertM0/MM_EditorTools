using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_DisableIfAttribute.
    /// Disables fields based on boolean conditions.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_DisableIfAttribute))]
    public class MM_DisableIfDrawer : PropertyDrawer
    {
        #region Fields
        
        private MM_DisableIfAttribute DisableIf => (MM_DisableIfAttribute)attribute;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the property with disabled state if condition is true
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool shouldDisable = ShouldDisable(property);
            
            GUI.enabled = !shouldDisable;
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
        /// Checks if the field should be disabled
        /// </summary>
        private bool ShouldDisable(SerializedProperty property)
        {
            return MM_EditorUtility.GetBooleanConditionValue(property, DisableIf.ConditionName, false, "MM_DisableIf");
        }
        
        #endregion
    }
}
