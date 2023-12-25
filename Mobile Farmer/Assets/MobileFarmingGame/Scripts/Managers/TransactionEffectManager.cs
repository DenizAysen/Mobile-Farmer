using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionEffectManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private ParticleSystem coinPS;
    [SerializeField] private RectTransform coinRectTransform;

    [Header("Settings")]
    private int coinAmount;
    [SerializeField] private float moveSpeed;
    private Camera camera;
    public static TransactionEffectManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {       
        camera = Camera.main;
    }
    [NaughtyAttributes.Button]
    private void PlayCoinParticlesTest()
    {
        PlayCoinParticles(100);
    }
    public void PlayCoinParticles(int amount)
    {
        if (coinPS.isPlaying)
            return;

        ParticleSystem.Burst burst = coinPS.emission.GetBurst(0);
        burst.count = amount;
        coinPS.emission.SetBurst(0, burst);

        ParticleSystem.MainModule main = coinPS.main;
        main.gravityModifier = 2;

        coinPS.Play();
        coinAmount = amount;

        StartCoroutine(PlayCoinParticlesCoroutine());
    }

    IEnumerator PlayCoinParticlesCoroutine()
    {
        yield return new WaitForSeconds(1f);

        ParticleSystem.MainModule main = coinPS.main;
        main.gravityModifier = 0;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[coinAmount];

        Vector3 direction = (coinRectTransform.position - camera.transform.position).normalized;

        while (coinPS.isPlaying)
        {
            coinPS.GetParticles(particles);
            
            Vector3 targetPosition = camera.transform.position + direction * Vector3.Distance(camera.transform.position, coinPS.transform.position);

            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].remainingLifetime <= 0)
                    continue;

                particles[i].position = Vector3.MoveTowards(particles[i].position,targetPosition,moveSpeed * Time.deltaTime);

                if(Vector3.Distance(particles[i].position,targetPosition)< .1f)
                {
                    //particles[i].remainingLifetime = 0;
                    particles[i].position += Vector3.up * 10000;
                    CashManager.instance.AddCoins(1);
                }
            }
            coinPS.SetParticles(particles);

            yield return null;
        }

    }
}
