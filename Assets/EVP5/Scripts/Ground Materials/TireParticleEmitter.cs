
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class TireParticleEmitter : MonoBehaviour
	{
	public enum Mode { PressureAndSkid, PressureAndVelocity }

	public Mode mode = Mode.PressureAndSkid;

	public float emissionRate = 10.0f;
	[Range(0,1)]
	public float emissionShuffle = 0.0f;
	public float maxLifetime = 7.0f;
	public float minVelocity = 1.0f;
	public float maxVelocity = 15.0f;
	[Range(0,1)]
	public float tireVelocityRatio = 0.5f;

	public Color Color1 = Color.white;
	public Color Color2 = Color.gray;
	public bool randomColor = false;



	ParticleSystem m_particles;
	ParticleSystem.Particle m_particle;


	void OnEnable ()
		{
		m_particles = GetComponent<ParticleSystem>();
		m_particles.Stop();
		}


	public float EmitParticle (Vector3 position, Vector3 wheelVelocity, Vector3 tireVelocity, float pressureRatio, float intensityRatio, float lastParticleTime)
		{
		if (!isActiveAndEnabled) return -1.0f;

		// Ensure first particle is emitted on new sequence started
		if (lastParticleTime < 0.0f) lastParticleTime = Time.time - 1.0f/emissionRate;

		int particleCount = (int)((Time.time - lastParticleTime) * emissionRate);
		if (particleCount <= 0)
			return lastParticleTime;

		// Base lifetime of the particles depend on the mode

		float baseLifetime = 0.0f;

		switch (mode)
			{
			case Mode.PressureAndSkid:
				baseLifetime = pressureRatio * intensityRatio * maxLifetime;
				break;

			case Mode.PressureAndVelocity:
				float velocity = tireVelocity.magnitude + wheelVelocity.magnitude;
				baseLifetime = pressureRatio * maxLifetime * Mathf.InverseLerp(minVelocity, maxVelocity, velocity);
				break;
			}

		if (baseLifetime <= 0.0f)
			return -1.0f;

		for (int i = 0; i < particleCount; i++)
			{
			// The actual tire velocity (aka forward skip in 3D world) affects the
			// initial velocity of the particles

			m_particle.position = position;
			m_particle.velocity = wheelVelocity * 0.9f + tireVelocity * tireVelocityRatio;

			float lifetime = baseLifetime * Random.Range(0.6f, 1.4f);

			m_particle.lifetime = lifetime;
			m_particle.startLifetime = lifetime;

			m_particle.size = lifetime / maxLifetime * Random.Range(0.8f, 1.4f);
			m_particle.rotation = Random.Range(0.0f, 360.0f);
			m_particle.angularVelocity = 0.0f;

			if (randomColor)
				m_particle.color = Color.Lerp(Color1, Color2, Random.value);
			else
				m_particle.color = Color1;

			m_particle.randomSeed = (uint)Random.Range(0, 20000);
			m_particles.Emit(m_particle);
			}

		return Time.time + Random.Range(-emissionShuffle, +emissionShuffle) / emissionRate;
		}
	}
