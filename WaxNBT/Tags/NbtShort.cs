namespace WaxNBT.Tags;

public class NbtShort : NbtTag
{
    public short Value;

    public NbtShort(short value) => Value = value;
    
    public NbtShort(string? name, short value)
    {
        Name = name;
        Value = value;
    }
    
    public static implicit operator NbtShort(short value) => new(value);
    public static implicit operator short(NbtShort tag) => tag.Value;

    public static NbtShort FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        short value = reader.ReadShort();

        return new NbtShort(name, value);
    }
    
    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Short;
}