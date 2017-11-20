using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ParticleSystem), typeof(ParticleSystemRenderer))]
public class PixelateFX : MonoBehaviour, IRecyclable
{
    [SerializeField]
    private Sprite image;
    
    [SerializeField]
    private float estimatedPixelSize = 0.009f;

    [SerializeField]
    private float StartingVelocity = 3.0f;

    private ParticleSystem particleSystem;

    private List<Vector3> positions = new List<Vector3>();
    private List<Color32> colours = new List<Color32>();

    private bool exploded;

    private ParticleSystem.Particle[] particles;

    void Awake()
    {

        particleSystem = GetComponent<ParticleSystem>();

        var textureRect = image.textureRect;
        for (int y = (int) textureRect.min.y; y < textureRect.max.y; y += 4)
        {
            for (int x = (int) textureRect.min.x; x < textureRect.max.x; x += 4)
            {
                var pixel = image.texture.GetPixel(x, y);
                if (pixel.Equals(Color.black))
                {
                    continue;
                }

                positions.Add(new Vector3(
                    -(image.textureRect.width / 2 + image.textureRect.min.x - x) * estimatedPixelSize,
                    -(image.textureRect.height / 2 + image.textureRect.min.y - y) * estimatedPixelSize,
                    0
                ));
                
                colours.Add(new Color32(
                    (byte) (255 * pixel.r),
                    (byte) (255 * pixel.g),
                    (byte) (255 * pixel.b),
                    (byte) (255 * pixel.a))
                );
            }
        }
        
        particles = new ParticleSystem.Particle[positions.Count];
        particleSystem.collision.SetPlane(0, GameObject.Find("Ground").transform);
//        Debug.Log("Plane has " + positions.Count + " particles!");
    }

    void Update()
    {
        if (!exploded)
        {
            Explode();
        }
    }

    public void Explode()
    {
        exploded = true;
        
        particleSystem.Emit(positions.Count);
        particleSystem.GetParticles(particles);
        for (int i = 0; i < particles.Length; i++)
        {
            var particle = particles[i];
            particles[i].position = positions[i];
            particles[i].startColor = colours[i];
            particles[i].startSize = estimatedPixelSize * 4;
            particles[i].velocity = Random.insideUnitCircle.normalized * StartingVelocity;
            particles[i].remainingLifetime = 1.0f;
        }
        particleSystem.SetParticles(particles, particles.Length);
    }

    public void Recycle()
    {
        exploded = false;
    }
}