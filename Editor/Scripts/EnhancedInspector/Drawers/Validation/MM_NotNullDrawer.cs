using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom drawer for MM_NotNull attribute.
    /// Shows warning if reference is null.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_NotNullAttribute))]
    public class MM_NotNullDrawer : PropertyDrawer
    {
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
            var notNull = attribute as MM_NotNullAttribute;
            var propertyRect = new Rect(position.x, position.y, position.width, EditorGUI.GetPropertyHeight(property, label, true));
            EditorGUI.PropertyField(propertyRect, property, label, true);

            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
            {
                EnsureStyles();
                float propHeight = EditorGUI.GetPropertyHeight(property, label, true);
                var warningRect = new Rect(position.x, position.y + propHeight + 4, position.width, EditorGUIUtility.singleLineHeight * 2);
                EditorGUI.DrawRect(warningRect, new Color(0.45f, 0.35f, 0.15f));
                EditorGUI.DrawRect(new Rect(warningRect.x, warningRect.y, 3f, warningRect.height), new Color(0.7f, 0.5f, 0.2f));
                EditorGUI.LabelField(new Rect(warningRect.x + 8f, warningRect.y, 20f, warningRect.height), "⚠", _iconStyle);
                EditorGUI.LabelField(new Rect(warningRect.x + 32f, warningRect.y, warningRect.width - 38f, warningRect.height), notNull.Message, _messageStyle);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUI.GetPropertyHeight(property, label, true);
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
                height += EditorGUIUtility.singleLineHeight * 2 + 4;
            return height;
        }
    }
}
