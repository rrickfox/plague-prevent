using static Constants;

public class Corona : IDisease
{
    public string diseaseName { get; } = "Covid-19";

    public float beta => (r0 * isolation * mt) / ti; //transmission rate
    public float r0 { get; set; } = 5.0f;                 //secondary infections, range 2-3
    public float isolation { get; set; } = 1f;            //degree to which people are isolated from the population
    public float mt { get; set; } = 1f;                   //time course of mitigation measures
    public float tl => 2f * timeScale;      //latency time from infection to infectiousness
    public float ti => 21f * timeScale;     //time an individual is infectious after which he/she recovers or falls severely ill
    public float th => 24f * timeScale;     //time a sick person recovers or deteriorates into a critical state
    public float tc => 28f * timeScale;     //time a person remains critical before dying or stabilizing
    public float m => 0.375f;               //fraction of infectious that are asymptomatic or mild
    public float c => 0.15f;                //fraction of severe cases that turn critical
    public float f => 0.1f;                 //fraction of critical cases that are fatal
}