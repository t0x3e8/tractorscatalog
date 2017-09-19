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
    partial class DetailsITractorPage : TractorBasePage
    {
        #region Fields
        protected TractorPresenter presenter;
        #endregion

        #region Constructors
        public DetailsITractorPage(TractorPresenter presenter)
        {
            this.InitializeComponent();
            this.presenter = presenter;
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {            
            this.pnlGears.Caption = ResourceReader.GetString("TractorDetailsI_GearsPanelCaption");
            this.rDrehzahlreduzierteMaxgeschwind.Label = ResourceReader.GetString("TractorDetailsI_DrehzahlreduzierteMaxgeschwindText");
            this.rGaengeUeber15Kmh.Label = ResourceReader.GetString("TractorDetailsI_GaengeUeber15KmhText");
            this.rGaenge4Bis12Kmh.Label = ResourceReader.GetString("TractorDetailsI_Gaenge4Bis12KmhText");
            this.rAutomatikfunktionenGetriebe.Label = ResourceReader.GetString("TractorDetailsI_AutomatikfunktionenGetriebeText");
            this.rKriechgetriebeab.Label = ResourceReader.GetString("TractorDetailsI_KriechgetriebeabText");
            this.rKriechgetriebe.Label = ResourceReader.GetString("TractorDetailsI_KriechgetriebeText");
            this.rWgVorwahlbar.Label = ResourceReader.GetString("TractorDetailsI_WgVorwahlbarText");
            this.rWgLastschaltbar.Label = ResourceReader.GetString("TractorDetailsI_WgLastschaltbarText");
            this.rWendeschaltung.Label = ResourceReader.GetString("TractorDetailsI_WendeschaltungText");
            this.rWendegetriebe.Label = ResourceReader.GetString("TractorDetailsI_WendegetriebeText");
            this.rStufenlosesCvt.Label = ResourceReader.GetString("TractorDetailsI_StufenlosesCvtText");
            this.rLSAnzahlStufen.Label = ResourceReader.GetString("TractorDetailsI_LSAnzahlStufenText");
            this.rLS_Getriebe.Label = ResourceReader.GetString("TractorDetailsI_LS_GetriebeText");
            this.rZahlDerSchalthebel.Label = ResourceReader.GetString("TractorDetailsI_ZahlDerSchalthebelText");
            this.rGetriebeWunsch.Label = ResourceReader.GetString("TractorDetailsI_GetriebeWunschText");
            this.rC60kmhundmehr.Label = ResourceReader.GetString("TractorDetailsI_C60kmhundmehrText");
            this.rC50kmh.Label = ResourceReader.GetString("TractorDetailsI_C50kmhText");
            this.rC40kmh.Label = ResourceReader.GetString("TractorDetailsI_C40kmhText");
            this.rC30kmh.Label = ResourceReader.GetString("TractorDetailsI_C30kmhText");
            this.rSynchronisation.Label = ResourceReader.GetString("TractorDetailsI_SynchronisationText");
            this.rEndgeschwindigkeitKmh.Label = ResourceReader.GetString("TractorDetailsI_EndgeschwindigkeitKmhText");
            this.rGaengeRueckwaerts.Label = ResourceReader.GetString("TractorDetailsI_GaengeRueckwaertsText");
            this.rGaengeVorwaerts.Label = ResourceReader.GetString("TractorDetailsI_GaengeVorwaertsText");
            this.rGetriebetyp.Label = ResourceReader.GetString("TractorDetailsI_GetriebetypText");
            this.rGetriebehersteller.Label = ResourceReader.GetString("TractorDetailsI_GetriebeherstellerText");

            this.pnlFuelConsumption.Caption = ResourceReader.GetString("TractorDetailsI_FuelConsumptionPanelCaption");
            this.rTankinhaltL.Label = ResourceReader.GetString("TractorDetailsI_TankinhaltLText");
            this.rAnzahlTanks.Label = ResourceReader.GetString("TractorDetailsI_AnzahlTanksText");
            this.rVerbrauchMaximalGkwh.Label = ResourceReader.GetString("TractorDetailsI_VerbrauchMaximalGkwhText");
            this.rBestverbrauchGkwh.Label = ResourceReader.GetString("TractorDetailsI_BestverbrauchGkwhText");
            this.rBestverbrauchBeiDrehzahl.Label = ResourceReader.GetString("TractorDetailsI_BestverbrauchBeiDrehzahlText");
            this.rMittlererOECD_VerbrauchgkWh.Label = ResourceReader.GetString("TractorDetailsI_MittlererOECD_VerbrauchgkWhText");
            this.rPowermixmittel.Label = ResourceReader.GetString("TractorDetailsI_rPowermixmittelText");
            this.rAdBlueTankinhalt.Label = ResourceReader.GetString("TractorDetailsI_AdBlueTankinhalt");
            
            this.pnlMotor.Caption = ResourceReader.GetString("TractorDetailsI_MotorPanelCaption");
            this.rHerstellerMotor.Label = ResourceReader.GetString("TractorDetailsI_HerstellerMotorText");
            this.rMotortyp.Label = ResourceReader.GetString("TractorDetailsI_MotortypText");
            this.rNennleistungKW.Label = ResourceReader.GetString("TractorDetailsI_NennleistungKWText");
            this.rBauartMotor.Label = ResourceReader.GetString("TractorDetailsI_BauartMotorText");
            this.rNenndrehzahlUmin.Label = ResourceReader.GetString("TractorDetailsI_NenndrehzahlUminText");
            this.rEceoderiso.Label = ResourceReader.GetString("TractorDetailsI_EceoderisoText");
            this.rMaxleistungBeiUmin.Label = ResourceReader.GetString("TractorDetailsI_MaxleistungBeiUminText");
            this.rMaximalleistungKW.Label = ResourceReader.GetString("TractorDetailsI_MaximalleistungKWText");
            this.rKuehlung.Label = ResourceReader.GetString("TractorDetailsI_KuehlungText");
            this.rHubraumCcm.Label = ResourceReader.GetString("TractorDetailsI_HubraumCcmText");
            this.rZylinderzahl.Label = ResourceReader.GetString("TractorDetailsI_ZylinderzahlText");
            this.rMdmaxNM.Label = ResourceReader.GetString("TractorDetailsI_MdmaxNMText");
            this.rMdmaxBeiDrehzahl.Label = ResourceReader.GetString("TractorDetailsI_MdmaxBeiDrehzahlText");
            this.rDrehmomentanstiegProzent.Label = ResourceReader.GetString("TractorDetailsI_DrehmomentanstiegProzentText");
            this.rPmaxAbUminUnten.Label = ResourceReader.GetString("TractorDetailsI_PmaxAbUminUntenText");
            this.rDrehzahlabfallProzent.Label = ResourceReader.GetString("TractorDetailsI_DrehzahlabfallProzentText");
            this.rAbgasnorm.Label = ResourceReader.GetString("TractorDetailsI_AbgasnormText");
            this.xBohrungXHub.Label = ResourceReader.GetString("TractorDetailsI_BohrungXHubText");
            this.rBoostleistungBeiUmin.Label = ResourceReader.GetString("TractorDetailsI_BoostleistungBeiUminText");
            this.rBoostleistungKW.Label = ResourceReader.GetString("TractorDetailsI_BoostleistungKWText");
            this.rUberleistungKW.Label = ResourceReader.GetString("TractorDetailsI_UberleistungKWText");
            this.rKonstantleistungProzent.Label = ResourceReader.GetString("TractorDetailsI_KonstantleistungProzentText");
            this.rSCRKatalysator.Label = ResourceReader.GetString("TractorDetailsI_SCRKatalysator");
            this.rDieseloxydationskatalysator.Label = ResourceReader.GetString("TractorDetailsI_Dieseloxydationskatalysator");
            this.rDieselpartikelfilter.Label = ResourceReader.GetString("TractorDetailsI_Dieselpartikelfilter");
        }


        public override void BindTractor(Tractor tractor)
        {
            // this invoke must stay here, since some parent's controls new to be updated
            base.BindTractor(tractor);

            this.rHerstellerMotor.Value = tractor.HerstellerMotor;
            this.rMotortyp.Value = tractor.MotorTyp;
            this.rNennleistungKW.Value = tractor.NennleistungkW;
            this.rBauartMotor.Value = tractor.BauartMotor;
            this.rNenndrehzahlUmin.Value = tractor.NenndrehzahlUmin;
            this.rEceoderiso.Value = tractor.ECEoderISO;
            this.rMaxleistungBeiUmin.Value = tractor.MaxleistungbeiUmin;
            this.rMaximalleistungKW.Value = tractor.MaximalleistungkW;
            this.rKuehlung.Value = tractor.Kuehlung;
            this.rHubraumCcm.Value = tractor.Hubraumccm;
            this.rZylinderzahl.Value = tractor.Zylinderzahl;
            this.rMdmaxNM.Value = tractor.MDmaxNm;
            this.rMdmaxBeiDrehzahl.Value = tractor.MDmaxbeiDrehzahl;
            this.rDrehmomentanstiegProzent.Value = tractor.DrehmomentanstiegProzent;
            this.rPmaxAbUminUnten.Value = tractor.PmaxabUminunten;
            this.rDrehzahlabfallProzent.Value = tractor.DrehzahlabfallProzent;
            this.xBohrungXHub.Value = tractor.BohrungxHub;
            this.rAbgasnorm.Value = tractor.Abgasnorm;
            this.rBoostleistungBeiUmin.Value = tractor.BoostleistungbeiUmin;
            this.rBoostleistungKW.Value = tractor.BoostleistungkW;
            this.rUberleistungKW.Value = tractor.UberleistungkW;
            this.rKonstantleistungProzent.Value = tractor.KonstantleistungProzent;
            this.rSCRKatalysator.Value = tractor.SCRKatalysator;
            this.rDieseloxydationskatalysator.Value = tractor.Dieseloxydationskatalysator;
            this.rDieselpartikelfilter.Value = tractor.Dieselpartikelfilter;

            this.rTankinhaltL.Value = tractor.Tankinhaltl;
            this.rAnzahlTanks.Value = tractor.AnzahlTanks;
            this.rVerbrauchMaximalGkwh.Value = tractor.VerbrauchmaximalgkWh;
            this.rBestverbrauchGkwh.Value = tractor.BestverbrauchgkWh;
            this.rBestverbrauchBeiDrehzahl.Value = tractor.BestverbrauchbeiDrehzahl;
            this.rMittlererOECD_VerbrauchgkWh.Value = tractor.MittlererOECD_VerbrauchgkWh;
            this.rPowermixmittel.Value = tractor.PowermixMittel;
            this.rAdBlueTankinhalt.Value = tractor.AdBlueTankinhaltL;

            this.rDrehzahlreduzierteMaxgeschwind.Value = tractor.DrehzahlreduzierteMaxGeschwindigkeit;
            this.rGaengeUeber15Kmh.Value = tractor.Gaengeueber15kmh;
            this.rGaenge4Bis12Kmh.Value = tractor.Gaenge4bis12kmh;
            this.rAutomatikfunktionenGetriebe.Value = tractor.AutomatikfunktionenGetriebe;
            this.rKriechgetriebeab.Value = tractor.Kriechgetriebeab;
            this.rKriechgetriebe.Value = tractor.Kriechgetriebe;
            this.rWgVorwahlbar.Value = tractor.WGVorwahlbar;
            this.rWgLastschaltbar.Value = tractor.WGLastschaltbar;
            this.rWendeschaltung.Value = tractor.Wendeschaltung;
            this.rWendegetriebe.Value = tractor.Wendegetriebe;
            this.rStufenlosesCvt.Value = tractor.StufenlosesCVT;
            this.rLSAnzahlStufen.Value = tractor.LSAnzahlStufen;
            this.rLS_Getriebe.Value = tractor.LS_Getriebe;
            this.rZahlDerSchalthebel.Value = tractor.ZahlderSchalthebel;
            this.rGetriebeWunsch.Value = tractor.GetriebeWunsch;
            this.rC60kmhundmehr.Value = tractor.C60kmhundmehr;
            this.rC50kmh.Value = tractor.C50kmh;
            this.rC40kmh.Value = tractor.C40kmh;
            this.rC30kmh.Value = tractor.C30kmh;
            this.rSynchronisation.Value = tractor.Synchronisation;
            this.rEndgeschwindigkeitKmh.Value = tractor.Endgeschwindigkeitkmh;
            this.rGaengeRueckwaerts.Value = tractor.Gaengerueckwaerts;
            this.rGaengeVorwaerts.Value = tractor.Gaengevorwaerts;
            this.rGetriebetyp.Value = tractor.Getriebetyp;
            this.rGetriebehersteller.Value = tractor.Getriebehersteller;
        }
        #endregion
    }
}
