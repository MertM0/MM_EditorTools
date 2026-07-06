using System;

namespace MM.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InputAxisAttribute : DrawerAttribute
    {
    }
}
