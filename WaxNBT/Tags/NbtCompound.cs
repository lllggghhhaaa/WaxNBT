namespace WaxNBT.Tags;

public class NbtCompound : NbtTag
{
    public List<NbtTag> Children = new();

    public NbtCompound(string? name = null) => Name = name;

    public void Add(NbtTag tag) => Children.Add(tag);

    internal override void SerializeValue(ref NbtWriter writer)
    {
        foreach (NbtTag child in Children) child.Serialize(ref writer);
        writer.Write(NbtTagType.End);
    }

    public override NbtTagType GetType() => NbtTagType.Compound;
}