using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Editor window for validating all Required fields in the scene.
    /// </summary>
    public class MM_ValidationWindow : EditorWindow
    {
        #region Fields
        
        private Vector2 _scrollPosition;
        private bool _hasErrors = false;
        
        #endregion
        
        #region Menu Items
        
        // Window menu item moved to MM_PackageMenuItems to avoid duplication
        // Use "MM Tools/Validation" from top menu
        
        /// <summary>
        /// Shows the validation window (called by menu items)
        /// </summary>
        public static void ShowWindow()
        {
            MM_ValidationWindow window = GetWindow<MM_ValidationWindow>("Scene Validation");
            window.minSize = new Vector2(400, 300);
            window.Show();
        }
        
        #endregion
        
        #region Unity Lifecycle
        
        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Scene Validation", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("Validates all [MM_Required] and [MM_NotNull] fields in the current scene.", MessageType.Info);
            EditorGUILayout.Space(10);
            
            if (GUILayout.Button("Validate Now", GUILayout.Height(30)))
            {
                ValidateAllFields();
            }
            
            EditorGUILayout.Space(10);
            
            if (_hasErrors)
            {
                EditorGUILayout.HelpBox("Validation issues found! Check the list below.", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.HelpBox("No validation issues found.", MessageType.Info);
            }
            
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            // Validation results will be displayed here
            EditorGUILayout.EndScrollView();
        }
        
        #endregion
        
        #region Validation Methods
        
        /// <summary>
        /// Validates all required fields in the scene
        /// </summary>
        private void ValidateAllFields()
        {
            _hasErrors = false;
            int errorCount = 0;
            int warningCount = 0;
            
            // Find all MonoBehaviours in scene
            MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            
            foreach (var obj in allObjects)
            {
                var fields = obj.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                foreach (var field in fields)
                {
                    // Check for Required attribute
                    var requiredAttr = field.GetCustomAttributes(typeof(MM_RequiredAttribute), true);
                    if (requiredAttr.Length > 0)
                    {
                        var value = field.GetValue(obj);
                        if (value == null || (value is UnityEngine.Object && !(value as UnityEngine.Object)))
                        {
                            Debug.LogError($"[Validation] Required field '{field.Name}' is null in {obj.gameObject.name}", obj.gameObject);
                            errorCount++;
                            _hasErrors = true;
                        }
                    }
                    
                    // Check for NotNull attribute
                    var notNullAttr = field.GetCustomAttributes(typeof(MM_NotNullAttribute), true);
                    if (notNullAttr.Length > 0)
                    {
                        var value = field.GetValue(obj);
                        if (value == null || (value is UnityEngine.Object && !(value as UnityEngine.Object)))
                        {
                            Debug.LogWarning($"[Validation] Field '{field.Name}' is null in {obj.gameObject.name}", obj.gameObject);
                            warningCount++;
                            _hasErrors = true;
                        }
                    }
                }
            }
            
            if (errorCount == 0 && warningCount == 0)
            {
                Debug.Log("[MM Tools] ✅ All validation checks passed!");
            }
            else
            {
                Debug.LogWarning($"[MM Tools] Validation completed: {errorCount} errors, {warningCount} warnings");
            }
            
            Repaint();
        }
        
        #endregion
    }
}
