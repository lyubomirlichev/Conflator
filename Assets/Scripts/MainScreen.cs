using System;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Formula
{
    None,
    KanyeCats,
    OwenWowson,
    MovieSnacks,
    FruitySoundtracks
}

public class MainScreen : MonoBehaviour
{
    public Action<Formula> OnFormulaSelected;
    public Action OnGenerate;

    [SerializeField] private Transform navigation;
    [SerializeField] private Button generateBtn;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Renderer imageRenderer;
    [SerializeField] private CanvasGroup container;

    [SerializeField] private Shader decalShader;
    public void Init()
    {
        foreach (Transform tile in navigation)
        {
            var text = tile.GetComponentInChildren<TextMeshProUGUI>();
            tile.GetComponent<Toggle>().onValueChanged.AddListener((value) => { SelectFormula(value, text.text); });
        }

        generateBtn.onClick.AddListener(() => { OnGenerate?.Invoke(); });

        ToggleContainer(false);
        imageRenderer.gameObject.SetActive(false);
        
        //Testing out a specific shader
        Material mat = new Material(decalShader);
        imageRenderer.material = mat;
    }

    private void ToggleContainer(bool state)
    {
        container.alpha = state ? 1f : 0f;
        container.blocksRaycasts = state;
        container.interactable = state;
    }

    private void SelectFormula(bool state, string formulaTitle)
    {
        if (state)
        {
            Formula selection;
            
            if (formulaTitle == "Kanye cats")
            {
                selection = Formula.KanyeCats;
                OnFormulaSelected?.Invoke(selection);
            }

            if (formulaTitle == "Owen Wowson")
            {
                selection = Formula.OwenWowson;
                OnFormulaSelected?.Invoke(selection);
            }
        }
    }

    public void ApplyResults(string result)
    {
        ToggleContainer(true);
        
        label.SetText(result);
    }

    public void ApplyTexture(Texture2D texture2D)
    {
        ToggleContainer(true);
        imageRenderer.gameObject.SetActive(true);

        UpdateQuadTexture(texture2D);
    }

    private void UpdateQuadTexture(Texture2D texture2D)
    {
        imageRenderer.material.SetTexture("_DecalTexture",texture2D);
        SetQuadWidth(texture2D);
    }
    private void SetQuadWidth(Texture texture)
    {
        var bounds = CalculateRendererBounds(imageRenderer.gameObject);
        Vector2 meshSize = bounds.size;
        var ratio = GetNormalisedTiling(texture,meshSize);
        var offset = GetRatioOffset(ratio);

        var mat = imageRenderer.material;
            
        mat.SetVector("_DecalTiling",ratio);
        mat.SetVector("_DecalOffset",offset);
        
    }
    private Bounds CalculateRendererBounds(GameObject holder)
    {
        Quaternion currentRotation = holder.transform.rotation;
        holder.transform.rotation = Quaternion.identity;
        Bounds calculatedBounds = new Bounds(holder.transform.position, Vector3.zero);
        MeshRenderer[] meshRenderers = holder.GetComponentsInChildren<MeshRenderer>();
        if (meshRenderers != null && meshRenderers.Length > 0)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                calculatedBounds.Encapsulate(meshRenderers[i].bounds);
            }
        }
        Vector3 localCenter = calculatedBounds.center - holder.transform.position;
        calculatedBounds.center = localCenter;
        holder.transform.rotation = currentRotation;
        return calculatedBounds;
    }
    private Vector2 GetNormalisedTiling(Texture texture, Vector2 meshBounds)
    {
        var ratio = Vector2.zero;
        
        ratio.x = texture.width / meshBounds.x;
        ratio.y = texture.height / meshBounds.y;

        if (ratio.x < ratio.y)
        {
            ratio.y = ratio.y / ratio.x;
            ratio.x = ratio.x / ratio.x;
        }
        else
        {
            ratio.x = ratio.x / ratio.y;
            ratio.y = ratio.y / ratio.y;
        }

        var x = ratio.x;
        ratio.x = ratio.y;
        ratio.y = x;
        
        return ratio;
    }
    Vector2 GetRatioOffset(Vector2 ratio)
    {
        var offset = Vector2.zero;

        offset.x = (ratio.x-1) / 2;
        offset.y = (ratio.y-1) / 2;
        
        return -offset;
    }
}