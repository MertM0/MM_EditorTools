# MM Attributes

Unified inspector attributes and grouping layout engine for Unity. This package provides custom attributes to organize and validate your scripts directly inside the Unity Inspector without writing custom editors.

## Features

- **Drawers:** `[Dropdown]`, `[MinMaxSlider]`, `[ProgressBar]`, `[ReorderableList]`, `[ShowAssetPreview]`, `[HideLabel]`, etc.
- **Grouping:** `[BoxGroup]`, `[Foldout]`
- **Conditionals:** `[ShowIf]`, `[HideIf]`, `[EnableIf]`, `[DisableIf]`, `[ShowIfEnum]`
- **Validation:** `[Required]`, `[ValidateInput]`, `[MinValue]`, `[MaxValue]`, `[RequiredType]`
- **Decorators:** `[HorizontalLine]`, `[InfoBox]`
- **Buttons:** `[Button]` for drawing clickable inspector buttons linked to methods.
- **C# Properties:** `[ShowNonSerializedField]`, `[ShowNativeProperty]`

## Usage

Simply use the `MM.Attributes` namespace in your scripts:

```csharp
using UnityEngine;
using MM.Attributes;

public class MyComponent : MonoBehaviour
{
    [BoxGroup("General Info")]
    [InfoBox("This is a box group with custom attributes.", EInfoBoxType.Normal)]
    public string gameName = "My Game";

    [Foldout("Combat Settings")]
    [Required]
    public GameObject weaponPrefab;

    [Foldout("Combat Settings")]
    [MinMaxSlider(0f, 100f)]
    public Vector2 damageRange = new Vector2(10f, 50f);

    [Button("Say Hello")]
    private void SayHello()
    {
        Debug.Log("Hello!");
    }
}
```
