using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Draws a horizontal separator line in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// public int health = 100;
    /// 
    /// [MM_Separator]
    /// public float speed = 5f;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_SeparatorAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Height of the separator in pixels
        /// </summary>
        public float Height { get; private set; }
        
        /// <summary>
        /// Color of the separator line
        /// </summary>
        public Color Color { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a separator line
        /// </summary>
        /// <param name="height">Height in pixels (default: 1)</param>
        public MM_SeparatorAttribute(float height = 1f)
        {
            Height = height;
            Color = new Color(0.5f, 0.5f, 0.5f, 1f); // Gray
        }
        
        /// <summary>
        /// Creates a separator line with custom color
        /// </summary>
        /// <param name="height">Height in pixels</param>
        /// <param name="r">Red component (0-1)</param>
        /// <param name="g">Green component (0-1)</param>
        /// <param name="b">Blue component (0-1)</param>
        public MM_SeparatorAttribute(float height, float r, float g, float b)
        {
            Height = height;
            Color = new Color(r, g, b, 1f);
        }
        
        #endregion
    }
}
