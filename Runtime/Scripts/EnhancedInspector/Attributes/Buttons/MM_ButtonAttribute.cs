using System;
using System.Reflection;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Adds a button in the Inspector that calls the specified method when clicked.
    /// Can be used on methods with no parameters or simple parameters.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_Button("Reset Player")]
    /// public void ResetPlayer()
    /// {
    ///     health = 100;
    ///     Debug.Log("Player reset!");
    /// }
    /// 
    /// [MM_Button("Test", tabGroup: "Main", tabName: "Actions")]
    /// public void TestMethod()
    /// {
    ///     Debug.Log("Test!");
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MM_ButtonAttribute : Attribute
    {
        #region Fields
        
        /// <summary>
        /// Custom button label (optional, uses method name if not provided)
        /// </summary>
        public string Label { get; private set; }
        
        /// <summary>
        /// Button height in pixels
        /// </summary>
        public int Height { get; private set; }
        
        /// <summary>
        /// Tab group name for organizing buttons
        /// </summary>
        public string TabGroup { get; set; }
        
        /// <summary>
        /// Tab name within the group
        /// </summary>
        public string TabName { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a button for this method
        /// </summary>
        /// <param name="label">Button label (uses method name if null)</param>
        /// <param name="height">Button height in pixels (default: 40)</param>
        public MM_ButtonAttribute(string label = null, int height = 40)
        {
            Label = label;
            Height = height;
            TabGroup = null;
            TabName = null;
        }
        
        #endregion
    }
}
