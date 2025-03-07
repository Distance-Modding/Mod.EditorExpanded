using System;

namespace EditorExpanded
{
    //Editor Additions
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class EditorToolAttribute : Attribute
    {
        public EditorToolAttribute()
        {
        }
    }
}
