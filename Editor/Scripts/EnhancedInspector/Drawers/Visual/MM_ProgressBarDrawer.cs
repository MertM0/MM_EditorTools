using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_ProgressBarAttribute.
    /// Displays a visual progress bar for numeric values.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_ProgressBarAttribute))]
    public class MM_ProgressBarDrawer : PropertyDrawer
    {
        #region Fields
        
        private MM_ProgressBarAttribute ProgressBar => (MM_ProgressBarAttribute)attribute;
        private const float BAR_HEIGHT = 20f;
        private const float PADDING = 2f;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the progress bar
        /// </summary>
        private static GUIStyle _centeredStyle;

        private static void EnsureStyles()
        {
            if (_centeredStyle != null) return;
            _centeredStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, normal = { textColor = Color.white } };
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnsureStyles();
            float value = GetValue(property);
            float normalizedValue = Mathf.InverseLerp(ProgressBar.MinValue, ProgressBar.MaxValue, value);
            
            // Draw label
            string labelText = string.IsNullOrEmpty(ProgressBar.Label) ? label.text : ProgressBar.Label;
            Rect labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, labelText);
            
            // Draw progress bar
            Rect barRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + PADDING, 
                                    position.width, BAR_HEIGHT);
            
            // Draw background
            EditorGUI.DrawRect(barRect, new Color(0.2f, 0.2f, 0.2f, 0.5f));
            
            // Draw filled portion
            Rect fillRect = new Rect(barRect.x, barRect.y, barRect.width * normalizedValue, barRect.height);
            EditorGUI.DrawRect(fillRect, ProgressBar.Color);
            
            // Draw border
            Handles.BeginGUI();
            Handles.color = new Color(0.1f, 0.1f, 0.1f, 1f);
            Handles.DrawLine(new Vector3(barRect.x, barRect.y), new Vector3(barRect.xMax, barRect.y));
            Handles.DrawLine(new Vector3(barRect.x, barRect.yMax), new Vector3(barRect.xMax, barRect.yMax));
            Handles.DrawLine(new Vector3(barRect.x, barRect.y), new Vector3(barRect.x, barRect.yMax));
            Handles.DrawLine(new Vector3(barRect.xMax, barRect.y), new Vector3(barRect.xMax, barRect.yMax));
            Handles.EndGUI();

            EditorGUI.LabelField(barRect, $"{value:F1} / {ProgressBar.MaxValue:F1}", _centeredStyle);
            
            // Handle mouse input (click and drag)
            Event e = Event.current;
            if (barRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.MouseDown || (e.type == EventType.MouseDrag && e.button == 0))
                {
                    float clickPosition = (e.mousePosition.x - barRect.x) / barRect.width;
                    float newValue = Mathf.Lerp(ProgressBar.MinValue, ProgressBar.MaxValue, clickPosition);
                    SetValue(property, Mathf.Clamp(newValue, ProgressBar.MinValue, ProgressBar.MaxValue));
                    GUI.changed = true;
                    e.Use();
                }
                
                // Change cursor to resize horizontal
                EditorGUIUtility.AddCursorRect(barRect, MouseCursor.ResizeHorizontal);
            }
            
            // Draw editable field below
            Rect fieldRect = new Rect(position.x, barRect.y + barRect.height + PADDING, 
                                     position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.BeginChangeCheck();
            float newFieldValue = EditorGUI.FloatField(fieldRect, "Value", value);
            if (EditorGUI.EndChangeCheck())
            {
                SetValue(property, Mathf.Clamp(newFieldValue, ProgressBar.MinValue, ProgressBar.MaxValue));
            }
        }
        
        /// <summary>
        /// Get total height
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2 + BAR_HEIGHT + PADDING * 2;
        }
        
        #endregion
        
        #region Private Methods
        
        /// <summary>
        /// Gets numeric value from property
        /// </summary>
        private float GetValue(SerializedProperty property)
        {
            return property.propertyType switch
            {
                SerializedPropertyType.Integer => property.intValue,
                SerializedPropertyType.Float => property.floatValue,
                _ => 0f
            };
        }
        
        /// <summary>
        /// Sets numeric value to property
        /// </summary>
        private void SetValue(SerializedProperty property, float value)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    property.intValue = Mathf.RoundToInt(value);
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = value;
                    break;
            }
        }
        
        #endregion
    }
}
