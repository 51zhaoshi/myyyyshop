using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType18<<pid>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <pid>j__TPar <pid>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType18(<pid>j__TPar pid)
    {
        this.<pid>i__Field = pid;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType18<<pid>j__TPar>;
        return ((type != null) && EqualityComparer<<pid>j__TPar>.Default.Equals(this.<pid>i__Field, type.<pid>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -721860727;
        return ((-1521134295 * num) + EqualityComparer<<pid>j__TPar>.Default.GetHashCode(this.<pid>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ pid = ");
        builder.Append(this.<pid>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <pid>j__TPar pid
    {
        get
        {
            return this.<pid>i__Field;
        }
    }
}

