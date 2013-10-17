using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType1<<area>j__TPar, <controller>j__TPar, <action>j__TPar, <id>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <area>j__TPar <area>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <id>j__TPar <id>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType1(<area>j__TPar area, <controller>j__TPar controller, <action>j__TPar action, <id>j__TPar id)
    {
        this.<area>i__Field = area;
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<id>i__Field = id;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType1<<area>j__TPar, <controller>j__TPar, <action>j__TPar, <id>j__TPar>;
        return ((((type != null) && EqualityComparer<<area>j__TPar>.Default.Equals(this.<area>i__Field, type.<area>i__Field)) && (EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field) && EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field))) && EqualityComparer<<id>j__TPar>.Default.Equals(this.<id>i__Field, type.<id>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x48219d07;
        num = (-1521134295 * num) + EqualityComparer<<area>j__TPar>.Default.GetHashCode(this.<area>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<id>j__TPar>.Default.GetHashCode(this.<id>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ area = ");
        builder.Append(this.<area>i__Field);
        builder.Append(", controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", id = ");
        builder.Append(this.<id>i__Field);
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

    public <area>j__TPar area
    {
        get
        {
            return this.<area>i__Field;
        }
    }

    public <controller>j__TPar controller
    {
        get
        {
            return this.<controller>i__Field;
        }
    }

    public <id>j__TPar id
    {
        get
        {
            return this.<id>i__Field;
        }
    }
}

