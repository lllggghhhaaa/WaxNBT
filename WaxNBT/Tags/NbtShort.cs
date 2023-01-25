namespace WaxNBT.Tags;

public class NbtShort : NbtTag
{
    public short Value;

    public NbtShort(short value = 0) => Value = value;

    public NbtShort(string? name = null, short value = 0)
    {
        Name = name;
        Value = value;
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Short;
}