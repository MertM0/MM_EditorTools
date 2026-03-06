using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom drawer for MM_ShowIfEnum attribute.
    /// Shows field only if enum matches specified value.
    /// Uses field caching for better performance.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_ShowIfEnumAttribute))]
    public class MM_ShowIfEnumDrawer : PropertyDrawer
    {
        #region Fields
        
        /// <summary>
        /// Cached field info to avoid repeated reflection lookups
        /// </summary>
        private FieldInfo _cachedEnumField;
        
        #endregion
        
        #region PropertyDrawer Override
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MM_ShowIfEnumAttribute showIfEnum = attribute as MM_ShowIfEnumAttribute;
            
            if (ShouldShow(property, showIfEnum))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            MM_ShowIfEnumAttribute showIfEnum = attribute as MM_ShowIfEnumAttribute;
            
            if (ShouldShow(property, showIfEnum))
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
            
            return 0f;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Checks if field should be shown with cached field lookup
        /// </summary>
        private bool ShouldShow(SerializedProperty property, MM_ShowIfEnumAttribute showIfEnum)
        {
            object target = property.serializedObject.targetObject;
            
            // Initialize cache on first use
            if (_cachedEnumField == null)
            {
                _cachedEnumField = target.GetType().GetField(showIfEnum.EnumFieldName, 
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                
                if (_cachedEnumField == null)
                {
                    Debug.LogWarning($"[MM_ShowIfEnum] Enum field '{showIfEnum.EnumFieldName}' not found!");
                    return true;
                }
            }
            
            object currentValue = _cachedEnumField.GetValue(target);
            return currentValue.Equals(showIfEnum.EnumValue);
        }
        
        #endregion
    }
}
