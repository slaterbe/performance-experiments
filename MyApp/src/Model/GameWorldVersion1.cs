public class GameWorldVersion1 : IGameWorld
{
    public List<Boid> Boids { get; set; } = new List<Boid>();

    public void Instantiate(int boidCount, int width, int height)
    {
        var random = new Random();

        for (int i = 0; i < boidCount; i++)
        {

            Boids.Add(new Boid
            {
                PositionX = random.Next(width),
                PositionY = random.Next(height),
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
        int xCohesion = 0;
        int yCohesion = 0;

        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > 100) continue;

            var vectorFrom = primary.VectorTo(secondary);
            xCohesion += vectorFrom[0];
            yCohesion += vectorFrom[0];
        }
        xCohesion /= Boids.Count - 1;
        yCohesion /= Boids.Count - 1;

        // 2. Alignment
        int xAlignment = 0;
        int yAlignment = 0;
        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > 100) continue;

            xAlignment += Boids[i].VectorX;
            yAlignment += Boids[i].VectorY;
        }
        xAlignment /= Boids.Count - 1;
        yAlignment /= Boids.Count - 1;

        // 3. Separation
        int xSeparation = 0;
        int ySeparation = 0;
        for (int i = 0; i < Boids.Count; i++)
        {
            var secondary = Boids[i];
            if (secondary == primary) continue;

            var distance = primary.Distance(secondary);
            if (distance > 20) continue;

            var vectorFrom = secondary.VectorTo(primary);

            xSeparation += vectorFrom[0];
            ySeparation += vectorFrom[1];
        }
        xSeparation /= Boids.Count - 1;
        ySeparation /= Boids.Count - 1;

        primary.VectorX = xCohesion + xAlignment + xSeparation;
        primary.VectorY = yCohesion + yAlignment + ySeparation;
    }
}

public class Boid
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int VectorX { get; set; }
    public int VectorY { get; set; }

    public int Distance(Boid boid)
    {
        var xDiff = boid.PositionX - PositionX;
        var yDiff = boid.PositionY - PositionY;

        return (int)Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
    }

    public int[] VectorTo(Boid boid)
    {
        var xDiff = PositionX - boid.PositionX;
        var yDiff = PositionY - boid.PositionY;

        return new int[] { xDiff, yDiff };
    }
}