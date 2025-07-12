public class GameWorldVersion1
{
    public List<Boid> Boids { get; set; }

    public void Instantiate(int size, int width, int height)
    {
        var random = new Random();

        for (int i = 0; i < size; i++)
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
        
    }
}

public class Boid
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int VectorX { get; set; }
    public int VectorY { get; set; }
}