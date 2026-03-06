using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Property drawer for MM_LabelTextAttribute.
    /// Shows custom label for fields.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_LabelTextAttribute))]
    public class MM_LabelTextDrawer : PropertyDrawer
    {
        #region Fields
        
        private MM_LabelTextAttribute LabelText => (MM_LabelTextAttribute)attribute;
        
        #endregion
        
        #region Unity Lifecycle
        
        /// <summary>
        /// Draws the property with custom label
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = LabelText.Label;
            EditorGUI.PropertyField(position, property, label, true);
        }
        
        #endregion
    }
}
