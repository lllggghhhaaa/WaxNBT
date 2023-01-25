namespace WaxNBT.Tags;

public class NbtString : NbtTag
{
    public string Value;

    public NbtString(string value = "") => Value = value;

    public NbtString(string? name = null, string value = "")
    {
        Name = name;
        Value = value;
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.String;
}