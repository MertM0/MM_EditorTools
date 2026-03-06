using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom drawer for MM_ColorPalette attribute.
    /// Shows a color picker with predefined palette.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_ColorPaletteAttribute))]
    public class MM_ColorPaletteDrawer : PropertyDrawer
    {
        #region PropertyDrawer Override
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MM_ColorPaletteAttribute palette = attribute as MM_ColorPaletteAttribute;
            
            if (property.propertyType != SerializedPropertyType.Color)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
            
            EditorGUI.BeginProperty(position, label, property);
            
            // Draw label
            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, label);
            
            // Draw color field
            Rect colorRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
            property.colorValue = EditorGUI.ColorField(colorRect, property.colorValue);
            
            // Draw palette buttons
            float buttonSize = 20f;
            float spacing = 2f;
            float startY = position.y + EditorGUIUtility.singleLineHeight + 4;
            float startX = position.x + EditorGUIUtility.labelWidth;
            
            for (int i = 0; i < palette.PaletteColors.Length; i++)
            {
                float x = startX + (i % 9) * (buttonSize + spacing);
                float y = startY + (i / 9) * (buttonSize + spacing);
                
                Rect buttonRect = new Rect(x, y, buttonSize, buttonSize);
                
                // Draw color directly
                EditorGUI.DrawRect(buttonRect, palette.PaletteColors[i]);
                
                // Draw clickable button overlay
                if (GUI.Button(buttonRect, "", GUIStyle.none))
                {
                    property.colorValue = palette.PaletteColors[i];
                }
                
                // Draw border
                Rect borderRect = new Rect(buttonRect.x - 1, buttonRect.y - 1, buttonSize + 2, buttonSize + 2);
                EditorGUI.DrawRect(new Rect(borderRect.x, borderRect.y, borderRect.width, 1), Color.black);
                EditorGUI.DrawRect(new Rect(borderRect.x, borderRect.yMax - 1, borderRect.width, 1), Color.black);
                EditorGUI.DrawRect(new Rect(borderRect.x, borderRect.y, 1, borderRect.height), Color.black);
                EditorGUI.DrawRect(new Rect(borderRect.xMax - 1, borderRect.y, 1, borderRect.height), Color.black);
            }
            
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            MM_ColorPaletteAttribute palette = attribute as MM_ColorPaletteAttribute;
            
            if (property.propertyType != SerializedPropertyType.Color)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            
            // Calculate rows needed for palette
            int rows = (palette.PaletteColors.Length + 8) / 9;
            return EditorGUIUtility.singleLineHeight + 4 + (rows * 22f);
        }
        
        #endregion
    }
}
