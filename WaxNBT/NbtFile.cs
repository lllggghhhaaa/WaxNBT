using WaxNBT.Tags;

namespace WaxNBT;

public class NbtFile
{
    private NbtWriter _writer = new();
    public NbtCompound Root;

    public NbtFile(string rootName = "") => Root = new NbtCompound(rootName);

    public void Parse(Stream stream) => Parse(new NbtReader(stream));

    public void Parse(byte[] data) => Parse(new NbtReader(data));

    public void Parse(NbtReader reader)
    {
        throw new NotImplementedException();
    }

    public Stream Serialize()
    {
        Root.Serialize(ref _writer);
        
        Stream stream = _writer.GetStream();
        stream.Position = 0;

        return stream;
    }
}