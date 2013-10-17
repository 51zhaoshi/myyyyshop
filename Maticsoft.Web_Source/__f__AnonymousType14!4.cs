using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType14<<controller>j__TPar, <action>j__TPar, <viewname>j__TPar, <id>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <id>j__TPar <id>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <viewname>j__TPar <viewname>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType14(<controller>j__TPar controller, <action>j__TPar action, <viewname>j__TPar viewname, <id>j__TPar id)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<viewname>i__Field = viewname;
        this.<id>i__Field = id;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType14<<controller>j__TPar, <action>j__TPar, <viewname>j__TPar, <id>j__TPar>;
        return ((((type != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field)) && (EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field) && EqualityComparer<<viewname>j__TPar>.Default.Equals(this.<viewname>i__Field, type.<viewname>i__Field))) && EqualityComparer<<id>j__TPar>.Default.Equals(this.<id>i__Field, type.<id>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x453b1986;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<viewname>j__TPar>.Default.GetHashCode(this.<viewname>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<id>j__TPar>.Default.GetHashCode(this.<id>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", viewname = ");
        builder.Append(this.<viewname>i__Field);
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

    public <viewname>j__TPar viewname
    {
        get
        {
            return this.<viewname>i__Field;
        }
    }
}

