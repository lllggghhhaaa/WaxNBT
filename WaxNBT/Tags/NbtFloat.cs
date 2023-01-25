namespace WaxNBT.Tags;

public class NbtFloat : NbtTag
{
    public float Value;

    public NbtFloat(float value = 0) => Value = value;

    public NbtFloat(string? name = null, float value = 0)
    {
        Name = name;
        Value = value;
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Float;
}