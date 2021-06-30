using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class UI_Generics
{
    public static IEnumerator CoroutineFadeImage(Image image, bool fadeIn, float fadeSpeed, bool useFakeDeltaTime = false) //Las corutinas no permiten ref o out :(
    {
        float delta = useFakeDeltaTime ? 0.01f * fadeSpeed : Time.deltaTime * fadeSpeed; //Evito el timescale si es necesario
        if (fadeIn)
        {
            while (image.color.a + delta < 1)
            {
                delta = useFakeDeltaTime ? 0.01f * fadeSpeed : Time.deltaTime * fadeSpeed;
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + delta);
                yield return null;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }
        else
        {
            while (image.color.a - delta > 0)
            {
                delta = useFakeDeltaTime ? 0.01f * fadeSpeed : Time.deltaTime * fadeSpeed;
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - delta);
                yield return null;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }

    //public static IEnumerator CoroutineFadeText(TextMeshProUGUI text, bool fadeIn, float fadeSpeed, bool useFakeDeltaTime = false) //Las corutinas no permiten ref o out :(
    //{
    //    float delta = useFakeDeltaTime ? 0.01f * fadeSpeed : Time.deltaTime * fadeSpeed; //Evito el timescale si es necesario
    //    if (fadeIn)
    //    {
    //        while (text.color.a + delta < 1)
    //        {
    //            delta = useFakeDeltaTime ? 0.01f * fadeSpeed : Time.deltaTime * fadeSpeed;
    //            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + delta);
    //            yield return null;
    //        }
    //        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    //    }
    //    else
    //    {
    //        while (text.color.a - Time.deltaTime * fadeSpeed > 0)
    //        {
    //            delta = useFakeDeltaTime ? 0.01f * fadeSpeed : Time.deltaTime * fadeSpeed;
    //            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - delta);
    //            yield return null;
    //        }
    //        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    //    }
    //}
}
