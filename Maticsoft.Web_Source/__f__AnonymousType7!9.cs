using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType7<<controller>j__TPar, <action>j__TPar, <cid>j__TPar, <brandid>j__TPar, <attrvalues>j__TPar, <mod>j__TPar, <price>j__TPar, <viewname>j__TPar, <ajaxViewName>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <ajaxViewName>j__TPar <ajaxViewName>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <attrvalues>j__TPar <attrvalues>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <brandid>j__TPar <brandid>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <cid>j__TPar <cid>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <mod>j__TPar <mod>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <price>j__TPar <price>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <viewname>j__TPar <viewname>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType7(<controller>j__TPar controller, <action>j__TPar action, <cid>j__TPar cid, <brandid>j__TPar brandid, <attrvalues>j__TPar attrvalues, <mod>j__TPar mod, <price>j__TPar price, <viewname>j__TPar viewname, <ajaxViewName>j__TPar ajaxViewName)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<cid>i__Field = cid;
        this.<brandid>i__Field = brandid;
        this.<attrvalues>i__Field = attrvalues;
        this.<mod>i__Field = mod;
        this.<price>i__Field = price;
        this.<viewname>i__Field = viewname;
        this.<ajaxViewName>i__Field = ajaxViewName;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType7<<controller>j__TPar, <action>j__TPar, <cid>j__TPar, <brandid>j__TPar, <attrvalues>j__TPar, <mod>j__TPar, <price>j__TPar, <viewname>j__TPar, <ajaxViewName>j__TPar>;
        return ((((((type != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field)) && (EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field) && EqualityComparer<<cid>j__TPar>.Default.Equals(this.<cid>i__Field, type.<cid>i__Field))) && ((EqualityComparer<<brandid>j__TPar>.Default.Equals(this.<brandid>i__Field, type.<brandid>i__Field) && EqualityComparer<<attrvalues>j__TPar>.Default.Equals(this.<attrvalues>i__Field, type.<attrvalues>i__Field)) && (EqualityComparer<<mod>j__TPar>.Default.Equals(this.<mod>i__Field, type.<mod>i__Field) && EqualityComparer<<price>j__TPar>.Default.Equals(this.<price>i__Field, type.<price>i__Field)))) && EqualityComparer<<viewname>j__TPar>.Default.Equals(this.<viewname>i__Field, type.<viewname>i__Field)) && EqualityComparer<<ajaxViewName>j__TPar>.Default.Equals(this.<ajaxViewName>i__Field, type.<ajaxViewName>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x508a8195;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<cid>j__TPar>.Default.GetHashCode(this.<cid>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<brandid>j__TPar>.Default.GetHashCode(this.<brandid>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<attrvalues>j__TPar>.Default.GetHashCode(this.<attrvalues>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<mod>j__TPar>.Default.GetHashCode(this.<mod>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<price>j__TPar>.Default.GetHashCode(this.<price>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<viewname>j__TPar>.Default.GetHashCode(this.<viewname>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<ajaxViewName>j__TPar>.Default.GetHashCode(this.<ajaxViewName>i__Field));
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
        builder.Append(", attrvalues = ");
        builder.Append(this.<attrvalues>i__Field);
        builder.Append(", mod = ");
        builder.Append(this.<mod>i__Field);
        builder.Append(", price = ");
        builder.Append(this.<price>i__Field);
        builder.Append(", viewname = ");
        builder.Append(this.<viewname>i__Field);
        builder.Append(", ajaxViewName = ");
        builder.Append(this.<ajaxViewName>i__Field);
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

    public <ajaxViewName>j__TPar ajaxViewName
    {
        get
        {
            return this.<ajaxViewName>i__Field;
        }
    }

    public <attrvalues>j__TPar attrvalues
    {
        get
        {
            return this.<attrvalues>i__Field;
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

    public <viewname>j__TPar viewname
    {
        get
        {
            return this.<viewname>i__Field;
        }
    }
}

