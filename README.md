# MM_EditorTools

Custom Inspector attributes for Unity. 32 attributes covering grouping, validation, conditionals, decorators, buttons, and visual controls. Zero dependencies, works out of the box via UPM.

## Table of Contents

- [Installation](#installation)
- [Quick Start](#quick-start)
- [Attribute Reference](#attribute-reference)
  - [Buttons](#buttons)
  - [Conditional](#conditional)
  - [Decorators](#decorators)
  - [Grouping](#grouping)
  - [Validation](#validation)
  - [Visual](#visual)
- [Scene Validation Window](#scene-validation-window)
- [Package Structure](#package-structure)
- [License](#license)

## Installation

**Package Manager (Git URL)**

1. Open **Window > Package Manager**
2. Click **+** > **Add package from git URL...**
3. Paste:

```
https://github.com/mertm0/MM_EditorTools.git
```

**manifest.json**

```json
{
  "dependencies": {
    "com.mm.editortools": "https://github.com/mertm0/MM_EditorTools.git"
  }
}
```

## Quick Start

**With MM_ prefix**

```csharp
using MM.EditorTools.EnhancedInspector;

public class EnemyData : MonoBehaviour
{
    [MM_Title("Combat Settings")]
    [MM_Required] public string enemyName;
    [MM_MinValue(0)] public int health = 100;

    [MM_BoxGroup("Movement")]
    [MM_Slider(0f, 20f)] public float speed = 5f;
}
```

**Without prefix**

```csharp
using MM.EditorTools.EnhancedInspector.Prefix;

public class EnemyData : MonoBehaviour
{
    [Title("Combat Settings")]
    [Required] public string enemyName;
    [MinValue(0)] public int health = 100;

    [BoxGroup("Movement")]
    [Slider(0f, 20f)] public float speed = 5f;
}
```

Both namespaces are identical in behavior. Use whichever you prefer.

## Attribute Reference

### Buttons

**`[MM_Button]`**  Adds a button in the Inspector that calls the method.

```csharp
[MM_Button]
private void ResetDefaults() { }

[MM_Button("Reset to Defaults")]
private void ResetDefaults() { }
```

**`[MM_ButtonGroup("GroupName")]`**  Groups multiple method buttons in a single row.

```csharp
[MM_ButtonGroup("Actions")]
private void Save() { }

[MM_ButtonGroup("Actions")]
private void Load() { }
```

### Conditional

**`[MM_ShowIf("fieldName")]`**  Shows the field when the referenced bool is `true`.

```csharp
public bool isAdvanced;

[MM_ShowIf("isAdvanced")]
public float advancedMultiplier = 1f;
```

**`[MM_HideIf("fieldName")]`**  Hides the field when the referenced bool is `true`.

```csharp
public bool hideDetails;

[MM_HideIf("hideDetails")]
public string details;
```

**`[MM_EnableIf("fieldName")]`**  Makes the field interactive only when the condition is `true`.

```csharp
public bool canEdit;

[MM_EnableIf("canEdit")]
public int editableValue;
```

**`[MM_DisableIf("fieldName")]`**  Disables the field when the condition is `true`.

```csharp
public bool locked;

[MM_DisableIf("locked")]
public string editableString;
```

**`[MM_ShowIfEnum("fieldName", EnumValue)]`**  Shows the field when an enum field matches the given value.

```csharp
public EnemyType enemyType;

[MM_ShowIfEnum("enemyType", EnemyType.Ranged)]
public float attackRange = 10f;
```

### Decorators

**`[MM_Title("Text")]`**  Bold header with an optional underline.

```csharp
[MM_Title("Health Settings")]
public int maxHealth = 100;

[MM_Title("Health Settings", line: false)]
public int maxHealth = 100;
```

**`[MM_Separator]`**  Horizontal line between fields.

```csharp
public float speed;

[MM_Separator]
public float jumpForce;
```

**`[MM_InfoBox("Message", InfoBoxType.Info)]`**  Info, Warning, or Error message box above the field.

```csharp
[MM_InfoBox("Value must be positive.", InfoBoxType.Warning)]
public int damage;
```

**`[MM_Space(pixels)]`**  Adds vertical space above the field.

```csharp
[MM_Space(10)]
public float nextField;
```

### Grouping

**`[MM_BoxGroup("GroupName")]`**  Wraps fields in a labeled box.

```csharp
[MM_BoxGroup("Movement")]
public float walkSpeed;

[MM_BoxGroup("Movement")]
public float runSpeed;
```

**`[MM_FoldoutGroup("GroupName")]`**  Collapsible foldout section.

```csharp
[MM_FoldoutGroup("Debug Options")]
public bool showGizmos;

[MM_FoldoutGroup("Debug Options")]
public Color gizmoColor = Color.green;
```

**`[MM_TabGroup("TabName")]`**  Organizes fields into named tabs.

```csharp
[MM_TabGroup("Stats")]
public int health;

[MM_TabGroup("Combat")]
public int damage;
```

**`[MM_HorizontalGroup("GroupName")]`**  Places fields side-by-side.

```csharp
[MM_HorizontalGroup("Range")]
public float minRange;

[MM_HorizontalGroup("Range")]
public float maxRange;
```

### Validation

Validation attributes highlight issues directly in the Inspector. They are also picked up by the [Scene Validation window](#scene-validation-window).

**`[MM_Required]`**  Warns if a string field is null or empty.

```csharp
[MM_Required]
public string enemyName;
```

**`[MM_NotNull]`**  Warns if an Object/Component reference is unassigned.

```csharp
[MM_NotNull]
public Transform target;
```

**`[MM_MinValue(min)]`**  Clamps the field to a minimum value.

```csharp
[MM_MinValue(0)]
public int health = 100;
```

**`[MM_MaxValue(max)]`**  Clamps the field to a maximum value.

```csharp
[MM_MaxValue(100)]
public float fillAmount = 1f;
```

**`[MM_Tag]`**  Tag dropdown for string fields.

```csharp
[MM_Tag]
public string targetTag;
```

**`[MM_Layer]`**  Layer dropdown for int fields.

```csharp
[MM_Layer]
public int collisionLayer;
```

**`[MM_Scene]`**  Dropdown of scenes from Build Settings.

```csharp
[MM_Scene]
public string nextScene;
```

**`[MM_ValidateInput("MethodName", "Message")]`**  Runs a custom validation method and shows the message when it returns `false`.

```csharp
[MM_ValidateInput("IsPositive", "Value must be positive.")]
public int damage;

private bool IsPositive() => damage > 0;
```

### Visual

**`[MM_ProgressBar(min, max, "Label")]`**  Colored progress bar. Click to edit the value.

```csharp
[MM_ProgressBar(0, 100, "Health")]
public float health = 75f;
```

**`[MM_ReadOnly]`**  Displays the field as non-editable.

```csharp
[MM_ReadOnly]
public string guid;
```

**`[MM_LabelText("Custom Label")]`**  Overrides the field label.

```csharp
[MM_LabelText("Max HP")]
public int maxHealth;
```

**`[MM_HideLabel]`**  Removes the label, giving the control full width.

```csharp
[MM_HideLabel]
public AnimationCurve curve;
```

**`[MM_ColorPalette]`**  Adds preset color swatches next to a Color field.

```csharp
[MM_ColorPalette]
public Color uiAccentColor;
```

**`[MM_Preview]`**  Shows a texture/sprite preview above the field.

```csharp
[MM_Preview]
public Texture2D icon;
```

**`[MM_Slider(min, max)]`**  Slider control for numeric fields.

```csharp
[MM_Slider(0f, 1f)]
public float volume = 0.8f;
```

**`[MM_MinMaxSlider(min, max)]`**  Min/max range slider for a `Vector2` field (`x` = min, `y` = max).

```csharp
[MM_MinMaxSlider(0f, 100f)]
public Vector2 spawnRange = new Vector2(10f, 50f);
```

**`[MM_ListDrawerSettings]`**  Adds index labels and cleaner add/remove buttons to arrays and lists.

```csharp
[MM_ListDrawerSettings]
public List<AudioClip> playlist;
```

## Scene Validation Window

Open via **MM Tools > Validate Scene**. Scans the active scene for any `MM_Required`, `MM_NotNull`, or `MM_ValidateInput` failures and lists them. Clicking a result selects the GameObject.

## Package Structure

```
MM_EditorTools/
 package.json
 README.md
 changelog.md
 Runtime/
    Scripts/
        EnhancedInspector/
            Attributes/
            MM_AttributeAliases.cs
 Editor/
     Scripts/
         EnhancedInspector/
             MM_EnhancedInspector.cs
             Drawers/
             MM_SceneValidator.cs
```

## License

[MIT](LICENSE)  Mert Mutlu
