﻿using UnityEngine;
using UnityEngine.UI;

public class SandController : MonoBehaviour
{
    public CustomRenderTexture Texture;
    public Material SandEffectMaterial;
    public Gradient SandColor;
    public enum GradType {Random, Smooth}
    public GradType GradientType = GradType.Smooth;
    public float radius = 1f;
    public RawImage img;
    public GradientUIController otherGrad;

    private int sandFallSpeed;
    private float gradientSmoothTime;
    private bool isGrad;
    private Color color;
    private Camera mainCamera;
    private Vector3 mousPos;
    private Color deleteColor; 

    private void Start()
    {
        sandFallSpeed = 1; ;
        gradientSmoothTime = 1f;
        Texture.Initialize();
        mainCamera = Camera.main;
        deleteColor = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        Texture.Update(sandFallSpeed);
        
        if (Input.GetMouseButton(0))
        {
            SandEffectMaterial.SetFloat("_radius", radius / 100);
            SandEffectMaterial.SetInt("_del", 0);
            mousPos = new Vector3 (Input.mousePosition.x * 4 / 3, Input.mousePosition.y, 0);
            SandEffectMaterial.SetVector("_DrawPosition", mainCamera.ScreenToViewportPoint(mousPos));

            if (isGrad)
            {
                if(SandColor != otherGrad.Gradient)
                {
                    SandColor = otherGrad.Gradient;
                }

                if (GradientType == GradType.Random)
                {
                    color = SandColor.Evaluate(Random.value);
                }
                else if (GradientType == GradType.Smooth)
                {
                    color = SandColor.Evaluate(Mathf.PingPong(Time.time * gradientSmoothTime, 1));
                }
            }
            else
            {
                if (color != img.color)
                {
                    color = img.color;
                }
            }

            SandEffectMaterial.SetVector("_DrawColor", color);
        }
        else if (Input.GetMouseButton(1))
        {
            SandEffectMaterial.SetFloat("_radius", radius / 100);
            SandEffectMaterial.SetInt("_del", 1);
            mousPos = new Vector3(Input.mousePosition.x * 4 / 3, Input.mousePosition.y, 0);
            SandEffectMaterial.SetVector("_DrawPosition", mainCamera.ScreenToViewportPoint(mousPos));
            SandEffectMaterial.SetVector("_DrawColor", deleteColor);
        }
        else 
        {
            SandEffectMaterial.SetVector("_DrawPosition", -Vector2.one);
        }
    }

    public void Scale(float value)
    {
        radius = value;
    }

    public void ChangeColor(Color color)
    {
        this.color = color;
    }

    public void ChangeisGrad(bool flag)
    {
        isGrad = flag;
    }

    public void ChangeGradSmoothTime(float value)
    {
        gradientSmoothTime = value;
    }

    public void ChangeFallSpeed(float value)
    {
        sandFallSpeed = Mathf.RoundToInt(value);
    }

    public void ChangeGradType(int value)
    {
        if(value == 0)
        {
            GradientType = GradType.Random;
        }
        else
        {
            GradientType = GradType.Smooth;
        }
    }
}
