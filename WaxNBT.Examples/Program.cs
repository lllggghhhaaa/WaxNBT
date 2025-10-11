using WaxNBT;
using WaxNBT.Tags;

var nbt = new NbtFile();

var numbers = new NbtCompound("numbers");
var texts = new NbtCompound("texts");
var arrays = new NbtCompound("arrays");

numbers
    .Add(new NbtByte("byte", 8))
    .Add(new NbtShort("short", 16))
    .Add(new NbtInt("int", 32))
    .Add(new NbtLong("long", 64))
    .Add(new NbtFloat("float", 32.1f))
    .Add(new NbtDouble("double", 64.2d));

texts
    .Add(new NbtString("first", "ceira"))
    .Add(new NbtString("second", "pura"))
    .Add(new NbtString("third", "sinas"));

arrays
    .Add(new NbtByteArray("bytes", "\tc"u8.ToArray()))
    .Add(new NbtIntArray("ints", new[] { 9, 99, 999, 9999 }))
    .Add(new NbtLongArray("longs", new long[] { 9, 99, 999, 9999, 99999, 999999, 9999999, 99999999 }));

var list = new NbtList
{
    new NbtString("pessoa"),
    new NbtString("de"),
    new NbtString("ceira"),
    new NbtString("cheia"),
    new NbtString("de"),
    new NbtString("ceira")
};

nbt.Root
    .Add(numbers)
    .Add(texts)
    .Add(arrays)
    .Add(list);

var stream = nbt.SerializeToStream();

var fs = File.Create("ceira.nbt");
stream.CopyTo(fs);

stream.Close();
fs.Close();

var readFile = NbtFile.Parse(File.ReadAllBytes("ceira.nbt"));

Console.WriteLine(readFile.Root.Name);
foreach (var child in readFile.Root.Children)
{
    Console.WriteLine(child.Name);
    Console.WriteLine(child.GetType());
}