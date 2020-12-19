using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;


public class UISimulator : MonoBehaviour
{
    /// <summary>
    /// Todas as informações de luz são referenciadas em nanometro (nm)
    /// </summary>

    // ========= Inputs ========= //

    public Dropdown dpd_colorIni;
    public Dropdown dpd_colorFin;
    public Dropdown dpd_massa;
    
    public Toggle tg_ivini;
    public Toggle tg_uvini;
    public Toggle tg_ivfin;
    public Toggle tg_uvfin;
    
    public InputField if_Lambdai;
    public InputField if_Lambdaf;
    public InputField if_Lambdad;
    public InputField if_AngField;
    public InputField if_MassaField;
    public InputField if_HMC;
    public InputField if_EnFinal;
    public InputField if_EnInicial;

    public Image pelicula;

    public GameObject window;
    public GameObject lightGO;
    
    private void Start()
    {
        pelicula.gameObject.SetActive(false);
        window.SetActive(false);
    }

    private void Update()
    {
        if (Screen.width == 1900 || Screen.height == 1000)
        {
            GameObject.Find("Canvas").GetComponent<CanvasScaler>().scaleFactor = 1.5f;
        }
    }

    // Canvas Values //
    private double _massa = 0;

    public void UpdateMassaFields()
    {
        if (dpd_massa.value != 0)
        {
            switch (dpd_massa.value)
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
            if_MassaField.text = _massa.ToString();
            if_MassaField.readOnly = true;
        }
        else
        {
            if_MassaField.readOnly = false;
        }
    }

    public void UpdateEnergyFields()
    {
        if (!String.IsNullOrEmpty(if_EnInicial.text) || !String.IsNullOrEmpty(if_EnFinal.text))
        {
            if_Lambdad.interactable = false;
            if_Lambdai.interactable = false;
            if_Lambdaf.interactable = false;
            dpd_colorIni.interactable = false;
            tg_ivini.interactable = false;
            tg_uvini.interactable = false;
            dpd_colorFin.interactable = false;
            tg_ivfin.interactable = false;
            tg_uvfin.interactable = false;
        }
        else
        {
            if_Lambdad.interactable = true;
            if_Lambdai.interactable = true;
            if_Lambdaf.interactable = true;
            dpd_colorIni.interactable = true;
            tg_ivini.interactable = true;
            tg_uvini.interactable = true;
            dpd_colorFin.interactable = true;
            tg_ivfin.interactable = true;
            tg_uvfin.interactable = true;
        }
    }

    public void UpdateInicialInField()
    {
        bool s = false;
        switch (dpd_colorIni.value)
        {
            case 0:
                if_Lambdai.text = "";
                s = true;
                break;
            
            case 1:
                if_Lambdai.text = "625e-14";
                break;

            case 2:
                if_Lambdai.text = "590e-14";
                break;

            case 3:
                if_Lambdai.text = "565e-14";
                break;

            case 4:
                if_Lambdai.text = "500e-14";
                
                break;

            case 5:
                if_Lambdai.text = "440e-14";
                break;

            case 6:
                if_Lambdai.text = "390e-14";
                break;
        }
        
        if_Lambdai.interactable = s;
        if_Lambdad.interactable = s;
        if_EnFinal.interactable = s;
        if_EnInicial.interactable = s;
        tg_ivini.interactable = s;
        tg_uvini.interactable = s;
    }
    
    public void UpdateFinalInField()
    {
        bool s = false;
        switch (dpd_colorFin.value)
        {
            case 0:
                if_Lambdaf.text = "";
                s = true;
                break;
            
            case 1:
                if_Lambdaf.text = "625e-14";
                break;

            case 2:
                if_Lambdaf.text = "590e-14";
                break;

            case 3:
                if_Lambdaf.text = "565e-14";
                break;

            case 4:
                if_Lambdaf.text = "500e-14";
                break;

            case 5:
                if_Lambdaf.text = "440e-14";
                break;

            case 6:
                if_Lambdaf.text = "390e-14";
                break;
        }   
        
        if_Lambdai.interactable = s;
        if_Lambdad.interactable = s;
        if_EnFinal.interactable = s;
        if_EnInicial.interactable = s;
        tg_ivini.interactable = s;
        tg_uvini.interactable = s;
    }
    
    public void UpdateIFs()
    {
        if(!String.IsNullOrEmpty(if_Lambdad.text))
        {
            if_Lambdai.interactable = false;
            if_Lambdaf.interactable = false;
            if_EnInicial.interactable = false;
            if_EnFinal.interactable = false;
            dpd_colorIni.interactable = false;
            tg_ivini.interactable = false;
            tg_uvini.interactable = false;
            dpd_colorFin.interactable = false;
            tg_ivfin.interactable = false;
            tg_uvfin.interactable = false;
            return;
        }
        
        if_Lambdai.interactable = true;
        if_Lambdaf.interactable = true;
        if_EnInicial.interactable = true;
        if_EnFinal.interactable = true;
        dpd_colorIni.interactable = true;
        tg_ivini.interactable = true;
        tg_uvini.interactable = true;
        dpd_colorFin.interactable = true;
        tg_ivfin.interactable = true;
        tg_uvfin.interactable = true;
        

        if (!String.IsNullOrEmpty(if_Lambdaf.text) || !String.IsNullOrEmpty(if_Lambdai.text))
        {
            if_Lambdad.interactable = false;
            if_EnInicial.interactable = false;
            if_EnFinal.interactable = false;
        }
        else
        {
            if_Lambdad.interactable = true;
            if_EnInicial.interactable = true;
            if_EnFinal.interactable = true;
        }
        
        if (!String.IsNullOrEmpty(if_Lambdai.text))
        {
            dpd_colorIni.interactable = false;
            if_Lambdad.interactable = false;
            tg_ivini.interactable = false;
            tg_uvini.interactable = false;
        }
        else
        {
            dpd_colorIni.interactable = true;
            tg_ivini.interactable = true;
            tg_uvini.interactable = true;
        }
        
        if(!String.IsNullOrEmpty(if_Lambdaf.text))
        {
            dpd_colorFin.interactable = false;
            if_Lambdad.interactable = false;
            tg_ivfin.interactable = false;
            tg_uvfin.interactable = false;
        }
        else
        {
            dpd_colorFin.interactable = true;
            tg_ivfin.interactable = true;
            tg_uvfin.interactable = true;
        }
    }
    
