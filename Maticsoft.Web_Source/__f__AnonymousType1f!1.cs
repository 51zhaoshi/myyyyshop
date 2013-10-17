using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType1f<<email>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <email>j__TPar <email>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType1f(<email>j__TPar email)
    {
        this.<email>i__Field = email;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typef = value as <>f__AnonymousType1f<<email>j__TPar>;
        return ((typef != null) && EqualityComparer<<email>j__TPar>.Default.Equals(this.<email>i__Field, typef.<email>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -2027351805;
        return ((-1521134295 * num) + EqualityComparer<<email>j__TPar>.Default.GetHashCode(this.<email>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ email = ");
        builder.Append(this.<email>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <email>j__TPar email
    {
        get
        {
            return this.<email>i__Field;
        }
    }
}

