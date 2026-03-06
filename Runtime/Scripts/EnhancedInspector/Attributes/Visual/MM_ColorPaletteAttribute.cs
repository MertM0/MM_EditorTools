using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays a color picker with predefined palette.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_ColorPalette]
    /// public Color teamColor = Color.white;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_ColorPaletteAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Predefined colors in the palette
        /// </summary>
        public Color[] PaletteColors { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a color palette
        /// </summary>
        public MM_ColorPaletteAttribute()
        {
            // Default palette with full alpha
            PaletteColors = new Color[]
            {
                new Color(1f, 0f, 0f, 1f),      // Red
                new Color(0f, 1f, 0f, 1f),      // Green
                new Color(0f, 0f, 1f, 1f),      // Blue
                new Color(1f, 1f, 0f, 1f),      // Yellow
                new Color(0f, 1f, 1f, 1f),      // Cyan
                new Color(1f, 0f, 1f, 1f),      // Magenta
                new Color(1f, 1f, 1f, 1f),      // White
                new Color(0f, 0f, 0f, 1f),      // Black
                new Color(0.5f, 0.5f, 0.5f, 1f) // Gray
            };
        }
        
        /// <summary>
        /// Creates a color palette with custom colors
        /// </summary>
        /// <param name="colors">Array of custom colors</param>
        public MM_ColorPaletteAttribute(params Color[] colors)
        {
            PaletteColors = colors;
        }
        
        #endregion
    }
}
