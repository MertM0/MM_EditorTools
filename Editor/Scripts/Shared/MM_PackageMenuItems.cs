using UnityEditor;
using UnityEngine;
using System.IO;
using MM.EditorTools.EnhancedInspector;
// using MM.EditorTools.EnhancedHierarchy;

namespace MM.EditorTools
{
    /// <summary>
    /// Creates menu items under Assets/Create/MM Packages for creating package-specific assets.
    /// This system automatically discovers installed MM packages and creates menu items for them.
    /// </summary>
    public static class MM_PackageMenuItems
    {
        #region Menu Items
        
        /// <summary>
        /// Creates a new MM package from template
        /// </summary>
        [MenuItem("Assets/Create/MM Packages/Create New Package", priority = 0)]
        public static void CreateNewPackage()
        {
            string packageName = "MM_NewPackage";
            
            // Ask for package name
            packageName = EditorUtility.SaveFilePanelInProject(
                "Create New MM Package",
                packageName,
                "",
                "Enter package name (will be prefixed with MM_ if not already)",
                "Assets/MM_Packages"
            );
            
            if (string.IsNullOrEmpty(packageName)) return;
            
            // Ensure MM_ prefix
            string folderName = Path.GetFileName(packageName);
            if (!folderName.StartsWith("MM_"))
            {
                folderName = "MM_" + folderName;
            }
            
            string packagePath = Path.Combine(Path.GetDirectoryName(packageName), folderName);
            
            // Create package structure
            CreatePackageStructure(packagePath, folderName);
            
            AssetDatabase.Refresh();
            Debug.Log($"[MM Tools] ✅ Created new package: {folderName}");
        }
        
        #endregion
        
        #region MM Tools Top Menu
        
        /// <summary>
        /// Creates separator from top menu
        /// </summary>
        [MenuItem("MM Tools/Enhanced Hierarchy/Create Separator", false, 2)]
        public static void CreateSeparatorFromTopMenu()
        {
            CreateSeparator();
        }
        
        /// <summary>
        /// Opens Enhanced Inspector Validation Window from top menu
        /// </summary>
        [MenuItem("MM Tools/Enhanced Inspector/Validation", false, 20)]
        public static void OpenValidationFromTopMenu()
        {
            EditorWindow.GetWindow<MM_ValidationWindow>("Validation");
        }
        
        #endregion
        
        #region MM_EditorTools Specific
        
        /// <summary>
        /// Creates MM_EditorTools separator GameObject
        /// </summary>
        [MenuItem("GameObject/MM Tools/Create Separator", false, 0)]
        public static void CreateSeparator()
        {
            GameObject separator = new GameObject("--- SEPARATOR ---");
            
            // Place in hierarchy
            if (Selection.activeGameObject != null)
            {
                separator.transform.SetParent(Selection.activeGameObject.transform.parent);
                separator.transform.SetSiblingIndex(Selection.activeGameObject.transform.GetSiblingIndex() + 1);
            }
            
            Selection.activeGameObject = separator;
            Undo.RegisterCreatedObjectUndo(separator, "Create Separator");
        }
        
        #endregion
        
        #region Private Methods
        
        /// <summary>
        /// Creates the complete package structure
        /// </summary>
        private static void CreatePackageStructure(string packagePath, string packageName)
        {
            // Create directories
            Directory.CreateDirectory(Path.Combine(packagePath, "Runtime/Scripts"));
            Directory.CreateDirectory(Path.Combine(packagePath, "Runtime/Prefabs"));
            Directory.CreateDirectory(Path.Combine(packagePath, "Runtime/Resources"));
            Directory.CreateDirectory(Path.Combine(packagePath, "Editor/Scripts"));
            
            // Create package.json
            string packageJson = $@"{{
  ""name"": ""com.mm.{packageName.ToLower().Replace("mm_", "")}"",
  ""version"": ""1.0.0"",
  ""displayName"": ""{packageName}"",
  ""description"": ""Description for {packageName}"",
  ""unity"": ""6000.0"",
  ""unityRelease"": ""60f1"",
  ""dependencies"": {{}},
  ""keywords"": [
    ""mm"",
    ""{packageName.ToLower()}""
  ],
  ""author"": {{
    ""name"": ""Mert Mutlu"",
    ""email"": ""mertapsi@gmail.com"",
    ""url"": ""https://github.com/mertm0""
  }}
}}";
            File.WriteAllText(Path.Combine(packagePath, "package.json"), packageJson);
            
            // Create README.md
            string readme = $@"# {packageName}

Description of your package.

## 📥 Installation

Via Unity Package Manager (Git URL):
```
https://github.com/mertm0/{packageName}.git
```

## 🎯 Usage

Usage instructions here.

---

**Developed by Mert Mutlu**
";
            File.WriteAllText(Path.Combine(packagePath, "README.md"), readme);
            
            // Create changelog.md
            string changelog = $@"# Changelog

## [1.0.0] - {System.DateTime.Now:yyyy-MM-dd}

### Added
- Initial release

---

**Developed by Mert Mutlu**
";
            File.WriteAllText(Path.Combine(packagePath, "changelog.md"), changelog);
        }
        
        #endregion
    }
}
