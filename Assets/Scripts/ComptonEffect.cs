using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

//using System.Math;

public class ComptonEffect : MonoBehaviour
{
    public GameObject c;
    public GameObject manager;
    
    private const double Planck = 6.62607e-34;
    private const double Luz = 2.997e8;
    private const double Eletron = 9.1095e-31;
    private const double Proton = 1.6762e-27;
    private const double Neutron = 1.6750e-27;

    private double Hmc;
    private double Massa;
    private double Delta;
    private double CInicial;
    private double CFinal;
    private double EInicial;
    private double EFinal;
    private double Angulo;
    
    // Equation =>  lf - li = (h / m * c) * (1 - Math.cos(a))
    public void Calculate()
    {
        double[] values = c.GetComponent<UISimulator>().GetCanvasValues(); 
        
        double ci = values[0];
        double cf = values[1];
        double d = values[2];
        double a = values[3];
        double m = values[4];
        double hmc = values[5];
        double ei = values[6];
        double ef = values[7];
        
        if (double.IsNaN(hmc)) // se não tem massa, assume que é eletron e calcula hmc
        {
            if (double.IsNaN(m))
            {
                manager.GetComponent<SceneManager>().SetIsEletron();
                m = 9.1095e-31;
            }
            hmc = Planck / (m * Luz);
            
            Massa = m;
            Hmc = hmc;
        }
        else 
            Massa = Planck * hmc / Luz;
        

        if (!double.IsNaN(a))
        {
            Angulo = a;
            
            double rad = (Math.PI / 180) * a;
            double calcAngulo = 1 - Math.Cos(rad);
            Delta = hmc * calcAngulo;
            
            if (!double.IsNaN(ci) && double.IsNaN(cf))
                cf = Delta + ci;
            
        
            if (!double.IsNaN(cf) && double.IsNaN(ci))
                ci = (Delta - cf) * -1;
            
        }
        
        if (!double.IsNaN(ci) && !double.IsNaN(cf))
        {
            CInicial = ci;
            CFinal = cf;
            EInicial = Planck * Luz / ci;
            EFinal = Planck * Luz / cf;
            Delta = cf - ci;
            d = Delta;
        }
        
        if (!double.IsNaN(ei) && !double.IsNaN(ef))
        {
            EInicial = ei;
            EFinal = ef;
            
            CInicial = Planck * Luz / ei;
            CFinal = Planck * Luz / ef;
            Delta = (Planck * Luz / ef) - (Planck * Luz / ei);
            d = Delta;
        }

        if (double.IsNaN(a))
        {
            double cos = Math.Round(d / hmc, 2, MidpointRounding.AwayFromZero);
            Angulo = 180 / Math.PI * Math.Acos(cos);
        }
        else Angulo = a;

        // print("------ valores calculados ------");
        //print("_compInicial: " + CInicial);
        // print("_compFinal: " + CFinal);
        // print("_compDelta: " + Delta);
        // print("_angulo: " + Angulo);
        // print("_masssa: " + Massa);
        // print("_hmc: " + Hmc);
        // print("_enInicial: " + EInicial);
        // print("_enFinal: " + EFinal);
        
        manager.GetComponent<SceneManager>().StartAnimation(CInicial, CFinal, Convert.ToSingle(Angulo));
        c.GetComponent<UISimulator>().UpdateValues(new [] { Angulo, CInicial, CFinal, Delta, Massa, Hmc, EInicial, EFinal });
    }
}


