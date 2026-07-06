using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MM.Attributes.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class MMInspector : UnityEditor.Editor
    {
        private List<SerializedProperty> _serializedProperties = new List<SerializedProperty>();
        private IEnumerable<FieldInfo> _nonSerializedFields;
        private IEnumerable<PropertyInfo> _nativeProperties;
        private IEnumerable<MethodInfo> _methods;
        private Dictionary<string, SavedBool> _foldouts = new Dictionary<string, SavedBool>();

        protected virtual void OnEnable()
        {
            _nonSerializedFields = ReflectionUtility.GetAllFields(
                target, f => f.GetCustomAttributes(typeof(ShowNonSerializedFieldAttribute), true).Length > 0);

            _nativeProperties = ReflectionUtility.GetAllProperties(
                target, p => p.GetCustomAttributes(typeof(ShowNativePropertyAttribute), true).Length > 0);

            _methods = ReflectionUtility.GetAllMethods(
                target, m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0);
        }

        protected virtual void OnDisable()
        {
            ReorderableListPropertyDrawer.Instance.ClearCache();
        }

        public override void OnInspectorGUI()
        {
            GetSerializedProperties(ref _serializedProperties);

            bool anyMMAttribute = _serializedProperties.Any(p => PropertyUtility.HasMMAttribute(p));
            if (!anyMMAttribute)
            {
                DrawDefaultInspector();
            }
            else
            {
                DrawSerializedProperties();
            }

            DrawNonSerializedFields();
            DrawNativeProperties();
            DrawButtons();
        }

        protected void GetSerializedProperties(ref List<SerializedProperty> outSerializedProperties)
        {
            outSerializedProperties.Clear();
            using (var iterator = serializedObject.GetIterator())
            {
                if (iterator.NextVisible(true))
                {
                    do
                    {
                        outSerializedProperties.Add(serializedObject.FindProperty(iterator.name));
                    }
                    while (iterator.NextVisible(false));
                }
            }
        }

        protected void DrawSerializedProperties()
        {
            serializedObject.Update();

            HashSet<string> drawnProperties = new HashSet<string>();

            foreach (var property in _serializedProperties)
            {
                if (drawnProperties.Contains(property.name))
                {
                    continue;
                }

                BoxGroupAttribute boxGroupAttr = PropertyUtility.GetAttribute<BoxGroupAttribute>(property);
                if (boxGroupAttr != null)
                {
                    var groupName = boxGroupAttr.Name;
                    var groupProperties = _serializedProperties
                        .Where(p => {
                            var attr = PropertyUtility.GetAttribute<BoxGroupAttribute>(p);
                            return attr != null && attr.Name == groupName;
                        })
                        .ToList();

                    foreach (var p in groupProperties)
                    {
                        drawnProperties.Add(p.name);
                    }

                    var visibleProperties = groupProperties.Where(p => PropertyUtility.IsVisible(p));
                    if (visibleProperties.Any())
                    {
                        MMEditorGUI.BeginBoxGroup_Layout(groupName);
                        foreach (var p in visibleProperties)
                        {
                            MMEditorGUI.PropertyField_Layout(p, includeChildren: true);
                        }
                        MMEditorGUI.EndBoxGroup_Layout();
                    }

                    continue;
                }

                FoldoutAttribute foldoutAttr = PropertyUtility.GetAttribute<FoldoutAttribute>(property);
                if (foldoutAttr != null)
                {
                    var foldoutName = foldoutAttr.Name;
                    var foldoutProperties = _serializedProperties
                        .Where(p => {
                            var attr = PropertyUtility.GetAttribute<FoldoutAttribute>(p);
                            return attr != null && attr.Name == foldoutName;
                        })
                        .ToList();

                    foreach (var p in foldoutProperties)
                    {
                        drawnProperties.Add(p.name);
                    }

                    var visibleProperties = foldoutProperties.Where(p => PropertyUtility.IsVisible(p));
                    if (visibleProperties.Any())
                    {
                        if (!_foldouts.ContainsKey(foldoutName))
                        {
#if UNITY_6000_4_OR_NEWER
                            _foldouts[foldoutName] = new SavedBool($"{EntityId.ToULong(target.GetEntityId())}.{foldoutName}", false);
#else
                            _foldouts[foldoutName] = new SavedBool($"{target.GetInstanceID()}.{foldoutName}", false);
#endif
                        }

                        _foldouts[foldoutName].Value = EditorGUILayout.Foldout(_foldouts[foldoutName].Value, foldoutName, true);
                        if (_foldouts[foldoutName].Value)
                        {
                            foreach (var p in visibleProperties)
                            {
                                MMEditorGUI.PropertyField_Layout(p, true);
                            }
                        }
                    }

                    continue;
                }

                drawnProperties.Add(property.name);

                if (property.name.Equals("m_Script", System.StringComparison.Ordinal))
                {
                    using (new EditorGUI.DisabledScope(disabled: true))
                    {
                        EditorGUILayout.PropertyField(property);
                    }
                }
                else
                {
                    MMEditorGUI.PropertyField_Layout(property, includeChildren: true);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawNonSerializedFields(bool drawHeader = false)
        {
            if (_nonSerializedFields.Any())
            {
                if (drawHeader)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Non-Serialized Fields", GetHeaderGUIStyle());
                    MMEditorGUI.HorizontalLine(
                        EditorGUILayout.GetControlRect(false), HorizontalLineAttribute.DefaultHeight, HorizontalLineAttribute.DefaultColor.GetColor());
                }

                foreach (var field in _nonSerializedFields)
                {
                    MMEditorGUI.NonSerializedField_Layout(serializedObject.targetObject, field);
                }
            }
        }

        protected void DrawNativeProperties(bool drawHeader = false)
        {
            if (_nativeProperties.Any())
            {
                if (drawHeader)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Native Properties", GetHeaderGUIStyle());
                    MMEditorGUI.HorizontalLine(
                        EditorGUILayout.GetControlRect(false), HorizontalLineAttribute.DefaultHeight, HorizontalLineAttribute.DefaultColor.GetColor());
                }

                foreach (var property in _nativeProperties)
                {
                    MMEditorGUI.NativeProperty_Layout(serializedObject.targetObject, property);
                }
            }
        }

        protected void DrawButtons(bool drawHeader = false)
        {
            if (_methods.Any())
            {
                if (drawHeader)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Buttons", GetHeaderGUIStyle());
                    MMEditorGUI.HorizontalLine(
                        EditorGUILayout.GetControlRect(false), HorizontalLineAttribute.DefaultHeight, HorizontalLineAttribute.DefaultColor.GetColor());
                }

                foreach (var method in _methods)
                {
                    MMEditorGUI.Button(serializedObject.targetObject, method);
                }
            }
        }

        private static IEnumerable<SerializedProperty> GetNonGroupedProperties(IEnumerable<SerializedProperty> properties)
        {
            return properties.Where(p => PropertyUtility.GetAttribute<IGroupAttribute>(p) == null);
        }

        private static IEnumerable<IGrouping<string, SerializedProperty>> GetGroupedProperties(IEnumerable<SerializedProperty> properties)
        {
            return properties
                .Where(p => PropertyUtility.GetAttribute<BoxGroupAttribute>(p) != null)
                .GroupBy(p => PropertyUtility.GetAttribute<BoxGroupAttribute>(p).Name);
        }

        private static IEnumerable<IGrouping<string, SerializedProperty>> GetFoldoutProperties(IEnumerable<SerializedProperty> properties)
        {
            return properties
                .Where(p => PropertyUtility.GetAttribute<FoldoutAttribute>(p) != null)
                .GroupBy(p => PropertyUtility.GetAttribute<FoldoutAttribute>(p).Name);
        }

        private static GUIStyle GetHeaderGUIStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.UpperCenter;

            return style;
        }
    }
}
