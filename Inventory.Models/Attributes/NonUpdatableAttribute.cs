using System;

namespace Inventory.Models.Attributes
{

    [AttributeUsage(AttributeTargets.All)]
    public class NonUpdatableAttribute : Attribute
    {
        public NonUpdatableAttribute()
        {
        }
    }
}
