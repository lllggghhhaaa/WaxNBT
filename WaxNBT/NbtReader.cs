using System.Buffers.Binary;
using System.Text;

namespace WaxNBT;

public class NbtReader
{
    public Encoding StringEncoder = Encoding.UTF8;

    private readonly MemoryStream _stream;

    public NbtReader(Stream stream)
    {
        _stream = new MemoryStream();
        stream.CopyTo(_stream);
        _stream.Position = 0;
    }

    public NbtReader(byte[] data) => _stream = new MemoryStream(data);

    public NbtTagType ReadTagType() => (NbtTagType)ReadByte();

    public byte ReadByte() => (byte)_stream.ReadByte();

    public byte[] ReadArray(int lenght)
    {
        byte[] data = new byte[lenght];
        int read = _stream.Read(data);

        return data;
    }
    
    public short ReadShort()
    {
        byte[] data = new byte[2];
        int read = _stream.Read(data);
        return BinaryPrimitives.ReadInt16BigEndian(data);
    }

    public int ReadInt()
    {
        byte[] data = new byte[4];
        int read = _stream.Read(data);
        return BinaryPrimitives.ReadInt32BigEndian(data);
    }

    public long ReadLong()
    {
        byte[] data = new byte[8];
        int read = _stream.Read(data);
        return BinaryPrimitives.ReadInt64BigEndian(data);
    }
    
    public float ReadFloat()
    {
        byte[] data = new byte[4];
        int read = _stream.Read(data);
        
        Array.Reverse(data);
        return BitConverter.ToSingle(data);
    }
    
    public double ReadDouble()
    {
        byte[] data = new byte[8];
        int read = _stream.Read(data);
        
        Array.Reverse(data);
        return BitConverter.ToDouble(data);
    }

    public string ReadString()
    {
        short lenght = ReadShort();

        byte[] data = new byte[lenght];
        int read = _stream.Read(data);

        return StringEncoder.GetString(data);
    }
    
    public Stream GetStream() => _stream;
}