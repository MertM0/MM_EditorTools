using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom PropertyDrawer for lists/arrays with Odin Inspector-style rendering.
    /// Provides drag-and-drop reordering, clean UI, and customization options.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_ListDrawerSettingsAttribute))]
    public class MM_ListDrawerSettingsDrawer : PropertyDrawer
    {
        #region Fields
        
        private static readonly Dictionary<string, ReorderableList> _listsByPropertyPath = new Dictionary<string, ReorderableList>();

        private static GUIStyle _headerLabelStyle;
        private static GUIStyle _addButtonStyle;
        private static GUIStyle _indexLabelStyle;
        private static GUIStyle _removeButtonStyle;

        private static void EnsureStyles()
        {
            if (_headerLabelStyle != null) return;
            _headerLabelStyle = new GUIStyle(EditorStyles.boldLabel) { fontSize = 11, normal = { textColor = new Color(0.9f, 0.9f, 0.9f) } };
            _addButtonStyle = new GUIStyle(GUI.skin.button) { fontSize = 16, fontStyle = FontStyle.Bold };
            _indexLabelStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, normal = { textColor = new Color(0.6f, 0.6f, 0.6f) } };
            _removeButtonStyle = new GUIStyle(GUI.skin.button) { fontSize = 14, fontStyle = FontStyle.Bold };
        }
        
        #endregion
        
        #region PropertyDrawer Methods
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isArray)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
            
            ReorderableList list = GetListForProperty(property);
            
            if (list != null)
            {
                return list.GetHeight();
            }
            
            return EditorGUIUtility.singleLineHeight;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnsureStyles();
            if (!property.isArray)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }
            
            ReorderableList list = GetListForProperty(property);
            
            if (list != null)
            {
                list.DoList(position);
            }
        }
        
        #endregion
        
        #region Initialization
        
        private ReorderableList GetListForProperty(SerializedProperty property)
        {
            string key = property.propertyPath;
            
            if (!_listsByPropertyPath.ContainsKey(key))
            {
                _listsByPropertyPath[key] = CreateList(property);
            }
            
            return _listsByPropertyPath[key];
        }
        
        private ReorderableList CreateList(SerializedProperty property)
        {
            var settings = attribute as MM_ListDrawerSettingsAttribute;
            
            var list = new ReorderableList(
                property.serializedObject,
                property,
                settings?.DraggableItems ?? true,
                true, // drawHeader
                false, // displayAdd - we'll draw custom
                false  // displayRemove - we'll draw custom
            );
            
            // Remove footer completely (Odin style - no bottom buttons)
            list.footerHeight = 0;
            
            // Custom header with + button
            list.drawHeaderCallback = (Rect rect) =>
            {
                DrawListHeader(rect, property, settings);
            };
            
            // Custom element with X button
            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                DrawListElement(rect, property, index, settings);
            };
            
            // Custom element height
            list.elementHeightCallback = (int index) =>
            {
                return GetElementHeight(property, index, settings);
            };
            
            return list;
        }
        
        #endregion
        
        #region Drawing Methods
        
        private void DrawListHeader(Rect rect, SerializedProperty property, MM_ListDrawerSettingsAttribute settings)
        {
            EnsureStyles();
            EditorGUI.DrawRect(rect, new Color(0.22f, 0.22f, 0.22f));

            var headerLabel = settings?.ListLabel ?? property.displayName;
            if (settings?.ShowItemCount ?? true)
                headerLabel += $" ({property.arraySize})";

            EditorGUI.LabelField(new Rect(rect.x + 10f, rect.y, rect.width - 50f, rect.height), headerLabel, _headerLabelStyle);

            if (!(settings?.HideAddButton ?? false))
            {
                GUI.backgroundColor = new Color(0.3f, 0.7f, 0.3f);
                if (GUI.Button(new Rect(rect.xMax - 35f, rect.y + 2f, 30f, rect.height - 4f), "+", _addButtonStyle))
                {
                    property.arraySize++;
                    property.serializedObject.ApplyModifiedProperties();
                }
                GUI.backgroundColor = Color.white;
            }
        }
        
        private void DrawListElement(Rect rect, SerializedProperty property, int index, MM_ListDrawerSettingsAttribute settings)
        {
            if (index >= property.arraySize || index < 0)
                return;
            
            SerializedProperty element = property.GetArrayElementAtIndex(index);
            
            // Adjust rect for padding
            rect.y += 2f;
            rect.height -= 4f;
            
            // Reserve space for X button on the right (Odin style)
            float removeButtonWidth = 25f;
            float contentWidth = rect.width - removeButtonWidth - 5f;
            
            // Draw index label if enabled
            if (settings?.ShowIndexLabels ?? true)
            {
                EditorGUI.LabelField(new Rect(rect.x, rect.y, 30f, EditorGUIUtility.singleLineHeight), index.ToString(), _indexLabelStyle);
                rect.x += 35f;
                contentWidth -= 35f;
            }
            
            // Draw element content
            Rect contentRect = new Rect(rect.x, rect.y, contentWidth, rect.height);
            
            if (settings?.CompactMode ?? false)
            {
                EditorGUI.PropertyField(contentRect, element, GUIContent.none, false);
            }
            else
            {
                EditorGUI.PropertyField(contentRect, element, GUIContent.none, true);
            }
            
            // Draw X button on the right (Odin style)
            if (!(settings?.HideRemoveButton ?? false))
            {
                GUI.backgroundColor = new Color(0.8f, 0.3f, 0.3f);
                if (GUI.Button(new Rect(rect.xMax - removeButtonWidth, rect.y, removeButtonWidth, EditorGUIUtility.singleLineHeight), "×", _removeButtonStyle))
                {
                    property.DeleteArrayElementAtIndex(index);
                    property.serializedObject.ApplyModifiedProperties();
                }
                GUI.backgroundColor = Color.white;
            }
        }
        
        private float GetElementHeight(SerializedProperty property, int index, MM_ListDrawerSettingsAttribute settings)
        {
            if (index >= property.arraySize || index < 0)
                return EditorGUIUtility.singleLineHeight;
            
            SerializedProperty element = property.GetArrayElementAtIndex(index);
            
            float height;
            if (settings?.CompactMode ?? false)
            {
                height = EditorGUIUtility.singleLineHeight;
            }
            else
            {
                height = EditorGUI.GetPropertyHeight(element, GUIContent.none, true);
            }
            
            return height + 4f; // Add padding
        }
        
        #endregion
    }
}
