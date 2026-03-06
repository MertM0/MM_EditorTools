using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom drawer for MM_MinMaxSlider attribute.
    /// Shows a min-max slider for Vector2 values.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_MinMaxSliderAttribute))]
    public class MM_MinMaxSliderDrawer : PropertyDrawer
    {
        #region PropertyDrawer Override
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MM_MinMaxSliderAttribute minMaxSlider = attribute as MM_MinMaxSliderAttribute;
            
            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
            
            EditorGUI.BeginProperty(position, label, property);
            
            Vector2 value = property.vector2Value;
            float minValue = value.x;
            float maxValue = value.y;
            
            // Draw label
            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, label);
            
            // Draw min field
            Rect minRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, 50, EditorGUIUtility.singleLineHeight);
            minValue = EditorGUI.FloatField(minRect, minValue);
            
            // Draw slider
            Rect sliderRect = new Rect(position.x + EditorGUIUtility.labelWidth + 55, position.y, position.width - EditorGUIUtility.labelWidth - 115, EditorGUIUtility.singleLineHeight);
            EditorGUI.MinMaxSlider(sliderRect, ref minValue, ref maxValue, minMaxSlider.MinLimit, minMaxSlider.MaxLimit);
            
            // Draw max field
            Rect maxRect = new Rect(position.xMax - 50, position.y, 50, EditorGUIUtility.singleLineHeight);
            maxValue = EditorGUI.FloatField(maxRect, maxValue);
            
            // Clamp values
            minValue = Mathf.Clamp(minValue, minMaxSlider.MinLimit, maxValue);
            maxValue = Mathf.Clamp(maxValue, minValue, minMaxSlider.MaxLimit);
            
            property.vector2Value = new Vector2(minValue, maxValue);
            
            EditorGUI.EndProperty();
        }
        
        #endregion
    }
}
