using UnityEditor;

namespace MM.EditorTools
{
    /// <summary>
    /// Settings manager for MM_EditorTools package.
    /// Note: UseAttributePrefix is available but not enforced - both styles work.
    /// See README.md for usage examples.
    /// </summary>
    public static class MM_EditorToolsSettings
    {
        #region Constants
        
        private const string PREFS_PREFIX = "MM_EditorTools_";
        private const string PREF_USE_PREFIX = PREFS_PREFIX + "UseAttributePrefix";
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Whether to recommend using MM_ prefix in attribute names.
        /// Default: false (prefix-free is recommended)
        /// Note: This is purely informational - both [TabGroup] and [MM_TabGroup] work.
        /// </summary>
        public static bool UseAttributePrefix
        {
            get => EditorPrefs.GetBool(PREF_USE_PREFIX, false);
            set => EditorPrefs.SetBool(PREF_USE_PREFIX, value);
        }
        
        #endregion
    }
}
