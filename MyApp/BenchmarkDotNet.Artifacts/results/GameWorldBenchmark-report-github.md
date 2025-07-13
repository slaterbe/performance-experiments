```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
Intel Core i7-5500U CPU 2.40GHz (Broadwell), 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.117
  [Host]     : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2
  Job-VNIRUV : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2

IterationCount=20  

```
| Method            | Mean     | Error    | StdDev   | Gen0    | Allocated |
|------------------ |---------:|---------:|---------:|--------:|----------:|
| GameWorldVersion1 | 18.88 ms | 0.764 ms | 0.850 ms | 93.7500 |  248.5 KB |
