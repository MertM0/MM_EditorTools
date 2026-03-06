using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Validates field value using a custom method.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_ValidateInput("IsValidName", "Name must not be empty!")]
    /// public string playerName = "";
    /// 
    /// private bool IsValidName(string value)
    /// {
    ///     return !string.IsNullOrEmpty(value);
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_ValidateInputAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the validation method
        /// </summary>
        public string MethodName { get; private set; }
        
        /// <summary>
        /// Error message to display if validation fails
        /// </summary>
        public string ErrorMessage { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Validates input using a custom method
        /// </summary>
        /// <param name="methodName">Name of the validation method (must return bool)</param>
        /// <param name="errorMessage">Error message to display</param>
        public MM_ValidateInputAttribute(string methodName, string errorMessage = "Validation failed")
        {
            MethodName = methodName;
            ErrorMessage = errorMessage;
        }
        
        #endregion
    }
}
