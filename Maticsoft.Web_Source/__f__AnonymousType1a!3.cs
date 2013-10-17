using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType1a<<sku>j__TPar, <count>j__TPar, <price>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <count>j__TPar <count>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <price>j__TPar <price>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <sku>j__TPar <sku>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType1a(<sku>j__TPar sku, <count>j__TPar count, <price>j__TPar price)
    {
        this.<sku>i__Field = sku;
        this.<count>i__Field = count;
        this.<price>i__Field = price;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typea = value as <>f__AnonymousType1a<<sku>j__TPar, <count>j__TPar, <price>j__TPar>;
        return ((((typea != null) && EqualityComparer<<sku>j__TPar>.Default.Equals(this.<sku>i__Field, typea.<sku>i__Field)) && EqualityComparer<<count>j__TPar>.Default.Equals(this.<count>i__Field, typea.<count>i__Field)) && EqualityComparer<<price>j__TPar>.Default.Equals(this.<price>i__Field, typea.<price>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -1684993075;
        num = (-1521134295 * num) + EqualityComparer<<sku>j__TPar>.Default.GetHashCode(this.<sku>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<count>j__TPar>.Default.GetHashCode(this.<count>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<price>j__TPar>.Default.GetHashCode(this.<price>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ sku = ");
        builder.Append(this.<sku>i__Field);
        builder.Append(", count = ");
        builder.Append(this.<count>i__Field);
        builder.Append(", price = ");
        builder.Append(this.<price>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <count>j__TPar count
    {
        get
        {
            return this.<count>i__Field;
        }
    }

    public <price>j__TPar price
    {
        get
        {
            return this.<price>i__Field;
        }
    }

    public <sku>j__TPar sku
    {
        get
        {
            return this.<sku>i__Field;
        }
    }
}

