using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays a min-max slider for Vector2 values.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_MinMaxSlider(0, 100)]
    /// public Vector2 damageRange = new Vector2(10, 20);
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_MinMaxSliderAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Minimum slider value
        /// </summary>
        public float MinLimit { get; private set; }
        
        /// <summary>
        /// Maximum slider value
        /// </summary>
        public float MaxLimit { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a min-max slider
        /// </summary>
        /// <param name="minLimit">Minimum limit</param>
        /// <param name="maxLimit">Maximum limit</param>
        public MM_MinMaxSliderAttribute(float minLimit, float maxLimit)
        {
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }
        
        #endregion
    }
}
