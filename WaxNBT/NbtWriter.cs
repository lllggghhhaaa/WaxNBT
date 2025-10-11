using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Text;

namespace WaxNBT;

public class NbtWriter(IBufferWriter<byte> writer)
{
    public Encoding StringEncoder = Encoding.UTF8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Span<byte> GetSpan(int sizeHint) => writer.GetSpan(sizeHint);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Advance(int count) => writer.Advance(count);

    public void Write(NbtTagType type) => Write((byte)type);

    public void Write(byte value)
    {
        var span = GetSpan(1);
        span[0] = value;
        Advance(1);
    }

    public void Write(ReadOnlySpan<byte> data)
    {
        var span = GetSpan(data.Length);
        data.CopyTo(span);
        Advance(data.Length);
    }

    public void Write(short value)
    {
        var span = GetSpan(2);
        BinaryPrimitives.WriteInt16BigEndian(span, value);
        Advance(2);
    }

    public void Write(int value)
    {
        var span = GetSpan(4);
        BinaryPrimitives.WriteInt32BigEndian(span, value);
        Advance(4);
    }

    public void Write(long value)
    {
        var span = GetSpan(8);
        BinaryPrimitives.WriteInt64BigEndian(span, value);
        Advance(8);
    }

    public void Write(float value)
    {
        var span = GetSpan(4);
        BinaryPrimitives.WriteInt32BigEndian(span, BitConverter.SingleToInt32Bits(value));
        Advance(4);
    }

    public void Write(double value)
    {
        var span = GetSpan(8);
        BinaryPrimitives.WriteInt64BigEndian(span, BitConverter.DoubleToInt64Bits(value));
        Advance(8);
    }

    public void Write(string value)
    {
        int charCount = value.Length;
        int maxByteCount = StringEncoder.GetMaxByteCount(charCount);

        var span = GetSpan(2 + maxByteCount);
        int bytesWritten = StringEncoder.GetBytes(value.AsSpan(), span.Slice(2));

        BinaryPrimitives.WriteInt16BigEndian(span, (short)bytesWritten);

        Advance(2 + bytesWritten);
    }
}