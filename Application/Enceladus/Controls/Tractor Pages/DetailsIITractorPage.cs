using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.StringLibrary;
using Enceladus.Api;

namespace Enceladus
{
    partial class DetailsIITractorPage : TractorBasePage
    {
        #region Fields
        protected TractorPresenter presenter;
        #endregion

        #region Constructors
        public DetailsIITractorPage(TractorPresenter presenter)
        {
            this.InitializeComponent();
            this.presenter = presenter;
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            this.pnlHydraulic.Caption = ResourceReader.GetString("TractorDetailsII_HyndraulicPanelCaption");
            this.rHydraulikPumpenleistungLPro.Label = ResourceReader.GetString("TractorDetailsII_HydraulikPumpenleistungLProText");
            this.rHydraulikNenndruckBar.Label = ResourceReader.GetString("TractorDetailsII_HydraulikNenndruckBarText");
            this.rAbreisskupplungen.Label = ResourceReader.GetString("TractorDetailsII_AbreisskupplungenText");
            this.rAnzahlSteuerventile.Label = ResourceReader.GetString("TractorDetailsII_AnzahlSteuerventileText");
            this.rArtHydrauliksystem.Label = ResourceReader.GetString("TractorDetailsII_ArtHydrauliksystemText");

            this.pnlHoist.Caption = ResourceReader.GetString("TractorDetailsII_HoistPanelCaption");
            this.rHubkraftMaximalDan.Label = ResourceReader.GetString("TractorDetailsII_HubkraftMaximalDanText");
            this.rFronthubwerkHubkraftdaN.Label = ResourceReader.GetString("TractorDetailsII_FronthubwerkHubkraftdaNText");
            this.rFronthubwerkUndZW.Label = ResourceReader.GetString("TractorDetailsII_FronthubwerkUndZWText");
            this.rSchlupfregelung.Label = ResourceReader.GetString("TractorDetailsII_SchlupfregelungText");
            this.rMischregelung.Label = ResourceReader.GetString("TractorDetailsII_MischregelungText");
            this.rZugwiderstandsregelung.Label = ResourceReader.GetString("TractorDetailsII_ZugwiderstandsregelungText");
            this.rSchwimmregelung.Label = ResourceReader.GetString("TractorDetailsII_SchwimmregelungText");
            this.rLageregelung.Label = ResourceReader.GetString("TractorDetailsII_LageregelungText");
            this.rUnterlenker_Regelung.Label = ResourceReader.GetString("TractorDetailsII_Unterlenker_RegelungText");
            this.rOberlenker_Regelung.Label = ResourceReader.GetString("TractorDetailsII_Oberlenker_RegelungText");
            this.lblRegulung.Text = ResourceReader.GetString("TractorDetailsII_RegulungText");
            this.rSchnellkuppler.Label = ResourceReader.GetString("TractorDetailsII_SchnellkupplerText");
            this.rFernbedienungimHeck.Label = ResourceReader.GetString("TractorDetailsII_FernbedienungimHeckText");
            this.rZusatz_Hubzylinder.Label = ResourceReader.GetString("TractorDetailsII_Zusatz_HubzylinderText");
            this.rHubwerkKategorie.Label = ResourceReader.GetString("TractorDetailsII_HubwerkKategorieText");
            this.rEhr.Label = ResourceReader.GetString("TractorDetailsII_EhrText");
            this.rHubkraftDurchgehendDan.Label = ResourceReader.GetString("TractorDetailsII_HubkraftDurchgehendDanText");

            this.pnlPowerTakeOff.Caption = ResourceReader.GetString("TractorDetailsII_PowerTakeOffPanelCaption");
            this.rZW_Drehzahlen.Label = ResourceReader.GetString("TractorDetailsII_ZW_DrehzahlenText");
            this.rZW_Uminmaximal.Label = ResourceReader.GetString("TractorDetailsII_ZW_UminmaximalText");
            this.rZW_Bauart.Label = ResourceReader.GetString("TractorDetailsII_ZW_BauartText");
            this.rZW_kW.Label = ResourceReader.GetString("TractorDetailsII_ZW_kWText");
            this.rZWFernbedienung.Label = ResourceReader.GetString("TractorDetailsII_ZWFernbedienungText");
            this.rWeg_ZW.Label = ResourceReader.GetString("TractorDetailsII_Weg_ZWText");
            this.rSpar_ZW.Label = ResourceReader.GetString("TractorDetailsII_Spar_ZWText");
            this.rZWlastschaltbar.Label = ResourceReader.GetString("TractorDetailsII_ZWlastschaltbarText");
            this.rZW_Profil.Label = ResourceReader.GetString("TractorDetailsII_ZW_ProfilText");
            this.rZW_Stummelzahl.Label = ResourceReader.GetString("TractorDetailsII_ZW_StummelzahlText");
            this.rFront_ZWUmin.Label = ResourceReader.GetString("TractorDetailsII_Front_ZWUminText");
        }

