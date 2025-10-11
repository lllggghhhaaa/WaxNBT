using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using WaxNBT.Tags;

namespace WaxNBT;

public ref struct NbtReader
{
    public Encoding StringEncoder = Encoding.UTF8;

    private SequenceReader<byte> _reader;

    public NbtReader(ReadOnlySequence<byte> sequence, Encoding? encoding = null)
    {
        _reader = new SequenceReader<byte>(sequence);
        StringEncoder = encoding ?? Encoding.UTF8;
    }

    public NbtReader(ReadOnlySpan<byte> data, Encoding? encoding = null)
        : this(new ReadOnlySequence<byte>(data.ToArray()), encoding) { }

    public static NbtReader FromStream(Stream stream, Encoding? encoding = null)
    {
        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        var seq = new ReadOnlySequence<byte>(ms.GetBuffer(), 0, (int)ms.Length);
        return new NbtReader(seq, encoding);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Skip(int length) => _reader.Advance(length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public NbtTagType ReadTagType() => (NbtTagType)ReadByte();

    public NbtTag ReadTag(NbtTagType? type = null, bool readName = true)
    {
        type ??= ReadTagType();

        return type switch
        {
            NbtTagType.End => NbtEnd.FromReader(),
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
            null => NbtEnd.FromReader(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ReadByte()
    {
        _reader.TryRead(out byte value);
        return value;
    }

    public void ReadBytes(Span<byte> destination)
    {
        _reader.TryCopyTo(destination);
        _reader.Advance(destination.Length);
    }

    public byte[] ReadArray(int length)
    {
        var result = new byte[length];
        _reader.TryCopyTo(result);
        _reader.Advance(length);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public short ReadShort()
    {
        _reader.TryReadBigEndian(out short value);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int ReadInt()
    {
        _reader.TryReadBigEndian(out int value);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long ReadLong()
    {
        _reader.TryReadBigEndian(out long value);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ReadFloat()
    {
        _reader.TryReadBigEndian(out int intValue);
        return BitConverter.Int32BitsToSingle(intValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ReadDouble()
    {
        _reader.TryReadBigEndian(out long longValue);
        return BitConverter.Int64BitsToDouble(longValue);
    }

    public string ReadString()
    {
        var length = ReadShort();

        // Fast-path: decode directly from the current unread span if contiguous
        if (_reader.UnreadSpan.Length >= length)
        {
            var span = _reader.UnreadSpan.Slice(0, length);
            _reader.Advance(length);
            return StringEncoder.GetString(span);
        }

        // Fallback: copy to a small stack buffer or a temporary array, then decode
        Span<byte> temp = length <= 256 ? stackalloc byte[length] : new byte[length];
        _reader.TryCopyTo(temp);
        _reader.Advance(length);
        return StringEncoder.GetString(temp);
    }
}