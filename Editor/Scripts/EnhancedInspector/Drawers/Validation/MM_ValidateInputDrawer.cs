using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom drawer for MM_ValidateInput attribute.
    /// Validates field using a custom method.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_ValidateInputAttribute))]
    public class MM_ValidateInputDrawer : PropertyDrawer
    {
        #region PropertyDrawer Override
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MM_ValidateInputAttribute validateInput = attribute as MM_ValidateInputAttribute;
            
            // Draw property
            EditorGUI.PropertyField(position, property, label, true);
            
            // Validate
            if (!IsValid(property, validateInput))
            {
                Rect helpBoxRect = new Rect(position.x, position.y + EditorGUI.GetPropertyHeight(property, label, true) + 2, position.width, EditorGUIUtility.singleLineHeight * 2);
                EditorGUI.HelpBox(helpBoxRect, validateInput.ErrorMessage, MessageType.Error);
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            MM_ValidateInputAttribute validateInput = attribute as MM_ValidateInputAttribute;
            float height = EditorGUI.GetPropertyHeight(property, label, true);
            
            if (!IsValid(property, validateInput))
            {
                height += EditorGUIUtility.singleLineHeight * 2 + 4;
            }
            
            return height;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the field value
        /// </summary>
        private bool IsValid(SerializedProperty property, MM_ValidateInputAttribute validateInput)
        {
            object target = property.serializedObject.targetObject;
            MethodInfo method = target.GetType().GetMethod(validateInput.MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (method != null && method.ReturnType == typeof(bool))
            {
                object value = GetValue(property);
                return (bool)method.Invoke(target, new object[] { value });
            }
            
            return true;
        }
        
        /// <summary>
        /// Gets the value of the property
        /// </summary>
        private object GetValue(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return property.intValue;
                case SerializedPropertyType.Float:
                    return property.floatValue;
                case SerializedPropertyType.Boolean:
                    return property.boolValue;
                case SerializedPropertyType.String:
                    return property.stringValue;
                default:
                    return property.objectReferenceValue;
            }
        }
        
        #endregion
    }
}
