using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.Api
{
    [Serializable]
    public class TractorSearchResult : TractorBase
    {
        public string GetValue(int index)
        {
            string result = string.Empty;
            switch (index)
            {
                case 0:
                    result = this.Schlepperhersteller;
                    break;
                case 1:
                    result = this.Schleppertyp;
                    break;
                case 2:
                    result = this.LetzteAktualisierung; ;
                    break;
                case 3:
                    result = this.NennleistungkW;
                    break;
                case 4:
                    result = this.NennleistungPS;
                    break;
                case 5:
                    result = this.Gesamtgewicht;
                    break;
                case 6:
                    result = this.Nutzlast;
                    break;
                case 7:
                    result = this.Wendekreis;
                    break;
                case 8:
                    result = this.Hoehe;
                    break;
                case 9:
                    result = this.LS_Getriebe;
                    break;
                case 10:
                    result = this.Kriechgetriebe;
                    break;
                case 11:
                    result = this.FronthubwerkundZW;
                    break;
                case 12:
                    result = this.HubkraftmaximaldaN;
                    break;
                case 13:
                    result = this.PreisvonEuro;
                    break;
                default:
                    throw new ArgumentException("This index is not mapped");
            }

            return result;
        }
    }
}
