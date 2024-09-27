using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    //파티클 프리팹을 이름으로 찾기 위한 딕셔너리
    private Dictionary<string, GameObject> particleDic;

    //여러 종류의 파티클 관리 배열
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

        //파티클 프리팹 배열을 딕셔너리에 추가해서 이름으로 찾을 수 있게 설정
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
            Debug.Log($"파티클'{particleName}'을 찾을 수 없습니다.");
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
