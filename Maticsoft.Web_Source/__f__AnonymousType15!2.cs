using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType15<<viewname>j__TPar, <id>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <id>j__TPar <id>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <viewname>j__TPar <viewname>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType15(<viewname>j__TPar viewname, <id>j__TPar id)
    {
        this.<viewname>i__Field = viewname;
        this.<id>i__Field = id;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType15<<viewname>j__TPar, <id>j__TPar>;
        return (((type != null) && EqualityComparer<<viewname>j__TPar>.Default.Equals(this.<viewname>i__Field, type.<viewname>i__Field)) && EqualityComparer<<id>j__TPar>.Default.Equals(this.<id>i__Field, type.<id>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -1373731517;
        num = (-1521134295 * num) + EqualityComparer<<viewname>j__TPar>.Default.GetHashCode(this.<viewname>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<id>j__TPar>.Default.GetHashCode(this.<id>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ viewname = ");
        builder.Append(this.<viewname>i__Field);
        builder.Append(", id = ");
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

    public <viewname>j__TPar viewname
    {
        get
        {
            return this.<viewname>i__Field;
        }
    }
}

