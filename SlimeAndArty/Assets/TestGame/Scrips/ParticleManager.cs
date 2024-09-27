using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    //��ƼŬ �������� �̸����� ã�� ���� ��ųʸ�
    private Dictionary<string, GameObject> particleDic;

    //���� ������ ��ƼŬ ���� �迭
    public GameObject[] particlePrefabs;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //��ƼŬ ������ �迭�� ��ųʸ��� �߰��ؼ� �̸����� ã�� �� �ְ� ����
        particleDic = new Dictionary<string, GameObject>();
        foreach(var pref in particlePrefabs)
        {
            particleDic.Add(pref.name, pref);
        }
    }
    public void PlayParticle(string particleName,Vector2 position,int layerOrder = 6)
    {
        if(!particleDic.ContainsKey(particleName))
        {
            Debug.Log($"��ƼŬ'{particleName}'�� ã�� �� �����ϴ�.");
            return;
        }
        
        GameObject particle = Instantiate(particleDic[particleName],position,Quaternion.identity);

        particle.name = particleName + "_Particle";

        SpriteRenderer spriteRenderer = particle.GetComponent<SpriteRenderer>();
        if(spriteRenderer != null )
        {
            spriteRenderer.sortingOrder = layerOrder;
        }

        Animator animator = particle.GetComponent<Animator>();
        if(animator != null )
        {
            animator.Play("ParticleAnimation");
        }
        StartCoroutine(DestroyParticleAnimation(particle,animator));
    }
    IEnumerator DestroyParticleAnimation(GameObject particle,Animator animator)
    {
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);

        Destroy(particle);
    }
}
