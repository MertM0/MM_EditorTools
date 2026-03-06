using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Disables this field in the Inspector if the specified condition is true.
    /// </summary>
    /// <example>
    /// <code>
    /// public bool locked = false;
    /// 
    /// [MM_DisableIf("locked")]
    /// public int value = 10;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_DisableIfAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the boolean field/property to check
        /// </summary>
        public string ConditionName { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Disables field if condition is true
        /// </summary>
        /// <param name="conditionName">Name of the boolean field to check</param>
        public MM_DisableIfAttribute(string conditionName)
        {
            ConditionName = conditionName;
        }
        
        #endregion
    }
}
