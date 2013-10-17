namespace Maticsoft.Json
{
    using System;
    using System.IO;

    public sealed class BufferedCharReader
    {
        private bool _backed;
        private char _backup;
        private char[] _buffer;
        private readonly int _bufferSize;
        private int _charCount;
        private int _end;
        private int _index;
        private int _lastLinePosition;
        private int _lineNumber;
        private int _linePosition;
        private readonly TextReader _reader;
        private bool _sawLineFeed;
        public const char EOF = '\0';

        public BufferedCharReader(TextReader reader) : this(reader, 0)
        {
        }

        public BufferedCharReader(TextReader reader, int bufferSize)
        {
            this._sawLineFeed = true;
            this._reader = reader;
            this._bufferSize = Math.Max(0x100, bufferSize);
        }

        public void Back()
        {
            if (this._charCount != 0)
            {
                this._backed = true;
                this._charCount--;
                this._linePosition--;
                if (this._linePosition == 0)
                {
                    this._lineNumber--;
                    this._linePosition = this._lastLinePosition;
                    this._sawLineFeed = true;
                }
            }
        }

        public bool More()
        {
            if (!this._backed && (this._index == this._end))
            {
                if (this._buffer == null)
                {
                    this._buffer = new char[this._bufferSize];
                }
                this._index = 0;
                this._end = this._reader.Read(this._buffer, 0, this._buffer.Length);
                if (this._end == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public char Next()
        {
            char ch;
            if (this._backed)
            {
                this._backed = false;
                ch = this._backup;
            }
            else
            {
                if (!this.More())
                {
                    return '\0';
                }
                ch = this._buffer[this._index++];
                this._backup = ch;
            }
            return this.UpdateCounters(ch);
        }

        private char UpdateCounters(char ch)
        {
            this._charCount++;
            if (this._sawLineFeed)
            {
                this._lineNumber++;
                this._lastLinePosition = this._linePosition;
                this._linePosition = 1;
                this._sawLineFeed = false;
            }
            else
            {
                this._linePosition++;
            }
            this._sawLineFeed = ch == '\n';
            return ch;
        }

        public int CharCount
        {
            get
            {
                return this._charCount;
            }
        }

        public int LineNumber
        {
            get
            {
                return this._lineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                return this._linePosition;
            }
        }
    }
}

