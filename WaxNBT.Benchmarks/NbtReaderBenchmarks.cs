using System.Buffers;
using BenchmarkDotNet.Attributes;
using WaxNBT;

namespace WaxNBT.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 10)]
public class NbtReaderBenchmarks
{
    private byte[] _testData = null!;
    private const int Iterations = 100;
    
    [GlobalSetup]
    public void Setup()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);

        for (int i = 0; i < Iterations; i++)
        {
            writer.Write((byte)42);
            writer.Write((short)12345);
            writer.Write(987654321);
            writer.Write(123456789012345L);
            writer.Write(3.14159f);
            writer.Write(2.718281828459);
            writer.Write("Hello NBT World!");
        }

        _testData = buffer.WrittenSpan.ToArray();
    }

    [Benchmark(Baseline = true)]
    public void ReadPrimitives_Old()
    {
        var reader = new NbtReaderOld(_testData);
        
        for (int i = 0; i < 100; i++)
        {
            _ = reader.ReadByte();
            _ = reader.ReadShort();
            _ = reader.ReadInt();
            _ = reader.ReadLong();
            _ = reader.ReadFloat();
            _ = reader.ReadDouble();
            _ = reader.ReadString();
        }
    }

    [Benchmark]
    public void ReadPrimitives_New()
    {
        var reader = new NbtReader(_testData);
        
        for (int i = 0; i < 100; i++)
        {
            _ = reader.ReadByte();
            _ = reader.ReadShort();
            _ = reader.ReadInt();
            _ = reader.ReadLong();
            _ = reader.ReadFloat();
            _ = reader.ReadDouble();
            _ = reader.ReadString();
        }
    }

    [Benchmark]
    public void ReadShort_Old()
    {
        var reader = new NbtReaderOld(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1); // byte
            _ = reader.ReadShort();
            reader.Skip(4 + 8 + 4 + 8); // int, long, float, double
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadShort_New()
    {
        var reader = new NbtReader(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1); // byte
            _ = reader.ReadShort();
            reader.Skip(4 + 8 + 4 + 8); // int, long, float, double
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadInt_Old()
    {
        var reader = new NbtReaderOld(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2); // byte, short
            _ = reader.ReadInt();
            reader.Skip(8 + 4 + 8); // long, float, double
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadInt_New()
    {
        var reader = new NbtReader(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2); // byte, short
            _ = reader.ReadInt();
            reader.Skip(8 + 4 + 8); // long, float, double
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadLong_Old()
    {
        var reader = new NbtReaderOld(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2 + 4); // byte, short, int
            _ = reader.ReadLong();
            reader.Skip(4 + 8); // float, double
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadLong_New()
    {
        var reader = new NbtReader(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2 + 4); // byte, short, int
            _ = reader.ReadLong();
            reader.Skip(4 + 8); // float, double
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadFloat_Old()
    {
        var reader = new NbtReaderOld(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2 + 4 + 8); // byte, short, int, long
            _ = reader.ReadFloat();
            reader.Skip(8); // double
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadFloat_New()
    {
        var reader = new NbtReader(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2 + 4 + 8); // byte, short, int, long
            _ = reader.ReadFloat();
            reader.Skip(8); // double
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadDouble_Old()
    {
        var reader = new NbtReaderOld(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2 + 4 + 8 + 4); // byte, short, int, long, float
            _ = reader.ReadDouble();
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadDouble_New()
    {
        var reader = new NbtReader(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2 + 4 + 8 + 4); // byte, short, int, long, float
            _ = reader.ReadDouble();
            reader.Skip(2 + 16); // string length + content
        }
    }

    [Benchmark]
    public void ReadString_Old()
    {
        var reader = new NbtReaderOld(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2 + 4 + 8 + 4 + 8); // byte, short, int, long, float, double
            _ = reader.ReadString();
        }
    }

    [Benchmark]
    public void ReadString_New()
    {
        var reader = new NbtReader(_testData);
        for (int i = 0; i < 100; i++)
        {
            reader.Skip(1 + 2 + 4 + 8 + 4 + 8); // byte, short, int, long, float, double
            _ = reader.ReadString();
        }
    }
}
