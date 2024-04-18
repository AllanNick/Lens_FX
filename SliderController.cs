using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    //public Slider slider;
    public Button button;
    public PostProcessVolume volume;
    public LensDistortion lensDistortion;
    // Start is called before the first frame update
    void Start()
    {
        if (volume.profile.TryGetSettings(out lensDistortion))
        {
            button.onClick.AddListener(() => StartCoroutine(AnimateSliderValue()));
        }
    }
    IEnumerator AnimateSliderValue()
    {
        int repeatCount = 3;  // 抖动重复次数
        float duration = 0.7f; // 单次抖动持续时间

        for (int i = 0; i < repeatCount; i++)
        {
            float elapsed = 0.0f;
            //float amp_factor = (repeatCount - i)

            while (elapsed < duration)
            {
                float newIntensity = Mathf.Lerp(-40f, 40f, elapsed / duration);
                lensDistortion.intensity.value = newIntensity;
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0.0f; // 重置计时器

            while (elapsed < duration)
            {
                float newIntensity = Mathf.Lerp(40f, -40f, elapsed / duration);
                lensDistortion.intensity.value = newIntensity;
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        lensDistortion.intensity.value = 0; // 最终将Intensity值重置为0
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
