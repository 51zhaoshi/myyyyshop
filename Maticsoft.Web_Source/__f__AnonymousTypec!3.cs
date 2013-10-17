using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousTypec<<controller>j__TPar, <action>j__TPar, <parentId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <parentId>j__TPar <parentId>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousTypec(<controller>j__TPar controller, <action>j__TPar action, <parentId>j__TPar parentId)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<parentId>i__Field = parentId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typec = value as <>f__AnonymousTypec<<controller>j__TPar, <action>j__TPar, <parentId>j__TPar>;
        return ((((typec != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, typec.<controller>i__Field)) && EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, typec.<action>i__Field)) && EqualityComparer<<parentId>j__TPar>.Default.Equals(this.<parentId>i__Field, typec.<parentId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -1518978597;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<parentId>j__TPar>.Default.GetHashCode(this.<parentId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", parentId = ");
        builder.Append(this.<parentId>i__Field);
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

    public <parentId>j__TPar parentId
    {
        get
        {
            return this.<parentId>i__Field;
        }
    }
}

