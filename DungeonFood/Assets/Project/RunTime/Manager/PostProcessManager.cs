using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PostProcessManager : Singleton<PostProcessManager>
{
    private static PostProcessManager inst;

    [SerializeField] private VolumeProfile volume;
    [SerializeField] private DepthOfField depthOfField;

    [Range(60, 100)]
    public float blurIntensity = 100;

    private void Awake()
    {
        DontDestroySet();

        if (volume == null) volume = GetComponent<Volume>().profile;
        volume.TryGet(out depthOfField);
    }

    private void Start()
    {
        depthOfField.focalLength.value = 0;
    }

    // 씬 로드시 오브젝트 삭제 방지
    private void DontDestroySet()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator BlurLerp(float startNum, float endNum, float duration)
    {
        float delta = 0;

        while (delta <= duration)
        {
            float t = delta / duration;

            float currentValue = Mathf.Lerp(startNum, endNum, t);
            depthOfField.focalLength.value = currentValue;

            delta += Time.unscaledDeltaTime;

            yield return null;
        }

        StopCoroutine("BlurLerp");
    }
}
