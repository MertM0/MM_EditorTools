using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Utility class for common editor operations with performance-optimized caching.
    /// </summary>
    public static class MM_EditorUtility
    {
        #region Member Cache
        
        /// <summary>
        /// Cache for field and property lookups to avoid repeated reflection
        /// </summary>
        private static System.Collections.Generic.Dictionary<(System.Type type, string name), MemberInfo> _memberCache = 
            new System.Collections.Generic.Dictionary<(System.Type, string), MemberInfo>();
        
        #endregion
        
        #region Condition Evaluation
        
        /// <summary>
        /// Gets the boolean value of a field or property by name from a SerializedProperty's target object.
        /// Uses caching to avoid repeated reflection calls.
        /// </summary>
        /// <param name="property">The SerializedProperty containing the target object</param>
        /// <param name="conditionName">Name of the boolean field or property</param>
        /// <param name="defaultValue">Default value if condition is not found</param>
        /// <param name="warningPrefix">Prefix for warning message (e.g., "MM_ShowIf")</param>
        /// <returns>The boolean value of the condition, or defaultValue if not found</returns>
        public static bool GetBooleanConditionValue(SerializedProperty property, string conditionName, bool defaultValue, string warningPrefix = "MM_Condition")
        {
            object targetObject = property.serializedObject.targetObject;
            System.Type targetType = targetObject.GetType();
            var cacheKey = (targetType, conditionName);
            
            // Try to get from cache
            if (!_memberCache.TryGetValue(cacheKey, out var memberInfo))
            {
                // Try to find field
                FieldInfo field = targetType.GetField(conditionName, 
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                
                if (field != null && field.FieldType == typeof(bool))
                {
                    memberInfo = field;
                    _memberCache[cacheKey] = field;
                }
                else
                {
                    // Try to find property
                    PropertyInfo propertyInfo = targetType.GetProperty(conditionName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    
                    if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
                    {
                        memberInfo = propertyInfo;
                        _memberCache[cacheKey] = propertyInfo;
                    }
                    else
                    {
                        Debug.LogWarning($"[{warningPrefix}] Condition '{conditionName}' not found or is not a boolean!");
                        return defaultValue;
                    }
                }
            }
            
            // Get value using cached member info
            if (memberInfo is FieldInfo fieldInfo)
                return (bool)fieldInfo.GetValue(targetObject);
            else if (memberInfo is PropertyInfo propInfo)
                return (bool)propInfo.GetValue(targetObject);
            
            return defaultValue;
        }
        
        #endregion
        
        #region Numeric Clamping
        
        /// <summary>
        /// Clamps a numeric property to minimum and/or maximum values.
        /// Supports int and float types.
        /// </summary>
        public static void ClampNumericProperty(SerializedProperty property, float? minValue = null, float? maxValue = null)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    int intVal = property.intValue;
                    if (minValue.HasValue && intVal < minValue.Value)
                        property.intValue = Mathf.RoundToInt(minValue.Value);
                    if (maxValue.HasValue && intVal > maxValue.Value)
                        property.intValue = Mathf.RoundToInt(maxValue.Value);
                    break;
                    
                case SerializedPropertyType.Float:
                    float floatVal = property.floatValue;
                    if (minValue.HasValue && floatVal < minValue.Value)
                        property.floatValue = minValue.Value;
                    if (maxValue.HasValue && floatVal > maxValue.Value)
                        property.floatValue = maxValue.Value;
                    break;
            }
        }
        
        #endregion
    }
}
