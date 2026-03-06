using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Enables this field in the Inspector only if the specified condition is true.
    /// </summary>
    /// <example>
    /// <code>
    /// public bool canModify = true;
    /// 
    /// [MM_EnableIf("canModify")]
    /// public int value = 10;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_EnableIfAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the boolean field/property to check
        /// </summary>
        public string ConditionName { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Enables field only if condition is true
        /// </summary>
        /// <param name="conditionName">Name of the boolean field to check</param>
        public MM_EnableIfAttribute(string conditionName)
        {
            ConditionName = conditionName;
        }
        
        #endregion
    }
}
