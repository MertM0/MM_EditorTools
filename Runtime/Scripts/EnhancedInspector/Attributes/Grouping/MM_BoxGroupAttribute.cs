using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Groups properties inside a styled box in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_BoxGroup("Player Stats")]
    /// public int health = 100;
    /// 
    /// [MM_BoxGroup("Player Stats")]
    /// public float speed = 5f;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_BoxGroupAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the group
        /// </summary>
        public string GroupName { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a box group
        /// </summary>
        /// <param name="groupName">Name displayed at the top of the box</param>
        public MM_BoxGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }
        
        #endregion
    }
}
