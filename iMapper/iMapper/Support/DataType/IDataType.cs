using System;

namespace iMapper.Support.DataType
{
    public interface IDataType
    {
        bool IsSupportNullable { get; }
        string EngineType { get; }

        string FrameworkType { get; }

        Type Type { get; }
    }
}