using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.StringLibrary;

namespace Enceladus.UIToolbox
{
    public static class StringManager
    {
        public static string GetUnit(Units unit)
        {
            switch(unit)
            {                
                case Units.cm3 :
                    return "cm\u00B3";
                case Units.min_1:
                    return "min\u02C9\u00B9";
                case Units.kW:
                    return "kW";
                case Units.PS:
                    return "PS";
                case Units.gKWh:
                    return "g/KWh";
                case Units.Percent:
                    return "%";
                case Units.km_h:
                    return "km/h";
                case Units.l_min:
                    return "l/min";
                case Units.daN:
                    return "daN";
                case Units.bar:
                    return "bar";
                case Units.kg:
                    return "kg";
                case Units.mm:
                    return "mm";
                case Units.m:
                    return "m";
                case Units.cm:
                    return "cm";
                case Units.ccm:
                    return "ccm";
                case Units.euro:
                    return "Euro";
                case Units.m_h:
                    return "m/h";
                case Units.l:
                    return "l";
                case Units.l_h :
                    return "l/h";
                case Units.U_min:
                    return "U/min";
                case Units.Nm:
                    return "Nm";
                case Units.dB_A:
                    return "dB(A)";
                case Units.Hour:
                    return ResourceReader.GetString("Unit_Hour");
                case Units.NumberOfTanks:
                    return ResourceReader.GetString("Unit_NumberOfTanks");
                case Units.Backward:
                    return ResourceReader.GetString("Unit_Backward");
                case Units.from:
                    return ResourceReader.GetString("Unit_from");
                case Units.hours:
                    return ResourceReader.GetString("Unit_Hours");
                case Units.None:
                default:
                    return null;
            }
        }
    }
}
