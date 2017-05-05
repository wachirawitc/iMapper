using System;
using System.Collections.Generic;

namespace iMapper.Support.DataType
{
    public class MsDataType
    {
        public List<IDataType> Types { get; set; }

        public MsDataType()
        {
            Types = new List<IDataType>
            {
                new Bigint(),
                new Binary(),
                new Char(),
                new Date(),
                new Datetime(),
                new DatetimeNull(),
                new Datetime2(),
                new Datetimeoffset(),
                new Decimal(),
                new Float(),
                new Image(),
                new Int() ,
                new IntNull(),
                new Money(),
                new Nchar(),
                new Ntext(),
                new Numeric(),
                new Nvarchar(),
                new Rowversion(),
                new SmallDatetime(),
                new SmallInt(),
                new SmallMoney(),
                new Text(),
                new Time(),
                new Timestamp(),
                new TinyInt(),
                new Varbinary(),
                new Varchar(),
                new Varchar2(),
                new Uniqueidentifier(),
                new Bit()
            };
        }
    }

    #region Type

    public class Bigint : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "bigint";
        public string FrameworkType => typeof(long).FullName;
        public Type Type => typeof(long);
    }

    public class Binary : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "binary";
        public string FrameworkType => typeof(byte[]).FullName;
        public Type Type => typeof(byte[]);
    }

    public class Char : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "char";
        public string FrameworkType => typeof(string).FullName;
        public Type Type => typeof(string);
    }

    public class Date : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "date";
        public string FrameworkType => typeof(DateTime).FullName;
        public Type Type => typeof(DateTime);
    }

    public class Datetime : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "datetime";
        public string FrameworkType => typeof(DateTime).FullName;
        public Type Type => typeof(DateTime);
    }

    public class DatetimeNull : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "datetime";
        public string FrameworkType => typeof(DateTime?).FullName;
        public Type Type => typeof(DateTime?);
    }

    public class Datetime2 : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "datetime2";
        public string FrameworkType => typeof(DateTime).FullName;
        public Type Type => typeof(DateTime);
    }

    public class Datetimeoffset : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "datetimeoffset";
        public string FrameworkType => typeof(DateTimeOffset).FullName;
        public Type Type => typeof(DateTimeOffset);
    }

    public class Decimal : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "decimal";
        public string FrameworkType => typeof(decimal).FullName;
        public Type Type => typeof(decimal);
    }

    public class Float : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "float";
        public string FrameworkType => typeof(double).FullName;
        public Type Type => typeof(double);
    }

    public class Image : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "image";
        public string FrameworkType => typeof(byte[]).FullName;
        public Type Type => typeof(byte[]);
    }

    public class Int : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "int";
        public string FrameworkType => typeof(int).FullName;
        public Type Type => typeof(int);
    }

    public class IntNull : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "int";
        public string FrameworkType => typeof(int?).FullName;
        public Type Type => typeof(int?);
    }

    public class Money : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "money";
        public string FrameworkType => typeof(decimal).FullName;
        public Type Type => typeof(decimal);
    }

    public class Nchar : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "nchar";
        public string FrameworkType => typeof(string).FullName;
        public Type Type => typeof(string);
    }

    public class Ntext : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "ntext";
        public string FrameworkType => typeof(string).FullName;
        public Type Type => typeof(string);
    }

    public class Numeric : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "numeric";
        public string FrameworkType => typeof(decimal).FullName;
        public Type Type => typeof(decimal);
    }

    public class Nvarchar : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "nvarchar";
        public string FrameworkType => typeof(string).FullName;
        public Type Type => typeof(string);
    }

    public class Rowversion : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "rowversion";
        public string FrameworkType => typeof(byte[]).FullName;
        public Type Type => typeof(byte[]);
    }

    public class SmallDatetime : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "smalldatetime";
        public string FrameworkType => typeof(DateTime).FullName;
        public Type Type => typeof(DateTime);
    }

    public class SmallInt : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "smallint";
        public string FrameworkType => typeof(short).FullName;
        public Type Type => typeof(short);
    }

    public class SmallMoney : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "smallmoney";
        public string FrameworkType => typeof(decimal).FullName;
        public Type Type => typeof(decimal);
    }

    public class Text : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "text";
        public string FrameworkType => typeof(string).FullName;
        public Type Type => typeof(string);
    }

    public class Time : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "time";
        public string FrameworkType => typeof(TimeSpan).FullName;
        public Type Type => typeof(TimeSpan);
    }

    public class Timestamp : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "timestamp";
        public string FrameworkType => typeof(byte[]).FullName;
        public Type Type => typeof(byte[]);
    }

    public class TinyInt : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "tinyint";
        public string FrameworkType => typeof(byte).FullName;
        public Type Type => typeof(byte);
    }

    public class Varbinary : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "varbinary";
        public string FrameworkType => typeof(byte[]).FullName;
        public Type Type => typeof(byte[]);
    }

    public class Varchar : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "varchar";
        public string FrameworkType => typeof(string).FullName;
        public Type Type => typeof(string);
    }

    public class Varchar2 : IDataType
    {
        public bool IsSupportNullable => false;
        public string EngineType => "varchar2";
        public string FrameworkType => typeof(string).FullName;
        public Type Type => typeof(string);
    }

    public class Uniqueidentifier : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "uniqueidentifier";
        public string FrameworkType => typeof(Guid).FullName;
        public Type Type => typeof(Guid);
    }

    public class Bit : IDataType
    {
        public bool IsSupportNullable => true;
        public string EngineType => "bit";
        public string FrameworkType => typeof(bool).FullName;
        public Type Type => typeof(bool);
    }

    #endregion Type
}