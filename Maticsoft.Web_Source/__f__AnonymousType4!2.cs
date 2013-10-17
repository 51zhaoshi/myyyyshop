using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType4<<area>j__TPar, <returnUrl>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <area>j__TPar <area>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <returnUrl>j__TPar <returnUrl>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType4(<area>j__TPar area, <returnUrl>j__TPar returnUrl)
    {
        this.<area>i__Field = area;
        this.<returnUrl>i__Field = returnUrl;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType4<<area>j__TPar,<returnUrl>j__TPar><<area>j__TPar, <returnUrl>j__TPar>;
        return (((type != null) && EqualityComparer<<area>j__TPar>.Default.Equals(this.<area>i__Field, type.<area>i__Field)) && EqualityComparer<<returnUrl>j__TPar>.Default.Equals(this.<returnUrl>i__Field, type.<returnUrl>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -419661490;
        num = (-1521134295 * num) + EqualityComparer<<area>j__TPar>.Default.GetHashCode(this.<area>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<returnUrl>j__TPar>.Default.GetHashCode(this.<returnUrl>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ area = ");
        builder.Append(this.<area>i__Field);
        builder.Append(", returnUrl = ");
        builder.Append(this.<returnUrl>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <area>j__TPar area
    {
        get
        {
            return this.<area>i__Field;
        }
    }

    public <returnUrl>j__TPar returnUrl
    {
        get
        {
            return this.<returnUrl>i__Field;
        }
    }
}

