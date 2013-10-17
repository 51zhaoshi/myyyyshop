using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType23<<controller>j__TPar, <action>j__TPar, <pageIndex>j__TPar, <sequence>j__TPar, <q>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <pageIndex>j__TPar <pageIndex>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <q>j__TPar <q>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <sequence>j__TPar <sequence>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType23(<controller>j__TPar controller, <action>j__TPar action, <pageIndex>j__TPar pageIndex, <sequence>j__TPar sequence, <q>j__TPar q)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<pageIndex>i__Field = pageIndex;
        this.<sequence>i__Field = sequence;
        this.<q>i__Field = q;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType23<<controller>j__TPar, <action>j__TPar, <pageIndex>j__TPar, <sequence>j__TPar, <q>j__TPar>;
        return (((((type != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field)) && (EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field) && EqualityComparer<<pageIndex>j__TPar>.Default.Equals(this.<pageIndex>i__Field, type.<pageIndex>i__Field))) && EqualityComparer<<sequence>j__TPar>.Default.Equals(this.<sequence>i__Field, type.<sequence>i__Field)) && EqualityComparer<<q>j__TPar>.Default.Equals(this.<q>i__Field, type.<q>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -897044420;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<pageIndex>j__TPar>.Default.GetHashCode(this.<pageIndex>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<sequence>j__TPar>.Default.GetHashCode(this.<sequence>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<q>j__TPar>.Default.GetHashCode(this.<q>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", pageIndex = ");
        builder.Append(this.<pageIndex>i__Field);
        builder.Append(", sequence = ");
        builder.Append(this.<sequence>i__Field);
        builder.Append(", q = ");
        builder.Append(this.<q>i__Field);
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

    public <pageIndex>j__TPar pageIndex
    {
        get
        {
            return this.<pageIndex>i__Field;
        }
    }

    public <q>j__TPar q
    {
        get
        {
            return this.<q>i__Field;
        }
    }

    public <sequence>j__TPar sequence
    {
        get
        {
            return this.<sequence>i__Field;
        }
    }
}

