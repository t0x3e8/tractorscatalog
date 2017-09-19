using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.StringLibrary;
using Enceladus.Api;
using Enceladus.Api.UI;

namespace Enceladus
{
    partial class OverviewTractorPage : TractorBasePage
    {
        #region Fields and Properties
        protected TractorPresenter presenter;
        #endregion

        #region Constructors
        public OverviewTractorPage(TractorPresenter presenter)
        {
            InitializeComponent();

            this.presenter = presenter;
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            this.pnlMotor.Caption = ResourceReader.GetString("TractorOverview_MotorPanelCaption");
            this.rBauart.Label = ResourceReader.GetString("TractorOverview_BauartText");
            this.rKuehlung.Label = ResourceReader.GetString("TractorOverview_KuehlungText");
            this.rNenndrehzahl.Label = ResourceReader.GetString("TractorOverview_NenndrehzahlText");
            this.rZylinder.Label = ResourceReader.GetString("TractorOverview_ZylinderText");
            this.rHubraumccm.Label = ResourceReader.GetString("TractorOverview_HubraumccmText");
            this.rNennleistungkW.Label = ResourceReader.GetString("TractorOverview_rNennleistungkWText");
            this.rVerbrauchmaximalgkWh.Label = ResourceReader.GetString("TractorOverview_VerbrauchmaximalgkWhText");
            this.rNennleistungPS.Label = ResourceReader.GetString("TractorOverview_NennleistungPSText");
            this.rECEOderISO.Label = ResourceReader.GetString("TractorOverview_ECEOderISOText");
            this.rBestverbrauchgkWh.Label = ResourceReader.GetString("TractorOverview_BestverbrauchgkWhText");
            this.rKonstantleistungProzent.Label = ResourceReader.GetString("TractorOverview_KonstantleistungProzentText");

            this.pnlGears.Caption = ResourceReader.GetString("TractorOverview_GearsPanelCaption");
            this.rEndgeschwindigkeitkmh.Label = ResourceReader.GetString("TractorOverview_EndgeschwindigkeitkmhText");
            this.rGetriebeWunsch.Label = ResourceReader.GetString("TractorOverview_GetriebeWunschText");
            this.rGaengeVorwaerts.Label = ResourceReader.GetString("TractorOverview_GaengeVorwaertsText");
            this.rGetriebetyp.Label = ResourceReader.GetString("TractorOverview_GetriebetypText");

            this.pnlPowerTakeOff.Caption = ResourceReader.GetString("TractorOverview_PowerTakeOffPanelCaption");
            this.rZW_Drehzahlen.Label = ResourceReader.GetString("TractorOverview_ZW_DrehzahlenText");
            this.rZW_kW.Label = ResourceReader.GetString("TractorOverview_ZW_kWText");

            this.pnlHydraulic.Caption = ResourceReader.GetString("TractorOverview_HydraulicPanelCaption");
            this.rHubwerkKategorie.Label = ResourceReader.GetString("TractorOverview_HubwerkKategorieText");
            this.rHydraulikPumpenleistunglproMin.Label = ResourceReader.GetString("TractorOverview_HydraulikPumpenleistunglproMinText");
            this.rHubkraftMaximalDan.Label = ResourceReader.GetString("TractorOverview_HubkraftMaximalDanText");
            this.rHydraulikNenndruckBar.Label = ResourceReader.GetString("TractorOverview_HydraulikNenndruckBarText");
            
            this.pnlWeight.Caption = ResourceReader.GetString("TractorOverview_WeightPanelCaption");
            this.rGesamtgewicht.Label = ResourceReader.GetString("TractorOverview_GesamtgewichtText");
            this.rWendekreis.Label = ResourceReader.GetString("TractorOverview_WendekreisText");
            this.rBereifungVorne.Label = ResourceReader.GetString("TractorOverview_BereifungVorneText");
            this.rBereifungHinten.Label = ResourceReader.GetString("TractorOverview_BereifungHintenText");

            this.pnlPrice.Caption = ResourceReader.GetString("TractorOverview_PricePanelCaption");
            this.rPreisVonEuro.Label = ResourceReader.GetString("TractorOverview_PreisVonEuroText");
            this.rPreisBisEuro.Label = ResourceReader.GetString("TractorOverview_PreisBisEuroText");

            this.pnlDetails.Caption = ResourceReader.GetString("TractorOverview_DetailsPanelCaption");
            this.pnlEquipment.Caption = ResourceReader.GetString("TractorOverview_EquipmentPanelCaption");
            this.pnlTestReport.Caption = ResourceReader.GetString("TractorOverview_TestReportPanelCaption");
        }

        public override void BindTractor(Tractor tractor)
        {
            // this invoke must stay here, since some parent's controls new to be updated
            base.BindTractor(tractor);

            this.rBauart.Value = tractor.BauartMotor;
            this.rKuehlung.Value = tractor.Kuehlung;
            this.rNenndrehzahl.Value = tractor.NenndrehzahlUmin;
            this.rZylinder.Value = tractor.Zylinderzahl;
            this.rHubraumccm.Value = tractor.Hubraumccm;
            this.rNennleistungkW.Value = tractor.NennleistungkW;
            this.rVerbrauchmaximalgkWh.Value = tractor.VerbrauchmaximalgkWh;
            this.rNennleistungPS.Value = tractor.NennleistungPS;
            this.rECEOderISO.Value = tractor.ECEoderISO;
            this.rBestverbrauchgkWh.Value = tractor.BestverbrauchgkWh;
            this.rKonstantleistungProzent.Value = tractor.KonstantleistungProzent;

            this.rEndgeschwindigkeitkmh.Value = tractor.Endgeschwindigkeitkmh;
            this.rGetriebeWunsch.Value = tractor.GetriebeWunsch;
            this.rGaengeVorwaerts.Value = tractor.Gaengevorwaerts;
            this.rGetriebetyp.Value = tractor.Getriebetyp;

            this.rZW_Drehzahlen.Value = tractor.ZW_Drehzahlen;
            this.rZW_kW.Value = tractor.ZW_kW;

            this.rHubwerkKategorie.Value = tractor.HubwerkKategorie;
            this.rHydraulikPumpenleistunglproMin.Value = tractor.HydraulikPumpenleistunglproMin;
            this.rHubkraftMaximalDan.Value = tractor.HubkraftmaximaldaN;
            this.rHydraulikNenndruckBar.Value = tractor.HydraulikNenndruckbar;

            this.rGesamtgewicht.Value = tractor.Gesamtgewicht;
            this.rWendekreis.Value = tractor.Wendekreis;
            this.rBereifungVorne.Value = tractor.Bereifungvorne;
            this.rBereifungHinten.Value = tractor.Bereifunghinten;

            this.rPreisVonEuro.Value = tractor.PreisvonEuro;
            this.rPreisBisEuro.Value = tractor.PreisbisEuro;

            this.rBesonderes.Value = tractor.Besonderes;
            this.rAusstatung.Value = tractor.Ausstattung;
            this.rPruefberichte.Value = tractor.Pruefberichte;
        }
        #endregion

    }
}
