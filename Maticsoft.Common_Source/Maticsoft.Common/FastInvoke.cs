namespace Maticsoft.Common
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public class FastInvoke
    {
        private static void EmitBoxIfNeeded(ILGenerator il, Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
        }

        private static void EmitCastToReference(ILGenerator il, Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                il.Emit(OpCodes.Castclass, type);
            }
        }

        private static void EmitFastInt(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;

                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;

                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;

                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;

                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;

                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;

                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;

                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;

                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;

                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }
            if ((value > -129) && (value < 0x80))
            {
                il.Emit(OpCodes.Ldc_I4_S, (sbyte) value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }

        public static FastInvokeHandler GetMethodInvoker(MethodInfo methodInfo)
        {
            DynamicMethod method = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, methodInfo.DeclaringType.Module);
            ILGenerator iLGenerator = method.GetILGenerator();
            ParameterInfo[] parameters = methodInfo.GetParameters();
            Type[] typeArray = new Type[parameters.Length];
            for (int i = 0; i < typeArray.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                {
                    typeArray[i] = parameters[i].ParameterType.GetElementType();
                }
                else
                {
                    typeArray[i] = parameters[i].ParameterType;
                }
            }
            LocalBuilder[] builderArray = new LocalBuilder[typeArray.Length];
            for (int j = 0; j < typeArray.Length; j++)
            {
                builderArray[j] = iLGenerator.DeclareLocal(typeArray[j], true);
            }
            for (int k = 0; k < typeArray.Length; k++)
            {
                iLGenerator.Emit(OpCodes.Ldarg_1);
                EmitFastInt(iLGenerator, k);
                iLGenerator.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(iLGenerator, typeArray[k]);
                iLGenerator.Emit(OpCodes.Stloc, builderArray[k]);
            }
            if (!methodInfo.IsStatic)
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
            }
            for (int m = 0; m < typeArray.Length; m++)
            {
                if (parameters[m].ParameterType.IsByRef)
                {
                    iLGenerator.Emit(OpCodes.Ldloca_S, builderArray[m]);
                }
                else
                {
                    iLGenerator.Emit(OpCodes.Ldloc, builderArray[m]);
                }
            }
            if (methodInfo.IsStatic)
            {
                iLGenerator.EmitCall(OpCodes.Call, methodInfo, null);
            }
            else
            {
                iLGenerator.EmitCall(OpCodes.Callvirt, methodInfo, null);
            }
            if (methodInfo.ReturnType == typeof(void))
            {
                iLGenerator.Emit(OpCodes.Ldnull);
            }
            else
            {
                EmitBoxIfNeeded(iLGenerator, methodInfo.ReturnType);
            }
            for (int n = 0; n < typeArray.Length; n++)
            {
                if (parameters[n].ParameterType.IsByRef)
                {
                    iLGenerator.Emit(OpCodes.Ldarg_1);
                    EmitFastInt(iLGenerator, n);
                    iLGenerator.Emit(OpCodes.Ldloc, builderArray[n]);
                    if (builderArray[n].LocalType.IsValueType)
                    {
                        iLGenerator.Emit(OpCodes.Box, builderArray[n].LocalType);
                    }
                    iLGenerator.Emit(OpCodes.Stelem_Ref);
                }
            }
            iLGenerator.Emit(OpCodes.Ret);
            return (FastInvokeHandler) method.CreateDelegate(typeof(FastInvokeHandler));
        }

        private static object InvokeMethod(FastInvokeHandler invoke, object target, params object[] paramters)
        {
            return invoke(null, paramters);
        }

        public delegate object FastInvokeHandler(object target, object[] paramters);
    }
}

