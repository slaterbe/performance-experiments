```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
Intel Core i7-5500U CPU 2.40GHz (Broadwell), 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.117
  [Host]     : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2
  Job-VNIRUV : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2

IterationCount=20  

```
| Method                                    | Mean       | Error      | StdDev     | Gen0     | Gen1     | Gen2     | Allocated |
|------------------------------------------ |-----------:|-----------:|-----------:|---------:|---------:|---------:|----------:|
| AllocateArrayOfInts                       |   3.174 ms |  0.4137 ms |  0.4598 ms | 234.3750 | 234.3750 | 234.3750 | 8000176 B |
| AllocateListOfInts                        |   7.367 ms |  0.5000 ms |  0.4911 ms | 203.1250 | 203.1250 | 203.1250 | 8000198 B |
| IncrementArrayOfInts                      |   5.124 ms |  0.4140 ms |  0.4768 ms |        - |        - |        - |         - |
| IncrementArrayJumpingAround               | 134.987 ms | 15.2093 ms | 17.5151 ms |        - |        - |        - |     168 B |
| IncrementArrayJumpingAroundInconsistently | 122.326 ms |  6.0847 ms |  6.5105 ms |        - |        - |        - |         - |
| SIMDIncrementArray                        |   2.700 ms |  0.0908 ms |  0.1009 ms |        - |        - |        - |         - |
| SIMDIncrementArrayRandomVector            |   4.440 ms |  0.3525 ms |  0.4059 ms |        - |        - |        - |         - |
