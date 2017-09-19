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
    partial class DetailsIIITractorPage : TractorBasePage
    {
        #region Fields
        protected TractorPresenter presenter;
        #endregion

        #region Constructors
        public DetailsIIITractorPage(TractorPresenter presenter)
        {
            this.InitializeComponent();
            this.presenter = presenter;
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            this.pnlKabine.Caption = ResourceReader.GetString("TractorDetailsIII_KabinePanelCaption");
            this.rKabine.Label = ResourceReader.GetString("TractorDetailsIII_KabineText");
            this.rLautstaerke.Label = ResourceReader.GetString("TractorDetailsIII_LautstaerkeText");
            this.rKabinenfederung.Label = ResourceReader.GetString("TractorDetailsIII_KabinenfederungText");
            this.rNiedrig_Kabine.Label = ResourceReader.GetString("TractorDetailsIII_Niedrig_KabineText");
            this.rRueckfahreinrchtg.Label = ResourceReader.GetString("TractorDetailsIII_RueckfahreinrchtgText");
            this.rAutoLenksystem.Label = ResourceReader.GetString("TractorDetailsIII_AutoLenksystemText");
            this.rZugpendel.Label = ResourceReader.GetString("TractorDetailsIII_ZugpendelText");
            this.rMotor_Getriebe_Management.Label = ResourceReader.GetString("TractorDetailsIII_Motor_Getriebe_ManagementText");
            this.rStuetzlastAHK.Label = ResourceReader.GetString("TractorDetailsIII_StuetzlastAHKText");
            this.rAhkSchnellverst.Label = ResourceReader.GetString("TractorDetailsIII_AhkSchnellverstText");
            this.rAutomatAHK.Label = ResourceReader.GetString("TractorDetailsIII_AutomatAHKText");
            this.rCAN_Bus.Label = ResourceReader.GetString("TractorDetailsIII_CAN_BusText");
            this.rISO_Bus.Label = ResourceReader.GetString("TractorDetailsIII_ISO_BusText");
            this.rKlima_Anlage.Label = ResourceReader.GetString("TractorDetailsIII_Klima_AnlageText");

            this.pnlWartung.Caption = ResourceReader.GetString("TractorDetailsIII_WartungPanelCaption");
            this.rOelwechselMotorStd.Label = ResourceReader.GetString("TractorDetailsIII_OelwechselMotorStdText");
            this.rOelExtern.Label = ResourceReader.GetString("TractorDetailsIII_OelExternText");
            this.rOelGetriebe.Label = ResourceReader.GetString("TractorDetailsIII_OelGetriebeText");
            this.rOelMotor.Label = ResourceReader.GetString("TractorDetailsIII_OelMotorText");
            this.rOelwechselGetrStd.Label = ResourceReader.GetString("TractorDetailsIII_OelwechselGetrStdText");
            this.rOelHydraulikSerie.Label = ResourceReader.GetString("TractorDetailsIII_OelHydraulikText");
            this.rOelHydraulikOption.Label = ResourceReader.GetString("TractorDetailsIII_OelHydraulikOptionText");
            this.rOelwechselHydrStd.Label = ResourceReader.GetString("TractorDetailsIII_OelwechselHydrStdText");

            this.pnlGewichte.Caption = ResourceReader.GetString("TractorDetailsIII_GewichtePanelCaption");
            this.rHoehe.Label = ResourceReader.GetString("TractorDetailsIII_HoeheText");
            this.rLeergewicht.Label = ResourceReader.GetString("TractorDetailsIII_LeergewichtText");
            this.rBreite.Label = ResourceReader.GetString("TractorDetailsIII_BreiteText");
            this.rNutzlast.Label = ResourceReader.GetString("TractorDetailsIII_NutzlastText");
            this.rLaenge.Label = ResourceReader.GetString("TractorDetailsIII_LaengeText");
            this.rZulGesamtgewMax.Label = ResourceReader.GetString("TractorDetailsIII_ZulGesamtgewMaxText");
            this.rGesamtgewicht.Label = ResourceReader.GetString("TractorDetailsIII_GesamtgewichtText");

            this.pnlAchsen.Caption = ResourceReader.GetString("TractorDetailsIII_AchsenPanelCaption");
            this.rSpurweiteHinten.Label = ResourceReader.GetString("TractorDetailsIII_SpurweiteHintenText");
            this.rSpurweiteVorne.Label = ResourceReader.GetString("TractorDetailsIII_SpurweiteVorneText");
            this.rBodenfreiheit.Label = ResourceReader.GetString("TractorDetailsIII_BodenfreiheitText");
            this.rRadstand.Label = ResourceReader.GetString("TractorDetailsIII_RadstandText");
            this.rWendekreis.Label = ResourceReader.GetString("TractorDetailsIII_WendekreisText");
            this.rLenkradVerstellbar.Label = ResourceReader.GetString("TractorDetailsIII_LenkradVerstellbarText");
            this.rHydrostatLenkung.Label = ResourceReader.GetString("TractorDetailsIII_HydrostatLenkungText");
            this.rLenkhilfe.Label = ResourceReader.GetString("TractorDetailsIII_LenkhilfeText");
            this.rGewichtsverteilung.Label = ResourceReader.GetString("TractorDetailsIII_GewichtsverteilungText");
            this.rAchslasthinten.Label = ResourceReader.GetString("TractorDetailsIII_AchslasthintenText");
            this.rAchsgewichtHinten.Label = ResourceReader.GetString("TractorDetailsIII_AchsgewichtHintenText");
            this.rAchslastVorne.Label = ResourceReader.GetString("TractorDetailsIII_AchslastVorneText");
            this.rAchsgewichtVorne.Label = ResourceReader.GetString("TractorDetailsIII_AchsgewichtVorneText");
            this.rVADiffsperre.Label = ResourceReader.GetString("TractorDetailsIII_VADiffsperreText");
            this.rVA_Federung.Label = ResourceReader.GetString("TractorDetailsIII_VA_FederungText");
            this.rDruckluftbremse.Label = ResourceReader.GetString("TractorDetailsIII_DruckluftbremseText");
            this.rVierradbremse.Label = ResourceReader.GetString("TractorDetailsIII_VierradbremseText");
            this.rAllradantrieb.Label = ResourceReader.GetString("TractorDetailsIII_AllradantriebText");
            this.rSchaltbarkeitHADiffsperre.Label = ResourceReader.GetString("TractorDetailsIII_SchaltbarkeitHADiffsperreText");
            this.rDifferentialsp.Label = ResourceReader.GetString("TractorDetailsIII_DifferentialspText");
            this.rBereifunghinten.Label = ResourceReader.GetString("TractorDetailsIII_BereifunghintenText");
            this.rBereifungVorne.Label = ResourceReader.GetString("TractorDetailsIII_BereifungVorneText");
        }

        public override void BindTractor(Tractor tractor)
        {
            // this invoke must stay here, since some parent's controls new to be updated
            base.BindTractor(tractor);

            this.rKabine.Value = tractor.Kabine;
            this.rLautstaerke.Value = tractor.Lautstaerke;
            this.rKabinenfederung.Value = tractor.Kabinenfederung;
            this.rNiedrig_Kabine.Value = tractor.Niedrig_Kabine;
            this.rRueckfahreinrchtg.Value = tractor.Rueckfahreinrchtg;
            this.rAutoLenksystem.Value = tractor.AutoLenksystem;
            this.rZugpendel.Value = tractor.Zugpendel;
            this.rMotor_Getriebe_Management.Value = tractor.Motor_Getriebe_Management;
            this.rStuetzlastAHK.Value = tractor.StuetzlastAHK;
            this.rAhkSchnellverst.Value = tractor.AHKschnellverst;
            this.rAutomatAHK.Value = tractor.AutomatAHK;
            this.rCAN_Bus.Value = tractor.CAN_Bus;
            this.rISO_Bus.Value = tractor.ISO_Bus;
            this.rKlima_Anlage.Value = tractor.Klima_Anlage;

            this.rOelwechselMotorStd.Value = tractor.OelwechselMotorStd;
            this.rOelExtern.Value = tractor.Oelextern;
            this.rOelHydraulikSerie.Value = tractor.OelHydraulikSerie;
            this.rOelHydraulikOption.Value = tractor.OelHydraulikOption;
            this.rOelwechselHydrStd.Value = tractor.OelwechselHydrStd;
            this.rOelGetriebe.Value = tractor.OelGetriebe;
            this.rOelMotor.Value = tractor.OelMotor;
            this.rOelwechselGetrStd.Value = tractor.OelwechselGetrStd;

            this.rHoehe.Value = tractor.Hoehe;
            this.rLeergewicht.Value = tractor.Leergewicht;
            this.rBreite.Value = tractor.Breite;
            this.rNutzlast.Value = tractor.Nutzlast;
            this.rLaenge.Value = tractor.Laenge;
            this.rZulGesamtgewMax.Value = tractor.ZulGesamtgewmax;
            this.rGesamtgewicht.Value = tractor.Gesamtgewicht;

            this.rSpurweiteHinten.Value = tractor.Spurweitehinten;
            this.rSpurweiteVorne.Value = tractor.Spurweitevorne;
            this.rBodenfreiheit.Value = tractor.Bodenfreiheit;
            this.rRadstand.Value = tractor.Radstand;
            this.rWendekreis.Value = tractor.Wendekreis;
            this.rLenkradVerstellbar.Value = tractor.Lenkradverstellbar;
            this.rHydrostatLenkung.Value = tractor.HydrostatLenkung;
            this.rLenkhilfe.Value = tractor.Lenkhilfe;
            this.rGewichtsverteilung.Value = tractor.Gewichtsverteilung;
            this.rAchslasthinten.Value = tractor.Achslasthinten;
            this.rAchsgewichtHinten.Value = tractor.Achslastvorne;
            this.rAchslastVorne.Value = tractor.Achslastvorne;
            this.rAchsgewichtVorne.Value = tractor.Achsgewichtvorne;
            this.rVADiffsperre.Value = tractor.VADiffsperre;
            this.rVA_Federung.Value = tractor.VA_Federung;
            this.rDruckluftbremse.Value = tractor.Druckluftbremse;
            this.rVierradbremse.Value = tractor.Vierradbremse;
            this.rAllradantrieb.Value = tractor.Allradantrieb;
            this.rSchaltbarkeitHADiffsperre.Value = tractor.SchaltbarkeitHADiffsperre;
            this.rDifferentialsp.Value = tractor.Differentialsp;
            this.rBereifunghinten.Value = tractor.Bereifunghinten;
            this.rBereifungVorne.Value = tractor.Bereifungvorne;
        }
        #endregion
    }
}
