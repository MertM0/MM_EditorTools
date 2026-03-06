using UnityEngine;
using MM.EditorTools.EnhancedInspector;
using System.Collections.Generic;
using static MM.EditorTools.EnhancedInspector.InfoType;

namespace MM.EditorTools.Examples
{
    /// <summary>
    /// Comprehensive example demonstrating all MM_EditorTools features.
    /// Organized into Upper and Lower groups with category tabs.
    /// </summary>
    public class MM_EditorToolsExample : MonoBehaviour
    {
        #region Upper Group - Grouping Tab
        
        [MM_TabGroup("Upper", "Grouping")]
        [MM_InfoBox("TabGroups, FoldoutGroups, BoxGroups, and HorizontalGroups.", InfoType.Info)]
        [MM_FoldoutGroup("Upper/Grouping/Foldouts", false)]
        public int foldoutExample1 = 10;
        
        [MM_TabGroup("Upper", "Grouping")]
        [MM_FoldoutGroup("Upper/Grouping/Foldouts")]
        public float foldoutExample2 = 5.5f;
        
        [MM_TabGroup("Upper", "Grouping")]
        [MM_FoldoutGroup("Upper/Grouping/Foldouts")]
        public bool foldoutExample3 = true;
        
        [MM_TabGroup("Upper", "Grouping")]
        [MM_BoxGroup("Upper/Grouping/Boxed")]
        public string boxField1 = "Field 1";
        
        [MM_TabGroup("Upper", "Grouping")]
        [MM_BoxGroup("Upper/Grouping/Boxed")]
        public int boxField2 = 42;
        
        [MM_TabGroup("Upper", "Grouping")]
        [MM_HorizontalGroup("Upper/Grouping/Horizontal")]
        public float horizontal1 = 10f;
        
        [MM_TabGroup("Upper", "Grouping")]
        [MM_HorizontalGroup("Upper/Grouping/Horizontal")]
        public float horizontal2 = 20f;
        
        [MM_TabGroup("Upper", "Grouping")]
        [MM_HorizontalGroup("Upper/Grouping/Horizontal")]
        public float horizontal3 = 30f;
        
        #endregion
        
