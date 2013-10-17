namespace Maticsoft.Json
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class JsonWriterBase : JsonWriter
    {
        private int _maxDepth = 30;
        private WriterState _state = new WriterState(JsonWriterBracket.Pending);
        private WriterStateStack _stateStack;

        protected JsonWriterBase()
        {
        }

        private void EnsureMemberOnObjectBracket()
        {
            if (this._state.Bracket == JsonWriterBracket.Object)
            {
                throw new JsonException("A JSON member value inside a JSON object must be preceded by its member name.");
            }
        }

        private void EnsureNotEnded()
        {
            if (this._state.Bracket == JsonWriterBracket.Closed)
            {
                throw new JsonException("JSON data has already been ended.");
            }
        }

        private void EnterBracket(JsonWriterBracket newBracket)
        {
            this.States.Push(this._state);
            this._state = new WriterState(newBracket);
        }

        private void EnteringBracket()
        {
            this.EnsureNotEnded();
            if (this._state.Bracket != JsonWriterBracket.Pending)
            {
                this.EnsureMemberOnObjectBracket();
            }
            if ((this.Depth + 1) > this.MaxDepth)
            {
                throw new Exception("Maximum allowed depth has been exceeded.");
            }
        }

        private void ExitBracket()
        {
            this._state = this.States.Pop();
            if (this._state.Bracket == JsonWriterBracket.Pending)
            {
                this._state.Bracket = JsonWriterBracket.Closed;
            }
            else
            {
                this.OnValueWritten();
            }
        }

        private void OnValueWritten()
        {
            if (this._state.Bracket == JsonWriterBracket.Member)
            {
                this._state.Bracket = JsonWriterBracket.Object;
            }
            this._state.Index++;
        }

        public sealed override void WriteBoolean(bool value)
        {
            if (this.Depth == 0)
            {
                this.WriteStartArray();
                this.WriteBoolean(value);
                this.WriteEndArray();
            }
            else
            {
                this.EnsureMemberOnObjectBracket();
                this.WriteBooleanImpl(value);
                this.OnValueWritten();
            }
        }

        protected abstract void WriteBooleanImpl(bool value);
        public sealed override void WriteEndArray()
        {
            if (this._state.Bracket != JsonWriterBracket.Array)
            {
                throw new JsonException("JSON Array tail not expected at this time.");
            }
            this.WriteEndArrayImpl();
            this.ExitBracket();
        }

        protected abstract void WriteEndArrayImpl();
        public sealed override void WriteEndObject()
        {
            if (this._state.Bracket != JsonWriterBracket.Object)
            {
                throw new JsonException("JSON Object tail not expected at this time.");
            }
            this.WriteEndObjectImpl();
            this.ExitBracket();
        }

        protected abstract void WriteEndObjectImpl();
        public sealed override void WriteMember(string name)
        {
            if (this._state.Bracket != JsonWriterBracket.Object)
            {
                throw new JsonException("A JSON Object member is not valid inside a JSON Array.");
            }
            this.WriteMemberImpl(name);
            this._state.Bracket = JsonWriterBracket.Member;
        }

        protected abstract void WriteMemberImpl(string name);
        public sealed override void WriteNull()
        {
            if (this.Depth == 0)
            {
                this.WriteStartArray();
                this.WriteNull();
                this.WriteEndArray();
            }
            else
            {
                this.EnsureMemberOnObjectBracket();
                this.WriteNullImpl();
                this.OnValueWritten();
            }
        }

        protected abstract void WriteNullImpl();
        public sealed override void WriteNumber(string value)
        {
            if (this.Depth == 0)
            {
                this.WriteStartArray();
                this.WriteNumber(value);
                this.WriteEndArray();
            }
            else
            {
                this.EnsureMemberOnObjectBracket();
                this.WriteNumberImpl(value);
                this.OnValueWritten();
            }
        }

        protected abstract void WriteNumberImpl(string value);
        public sealed override void WriteStartArray()
        {
            this.EnteringBracket();
            this.WriteStartArrayImpl();
            this.EnterBracket(JsonWriterBracket.Array);
        }

        protected abstract void WriteStartArrayImpl();
        public sealed override void WriteStartObject()
        {
            this.EnteringBracket();
            this.WriteStartObjectImpl();
            this.EnterBracket(JsonWriterBracket.Object);
        }

        protected abstract void WriteStartObjectImpl();
        public sealed override void WriteString(string value)
        {
            this.WriteStringOrChars(value, null, 0, 0);
        }

        public sealed override void WriteString(char[] chars, int offset, int length)
        {
            if (chars == null)
            {
                throw new ArgumentNullException("chars");
            }
            this.WriteStringOrChars(null, chars, offset, length);
        }

        protected abstract void WriteStringImpl(string value);
        protected virtual void WriteStringImpl(char[] buffers, int offset, int length)
        {
            this.WriteStringImpl(new string(buffers, offset, length));
        }

        private void WriteStringOrChars(string value, char[] chars, int offset, int length)
        {
            if (this.Depth == 0)
            {
                this.WriteStartArray();
                if (chars != null)
                {
                    this.WriteString(chars, offset, length);
                }
                else
                {
                    this.WriteString(value);
                }
                this.WriteEndArray();
            }
            else
            {
                this.EnsureMemberOnObjectBracket();
                if (chars != null)
                {
                    this.WriteStringImpl(chars, offset, length);
                }
                else
                {
                    this.WriteStringImpl(value);
                }
                this.OnValueWritten();
            }
        }

        public sealed override JsonWriterBracket Bracket
        {
            get
            {
                return this._state.Bracket;
            }
        }

        public sealed override int Depth
        {
            get
            {
                if (!this.HasStates)
                {
                    return 0;
                }
                return this.States.Count;
            }
        }

        private bool HasStates
        {
            get
            {
                return ((this._stateStack != null) && (this._stateStack.Count > 0));
            }
        }

        public sealed override int Index
        {
            get
            {
                if (this.Depth != 0)
                {
                    return this._state.Index;
                }
                return -1;
            }
        }

        public override int MaxDepth
        {
            get
            {
                return this._maxDepth;
            }
            set
            {
                this._maxDepth = value;
            }
        }

        private WriterStateStack States
        {
            get
            {
                if (this._stateStack == null)
                {
                    this._stateStack = new WriterStateStack();
                }
                return this._stateStack;
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct WriterState
        {
            public JsonWriterBracket Bracket;
            public int Index;
            public WriterState(JsonWriterBracket bracket)
            {
                this.Bracket = bracket;
                this.Index = 0;
            }
        }

        [Serializable]
        private sealed class WriterStateStack
        {
            private int _count;
            private JsonWriterBase.WriterState[] _states;

            public JsonWriterBase.WriterState Pop()
            {
                if (this._count == 0)
                {
                    throw new InvalidOperationException();
                }
                JsonWriterBase.WriterState state = this._states[--this._count];
                if (this._count == 0)
                {
                    this._states = null;
                }
                return state;
            }

            public void Push(JsonWriterBase.WriterState state)
            {
                if (this._states == null)
                {
                    this._states = new JsonWriterBase.WriterState[6];
                }
                else if (this._count == this._states.Length)
                {
                    JsonWriterBase.WriterState[] array = new JsonWriterBase.WriterState[this._states.Length * 2];
                    this._states.CopyTo(array, 0);
                    this._states = array;
                }
                this._states[this._count++] = state;
            }

            public int Count
            {
                get
                {
                    return this._count;
                }
            }
        }
    }
}

