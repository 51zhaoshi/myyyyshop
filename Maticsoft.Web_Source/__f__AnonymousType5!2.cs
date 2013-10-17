using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType5<<area>j__TPar, <id>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <area>j__TPar <area>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <id>j__TPar <id>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType5(<area>j__TPar area, <id>j__TPar id)
    {
        this.<area>i__Field = area;
        this.<id>i__Field = id;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType5<<area>j__TPar,<id>j__TPar><<area>j__TPar, <id>j__TPar>;
        return (((type != null) && EqualityComparer<<area>j__TPar>.Default.Equals(this.<area>i__Field, type.<area>i__Field)) && EqualityComparer<<id>j__TPar>.Default.Equals(this.<id>i__Field, type.<id>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -1862228868;
        num = (-1521134295 * num) + EqualityComparer<<area>j__TPar>.Default.GetHashCode(this.<area>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<id>j__TPar>.Default.GetHashCode(this.<id>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ area = ");
        builder.Append(this.<area>i__Field);
        builder.Append(", id = ");
        builder.Append(this.<id>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <area>j__TPar area
    {
        get
        {
            return this.<area>i__Field;
        }
    }

    public <id>j__TPar id
    {
        get
        {
            return this.<id>i__Field;
        }
    }
}

