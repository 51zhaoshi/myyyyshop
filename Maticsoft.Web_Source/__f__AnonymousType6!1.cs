using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType6<<addressId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <addressId>j__TPar <addressId>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType6(<addressId>j__TPar addressId)
    {
        this.<addressId>i__Field = addressId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType6<<addressId>j__TPar>;
        return ((type != null) && EqualityComparer<<addressId>j__TPar>.Default.Equals(this.<addressId>i__Field, type.<addressId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = 0x335f22aa;
        return ((-1521134295 * num) + EqualityComparer<<addressId>j__TPar>.Default.GetHashCode(this.<addressId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ addressId = ");
        builder.Append(this.<addressId>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <addressId>j__TPar addressId
    {
        get
        {
            return this.<addressId>i__Field;
        }
    }
}

