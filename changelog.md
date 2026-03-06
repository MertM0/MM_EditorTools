# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [0.1.0] - 06.03.2026

### Added

**Buttons**
- `[MM_Button]` — Invokes a method via a clickable Inspector button. Supports custom labels.
- `[MM_ButtonGroup("GroupName")]` — Renders multiple method buttons in a single horizontal row.

**Conditional**
- `[MM_ShowIf("field")]` — Shows a field only when a referenced bool field is `true`.
- `[MM_HideIf("field")]` — Hides a field when a referenced bool field is `true`.
- `[MM_EnableIf("field")]` — Makes a field interactive only when the condition is `true`.
- `[MM_DisableIf("field")]` — Disables a field when the condition is `true`.
- `[MM_ShowIfEnum("field", value)]` — Shows a field when an enum field matches the specified value.

**Decorators**
- `[MM_Title("Text")]` — Draws a bold header with an optional horizontal underline.
- `[MM_Separator]` — Draws a horizontal rule between Inspector fields.
- `[MM_InfoBox("Message", type)]` — Displays an Info, Warning, or Error message box.
- `[MM_Space(pixels)]` — Inserts vertical spacing above a field.

**Grouping**
- `[MM_BoxGroup("Name")]` — Wraps fields in a labeled box.
- `[MM_FoldoutGroup("Name")]` — Wraps fields in a collapsible foldout.
- `[MM_TabGroup("Name")]` — Organizes fields into named tabs.
- `[MM_HorizontalGroup("Name")]` — Arranges fields side-by-side horizontally.

**Validation**
- `[MM_Required]` — Flags string fields that are null or empty.
- `[MM_NotNull]` — Flags Object/Component references that are unassigned.
- `[MM_MinValue(min)]` — Clamps a numeric field to a minimum value.
- `[MM_MaxValue(max)]` — Clamps a numeric field to a maximum value.
- `[MM_Tag]` — Draws a tag dropdown populated from the project tag list.
- `[MM_Layer]` — Draws a layer dropdown populated from the project layer list.
- `[MM_Scene]` — Draws a dropdown of scenes registered in Build Settings.
- `[MM_ValidateInput("Method", "Message")]` — Shows a message when a custom validation method returns `false`.

**Visual**
- `[MM_ProgressBar(min, max, "Label")]` — Renders an editable color progress bar.
- `[MM_ReadOnly]` — Displays a field as non-editable in the Inspector.
- `[MM_LabelText("Label")]` — Overrides the display label of a field.
- `[MM_HideLabel]` — Removes the field label, giving the control full width.
- `[MM_ColorPalette]` — Adds preset color swatches alongside a Color field.
- `[MM_Preview]` — Renders a Texture2D/Sprite preview above the field.
- `[MM_Slider(min, max)]` — Replaces a numeric field with a labeled slider.
- `[MM_MinMaxSlider(min, max)]` — Draws a min/max range slider for a `Vector2` field.
- `[MM_ListDrawerSettings]` — Enhances arrays and lists with index labels and custom add/remove buttons.

**Utilities**
- `MM.EditorTools.EnhancedInspector.Prefix` namespace — Prefix-free aliases for all 32 attributes (`[Title]`, `[Required]`, etc.).
- Scene Validation window (`MM Tools → Validate Scene`) — Scans the active scene for failed validation attributes.

---

*Developed by [Mert Mutlu](https://github.com/mertm0)*