    public void UpdateInitialFieldsByToggles()
    {
        dpd_colorIni.value = 0;
        if (tg_ivini.isOn)
        {
            if_Lambdai.text = "1e-4";
            if_Lambdai.interactable = false;
            dpd_colorIni.interactable = false;
            if_EnInicial.interactable = false;
        }
        else if (tg_uvini.isOn)
        {
            if_Lambdai.text = "8,82e-9";
            if_Lambdai.interactable = false;
            dpd_colorIni.interactable = false;
            if_EnInicial.interactable = false;

        }

        if (tg_ivini.isOn || tg_uvini.isOn) return;
        tg_ivini.interactable = true;
        tg_uvini.interactable = true;
        if_Lambdai.interactable = true;
        dpd_colorIni.interactable = true;
        if_EnInicial.interactable = true;
        if_EnFinal.interactable = true;
    }

    public void UpdateFinalFieldsByToggles()
    {
        dpd_colorFin.value = 0;
        if (tg_ivfin.isOn)
        {
            if_Lambdaf.text = "1e-4";
            if_Lambdaf.interactable = false;
            dpd_colorFin.interactable = false;
            if_EnFinal.interactable = false;
        }
        else if (tg_uvfin.isOn)
        {
            if_Lambdaf.text = "8,82e-9";
            if_Lambdaf.interactable = false;
            dpd_colorFin.interactable = false;
            if_EnFinal.interactable = false;
        }

        if (tg_ivfin.isOn || tg_uvfin.isOn) return;
        tg_ivfin.interactable = true;
        tg_uvfin.interactable = true;
        if_Lambdaf.interactable = true;
        dpd_colorFin.interactable = true;
        if_EnFinal.interactable = true;
        if_EnInicial.interactable = true;


    }
    
    public void UpdateMFs()
    {
        if (!String.IsNullOrEmpty(if_HMC.text))
        {
            if_MassaField.interactable = false;
            dpd_massa.interactable = false;
        }
        else
        {
            if_MassaField.interactable = true;
            dpd_massa.interactable = true;
        }
        
        if (!String.IsNullOrEmpty(if_MassaField.text) || dpd_massa.value != 0)
            if_HMC.interactable = false;
        else
            if_HMC.interactable = true;
    }
    public double[] GetCanvasValues()
    {
        double angulo;
        double iniLambda;
        double finLambda;
        double dLambda;
        double _massa;
        double _hmc;
        double _enInicial;
        double _enFinal;

        if(String.IsNullOrEmpty(if_AngField.text))
            angulo = Double.NaN;
        else
            double.TryParse(if_AngField.text, out angulo);

        if (String.IsNullOrEmpty(if_Lambdai.text))
            iniLambda = Double.NaN;
        else
            double.TryParse(if_Lambdai.text, out iniLambda);
           
        if (String.IsNullOrEmpty(if_Lambdaf.text))
            finLambda = Double.NaN;
        else
            double.TryParse(if_Lambdaf.text, out finLambda);
        
        if (String.IsNullOrEmpty(if_Lambdad.text))
            dLambda = Double.NaN;
        else
            double.TryParse(if_Lambdad.text, out dLambda);
        
        if (String.IsNullOrEmpty(if_MassaField.text))
            _massa = Double.NaN;
        else
            double.TryParse(if_MassaField.text, out _massa);
        
        if (String.IsNullOrEmpty(if_HMC.text))
            _hmc = Double.NaN;
        else
            double.TryParse(if_HMC.text, out _hmc);
        
        if (String.IsNullOrEmpty(if_EnInicial.text))
            _enInicial = Double.NaN;
        else
            double.TryParse(if_EnInicial.text, out _enInicial);
        
        if (String.IsNullOrEmpty(if_EnFinal.text))
            _enFinal = Double.NaN;
        else
            double.TryParse(if_EnFinal.text, out _enFinal);
        
        return new [] {iniLambda, finLambda, dLambda, angulo, _massa, _hmc, _enInicial, _enFinal};
    }

    public void UpdateValues(double[] newValues)
    {
        pelicula.gameObject.SetActive(true);

        float ang = lightGO.transform.rotation.z;

        if_AngField.text = ang.ToString(CultureInfo.InvariantCulture);
        if_Lambdai.text = newValues[1].ToString("e2");
        if_Lambdaf.text = newValues[2].ToString("e2");
        if_Lambdad.text = newValues[3].ToString("e2");
        if_MassaField.text = newValues[4].ToString("e2");
        if_HMC.text = newValues[5].ToString("e2");
        if_EnInicial.text = newValues[6].ToString("e2");
        if_EnFinal.text = newValues[7].ToString("e2");

        if_AngField.interactable = true;
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
