using System;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Groups multiple buttons horizontally in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_ButtonGroup]
    /// [MM_Button("Button 1")]
    /// public void Method1() { }
    /// 
    /// [MM_ButtonGroup]
    /// [MM_Button("Button 2")]
    /// public void Method2() { }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MM_ButtonGroupAttribute : Attribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the button group
        /// </summary>
        public string GroupName { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a button group
        /// </summary>
        /// <param name="groupName">Name of the group (buttons with same name will be grouped)</param>
        public MM_ButtonGroupAttribute(string groupName = "")
        {
            GroupName = groupName;
        }
        
        #endregion
    }
}
