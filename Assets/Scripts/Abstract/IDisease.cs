using System;

public interface IDisease
{
    string diseaseName { get; }
    float beta { get; }
    float r0 { get; set; }
    float isolation { get; set; }
    float mt { get; set; }
    float tl { get; }
    float ti { get; }
    float th { get; }
    float tc { get; }
    float m { get; }
    float c { get; }
    float f { get; }
}
