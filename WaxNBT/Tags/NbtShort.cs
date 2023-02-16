namespace WaxNBT.Tags;

public class NbtShort : NbtTag
{
    public short Value;

    public NbtShort(short value = 0, string? name = null)
    {
        Name = name;
        Value = value;
    }

    public static NbtShort FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        short value = reader.ReadShort();

        return new NbtShort(value, name);
    }
    
    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Short;
}