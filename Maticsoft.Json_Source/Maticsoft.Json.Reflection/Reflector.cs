namespace Maticsoft.Json.Reflection
{
    using System;

    public sealed class Reflector
    {
        private static readonly Type[] _commonTupleTypes = new Type[] { typeof(Tuple<,>), typeof(Tuple<,,>), typeof(Tuple<,,,>), typeof(Tuple<,,,,>) };

        private Reflector()
        {
            throw new NotSupportedException();
        }

        internal static bool IsConstructionOfGenericTypeDefinition(Type type, Type genericTypeDefinition)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (genericTypeDefinition == null)
            {
                throw new ArgumentNullException("genericTypeDefinition");
            }
            if (!genericTypeDefinition.IsGenericTypeDefinition)
            {
                throw new ArgumentException(string.Format("{0} is not a generic type definition.", genericTypeDefinition), "genericTypeDefinition");
            }
            return ((type.IsGenericType && !type.IsGenericTypeDefinition) && (type.GetGenericTypeDefinition() == genericTypeDefinition));
        }

        public static bool IsConstructionOfNullable(Type type)
        {
            return IsConstructionOfGenericTypeDefinition(type, typeof(Nullable<>));
        }

        public static bool IsTupleFamily(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (!type.IsGenericType || type.IsGenericTypeDefinition)
            {
                return false;
            }
            if (Array.IndexOf<Type>(_commonTupleTypes, type.GetGenericTypeDefinition()) >= 0)
            {
                return true;
            }
            Type type2 = _commonTupleTypes[0];
            int index = type.FullName.IndexOf('`');
            return (((type.Assembly == type2.Assembly) && (index == type2.FullName.IndexOf('`'))) && (0 == string.CompareOrdinal(type2.FullName, 0, type.FullName, 0, index)));
        }
    }
}

