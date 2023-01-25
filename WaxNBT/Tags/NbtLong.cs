namespace WaxNBT.Tags;

public class NbtLong : NbtTag
{
    public long Value;

    public NbtLong(long value = 0) => Value = value;

    public NbtLong(string? name = null, long value = 0)
    {
        Name = name;
        Value = value;
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Long;
}