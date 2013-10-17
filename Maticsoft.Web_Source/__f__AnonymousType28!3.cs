using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType28<<controller>j__TPar, <action>j__TPar, <pid>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <pid>j__TPar <pid>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType28(<controller>j__TPar controller, <action>j__TPar action, <pid>j__TPar pid)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<pid>i__Field = pid;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType28<<controller>j__TPar, <action>j__TPar, <pid>j__TPar>;
        return ((((type != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field)) && EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field)) && EqualityComparer<<pid>j__TPar>.Default.Equals(this.<pid>i__Field, type.<pid>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x3641c714;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<pid>j__TPar>.Default.GetHashCode(this.<pid>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", pid = ");
        builder.Append(this.<pid>i__Field);
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

    public <pid>j__TPar pid
    {
        get
        {
            return this.<pid>i__Field;
        }
    }
}

