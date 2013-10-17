namespace Maticsoft.Json.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class DynamicMetaObject<T> : DynamicMetaObject
    {
        private readonly bool _dontFallbackFirst;
        private readonly DynamicObjectRuntime<T> _runtime;
        private static readonly Expression[] NoArgs;

        static DynamicMetaObject()
        {
            DynamicMetaObject<T>.NoArgs = new Expression[0];
        }

        internal DynamicMetaObject(Expression expression, T value, DynamicObjectRuntime<T> runtime, bool dontFallbackFirst = false) : base(expression, BindingRestrictions.Empty, value)
        {
            this._runtime = runtime;
            this._dontFallbackFirst = dontFallbackFirst;
        }

        public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
        {
            Fallback<T> fallback = null;
            if (this._runtime.TryBinaryOperation == null)
            {
                return base.BindBinaryOperation(binder, arg);
            }
            if (fallback == null)
            {
                fallback = e => binder.FallbackBinaryOperation((DynamicMetaObject<T>) this, arg, e);
            }
            return this.CallMethodWithResult("TryBinaryOperation", binder, DynamicMetaObject<T>.GetArgs(new DynamicMetaObject[] { arg }), fallback, null);
        }

        public override DynamicMetaObject BindConvert(ConvertBinder binder)
        {
            if (this._runtime.TryConvert == null)
            {
                return base.BindConvert(binder);
            }
            return this.CallMethodWithResult("TryConvert", binder, DynamicMetaObject<T>.NoArgs, e => binder.FallbackConvert((DynamicMetaObject<T>) this, e), null);
        }

        public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
        {
            if (this._runtime.TryCreateInstance == null)
            {
                return base.BindCreateInstance(binder, args);
            }
            return this.CallMethodWithResult("TryCreateInstance", binder, DynamicMetaObject<T>.GetArgArray(args), e => binder.FallbackCreateInstance((DynamicMetaObject<T>) this, args, e), null);
        }

        public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
        {
            if (this._runtime.TryDeleteIndex == null)
            {
                return base.BindDeleteIndex(binder, indexes);
            }
            return this.CallMethodNoResult("TryDeleteIndex", binder, DynamicMetaObject<T>.GetArgArray(indexes), e => binder.FallbackDeleteIndex((DynamicMetaObject<T>) this, indexes, e));
        }

        public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
        {
            if (this._runtime.TryDeleteMember == null)
            {
                return base.BindDeleteMember(binder);
            }
            return this.CallMethodNoResult("TryDeleteMember", binder, DynamicMetaObject<T>.NoArgs, e => binder.FallbackDeleteMember((DynamicMetaObject<T>) this, e));
        }

        public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
        {
            if (this._runtime.TryGetIndex == null)
            {
                return base.BindGetIndex(binder, indexes);
            }
            return this.CallMethodWithResult("TryGetIndex", binder, DynamicMetaObject<T>.GetArgArray(indexes), e => binder.FallbackGetIndex((DynamicMetaObject<T>) this, indexes, e), null);
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            if (this._runtime.TryGetMember == null)
            {
                return base.BindGetMember(binder);
            }
            return this.CallMethodWithResult("TryGetMember", binder, DynamicMetaObject<T>.NoArgs, e => binder.FallbackGetMember((DynamicMetaObject<T>) this, e), null);
        }

        public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
        {
            if (this._runtime.TryInvoke == null)
            {
                return base.BindInvoke(binder, args);
            }
            return this.CallMethodWithResult("TryInvoke", binder, DynamicMetaObject<T>.GetArgArray(args), e => binder.FallbackInvoke((DynamicMetaObject<T>) this, args, e), null);
        }

        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
        {
            if (this._runtime.TryInvokeMember == null)
            {
                return base.BindInvokeMember(binder, args);
            }
            Fallback<T> fallback = e => binder.FallbackInvokeMember((DynamicMetaObject<T>) this, args, e);
            DynamicMetaObject errorSuggestion = this.BuildCallMethodWithResult("TryInvokeMember", binder, DynamicMetaObject<T>.GetArgArray(args), this.BuildCallMethodWithResult("TryGetMember", new GetBinderAdapter<T>(binder), DynamicMetaObject<T>.NoArgs, fallback(null), e => binder.FallbackInvoke(e, args, null)), null);
            if (!this._dontFallbackFirst)
            {
                return fallback(errorSuggestion);
            }
            return errorSuggestion;
        }

        public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
        {
            if (this._runtime.TrySetIndex == null)
            {
                return base.BindSetIndex(binder, indexes, value);
            }
            return this.CallMethodReturnLast("TrySetIndex", binder, DynamicMetaObject<T>.GetArgArray(indexes, value), e => binder.FallbackSetIndex((DynamicMetaObject<T>) this, indexes, value, e));
        }

        public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
        {
            if (this._runtime.TrySetMember == null)
            {
                return base.BindSetMember(binder, value);
            }
            return this.CallMethodReturnLast("TrySetMember", binder, DynamicMetaObject<T>.GetArgs(new DynamicMetaObject[] { value }), e => binder.FallbackSetMember((DynamicMetaObject<T>) this, value, e));
        }

        public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
        {
            if (this._runtime.TryUnaryOperation == null)
            {
                return base.BindUnaryOperation(binder);
            }
            return this.CallMethodWithResult("TryUnaryOperation", binder, DynamicMetaObject<T>.NoArgs, e => binder.FallbackUnaryOperation((DynamicMetaObject<T>) this, e), null);
        }

        private DynamicMetaObject BuildCallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicMetaObject fallbackResult, Fallback<T> fallbackInvoke)
        {
            ParameterExpression expression = Expression.Parameter(typeof(Option<object>), null);
            IEnumerable<Expression> arguments = new Expression[] { Expression.Convert(base.Expression, typeof(T)), DynamicMetaObject<T>.Constant(binder) }.Concat<Expression>(args);
            DynamicMetaObject errorSuggestion = new DynamicMetaObject(Expression.MakeMemberAccess(expression, typeof(Option<object>).GetProperty("Value")), BindingRestrictions.Empty);
            if (binder.ReturnType != typeof(object))
            {
                errorSuggestion = new DynamicMetaObject(Expression.Convert(errorSuggestion.Expression, binder.ReturnType), errorSuggestion.Restrictions);
            }
            if (fallbackInvoke != null)
            {
                errorSuggestion = fallbackInvoke(errorSuggestion);
            }
            return new DynamicMetaObject(Expression.Block(new ParameterExpression[] { expression }, new Expression[] { Expression.Assign(expression, Expression.Invoke(Expression.MakeMemberAccess(Expression.Constant(this._runtime), typeof(DynamicObjectRuntime<T>).GetProperty(methodName)), arguments)), Expression.Condition(Expression.MakeMemberAccess(expression, typeof(Option<object>).GetProperty("HasValue")), errorSuggestion.Expression, fallbackResult.Expression, binder.ReturnType) }), this.GetRestrictions().Merge(errorSuggestion.Restrictions).Merge(fallbackResult.Restrictions));
        }

        private DynamicMetaObject CallMethodNoResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, Fallback<T> fallback)
        {
            DynamicMetaObject obj2 = fallback(null);
            DynamicMetaObject errorSuggestion = new DynamicMetaObject(Expression.Condition(Expression.Invoke(Expression.MakeMemberAccess(Expression.Constant(this._runtime), typeof(DynamicObjectRuntime<T>).GetProperty(methodName)), new Expression[] { Expression.Convert(base.Expression, typeof(T)), DynamicMetaObject<T>.Constant(binder) }.Concat<Expression>(args)), Expression.Empty(), obj2.Expression, typeof(void)), this.GetRestrictions().Merge(obj2.Restrictions));
            if (!this._dontFallbackFirst)
            {
                return fallback(errorSuggestion);
            }
            return errorSuggestion;
        }

        private DynamicMetaObject CallMethodReturnLast(string methodName, DynamicMetaObjectBinder binder, Expression[] args, Fallback<T> fallback)
        {
            DynamicMetaObject obj2 = fallback(null);
            ParameterExpression ifTrue = Expression.Parameter(typeof(object), null);
            IEnumerable<Expression> arguments = new Expression[] { Expression.Convert(base.Expression, typeof(T)), DynamicMetaObject<T>.Constant(binder) }.Concat<Expression>(args);
            DynamicMetaObject errorSuggestion = new DynamicMetaObject(Expression.Block(new ParameterExpression[] { ifTrue }, new Expression[] { Expression.Condition(Expression.Invoke(Expression.MakeMemberAccess(Expression.Constant(this._runtime), typeof(DynamicObjectRuntime<T>).GetProperty(methodName)), arguments), ifTrue, obj2.Expression, typeof(object)) }), this.GetRestrictions().Merge(obj2.Restrictions));
            if (!this._dontFallbackFirst)
            {
                return fallback(errorSuggestion);
            }
            return errorSuggestion;
        }

        private DynamicMetaObject CallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, Fallback<T> fallback, Fallback<T> fallbackInvoke = new Fallback<T>())
        {
            DynamicMetaObject fallbackResult = fallback(null);
            DynamicMetaObject errorSuggestion = this.BuildCallMethodWithResult(methodName, binder, args, fallbackResult, fallbackInvoke);
            if (!this._dontFallbackFirst)
            {
                return fallback(errorSuggestion);
            }
            return errorSuggestion;
        }

        private static ConstantExpression Constant(DynamicMetaObjectBinder binder)
        {
            Type baseType = binder.GetType();
            while (!baseType.IsVisible)
            {
                baseType = baseType.BaseType;
            }
            return Expression.Constant(binder, baseType);
        }

        private static Expression[] GetArgArray(DynamicMetaObject[] args)
        {
            return new NewArrayExpression[] { Expression.NewArrayInit(typeof(object), DynamicMetaObject<T>.GetArgs(args)) };
        }

        private static Expression[] GetArgArray(DynamicMetaObject[] args, DynamicMetaObject value)
        {
            return new Expression[] { Expression.NewArrayInit(typeof(object), DynamicMetaObject<T>.GetArgs(args)), Expression.Convert(value.Expression, typeof(object)) };
        }

        private static Expression[] GetArgs(params DynamicMetaObject[] args)
        {
            return (from arg in args select Expression.Convert(arg.Expression, typeof(object))).ToArray<UnaryExpression>();
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return this._runtime.GetDynamicMemberNames(this.Value);
        }

        private BindingRestrictions GetRestrictions()
        {
            if ((this.Value == null) && base.HasValue)
            {
                return BindingRestrictions.GetInstanceRestriction(base.Expression, null);
            }
            return BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
        }

        private T Value
        {
            get
            {
                return (T) base.Value;
            }
        }

        private delegate DynamicMetaObject Fallback(DynamicMetaObject errorSuggestion);

        private sealed class GetBinderAdapter : GetMemberBinder
        {
            internal GetBinderAdapter(InvokeMemberBinder binder) : base(binder.Name, binder.IgnoreCase)
            {
            }

            public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
            {
                throw new NotSupportedException();
            }
        }
    }
}

