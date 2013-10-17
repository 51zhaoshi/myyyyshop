using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType2<<area>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <area>j__TPar <area>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType2(<area>j__TPar area)
    {
        this.<area>i__Field = area;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType2<<area>j__TPar>;
        return ((type != null) && EqualityComparer<<area>j__TPar>.Default.Equals(this.<area>i__Field, type.<area>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x5b1aa890;
        return ((-1521134295 * num) + EqualityComparer<<area>j__TPar>.Default.GetHashCode(this.<area>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ area = ");
        builder.Append(this.<area>i__Field);
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
}

