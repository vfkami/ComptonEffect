using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISimulator : MonoBehaviour
{
    // Canvas Values //
    private double _massa;
    public Dropdown dpdDegree;

    public Dropdown dpdFinalColor;
    // ========= Inputs ========= //

    public Dropdown dpdInitialColor;
    public Dropdown dpdMass;
    public InputField if_EnFinal;
    public InputField if_EnInicial;
    public InputField if_HMC;
    public InputField if_Lambdad;
    public InputField if_Lambdaf;

    public InputField if_Lambdai;
    public InputField if_MassaField;
    public GameObject lightGO;

    public Image pelicula;
    public Toggle tgFinalIv;
    public Toggle tgFinalUv;

    public Toggle tgInitialIv;
    public Toggle tgInitialUv;

    public GameObject window;

    private void Start()
    {
        pelicula.gameObject.SetActive(false);
        window.SetActive(false);
    }

    private void Update()
    {
        if (Screen.width == 1900 || Screen.height == 1000)
            GameObject.Find("Canvas").GetComponent<CanvasScaler>().scaleFactor = 1.5f;
        
        if(!pelicula.IsActive()) UpdateUiElementsInteractableStatus();
    }

    private void UpdateUiElementsInteractableStatus()
    {
        if_HMC.interactable = string.IsNullOrEmpty(if_MassaField.text);
        if_MassaField.interactable = string.IsNullOrEmpty(if_HMC.text);
        dpdMass.interactable = string.IsNullOrEmpty(if_HMC.text);

        bool interactable = false || string.IsNullOrEmpty(if_EnInicial.text) && string.IsNullOrEmpty(if_EnFinal.text) && String.IsNullOrEmpty(if_Lambdad.text);

        if_Lambdai.interactable = interactable;
        if_Lambdaf.interactable = interactable;
        tgFinalIv.interactable = interactable;
        tgFinalUv.interactable = interactable;
        tgInitialIv.interactable = interactable;
        tgInitialUv.interactable = interactable;
        dpdInitialColor.interactable = interactable;
        dpdFinalColor.interactable = interactable;

        interactable = false || string.IsNullOrEmpty(if_Lambdai.text) && string.IsNullOrEmpty(if_Lambdaf.text);
       
        if_EnInicial.interactable = interactable;
        if_EnFinal.interactable = interactable;
        if_Lambdad.interactable = interactable;
    }

    public void UpdateMassaFields()
    {
        if (dpdMass.value != 0)
        {
            switch (dpdMass.value)
            {
                case 1:
                    _massa = 1.6762e-27;
                    break;

                case 2:
                    _massa = 1.6750e-27;
                    break;

                case 3:
                    _massa = 9.1095e-31;
                    break;
            }

            if_MassaField.text = _massa.ToString(CultureInfo.InvariantCulture);
            if_MassaField.readOnly = true;
        }
        else
        {
            if_MassaField.readOnly = false;
        }
    }

    public void UpdateInputFieldsWithColorValues(int ifComp)
    {
        int index;
        InputField compInpField;

        if (ifComp == 0)
        {
            index = dpdInitialColor.value;
            compInpField = if_Lambdai;
        }
        else
        {
            index = dpdFinalColor.value;
            compInpField = if_Lambdaf;
        }

        switch (index)
        {
            case 0:
                compInpField.text = "";
                break;

            case 1:
                compInpField.text = "625e-14";
                break;

            case 2:
                compInpField.text = "590e-14";
                break;

            case 3:
                compInpField.text = "565e-14";
                break;

            case 4:
                compInpField.text = "500e-14";
                break;

            case 5:
                compInpField.text = "440e-14";
                break;

            case 6:
                compInpField.text = "390e-14";
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void UpdateInitialLenghtWithToggles()
    {
        dpdInitialColor.value = 0;

        if (tgInitialIv.isOn)
        {
            tgInitialUv.isOn = false;
            if_Lambdai.text = "1e-4";
        }
        else if (tgInitialUv.isOn)
        {
            tgInitialIv.isOn = false;
            if_Lambdai.text = "8,82e-9";
        }

        RefreshLengthFieldsInteractableStatus();
    }

    public void UpdateFinalLenghtWithToggles()
    {
        dpdFinalColor.value = 0;
        
        if (tgFinalIv.isOn)
        {
            tgFinalUv.isOn = false;
            if_Lambdaf.text = "1e-4";
        }
        else if (tgFinalUv.isOn)
        {
            tgFinalIv.isOn = false;
            if_Lambdaf.text = "8,82e-9";
        }

        RefreshLengthFieldsInteractableStatus();
    }

    private void RefreshLengthFieldsInteractableStatus()
    {
        var value = !(tgInitialIv.isOn || tgInitialUv.isOn || tgFinalIv.isOn || tgFinalUv.isOn);

        print(value);
        if_Lambdad.interactable = value;
        if_EnFinal.interactable = value;
        if_EnInicial.interactable = value;
    }


    public void UpdateHmcFields()
    {
        var isInteractable = string.IsNullOrEmpty(if_HMC.text);

        if_MassaField.interactable = isInteractable;
        dpdMass.interactable = isInteractable;

        if (!string.IsNullOrEmpty(if_MassaField.text) || dpdDegree.value != 0)
            if_HMC.interactable = false;
        else
            if_HMC.interactable = true;
    }

    public double[] GetInputedValues()
    {
        double degree = 0;
        double initialLength;
        double finalLength;
        double deltaLength;
        double particleMass;
        double hmc;
        double initialEnergy;
        double finalEnergy;

        switch (dpdDegree.value)
        {
            case 0:
                degree = double.NaN;
                break;

            case 1:
                degree = 30;
                break;

            case 2:
                degree = 45;
                break;

            case 3:
                degree = 60;
                break;
        }

        if (string.IsNullOrEmpty(if_Lambdai.text))
            initialLength = double.NaN;
        else
            double.TryParse(if_Lambdai.text, out initialLength);

        if (string.IsNullOrEmpty(if_Lambdaf.text))
            finalLength = double.NaN;
        else
            double.TryParse(if_Lambdaf.text, out finalLength);

        if (string.IsNullOrEmpty(if_Lambdad.text))
            deltaLength = double.NaN;
        else
            double.TryParse(if_Lambdad.text, out deltaLength);

        if (string.IsNullOrEmpty(if_MassaField.text))
            particleMass = double.NaN;
        else
            double.TryParse(if_MassaField.text, out particleMass);

        if (string.IsNullOrEmpty(if_HMC.text))
            hmc = double.NaN;
        else
            double.TryParse(if_HMC.text, out hmc);

        if (string.IsNullOrEmpty(if_EnInicial.text))
            initialEnergy = double.NaN;
        else
            double.TryParse(if_EnInicial.text, out initialEnergy);

        if (string.IsNullOrEmpty(if_EnFinal.text))
            finalEnergy = double.NaN;
        else
            double.TryParse(if_EnFinal.text, out finalEnergy);

        return new[] {initialLength, finalLength, deltaLength, degree, particleMass, hmc, initialEnergy, finalEnergy};
    }

    public void SetValues(double[] newValues)
    {
        pelicula.gameObject.SetActive(true);

        var ang = lightGO.transform.rotation.z;

        if_Lambdai.text = newValues[1].ToString("e2");
        if_Lambdaf.text = newValues[2].ToString("e2");
        if_Lambdad.text = newValues[3].ToString("e2");
        if_MassaField.text = newValues[4].ToString("e2");
        if_HMC.text = newValues[5].ToString("e2");
        if_EnInicial.text = newValues[6].ToString("e2");
        if_EnFinal.text = newValues[7].ToString("e2");

        if_Lambdai.interactable = true;
        if_Lambdaf.interactable = true;
        if_Lambdad.interactable = true;
        if_MassaField.interactable = true;
        if_HMC.interactable = true;
        if_EnInicial.interactable = true;
        if_EnFinal.interactable = true;
    }


    public void CloseInstructions()
    {
        window.SetActive(false);
    }

    public void OpenInstructions()
    {
        window.SetActive(true);
    }
}