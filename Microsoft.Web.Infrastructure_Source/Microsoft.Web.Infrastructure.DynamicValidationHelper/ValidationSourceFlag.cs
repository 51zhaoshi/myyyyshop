namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;

    internal enum ValidationSourceFlag
    {
        contentEncodingResolved = 0x20,
        hasValidateInputBeenCalled = 0x8000,
        needToValidateCookielessHeader = 0x10000,
        needToValidateCookies = 4,
        needToValidateForm = 2,
        needToValidateHeaders = 8,
        needToValidatePath = 0x100,
        needToValidatePathInfo = 0x200,
        needToValidatePostedFiles = 0x40,
        needToValidateQueryString = 1,
        needToValidateRawUrl = 0x80,
        needToValidateServerVariables = 0x10
    }
}

