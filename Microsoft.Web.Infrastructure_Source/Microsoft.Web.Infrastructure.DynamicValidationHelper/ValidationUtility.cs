namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Web;
    using System.Web.Util;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ValidationUtility
    {
        [SecuritySafeCritical]
        public static void EnableDynamicValidation(HttpContext context)
        {
            if (DynamicValidationShimReflectionUtil.Instance != null)
            {
                DynamicValidationShimReflectionUtil.Instance.EnableDynamicValidation(context);
            }
            else
            {
                CollectionReplacer.MakeCollectionsLazy(context);
            }
        }

        [SecuritySafeCritical]
        public static void GetUnvalidatedCollections(HttpContext context, out Func<NameValueCollection> formGetter, out Func<NameValueCollection> queryStringGetter)
        {
            if (IsValidationEnabled(context) == true)
            {
                EnableDynamicValidation(context);
            }
            if (DynamicValidationShimReflectionUtil.Instance != null)
            {
                DynamicValidationShimReflectionUtil.Instance.GetUnvalidatedCollections(context, out formGetter, out queryStringGetter);
            }
            else
            {
                UnvalidatedCollections unvalidatedCollections = CollectionReplacer.GetUnvalidatedCollections(context);
                formGetter = new Func<NameValueCollection>(unvalidatedCollections.EnsureForm);
                queryStringGetter = new Func<NameValueCollection>(unvalidatedCollections.EnsureQueryString);
            }
        }

        [SecuritySafeCritical]
        public static bool? IsValidationEnabled(HttpContext context)
        {
            if (DynamicValidationShimReflectionUtil.Instance != null)
            {
                return new bool?(DynamicValidationShimReflectionUtil.Instance.IsValidationEnabled(context));
            }
            return CollectionReplacer.IsValidationEnabled(context);
        }

        [SecurityCritical]
        private static class CollectionReplacer
        {
            private static readonly GranularValidationReflectionUtil _reflectionUtil = GranularValidationReflectionUtil.Instance;
            private static readonly object _unvalidatedCollectionsKey = new object();

            public static ValidationUtility.UnvalidatedCollections GetUnvalidatedCollections(HttpContext context)
            {
                return (((ValidationUtility.UnvalidatedCollections) context.Items[_unvalidatedCollectionsKey]) ?? new ValidationUtility.UnvalidatedCollections(context.Request));
            }

            public static bool? IsValidationEnabled(HttpContext context)
            {
                if (((context != null) && (context.Request != null)) && (_reflectionUtil != null))
                {
                    return new bool?(_reflectionUtil.GetRequestValidationFlag(context.Request, ValidationSourceFlag.hasValidateInputBeenCalled));
                }
                return null;
            }

            public static void MakeCollectionsLazy(HttpContext context)
            {
                HttpRequest request;
                ValidationUtility.UnvalidatedCollections unvalidatedCollections;
                if (((context != null) && (context.Items[_unvalidatedCollectionsKey] == null)) && (_reflectionUtil != null))
                {
                    request = context.Request;
                    request.ValidateInput();
                    unvalidatedCollections = new ValidationUtility.UnvalidatedCollections(request);
                    context.Items[_unvalidatedCollectionsKey] = unvalidatedCollections;
                    HttpContext context2 = context;
                    Func<NameValueCollection> getter = () => _reflectionUtil.GetHttpRequestFormField(request);
                    Action<NameValueCollection> setter = delegate (NameValueCollection val) {
                        _reflectionUtil.SetHttpRequestFormField(request, val);
                    };
                    FieldAccessor<NameValueCollection> fieldAccessor = new FieldAccessor<NameValueCollection>(getter, setter);
                    Func<NameValueCollection> propertyAccessor = () => request.Form;
                    Action<NameValueCollection> storeInUnvalidatedCollection = delegate (NameValueCollection col) {
                        unvalidatedCollections.Form = col;
                    };
                    RequestValidationSource form = RequestValidationSource.Form;
                    ValidationSourceFlag needToValidateForm = ValidationSourceFlag.needToValidateForm;
                    ReplaceCollection(context2, fieldAccessor, propertyAccessor, storeInUnvalidatedCollection, form, needToValidateForm);
                    HttpContext context3 = context;
                    Func<NameValueCollection> func3 = () => _reflectionUtil.GetHttpRequestQueryStringField(request);
                    Action<NameValueCollection> action3 = delegate (NameValueCollection val) {
                        _reflectionUtil.SetHttpRequestQueryStringField(request, val);
                    };
                    FieldAccessor<NameValueCollection> accessor2 = new FieldAccessor<NameValueCollection>(func3, action3);
                    Func<NameValueCollection> func4 = () => request.QueryString;
                    Action<NameValueCollection> action4 = delegate (NameValueCollection col) {
                        unvalidatedCollections.QueryString = col;
                    };
                    RequestValidationSource queryString = RequestValidationSource.QueryString;
                    ValidationSourceFlag needToValidateQueryString = ValidationSourceFlag.needToValidateQueryString;
                    ReplaceCollection(context3, accessor2, func4, action4, queryString, needToValidateQueryString);
                }
            }

            private static void ReplaceCollection(HttpContext context, FieldAccessor<NameValueCollection> fieldAccessor, Func<NameValueCollection> propertyAccessor, Action<NameValueCollection> storeInUnvalidatedCollection, RequestValidationSource validationSource, ValidationSourceFlag validationSourceFlag)
            {
                NameValueCollection originalBackingCollection;
                ValidateStringCallback validateString;
                SimpleValidateStringCallback simpleValidateString;
                Func<NameValueCollection> getActualCollection;
                Action<NameValueCollection> makeCollectionLazy;
                HttpRequest request = context.Request;
                Func<bool> getValidationFlag = () => _reflectionUtil.GetRequestValidationFlag(request, validationSourceFlag);
                Func<bool> func = () => !getValidationFlag();
                Action<bool> setValidationFlag = delegate (bool value) {
                    _reflectionUtil.SetRequestValidationFlag(request, validationSourceFlag, value);
                };
                if ((fieldAccessor.Value != null) && func())
                {
                    storeInUnvalidatedCollection(fieldAccessor.Value);
                }
                else
                {
                    originalBackingCollection = fieldAccessor.Value;
                    validateString = _reflectionUtil.MakeValidateStringCallback(context.Request);
                    simpleValidateString = delegate (string value, string key) {
                        if (((key == null) || !key.StartsWith("__", StringComparison.Ordinal)) && !string.IsNullOrEmpty(value))
                        {
                            validateString(value, key, validationSource);
                        }
                    };
                    getActualCollection = delegate {
                        fieldAccessor.Value = originalBackingCollection;
                        bool flag = getValidationFlag();
                        setValidationFlag(false);
                        NameValueCollection col = propertyAccessor();
                        setValidationFlag(flag);
                        storeInUnvalidatedCollection(new NameValueCollection(col));
                        return col;
                    };
                    makeCollectionLazy = delegate (NameValueCollection col) {
                        simpleValidateString(col[null], null);
                        LazilyValidatingArrayList array = new LazilyValidatingArrayList(_reflectionUtil.GetNameObjectCollectionEntriesArray(col), simpleValidateString);
                        _reflectionUtil.SetNameObjectCollectionEntriesArray(col, array);
                        LazilyValidatingHashtable table = new LazilyValidatingHashtable(_reflectionUtil.GetNameObjectCollectionEntriesTable(col), simpleValidateString);
                        _reflectionUtil.SetNameObjectCollectionEntriesTable(col, table);
                    };
                    Func<bool> hasValidationFired = func;
                    Action disableValidation = delegate {
                        setValidationFlag(false);
                    };
                    Func<int> fillInActualFormContents = delegate {
                        NameValueCollection values = getActualCollection();
                        makeCollectionLazy(values);
                        return values.Count;
                    };
                    DeferredCountArrayList list = new DeferredCountArrayList(hasValidationFired, disableValidation, fillInActualFormContents);
                    NameValueCollection target = _reflectionUtil.NewHttpValueCollection();
                    _reflectionUtil.SetNameObjectCollectionEntriesArray(target, list);
                    fieldAccessor.Value = target;
                }
            }
        }

        [SecurityCritical]
        private sealed class UnvalidatedCollections
        {
            private readonly HttpRequest _request;
            public NameValueCollection Form;
            public NameValueCollection QueryString;

            public UnvalidatedCollections(HttpRequest request)
            {
                this._request = request;
            }

            public NameValueCollection EnsureForm()
            {
                NameValueCollection form = null;
                if (this.Form == null)
                {
                    form = this._request.Form;
                }
                return (this.Form ?? form);
            }

            public NameValueCollection EnsureQueryString()
            {
                NameValueCollection queryString = null;
                if (this.QueryString == null)
                {
                    queryString = this._request.QueryString;
                }
                return (this.QueryString ?? queryString);
            }
        }
    }
}

