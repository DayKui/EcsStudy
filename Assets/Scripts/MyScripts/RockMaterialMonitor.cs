using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RockMaterialMonitor : MonoBehaviour {

    [SerializeField] private int pictureWidth = 200;
    [SerializeField] private int pictureHeight = 200;
    [SerializeField] private float xOrg = 0f;
    [SerializeField] private float yOrg = 0f;
    //柏林噪声缩放值越大，柏林噪声计算越密集
    [SerializeField] private float scale = 20;

    private Texture2D noiseTex;

    private Color[] pix;
    private MeshRenderer meshRender;

    private void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        noiseTex = new Texture2D(pictureWidth,pictureHeight);
        pix = new Color[noiseTex.width*noiseTex.height];
        meshRender.material.mainTexture = noiseTex;
    }

    private void Update()
    {
        calcNoise();
    }

    //计算噪声
    private void calcNoise()
    {
        float y = 0f;
        while (y<noiseTex.height)
        {
            float x = 0;
            while (x<noiseTex.width)
            {
                //计算出x的采样值
                float xCoord = xOrg + x / noiseTex.width * scale;
                //y的采样值
                float yCoord = yOrg + y / noiseTex.height * scale;
                //用计算出的采样值计算柏林噪声
                float sample = Mathf.PerlinNoise(xCoord,yCoord);
                pix[Convert.ToInt32(y * noiseTex.width + x)] = new Color(sample,sample,sample);
                x++;
            }
            y++;
        }
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }
}
