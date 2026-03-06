using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Shows this field in the Inspector only if the specified condition is true.
    /// </summary>
    /// <example>
    /// <code>
    /// public bool enableDamage = true;
    /// 
    /// [MM_ShowIf("enableDamage")]
    /// public int damageAmount = 10;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_ShowIfAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the boolean field/property to check
        /// </summary>
        public string ConditionName { get; private set; }
        
        /// <summary>
        /// If true, inverts the condition
        /// </summary>
        public bool Invert { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Shows field only if condition is true
        /// </summary>
        /// <param name="conditionName">Name of the boolean field to check</param>
        /// <param name="invert">Invert the condition (default: false)</param>
        public MM_ShowIfAttribute(string conditionName, bool invert = false)
        {
            ConditionName = conditionName;
            Invert = invert;
        }
        
        #endregion
    }
}
