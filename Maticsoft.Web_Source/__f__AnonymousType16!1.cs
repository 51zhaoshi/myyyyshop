using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType16<<GroupId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <GroupId>j__TPar <GroupId>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType16(<GroupId>j__TPar GroupId)
    {
        this.<GroupId>i__Field = GroupId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType16<<GroupId>j__TPar>;
        return ((type != null) && EqualityComparer<<GroupId>j__TPar>.Default.Equals(this.<GroupId>i__Field, type.<GroupId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x5f33aada;
        return ((-1521134295 * num) + EqualityComparer<<GroupId>j__TPar>.Default.GetHashCode(this.<GroupId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ GroupId = ");
        builder.Append(this.<GroupId>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <GroupId>j__TPar GroupId
    {
        get
        {
            return this.<GroupId>i__Field;
        }
    }
}

