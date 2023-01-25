namespace WaxNBT.Tags;

public class NbtLongArray : NbtTag
{
    public long[] Data;

    public NbtLongArray(long[] data) => Data = data;

    public NbtLongArray(string name, long[] data)
    {
        Name = name;
        Data = data;
    }

    public NbtLongArray(long[] data, string? name = null)
    {
        Name = name;
        Data = data;
    }

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(Data.Length);
        foreach (long i in Data) writer.Write(i);
    }

    public override NbtTagType GetType() => NbtTagType.LongArray;
}