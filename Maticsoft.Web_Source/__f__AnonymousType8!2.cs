using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType8<<mod>j__TPar, <cid>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <cid>j__TPar <cid>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <mod>j__TPar <mod>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType8(<mod>j__TPar mod, <cid>j__TPar cid)
    {
        this.<mod>i__Field = mod;
        this.<cid>i__Field = cid;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType8<<mod>j__TPar, <cid>j__TPar>;
        return (((type != null) && EqualityComparer<<mod>j__TPar>.Default.Equals(this.<mod>i__Field, type.<mod>i__Field)) && EqualityComparer<<cid>j__TPar>.Default.Equals(this.<cid>i__Field, type.<cid>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x6c1721f1;
        num = (-1521134295 * num) + EqualityComparer<<mod>j__TPar>.Default.GetHashCode(this.<mod>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<cid>j__TPar>.Default.GetHashCode(this.<cid>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ mod = ");
        builder.Append(this.<mod>i__Field);
        builder.Append(", cid = ");
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

    public <mod>j__TPar mod
    {
        get
        {
            return this.<mod>i__Field;
        }
    }
}

