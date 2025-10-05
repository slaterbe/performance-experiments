```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
Intel Core i7-5500U CPU 2.40GHz (Broadwell), 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.117
  [Host]     : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2
  Job-VNIRUV : .NET 8.0.17 (8.0.1725.26602), X64 RyuJIT AVX2

IterationCount=20  

```
| Method                       | Mean      | Error     | StdDev    | Allocated |
|----------------------------- |----------:|----------:|----------:|----------:|
| EculidanDistance             |  79.88 ms |  3.267 ms |  3.496 ms |         - |
| SquaredEculidanDistance      |  63.90 ms |  2.894 ms |  3.333 ms |         - |
| ManhattenDistancev1          |  79.49 ms | 12.029 ms | 13.853 ms |         - |
| ManhattenDistancev2          |  68.79 ms |  2.420 ms |  2.485 ms |         - |
| OctileDistance               | 184.73 ms |  0.859 ms |  0.882 ms |         - |
| FasterHypDistance            |  88.81 ms |  1.390 ms |  1.365 ms |         - |
| SquaredMultipleAddDistance   |  61.98 ms |  1.773 ms |  2.042 ms |         - |
| EuclideanDistance            |  26.93 ms |  1.572 ms |  1.747 ms |         - |
| SIMDEuclideanDistance        |  31.78 ms |  3.882 ms |  3.987 ms |      21 B |
| SIMDEuclideanSquaredDistance |  36.98 ms |  4.741 ms |  5.460 ms |         - |
| SumEuclideanAvx2             |  34.53 ms |  1.458 ms |  1.679 ms |         - |
