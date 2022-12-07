using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Renderer bgRenderer;

    private void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
