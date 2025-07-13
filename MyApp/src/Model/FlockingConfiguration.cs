public class FlockingConfiguration
{
    public int BoidCount { get; set; }
    public int WorldWidth { get; set; }
    public int WorldHeight { get; set; }

    public float PerceptionDistance { get; set; }
    public float DesiredSeparation { get; set; }

    public float AlignmentWeight { get; set; }
    public float SeparationWeight { get; set; }
    public float CohesionWeight { get; set; }
}