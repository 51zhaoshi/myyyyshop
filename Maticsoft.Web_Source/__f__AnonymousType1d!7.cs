using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType1d<<controller>j__TPar, <action>j__TPar, <cid>j__TPar, <brandid>j__TPar, <mod>j__TPar, <price>j__TPar, <keyword>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <brandid>j__TPar <brandid>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <cid>j__TPar <cid>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <keyword>j__TPar <keyword>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <mod>j__TPar <mod>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <price>j__TPar <price>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType1d(<controller>j__TPar controller, <action>j__TPar action, <cid>j__TPar cid, <brandid>j__TPar brandid, <mod>j__TPar mod, <price>j__TPar price, <keyword>j__TPar keyword)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<cid>i__Field = cid;
        this.<brandid>i__Field = brandid;
        this.<mod>i__Field = mod;
        this.<price>i__Field = price;
        this.<keyword>i__Field = keyword;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typed = value as <>f__AnonymousType1d<<controller>j__TPar, <action>j__TPar, <cid>j__TPar, <brandid>j__TPar, <mod>j__TPar, <price>j__TPar, <keyword>j__TPar>;
        return (((((typed != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, typed.<controller>i__Field)) && (EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, typed.<action>i__Field) && EqualityComparer<<cid>j__TPar>.Default.Equals(this.<cid>i__Field, typed.<cid>i__Field))) && ((EqualityComparer<<brandid>j__TPar>.Default.Equals(this.<brandid>i__Field, typed.<brandid>i__Field) && EqualityComparer<<mod>j__TPar>.Default.Equals(this.<mod>i__Field, typed.<mod>i__Field)) && EqualityComparer<<price>j__TPar>.Default.Equals(this.<price>i__Field, typed.<price>i__Field))) && EqualityComparer<<keyword>j__TPar>.Default.Equals(this.<keyword>i__Field, typed.<keyword>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x1e271683;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<cid>j__TPar>.Default.GetHashCode(this.<cid>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<brandid>j__TPar>.Default.GetHashCode(this.<brandid>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<mod>j__TPar>.Default.GetHashCode(this.<mod>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<price>j__TPar>.Default.GetHashCode(this.<price>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<keyword>j__TPar>.Default.GetHashCode(this.<keyword>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", cid = ");
        builder.Append(this.<cid>i__Field);
        builder.Append(", brandid = ");
        builder.Append(this.<brandid>i__Field);
        builder.Append(", mod = ");
        builder.Append(this.<mod>i__Field);
        builder.Append(", price = ");
        builder.Append(this.<price>i__Field);
        builder.Append(", keyword = ");
        builder.Append(this.<keyword>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <action>j__TPar action
    {
        get
        {
            return this.<action>i__Field;
        }
    }

    public <brandid>j__TPar brandid
    {
        get
        {
            return this.<brandid>i__Field;
        }
    }

    public <cid>j__TPar cid
    {
        get
        {
            return this.<cid>i__Field;
        }
    }

    public <controller>j__TPar controller
    {
        get
        {
            return this.<controller>i__Field;
        }
    }

    public <keyword>j__TPar keyword
    {
        get
        {
            return this.<keyword>i__Field;
        }
    }

    public <mod>j__TPar mod
    {
        get
        {
            return this.<mod>i__Field;
        }
    }

    public <price>j__TPar price
    {
        get
        {
            return this.<price>i__Field;
        }
    }
}

