using BenchmarkDotNet.Running;
using WaxNBT.Benchmarks;

// Run all benchmarks
BenchmarkRunner.Run<NbtReaderBenchmarks>();
BenchmarkRunner.Run<NbtWriterBenchmarks>();