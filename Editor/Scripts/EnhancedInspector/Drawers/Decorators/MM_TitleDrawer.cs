using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_TitleAttribute.
    /// Draws a title header in the Inspector.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_TitleAttribute))]
    public class MM_TitleDrawer : DecoratorDrawer
    {
        private MM_TitleAttribute Title => (MM_TitleAttribute)attribute;

        private static GUIStyle _titleStyle;

        private static void EnsureStyles()
        {
            if (_titleStyle != null) return;
            _titleStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleLeft,
                normal = { textColor = new Color(0.9f, 0.9f, 0.9f) }
            };
        }

        public override void OnGUI(Rect position)
        {
            EnsureStyles();
            position.y += 10;
            position.height = 22;

            _titleStyle.fontSize = Title.FontSize;
            EditorGUI.LabelField(position, Title.Title, _titleStyle);

            if (Title.DrawLine)
            {
                EditorGUI.DrawRect(new Rect(position.x, position.y + 22, position.width, 2),
                    new Color(0.38f, 0.38f, 0.38f, 0.85f));
            }
        }

        public override float GetHeight() => Title.DrawLine ? 35 : 30;
    }
}
