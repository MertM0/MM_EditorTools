using UnityEngine;
using MM.Attributes;
using System.Collections.Generic;

namespace MM.Attributes.Examples
{
    public class MM_AttributesDemo : MonoBehaviour
    {
        [BoxGroup("General Info")]
        [InfoBox("This is a MM.Attributes demo.", EInfoBoxType.Normal)]
        public string gameName = "MM Project";

        [BoxGroup("General Info")]
        [ReadOnly]
        public float timeElapsed;

        [Foldout("Combat Settings")]
        [Required("We need a sword prefab!")]
        public GameObject weaponPrefab;

        [Foldout("Combat Settings")]
        [MinMaxSlider(0f, 100f)]
        public Vector2 damageRange = new Vector2(10f, 50f);

        [Foldout("Combat Settings")]
        [ProgressBar("Health", 100f, EColor.Red)]
        public float currentHealth = 75f;

        [Foldout("Combat Settings")]
        [ValidateInput("ValidateDamage", "Damage must be non-negative!")]
        public float attackDamage = 15f;

        [Foldout("Combat Settings")]
        [ReorderableList]
        public List<string> spellSlots = new List<string> { "Fireball", "Heal" };

        [BoxGroup("Conditional Setup")]
        public bool showAdvancedSettings;

        [BoxGroup("Conditional Setup")]
        [ShowIf("showAdvancedSettings")]
        public int advancedLevel = 5;

        [BoxGroup("Conditional Setup")]
        [HideIf("showAdvancedSettings")]
        public string simpleStatus = "Running in simple mode";

        [BoxGroup("Dropdown Selection")]
        [Dropdown("GetDifficulties")]
        public string selectedDifficulty = "Normal";

        private List<string> GetDifficulties()
        {
            return new List<string> { "Easy", "Normal", "Hard" };
        }

        [BoxGroup("Visual Elements")]
        [ShowAssetPreview]
        public Sprite characterSprite;

        [BoxGroup("Visual Elements")]
        [ResizableTextArea]
        public string description = "A detailed description goes here.";

        [BoxGroup("Unity Helpers")]
        [Tag]
        public string targetTag = "Player";

        [BoxGroup("Unity Helpers")]
        [Layer]
        public int collisionLayer = 0;

        [BoxGroup("Unity Helpers")]
        [Scene]
        public string nextScene = "";

        public enum DifficultyMode { Easy, Normal, Hard }

        [BoxGroup("New Features")]
        public DifficultyMode currentDifficulty;

        [BoxGroup("New Features")]
        [ShowIfEnum("currentDifficulty", DifficultyMode.Hard)]
        public float hardModeMultiplier = 2.0f;

        [BoxGroup("New Features")]
        [HideLabel]
        public string hiddenLabelField = "Look, no label!";

        [Button("Print Player Stats", EButtonEnableMode.Always)]
        private void PrintPlayerStats()
        {
            Debug.Log($"Name: {gameName}, Health: {currentHealth}, Difficulty: {selectedDifficulty}");
        }

        [Button("Randomize Health", EButtonEnableMode.Playmode)]
        private void RandomizeHealth()
        {
            currentHealth = Random.Range(0f, 100f);
        }

        private void Update()
        {
            timeElapsed += Time.deltaTime;
        }

        private bool ValidateDamage(float damage)
        {
            return damage >= 0f;
        }
    }
}
