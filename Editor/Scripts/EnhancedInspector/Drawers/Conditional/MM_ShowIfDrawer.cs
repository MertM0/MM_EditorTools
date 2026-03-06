using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_ShowIfAttribute.
    /// Shows/hides fields based on boolean conditions.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_ShowIfAttribute))]
    public class MM_ShowIfDrawer : PropertyDrawer
    {
        #region Fields
        
        private MM_ShowIfAttribute ShowIf => (MM_ShowIfAttribute)attribute;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the property if condition is met
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (ShouldShow(property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
        
        /// <summary>
        /// Returns height based on visibility
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (ShouldShow(property))
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
            
            return -EditorGUIUtility.standardVerticalSpacing; // Hide completely
        }
        
        #endregion
        
        #region Private Methods
        
        /// <summary>
        /// Checks if the field should be shown
        /// </summary>
        private bool ShouldShow(SerializedProperty property)
        {
            bool conditionMet = MM_EditorUtility.GetBooleanConditionValue(property, ShowIf.ConditionName, true, "MM_ShowIf");
            return ShowIf.Invert ? !conditionMet : conditionMet;
        }
        
        #endregion
    }
}
