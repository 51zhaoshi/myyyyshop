using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType3<<Data>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <Data>j__TPar <Data>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType3(<Data>j__TPar Data)
    {
        this.<Data>i__Field = Data;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType3<<Data>j__TPar>;
        return ((type != null) && EqualityComparer<<Data>j__TPar>.Default.Equals(this.<Data>i__Field, type.<Data>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x1bcc0e8d;
        return ((-1521134295 * num) + EqualityComparer<<Data>j__TPar>.Default.GetHashCode(this.<Data>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ Data = ");
        builder.Append(this.<Data>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <Data>j__TPar Data
    {
        get
        {
            return this.<Data>i__Field;
        }
    }
}

