namespace WaxNBT.Tags;

public class NbtEnd : NbtTag
{
    internal override void SerializeValue(ref NbtWriter writer) { }

    public static NbtEnd FromReader(NbtReader reader, bool readName) => new();
    
    public override NbtTagType GetType() => NbtTagType.End;
}