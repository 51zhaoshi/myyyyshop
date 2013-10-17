using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousTypea<<controller>j__TPar, <action>j__TPar, <cid>j__TPar, <keyword>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <action>j__TPar <action>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <cid>j__TPar <cid>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <controller>j__TPar <controller>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <keyword>j__TPar <keyword>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousTypea(<controller>j__TPar controller, <action>j__TPar action, <cid>j__TPar cid, <keyword>j__TPar keyword)
    {
        this.<controller>i__Field = controller;
        this.<action>i__Field = action;
        this.<cid>i__Field = cid;
        this.<keyword>i__Field = keyword;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typea = value as <>f__AnonymousTypea<<controller>j__TPar, <action>j__TPar, <cid>j__TPar, <keyword>j__TPar>;
        return ((((typea != null) && EqualityComparer<<controller>j__TPar>.Default.Equals(this.<controller>i__Field, typea.<controller>i__Field)) && (EqualityComparer<<action>j__TPar>.Default.Equals(this.<action>i__Field, typea.<action>i__Field) && EqualityComparer<<cid>j__TPar>.Default.Equals(this.<cid>i__Field, typea.<cid>i__Field))) && EqualityComparer<<keyword>j__TPar>.Default.Equals(this.<keyword>i__Field, typea.<keyword>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -1257278302;
        num = (-1521134295 * num) + EqualityComparer<<controller>j__TPar>.Default.GetHashCode(this.<controller>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<action>j__TPar>.Default.GetHashCode(this.<action>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<cid>j__TPar>.Default.GetHashCode(this.<cid>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<keyword>j__TPar>.Default.GetHashCode(this.<keyword>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ controller = ");
        builder.Append(this.<controller>i__Field);
        builder.Append(", action = ");
        builder.Append(this.<action>i__Field);
        builder.Append(", cid = ");
        builder.Append(this.<cid>i__Field);
        builder.Append(", keyword = ");
        builder.Append(this.<keyword>i__Field);
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

    public <cid>j__TPar cid
    {
        get
        {
            return this.<cid>i__Field;
        }
    }

    public <controller>j__TPar controller
    {
        get
        {
            return this.<controller>i__Field;
        }
    }

    public <keyword>j__TPar keyword
    {
        get
        {
            return this.<keyword>i__Field;
        }
    }
}

