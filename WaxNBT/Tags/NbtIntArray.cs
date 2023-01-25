namespace WaxNBT.Tags;

public class NbtIntArray : NbtTag
{
    public int[] Data;

    public NbtIntArray(int[] data) => Data = data;

    public NbtIntArray(string name, int[] data)
    {
        Name = name;
        Data = data;
    }

    public NbtIntArray(int[] data, string? name = null)
    {
        Name = name;
        Data = data;
    }

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(Data.Length);
        foreach (int i in Data) writer.Write(i);
    }

    public override NbtTagType GetType() => NbtTagType.IntArray;
}