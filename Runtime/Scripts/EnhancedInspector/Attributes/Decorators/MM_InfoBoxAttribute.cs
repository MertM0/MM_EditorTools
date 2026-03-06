using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Type of information box
    /// </summary>
    public enum InfoType
    {
        Info,
        Warning,
        Error,
        Success
    }
    
    /// <summary>
    /// Displays an information box above a field in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_InfoBox("This value affects player speed", InfoType.Warning)]
    /// public float speed = 5f;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class MM_InfoBoxAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// The message to display
        /// </summary>
        public string Message { get; private set; }
        
        /// <summary>
        /// Type of the info box
        /// </summary>
        public InfoType Type { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates an information box
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="type">Type of message (default: Info)</param>
        public MM_InfoBoxAttribute(string message, InfoType type = InfoType.Info)
        {
            Message = message;
            Type = type;
        }
        
        #endregion
    }
}
