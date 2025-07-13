```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
Intel Core i7-5500U CPU 2.40GHz (Broadwell), 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.117
  [Host]     : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2
  Job-VNIRUV : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2

IterationCount=20  

```
| Method                                    | Mean       | Error      | StdDev     | Gen0    | Gen1    | Gen2    | Allocated |
|------------------------------------------ |-----------:|-----------:|-----------:|--------:|--------:|--------:|----------:|
| AllocateArrayOfInts                       |   4.779 ms |  0.1919 ms |  0.2053 ms | 78.1250 | 78.1250 | 78.1250 | 8000076 B |
| AllocateListOfInts                        |   8.868 ms |  0.3087 ms |  0.3431 ms | 62.5000 | 62.5000 | 62.5000 | 8000097 B |
| IncrementArrayOfInts                      |   4.544 ms |  0.4262 ms |  0.4737 ms |       - |       - |       - |         - |
| IncrementArrayJumpingAround               | 133.102 ms | 29.6891 ms | 34.1900 ms |       - |       - |       - |         - |
| IncrementArrayJumpingAroundInconsistently |  60.844 ms |  6.0089 ms |  6.4295 ms |       - |       - |       - |         - |
| SIMDIncrementArray                        |   1.709 ms |  0.2394 ms |  0.2661 ms |       - |       - |       - |         - |
| SIMDIntIncrementArrayRandomVector         |   5.254 ms |  1.0588 ms |  1.1329 ms |       - |       - |       - |         - |
| SIMDFloatIncrementArrayRandomVector       |   4.298 ms |  0.4508 ms |  0.5010 ms |       - |       - |       - |      10 B |
