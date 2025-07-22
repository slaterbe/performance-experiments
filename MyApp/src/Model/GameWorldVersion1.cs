public class GameWorldVersion1 : IGameWorld
{
    public List<Boid> Boids { get; set; } = new List<Boid>();

    public readonly float CohesionWeight;
    public readonly float AlignmentWeight;
    public readonly float SeparationWeight;

    public readonly float PerceptionDistance;
    public readonly float DesiredSeparation;

    private readonly float WorldHeight;
    private readonly float WorldWidth;

    public GameWorldVersion1(FlockingConfiguration config)
    {
        var random = new Random();
        CohesionWeight = config.CohesionWeight;
        AlignmentWeight = config.AlignmentWeight;
        SeparationWeight = config.SeparationWeight;

        PerceptionDistance = config.PerceptionDistance;
        DesiredSeparation = config.DesiredSeparation;

        WorldHeight = config.WorldHeight;
        WorldWidth = config.WorldWidth;

        for (int i = 0; i < config.BoidCount; i++)
        {

            Boids.Add(new Boid
            {
                PositionX = random.Next(config.WorldWidth),
                PositionY = random.Next(config.WorldHeight),
                VectorX = random.Next(10),
                VectorY = random.Next(10)
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

            if (Boids[i].PositionX > WorldWidth)
                Boids[i].PositionX -= WorldWidth;

            if (Boids[i].PositionX < 0)
                Boids[i].PositionX += WorldWidth;

            if (Boids[i].PositionY > WorldHeight)
                Boids[i].PositionY -= WorldHeight;

            if (Boids[i].PositionY < 0)
                Boids[i].PositionY += WorldHeight;            
        }
    }

    public void IncrementBoid(Boid primary)
    {
        // 1. Cohesion
        var cohesion = new float[] { 0, 0 };
        float count = 0;

        count = 0;
        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > PerceptionDistance) continue;

            var vectorFrom = primary.VectorTo(secondary)
                .GetInvertedLinearForce(distance);
            cohesion[0] += vectorFrom[0];
            cohesion[1] += vectorFrom[1];
            count++;
        }
        cohesion = cohesion.GetNormalisedVector();

        // 2. Alignment
        var alignment = new float[] { 0, 0 };
        count = 0;
        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            // if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > PerceptionDistance) continue;

            alignment[0] += Boids[i].VectorX;
            alignment[1] += Boids[i].VectorY;
            count++;
        }
        alignment = alignment.GetNormalisedVector();

        // 3. Separation
        var seperation = new float[] { 0, 0 };
        count = 0;
        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > DesiredSeparation) continue;

            var vectorFrom = primary.VectorTo(secondary)
                .GetInvertedLinearForce(distance);

            seperation[0] += vectorFrom[0];
            seperation[1] += vectorFrom[1];
            count++;
        }
        seperation = seperation
            .DivideByCount(count)
            .GetNormalisedVector();

        primary.VectorX = (cohesion[0] * CohesionWeight)
            + (alignment[0] * AlignmentWeight)
            + (seperation[0] * SeparationWeight);
        primary.VectorY = (cohesion[1] * CohesionWeight)
            + (alignment[1] * AlignmentWeight)
            + (seperation[1] * SeparationWeight);
    }

    public float[] GetBoidXPosition()
    {
        return this.Boids.Select(b => b.PositionX).ToArray();
    }

    public float[] GetBoidYPosition()
    {
        return this.Boids.Select(b => b.PositionY).ToArray();
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