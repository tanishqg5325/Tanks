using UnityEngine;

public class TrainingTankMovement : MonoBehaviour
{
    public Color m_PlayerColor;
    public GameObject m_target;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_MovementAudio;
    public AudioSource m_ShootingAudio;
    public AudioClip m_EngineIdling;            
    public AudioClip m_EngineDriving;
    public AudioClip m_FireClip;
    public float m_PitchRange = 0.2f;

    private float m_radius = 20f;
    private float m_dist;
    private float m_time;
    private TankHealth m_health;
    private float m_OriginalPitch;

    private void Start()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
        m_time = Time.time;
        m_health = m_target.GetComponent<TankHealth>();
        m_OriginalPitch = m_MovementAudio.pitch;
    }

    private void Update()
    {
        EngineAudio();
    }

    private void FixedUpdate()
    {
        if (!m_health.m_Dead)
        {
            //transform.LookAt(m_target.GetComponent<Transform>());
            Vector3 relativeLocation = transform.position - m_target.transform.position;
            m_dist = relativeLocation.magnitude;
            //if (m_dist > 5f)
                //transform.position = Vector3.MoveTowards(transform.position, m_target.transform.position, m_dist/400);
            if (m_dist < m_radius && (Time.time - m_time) > 3f)
            {
                Fire();
                m_time = Time.time;
            }
        }
    }

    private void EngineAudio()
    {
        if (m_health.m_Dead)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }

    private void Fire()
    {
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_dist * 0.75f * m_FireTransform.forward;

        shellInstance.GetComponent<ShellExplosion>().m_owner = this.GetComponent<Rigidbody>();

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }
}