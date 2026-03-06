using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Groups properties inside a collapsible foldout in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_FoldoutGroup("Advanced Settings")]
    /// public bool debugMode = false;
    /// 
    /// [MM_FoldoutGroup("Advanced Settings")]
    /// public int maxIterations = 100;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_FoldoutGroupAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the foldout group
        /// </summary>
        public string GroupName { get; private set; }
        
        /// <summary>
        /// Whether the foldout starts expanded
        /// </summary>
        public bool DefaultExpanded { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a foldout group
        /// </summary>
        /// <param name="groupName">Name displayed in the foldout</param>
        /// <param name="defaultExpanded">Whether the foldout starts expanded (default: false)</param>
        public MM_FoldoutGroupAttribute(string groupName, bool defaultExpanded = false)
        {
            GroupName = groupName;
            DefaultExpanded = defaultExpanded;
        }
        
        #endregion
    }
}
