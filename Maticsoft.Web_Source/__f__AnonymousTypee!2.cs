using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousTypee<<controller>j__TPar, <action>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousTypee(<controller>j__TPar controller, <action>j__TPar action)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typee = value as <>f__AnonymousTypee<<controller>j__TPar, <action>j__TPar>;
        return (((typee != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, typee.<controller>i__Field)) && EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, typee.<action>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x59d5f193;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
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
}

