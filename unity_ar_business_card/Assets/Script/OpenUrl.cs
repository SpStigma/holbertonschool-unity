using UnityEngine;

public class OpenURLOnClick : MonoBehaviour
{
    public string url;

    private void OnMouseDown()
    {
        Application.OpenURL(url);
    }
}
