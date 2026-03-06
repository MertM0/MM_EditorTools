using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_InfoBoxAttribute.
    /// Displays an information/warning/error box above the field.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_InfoBoxAttribute))]
    public class MM_InfoBoxDrawer : DecoratorDrawer
    {
        private MM_InfoBoxAttribute InfoBox => (MM_InfoBoxAttribute)attribute;
        private const float PADDING = 8f;
        private const float MIN_BOX_HEIGHT = 30f;

        private static GUIStyle _iconStyle;
        private static GUIStyle _messageStyle;

        private static void EnsureStyles()
        {
            if (_iconStyle != null) return;
            _iconStyle = new GUIStyle(EditorStyles.label) { fontSize = 14, alignment = TextAnchor.MiddleCenter };
            _messageStyle = new GUIStyle(EditorStyles.label) { fontSize = 11, wordWrap = true, alignment = TextAnchor.MiddleLeft, normal = { textColor = new Color(0.9f, 0.9f, 0.9f) } };
        }

        public override void OnGUI(Rect position)
        {
            if (MM_EnhancedInspector.SkipDecorators) return;
            EnsureStyles();

            position.y += PADDING;
            position.height -= PADDING * 2;

            Color bgColor, borderColor, iconColor;
            string icon;

            switch (InfoBox.Type)
            {
                case InfoType.Warning:
                    bgColor = new Color(0.4f, 0.35f, 0.15f); borderColor = new Color(0.6f, 0.5f, 0.2f);
                    iconColor = new Color(1f, 0.85f, 0.3f); icon = "⚠"; break;
                case InfoType.Error:
                    bgColor = new Color(0.4f, 0.15f, 0.15f); borderColor = new Color(0.6f, 0.2f, 0.2f);
                    iconColor = new Color(1f, 0.4f, 0.4f); icon = "✖"; break;
                case InfoType.Success:
                    bgColor = new Color(0.15f, 0.35f, 0.15f); borderColor = new Color(0.2f, 0.5f, 0.2f);
                    iconColor = new Color(0.4f, 0.9f, 0.4f); icon = "✔"; break;
                default:
                    bgColor = new Color(0.2f, 0.3f, 0.4f); borderColor = new Color(0.3f, 0.45f, 0.6f);
                    iconColor = new Color(0.5f, 0.75f, 1f); icon = "ℹ"; break;
            }

            EditorGUI.DrawRect(position, bgColor);
            EditorGUI.DrawRect(new Rect(position.x, position.y, 3f, position.height), borderColor);

            _iconStyle.normal.textColor = iconColor;
            EditorGUI.LabelField(new Rect(position.x + 8f, position.y, 20f, position.height), icon, _iconStyle);
            EditorGUI.LabelField(new Rect(position.x + 28f, position.y, position.width - 36f, position.height), InfoBox.Message, _messageStyle);
        }

        public override float GetHeight()
        {
            if (MM_EnhancedInspector.SkipDecorators) return 0f;
            int lines = Mathf.Max(1, InfoBox.Message.Length / 60);
            return Mathf.Max(lines * 20f + 20f, MIN_BOX_HEIGHT) + PADDING * 2;
        }
    }
}
