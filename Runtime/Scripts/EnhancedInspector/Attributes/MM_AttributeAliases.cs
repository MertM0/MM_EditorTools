// MM_EditorTools - Prefix-Free Attribute Aliases
// This namespace provides clean, prefix-free access to all MM_EditorTools attributes.
// 
// Usage:
//   using MM.EditorTools.EnhancedInspector;         // with MM_ prefix
//   using MM.EditorTools.EnhancedInspector.Prefix;  // without MM_ prefix
//   
//   [TabGroup("Group", "Tab")]
//   [Required]
//   public GameObject myObject;
//
// This is functionally identical to using the MM_ prefix, just cleaner syntax!

using InfoType = MM.EditorTools.EnhancedInspector.InfoType;

namespace MM.EditorTools.EnhancedInspector.Prefix
{
    // ==================== GROUPING ATTRIBUTES ====================
    
    /// <summary>
    /// Alias for MM_TabGroupAttribute - Organize fields in tabs.
    /// </summary>
    public class TabGroupAttribute : MM.EditorTools.EnhancedInspector.MM_TabGroupAttribute
    {
        public TabGroupAttribute(string groupName, string tabName) 
            : base(groupName, tabName) { }
    }
    
    /// <summary>
    /// Alias for MM_FoldoutGroupAttribute - Group fields in collapsible foldout.
    /// </summary>
    public class FoldoutGroupAttribute : MM.EditorTools.EnhancedInspector.MM_FoldoutGroupAttribute
    {
        public FoldoutGroupAttribute(string path, bool defaultExpanded = true) 
            : base(path, defaultExpanded) { }
    }
    
    /// <summary>
    /// Alias for MM_BoxGroupAttribute - Group fields in styled box.
    /// </summary>
    public class BoxGroupAttribute : MM.EditorTools.EnhancedInspector.MM_BoxGroupAttribute
    {
        public BoxGroupAttribute(string path) : base(path) { }
    }
    
    /// <summary>
    /// Alias for MM_HorizontalGroupAttribute - Arrange fields horizontally.
    /// </summary>
    public class HorizontalGroupAttribute : MM.EditorTools.EnhancedInspector.MM_HorizontalGroupAttribute
    {
        public HorizontalGroupAttribute(string path, float labelWidth = 60f) 
            : base(path, labelWidth) { }
    }
    
    // ==================== VALIDATION ATTRIBUTES ====================
    
    /// <summary>
    /// Alias for MM_RequiredAttribute - Mark field as required.
    /// </summary>
    public class RequiredAttribute : MM.EditorTools.EnhancedInspector.MM_RequiredAttribute
    {
        public RequiredAttribute(string message = null) : base(message) { }
    }
    
    /// <summary>
    /// Alias for MM_NotNullAttribute - Display warning if reference is null.
    /// </summary>
    public class NotNullAttribute : MM.EditorTools.EnhancedInspector.MM_NotNullAttribute
    {
        public NotNullAttribute(string message = null) : base(message) { }
    }
    
    /// <summary>
    /// Alias for MM_MinValueAttribute - Clamp numeric value to minimum.
    /// </summary>
    public class MinValueAttribute : MM.EditorTools.EnhancedInspector.MM_MinValueAttribute
    {
        public MinValueAttribute(float min) : base(min) { }
    }
    
    /// <summary>
    /// Alias for MM_MaxValueAttribute - Clamp numeric value to maximum.
    /// </summary>
    public class MaxValueAttribute : MM.EditorTools.EnhancedInspector.MM_MaxValueAttribute
    {
        public MaxValueAttribute(float max) : base(max) { }
    }
    
    /// <summary>
    /// Alias for MM_TagAttribute - Tag dropdown for strings.
    /// </summary>
    public class TagAttribute : MM.EditorTools.EnhancedInspector.MM_TagAttribute
    {
        public TagAttribute() : base() { }
    }
    
