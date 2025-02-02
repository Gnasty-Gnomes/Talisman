using UnityEngine;

public class LeftHandState
{
    public Animator m_animator;
    public ParticleSystem m_particles;
    public virtual void StartState(float startValue) { }
    public virtual void Update() { }
    public virtual void StopState() { }
}