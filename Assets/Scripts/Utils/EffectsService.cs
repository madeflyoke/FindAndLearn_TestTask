using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class EffectsService : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _starsVfx;
        private readonly Dictionary<string, Queue<ParticleSystem>> _pool = new Dictionary<string, Queue<ParticleSystem>>();
        private readonly List<ParticleSystem> _activeParticles = new List<ParticleSystem>();

        private void Awake()
        {
            _pool.Add(_starsVfx.name, new Queue<ParticleSystem>());
        }

        public void PlayStarsVfx(Vector3 position)
        {
            var effect = SpawnEffect(_starsVfx);
            effect.transform.position = position;
            effect.Play();
            _activeParticles.Add(effect);
        }

        private void Update()
        {
            for (int i = _activeParticles.Count - 1; i >= 0; i--)
            {
                var particle = _activeParticles[i];
                if (particle.isPlaying==false)
                {
                    particle.gameObject.SetActive(false);
                    _activeParticles.RemoveAt(i);
                    _pool[particle.name].Enqueue(particle);
                }
            }
        }

        private ParticleSystem SpawnEffect(ParticleSystem prefab)
        {
            var pool = _pool[prefab.name];
            
            if (pool.Count > 0)
            {
                var particle = pool.Dequeue();
                if (particle.isPlaying==false)
                {
                    particle.gameObject.SetActive(true);
                    return particle;
                }
            }
            
            var newParticle = Instantiate(prefab);
            newParticle.name = prefab.name;
            return newParticle;
        }
    }
}