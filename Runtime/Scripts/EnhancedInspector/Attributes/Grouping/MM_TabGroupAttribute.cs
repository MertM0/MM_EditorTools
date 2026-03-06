using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Groups properties inside tabbed interface in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_TabGroup("Stats", "Health")]
    /// public int health = 100;
    /// 
    /// [MM_TabGroup("Stats", "Mana")]
    /// public float mana = 50f;
    /// 
    /// [MM_TabGroup("Stats", "Health")]
    /// public int maxHealth = 100;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_TabGroupAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the tab group
        /// </summary>
        public string GroupName { get; private set; }
        
        /// <summary>
        /// Name of the specific tab
        /// </summary>
        public string TabName { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a tab group
        /// </summary>
        /// <param name="groupName">Name of the tab group</param>
        /// <param name="tabName">Name of the specific tab</param>
        public MM_TabGroupAttribute(string groupName, string tabName)
        {
            GroupName = groupName;
            TabName = tabName;
        }
        
        #endregion
    }
}
