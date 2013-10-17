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

    public class TupleExporter : ExporterBase
    {
        private readonly Action<ExportContext, object, JsonWriter> _exporter;
        private static readonly MethodInfo _exportMethod = ((MethodCallExpression) context => context.Export(null, null).Body).Method;

        public TupleExporter(Type inputType) : base(inputType)
        {
            if (!Reflector.IsTupleFamily(inputType))
            {
                throw new ArgumentException(null, "inputType");
            }
            this._exporter = CompileExporter(inputType);
        }

        private static Action<ExportContext, object, JsonWriter> CompileExporter(Type tupleType)
        {
            ParameterExpression expression;
            ParameterExpression expression2;
            ParameterExpression expression3;
            ParameterExpression left = Expression.Variable(tupleType, "tuple");
            return Expression.Lambda<Action<ExportContext, object, JsonWriter>>(Expression.Block(new ParameterExpression[] { left }, new BinaryExpression[] { Expression.Assign(left, Expression.Convert(expression2 = Expression.Parameter(typeof(object), "obj"), tupleType)) }.Concat<Expression>(CreateItemExportCallExpressions(expression = Expression.Parameter(typeof(ExportContext), "context"), left, expression3 = Expression.Parameter(typeof(JsonWriter), "writer")))), new ParameterExpression[] { expression, expression2, expression3 }).Compile();
        }

        private static IEnumerable<Expression> CreateItemExportCallExpressions(ParameterExpression context, ParameterExpression tuple, ParameterExpression writer)
        {
            return (IEnumerable<Expression>) (from property in tuple.Type.GetProperties() select Expression.Call(context, _exportMethod, Expression.Convert(Expression.MakeMemberAccess(tuple, property), typeof(object)), writer));
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WriteStartArray();
            this._exporter(context, value, writer);
            writer.WriteEndArray();
        }
    }
}

