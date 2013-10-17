using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType13<<id>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <id>j__TPar <id>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType13(<id>j__TPar id)
    {
        this.<id>i__Field = id;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType13<<id>j__TPar>;
        return ((type != null) && EqualityComparer<<id>j__TPar>.Default.Equals(this.<id>i__Field, type.<id>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x57f3d56c;
        return ((-1521134295 * num) + EqualityComparer<<id>j__TPar>.Default.GetHashCode(this.<id>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ id = ");
        builder.Append(this.<id>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <id>j__TPar id
    {
        get
        {
            return this.<id>i__Field;
        }
    }
}

