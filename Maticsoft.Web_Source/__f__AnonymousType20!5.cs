using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class <>f__AnonymousType20<<OrderId>j__TPar, <OrderCode>j__TPar, <Amount>j__TPar, <PaymentTypeId>j__TPar, <PaymentTypeName>j__TPar>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <Amount>j__TPar <Amount>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <OrderCode>j__TPar <OrderCode>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <OrderId>j__TPar <OrderId>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <PaymentTypeId>j__TPar <PaymentTypeId>i__Field;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly <PaymentTypeName>j__TPar <PaymentTypeName>i__Field;

    [DebuggerHidden]
    public <>f__AnonymousType20(<OrderId>j__TPar OrderId, <OrderCode>j__TPar OrderCode, <Amount>j__TPar Amount, <PaymentTypeId>j__TPar PaymentTypeId, <PaymentTypeName>j__TPar PaymentTypeName)
    {
        this.<OrderId>i__Field = OrderId;
        this.<OrderCode>i__Field = OrderCode;
        this.<Amount>i__Field = Amount;
        this.<PaymentTypeId>i__Field = PaymentTypeId;
        this.<PaymentTypeName>i__Field = PaymentTypeName;
    }

    [DebuggerHidden]
    public override bool Equals(object value)
    {
        var type = value as <>f__AnonymousType20<<OrderId>j__TPar, <OrderCode>j__TPar, <Amount>j__TPar, <PaymentTypeId>j__TPar, <PaymentTypeName>j__TPar>;
        return (((((type != null) && EqualityComparer<<OrderId>j__TPar>.Default.Equals(this.<OrderId>i__Field, type.<OrderId>i__Field)) && (EqualityComparer<<OrderCode>j__TPar>.Default.Equals(this.<OrderCode>i__Field, type.<OrderCode>i__Field) && EqualityComparer<<Amount>j__TPar>.Default.Equals(this.<Amount>i__Field, type.<Amount>i__Field))) && EqualityComparer<<PaymentTypeId>j__TPar>.Default.Equals(this.<PaymentTypeId>i__Field, type.<PaymentTypeId>i__Field)) && EqualityComparer<<PaymentTypeName>j__TPar>.Default.Equals(this.<PaymentTypeName>i__Field, type.<PaymentTypeName>i__Field));
    }

    [DebuggerHidden]
    public override int GetHashCode()
    {
        int num = -160879827;
        num = (-1521134295 * num) + EqualityComparer<<OrderId>j__TPar>.Default.GetHashCode(this.<OrderId>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<OrderCode>j__TPar>.Default.GetHashCode(this.<OrderCode>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<Amount>j__TPar>.Default.GetHashCode(this.<Amount>i__Field);
        num = (-1521134295 * num) + EqualityComparer<<PaymentTypeId>j__TPar>.Default.GetHashCode(this.<PaymentTypeId>i__Field);
        return ((-1521134295 * num) + EqualityComparer<<PaymentTypeName>j__TPar>.Default.GetHashCode(this.<PaymentTypeName>i__Field));
    }

    [DebuggerHidden]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ OrderId = ");
        builder.Append(this.<OrderId>i__Field);
        builder.Append(", OrderCode = ");
        builder.Append(this.<OrderCode>i__Field);
        builder.Append(", Amount = ");
        builder.Append(this.<Amount>i__Field);
        builder.Append(", PaymentTypeId = ");
        builder.Append(this.<PaymentTypeId>i__Field);
        builder.Append(", PaymentTypeName = ");
        builder.Append(this.<PaymentTypeName>i__Field);
        builder.Append(" }");
        return builder.ToString();
    }

    public <Amount>j__TPar Amount
    {
        get
        {
            return this.<Amount>i__Field;
        }
    }

    public <OrderCode>j__TPar OrderCode
    {
        get
        {
            return this.<OrderCode>i__Field;
        }
    }

    public <OrderId>j__TPar OrderId
    {
        get
        {
            return this.<OrderId>i__Field;
        }
    }

    public <PaymentTypeId>j__TPar PaymentTypeId
    {
        get
        {
            return this.<PaymentTypeId>i__Field;
        }
    }

    public <PaymentTypeName>j__TPar PaymentTypeName
    {
        get
        {
            return this.<PaymentTypeName>i__Field;
        }
    }
}

