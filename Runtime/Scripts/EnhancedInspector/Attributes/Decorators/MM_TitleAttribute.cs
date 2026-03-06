using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Draws a title header above a field in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_Title("Combat Settings")]
    /// public int damage = 10;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_TitleAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// The title text to display
        /// </summary>
        public string Title { get; private set; }
        
        /// <summary>
        /// Font size of the title
        /// </summary>
        public int FontSize { get; private set; }
        
        /// <summary>
        /// Whether to draw a line under the title
        /// </summary>
        public bool DrawLine { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a title header
        /// </summary>
        /// <param name="title">Title text</param>
        /// <param name="fontSize">Font size (default: 14)</param>
        /// <param name="drawLine">Draw line under title (default: true)</param>
        public MM_TitleAttribute(string title, int fontSize = 14, bool drawLine = true)
        {
            Title = title;
            FontSize = fontSize;
            DrawLine = drawLine;
        }
        
        #endregion
    }
}
