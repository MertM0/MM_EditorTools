using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom drawer for MM_Slider attribute.
    /// Shows a slider for numeric values.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_SliderAttribute))]
    public class MM_SliderDrawer : PropertyDrawer
    {
        #region PropertyDrawer Override
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MM_SliderAttribute slider = attribute as MM_SliderAttribute;
            
            EditorGUI.BeginProperty(position, label, property);
            
            if (property.propertyType == SerializedPropertyType.Float)
            {
                property.floatValue = EditorGUI.Slider(position, label, property.floatValue, slider.MinValue, slider.MaxValue);
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUI.IntSlider(position, label, property.intValue, (int)slider.MinValue, (int)slider.MaxValue);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
            
            EditorGUI.EndProperty();
        }
        
        #endregion
    }
}
