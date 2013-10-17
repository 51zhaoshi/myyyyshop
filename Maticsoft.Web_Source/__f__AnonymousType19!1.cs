using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType19<<Area>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <Area>j__TPar <Area>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType19(<Area>j__TPar Area)
    {
        this.<Area>i__Field = Area;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType19<<Area>j__TPar>;
        return ((type != null) && EqualityComparer<<Area>j__TPar>.Default.Equals(this.<Area>i__Field, type.<Area>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x26870f88;
        return ((-1521134295 * num) + EqualityComparer<<Area>j__TPar>.Default.GetHashCode(this.<Area>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ Area = ");
        builder.Append(this.<Area>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <Area>j__TPar Area
    {
        get
        {
            return this.<Area>i__Field;
        }
    }
}

