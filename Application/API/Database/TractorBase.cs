using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.Api
{
    [Serializable]
    public class TractorBase
    {
        public virtual string Satz { get; set; }
        public virtual string LetzteAktualisierung { get; set; }
        public virtual string Schlepperhersteller { get; set; }
        public virtual string Schleppertyp { get; set; }
        public virtual string NennleistungkW { get; set; }
        public virtual string NennleistungPS { get; set; }
        public virtual string Gesamtgewicht { get; set; }
        public virtual string Nutzlast { get; set; }
        public virtual string Wendekreis { get; set; }
        public virtual string Hoehe { get; set; }
        public virtual string LS_Getriebe { get; set; }
        public virtual string Kriechgetriebe { get; set; }
        public virtual string FronthubwerkundZW { get; set; }
        public virtual string HubkraftmaximaldaN { get; set; }
        public virtual string PreisvonEuro { get; set; }
        public virtual string Katalogteil { get; set; }
        public virtual string DisplayName { get { return string.Format("{0} {1}", this.Schlepperhersteller, this.Schleppertyp); } }
        public virtual int SatzAsInteger
        {
            get
            {
                int currentTractorIndex = 0;
                if (this.Satz != null && int.TryParse(this.Satz.ToString(), out currentTractorIndex))
                    return currentTractorIndex;
                else
                    return 0;
            }
        }

        public override string ToString()
        {
            return this.DisplayName;
        }

        public override bool Equals(object obj)
        {
            TractorBase tractor = obj as TractorBase;
            if (tractor == null) return false;
            else
                return this.SatzAsInteger.Equals(tractor.SatzAsInteger);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