        public override void BindTractor(Tractor tractor)
        {
            // this invoke must stay here, since some parent's controls new to be updated
            base.BindTractor(tractor);

            this.rHydraulikPumpenleistungLPro.Value = tractor.HydraulikPumpenleistunglproMin;
            this.rHydraulikNenndruckBar.Value = tractor.HydraulikNenndruckbar;
            this.rAbreisskupplungen.Value = tractor.Abreisskupplungen;
            this.rAnzahlSteuerventile.Value = tractor.AnzahlSteuerventile;
            this.rArtHydrauliksystem.Value = tractor.ArtHydrauliksystem;

            this.rHubkraftMaximalDan.Value = tractor.HubkraftmaximaldaN;
            this.rFronthubwerkHubkraftdaN.Value = tractor.FronthubwerkHubkraftdaN;
            this.rFronthubwerkUndZW.Value = tractor.FronthubwerkundZW;
            this.rSchlupfregelung.Value = tractor.Schlupfregelung;
            this.rMischregelung.Value = tractor.Mischregelung;
            this.rZugwiderstandsregelung.Value = tractor.Zugwiderstandsregelung;
            this.rSchwimmregelung.Value = tractor.Schwimmregelung;
            this.rLageregelung.Value = tractor.Lageregelung;
            this.rUnterlenker_Regelung.Value = tractor.Unterlenker_Regelung;
            this.rOberlenker_Regelung.Value = tractor.Oberlenker_Regelung;
            this.rSchnellkuppler.Value = tractor.Schnellkuppler;
            this.rFernbedienungimHeck.Value = tractor.FernbedienungimHeck;
            this.rZusatz_Hubzylinder.Value = tractor.Zusatz_Hubzylinder;
            this.rHubwerkKategorie.Value = tractor.HubwerkKategorie;
            this.rEhr.Value = tractor.EHR;
            this.rHubkraftDurchgehendDan.Value = tractor.HubkraftdurchgehenddaN;

            this.rZW_Drehzahlen.Value = tractor.ZW_Drehzahlen;
            this.rZW_Uminmaximal.Value = tractor.ZW_Uminmaximal;
            this.rZW_Bauart.Value = tractor.ZW_Bauart;
            this.rZW_kW.Value = tractor.ZW_kW;
            this.rZWFernbedienung.Value = tractor.ZWFernbedienung;
            this.rWeg_ZW.Value = tractor.Weg_ZW;
            this.rSpar_ZW.Value = tractor.Spar_ZW;
            this.rZWlastschaltbar.Value = tractor.ZWlastschaltbar;
            this.rZW_Profil.Value = tractor.ZW_Profil;
            this.rZW_Stummelzahl.Value = tractor.ZW_Stummelzahl;
            this.rFront_ZWUmin.Value = tractor.Front_ZWUmin;
            this.rZW_Mot_Umin_1000.Value = tractor.ZW_Mot_Umin_1000;
            this.rZW_Mot_Umin_1000E.Value = tractor.ZW_Mot_Umin_1000E;
            this.rZW_Mot_Umin_540.Value = tractor.ZW_Mot_Umin_540;
            this.rZW_Mot_Umin_540E.Value = tractor.ZW_Mot_Umin_540E;
        }
        #endregion
    }
}
