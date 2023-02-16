using System.Buffers.Binary;
using System.Text;
using WaxNBT.Tags;

namespace WaxNBT;

public class NbtReader
{
    public Encoding StringEncoder = Encoding.UTF8;

    private readonly byte[] _data;
    private int _position;

    public NbtReader(Stream stream)
    {
        MemoryStream ms = new MemoryStream();
        stream.CopyTo(ms);

        _data = ms.ToArray();
    }

    public NbtReader(byte[] data) => _data = data;

    public void Skip(int lenght) => _position += lenght;
    
    public NbtTagType ReadTagType() => (NbtTagType)ReadByte();

    public NbtTag ReadTag(NbtTagType? type = null, bool readName = true)
    {
        type ??= ReadTagType();

        return type switch
        {
            NbtTagType.End => NbtEnd.FromReader(this, readName),
            NbtTagType.Byte => NbtByte.FromReader(this, readName),
            NbtTagType.Short => NbtShort.FromReader(this, readName),
            NbtTagType.Int => NbtInt.FromReader(this, readName),
            NbtTagType.Long => NbtLong.FromReader(this, readName),
            NbtTagType.Float => NbtFloat.FromReader(this, readName),
            NbtTagType.Double => NbtDouble.FromReader(this, readName),
            NbtTagType.ByteArray => NbtByteArray.FromReader(this, readName),
            NbtTagType.String => NbtString.FromReader(this, readName),
            NbtTagType.List => NbtList.FromReader(this, readName),
            NbtTagType.Compound => NbtCompound.FromReader(this, readName),
            NbtTagType.IntArray => NbtIntArray.FromReader(this, readName),
            NbtTagType.LongArray => NbtLongArray.FromReader(this, readName),
            null => NbtEnd.FromReader(this, readName),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public byte ReadByte()
    {
        byte data = _data[_position];
        _position++;

        return data;
    }

    public byte[] ReadArray(int lenght)
    {
        byte[] data = _data[_position..(_position + lenght)];
        _position += lenght;
        
        return data;
    }
    
    public short ReadShort()
    {
        byte[] data = ReadArray(2);
        return BinaryPrimitives.ReadInt16BigEndian(data);
    }

    public int ReadInt()
    {
        byte[] data = ReadArray(4);
        return BinaryPrimitives.ReadInt32BigEndian(data);
    }

    public long ReadLong()
    {
        byte[] data = ReadArray(8);
        return BinaryPrimitives.ReadInt64BigEndian(data);
    }
    
    public float ReadFloat()
    {
        byte[] data = ReadArray(4);
        
        Array.Reverse(data);
        return BitConverter.ToSingle(data);
    }
    
    public double ReadDouble()
    {
        byte[] data = ReadArray(8);
        
        Array.Reverse(data);
        return BitConverter.ToDouble(data);
    }

    public string ReadString()
    {
        short lenght = ReadShort();

        byte[] data = ReadArray(lenght);

        return StringEncoder.GetString(data);
    }
}