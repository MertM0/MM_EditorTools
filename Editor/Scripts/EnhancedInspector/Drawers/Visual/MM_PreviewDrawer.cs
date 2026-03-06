using UnityEditor;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Custom drawer for MM_Preview attribute.
    /// Shows a preview thumbnail for assets.
    /// </summary>
    [CustomPropertyDrawer(typeof(MM_PreviewAttribute))]
    public class MM_PreviewDrawer : PropertyDrawer
    {
        #region PropertyDrawer Override
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MM_PreviewAttribute preview = attribute as MM_PreviewAttribute;
            
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
            
            EditorGUI.BeginProperty(position, label, property);
            
            // Draw property field
            Rect propertyRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(propertyRect, property, label);
            
            // Draw preview if object is not null
            if (property.objectReferenceValue != null)
            {
                Rect previewRect = new Rect(
                    position.x + EditorGUIUtility.labelWidth,
                    position.y + EditorGUIUtility.singleLineHeight + 4,
                    preview.Width,
                    preview.Height
                );
                
                Texture2D previewTexture = AssetPreview.GetAssetPreview(property.objectReferenceValue);
                
                if (previewTexture != null)
                {
                    GUI.DrawTexture(previewRect, previewTexture, ScaleMode.ScaleToFit);
                }
                else
                {
                    // Draw loading indicator
                    EditorGUI.LabelField(previewRect, "Loading preview...", EditorStyles.centeredGreyMiniLabel);
                }
            }
            
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            MM_PreviewAttribute preview = attribute as MM_PreviewAttribute;
            
            if (property.propertyType != SerializedPropertyType.ObjectReference || property.objectReferenceValue == null)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            
            return EditorGUIUtility.singleLineHeight + preview.Height + 8;
        }
        
        #endregion
    }
}