        #region Upper Group - Validation Tab
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_InfoBox("Validation attributes ensure proper data assignment.", InfoType.Info)]
        [MM_FoldoutGroup("Upper/Validation/Required", false)]
        [MM_Required("Required!")]
        public GameObject playerRef;
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_FoldoutGroup("Upper/Validation/Required")]
        [MM_Required]
        public Transform spawnPoint;
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_FoldoutGroup("Upper/Validation/NotNull", false)]
        [MM_NotNull("Cannot be null!")]
        public Transform targetRef;
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_FoldoutGroup("Upper/Validation/NotNull")]
        [MM_NotNull]
        public Camera mainCamera;
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_FoldoutGroup("Upper/Validation/Custom", false)]
        [MM_ValidateInput("IsPositive", "Must be positive!")]
        public int validatedNumber = 10;
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_FoldoutGroup("Upper/Validation/Custom")]
        [MM_ValidateInput("IsInRange", "Must be 0-100!")]
        public float validatedFloat = 50f;
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_FoldoutGroup("Upper/Validation/Unity", false)]
        [MM_Scene]
        public string sceneRef = "";
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_FoldoutGroup("Upper/Validation/Unity")]
        [MM_Tag]
        public string tagSelect = "Player";
        
        [MM_TabGroup("Upper", "Validation")]
        [MM_FoldoutGroup("Upper/Validation/Unity")]
        [MM_Layer]
        public int layerSelect = 0;
        
        #endregion
        
        #region Upper Group - Conditional Tab
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_InfoBox("Conditional visibility based on field values.", InfoType.Info)]
        [MM_FoldoutGroup("Upper/Conditional/ShowIf", false)]
        public bool showFields = false;
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/ShowIf")]
        [MM_ShowIf("showFields")]
        public string conditionalField = "Visible!";
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/ShowIf")]
        [MM_ShowIf("showFields")]
        public int conditionalNumber = 42;
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/HideIf", false)]
        public bool hideFields = false;
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/HideIf")]
        [MM_HideIf("hideFields")]
        public string hiddenField = "Hidden when checked";
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/Enum", false)]
        public WeaponType weaponType = WeaponType.Melee;
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/Enum")]
        [MM_ShowIfEnum("weaponType", WeaponType.Melee)]
        public float meleeRange = 2f;
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/Enum")]
        [MM_ShowIfEnum("weaponType", WeaponType.Ranged)]
        public int ammoCount = 30;
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/Enum")]
        [MM_ShowIfEnum("weaponType", WeaponType.Magic)]
        public float manaPool = 100f;
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/Enable", false)]
        public bool lockSettings = false;
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/Enable")]
        [MM_EnableIf("lockSettings")]
        public string password = "admin";
        
        [MM_TabGroup("Upper", "Conditional")]
        [MM_FoldoutGroup("Upper/Conditional/Enable")]
        [MM_DisableIf("lockSettings")]
        public int maxPlayers = 4;
        
        #endregion
        
        #region Lower Group - Decorators Tab
        
        [MM_TabGroup("Lower", "Decorators")]
        [MM_InfoBox("Decorators add visual elements.", InfoType.Info)]
        [MM_FoldoutGroup("Lower/Decorators/InfoBox", false)]
        [MM_InfoBox("Informational message.", InfoType.Info)]
        public int infoValue = 100;
        
        [MM_TabGroup("Lower", "Decorators")]
        [MM_FoldoutGroup("Lower/Decorators/InfoBox")]
        [MM_InfoBox("Warning message!", InfoType.Warning)]
        public int warningValue = 50;
        
        [MM_TabGroup("Lower", "Decorators")]
        [MM_FoldoutGroup("Lower/Decorators/InfoBox")]
        [MM_InfoBox("Error message!", InfoType.Error)]
        public int errorValue = 0;
        
        [MM_TabGroup("Lower", "Decorators")]
        [MM_FoldoutGroup("Lower/Decorators/Titles", false)]
        [MM_Title("Character Settings")]
        public int characterLevel = 1;
        
        [MM_TabGroup("Lower", "Decorators")]
        [MM_FoldoutGroup("Lower/Decorators/Titles")]
        [MM_Space(20)]
        public int experience = 0;
        
        [MM_TabGroup("Lower", "Decorators")]
        [MM_FoldoutGroup("Lower/Decorators/ReadOnly", false)]
        [MM_ReadOnly]
        public int readOnlyScore = 100;
        
        [MM_TabGroup("Lower", "Decorators")]
        [MM_FoldoutGroup("Lower/Decorators/ReadOnly")]
        [MM_ReadOnly]
        public float readOnlyTime = 0f;
        
        #endregion
        
        #region Lower Group - Visual Tab
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_InfoBox("Visual attributes enhance the Inspector.", InfoType.Info)]
        [MM_FoldoutGroup("Lower/Visual/Colors", false)]
        [MM_ColorPalette]
        public Color teamColor = new Color(0.2f, 0.8f, 0.3f, 1f);
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/Colors")]
        [MM_ColorPalette]
        public Color enemyColor = new Color(0.9f, 0.2f, 0.2f, 1f);
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/Preview", false)]
        [MM_Preview(80, 80)]
        public Sprite characterIcon;
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/Preview")]
        [MM_Preview(64, 64)]
        public Texture2D weaponTexture;
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/Progress", false)]
        [MM_ProgressBar(0, 100, "Health")]
        public float healthBar = 75f;
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/Progress")]
        [MM_ProgressBar(0, 1, "Load")]
        public float loadProgress = 0.65f;
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/Sliders", false)]
        [MM_Slider(0, 100)]
        public float volume = 50f;
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/Sliders")]
        [MM_Slider(1, 10)]
        public int difficulty = 5;
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/MinMax", false)]
        [MM_MinMaxSlider(0, 100)]
        public Vector2 damageRange = new Vector2(10, 50);
        
        [MM_TabGroup("Lower", "Visual")]
        [MM_FoldoutGroup("Lower/Visual/MinMax")]
        [MM_MinMaxSlider(0, 10)]
        public Vector2 speedRange = new Vector2(2f, 8f);
        
        #endregion
        
        #region Lower Group - Lists Tab
        
        [MM_TabGroup("Lower", "Lists")]
        [MM_InfoBox("Standard Unity lists.", InfoType.Info)]
        [MM_FoldoutGroup("Lower/Lists/Basic", false)]
        public List<string> playerNames = new List<string> { "Player1", "Player2" };
        
        [MM_TabGroup("Lower", "Lists")]
        [MM_FoldoutGroup("Lower/Lists/Basic")]
        public List<int> inventorySlots = new List<int> { 10, 20, 30 };
        
        [MM_TabGroup("Lower", "Lists")]
        [MM_FoldoutGroup("Lower/Lists/References", false)]
        public List<GameObject> enemyPrefabs = new List<GameObject>();
        
        [MM_TabGroup("Lower", "Lists")]
        [MM_FoldoutGroup("Lower/Lists/References")]
        public List<Transform> waypoints = new List<Transform>();
        
        [MM_TabGroup("Lower", "Lists")]
        [MM_FoldoutGroup("Lower/Lists/Data", false)]
        public List<EnemyData> enemyDatabase = new List<EnemyData>
        {
            new EnemyData { enemyName = "Goblin", health = 50, damage = 10 }
        };
        
        [MM_TabGroup("Lower", "Lists")]
        [MM_FoldoutGroup("Lower/Lists/Data")]
        public List<ItemData> itemInventory = new List<ItemData>
        {
            new ItemData { itemName = "Sword", stackSize = 1, price = 100f }
        };
        
        #endregion
        
        #region Lower Group - Buttons Tab
        
        [MM_TabGroup("Lower", "Buttons")]
        [MM_InfoBox("Methods exposed as Inspector buttons.", InfoType.Info)]
        [MM_FoldoutGroup("Lower/Buttons/General", false)]
        public int buttonTestValue = 0;
        
        #endregion
        
        #region Unity Lifecycle
        
        private void Update()
        {
            readOnlyTime += Time.deltaTime;
        }
        
        #endregion
        
        #region Button Methods - Upper/Validation
        
        [MM_Button("🔍 Validate All", TabGroup = "Upper", TabName = "Validation")]
        public void ValidateAll()
        {
            Debug.Log("Validating all references...");
            if (playerRef == null) Debug.LogWarning("Player Ref is null!");
            if (spawnPoint == null) Debug.LogWarning("Spawn Point is null!");
            if (targetRef == null) Debug.LogWarning("Target Ref is null!");
            if (mainCamera == null) Debug.LogWarning("Main Camera is null!");
        }
        
        #endregion
        
        #region Button Methods - Lower/Decorators
        
        [MM_Button("Reset Score", TabGroup = "Lower", TabName = "Decorators")]
        public void ResetScore()
        {
            readOnlyScore = 0;
            readOnlyTime = 0f;
            Debug.Log("Score reset!");
        }
        
        #endregion
        
        #region Button Methods - Lower/Visual
        
        [MM_Button("Randomize Health", TabGroup = "Lower", TabName = "Visual")]
        public void RandomizeHealth()
        {
            healthBar = Random.Range(0f, 100f);
            loadProgress = Random.Range(0f, 1f);
        }
        
        #endregion
        
        #region Button Methods - Lower/Lists
        
        [MM_ButtonGroup("ListActions")]
        [MM_Button("Clear Lists", TabGroup = "Lower", TabName = "Lists")]
        private void ClearLists()
        {
            playerNames.Clear();
            enemyDatabase.Clear();
            itemInventory.Clear();
            Debug.Log("Lists cleared!");
        }
        
        [MM_ButtonGroup("ListActions")]
        [MM_Button("Populate Lists", TabGroup = "Lower", TabName = "Lists")]
        private void PopulateLists()
        {
            playerNames = new List<string> { "Alice", "Bob", "Charlie" };
            enemyDatabase = new List<EnemyData>
            {
                new EnemyData { enemyName = "Skeleton", health = 30, damage = 8 },
                new EnemyData { enemyName = "Zombie", health = 60, damage = 12 }
            };
            itemInventory = new List<ItemData>
            {
                new ItemData { itemName = "Health Potion", stackSize = 5, price = 10f }
            };
            Debug.Log("Lists populated!");
        }
        
        [MM_Button("Add Enemy", TabGroup = "Lower", TabName = "Lists")]
        private void AddEnemy()
        {
            string[] names = { "Goblin", "Orc", "Troll", "Dragon" };
            enemyDatabase.Add(new EnemyData
            {
                enemyName = names[Random.Range(0, names.Length)],
                health = Random.Range(50, 200),
                damage = Random.Range(10, 50)
            });
            Debug.Log($"Added enemy! Total: {enemyDatabase.Count}");
        }
        
        #endregion
        
        #region Button Methods - Lower/Buttons
        
        [MM_Button("🎮 Test Systems", TabGroup = "Lower", TabName = "Buttons")]
        public void TestSystems()
        {
            Debug.Log("Testing MM_EditorTools...");
            Debug.Log($"Players: {playerNames.Count}");
            Debug.Log($"Enemies: {enemyDatabase.Count}");
            readOnlyScore += 100;
            buttonTestValue++;
        }
        
        [MM_Button("Reset All Data", TabGroup = "Lower", TabName = "Buttons")]
        public void ResetData()
        {
            readOnlyScore = 0;
            readOnlyTime = 0f;
            healthBar = 100f;
            buttonTestValue = 0;
            Debug.Log("All data reset!");
        }
        
        #endregion
        
        #region Validation Methods
        
        private bool IsPositive(int value) => value > 0;
        private bool IsInRange(float value) => value >= 0 && value <= 100;
        
        #endregion
    }
    
    #region Data Classes
    
    [System.Serializable]
    public class EnemyData
    {
        public string enemyName = "Enemy";
        public int health = 100;
        public int damage = 10;
    }
    
    [System.Serializable]
    public class ItemData
    {
        public string itemName = "Item";
        public int stackSize = 1;
        public float price = 0f;
    }
    
    #endregion
    
    public enum WeaponType
    {
        Melee,
        Ranged,
        Magic
    }
}
