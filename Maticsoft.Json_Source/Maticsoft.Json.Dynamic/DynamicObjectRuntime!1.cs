namespace Maticsoft.Json.Dynamic
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class DynamicObjectRuntime<T>
    {
        public Func<T, IEnumerable<string>> GetDynamicMemberNames
        {
            [CompilerGenerated]
            get
            {
                return this.<GetDynamicMemberNames>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<GetDynamicMemberNames>k__BackingField = value;
            }
        }

        public Func<T, BinaryOperationBinder, object, Option<object>> TryBinaryOperation
        {
            [CompilerGenerated]
            get
            {
                return this.<TryBinaryOperation>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryBinaryOperation>k__BackingField = value;
            }
        }

        public Func<T, ConvertBinder, object[], Option<object>> TryConvert
        {
            [CompilerGenerated]
            get
            {
                return this.<TryConvert>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryConvert>k__BackingField = value;
            }
        }

        public Func<T, CreateInstanceBinder, object[], Option<object>> TryCreateInstance
        {
            [CompilerGenerated]
            get
            {
                return this.<TryCreateInstance>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryCreateInstance>k__BackingField = value;
            }
        }

        public Func<T, DeleteIndexBinder, object[], Option<object>> TryDeleteIndex
        {
            [CompilerGenerated]
            get
            {
                return this.<TryDeleteIndex>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryDeleteIndex>k__BackingField = value;
            }
        }

        public Func<T, DeleteMemberBinder, bool> TryDeleteMember
        {
            [CompilerGenerated]
            get
            {
                return this.<TryDeleteMember>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryDeleteMember>k__BackingField = value;
            }
        }

        public Func<T, GetIndexBinder, object[], Option<object>> TryGetIndex
        {
            [CompilerGenerated]
            get
            {
                return this.<TryGetIndex>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryGetIndex>k__BackingField = value;
            }
        }

        public Func<T, GetMemberBinder, Option<object>> TryGetMember
        {
            [CompilerGenerated]
            get
            {
                return this.<TryGetMember>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryGetMember>k__BackingField = value;
            }
        }

        public Func<T, InvokeBinder, object[], Option<object>> TryInvoke
        {
            [CompilerGenerated]
            get
            {
                return this.<TryInvoke>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryInvoke>k__BackingField = value;
            }
        }

        public Func<T, InvokeMemberBinder, object[], Option<object>> TryInvokeMember
        {
            [CompilerGenerated]
            get
            {
                return this.<TryInvokeMember>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryInvokeMember>k__BackingField = value;
            }
        }

        public Func<T, SetIndexBinder, object[], object> TrySetIndex
        {
            [CompilerGenerated]
            get
            {
                return this.<TrySetIndex>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TrySetIndex>k__BackingField = value;
            }
        }

        public Func<T, SetMemberBinder, object, bool> TrySetMember
        {
            [CompilerGenerated]
            get
            {
                return this.<TrySetMember>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TrySetMember>k__BackingField = value;
            }
        }

        public Func<T, UnaryOperationBinder, Option<object>> TryUnaryOperation
        {
            [CompilerGenerated]
            get
            {
                return this.<TryUnaryOperation>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<TryUnaryOperation>k__BackingField = value;
            }
        }
    }
}

