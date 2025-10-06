`dotnet publish -c Release -r linux-x64 /p:PublishAot=true`

__Execute Builds__

`./bin/Release/net8.0/linux-x64/MyApp`

__Quick Execution__

`dotnet run`


__Benchmark Commands__

`dotnet build -c Release -p:AllowUnsafeBlocks=true`

`dotnet run -c Release`


__Next things to do__

- Wrap units around ege of screen
- Balance algorithm

- Identify how many elements can run optimally
- Monitor Rendering loop time in OpenGL

Future:

- First Round of optimisation


Done: 
- Connect GameWorldVersion1 to Rendering Engine (14/07/2025)

__Development__
Flocking
    - Per Squad
        - Formation center
        - Morale System
    - Per Boid
        - Formation Position
    - Find Nearest Neighbours in range
        - Separation Force

__Laptop Performance__

| Method                       | Mean      | Error    | StdDev    | Allocated |
|----------------------------- |----------:|---------:|----------:|----------:|
| EculidanDistance             |  76.16 ms | 0.702 ms |  0.780 ms |         - |
| EculidanDistanceWithThread10 |  45.19 ms | 1.041 ms |  1.157 ms |    2478 B |
| EculidanDistanceWithThread20 |  45.13 ms | 0.728 ms |  0.779 ms |    3720 B |
| EculidanReciprocalDistance   |  68.46 ms | 1.368 ms |  1.520 ms |         - |
| SquaredEculidanDistance      |  56.29 ms | 0.959 ms |  1.026 ms |         - |
| ManhattenDistancev1          |  61.34 ms | 1.051 ms |  1.124 ms |         - |
| ManhattenDistancev2          |  69.38 ms | 3.097 ms |  3.041 ms |         - |
| OctileDistance               | 191.82 ms | 3.634 ms |  3.732 ms |         - |
| FasterHypDistance            | 104.41 ms | 9.954 ms | 11.463 ms |         - |
| SquaredMultipleAddDistance   |  71.97 ms | 2.998 ms |  3.332 ms |         - |
| SIMDEuclideanDistance        |  35.40 ms | 5.108 ms |  5.466 ms |         - |
| SIMDEuclideanSquaredDistance |  35.66 ms | 6.247 ms |  6.944 ms |         - |
| SumEuclideanAvx2             |  23.42 ms | 0.829 ms |  0.954 ms |         - |
