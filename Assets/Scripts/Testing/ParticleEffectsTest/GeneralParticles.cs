using UnityEngine;

public class GeneralParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private void Start()
    {
        // print(_effect.)
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayEffect(_effect, transform.position);
        }
    }

    private void PlayEffect(ParticleSystem effect, Vector3 position)
    {
        Destroy(Instantiate(effect, position, effect.transform.rotation), effect.main.duration);
    }
}
