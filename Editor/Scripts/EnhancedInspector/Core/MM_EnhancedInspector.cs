using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Optimized Enhanced Custom Inspector for Unity MonoBehaviours.
    /// Provides nested grouping (Box, Foldout, Tab, Horizontal) and method buttons.
    /// Performance-optimized with reflection caching.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MM_EnhancedInspector : UnityEditor.Editor
    {
        #region Cache Classes
        
        private class InspectorCache
        {
            public List<FieldInfo> SerializedFields;
            public List<MethodInfo> ButtonMethods;
        }

        private class GroupNode
        {
            public string Name;
            public string FullPath;
            public string Type; // "Tab", "Box", "Foldout", "Horizontal", "Container"
            public bool DefaultExpanded;
            public List<FieldInfo> Fields = new List<FieldInfo>();
            public List<GroupNode> Children = new List<GroupNode>();
        }
        
        #endregion
        
        #region Static Cache
        
        private static readonly Dictionary<Type, InspectorCache> _typeCache = new Dictionary<Type, InspectorCache>();
        private static readonly Dictionary<string, bool> _foldoutStates = new Dictionary<string, bool>();
        private static readonly Dictionary<string, int> _tabIndices = new Dictionary<string, int>();
        private static readonly Dictionary<Color, Texture2D> _texCache = new Dictionary<Color, Texture2D>();

        internal static bool SkipDecorators = false;

        // Drag & Drop state
        private static int _draggingIndex = -1;
        private static string _draggingArrayPath = null;

        // Cached GUIStyles (lazy-initialized on first use)
        private static GUIStyle _arrowStyle;
        private static GUIStyle _headerLabelStyle;
        private static GUIStyle _countStyle;
        private static GUIStyle _addButtonStyle;
        private static GUIStyle _hamburgerStyle;
        private static GUIStyle _indexLabelStyle;
        private static GUIStyle _deleteButtonStyle;
        private static GUIStyle _tabLabelStyle;
        private static GUIStyle _complexEvenStyle;
        private static GUIStyle _complexOddStyle;
        private static GUIStyle _complexDragStyle;
        private static GUIStyle _buttonContainerStyle;
        private static GUIStyle _buttonDrawStyle;

        private static void EnsureStyles()
        {
            if (_arrowStyle != null) return;
            _arrowStyle = new GUIStyle(EditorStyles.label) { fontSize = 9, alignment = TextAnchor.MiddleCenter, normal = { textColor = new Color(0.65f, 0.65f, 0.65f) } };
            _headerLabelStyle = new GUIStyle(EditorStyles.label) { fontSize = 11, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleLeft, normal = { textColor = new Color(0.85f, 0.85f, 0.85f) } };
            _countStyle = new GUIStyle(EditorStyles.label) { fontSize = 10, alignment = TextAnchor.MiddleRight, normal = { textColor = new Color(0.7f, 0.7f, 0.7f) } };
            _addButtonStyle = new GUIStyle(EditorStyles.label) { fontSize = 14, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter, normal = { textColor = new Color(0.6f, 0.6f, 0.6f) }, hover = { textColor = new Color(0.3f, 0.9f, 0.3f) } };
            _hamburgerStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter, fontSize = 12, fontStyle = FontStyle.Bold, normal = { textColor = new Color(0.5f, 0.5f, 0.5f) } };
            _indexLabelStyle = new GUIStyle(EditorStyles.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleLeft, fontSize = 11, normal = { textColor = new Color(0.75f, 0.75f, 0.75f) } };
            _deleteButtonStyle = new GUIStyle(EditorStyles.label) { fontSize = 14, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter, normal = { textColor = new Color(0.6f, 0.6f, 0.6f) }, hover = { textColor = new Color(0.9f, 0.3f, 0.3f) } };
            _tabLabelStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter, fontSize = 11 };
            _complexEvenStyle = new GUIStyle(EditorStyles.helpBox) { normal = { background = GetCachedTex(new Color(0.24f, 0.24f, 0.24f)) } };
            _complexOddStyle  = new GUIStyle(EditorStyles.helpBox) { normal = { background = GetCachedTex(new Color(0.20f, 0.20f, 0.20f)) } };
            _complexDragStyle = new GUIStyle(EditorStyles.helpBox) { normal = { background = GetCachedTex(new Color(0.3f, 0.5f, 0.9f, 0.4f)) } };
            _buttonContainerStyle = new GUIStyle(EditorStyles.helpBox) { padding = new RectOffset(8, 8, 8, 8) };
            _buttonDrawStyle = new GUIStyle(GUI.skin.button) { fontSize = 11, padding = new RectOffset(12, 12, 6, 6), margin = new RectOffset(2, 2, 2, 2) };
        }
        
        #endregion
        
        #region Unity Lifecycle
        
        public override void OnInspectorGUI()
        {
            EnsureStyles();
            serializedObject.Update();
            DrawPropertiesWithGrouping();
            serializedObject.ApplyModifiedProperties();
            DrawButtons();
        }
        
        #endregion
        
        #region Cache Building
        
        private InspectorCache GetOrCreateCache()
        {
            Type targetType = target.GetType();
            
            if (_typeCache.TryGetValue(targetType, out var cache))
                return cache;
            
            cache = new InspectorCache
            {
                SerializedFields = targetType
                    .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(f => f.IsPublic || f.GetCustomAttribute<SerializeField>() != null)
                    .ToList(),
                
                ButtonMethods = targetType
                    .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(m => m.GetCustomAttribute<MM_ButtonAttribute>() != null)
                    .ToList()
            };
            
            _typeCache[targetType] = cache;
            return cache;
        }
        
        #endregion
        
        #region Property Drawing
        
        private void DrawPropertiesWithGrouping()
        {
            var cache = GetOrCreateCache();
            var drawnFields = new HashSet<string>();
            
            // Draw script field
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            if (scriptProp != null)
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(scriptProp);
                GUI.enabled = true;
            }
            
            EditorGUILayout.Space(5);
            
            // Build and draw nested groups
            var rootGroups = BuildNestedGroupStructure(cache);
            foreach (var group in rootGroups)
                DrawNestedGroup(group, drawnFields);
            
            // Draw ungrouped fields
            foreach (var field in cache.SerializedFields)
            {
                if (drawnFields.Contains(field.Name) || HasGroupAttribute(field))
                    continue;
                
                SerializedProperty prop = serializedObject.FindProperty(field.Name);
                if (prop != null)
                {
                    EditorGUILayout.PropertyField(prop, true);
                    drawnFields.Add(field.Name);
                }
            }
        }
        
        private bool HasGroupAttribute(FieldInfo field)
        {
            return field.GetCustomAttribute<MM_BoxGroupAttribute>() != null ||
                   field.GetCustomAttribute<MM_FoldoutGroupAttribute>() != null ||
                   field.GetCustomAttribute<MM_TabGroupAttribute>() != null ||
                   field.GetCustomAttribute<MM_HorizontalGroupAttribute>() != null;
        }
        
        #endregion
        
        #region Nested Group Building
        
        private List<GroupNode> BuildNestedGroupStructure(InspectorCache cache)
        {
            var rootNodes = new List<GroupNode>();
            var allNodes = new Dictionary<string, GroupNode>();
            
            foreach (var field in cache.SerializedFields)
            {
                // Find deepest grouping attribute
                string path = GetDeepestGroupPath(field, out string type, out bool defaultExpanded);
                
                if (path != null)
                    ProcessNestedPath(path, type, field, allNodes, rootNodes, defaultExpanded);
            }
            
            return rootNodes;
        }
        
        private string GetDeepestGroupPath(FieldInfo field, out string type, out bool defaultExpanded)
        {
            string deepestPath = null;
            type = null;
            defaultExpanded = false;
            
            var foldout = field.GetCustomAttribute<MM_FoldoutGroupAttribute>();
            if (foldout != null && (deepestPath == null || foldout.GroupName.Length > deepestPath.Length))
            {
                deepestPath = foldout.GroupName;
                type = "Foldout";
                defaultExpanded = foldout.DefaultExpanded;
            }
            
            var box = field.GetCustomAttribute<MM_BoxGroupAttribute>();
            if (box != null && (deepestPath == null || box.GroupName.Length > deepestPath.Length))
            {
                deepestPath = box.GroupName;
                type = "Box";
            }
            
            var tab = field.GetCustomAttribute<MM_TabGroupAttribute>();
            if (tab != null)
            {
                string tabPath = tab.GroupName + "/" + tab.TabName;
                if (deepestPath == null || tabPath.Length > deepestPath.Length)
                {
                    deepestPath = tabPath;
                    type = "TabContainer";
                }
            }
            
            var horizontal = field.GetCustomAttribute<MM_HorizontalGroupAttribute>();
            if (horizontal != null && (deepestPath == null || horizontal.GroupName.Length > deepestPath.Length))
            {
                deepestPath = horizontal.GroupName;
                type = "Horizontal";
            }
            
            return deepestPath;
        }
        
        private void ProcessNestedPath(string path, string type, FieldInfo field,
            Dictionary<string, GroupNode> allNodes,
            List<GroupNode> rootNodes,
            bool defaultExpanded)
        {
            string[] parts = path.Split('/');
            GroupNode parent = null;
            string currentPath = "";
            
            for (int i = 0; i < parts.Length; i++)
            {
                currentPath = string.IsNullOrEmpty(currentPath) ? parts[i] : currentPath + "/" + parts[i];
                
                if (!allNodes.ContainsKey(currentPath))
                {
                    string nodeType = DetermineNodeType(field, currentPath, i, parts.Length, type, defaultExpanded, out bool nodeExpanded);
                    
                    var node = new GroupNode
                    {
                        Name = parts[i],
                        FullPath = currentPath,
                        Type = nodeType,
                        DefaultExpanded = nodeExpanded
                    };
                    
                    allNodes[currentPath] = node;
                    
                    if (parent == null)
                        rootNodes.Add(node);
                    else
                        parent.Children.Add(node);
                }
                else
                {
                    // Upgrade Container to specific type if needed
                    UpgradeNodeType(field, currentPath, allNodes[currentPath]);
                }
                
                parent = allNodes[currentPath];
            }
            
            if (parent != null)
                parent.Fields.Add(field);
        }
        
        private string DetermineNodeType(FieldInfo field, string currentPath, int level, int totalLevels, 
            string leafType, bool defaultExpanded, out bool nodeExpanded)
        {
            nodeExpanded = false;
            
            var foldout = field.GetCustomAttribute<MM_FoldoutGroupAttribute>();
            if (foldout != null && currentPath == foldout.GroupName)
            {
                nodeExpanded = foldout.DefaultExpanded;
                return "Foldout";
            }
            
            var box = field.GetCustomAttribute<MM_BoxGroupAttribute>();
            if (box != null && currentPath == box.GroupName)
                return "Box";
            
            var tab = field.GetCustomAttribute<MM_TabGroupAttribute>();
            if (tab != null && currentPath == tab.GroupName)
                return "Tab";
            
            if (level == totalLevels - 1)
            {
                nodeExpanded = defaultExpanded;
                return leafType;
            }
            
            return "Container";
        }
        
        private void UpgradeNodeType(FieldInfo field, string currentPath, GroupNode node)
        {
            if (node.Type != "Container")
                return;
            
            var foldout = field.GetCustomAttribute<MM_FoldoutGroupAttribute>();
            if (foldout != null && currentPath == foldout.GroupName)
            {
                node.Type = "Foldout";
                node.DefaultExpanded = foldout.DefaultExpanded;
                return;
            }
            
            var box = field.GetCustomAttribute<MM_BoxGroupAttribute>();
            if (box != null && currentPath == box.GroupName)
            {
                node.Type = "Box";
            }
        }
        
        #endregion
        
        #region Group Drawing
        
        private void DrawNestedGroup(GroupNode node, HashSet<string> drawnFields)
        {
            switch (node.Type)
            {
                case "Tab":
                    DrawNestedTabGroup(node, drawnFields);
                    break;
                case "Box":
                    DrawNestedBoxGroup(node, drawnFields);
                    break;
                case "Foldout":
                    DrawNestedFoldoutGroup(node, drawnFields);
                    break;
                case "Horizontal":
                    DrawNestedHorizontalGroup(node, drawnFields);
                    break;
                default: // Container or TabContainer
                    DrawContainerFields(node, drawnFields);
                    break;
            }
        }
        
        private void DrawContainerFields(GroupNode node, HashSet<string> drawnFields)
        {
            foreach (var field in node.Fields)
            {
                SerializedProperty prop = serializedObject.FindProperty(field.Name);
                if (prop != null)
                {
                    DrawPropertyWithConstrainedWidth(prop);
                    drawnFields.Add(field.Name);
                }
            }
            
            foreach (var child in node.Children)
                DrawNestedGroup(child, drawnFields);
        }
        
        private void DrawPropertyWithConstrainedWidth(SerializedProperty prop)
        {
            // For arrays/lists, draw with FoldoutGroup style
            if (prop.isArray && prop.propertyType != SerializedPropertyType.String)
            {
                DrawArrayAsFoldoutGroup(prop);
            }
            else
            {
                EditorGUILayout.PropertyField(prop, true);
            }
        }
        
        private void DrawArrayAsFoldoutGroup(SerializedProperty prop)
        {
            string foldoutKey = target.GetInstanceID() + "_array_" + prop.propertyPath;
            if (!_foldoutStates.ContainsKey(foldoutKey))
                _foldoutStates[foldoutKey] = prop.isExpanded;
            
            EditorGUILayout.Space(3);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            // Draw header
            Rect headerRect = EditorGUILayout.GetControlRect(false, 22f);
            EditorGUI.DrawRect(headerRect, new Color(0.25f, 0.25f, 0.25f, 1f));
            
            EditorGUI.LabelField(new Rect(headerRect.x + 5f, headerRect.y, 14f, headerRect.height),
                _foldoutStates[foldoutKey] ? "▼" : "▶", _arrowStyle);
            EditorGUI.LabelField(new Rect(headerRect.x + 22f, headerRect.y, headerRect.width - 100f, headerRect.height),
                prop.displayName, _headerLabelStyle);
            EditorGUI.LabelField(new Rect(headerRect.xMax - 65f, headerRect.y, 40f, headerRect.height),
                prop.arraySize.ToString(), _countStyle);

            Rect addButtonRect = new Rect(headerRect.xMax - 24f, headerRect.y + 3f, 20f, headerRect.height - 6f);
            if (GUI.Button(addButtonRect, "+", _addButtonStyle))
            {
                prop.arraySize++;
                Event.current.Use();
            }
            
            if (Event.current.type == EventType.MouseDown && headerRect.Contains(Event.current.mousePosition) && !addButtonRect.Contains(Event.current.mousePosition))
            {
                _foldoutStates[foldoutKey] = !_foldoutStates[foldoutKey];
                prop.isExpanded = _foldoutStates[foldoutKey];
                Event.current.Use();
            }
            
            if (_foldoutStates[foldoutKey])
            {
                EditorGUILayout.Space(2);
                
                // Draw array elements with drag & drop
                int deleteIndex = -1;
                int moveFromIndex = -1;
                int moveToIndex = -1;

                for (int i = 0; i < prop.arraySize; i++)
                {
                    var element = prop.GetArrayElementAtIndex(i);
                    bool isComplex = element.propertyType == SerializedPropertyType.Generic && element.hasChildren;

                    if (isComplex)
                        DrawComplexArrayElement(prop, element, i, ref deleteIndex, ref moveFromIndex, ref moveToIndex);
                    else
                        DrawSimpleArrayElement(prop, element, i, ref deleteIndex, ref moveFromIndex, ref moveToIndex);
                }

                if (deleteIndex >= 0)
                    prop.DeleteArrayElementAtIndex(deleteIndex);

                if (moveFromIndex >= 0 && moveToIndex >= 0 && moveFromIndex != moveToIndex)
                    prop.MoveArrayElement(moveFromIndex, moveToIndex);
                
                EditorGUILayout.Space(2);
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
        }
        
        private void DrawSimpleArrayElement(SerializedProperty prop, SerializedProperty element, int index,
            ref int deleteIndex, ref int moveFromIndex, ref int moveToIndex)
        {
            var bgColor = index % 2 == 0 ? new Color(0.24f, 0.24f, 0.24f) : new Color(0.20f, 0.20f, 0.20f);
            bool isDragging = _draggingIndex == index && _draggingArrayPath == prop.propertyPath;

            var lineRect = EditorGUILayout.GetControlRect(false, 22f);
            EditorGUI.DrawRect(lineRect, isDragging ? new Color(0.3f, 0.5f, 0.9f, 0.4f) : bgColor);

            float x = lineRect.x + 4f;
            float y = lineRect.y + 3f;
            float h = lineRect.height - 6f;

            GUI.Label(new Rect(x, y, 20f, h), "☰", _hamburgerStyle);
            x += 22f;
            GUI.Label(new Rect(x, lineRect.y, 20f, lineRect.height), index.ToString(), _indexLabelStyle);
            x += 22f;

            var deleteRect = new Rect(lineRect.xMax - 24f, y, 20f, h);
            var propRect = new Rect(x, lineRect.y + 2f, deleteRect.x - x - 4f, lineRect.height - 4f);
            EditorGUI.PropertyField(propRect, element, GUIContent.none, true);

            if (GUI.Button(deleteRect, "×", _deleteButtonStyle))
                deleteIndex = index;

            var draggableLeft = new Rect(lineRect.x, lineRect.y, propRect.x - lineRect.x, lineRect.height);
            var draggableRight = new Rect(deleteRect.x, lineRect.y, lineRect.xMax - deleteRect.x, lineRect.height);
            HandleDragAndDrop(draggableLeft, draggableRight, lineRect, prop.propertyPath, index, ref moveFromIndex, ref moveToIndex);
        }
        
        private void DrawComplexArrayElement(SerializedProperty prop, SerializedProperty element, int index,
            ref int deleteIndex, ref int moveFromIndex, ref int moveToIndex)
        {
            bool isDragging = _draggingIndex == index && _draggingArrayPath == prop.propertyPath;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(isDragging ? _complexDragStyle : (index % 2 == 0 ? _complexEvenStyle : _complexOddStyle));
            
            // Foldout state
            string elementFoldoutKey = target.GetInstanceID() + "_arrayElement_" + prop.propertyPath + "_" + index;
            if (!_foldoutStates.ContainsKey(elementFoldoutKey))
                _foldoutStates[elementFoldoutKey] = element.isExpanded;
            
            // Header
            var headerRect = EditorGUILayout.GetControlRect(false, 22f);
            var headerBg = index % 2 == 0 ? new Color(0.26f, 0.26f, 0.26f) : new Color(0.22f, 0.22f, 0.22f);
            EditorGUI.DrawRect(headerRect, isDragging ? new Color(0.35f, 0.55f, 0.95f, 0.6f) : headerBg);

            float xPos = headerRect.x + 4f;
            float yCenter = headerRect.y + 3f;
            float itemHeight = headerRect.height - 6f;

            GUI.Label(new Rect(xPos, yCenter, 20f, itemHeight), "☰", _hamburgerStyle);
            xPos += 22f;
            GUI.Label(new Rect(xPos, headerRect.y, 20f, headerRect.height), index.ToString(), _indexLabelStyle);
            xPos += 22f;

            if (GUI.Button(new Rect(xPos, yCenter, 20f, itemHeight),
                _foldoutStates[elementFoldoutKey] ? "▼" : "▶", _arrowStyle))
            {
                _foldoutStates[elementFoldoutKey] = !_foldoutStates[elementFoldoutKey];
                element.isExpanded = _foldoutStates[elementFoldoutKey];
            }
            xPos += 22f;

            var deleteRect = new Rect(headerRect.xMax - 24f, yCenter, 20f, itemHeight);
            if (GUI.Button(deleteRect, "×", _deleteButtonStyle))
                deleteIndex = index;
            
            // Draw children if expanded
            if (_foldoutStates[elementFoldoutKey])
            {
                EditorGUILayout.Space(3);
                EditorGUI.indentLevel++;
                
                SerializedProperty child = element.Copy();
                SerializedProperty endProperty = child.GetEndProperty();
                child.NextVisible(true);
                
                while (!SerializedProperty.EqualContents(child, endProperty))
                {
                    EditorGUILayout.PropertyField(child, true);
                    if (!child.NextVisible(false))
                        break;
                }
                
                EditorGUI.indentLevel--;
                EditorGUILayout.Space(2);
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            
            var fullElementRect = GUILayoutUtility.GetLastRect();
            var draggableLeft = new Rect(headerRect.x, headerRect.y, deleteRect.x - headerRect.x, headerRect.height);
            var draggableRight = new Rect(deleteRect.x, headerRect.y, headerRect.xMax - deleteRect.x, headerRect.height);
            HandleDragAndDrop(draggableLeft, draggableRight, fullElementRect, prop.propertyPath, index, ref moveFromIndex, ref moveToIndex);
        }
        
        private static Texture2D GetCachedTex(Color col)
        {
            if (_texCache.TryGetValue(col, out var tex) && tex != null)
                return tex;
            tex = new Texture2D(2, 2);
            tex.SetPixels(new[] { col, col, col, col });
            tex.Apply();
            _texCache[col] = tex;
            return tex;
        }

        private void HandleDragAndDrop(Rect draggableLeft, Rect draggableRight, Rect fullRect, string arrayPath, int index, ref int moveFromIndex, ref int moveToIndex)
        {
            var evt = Event.current;
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            bool inDraggableArea = draggableLeft.Contains(evt.mousePosition) || draggableRight.Contains(evt.mousePosition);
            
            switch (evt.GetTypeForControl(controlID))
            {
                case EventType.MouseDown:
                    if (inDraggableArea && evt.button == 0)
                    {
                        _draggingIndex = index;
                        _draggingArrayPath = arrayPath;
                        GUIUtility.hotControl = controlID;
                        evt.Use();
                    }
                    break;
                    
                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == controlID && _draggingIndex == index)
                    {
                        DragAndDrop.PrepareStartDrag();
                        DragAndDrop.StartDrag("Reordering");
                        evt.Use();
                    }
                    break;
                    
                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlID)
                    {
                        GUIUtility.hotControl = 0;
                        _draggingIndex = -1;
                        _draggingArrayPath = null;
                        evt.Use();
                    }
                    break;
                    
                case EventType.DragUpdated:
                    if (_draggingIndex >= 0 && _draggingArrayPath == arrayPath && fullRect.Contains(evt.mousePosition))
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Move;
                        evt.Use();
                    }
                    break;
                    
                case EventType.DragPerform:
                    if (_draggingIndex >= 0 && _draggingArrayPath == arrayPath && fullRect.Contains(evt.mousePosition) && _draggingIndex != index)
                    {
                        moveFromIndex = _draggingIndex;
                        bool insertBefore = evt.mousePosition.y < fullRect.y + fullRect.height * 0.5f;
                        moveToIndex = insertBefore ? index : index + 1;
                        if (moveFromIndex < moveToIndex) moveToIndex--;
                        
                        DragAndDrop.AcceptDrag();
                        GUIUtility.hotControl = 0;
                        _draggingIndex = -1;
                        _draggingArrayPath = null;
                        evt.Use();
                    }
                    break;
                    
                case EventType.Repaint:
                    if (_draggingIndex >= 0 && _draggingArrayPath == arrayPath && fullRect.Contains(evt.mousePosition) && _draggingIndex != index)
                    {
                        bool insertBefore = evt.mousePosition.y < fullRect.y + fullRect.height * 0.5f;
                        bool skip = insertBefore ? _draggingIndex == index - 1 : _draggingIndex == index + 1;
                        if (!skip)
                        {
                            var dropLine = insertBefore
                                ? new Rect(fullRect.x, fullRect.y, fullRect.width, 2f)
                                : new Rect(fullRect.x, fullRect.yMax - 2f, fullRect.width, 2f);
                            EditorGUI.DrawRect(dropLine, new Color(0.3f, 0.6f, 1f, 0.9f));
                        }
                    }
                    break;
            }
        }
        
        private void DrawNestedTabGroup(GroupNode node, HashSet<string> drawnFields)
        {
            var childrenByTab = new Dictionary<string, List<GroupNode>>();
            int parentDepth = node.FullPath.Split('/').Length;
            foreach (var child in node.Children)
            {
                var parts = child.FullPath.Split('/');
                if (parts.Length > parentDepth)
                {
                    var tabName = parts[parentDepth];
                    if (!childrenByTab.ContainsKey(tabName))
                        childrenByTab[tabName] = new List<GroupNode>();
                    childrenByTab[tabName].Add(child);
                }
            }
            
            var tabs = childrenByTab.Keys.ToArray();
            if (tabs.Length == 0) return;
            
            string tabKey = target.GetInstanceID() + "_" + node.FullPath;
            if (!_tabIndices.ContainsKey(tabKey))
                _tabIndices[tabKey] = 0;
            
            // Draw tab UI
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            Rect tabBarRect = EditorGUILayout.GetControlRect(false, 22f);
            EditorGUI.DrawRect(tabBarRect, new Color(0.22f, 0.22f, 0.22f, 1f));
            
            float tabWidth = tabBarRect.width / tabs.Length;
            for (int i = 0; i < tabs.Length; i++)
            {
                var tabRect = new Rect(tabBarRect.x + i * tabWidth, tabBarRect.y, tabWidth, tabBarRect.height);
                bool isSelected = _tabIndices[tabKey] == i;

                if (isSelected)
                {
                    EditorGUI.DrawRect(tabRect, new Color(0.28f, 0.28f, 0.28f));
                    EditorGUI.DrawRect(new Rect(tabRect.x, tabRect.yMax - 2f, tabRect.width, 2f), new Color(0.3f, 0.5f, 0.9f));
                }

                if (i > 0)
                    EditorGUI.DrawRect(new Rect(tabRect.x, tabRect.y + 4f, 1f, tabRect.height - 8f), new Color(0.15f, 0.15f, 0.15f));

                _tabLabelStyle.fontStyle = isSelected ? FontStyle.Bold : FontStyle.Normal;
                _tabLabelStyle.normal.textColor = isSelected ? new Color(0.9f, 0.9f, 0.9f) : new Color(0.65f, 0.65f, 0.65f);
                EditorGUI.LabelField(tabRect, tabs[i], _tabLabelStyle);
                
                if (Event.current.type == EventType.MouseDown && tabRect.Contains(Event.current.mousePosition))
                {
                    _tabIndices[tabKey] = i;
                    Event.current.Use();
                }
            }
            
            EditorGUILayout.Space(4);
            
            var selectedTab = tabs[_tabIndices[tabKey]];
            if (childrenByTab.TryGetValue(selectedTab, out var tabChildren))
                foreach (var child in tabChildren)
                    DrawNestedGroup(child, drawnFields);

            DrawButtonsForTab(node.Name, selectedTab);
            
            EditorGUILayout.Space(2);
            foreach (var field in node.Fields)
                drawnFields.Add(field.Name);
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(3);
        }
        
        private void DrawNestedBoxGroup(GroupNode node, HashSet<string> drawnFields)
        {
            EditorGUILayout.Space(3);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField(node.Name, EditorStyles.boldLabel);
            
            EditorGUILayout.Space(2);

            foreach (var field in node.Fields)
            {
                var prop = serializedObject.FindProperty(field.Name);
                if (prop != null)
                {
                    DrawPropertyWithConstrainedWidth(prop);
                    drawnFields.Add(field.Name);
                }
            }

            foreach (var child in node.Children)
                DrawNestedGroup(child, drawnFields);
            
            EditorGUILayout.Space(2);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
        }
        
        private void DrawNestedFoldoutGroup(GroupNode node, HashSet<string> drawnFields)
        {
            var foldoutKey = target.GetInstanceID() + "_" + node.FullPath;
            if (!_foldoutStates.ContainsKey(foldoutKey))
                _foldoutStates[foldoutKey] = node.DefaultExpanded;

            EditorGUILayout.Space(3);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            var headerRect = EditorGUILayout.GetControlRect(false, 20f);
            EditorGUI.DrawRect(headerRect, new Color(0.25f, 0.25f, 0.25f));
            EditorGUI.LabelField(new Rect(headerRect.x + 5f, headerRect.y, 14f, headerRect.height),
                _foldoutStates[foldoutKey] ? "▼" : "▶", _arrowStyle);
            EditorGUI.LabelField(new Rect(headerRect.x + 20f, headerRect.y, headerRect.width - 24f, headerRect.height),
                node.Name, _headerLabelStyle);
            
            if (Event.current.type == EventType.MouseDown && headerRect.Contains(Event.current.mousePosition))
            {
                _foldoutStates[foldoutKey] = !_foldoutStates[foldoutKey];
                Event.current.Use();
            }
            
            if (_foldoutStates[foldoutKey])
            {
                EditorGUILayout.Space(2);

                foreach (var field in node.Fields)
                {
                    var prop = serializedObject.FindProperty(field.Name);
                    if (prop != null)
                    {
                        DrawPropertyWithConstrainedWidth(prop);
                        drawnFields.Add(field.Name);
                    }
                }

                foreach (var child in node.Children)
                    DrawNestedGroup(child, drawnFields);

                EditorGUILayout.Space(2);
            }
            else
            {
                foreach (var field in node.Fields)
                    drawnFields.Add(field.Name);
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
        }
        
        private void DrawNestedHorizontalGroup(GroupNode node, HashSet<string> drawnFields)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 60f;

            foreach (var field in node.Fields)
            {
                var prop = serializedObject.FindProperty(field.Name);
                if (prop != null)
                {
                    EditorGUILayout.PropertyField(prop, true);
                    drawnFields.Add(field.Name);
                }
            }

            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
        }
        
        #endregion
        
        #region Button Drawing
        
        private void DrawButtons()
        {
            var cache = GetOrCreateCache();
            if (cache.ButtonMethods.Count == 0) return;

            var ungrouped = cache.ButtonMethods
                .Where(m => { var a = m.GetCustomAttribute<MM_ButtonAttribute>(); return string.IsNullOrEmpty(a.TabGroup) && string.IsNullOrEmpty(a.TabName); })
                .ToList();

            if (ungrouped.Count == 0) return;

            EditorGUILayout.Space(10);
            EditorGUILayout.BeginVertical(_buttonContainerStyle);
            DrawButtonList(ungrouped);
            EditorGUILayout.EndVertical();
        }

        private void DrawButtonList(List<MethodInfo> methods)
        {
            foreach (var group in methods.GroupBy(m => m.GetCustomAttribute<MM_ButtonGroupAttribute>()?.GroupName ?? ""))
            {
                if (string.IsNullOrEmpty(group.Key))
                {
                    foreach (var method in group) { DrawButton(method); EditorGUILayout.Space(2); }
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    foreach (var method in group) DrawButton(method);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(2);
                }
            }
        }
        
        private void DrawButtonsForTab(string tabGroup, string tabName)
        {
            var cache = GetOrCreateCache();
            if (cache.ButtonMethods.Count == 0) return;

            var tabButtons = cache.ButtonMethods
                .Where(m => { var a = m.GetCustomAttribute<MM_ButtonAttribute>(); return a.TabGroup == tabGroup && a.TabName == tabName; })
                .ToList();

            if (tabButtons.Count == 0) return;

            EditorGUILayout.Space(5);
            DrawButtonList(tabButtons);
        }
        
        private void DrawButton(MethodInfo method)
        {
            var attr = method.GetCustomAttribute<MM_ButtonAttribute>();
            var label = string.IsNullOrEmpty(attr.Label) ? ObjectNames.NicifyVariableName(method.Name) : attr.Label;

            var prevBg = GUI.backgroundColor;
            GUI.backgroundColor = new Color(0.85f, 0.85f, 0.85f);

            if (GUILayout.Button(label, _buttonDrawStyle, GUILayout.Height(attr.Height)))
            {
                var parameters = method.GetParameters();
                if (parameters.Length == 0)
                {
                    method.Invoke(target, null);
                }
                else
                {
                    try
                    {
                        method.Invoke(target, parameters.Select(p =>
                            p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType) : null).ToArray());
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"[MM_Button] Failed to invoke '{method.Name}': {e.Message}");
                    }
                }
                EditorUtility.SetDirty(target);
            }

            GUI.backgroundColor = prevBg;
        }
        
        #endregion
    }
}
