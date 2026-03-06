using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Clamps numeric values to a maximum value.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_MaxValue(100)]
    /// public int health = 50; // Cannot exceed 100
    /// 
    /// [MM_MaxValue(10f)]
    /// public float speed = 5f; // Cannot exceed 10
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_MaxValueAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Maximum allowed value
        /// </summary>
        public float MaxValue { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Sets maximum value constraint
        /// </summary>
        /// <param name="maxValue">Maximum allowed value</param>
        public MM_MaxValueAttribute(float maxValue)
        {
            MaxValue = maxValue;
        }
        
        #endregion
    }
}
