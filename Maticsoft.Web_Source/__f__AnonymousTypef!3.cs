using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousTypef<<controller>j__TPar, <action>j__TPar, <productId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <productId>j__TPar <productId>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousTypef(<controller>j__TPar controller, <action>j__TPar action, <productId>j__TPar productId)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<productId>i__Field = productId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typef = value as <>f__AnonymousTypef<<controller>j__TPar, <action>j__TPar, <productId>j__TPar>;
        return ((((typef != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, typef.<controller>i__Field)) && EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, typef.<action>i__Field)) && EqualityComparer<<productId>j__TPar>.Default.Equals(this.<productId>i__Field, typef.<productId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x31863e01;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<productId>j__TPar>.Default.GetHashCode(this.<productId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", productId = ");
        builder.Append(this.<productId>i__Field);
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

    public <productId>j__TPar productId
    {
        get
        {
            return this.<productId>i__Field;
        }
    }
}

