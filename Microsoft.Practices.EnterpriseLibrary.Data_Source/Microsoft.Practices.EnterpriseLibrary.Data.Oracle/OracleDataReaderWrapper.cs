namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.OracleClient;
    using System.Globalization;
    using System.Reflection;

    internal class OracleDataReaderWrapper : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable
    {
        private OracleDataReader innerReader;

        public OracleDataReaderWrapper(OracleDataReader reader)
        {
            this.innerReader = reader;
        }

        public void Close()
        {
            this.InnerReader.Close();
        }

        public bool GetBoolean(int index)
        {
            return Convert.ToBoolean(this.InnerReader[index], CultureInfo.InvariantCulture);
        }

        public byte GetByte(int index)
        {
            return Convert.ToByte(this.InnerReader[index], CultureInfo.InvariantCulture);
        }

        public long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
        {
            return this.InnerReader.GetBytes(ordinal, dataIndex, buffer, bufferIndex, length);
        }

        public char GetChar(int index)
        {
            return this.InnerReader.GetChar(index);
        }

        public long GetChars(int index, long dataIndex, char[] buffer, int bufferIndex, int length)
        {
            return this.InnerReader.GetChars(index, dataIndex, buffer, bufferIndex, length);
        }

        public IDataReader GetData(int index)
        {
            return this.InnerReader.GetData(index);
        }

        public string GetDataTypeName(int index)
        {
            return this.InnerReader.GetDataTypeName(index);
        }

        public DateTime GetDateTime(int ordinal_)
        {
            return this.InnerReader.GetDateTime(ordinal_);
        }

        public decimal GetDecimal(int index)
        {
            return this.InnerReader.GetDecimal(index);
        }

        public double GetDouble(int index)
        {
            return this.InnerReader.GetDouble(index);
        }

        public Type GetFieldType(int index)
        {
            return this.InnerReader.GetFieldType(index);
        }

        public float GetFloat(int index)
        {
            return this.InnerReader.GetFloat(index);
        }

        public Guid GetGuid(int index)
        {
            return new Guid((byte[]) this.InnerReader[index]);
        }

        public short GetInt16(int index)
        {
            return Convert.ToInt16(this.InnerReader[index], CultureInfo.InvariantCulture);
        }

        public int GetInt32(int index)
        {
            return this.InnerReader.GetInt32(index);
        }

        public long GetInt64(int index)
        {
            return this.InnerReader.GetInt64(index);
        }

        public string GetName(int index)
        {
            return this.InnerReader.GetName(index);
        }

        public int GetOrdinal(string index)
        {
            return this.InnerReader.GetOrdinal(index);
        }

        public DataTable GetSchemaTable()
        {
            return this.InnerReader.GetSchemaTable();
        }

        public string GetString(int index)
        {
            return this.InnerReader.GetString(index);
        }

        public object GetValue(int index)
        {
            return this.InnerReader.GetValue(index);
        }

        public int GetValues(object[] values)
        {
            return this.InnerReader.GetValues(values);
        }

        public bool IsDBNull(int index)
        {
            return this.InnerReader.IsDBNull(index);
        }

        public bool NextResult()
        {
            return this.InnerReader.NextResult();
        }

        public static explicit operator OracleDataReader(OracleDataReaderWrapper oracleDataReaderWrapper)
        {
            return oracleDataReaderWrapper.InnerReader;
        }

        public bool Read()
        {
            return this.InnerReader.Read();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.InnerReader.GetEnumerator();
        }

        void IDisposable.Dispose()
        {
            this.InnerReader.Dispose();
        }

        public int Depth
        {
            get
            {
                return this.InnerReader.Depth;
            }
        }

        public int FieldCount
        {
            get
            {
                return this.InnerReader.FieldCount;
            }
        }

        public OracleDataReader InnerReader
        {
            get
            {
                return this.innerReader;
            }
        }

        public bool IsClosed
        {
            get
            {
                return this.InnerReader.IsClosed;
            }
        }

        public object this[int index]
        {
            get
            {
                return this.InnerReader[index];
            }
        }

        public object this[string name]
        {
            get
            {
                return this.InnerReader[name];
            }
        }

        public int RecordsAffected
        {
            get
            {
                return this.InnerReader.RecordsAffected;
            }
        }
    }
}

