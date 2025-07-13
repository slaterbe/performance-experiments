public class GameWorldVersion1 : IGameWorld
{
    public List<Boid> Boids { get; set; } = new List<Boid>();

    public readonly float CohesionWeight;
    public readonly float AlignmentWeight;
    public readonly float SeparationWeight;

    public readonly float PerceptionDistance;
    public readonly float DesiredSeparation;

    public GameWorldVersion1(FlockingConfiguration config)
    {
        var random = new Random();
        CohesionWeight = config.CohesionWeight;
        AlignmentWeight = config.AlignmentWeight;
        SeparationWeight = config.SeparationWeight;

        PerceptionDistance = config.PerceptionDistance;
        DesiredSeparation = config.DesiredSeparation;

        for (int i = 0; i < config.BoidCount; i++)
        {

            Boids.Add(new Boid
            {
                PositionX = random.Next(config.WorldWidth),
                PositionY = random.Next(config.WorldHeight),
                VectorX = 0,
                VectorY = 0
            });
        }
    }

    public void Increment()
    {
        for (int i = 0; i < Boids.Count(); i++)
        {
            IncrementBoid(Boids[i]);
        }

        for (int i = 0; i < Boids.Count(); i++)
        {
            Boids[i].PositionX += Boids[i].VectorX;
            Boids[i].PositionY += Boids[i].VectorY;
        }
    }

    public void IncrementBoid(Boid primary)
    {
        // 1. Cohesion
        float xCohesion = 0;
        float yCohesion = 0;

        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > PerceptionDistance) continue;

            var vectorFrom = primary.VectorTo(secondary)
                .GetNormalisedVector();
            xCohesion += vectorFrom[0];
            yCohesion += vectorFrom[0];
        }
        xCohesion /= Boids.Count - 1;
        yCohesion /= Boids.Count - 1;

        // 2. Alignment
        float xAlignment = 0;
        float yAlignment = 0;
        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > PerceptionDistance) continue;

            xAlignment += Boids[i].VectorX;
            yAlignment += Boids[i].VectorY;
        }
        xAlignment /= Boids.Count - 1;
        yAlignment /= Boids.Count - 1;

        // 3. Separation
        float xSeparation = 0;
        float ySeparation = 0;
        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > DesiredSeparation) continue;

            var vectorFrom = secondary.VectorTo(primary)
                .GetNormalisedVector();

            xSeparation += vectorFrom[0];
            ySeparation += vectorFrom[1];
        }
        xSeparation /= Boids.Count - 1;
        ySeparation /= Boids.Count - 1;

        primary.VectorX = (xCohesion * CohesionWeight)
            + (xAlignment * AlignmentWeight)
            + (xSeparation * SeparationWeight);
        primary.VectorY = (yCohesion * CohesionWeight)
            + (yAlignment * AlignmentWeight)
            + (ySeparation * SeparationWeight);
    }
}

public class Boid
{
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float VectorX { get; set; }
    public float VectorY { get; set; }

    public float Distance(Boid boid)
    {
        var xDiff = boid.PositionX - PositionX;
        var yDiff = boid.PositionY - PositionY;

        return (float)Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
    }

    public float[] VectorTo(Boid boid)
    {
        var xDiff = PositionX - boid.PositionX;
        var yDiff = PositionY - boid.PositionY;

        return new float[] { xDiff, yDiff };
    }
}