using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType2b<<area>j__TPar, <viewname>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <area>j__TPar <area>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <viewname>j__TPar <viewname>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType2b(<area>j__TPar area, <viewname>j__TPar viewname)
    {
        this.<area>i__Field = area;
        this.<viewname>i__Field = viewname;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typeb = value as <>f__AnonymousType2b<<area>j__TPar, <viewname>j__TPar>;
        return (((typeb != null) && EqualityComparer<<area>j__TPar>.Default.Equals(this.<area>i__Field, typeb.<area>i__Field)) && EqualityComparer<<viewname>j__TPar>.Default.Equals(this.<viewname>i__Field, typeb.<viewname>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x3d004c0f;
        num = (-1521134295 * num) + EqualityComparer<<area>j__TPar>.Default.GetHashCode(this.<area>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<viewname>j__TPar>.Default.GetHashCode(this.<viewname>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ area = ");
        builder.Append(this.<area>i__Field);
        builder.Append(", viewname = ");
        builder.Append(this.<viewname>i__Field);
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

    public <viewname>j__TPar viewname
    {
        get
        {
            return this.<viewname>i__Field;
        }
    }
}

