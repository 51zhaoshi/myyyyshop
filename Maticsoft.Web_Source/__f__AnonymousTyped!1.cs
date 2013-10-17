using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousTyped<<parentId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <parentId>j__TPar <parentId>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousTyped(<parentId>j__TPar parentId)
    {
        this.<parentId>i__Field = parentId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typed = value as <>f__AnonymousTyped<<parentId>j__TPar>;
        return ((typed != null) && EqualityComparer<<parentId>j__TPar>.Default.Equals(this.<parentId>i__Field, typed.<parentId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x442dba50;
        return ((-1521134295 * num) + EqualityComparer<<parentId>j__TPar>.Default.GetHashCode(this.<parentId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ parentId = ");
        builder.Append(this.<parentId>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <parentId>j__TPar parentId
    {
        get
        {
            return this.<parentId>i__Field;
        }
    }
}

