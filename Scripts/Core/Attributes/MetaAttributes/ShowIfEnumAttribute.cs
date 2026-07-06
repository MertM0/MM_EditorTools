using System;

namespace MM.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ShowIfEnumAttribute : ShowIfAttributeBase
    {
        public ShowIfEnumAttribute(string enumName, object enumValue)
            : base(enumName, enumValue as Enum)
        {
            Inverted = false;
        }
    }
}
