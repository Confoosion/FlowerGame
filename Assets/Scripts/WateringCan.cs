using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [SerializeField] private float _waterPerSecond = 2f;
    [SerializeField] private ParticleSystem waterParticles;

    public void ApplyWater()
    {
        if(!waterParticles.isPlaying)
            waterParticles.Play();
            // waterParticles.Emit(10);
    }

    public void StopParticles()
    {
        waterParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    } 
}
