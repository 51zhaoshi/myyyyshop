using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType27<<orderby>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <orderby>j__TPar <orderby>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType27(<orderby>j__TPar orderby)
    {
        this.<orderby>i__Field = orderby;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType27<<orderby>j__TPar>;
        return ((type != null) && EqualityComparer<<orderby>j__TPar>.Default.Equals(this.<orderby>i__Field, type.<orderby>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -623919866;
        return ((-1521134295 * num) + EqualityComparer<<orderby>j__TPar>.Default.GetHashCode(this.<orderby>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ orderby = ");
        builder.Append(this.<orderby>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <orderby>j__TPar orderby
    {
        get
        {
            return this.<orderby>i__Field;
        }
    }
}

