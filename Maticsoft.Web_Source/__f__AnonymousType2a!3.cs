using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType2a<<controller>j__TPar, <action>j__TPar, <classId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <classId>j__TPar <classId>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType2a(<controller>j__TPar controller, <action>j__TPar action, <classId>j__TPar classId)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<classId>i__Field = classId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typea = value as <>f__AnonymousType2a<<controller>j__TPar, <action>j__TPar, <classId>j__TPar>;
        return ((((typea != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, typea.<controller>i__Field)) && EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, typea.<action>i__Field)) && EqualityComparer<<classId>j__TPar>.Default.Equals(this.<classId>i__Field, typea.<classId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x570d4620;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<classId>j__TPar>.Default.GetHashCode(this.<classId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", classId = ");
        builder.Append(this.<classId>i__Field);
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

    public <classId>j__TPar classId
    {
        get
        {
            return this.<classId>i__Field;
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

