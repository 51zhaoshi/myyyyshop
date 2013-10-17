namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using Maticsoft.Json.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class TupleImporter : ImporterBase
    {
        private readonly Func<ImportContext, JsonReader, object> _importer;
        private static readonly MethodInfo _importMethod = ((MethodCallExpression) context => context.Import(null, null).Body).Method;
        private readonly bool _single;

        public TupleImporter(Type outputType) : base(outputType)
        {
            if (!Reflector.IsTupleFamily(outputType))
            {
                throw new ArgumentException(null, "outputType");
            }
            this._importer = CompileItemsImporter(outputType);
            this._single = outputType.GetGenericArguments().Length == 1;
        }

        private static Func<ImportContext, JsonReader, object> CompileItemsImporter(Type tupleType)
        {
            ParameterExpression context = Expression.Parameter(typeof(ImportContext), "context");
            ParameterExpression reader = Expression.Parameter(typeof(JsonReader), "reader");
            Type[] argTypes = tupleType.GetGenericArguments();
            MethodInfo info = typeof(Tuple).GetMethods(BindingFlags.Public | BindingFlags.Static).Single<MethodInfo>(method => ((method.IsGenericMethodDefinition && "Create".Equals(method.Name, StringComparison.Ordinal)) && (argTypes.Length == method.GetGenericArguments().Length))).MakeGenericMethod(argTypes);
            IEnumerable<UnaryExpression> arguments = from argType in argTypes select Expression.Convert(Expression.Call(context, _importMethod, Expression.Constant(argType), reader), argType);
            return Expression.Lambda<Func<ImportContext, JsonReader, object>>(Expression.Call(info, arguments), new ParameterExpression[] { context, reader }).Compile();
        }

        protected override object ImportFromArray(ImportContext context, JsonReader reader)
        {
            if (this._single)
            {
                return this._importer(context, reader);
            }
            reader.Read();
            object obj2 = this._importer(context, reader);
            reader.ReadToken(JsonTokenClass.EndArray);
            return obj2;
        }

        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            if (!this._single)
            {
                return base.ImportFromBoolean(context, reader);
            }
            return this._importer(context, reader);
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            if (!this._single)
            {
                return base.ImportFromNumber(context, reader);
            }
            return this._importer(context, reader);
        }

        protected override object ImportFromObject(ImportContext context, JsonReader reader)
        {
            if (!this._single)
            {
                return base.ImportFromObject(context, reader);
            }
            return this._importer(context, reader);
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            if (!this._single)
            {
                return base.ImportFromString(context, reader);
            }
            return this._importer(context, reader);
        }
    }
}

