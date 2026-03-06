using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Shows a preview thumbnail for asset fields (sprites, textures, etc.).
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_Preview(64, 64)]
    /// public Sprite icon;
    /// 
    /// [MM_Preview]
    /// public Texture2D texture;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_PreviewAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Width of the preview
        /// </summary>
        public float Width { get; private set; }
        
        /// <summary>
        /// Height of the preview
        /// </summary>
        public float Height { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a preview with specified size
        /// </summary>
        /// <param name="width">Width of the preview (default: 64)</param>
        /// <param name="height">Height of the preview (default: 64)</param>
        public MM_PreviewAttribute(float width = 64f, float height = 64f)
        {
            Width = width;
            Height = height;
        }
        
        #endregion
    }
}
