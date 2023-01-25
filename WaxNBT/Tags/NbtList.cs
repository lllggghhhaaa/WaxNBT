namespace WaxNBT.Tags;

public class NbtList<T> : NbtTag where T : NbtTag
{
    public List<T> Data = new();

    public NbtList(string? name = null) => Name = name;

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(Data[0].GetType());
        writer.Write(Data.Count);
        foreach (T tag in Data) tag.SerializeValue(ref writer);
    }

    public override NbtTagType GetType() => NbtTagType.List;
}