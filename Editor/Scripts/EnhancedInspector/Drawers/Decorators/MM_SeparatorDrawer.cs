using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_SeparatorAttribute.
    /// Draws a horizontal line separator in the Inspector.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_SeparatorAttribute))]
    public class MM_SeparatorDrawer : DecoratorDrawer
    {
        #region Fields
        
        private MM_SeparatorAttribute Separator => (MM_SeparatorAttribute)attribute;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the separator line - Odin Inspector style
        /// </summary>
        public override void OnGUI(Rect position)
        {
            // Add some space above
            position.y += 8;
            
            // Odin-style gradient separator
            float lineWidth = position.width;
            float lineHeight = Separator.Height;
            
            for (int i = 0; i < lineWidth; i++)
            {
                float t = i / lineWidth;
                float alpha = 1f - Mathf.Abs(t - 0.5f) * 1.8f; // Stronger fade at edges
                alpha = Mathf.Clamp01(alpha) * Separator.Color.a;
                
                Color pixelColor = new Color(Separator.Color.r, Separator.Color.g, Separator.Color.b, alpha);
                Rect pixelRect = new Rect(position.x + i, position.y, 1, lineHeight);
                EditorGUI.DrawRect(pixelRect, pixelColor);
            }
        }
        
        /// <summary>
        /// Get the height of the separator
        /// </summary>
        public override float GetHeight()
        {
            return Separator.Height + 16; // Add padding
        }
        
        #endregion
    }
}
