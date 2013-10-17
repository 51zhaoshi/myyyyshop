namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using Microsoft.Web.Infrastructure;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Web;
    using System.Web.Util;

    [SecurityCritical]
    internal sealed class GranularValidationReflectionUtil
    {
        private Func<HttpRequest, int, bool> _del_BitVector32_get_Item;
        private Action<HttpRequest, int, bool> _del_BitVector32_set_Item;
        private Func<HttpRequest, NameValueCollection> _del_get_HttpRequest_form;
        private Func<HttpRequest, NameValueCollection> _del_get_HttpRequest_queryString;
        private Func<NameObjectCollectionBase, ArrayList> _del_get_NameObjectCollectionBase_entriesArray;
        private Func<NameObjectCollectionBase, Hashtable> _del_get_NameObjectCollectionBase_entriesTable;
        private Func<object, string> _del_get_NameObjectEntry_Key;
        private Func<object, object> _del_get_NameObjectEntry_Value;
        private Func<NameValueCollection> _del_HttpValueCollection_ctor;
        private Action<object, object> _del_set_HttpRequest_form;
        private Action<object, object> _del_set_HttpRequest_queryString;
        private Action<NameObjectCollectionBase, ArrayList> _del_set_NameObjectCollectionBase_entriesArray;
        private Action<NameObjectCollectionBase, Hashtable> _del_set_NameObjectCollectionBase_entriesTable;
        private Action<object, object> _del_set_NameObjectEntry_Value;
        private Func<HttpRequest, ValidateStringCallback> _del_validateStringCallback;
        public static readonly GranularValidationReflectionUtil Instance = GetInstance();

        private GranularValidationReflectionUtil()
        {
        }

        public NameValueCollection GetHttpRequestFormField(HttpRequest target)
        {
            return this._del_get_HttpRequest_form(target);
        }

        public NameValueCollection GetHttpRequestQueryStringField(HttpRequest target)
        {
            return this._del_get_HttpRequest_queryString(target);
        }

        private static GranularValidationReflectionUtil GetInstance()
        {
            try
            {
                if (DynamicValidationShimReflectionUtil.Instance != null)
                {
                    return null;
                }
                GranularValidationReflectionUtil util = new GranularValidationReflectionUtil();
                Type containingType = typeof(NameObjectCollectionBase);
                string fieldName = "_entriesArray";
                bool isStatic = false;
                Type fieldType = typeof(ArrayList);
                FieldInfo fieldInfo = CommonReflectionUtil.FindField(containingType, fieldName, isStatic, fieldType);
                util._del_get_NameObjectCollectionBase_entriesArray = MakeFieldGetterFunc<NameObjectCollectionBase, ArrayList>(fieldInfo);
                util._del_set_NameObjectCollectionBase_entriesArray = MakeFieldSetterFunc<NameObjectCollectionBase, ArrayList>(fieldInfo);
                Type type6 = typeof(NameObjectCollectionBase);
                string str2 = "_entriesTable";
                bool flag2 = false;
                Type type7 = typeof(Hashtable);
                FieldInfo info2 = CommonReflectionUtil.FindField(type6, str2, flag2, type7);
                util._del_get_NameObjectCollectionBase_entriesTable = MakeFieldGetterFunc<NameObjectCollectionBase, Hashtable>(info2);
                util._del_set_NameObjectCollectionBase_entriesTable = MakeFieldSetterFunc<NameObjectCollectionBase, Hashtable>(info2);
                Type targetType = CommonAssemblies.System.GetType("System.Collections.Specialized.NameObjectCollectionBase+NameObjectEntry");
                Type type8 = targetType;
                string str3 = "Key";
                bool flag3 = false;
                Type type9 = typeof(string);
                FieldInfo info3 = CommonReflectionUtil.FindField(type8, str3, flag3, type9);
                util._del_get_NameObjectEntry_Key = MakeFieldGetterFunc<string>(targetType, info3);
                Type type10 = targetType;
                string str4 = "Value";
                bool flag4 = false;
                Type type11 = typeof(object);
                FieldInfo info4 = CommonReflectionUtil.FindField(type10, str4, flag4, type11);
                util._del_get_NameObjectEntry_Value = MakeFieldGetterFunc<object>(targetType, info4);
                util._del_set_NameObjectEntry_Value = MakeFieldSetterFunc(targetType, info4);
                Type type12 = typeof(HttpRequest);
                string methodName = "ValidateString";
                bool flag5 = false;
                Type[] argumentTypes = new Type[] { typeof(string), typeof(string), typeof(RequestValidationSource) };
                Type returnType = typeof(void);
                MethodInfo methodInfo = CommonReflectionUtil.FindMethod(type12, methodName, flag5, argumentTypes, returnType);
                util._del_validateStringCallback = CommonReflectionUtil.MakeFastCreateDelegate<HttpRequest, ValidateStringCallback>(methodInfo);
                Type type = CommonAssemblies.SystemWeb.GetType("System.Web.HttpValueCollection");
                util._del_HttpValueCollection_ctor = CommonReflectionUtil.MakeFastNewObject<Func<NameValueCollection>>(type);
                Type type14 = typeof(HttpRequest);
                string str6 = "_form";
                bool flag6 = false;
                Type type15 = type;
                FieldInfo info6 = CommonReflectionUtil.FindField(type14, str6, flag6, type15);
                util._del_get_HttpRequest_form = MakeFieldGetterFunc<HttpRequest, NameValueCollection>(info6);
                util._del_set_HttpRequest_form = MakeFieldSetterFunc(typeof(HttpRequest), info6);
                Type type16 = typeof(HttpRequest);
                string str7 = "_queryString";
                bool flag7 = false;
                Type type17 = type;
                FieldInfo info7 = CommonReflectionUtil.FindField(type16, str7, flag7, type17);
                util._del_get_HttpRequest_queryString = MakeFieldGetterFunc<HttpRequest, NameValueCollection>(info7);
                util._del_set_HttpRequest_queryString = MakeFieldSetterFunc(typeof(HttpRequest), info7);
                Type type3 = CommonAssemblies.SystemWeb.GetType("System.Web.Util.SimpleBitVector32");
                Type type18 = typeof(HttpRequest);
                string str8 = "_flags";
                bool flag8 = false;
                Type type19 = type3;
                FieldInfo flagsFieldInfo = CommonReflectionUtil.FindField(type18, str8, flag8, type19);
                Type type20 = type3;
                string str9 = "get_Item";
                bool flag9 = false;
                Type[] typeArray4 = new Type[] { typeof(int) };
                Type type21 = typeof(bool);
                MethodInfo itemGetter = CommonReflectionUtil.FindMethod(type20, str9, flag9, typeArray4, type21);
                Type type22 = type3;
                string str10 = "set_Item";
                bool flag10 = false;
                Type[] typeArray6 = new Type[] { typeof(int), typeof(bool) };
                Type type23 = typeof(void);
                MethodInfo itemSetter = CommonReflectionUtil.FindMethod(type22, str10, flag10, typeArray6, type23);
                MakeRequestValidationFlagsAccessors(flagsFieldInfo, itemGetter, itemSetter, out util._del_BitVector32_get_Item, out util._del_BitVector32_set_Item);
                return util;
            }
            catch
            {
                return null;
            }
        }

        public ArrayList GetNameObjectCollectionEntriesArray(NameObjectCollectionBase target)
        {
            return this._del_get_NameObjectCollectionBase_entriesArray(target);
        }

        public Hashtable GetNameObjectCollectionEntriesTable(NameObjectCollectionBase target)
        {
            return this._del_get_NameObjectCollectionBase_entriesTable(target);
        }

        public string GetNameObjectEntryKey(object target)
        {
            return this._del_get_NameObjectEntry_Key(target);
        }

        public object GetNameObjectEntryValue(object target)
        {
            return this._del_get_NameObjectEntry_Value(target);
        }

        public bool GetRequestValidationFlag(HttpRequest target, ValidationSourceFlag flag)
        {
            return this._del_BitVector32_get_Item(target, (int) flag);
        }

        [ReflectionPermission(SecurityAction.Assert, MemberAccess=true)]
        private static Func<TTarget, TFieldType> MakeFieldGetterFunc<TTarget, TFieldType>(FieldInfo fieldInfo)
        {
            ParameterExpression expression = Expression.Parameter(typeof(TTarget));
            return Expression.Lambda<Func<TTarget, TFieldType>>(Expression.Field(expression, fieldInfo), new ParameterExpression[] { expression }).Compile();
        }

        [ReflectionPermission(SecurityAction.Assert, MemberAccess=true)]
        private static Func<object, TFieldType> MakeFieldGetterFunc<TFieldType>(Type targetType, FieldInfo fieldInfo)
        {
            ParameterExpression expression = Expression.Parameter(typeof(object));
            return Expression.Lambda<Func<object, TFieldType>>(Expression.Field(Expression.Convert(expression, targetType), fieldInfo), new ParameterExpression[] { expression }).Compile();
        }

        [ReflectionPermission(SecurityAction.Assert, MemberAccess=true)]
        private static Action<TTarget, TFieldType> MakeFieldSetterFunc<TTarget, TFieldType>(FieldInfo fieldInfo)
        {
            ParameterExpression expression = Expression.Parameter(typeof(TTarget));
            ParameterExpression right = Expression.Parameter(typeof(TFieldType));
            return Expression.Lambda<Action<TTarget, TFieldType>>(Expression.Assign(Expression.Field(expression, fieldInfo), right), new ParameterExpression[] { expression, right }).Compile();
        }

        [ReflectionPermission(SecurityAction.Assert, MemberAccess=true)]
        private static Action<object, object> MakeFieldSetterFunc(Type targetType, FieldInfo fieldInfo)
        {
            ParameterExpression expression = Expression.Parameter(typeof(object));
            UnaryExpression expression2 = Expression.Convert(expression, targetType);
            ParameterExpression expression3 = Expression.Parameter(typeof(object));
            UnaryExpression right = Expression.Convert(expression3, fieldInfo.FieldType);
            return Expression.Lambda<Action<object, object>>(Expression.Assign(Expression.Field(expression2, fieldInfo), right), new ParameterExpression[] { expression, expression3 }).Compile();
        }

        [ReflectionPermission(SecurityAction.Assert, MemberAccess=true)]
        private static void MakeRequestValidationFlagsAccessors(FieldInfo flagsFieldInfo, MethodInfo itemGetter, MethodInfo itemSetter, out Func<HttpRequest, int, bool> delGetter, out Action<HttpRequest, int, bool> delSetter)
        {
            ParameterExpression expression = Expression.Parameter(typeof(HttpRequest));
            ParameterExpression expression2 = Expression.Parameter(typeof(int));
            ParameterExpression expression3 = Expression.Parameter(typeof(bool));
            MemberExpression instance = Expression.Field(expression, flagsFieldInfo);
            delGetter = Expression.Lambda<Func<HttpRequest, int, bool>>(Expression.Call(instance, itemGetter, new Expression[] { expression2 }), new ParameterExpression[] { expression, expression2 }).Compile();
            delSetter = Expression.Lambda<Action<HttpRequest, int, bool>>(Expression.Call(instance, itemSetter, expression2, expression3), new ParameterExpression[] { expression, expression2, expression3 }).Compile();
        }

        public ValidateStringCallback MakeValidateStringCallback(HttpRequest request)
        {
            return this._del_validateStringCallback(request);
        }

        public NameValueCollection NewHttpValueCollection()
        {
            return this._del_HttpValueCollection_ctor();
        }

        public void SetHttpRequestFormField(HttpRequest target, NameValueCollection value)
        {
            this._del_set_HttpRequest_form(target, value);
        }

        public void SetHttpRequestQueryStringField(HttpRequest target, NameValueCollection value)
        {
            this._del_set_HttpRequest_queryString(target, value);
        }

        public void SetNameObjectCollectionEntriesArray(NameObjectCollectionBase target, ArrayList array)
        {
            this._del_set_NameObjectCollectionBase_entriesArray(target, array);
        }

        public void SetNameObjectCollectionEntriesTable(NameObjectCollectionBase target, Hashtable table)
        {
            this._del_set_NameObjectCollectionBase_entriesTable(target, table);
        }

        public void SetNameObjectEntryValue(object target, object newValue)
        {
            this._del_set_NameObjectEntry_Value(target, newValue);
        }

        public void SetRequestValidationFlag(HttpRequest target, ValidationSourceFlag flag, bool value)
        {
            this._del_BitVector32_set_Item(target, (int) flag, value);
        }
    }
}

