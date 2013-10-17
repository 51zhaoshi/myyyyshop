using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType29<<controller>j__TPar, <action>j__TPar, <type>j__TPar, <categoryId>j__TPar, <address>j__TPar, <orderby>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <address>j__TPar <address>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <categoryId>j__TPar <categoryId>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <orderby>j__TPar <orderby>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <type>j__TPar <type>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType29(<controller>j__TPar controller, <action>j__TPar action, <type>j__TPar type, <categoryId>j__TPar categoryId, <address>j__TPar address, <orderby>j__TPar orderby)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<type>i__Field = type;
        this.<categoryId>i__Field = categoryId;
        this.<address>i__Field = address;
        this.<orderby>i__Field = orderby;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType29<<controller>j__TPar, <action>j__TPar, <type>j__TPar, <categoryId>j__TPar, <address>j__TPar, <orderby>j__TPar>;
        return (((((type != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field)) && (EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field) && EqualityComparer<<type>j__TPar>.Default.Equals(this.<type>i__Field, type.<type>i__Field))) && (EqualityComparer<<categoryId>j__TPar>.Default.Equals(this.<categoryId>i__Field, type.<categoryId>i__Field) && EqualityComparer<<address>j__TPar>.Default.Equals(this.<address>i__Field, type.<address>i__Field))) && EqualityComparer<<orderby>j__TPar>.Default.Equals(this.<orderby>i__Field, type.<orderby>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -234379487;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<type>j__TPar>.Default.GetHashCode(this.<type>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<categoryId>j__TPar>.Default.GetHashCode(this.<categoryId>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<address>j__TPar>.Default.GetHashCode(this.<address>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<orderby>j__TPar>.Default.GetHashCode(this.<orderby>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", type = ");
        builder.Append(this.<type>i__Field);
        builder.Append(", categoryId = ");
        builder.Append(this.<categoryId>i__Field);
        builder.Append(", address = ");
        builder.Append(this.<address>i__Field);
        builder.Append(", orderby = ");
        builder.Append(this.<orderby>i__Field);
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

    public <address>j__TPar address
    {
        get
        {
            return this.<address>i__Field;
        }
    }

    public <categoryId>j__TPar categoryId
    {
        get
        {
            return this.<categoryId>i__Field;
        }
    }

    public <controller>j__TPar controller
    {
        get
        {
            return this.<controller>i__Field;
        }
    }

    public <orderby>j__TPar orderby
    {
        get
        {
            return this.<orderby>i__Field;
        }
    }

    public <type>j__TPar type
    {
        get
        {
            return this.<type>i__Field;
        }
    }
}

