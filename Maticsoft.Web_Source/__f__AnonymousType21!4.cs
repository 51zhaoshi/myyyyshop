using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType21<<controller>j__TPar, <action>j__TPar, <q>j__TPar, <page>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <page>j__TPar <page>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <q>j__TPar <q>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType21(<controller>j__TPar controller, <action>j__TPar action, <q>j__TPar q, <page>j__TPar page)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<q>i__Field = q;
        this.<page>i__Field = page;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType21<<controller>j__TPar, <action>j__TPar, <q>j__TPar, <page>j__TPar>;
        return ((((type != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, type.<controller>i__Field)) && (EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, type.<action>i__Field) && EqualityComparer<<q>j__TPar>.Default.Equals(this.<q>i__Field, type.<q>i__Field))) && EqualityComparer<<page>j__TPar>.Default.Equals(this.<page>i__Field, type.<page>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x33847781;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<q>j__TPar>.Default.GetHashCode(this.<q>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<page>j__TPar>.Default.GetHashCode(this.<page>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", q = ");
        builder.Append(this.<q>i__Field);
        builder.Append(", page = ");
        builder.Append(this.<page>i__Field);
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

    public <page>j__TPar page
    {
        get
        {
            return this.<page>i__Field;
        }
    }

    public <q>j__TPar q
    {
        get
        {
            return this.<q>i__Field;
        }
    }
}

