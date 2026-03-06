using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Shows field only if enum matches specified value.
    /// </summary>
    /// <example>
    /// <code>
    /// public WeaponType weaponType;
    /// 
    /// [MM_ShowIfEnum("weaponType", WeaponType.Melee)]
    /// public float meleeRange = 2f;
    /// 
    /// [MM_ShowIfEnum("weaponType", WeaponType.Ranged)]
    /// public int ammoCount = 30;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_ShowIfEnumAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the enum field to check
        /// </summary>
        public string EnumFieldName { get; private set; }
        
        /// <summary>
        /// Expected enum value
        /// </summary>
        public object EnumValue { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Shows field if enum matches value
        /// </summary>
        /// <param name="enumFieldName">Name of the enum field</param>
        /// <param name="enumValue">Expected enum value</param>
        public MM_ShowIfEnumAttribute(string enumFieldName, object enumValue)
        {
            EnumFieldName = enumFieldName;
            EnumValue = enumValue;
        }
        
        #endregion
    }
}
