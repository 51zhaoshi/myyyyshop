using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType10<<productId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <productId>j__TPar <productId>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType10(<productId>j__TPar productId)
    {
        this.<productId>i__Field = productId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType10<<productId>j__TPar>;
        return ((type != null) && EqualityComparer<<productId>j__TPar>.Default.Equals(this.<productId>i__Field, type.<productId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -801259914;
        return ((-1521134295 * num) + EqualityComparer<<productId>j__TPar>.Default.GetHashCode(this.<productId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ productId = ");
        builder.Append(this.<productId>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <productId>j__TPar productId
    {
        get
        {
            return this.<productId>i__Field;
        }
    }
}

