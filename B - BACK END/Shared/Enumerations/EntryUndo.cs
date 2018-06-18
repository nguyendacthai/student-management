using System;

namespace Shared.Enumerations
{
    [Flags]
    public enum EntryUndo
    {
        Modified = 1,
        Added,
        Deleted
    }
}