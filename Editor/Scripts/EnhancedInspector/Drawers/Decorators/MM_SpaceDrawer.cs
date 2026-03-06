using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_SpaceAttribute.
    /// Adds vertical space before a field.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_SpaceAttribute))]
    public class MM_SpaceDrawer : DecoratorDrawer
    {
        #region Fields
        
        private MM_SpaceAttribute Space => (MM_SpaceAttribute)attribute;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws empty space
        /// </summary>
        public override void OnGUI(Rect position)
        {
            // No visual drawing needed, just creates space
        }
        
        /// <summary>
        /// Returns the height of the space
        /// </summary>
        public override float GetHeight()
        {
            // Skip decorator if flag is set
            if (MM_EnhancedInspector.SkipDecorators)
                return 0f;
            
            return Space.Height;
        }
        
        #endregion
    }
}
