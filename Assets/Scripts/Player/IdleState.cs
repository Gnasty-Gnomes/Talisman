using UnityEngine;

public class IdleState : LeftHandState
{
    public IdleState(Animator anim, ParticleSystem ps)
    {
        m_animator = anim;
        m_particles = ps;
    }
    public override void StartState(float startValue)
    {
        m_particles.Stop();
    }
}