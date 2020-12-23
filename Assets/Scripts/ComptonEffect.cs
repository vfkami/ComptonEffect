using System;
using UnityEngine;

//using System.Math;

public class ComptonEffect : MonoBehaviour
{
    private const double Planck = 6.62607e-34;
    private const double Luz = 2.997e8;
    private const double Eletron = 9.1095e-31;
    private const double Proton = 1.6762e-27;
    private const double Neutron = 1.6750e-27;
    private double Angulo;
    public GameObject c;
    private double CFinal;
    private double CInicial;
    private double Delta;
    private double EFinal;
    private double EInicial;

    private double Hmc;
    public GameObject manager;
    private double Massa;

    // Equation =>  lf - li = (h / m * c) * (1 - Math.cos(a))
    public void Calculate()
    {
        var values = c.GetComponent<UISimulator>().GetInputedValues();

        var ci = values[0];
        var cf = values[1];
        var d = values[2];
        var a = values[3];
        var m = values[4];
        var hmc = values[5];
        var ei = values[6];
        var ef = values[7];

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
        {
            Massa = Planck * hmc / Luz;
        }

        var rad = Math.PI / 180 * a;

        if (!double.IsNaN(a))
        {
            Angulo = a;

            var calcAngulo = 1 - Math.Cos(rad);
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
            Delta = Planck * Luz / ef - Planck * Luz / ei;
            d = Delta;
        }

        if (double.IsNaN(a))
        {
            var cos = Math.Round(d / hmc, 2, MidpointRounding.AwayFromZero);
            Angulo = 180 / Math.PI * Math.Acos(cos);
        }
        else
        {
            Angulo = a;
        }
        
        double newRad = (Math.PI / 180) * (Angulo / 2);
        double cotangent = 1 / Math.Tan(1 + (Planck / Math.Pow(Massa * Luz, 2)) * Math.Tan(newRad));
        double radPhi = Math.Pow(cotangent, -1);
        double degPhi = radPhi * (180 / Math.PI);
            
        manager.GetComponent<SceneManager>().StartAnimation(CInicial, CFinal, Convert.ToSingle(Angulo), Convert.ToSingle(degPhi));
        c.GetComponent<UISimulator>().SetValues(new[] {Angulo, CInicial, CFinal, Delta, Massa, Hmc, EInicial, EFinal, degPhi});
    }
}