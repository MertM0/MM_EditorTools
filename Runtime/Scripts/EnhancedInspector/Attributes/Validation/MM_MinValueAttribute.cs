using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Clamps numeric values to a minimum value.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_MinValue(0)]
    /// public int health = 100; // Cannot go below 0
    /// 
    /// [MM_MinValue(0.1f)]
    /// public float speed = 5f; // Cannot go below 0.1
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_MinValueAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Minimum allowed value
        /// </summary>
        public float MinValue { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Sets minimum value constraint
        /// </summary>
        /// <param name="minValue">Minimum allowed value</param>
        public MM_MinValueAttribute(float minValue)
        {
            MinValue = minValue;
        }
        
        #endregion
    }
}
