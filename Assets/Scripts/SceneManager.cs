using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject particle;
    public GameObject particleParent;

    public GameObject eletron;
    public GameObject eletronParent;

    private Dropdown _dpdMass;

    private GameObject _animateParticle;
    private GameObject _animParticleParent;

    private Color _final;
    private Color _inicial;
    
    private float _angle = 120;
    private float _partAngle;
    private float _tempo = 2f;
    
    private bool _startButton;
    private bool _anotherTrigger;
    //private bool _isEletron;

    public Canvas mainCanvas;
    
    public GameObject lightP;
    public GameObject sprLight;

    public Material protonColor;
    
    private void Start()
    {
        _dpdMass = GameObject.Find("DPD_Massa").GetComponent<Dropdown>();

        sprLight.GetComponent<SpriteRenderer>().color = Color.white;
        particleParent.SetActive(true);
        eletronParent.SetActive(false);

    }

    private void Update()
    {
        if (_startButton)
            lightP.transform.localPosition += new Vector3(0.05f, 0f, 0f);
        if (!_anotherTrigger) return;

        sprLight.transform.localPosition += new Vector3(0.05f, 0f, 0f);
        _animateParticle.transform.localPosition += new Vector3(0.05f, 0f, 0f);
        

    }
    public void StartAnimation(double i, double f, float a, float p)
    {
        _angle = a;
        if (_dpdMass.value == 3) _partAngle = p;
        else _partAngle = a;
        
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
        _animParticleParent.transform.Rotate(Vector3.forward, -_partAngle, Space.Self);

        sprLight.GetComponent<SpriteRenderer>().color = _final;
        sprLight.transform.localScale = new Vector3(0.5f, 0.2f, 0.2f);
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

        switch (massSelector.value)
        {
            case 1: //proton
                protonColor.color = Color.white;
                break;

            case 2: //neutron
                protonColor.color = Color.black;
                break;

            case 3: // eletron
                //_tempo = 1.5f;
                //_isEletron = true;
                _animateParticle = eletron;
                eletronParent.SetActive(true);
                particleParent.SetActive(false);

                _animParticleParent = eletronParent;
                _animParticleParent.transform.position = new Vector3(0.37f, -0.1f, -9.112f);
                return;
        }

        eletronParent.transform.position = new Vector3(0.37f, -0.1f, -9.112f); //default location

        //_tempo = 2.6f;
        particleParent.SetActive(true);
        eletronParent.SetActive(false);

        //_isEletron = false;
        _animParticleParent = particleParent;
        _animateParticle = particle;
    }

    public void SetIsEletron()
    {
        _dpdMass.value = 3;
        ModifyCollisionByParticleType();
    }

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/MainScene", LoadSceneMode.Single);
    }
}