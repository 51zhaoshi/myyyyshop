using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType1b<<minPrice>j__TPar, <maxPrice>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <maxPrice>j__TPar <maxPrice>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <minPrice>j__TPar <minPrice>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType1b(<minPrice>j__TPar minPrice, <maxPrice>j__TPar maxPrice)
    {
        this.<minPrice>i__Field = minPrice;
        this.<maxPrice>i__Field = maxPrice;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typeb = value as <>f__AnonymousType1b<<minPrice>j__TPar, <maxPrice>j__TPar>;
        return (((typeb != null) && EqualityComparer<<minPrice>j__TPar>.Default.Equals(this.<minPrice>i__Field, typeb.<minPrice>i__Field)) && EqualityComparer<<maxPrice>j__TPar>.Default.Equals(this.<maxPrice>i__Field, typeb.<maxPrice>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -776723493;
        num = (-1521134295 * num) + EqualityComparer<<minPrice>j__TPar>.Default.GetHashCode(this.<minPrice>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<maxPrice>j__TPar>.Default.GetHashCode(this.<maxPrice>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ minPrice = ");
        builder.Append(this.<minPrice>i__Field);
        builder.Append(", maxPrice = ");
        builder.Append(this.<maxPrice>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <maxPrice>j__TPar maxPrice
    {
        get
        {
            return this.<maxPrice>i__Field;
        }
    }

    public <minPrice>j__TPar minPrice
    {
        get
        {
            return this.<minPrice>i__Field;
        }
    }
}

