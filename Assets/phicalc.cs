using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class phicalc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        const double Planck = 6.62e-34;
        const double Luz = 3e8; //aaa
        const double Eletron = 9.1095e-31;
        const double Proton = 1.6762e-27;
        const double Neutron = 1.6750e-27;

        double Massa = Eletron;
        double CInicial = 565e-9;
        const double Angulo = 30;
        
        
        double newRad = (Math.PI / 180) * (Angulo / 2);
        double fstep = (Massa * Luz * CInicial);
        double sstep = Planck / fstep;
        double tstep = (1 + sstep) * Math.Tan(newRad);

        double fistep = 1 / tstep;

        double radPhi = Math.Atan(fistep);
        double degPhi = radPhi * (180 / Math.PI);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}