    /// <summary>
    /// Alias for MM_LayerAttribute - Layer dropdown for integers.
    /// </summary>
    public class LayerAttribute : MM.EditorTools.EnhancedInspector.MM_LayerAttribute
    {
        public LayerAttribute() : base() { }
    }
    
    /// <summary>
    /// Alias for MM_SceneAttribute - Scene picker dropdown.
    /// </summary>
    public class SceneAttribute : MM.EditorTools.EnhancedInspector.MM_SceneAttribute
    {
        public SceneAttribute() : base() { }
    }
    
    /// <summary>
    /// Alias for MM_ValidateInputAttribute - Custom validation with method.
    /// </summary>
    public class ValidateInputAttribute : MM.EditorTools.EnhancedInspector.MM_ValidateInputAttribute
    {
        public ValidateInputAttribute(string methodName, string message = null) 
            : base(methodName, message) { }
    }
    
    // ==================== BUTTON ATTRIBUTES ====================
    
    /// <summary>
    /// Alias for MM_ButtonAttribute - Add Inspector button for method.
    /// </summary>
    public class ButtonAttribute : MM.EditorTools.EnhancedInspector.MM_ButtonAttribute
    {
        public ButtonAttribute(string label = null, int height = 40) 
            : base(label, height) { }
    }
    
    /// <summary>
    /// Alias for MM_ButtonGroupAttribute - Group buttons horizontally.
    /// </summary>
    public class ButtonGroupAttribute : MM.EditorTools.EnhancedInspector.MM_ButtonGroupAttribute
    {
        public ButtonGroupAttribute(string groupName) : base(groupName) { }
    }
    
    // ==================== DECORATOR ATTRIBUTES ====================
    
    /// <summary>
    /// Alias for MM_TitleAttribute - Styled header above field.
    /// </summary>
    public class TitleAttribute : MM.EditorTools.EnhancedInspector.MM_TitleAttribute
    {
        public TitleAttribute(string title, int fontSize = 14, bool drawLine = true) 
            : base(title, fontSize, drawLine) { }
    }
    
    /// <summary>
    /// Alias for MM_SeparatorAttribute - Horizontal separator line.
    /// </summary>
    public class SeparatorAttribute : MM.EditorTools.EnhancedInspector.MM_SeparatorAttribute
    {
        public SeparatorAttribute(float height = 1f) 
            : base(height) { }
        
        public SeparatorAttribute(float height, float r, float g, float b) 
            : base(height, r, g, b) { }
    }
    
    /// <summary>
    /// Alias for MM_InfoBoxAttribute - Information/Warning/Error message box.
    /// </summary>
    public class InfoBoxAttribute : MM.EditorTools.EnhancedInspector.MM_InfoBoxAttribute
    {
        public InfoBoxAttribute(string message, InfoType type = InfoType.Info) 
            : base(message, type) { }
    }
    
    /// <summary>
    /// Alias for MM_SpaceAttribute - Custom vertical spacing.
    /// </summary>
    public class SpaceAttribute : MM.EditorTools.EnhancedInspector.MM_SpaceAttribute
    {
        public SpaceAttribute(float height = 10f) : base(height) { }
    }
    
    // ==================== VISUAL ATTRIBUTES ====================
    
    /// <summary>
    /// Alias for MM_ProgressBarAttribute - Visual progress bar for numeric values.
    /// </summary>
    public class ProgressBarAttribute : MM.EditorTools.EnhancedInspector.MM_ProgressBarAttribute
    {
        public ProgressBarAttribute(float min, float max, string label = null) 
            : base(min, max, label) { }
    }
    
    /// <summary>
    /// Alias for MM_ReadOnlyAttribute - Display-only field.
    /// </summary>
    public class ReadOnlyAttribute : MM.EditorTools.EnhancedInspector.MM_ReadOnlyAttribute
    {
        public ReadOnlyAttribute() : base() { }
    }
    
