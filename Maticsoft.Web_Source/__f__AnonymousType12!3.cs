using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType12<<controller>j__TPar, <action>j__TPar, <userId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <userId>j__TPar <userId>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType12(<controller>j__TPar controller, <action>j__TPar action, <userId>j__TPar userId)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<userId>i__Field = userId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType12<<controller>j__TPar, <action>j__TPar, <userId>j__TPar>;
        return ((((type != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field)) && EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field)) && EqualityComparer<<userId>j__TPar>.Default.Equals(this.<userId>i__Field, type.<userId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -2129902616;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<userId>j__TPar>.Default.GetHashCode(this.<userId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", userId = ");
        builder.Append(this.<userId>i__Field);
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

    public <userId>j__TPar userId
    {
        get
        {
            return this.<userId>i__Field;
        }
    }
}

