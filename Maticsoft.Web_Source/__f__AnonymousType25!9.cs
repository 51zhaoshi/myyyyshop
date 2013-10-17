using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType25<<controller>j__TPar, <action>j__TPar, <cname>j__TPar, <topcid>j__TPar, <cid>j__TPar, <minprice>j__TPar, <maxprice>j__TPar, <sequence>j__TPar, <color>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <cid>j__TPar <cid>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <cname>j__TPar <cname>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <color>j__TPar <color>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <maxprice>j__TPar <maxprice>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <minprice>j__TPar <minprice>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <sequence>j__TPar <sequence>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <topcid>j__TPar <topcid>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType25(<controller>j__TPar controller, <action>j__TPar action, <cname>j__TPar cname, <topcid>j__TPar topcid, <cid>j__TPar cid, <minprice>j__TPar minprice, <maxprice>j__TPar maxprice, <sequence>j__TPar sequence, <color>j__TPar color)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<cname>i__Field = cname;
        this.<topcid>i__Field = topcid;
        this.<cid>i__Field = cid;
        this.<minprice>i__Field = minprice;
        this.<maxprice>i__Field = maxprice;
        this.<sequence>i__Field = sequence;
        this.<color>i__Field = color;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType25<<controller>j__TPar, <action>j__TPar, <cname>j__TPar, <topcid>j__TPar, <cid>j__TPar, <minprice>j__TPar, <maxprice>j__TPar, <sequence>j__TPar, <color>j__TPar>;
        return ((((((type != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field)) && (EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field) && EqualityComparer<<cname>j__TPar>.Default.Equals(this.<cname>i__Field, type.<cname>i__Field))) && ((EqualityComparer<<topcid>j__TPar>.Default.Equals(this.<topcid>i__Field, type.<topcid>i__Field) && EqualityComparer<<cid>j__TPar>.Default.Equals(this.<cid>i__Field, type.<cid>i__Field)) && (EqualityComparer<<minprice>j__TPar>.Default.Equals(this.<minprice>i__Field, type.<minprice>i__Field) && EqualityComparer<<maxprice>j__TPar>.Default.Equals(this.<maxprice>i__Field, type.<maxprice>i__Field)))) && EqualityComparer<<sequence>j__TPar>.Default.Equals(this.<sequence>i__Field, type.<sequence>i__Field)) && EqualityComparer<<color>j__TPar>.Default.Equals(this.<color>i__Field, type.<color>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x33e3e6c9;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<cname>j__TPar>.Default.GetHashCode(this.<cname>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<topcid>j__TPar>.Default.GetHashCode(this.<topcid>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<cid>j__TPar>.Default.GetHashCode(this.<cid>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<minprice>j__TPar>.Default.GetHashCode(this.<minprice>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<maxprice>j__TPar>.Default.GetHashCode(this.<maxprice>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<sequence>j__TPar>.Default.GetHashCode(this.<sequence>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<color>j__TPar>.Default.GetHashCode(this.<color>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", cname = ");
        builder.Append(this.<cname>i__Field);
        builder.Append(", topcid = ");
        builder.Append(this.<topcid>i__Field);
        builder.Append(", cid = ");
        builder.Append(this.<cid>i__Field);
        builder.Append(", minprice = ");
        builder.Append(this.<minprice>i__Field);
        builder.Append(", maxprice = ");
        builder.Append(this.<maxprice>i__Field);
        builder.Append(", sequence = ");
        builder.Append(this.<sequence>i__Field);
        builder.Append(", color = ");
        builder.Append(this.<color>i__Field);
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

    public <cid>j__TPar cid
    {
        get
        {
            return this.<cid>i__Field;
        }
    }

    public <cname>j__TPar cname
    {
        get
        {
            return this.<cname>i__Field;
        }
    }

    public <color>j__TPar color
    {
        get
        {
            return this.<color>i__Field;
        }
    }

    public <controller>j__TPar controller
    {
        get
        {
            return this.<controller>i__Field;
        }
    }

    public <maxprice>j__TPar maxprice
    {
        get
        {
            return this.<maxprice>i__Field;
        }
    }

    public <minprice>j__TPar minprice
    {
        get
        {
            return this.<minprice>i__Field;
        }
    }

    public <sequence>j__TPar sequence
    {
        get
        {
            return this.<sequence>i__Field;
        }
    }

    public <topcid>j__TPar topcid
    {
        get
        {
            return this.<topcid>i__Field;
        }
    }
}