    /// <summary>
    /// Alias for MM_LabelTextAttribute - Custom field label.
    /// </summary>
    public class LabelTextAttribute : MM.EditorTools.EnhancedInspector.MM_LabelTextAttribute
    {
        public LabelTextAttribute(string label) : base(label) { }
    }
    
    /// <summary>
    /// Alias for MM_HideLabelAttribute - Hide field label.
    /// </summary>
    public class HideLabelAttribute : MM.EditorTools.EnhancedInspector.MM_HideLabelAttribute
    {
        public HideLabelAttribute() : base() { }
    }
    
    /// <summary>
    /// Alias for MM_ColorPaletteAttribute - Color picker with predefined palette.
    /// </summary>
    public class ColorPaletteAttribute : MM.EditorTools.EnhancedInspector.MM_ColorPaletteAttribute
    {
        public ColorPaletteAttribute() : base() { }
    }
    
    /// <summary>
    /// Alias for MM_PreviewAttribute - Asset preview thumbnail.
    /// </summary>
    public class PreviewAttribute : MM.EditorTools.EnhancedInspector.MM_PreviewAttribute
    {
        public PreviewAttribute(float height = 64f) : base(height) { }
    }
    
    /// <summary>
    /// Alias for MM_SliderAttribute - Custom slider for numeric values.
    /// </summary>
    public class SliderAttribute : MM.EditorTools.EnhancedInspector.MM_SliderAttribute
    {
        public SliderAttribute(float min, float max) : base(min, max) { }
    }
    
    /// <summary>
    /// Alias for MM_MinMaxSliderAttribute - Min-max range slider for Vector2.
    /// </summary>
    public class MinMaxSliderAttribute : MM.EditorTools.EnhancedInspector.MM_MinMaxSliderAttribute
    {
        public MinMaxSliderAttribute(float min, float max) : base(min, max) { }
    }
    
    /// <summary>
    /// Alias for MM_ListDrawerSettingsAttribute - Customizes how lists/arrays are displayed.
    /// </summary>
    public class ListDrawerSettingsAttribute : MM.EditorTools.EnhancedInspector.MM_ListDrawerSettingsAttribute
    {
        public ListDrawerSettingsAttribute() : base() { }
    }
    
    // ==================== CONDITIONAL ATTRIBUTES ====================
    
    /// <summary>
    /// Alias for MM_ShowIfAttribute - Show field based on boolean condition.
    /// </summary>
    public class ShowIfAttribute : MM.EditorTools.EnhancedInspector.MM_ShowIfAttribute
    {
        public ShowIfAttribute(string conditionFieldName) : base(conditionFieldName) { }
    }
    
    /// <summary>
    /// Alias for MM_HideIfAttribute - Hide field based on boolean condition.
    /// </summary>
    public class HideIfAttribute : MM.EditorTools.EnhancedInspector.MM_HideIfAttribute
    {
        public HideIfAttribute(string conditionFieldName) : base(conditionFieldName) { }
    }
    
    /// <summary>
    /// Alias for MM_EnableIfAttribute - Enable field based on condition.
    /// </summary>
    public class EnableIfAttribute : MM.EditorTools.EnhancedInspector.MM_EnableIfAttribute
    {
        public EnableIfAttribute(string conditionFieldName) : base(conditionFieldName) { }
    }
    
    /// <summary>
    /// Alias for MM_DisableIfAttribute - Disable field based on condition.
    /// </summary>
    public class DisableIfAttribute : MM.EditorTools.EnhancedInspector.MM_DisableIfAttribute
    {
        public DisableIfAttribute(string conditionFieldName) : base(conditionFieldName) { }
    }
    
    /// <summary>
    /// Alias for MM_ShowIfEnumAttribute - Show field based on enum value.
    /// </summary>
    public class ShowIfEnumAttribute : MM.EditorTools.EnhancedInspector.MM_ShowIfEnumAttribute
    {
        public ShowIfEnumAttribute(string enumFieldName, object enumValue) 
            : base(enumFieldName, enumValue) { }
    }
}
