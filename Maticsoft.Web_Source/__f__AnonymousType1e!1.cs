using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType1e<<AlbumId>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <AlbumId>j__TPar <AlbumId>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType1e(<AlbumId>j__TPar AlbumId)
    {
        this.<AlbumId>i__Field = AlbumId;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var typee = value as <>f__AnonymousType1e<<AlbumId>j__TPar>;
        return ((typee != null) && EqualityComparer<<AlbumId>j__TPar>.Default.Equals(this.<AlbumId>i__Field, typee.<AlbumId>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -1496622893;
        return ((-1521134295 * num) + EqualityComparer<<AlbumId>j__TPar>.Default.GetHashCode(this.<AlbumId>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ AlbumId = ");
        builder.Append(this.<AlbumId>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <AlbumId>j__TPar AlbumId
    {
        get
        {
            return this.<AlbumId>i__Field;
        }
    }
}

