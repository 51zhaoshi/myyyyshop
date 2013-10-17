using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousTypeb<<cid>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <cid>j__TPar <cid>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousTypeb(<cid>j__TPar cid)
    {
        this.<cid>i__Field = cid;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typeb = value as <>f__AnonymousTypeb<<cid>j__TPar>;
        return ((typeb != null) && EqualityComparer<<cid>j__TPar>.Default.Equals(this.<cid>i__Field, typeb.<cid>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x349c53c2;
        return ((-1521134295 * num) + EqualityComparer<<cid>j__TPar>.Default.GetHashCode(this.<cid>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ cid = ");
        builder.Append(this.<cid>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <cid>j__TPar cid
    {
        get
        {
            return this.<cid>i__Field;
        }
    }
}

