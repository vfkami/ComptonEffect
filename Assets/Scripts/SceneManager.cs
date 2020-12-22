using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private GameObject _particle;
    private GameObject _particleParent;
    
    private Color _final;
    private Color _inicial;
    
    private float _angle = 120;
    private float _tempo = 2.5f;
    
    private bool _startButton;
    private bool _anotherTrigger;
    private bool _isEletron;

    public Canvas mainCanvas;
    
    public GameObject lightP;
    public GameObject sprLight;

    public Material protonColor;
    
    private void Start()
    {
        sprLight.GetComponent<SpriteRenderer>().color = Color.white;
        _particleParent = GameObject.Find("PParent");
        _particle = GameObject.Find("GO_Particle");
    }

    private void Update()
    {
        if (_startButton)
            lightP.transform.localPosition += new Vector3(0.05f, 0f, 0f);
        if (!_anotherTrigger) return;

        sprLight.transform.localPosition += new Vector3(0.05f, 0f, 0f);
        _particle.transform.localPosition += new Vector3(0.05f, 0f, 0f);
    }

    public void StartAnimation(double i, double f, float a)
    {
        _angle = a;
        SetColor(i, f);

        GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
        StartCoroutine(AnimationTime());
    }

    private IEnumerator AnimationTime()
    {
        // ----- Delay ----- //
        mainCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        // ----- Start Animation ----- //
        _startButton = true;
        sprLight.GetComponent<SpriteRenderer>().color = _inicial;
        yield return new WaitForSeconds(_tempo);
        _startButton = false;

        // ----- Shock ----- //
        lightP.transform.Rotate(Vector3.forward, _angle, Space.Self);
        _particleParent.transform.Rotate(Vector3.forward, -_angle, Space.Self);

        sprLight.GetComponent<SpriteRenderer>().color = _final;
        sprLight.transform.localScale = new Vector3(0.5f, 0.3f, 0.3f);
        _anotherTrigger = true;

        // ----- End Animation ----- //
        yield return new WaitForSeconds(2.0f);
        _anotherTrigger = false;
        mainCanvas.gameObject.SetActive(true);
    }

    public void SetColor(double i, double f)
    {
        _inicial = GetColorByLengthValue(i);
        _final = GetColorByLengthValue(f);
    }

    private Color GetColorByLengthValue(double value)
    {
        if (value <= 7.90e-12 && value >= 6.25e-12)
            return Color.red;

        if (value <= 6.24e-12 && value >= 5.90e-12)
            return new Color(1, 0.64f, 0);

        if (value <= 5.89e-12 && value >= 5.65e-12)
            return Color.yellow;

        if (value <= 5.64e-12 && value >= 5e-12)
            return Color.green;

        if (value <= 4.99e-12 && value >= 4.40e-12)
            return Color.blue;

        if ((value <= 4.39e-12) & (value >= 3.90e-12))
            return new Color(0.6f, 0.2f, 0.6f);

        return Color.white;
    }

    public void ModifyCollisionByParticleType()
    {
        var massSelector = GameObject.Find("DPD_Massa").GetComponent<Dropdown>();
        _particle = GameObject.Find("GO_Particle");

        switch (massSelector.value)
        {
            case 1: //proton
                protonColor.color = Color.white;
                break;

            case 2: //neutron
                protonColor.color = Color.black;
                break;

            case 3: // eletron
                _tempo = 1.5f;
                _isEletron = true;
                _particleParent = GameObject.Find("EParent");
                _particle = GameObject.Find("GO_Eletron");
                _particleParent.transform.position = new Vector3(-2.346f, 0.064f, -9.112f);
                return;
        }

        GameObject.Find("EParent").transform.position = new Vector3(3.16f, -0.197f, -9.84f); //default location

        _tempo = 2.6f;
        _isEletron = false;
        _particleParent = GameObject.Find("PParent");
        _particle = GameObject.Find("GO_Particle");
    }

    public void SetIsEletron()
    {
        var massSelector = GameObject.Find("DPD_Massa").GetComponent<Dropdown>();
        massSelector.value = 3;
        ModifyCollisionByParticleType();
    }


    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/MainScene", LoadSceneMode.Single);
    }
}