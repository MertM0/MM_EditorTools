using UnityEditor;
using UnityEngine;
using MM.EditorTools.EnhancedInspector;

namespace MM.EditorTools
{
    /// <summary>
    /// Package-specific menu items for MM_EditorTools.
    /// </summary>
    public static class MM_PackageMenuItems
    {
        #region MM Tools Top Menu
        
        /// <summary>
        /// Opens Enhanced Inspector Validation Window from top menu
        /// </summary>
        [MenuItem("MM Tools/Enhanced Inspector/Validation", false, 20)]
        public static void OpenValidationFromTopMenu()
        {
            EditorWindow.GetWindow<MM_ValidationWindow>("Validation");
        }
        
        #endregion
    }
}
