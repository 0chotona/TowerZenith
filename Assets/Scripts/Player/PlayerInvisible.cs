using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisible : MonoBehaviour
{
    Renderer[] _renderers;
    bool _isInvisible = false;
    public bool _IsInvisible => _isInvisible;

    [SerializeField] Shader _invisible_Shader;
    [SerializeField] Shader _toonShader;
    [SerializeField] Transform _playerModel;
    Shader _standardShader;

    void Start()
    {
        _renderers = _playerModel.GetComponentsInChildren<Renderer>();
        _standardShader = Shader.Find("Standard");
    }

    public void Sneak(float durTime)
    {
        _isInvisible = true;
        StartCoroutine(CRT_ChangeShader(durTime));
    }
    void ChangeShader(Shader shader)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            if (_renderers[i] != null)
            {
                Material[] materials = _renderers[i].materials;
                if (materials.Length > 0)
                {
                    for (int j = 0; j < materials.Length; j++)
                        materials[j].shader = shader;
                }
            }
            
            
        }
    }

    IEnumerator CRT_ChangeShader(float durTime)
    {

        ChangeShader(_invisible_Shader);
        yield return new WaitForSeconds(durTime);
        ChangeShader(_standardShader);
        _isInvisible = false;

    }
}
