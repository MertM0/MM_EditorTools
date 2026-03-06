using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Hides this field in the Inspector if the specified condition is true.
    /// </summary>
    /// <example>
    /// <code>
    /// public bool disableDamage = false;
    /// 
    /// [MM_HideIf("disableDamage")]
    /// public int damageAmount = 10;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_HideIfAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the boolean field/property to check
        /// </summary>
        public string ConditionName { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Hides field if condition is true
        /// </summary>
        /// <param name="conditionName">Name of the boolean field to check</param>
        public MM_HideIfAttribute(string conditionName)
        {
            ConditionName = conditionName;
        }
        
        #endregion
    }
}
