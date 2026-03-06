using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays a progress bar for numeric fields in the Inspector.
    /// Shows current value as a filled bar with optional label.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_ProgressBar(0, 100)]
    /// public float health = 75f;
    /// 
    /// [MM_ProgressBar(0, 1, "Loading")]
    /// public float loadProgress = 0.5f;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_ProgressBarAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Minimum value of the progress bar
        /// </summary>
        public float MinValue { get; private set; }
        
        /// <summary>
        /// Maximum value of the progress bar
        /// </summary>
        public float MaxValue { get; private set; }
        
        /// <summary>
        /// Custom label for the progress bar
        /// </summary>
        public string Label { get; private set; }
        
        /// <summary>
        /// Color of the progress bar
        /// </summary>
        public Color Color { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a progress bar
        /// </summary>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <param name="label">Custom label (optional)</param>
        public MM_ProgressBarAttribute(float minValue, float maxValue, string label = "")
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Label = label;
            Color = new Color(0.2f, 0.8f, 0.2f, 1f); // Green by default
        }
        
        /// <summary>
        /// Creates a progress bar with custom color
        /// </summary>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <param name="r">Red component (0-1)</param>
        /// <param name="g">Green component (0-1)</param>
        /// <param name="b">Blue component (0-1)</param>
        public MM_ProgressBarAttribute(float minValue, float maxValue, float r, float g, float b)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Label = "";
            Color = new Color(r, g, b, 1f);
        }
        
        #endregion
    }
}
