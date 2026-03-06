using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays a custom slider for numeric values.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_Slider(0, 100)]
    /// public float volume = 50f;
    /// 
    /// [MM_Slider(1, 10)]
    /// public int level = 1;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_SliderAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Minimum slider value
        /// </summary>
        public float MinValue { get; private set; }
        
        /// <summary>
        /// Maximum slider value
        /// </summary>
        public float MaxValue { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a slider
        /// </summary>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        public MM_SliderAttribute(float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
        
        #endregion
    }
}
