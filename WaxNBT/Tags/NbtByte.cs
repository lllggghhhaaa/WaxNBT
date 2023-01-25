namespace WaxNBT.Tags;

public class NbtByte : NbtTag
{
    public byte Value;

    public NbtByte(byte value = 0) => Value = value;

    public NbtByte(string? name = null, byte value = 0)
    {
        Name = name;
        Value = value;
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Byte;
}