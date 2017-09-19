using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.StringLibrary;
using Enceladus.UIToolbox;
using Enceladus.Api;

namespace Enceladus
{
    partial class ComparisonTractorPage : TractorBasePage
    {
        #region Fields
        protected TractorPresenter presenter;
        #endregion

        #region Constructors
        public ComparisonTractorPage(TractorPresenter presenter)
        {
            this.InitializeComponent();
            this.lbSelectedTractors.BackColor = Defines.ParsnipColor;
            this.lbSelectedTractors.Font = Defines.SmallBoldFont;
            this.lbSelectedTractors.ForeColor = Defines.CarrotColor;

            this.presenter = presenter;

            //this.inputBoxLabel1.Font = Defines.NormalItalicFont;
            //this.inputBoxLabel1.ForeColor = Defines.CabbageColor;
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            this.pnlGewichte.Caption = ResourceReader.GetString("TractorComparison_GewichtePanelCaption");
            this.rGesamtgewicht.Label = ResourceReader.GetString("TractorComparison_GesamtgewichtText");
            this.rNutzlast.Label = ResourceReader.GetString("TractorComparison_NutzlastText");
            this.rWendekreis.Label = ResourceReader.GetString("TractorComparison_WendekreisText");

            this.pnlHydraulik.Caption = ResourceReader.GetString("TractorComparison_HydraulikPanelCaption");
            this.rHydraulikNenndruckbar.Label = ResourceReader.GetString("TractorComparison_HydraulikNenndruckbarText");
            this.rHydraulikPumpenleistungLPro.Label = ResourceReader.GetString("TractorComparison_HydraulikPumpenleistungLProText");
            this.rHubkraftMaximalDan.Label = ResourceReader.GetString("TractorComparison_HubkraftMaximalDanText");
            this.rHubwerkKategorie.Label = ResourceReader.GetString("TractorComparison_HubwerkKategorieText");

            this.pnlZapfwelle.Caption = ResourceReader.GetString("TractorComparison_ZapfwellePanelCaption");
            this.rZW_kW.Label = ResourceReader.GetString("TractorComparison_ZW_kWText");
            this.rZW_Drehzahlen.Label = ResourceReader.GetString("TractorComparison_ZW_DrehzahlenText");

            this.pnlGetriebe.Caption = ResourceReader.GetString("TractorComparison_GetriebePanelCaption");
            this.rGetriebeWunsch.Label = ResourceReader.GetString("TractorComparison_GetriebeWunschText");
            this.rGaengevorwaerts.Label = ResourceReader.GetString("TractorComparison_GaengevorwaertsText");
            this.rGetriebetyp.Label = ResourceReader.GetString("TractorComparison_GetriebetypText");
            this.rEndgeschwindigkeitKmh.Label = ResourceReader.GetString("TractorComparison_EndgeschwindigkeitKmhText");

            this.pnlMotor.Caption = ResourceReader.GetString("TractorComparison_MotorPanelCaption");
            this.rBauartMotor.Label = ResourceReader.GetString("TractorComparison_BauartMotorText");
            this.rHubraumCcm.Label = ResourceReader.GetString("TractorComparison_HubraumCcmText");
            this.rZylinderZahl.Label = ResourceReader.GetString("TractorComparison_ZylinderZahlText");
            this.rNenndrehzahlUmin.Label = ResourceReader.GetString("TractorComparison_NenndrehzahlUminText");
            this.rKuehlung.Label = ResourceReader.GetString("TractorComparison_KuehlungText");
            this.rKonstantleistungProzent.Label = ResourceReader.GetString("TractorComparison_KonstantleistungProzentText");
            this.rNennleistungPS.Label = ResourceReader.GetString("TractorComparison_NennleistungPSText");
            this.rBestverbrauchGkwh.Label = ResourceReader.GetString("TractorComparison_BestverbrauchGkwhText");
            this.rVerbrauchMaximalGkwh.Label = ResourceReader.GetString("TractorComparison_VerbrauchMaximalGkwhText");
            this.rNennleistungKW.Label = ResourceReader.GetString("TractorComparison_NennleistungKWText");

            this.pnlOptions.Caption = ResourceReader.GetString("TractorComparison_MarkierteMaschinenText");

            this.simpleButton1.Text = ResourceReader.GetString("TractorComparison_RemoveSelectedButtonCaption");
            this.simpleButton2.Text = ResourceReader.GetString("TractorComparison_ViewTractorButtonCaption");
            this.simpleButton3.Text = ResourceReader.GetString("TractorComparison_CompareTractorsButtonCaption");
            this.simpleButton4.Text = ResourceReader.GetString("TractorComparison_CleanListButtonCaption");
        }

        public override void BindTractor(Tractor tractor)
        {
            // this invoke must stay here, since some parent's controls new to be updated
            base.BindTractor(tractor);

            this.rGesamtgewicht.Value = tractor.Gesamtgewicht;
            this.rNutzlast.Value = tractor.Nutzlast;
            this.rWendekreis.Value = tractor.Wendekreis;

            this.rHydraulikNenndruckbar.Value = tractor.HydraulikNenndruckbar;
            this.rHydraulikPumpenleistungLPro.Value = tractor.HydraulikPumpenleistunglproMin;
            this.rHubkraftMaximalDan.Value = tractor.HubkraftmaximaldaN;
            this.rHubwerkKategorie.Value = tractor.HubwerkKategorie;

            this.rZW_kW.Value = tractor.ZW_kW;
            this.rZW_Drehzahlen.Value = tractor.ZW_Drehzahlen;

            this.rGetriebeWunsch.Value = tractor.GetriebeWunsch;
            this.rGaengevorwaerts.Value = tractor.Gaengevorwaerts;
            this.rGetriebetyp.Value = tractor.Getriebetyp;
            this.rEndgeschwindigkeitKmh.Value = tractor.Endgeschwindigkeitkmh;

            this.rBauartMotor.Value = tractor.BauartMotor;
            this.rHubraumCcm.Value = tractor.Hubraumccm;
            this.rZylinderZahl.Value = tractor.Zylinderzahl;
            this.rNenndrehzahlUmin.Value = tractor.NenndrehzahlUmin;
            this.rKuehlung.Value = tractor.Kuehlung;
            this.rKonstantleistungProzent.Value = tractor.KonstantleistungProzent;
            this.rNennleistungPS.Value = tractor.NennleistungPS;
            this.rBestverbrauchGkwh.Value = tractor.BestverbrauchgkWh;
            this.rVerbrauchMaximalGkwh.Value = tractor.VerbrauchmaximalgkWh;
            this.rNennleistungKW.Value = tractor.NennleistungkW;
        }
        #endregion
    }       
}