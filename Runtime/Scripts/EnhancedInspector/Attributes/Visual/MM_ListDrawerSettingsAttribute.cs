using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Customizes how lists/arrays are displayed in the Inspector (Odin-style).
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_ListDrawerSettings(ShowIndexLabels = true, DraggableItems = true)]
    /// public List&lt;GameObject&gt; enemyPrefabs;
    /// 
    /// [MM_ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    /// public int[] fixedArray = new int[5];
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_ListDrawerSettingsAttribute : PropertyAttribute
    {
        #region Properties
        
        /// <summary>
        /// Shows index labels (0, 1, 2...) for each element
        /// </summary>
        public bool ShowIndexLabels { get; set; } = true;
        
        /// <summary>
        /// Enables drag handles for reordering items
        /// </summary>
        public bool DraggableItems { get; set; } = true;
        
        /// <summary>
        /// Hides the add (+) button at the bottom
        /// </summary>
        public bool HideAddButton { get; set; } = false;
        
        /// <summary>
        /// Hides the remove (x) button for each item
        /// </summary>
        public bool HideRemoveButton { get; set; } = false;
        
        /// <summary>
        /// Shows the list count at the top
        /// </summary>
        public bool ShowItemCount { get; set; } = true;
        
        /// <summary>
        /// Expands all items by default
        /// </summary>
        public bool Expanded { get; set; } = true;
        
        /// <summary>
        /// Uses compact mode (less spacing)
        /// </summary>
        public bool CompactMode { get; set; } = false;
        
        /// <summary>
        /// Custom label for add button
        /// </summary>
        public string AddButtonLabel { get; set; } = null;
        
        /// <summary>
        /// Custom label for list header
        /// </summary>
        public string ListLabel { get; set; } = null;
        
        #endregion
        
        #region Constructor
        
        public MM_ListDrawerSettingsAttribute()
        {
        }
        
        #endregion
    }
}
