using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;
using WaxNBT.Tags;

namespace WaxNBT;

public class NbtReaderOld
{
    public Encoding StringEncoder = Encoding.UTF8;

    private readonly byte[] _data;
    private int _position;

    public NbtReaderOld(Stream stream)
    {
        using var ms = new MemoryStream();
        stream.CopyTo(ms);

        _data = ms.ToArray();
    }

    public NbtReaderOld(byte[] data) => _data = data;

    public void Skip(int length) => _position += length;
    
    public NbtTagType ReadTagType() => (NbtTagType)ReadByte();

    public byte ReadByte()
    {
        var data = _data[_position];
        _position++;

        return data;
    }

    public byte[] ReadArray(int length)
    {
        var data = _data[_position..(_position + length)];
        _position += length;
        
        return data;
    }
    
    public short ReadShort()
    {
        var data = ReadArray(2);
        return BinaryPrimitives.ReadInt16BigEndian(data);
    }

    public int ReadInt()
    {
        var data = ReadArray(4);
        return BinaryPrimitives.ReadInt32BigEndian(data);
    }

    public long ReadLong()
    {
        var data = ReadArray(8);
        return BinaryPrimitives.ReadInt64BigEndian(data);
    }
    
    public float ReadFloat()
    {
        var data = ReadArray(4);
        
        Array.Reverse(data);
        return BitConverter.ToSingle(data);
    }
    
    public double ReadDouble()
    {
        var data = ReadArray(8);
        
        Array.Reverse(data);
        return BitConverter.ToDouble(data);
    }

    public string ReadString()
    {
        var length = ReadShort();

        var data = ReadArray(length);

        return StringEncoder.GetString(data);
    }
}