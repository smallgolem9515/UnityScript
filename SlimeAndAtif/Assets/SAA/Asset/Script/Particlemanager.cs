using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particlemanager : MonoBehaviour
{
    public static Particlemanager instance;

    Dictionary<string, GameObject> particleDic;

    public GameObject[] particlePrefebs;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        particleDic = new Dictionary<string, GameObject>();
        foreach(var pref in particlePrefebs)
        {
            particleDic.Add(pref.name, pref);
        }
    }

    public void PlayParticle(string particleName, Vector2 position,int layerOrder = 6)
    {
        if(!particleDic.ContainsKey(particleName))
        {
            Debug.Log($"파티클 '{particleName}'을 찾을 수 없습니다.");
            return;
        }

        GameObject particle = Instantiate(particleDic[particleName],position,Quaternion.identity);

        particle.name = particleName + "_Particle";

        SpriteRenderer spriteRenderer = particle.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = layerOrder;
        }

        Animator animator = particle.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("ParticleAnimation");
        }
        StartCoroutine(DestroyParticleAnimation(particle,animator));
    }
    IEnumerator DestroyParticleAnimation(GameObject particle,Animator animator)
    {
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(animationLength);
        yield return new WaitForSeconds(animationLength);

        Destroy(particle);
    }
}
