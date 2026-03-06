using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_RequiredAttribute.
    /// Shows a warning if the field is null or empty.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_RequiredAttribute))]
    public class MM_RequiredDrawer : PropertyDrawer
    {
        private MM_RequiredAttribute Required => (MM_RequiredAttribute)attribute;
        private const float HELP_BOX_HEIGHT = 40f;

        private static GUIStyle _iconStyle;
        private static GUIStyle _messageStyle;

        private static void EnsureStyles()
        {
            if (_iconStyle != null) return;
            _iconStyle = new GUIStyle(EditorStyles.label) { fontSize = 14, alignment = TextAnchor.MiddleCenter, normal = { textColor = new Color(1f, 0.85f, 0.3f) } };
            _messageStyle = new GUIStyle(EditorStyles.label) { fontSize = 11, wordWrap = true, alignment = TextAnchor.MiddleLeft, normal = { textColor = new Color(0.95f, 0.95f, 0.95f) } };
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnsureStyles();
            var propertyRect = position;

            if (IsEmpty(property))
            {
                propertyRect.height = EditorGUI.GetPropertyHeight(property, label, true);

                var warningRect = new Rect(position.x, position.y + propertyRect.height + 4, position.width, HELP_BOX_HEIGHT);
                EditorGUI.DrawRect(warningRect, new Color(0.45f, 0.35f, 0.15f));
                EditorGUI.DrawRect(new Rect(warningRect.x, warningRect.y, 3f, warningRect.height), new Color(0.7f, 0.5f, 0.2f));
                EditorGUI.LabelField(new Rect(warningRect.x + 8f, warningRect.y, 20f, warningRect.height), "⚠", _iconStyle);
                EditorGUI.LabelField(new Rect(warningRect.x + 32f, warningRect.y, warningRect.width - 38f, warningRect.height), Required.Message, _messageStyle);
            }

            EditorGUI.PropertyField(propertyRect, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float baseHeight = EditorGUI.GetPropertyHeight(property, label, true);
            return IsEmpty(property) ? baseHeight + HELP_BOX_HEIGHT + 4 : baseHeight;
        }

        private bool IsEmpty(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference: return property.objectReferenceValue == null;
                case SerializedPropertyType.String:          return string.IsNullOrEmpty(property.stringValue);
                case SerializedPropertyType.ArraySize:       return property.arraySize == 0;
                default:                                     return false;
            }
        }
    }
}
