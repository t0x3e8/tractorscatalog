using System;
using System.Collections.Generic;
using Enceladus.Api;
using System.Xml;
using Enceladus.UIToolbox;
using System.IO;
using System.Xml.Xsl;
using System.Reflection;
using Enceladus.StringLibrary;
using System.Diagnostics;
using System.Threading;
using Enceladus.Properties;

namespace Enceladus
{
    class GenerateComparisonSheetCommand : CommandBase
    {
        public GenerateComparisonSheetCommand() { Logger.Instance.Log(LogType.Info, "GenerateComparosonSheetCommand initialized"); }

        public override void Execute<T>(T state)
        {
            Logger.Instance.Log(LogType.Info, "GenerateComparosonSheetCommand.Execute");

            try
            {
                IEnumerable<Tractor> tractors = this.BuildTractorsCollection(state as IEnumerable<TractorBase>);
                string xmlFilePath = this.GenerateXML(tractors);
                string outputFilePath = Path.Combine(GlobalSettings.HTTPDataDirectoryPath, Path.ChangeExtension(this.CombineFileName(), "html"));
                this.TransformToHTMLDocument(xmlFilePath, outputFilePath);
                this.CopyRequiredResources(Directory.GetParent(outputFilePath).FullName);
                this.OpenHTMLDocument(outputFilePath);
            }
            catch(Exception ex) 
            {
                Logger.Instance.Log(LogType.Error, "GenerateComparosonSheetCommand.Execute", ex.ToString());
            }
        }

        private void CopyRequiredResources(string outputDirectory)
        {
            Resources.collapse.Save(Path.Combine(outputDirectory, "collapse.png"));
            Resources.expand.Save(Path.Combine(outputDirectory, "expand.png"));
            File.WriteAllBytes(Path.Combine(outputDirectory, "jquery.js"), Resources.jquery_1_6_4_min);
        }

        protected string CombineFileName()
        {
            DateTime dt  = DateTime.Now;
            return string.Format("{0}{1}{2}{3}{4}{5}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        public override void Execute()
        {
            throw new NotSupportedException("This method is not supported");
        }

        /// <summary>
        /// Method maps instances of TractoreBase type to Tractor type.  
        /// </summary>
        /// <param name="tractors"></param>
        protected IEnumerable<Tractor> BuildTractorsCollection(IEnumerable<TractorBase> tractors)
        {
            IDatabaseStorage dbStorage = new DatabaseStorage();
            IList<Tractor> completeTractors = new List<Tractor>();

            foreach (TractorBase tractor in tractors)
            {
                Tractor completeTractor = tractor as Tractor;
                if (completeTractor == null)
                    completeTractor = dbStorage.Get(int.Parse(tractor.Satz));

                completeTractors.Add(completeTractor);
            }

            return completeTractors;
        }

        protected string  GenerateXML(IEnumerable<Tractor> tractors)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null));

            XmlElement rootElement = xmlDocument.CreateElement("CompareInformation");
            xmlDocument.AppendChild(rootElement);

            this.CreatePanelElements(ref rootElement, xmlDocument);

            foreach (var tractor in tractors)
            {
                rootElement.AppendChild(this.CreateTractorElement(tractor, xmlDocument));
            }

            string tempXmlfile = Path.GetTempFileName();
            xmlDocument.Save(tempXmlfile);
            return tempXmlfile;
        }

        protected void CreatePanelElements(ref XmlElement rootElement, XmlDocument xmlDocument)
        {
            XmlElement katalogjahrPanelElement = xmlDocument.CreateElement("KatalogjahrPanelTitle");
            katalogjahrPanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_KatalogjahrPanelTitle"));
            rootElement.AppendChild(katalogjahrPanelElement);

            XmlElement motorPanelElement = xmlDocument.CreateElement("MotorPanelTitle");
            motorPanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_MotorPanelTitle"));
            rootElement.AppendChild(motorPanelElement);

            XmlElement getriebePanelElement = xmlDocument.CreateElement("GetriebePanelTitle");
            getriebePanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_GetriebePanelTitle"));
            rootElement.AppendChild(getriebePanelElement);

            XmlElement zapfwellePanelElement = xmlDocument.CreateElement("ZapfwellePanelTitle");
            zapfwellePanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZapfwellePanelTitle"));
            rootElement.AppendChild(zapfwellePanelElement);

            XmlElement hubwerkPanelElement = xmlDocument.CreateElement("HubwerkPanelTitle");
            hubwerkPanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_HubwerkPanelTitle"));
            rootElement.AppendChild(hubwerkPanelElement);

            XmlElement hydraulikPanelElement = xmlDocument.CreateElement("HydraulikPanelTitle");
            hydraulikPanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_HydraulikPanelTitle"));
            rootElement.AppendChild(hydraulikPanelElement);

            XmlElement achsenPanelElement = xmlDocument.CreateElement("AchsenPanelTitle");
            achsenPanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_AchsenPanelTitle"));
            rootElement.AppendChild(achsenPanelElement);

            XmlElement gewichtPanelElement = xmlDocument.CreateElement("GewichtPanelTitle");
            gewichtPanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_GewichtPanelTitle"));
            rootElement.AppendChild(gewichtPanelElement);

            XmlElement wartungPanelElement = xmlDocument.CreateElement("WartungPanelTitle");
            wartungPanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_WartungPanelTitle"));
            rootElement.AppendChild(wartungPanelElement);

            XmlElement kabinePanelElement = xmlDocument.CreateElement("KabinePanelTitle");
            kabinePanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_KabinePanelTitle"));
            rootElement.AppendChild(kabinePanelElement);

            XmlElement preissePanelElement = xmlDocument.CreateElement("PreisePanelTitle");
            preissePanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_PreisePanelTitle"));
            rootElement.AppendChild(preissePanelElement);

            XmlElement sonstigesPanelElement = xmlDocument.CreateElement("SonstigesPanelTitle");
            sonstigesPanelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_SonstigesPanelTitle"));
            rootElement.AppendChild(sonstigesPanelElement);

            XmlElement programNameElement = xmlDocument.CreateElement("ProgramName");
            programNameElement.InnerText = ResourceReader.GetString("Comparison_ProgramName");
            rootElement.AppendChild(programNameElement);

            XmlElement documentTitleElement = xmlDocument.CreateElement("DocumentTitle");
            documentTitleElement.InnerText = ResourceReader.GetString("Comparison_DocumentTitle");
            rootElement.AppendChild(documentTitleElement);

            XmlElement companyDataElement = xmlDocument.CreateElement("CompanyData");
            companyDataElement.InnerText = ResourceReader.GetString("Comparison_CompanyData");
            rootElement.AppendChild(companyDataElement);
        }

        protected XmlElement CreateTractorElement(Tractor tractor, XmlDocument xmlDocument)
        {
            XmlElement tractorElement = xmlDocument.CreateElement("Tractor");
            
            XmlElement nameElement = xmlDocument.CreateElement("TractorName");
            nameElement.InnerText = tractor.DisplayName;
            tractorElement.AppendChild(nameElement);

            XmlElement letzteAktualisierungElement = xmlDocument.CreateElement("LetzteAktualisierung");
            letzteAktualisierungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_LetzteAktualisierung"));
            letzteAktualisierungElement.InnerText = tractor.LetzteAktualisierung;
            tractorElement.AppendChild(letzteAktualisierungElement);

            #region Motor
            XmlElement bauartKWElement = xmlDocument.CreateElement("BauartMotor");
            bauartKWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_BauartMotor"));
            bauartKWElement.InnerText = tractor.BauartMotor ?? string.Empty;
            tractorElement.AppendChild(bauartKWElement);

            XmlElement nennleistungElement = xmlDocument.CreateElement("Nennleistung");
            nennleistungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Nennleistung"));
            nennleistungElement.InnerText = string.Format("{0} {1} / {2} {3}", tractor.NennleistungkW, StringManager.GetUnit(Units.kW), tractor.NennleistungPS, StringManager.GetUnit(Units.PS));
            tractorElement.AppendChild(nennleistungElement);

            XmlElement nenndrehzahlUminElement = xmlDocument.CreateElement("NenndrehzahlUmin");
            nenndrehzahlUminElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_NenndrehzahlUmin"));
            nenndrehzahlUminElement.InnerText = string.Format("{0} {1}", tractor.NenndrehzahlUmin, StringManager.GetUnit(Units.min_1));
            tractorElement.AppendChild(nenndrehzahlUminElement);

            XmlElement maximalleistungKWElement = xmlDocument.CreateElement("MaximalleistungKW");
            maximalleistungKWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_MaximalleistungKW"));
            maximalleistungKWElement.InnerText = string.Format("{0} {1}", tractor.MaximalleistungkW, StringManager.GetUnit(Units.kW));
            tractorElement.AppendChild(maximalleistungKWElement);

            XmlElement kuehlungKWElement = xmlDocument.CreateElement("Kuehlung");
            kuehlungKWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Kuehlung"));
            kuehlungKWElement.InnerText = tractor.Kuehlung ?? string.Empty;
            tractorElement.AppendChild(kuehlungKWElement);

            XmlElement zylinderzahlKWElement = xmlDocument.CreateElement("Zylinderzahl");
            zylinderzahlKWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Zylinder"));
            zylinderzahlKWElement.InnerText = tractor.Zylinderzahl ?? string.Empty;
            tractorElement.AppendChild(zylinderzahlKWElement);

            XmlElement hubraumccmKWElement = xmlDocument.CreateElement("Hubraumccm");
            hubraumccmKWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Hubraumccm"));
            hubraumccmKWElement.InnerText = string.Format("{0} {1}", tractor.Hubraumccm, StringManager.GetUnit(Units.ccm));
            tractorElement.AppendChild(hubraumccmKWElement);

            XmlElement mDmaxElement = xmlDocument.CreateElement("MDmax");
            mDmaxElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_MDmax"));
            mDmaxElement.InnerText = string.Format("{0} {1}, {2} {3}", tractor.MDmaxNm, StringManager.GetUnit(Units.min_1), tractor.MDmaxbeiDrehzahl, StringManager.GetUnit(Units.Nm));
            tractorElement.AppendChild(mDmaxElement);

            XmlElement drehmomentanstiegProzentElement = xmlDocument.CreateElement("DrehmomentanstiegProzent");
            drehmomentanstiegProzentElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_DrehmomentanstiegProzent"));
            drehmomentanstiegProzentElement.InnerText = string.Format("{0} {1}", tractor.DrehmomentanstiegProzent, StringManager.GetUnit(Units.Percent));
            tractorElement.AppendChild(drehmomentanstiegProzentElement);

            XmlElement drehzahlabfallProzentElement = xmlDocument.CreateElement("DrehzahlabfallProzent");
            drehzahlabfallProzentElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_DrehzahlabfallProzent"));
            drehzahlabfallProzentElement.InnerText = string.Format("{0} {1}", tractor.DrehzahlabfallProzent, StringManager.GetUnit(Units.Percent));
            tractorElement.AppendChild(drehzahlabfallProzentElement);

            XmlElement konstantleistungProzentElement = xmlDocument.CreateElement("KonstantleistungProzent");
            konstantleistungProzentElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Konstantleistungsbereich"));
            if (!string.IsNullOrEmpty(tractor.KonstantleistungProzent))
                konstantleistungProzentElement.InnerText = string.Format("{0} {1}", tractor.KonstantleistungProzent, StringManager.GetUnit(Units.Percent));
            tractorElement.AppendChild(konstantleistungProzentElement);

            XmlElement uberleistungkWElement = xmlDocument.CreateElement("UberleistungkW");
            uberleistungkWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_UberleistungkW"));
            uberleistungkWElement.InnerText = string.Format("{0} {1}", tractor.UberleistungkW, StringManager.GetUnit(Units.kW));
            tractorElement.AppendChild(uberleistungkWElement);

            XmlElement bohrungxHubElement = xmlDocument.CreateElement("BohrungxHub");
            bohrungxHubElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_BohrungxHub"));
            if (!string.IsNullOrEmpty(tractor.BohrungxHub))
                bohrungxHubElement.InnerText = string.Format("{0} {1}", tractor.BohrungxHub, StringManager.GetUnit(Units.mm));
            else
                bohrungxHubElement.InnerText = string.Empty;
            tractorElement.AppendChild(bohrungxHubElement);

            XmlElement tanksElement = xmlDocument.CreateElement("Tanks");
            tanksElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Tanks"));
            tanksElement.InnerText = string.Format("{0} {1} - {2} {3}", tractor.Tankinhaltl, StringManager.GetUnit(Units.l), StringManager.GetUnit(Units.NumberOfTanks), tractor.AnzahlTanks);
            tractorElement.AppendChild(tanksElement);

            XmlElement verbrauchmaximalgkWhElement = xmlDocument.CreateElement("VerbrauchmaximalgkWh");
            verbrauchmaximalgkWhElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_VerbrauchmaximalgkWh"));
            verbrauchmaximalgkWhElement.InnerText = string.Format("{0} {1}", tractor.VerbrauchmaximalgkWh, StringManager.GetUnit(Units.gKWh));
            tractorElement.AppendChild(verbrauchmaximalgkWhElement);

            XmlElement bestverbrauchElement = xmlDocument.CreateElement("Bestverbrauch");
            bestverbrauchElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Bestverbrauch"));
            bestverbrauchElement.InnerText = string.Format("{0} {1}, {2} {3}", tractor.BestverbrauchgkWh, StringManager.GetUnit(Units.gKWh), tractor.Bestverbrauchlproh, StringManager.GetUnit(Units.l_h));
            tractorElement.AppendChild(bestverbrauchElement);

            XmlElement bestverbrauchbeiDrehzahlElement = xmlDocument.CreateElement("BestverbrauchbeiDrehzahl");
            bestverbrauchbeiDrehzahlElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_BestverbrauchbeiDrehzahl"));
            bestverbrauchbeiDrehzahlElement.InnerText = string.Format("{0} {1}", tractor.BestverbrauchbeiDrehzahl, StringManager.GetUnit(Units.min_1));
            tractorElement.AppendChild(bestverbrauchbeiDrehzahlElement);

            XmlElement mittlererOECD_VerbrauchgkWhElement = xmlDocument.CreateElement("MittlererOECD_VerbrauchgkWh");
            mittlererOECD_VerbrauchgkWhElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_MittlererOECD_VerbrauchgkWh"));
            if(!string.IsNullOrEmpty(tractor.MittlererOECD_VerbrauchgkWh))
                mittlererOECD_VerbrauchgkWhElement.InnerText = string.Format("{0} {1}", tractor.MittlererOECD_VerbrauchgkWh, StringManager.GetUnit(Units.gKWh));
            tractorElement.AppendChild(mittlererOECD_VerbrauchgkWhElement);
            #endregion

            #region Getriebe
            XmlElement gaengeElement = xmlDocument.CreateElement("Gaenge");
            gaengeElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Gangzah"));
            gaengeElement.InnerText = string.Format("{0} {1} {2}", tractor.Gaengevorwaerts, StringManager.GetUnit(Units.Backward), tractor.Gaengerueckwaerts);
            tractorElement.AppendChild(gaengeElement);

            XmlElement endgeschwindigkeitkmhElement = xmlDocument.CreateElement("Endgeschwindigkeitkmh");
            endgeschwindigkeitkmhElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Endgeschwindigkeitkmh"));
            endgeschwindigkeitkmhElement.InnerText = string.Format("{0} {1}", tractor.Endgeschwindigkeitkmh, StringManager.GetUnit(Units.km_h));
            tractorElement.AppendChild(endgeschwindigkeitkmhElement);

            XmlElement synchronisationElement = xmlDocument.CreateElement("Synchronisation");
            synchronisationElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Synchronisation"));
            synchronisationElement.InnerText = tractor.Synchronisation ?? string.Empty;
            tractorElement.AppendChild(synchronisationElement);

            XmlElement c30kmhElement = xmlDocument.CreateElement("C30kmh");
            c30kmhElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_C30kmh"));
            c30kmhElement.InnerText = tractor.C30kmh ?? string.Empty;
            tractorElement.AppendChild(c30kmhElement);

            XmlElement c40kmhElement = xmlDocument.CreateElement("C40kmh");
            c40kmhElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_C40kmh"));
            c40kmhElement.InnerText = tractor.C40kmh ?? string.Empty;
            tractorElement.AppendChild(c40kmhElement);

            XmlElement c50kmhElement = xmlDocument.CreateElement("C50kmh");
            c50kmhElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_C50kmh"));
            c50kmhElement.InnerText = tractor.C50kmh ?? string.Empty;
            tractorElement.AppendChild(c50kmhElement);

            XmlElement c60kmhundmehrElement = xmlDocument.CreateElement("C60kmhundmehr");
            c60kmhundmehrElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_C60kmhundmehr"));
            c60kmhundmehrElement.InnerText = tractor.C60kmhundmehr ?? string.Empty;
            tractorElement.AppendChild(c60kmhundmehrElement);

            XmlElement getriebeWunschElement = xmlDocument.CreateElement("GetriebeWunsch");
            getriebeWunschElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_GetriebeWunsch"));
            getriebeWunschElement.InnerText = tractor.GetriebeWunsch ?? string.Empty;
            tractorElement.AppendChild(getriebeWunschElement);

            XmlElement zahlderSchalthebelElement = xmlDocument.CreateElement("ZahlderSchalthebel");
            zahlderSchalthebelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZahlderSchalthebel"));
            zahlderSchalthebelElement.InnerText = tractor.ZahlderSchalthebel ?? string.Empty;
            tractorElement.AppendChild(zahlderSchalthebelElement);

            XmlElement wendegetriebeElement = xmlDocument.CreateElement("Wendegetriebe");
            wendegetriebeElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Wendegetriebe"));
            wendegetriebeElement.InnerText = tractor.Wendegetriebe ?? string.Empty;
            tractorElement.AppendChild(wendegetriebeElement);

            XmlElement lS_GetriebeElement = xmlDocument.CreateElement("LS_Getriebe");
            lS_GetriebeElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_LS_Getriebe"));
            lS_GetriebeElement.InnerText = tractor.LS_Getriebe ?? string.Empty;
            tractorElement.AppendChild(lS_GetriebeElement);

            XmlElement lSAnzahlStufenElement = xmlDocument.CreateElement("LSAnzahlStufen");
            lSAnzahlStufenElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_LSAnzahlStufen"));
            lSAnzahlStufenElement.InnerText = tractor.LSAnzahlStufen ?? string.Empty;
            tractorElement.AppendChild(lSAnzahlStufenElement);

            XmlElement stufenlosesCVTElement = xmlDocument.CreateElement("StufenlosesCVT");
            stufenlosesCVTElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_StufenlosesCVT"));
            stufenlosesCVTElement.InnerText = tractor.StufenlosesCVT ?? string.Empty;
            tractorElement.AppendChild(stufenlosesCVTElement);

            XmlElement getriebevolllastschaltbarElement = xmlDocument.CreateElement("Getriebevolllastschaltbar");
            getriebevolllastschaltbarElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Getriebevolllastschaltbar"));
            getriebevolllastschaltbarElement.InnerText = tractor.Getriebevolllastschaltbar ?? string.Empty;
            tractorElement.AppendChild(getriebevolllastschaltbarElement);

            XmlElement wendeschaltungElement = xmlDocument.CreateElement("Wendeschaltung");
            wendeschaltungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Wendeschaltung"));
            wendeschaltungElement.InnerText = tractor.Wendeschaltung ?? string.Empty;
            tractorElement.AppendChild(wendeschaltungElement);

            XmlElement wGLastschaltbarElement = xmlDocument.CreateElement("WGLastschaltbar");
            wGLastschaltbarElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_WGLastschaltbar"));
            wGLastschaltbarElement.InnerText = tractor.WGLastschaltbar ?? string.Empty;
            tractorElement.AppendChild(wGLastschaltbarElement);

            XmlElement wGVorwahlbarElement = xmlDocument.CreateElement("WGVorwahlbar");
            wGVorwahlbarElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_WGVorwahlbar"));
            wGVorwahlbarElement.InnerText = tractor.WGVorwahlbar ?? string.Empty;
            tractorElement.AppendChild(wGVorwahlbarElement);

            XmlElement kriechgetriebeElement = xmlDocument.CreateElement("Kriechgetriebe");
            kriechgetriebeElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Kriechgetriebe"));
            kriechgetriebeElement.InnerText = string.Format("{0} {1} {2}", tractor.Kriechgetriebe, StringManager.GetUnit(Units.from), tractor.Kriechgetriebeab, StringManager.GetUnit(Units.m_h));
            tractorElement.AppendChild(kriechgetriebeElement);

            XmlElement gaenge4bis12kmhElement = xmlDocument.CreateElement("Gaenge4bis12kmh");
            gaenge4bis12kmhElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Gaenge4bis12kmh"));
            gaenge4bis12kmhElement.InnerText = tractor.Gaenge4bis12kmh ?? string.Empty;
            tractorElement.AppendChild(gaenge4bis12kmhElement);

            XmlElement gaengeueber15kmhElement = xmlDocument.CreateElement("Gaengeueber15kmh");
            gaengeueber15kmhElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Gaengeueber15kmh"));
            gaengeueber15kmhElement.InnerText = tractor.Gaengeueber15kmh ?? string.Empty;
            tractorElement.AppendChild(gaengeueber15kmhElement);
            #endregion

            #region Zapfwelle
            XmlElement ZW_DrehzahlenElement = xmlDocument.CreateElement("ZW_Drehzahlen");
            ZW_DrehzahlenElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Drehzahlen"));
            ZW_DrehzahlenElement.InnerText = string.Format("{0} {1}", tractor.ZW_Drehzahlen, StringManager.GetUnit(Units.min_1));
            tractorElement.AppendChild(ZW_DrehzahlenElement);

            XmlElement ZW_UminmaximalElement = xmlDocument.CreateElement("ZW_Uminmaximal");
            ZW_UminmaximalElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Uminmaximal"));
            ZW_UminmaximalElement.InnerText = string.Format("{0} {1}", tractor.ZW_Uminmaximal, StringManager.GetUnit(Units.U_min));
            tractorElement.AppendChild(ZW_UminmaximalElement);

            XmlElement ZW_kWElement = xmlDocument.CreateElement("ZW_kW");
            ZW_kWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_kW"));
            ZW_kWElement.InnerText = string.Format("{0} {1}", tractor.ZW_kW, StringManager.GetUnit(Units.kW));
            tractorElement.AppendChild(ZW_kWElement);

            XmlElement ZW_BauartElement = xmlDocument.CreateElement("ZW_Bauart");
            ZW_BauartElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Bauart"));
            ZW_BauartElement.InnerText = tractor.ZW_Bauart ?? string.Empty;
            tractorElement.AppendChild(ZW_BauartElement);

            XmlElement Spar_ZWElement = xmlDocument.CreateElement("Spar_ZW");
            Spar_ZWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Spar_ZW"));
            Spar_ZWElement.InnerText = tractor.Spar_ZW ?? string.Empty;
            tractorElement.AppendChild(Spar_ZWElement);

            XmlElement Weg_ZWElement = xmlDocument.CreateElement("Weg_ZW");
            Weg_ZWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Weg_ZW"));
            Weg_ZWElement.InnerText = tractor.Weg_ZW ?? string.Empty;
            tractorElement.AppendChild(Weg_ZWElement);

            XmlElement BedienungHeckzapfwelleElement = xmlDocument.CreateElement("BedienungHeckzapfwelle");
            BedienungHeckzapfwelleElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_BedienungHeckzapfwelle"));
            BedienungHeckzapfwelleElement.InnerText = tractor.BedienungHeckzapfwelle ?? string.Empty;
            tractorElement.AppendChild(BedienungHeckzapfwelleElement);

            XmlElement ZW_StummelzahlElement = xmlDocument.CreateElement("ZW_Stummelzahl");
            ZW_StummelzahlElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Stummelzahl"));
            ZW_StummelzahlElement.InnerText = tractor.ZW_Stummelzahl ?? string.Empty;
            tractorElement.AppendChild(ZW_StummelzahlElement);

            XmlElement ZW_ProfilElement = xmlDocument.CreateElement("ZW_Profil");
            ZW_ProfilElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Profil"));
            ZW_ProfilElement.InnerText = tractor.ZW_Profil ?? string.Empty;
            tractorElement.AppendChild(ZW_ProfilElement);

            XmlElement ZWlastschaltbarElement = xmlDocument.CreateElement("ZWlastschaltbar");
            ZWlastschaltbarElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZWlastschaltbar"));
            ZWlastschaltbarElement.InnerText = tractor.ZWlastschaltbar ?? string.Empty;
            tractorElement.AppendChild(ZWlastschaltbarElement);

            XmlElement ZW_Mot_Umin_540Element = xmlDocument.CreateElement("ZW_Mot_Umin_540");
            ZW_Mot_Umin_540Element.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Mot_Umin_540"));
            if (!string.IsNullOrEmpty(tractor.ZW_Mot_Umin_540))
                ZW_Mot_Umin_540Element.InnerText = string.Format("{0} {1}", tractor.ZW_Mot_Umin_540, StringManager.GetUnit(Units.min_1));
            tractorElement.AppendChild(ZW_Mot_Umin_540Element);

            XmlElement ZW_Mot_Umin_540EElement = xmlDocument.CreateElement("ZW_Mot_Umin_540E");
            ZW_Mot_Umin_540EElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Mot_Umin_540E"));
            if (!string.IsNullOrEmpty(tractor.ZW_Mot_Umin_540E))
                ZW_Mot_Umin_540EElement.InnerText = string.Format("{0} {1}", tractor.ZW_Mot_Umin_540E, StringManager.GetUnit(Units.min_1));
            tractorElement.AppendChild(ZW_Mot_Umin_540EElement);

            XmlElement ZW_Mot_Umin_1000Element = xmlDocument.CreateElement("ZW_Mot_Umin_1000");
            ZW_Mot_Umin_1000Element.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Mot_Umin_1000"));
            if (!string.IsNullOrEmpty(tractor.ZW_Mot_Umin_1000))
                ZW_Mot_Umin_1000Element.InnerText = string.Format("{0} {1}", tractor.ZW_Mot_Umin_1000, StringManager.GetUnit(Units.min_1));
            tractorElement.AppendChild(ZW_Mot_Umin_1000Element);

            XmlElement ZW_Mot_Umin_1000EElement = xmlDocument.CreateElement("ZW_Mot_Umin_1000E");
            ZW_Mot_Umin_1000EElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZW_Mot_Umin_1000E"));
            if (!string.IsNullOrEmpty(tractor.ZW_Mot_Umin_1000E))
                ZW_Mot_Umin_1000EElement.InnerText = string.Format("{0} {1}", tractor.ZW_Mot_Umin_1000E, StringManager.GetUnit(Units.min_1));
            tractorElement.AppendChild(ZW_Mot_Umin_1000EElement);

            XmlElement Front_ZWUminElement = xmlDocument.CreateElement("Front_ZWUmin");
            Front_ZWUminElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Front_ZWUmin"));
            if (!string.IsNullOrEmpty(tractor.Front_ZWUmin))
                Front_ZWUminElement.InnerText = string.Format("{0} {1}", tractor.Front_ZWUmin, StringManager.GetUnit(Units.U_min));
            tractorElement.AppendChild(Front_ZWUminElement);
            #endregion

            #region Hubwerk
            XmlElement HubkraftmaximaldaNElement = xmlDocument.CreateElement("HubkraftmaximaldaN");
            HubkraftmaximaldaNElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_HubkraftmaximaldaN"));
            HubkraftmaximaldaNElement.InnerText = string.Format("{0} {1}", tractor.HubkraftmaximaldaN, StringManager.GetUnit(Units.daN));
            tractorElement.AppendChild(HubkraftmaximaldaNElement);

            XmlElement HubkraftdurchgehenddaNElement = xmlDocument.CreateElement("HubkraftdurchgehenddaN");
            HubkraftdurchgehenddaNElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_HubkraftdurchgehenddaN"));
            HubkraftdurchgehenddaNElement.InnerText = string.Format("{0} {1}", tractor.HubkraftdurchgehenddaN, StringManager.GetUnit(Units.daN));
            tractorElement.AppendChild(HubkraftdurchgehenddaNElement);

            XmlElement EHRElement = xmlDocument.CreateElement("EHR");
            EHRElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_EHR"));
            EHRElement.InnerText = tractor.EHR ?? string.Empty;
            tractorElement.AppendChild(EHRElement);

            XmlElement HubwerkKategorieElement = xmlDocument.CreateElement("HubwerkKategorie");
            HubwerkKategorieElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_HubwerkKategorie"));
            HubwerkKategorieElement.InnerText = tractor.HubwerkKategorie ?? string.Empty;
            tractorElement.AppendChild(HubwerkKategorieElement);

            XmlElement Zusatz_HubzylinderElement = xmlDocument.CreateElement("Zusatz_Hubzylinder");
            Zusatz_HubzylinderElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Zusatz_Hubzylinder"));
            Zusatz_HubzylinderElement.InnerText = tractor.Zusatz_Hubzylinder ?? string.Empty;
            tractorElement.AppendChild(Zusatz_HubzylinderElement);

            XmlElement FernbedienungimHeckElement = xmlDocument.CreateElement("FernbedienungimHeck");
            FernbedienungimHeckElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_FernbedienungimHeck"));
            FernbedienungimHeckElement.InnerText = tractor.FernbedienungimHeck ?? string.Empty;
            tractorElement.AppendChild(FernbedienungimHeckElement);

            XmlElement SchnellkupplerElement = xmlDocument.CreateElement("Schnellkuppler");
            SchnellkupplerElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Schnellkuppler"));
            SchnellkupplerElement.InnerText = tractor.Schnellkuppler ?? string.Empty;
            tractorElement.AppendChild(SchnellkupplerElement);

            XmlElement Oberlenker_RegelungElement = xmlDocument.CreateElement("Oberlenker_Regelung");
            Oberlenker_RegelungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Oberlenker_Regelung"));
            Oberlenker_RegelungElement.InnerText = tractor.Oberlenker_Regelung ?? string.Empty;
            tractorElement.AppendChild(Oberlenker_RegelungElement);

            XmlElement Unterlenker_RegelungElement = xmlDocument.CreateElement("Unterlenker_Regelung");
            Unterlenker_RegelungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Unterlenker_Regelung"));
            Unterlenker_RegelungElement.InnerText = tractor.Unterlenker_Regelung ?? string.Empty;
            tractorElement.AppendChild(Unterlenker_RegelungElement);

            XmlElement LageregelungElement = xmlDocument.CreateElement("Lageregelung");
            LageregelungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Lageregelung"));
            LageregelungElement.InnerText = tractor.Lageregelung ?? string.Empty;
            tractorElement.AppendChild(LageregelungElement);

            XmlElement SchwimmregelungElement = xmlDocument.CreateElement("Schwimmregelung");
            SchwimmregelungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Schwimmregelung"));
            SchwimmregelungElement.InnerText = tractor.Schwimmregelung ?? string.Empty;
            tractorElement.AppendChild(SchwimmregelungElement);

            XmlElement ZugwiderstandsregelungElement = xmlDocument.CreateElement("Zugwiderstandsregelung");
            ZugwiderstandsregelungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Zugwiderstandsregelung"));
            ZugwiderstandsregelungElement.InnerText = tractor.Zugwiderstandsregelung ?? string.Empty;
            tractorElement.AppendChild(ZugwiderstandsregelungElement);

            XmlElement MischregelungElement = xmlDocument.CreateElement("Mischregelung");
            MischregelungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Mischregelung"));
            MischregelungElement.InnerText = tractor.Mischregelung ?? string.Empty;
            tractorElement.AppendChild(MischregelungElement);

            XmlElement SchlupfregelungElement = xmlDocument.CreateElement("Schlupfregelung");
            SchlupfregelungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Schlupfregelung"));
            SchlupfregelungElement.InnerText = tractor.Schlupfregelung ?? string.Empty;
            tractorElement.AppendChild(SchlupfregelungElement);

            XmlElement FronthubwerkundZWElement = xmlDocument.CreateElement("FronthubwerkundZW");
            FronthubwerkundZWElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_FronthubwerkundZW"));
            FronthubwerkundZWElement.InnerText = tractor.FronthubwerkundZW ?? string.Empty;
            tractorElement.AppendChild(FronthubwerkundZWElement);

            XmlElement FronthubwerkHubkraftdaNElement = xmlDocument.CreateElement("FronthubwerkHubkraftdaN");
            FronthubwerkHubkraftdaNElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_FronthubwerkHubkraftdaN"));
            FronthubwerkHubkraftdaNElement.InnerText = string.Format("{0} {1}", tractor.FronthubwerkHubkraftdaN, StringManager.GetUnit(Units.daN));
            tractorElement.AppendChild(FronthubwerkHubkraftdaNElement);
            #endregion

            #region Hydraulik
            XmlElement HydraulikPumpenleistunglproMinElement = xmlDocument.CreateElement("HydraulikPumpenleistunglproMin");
            HydraulikPumpenleistunglproMinElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_HydraulikPumpenleistunglproMin"));
            HydraulikPumpenleistunglproMinElement.InnerText = string.Format("{0} {1}", tractor.HydraulikPumpenleistunglproMin, StringManager.GetUnit(Units.l_min));
            tractorElement.AppendChild(HydraulikPumpenleistunglproMinElement);

            XmlElement HydraulikNenndruckbarElement = xmlDocument.CreateElement("HydraulikNenndruckbar");
            HydraulikNenndruckbarElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_HydraulikNenndruckbar"));
            HydraulikNenndruckbarElement.InnerText = string.Format("{0} {1}", tractor.HydraulikNenndruckbar, StringManager.GetUnit(Units.bar));
            tractorElement.AppendChild(HydraulikNenndruckbarElement);

            XmlElement ArtHydrauliksystemElement = xmlDocument.CreateElement("ArtHydrauliksystem");
            ArtHydrauliksystemElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ArtHydrauliksystem"));
            ArtHydrauliksystemElement.InnerText = tractor.ArtHydrauliksystem ?? string.Empty;
            tractorElement.AppendChild(ArtHydrauliksystemElement);

            XmlElement AnzahlSteuerventileElement = xmlDocument.CreateElement("AnzahlSteuerventile");
            AnzahlSteuerventileElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_AnzahlSteuerventile"));
            AnzahlSteuerventileElement.InnerText = tractor.AnzahlSteuerventile ?? string.Empty;
            tractorElement.AppendChild(AnzahlSteuerventileElement);

            XmlElement AbreisskupplungenElement = xmlDocument.CreateElement("Abreisskupplungen");
            AbreisskupplungenElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Abreisskupplungen"));
            AbreisskupplungenElement.InnerText = tractor.Abreisskupplungen ?? string.Empty;
            tractorElement.AppendChild(AbreisskupplungenElement);
            #endregion

            #region Achsen
            XmlElement LenkhilfeElement = xmlDocument.CreateElement("Lenkhilfe");
            LenkhilfeElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Lenkhilfe"));
            LenkhilfeElement.InnerText = tractor.Lenkhilfe ?? string.Empty;
            tractorElement.AppendChild(LenkhilfeElement);

            XmlElement HydrostatLenkungElement = xmlDocument.CreateElement("HydrostatLenkung");
            HydrostatLenkungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_HydrostatLenkung"));
            HydrostatLenkungElement.InnerText = tractor.HydrostatLenkung ?? string.Empty;
            tractorElement.AppendChild(HydrostatLenkungElement);

            XmlElement LenkradverstellbarElement = xmlDocument.CreateElement("Lenkradverstellbar");
            LenkradverstellbarElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Lenkradverstellbar"));
            LenkradverstellbarElement.InnerText = tractor.Lenkradverstellbar ?? string.Empty;
            tractorElement.AppendChild(LenkradverstellbarElement);

            XmlElement WendekreisElement = xmlDocument.CreateElement("Wendekreis");
            WendekreisElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Wendekreis"));
            WendekreisElement.InnerText = string.Format("{0} {1}", tractor.Wendekreis, StringManager.GetUnit(Units.m));
            tractorElement.AppendChild(WendekreisElement);

            XmlElement RadstandElement = xmlDocument.CreateElement("Radstand");
            RadstandElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Radstand"));
            RadstandElement.InnerText = string.Format("{0} {1}", tractor.Radstand, StringManager.GetUnit(Units.cm));
            tractorElement.AppendChild(RadstandElement);

            XmlElement BodenfreiheitElement = xmlDocument.CreateElement("Bodenfreiheit");
            BodenfreiheitElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Bodenfreiheit"));
            BodenfreiheitElement.InnerText = string.Format("{0} {1}", tractor.Bodenfreiheit, StringManager.GetUnit(Units.cm));
            tractorElement.AppendChild(BodenfreiheitElement);

            XmlElement SpurweitevorneElement = xmlDocument.CreateElement("Spurweitevorne");
            SpurweitevorneElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Spurweitevorne"));
            SpurweitevorneElement.InnerText = string.Format("{0} {1}", tractor.Spurweitevorne, StringManager.GetUnit(Units.cm));
            tractorElement.AppendChild(SpurweitevorneElement);

            XmlElement SpurweitehintenElement = xmlDocument.CreateElement("Spurweitehinten");
            SpurweitehintenElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Spurweitehinten"));
            SpurweitehintenElement.InnerText = string.Format("{0} {1}", tractor.Spurweitehinten, StringManager.GetUnit(Units.cm));
            tractorElement.AppendChild(SpurweitehintenElement);

            XmlElement BereifungvorneElement = xmlDocument.CreateElement("Bereifungvorne");
            BereifungvorneElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Bereifungvorne"));
            BereifungvorneElement.InnerText = tractor.Bereifungvorne ?? string.Empty;
            tractorElement.AppendChild(BereifungvorneElement);

            XmlElement BereifunghintenElement = xmlDocument.CreateElement("Bereifunghinten");
            BereifunghintenElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Bereifunghinten"));
            BereifunghintenElement.InnerText = tractor.Bereifunghinten ?? string.Empty;
            tractorElement.AppendChild(BereifunghintenElement);

            XmlElement DifferentialspElement = xmlDocument.CreateElement("Differentialsp");
            DifferentialspElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Differentialsp"));
            DifferentialspElement.InnerText = tractor.Differentialsp ?? string.Empty;
            tractorElement.AppendChild(DifferentialspElement);

            XmlElement AllradantriebElement = xmlDocument.CreateElement("Allradantrieb");
            AllradantriebElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Allradantrieb"));
            AllradantriebElement.InnerText = tractor.Allradantrieb ?? string.Empty;
            tractorElement.AppendChild(AllradantriebElement);

            XmlElement VADiffsperreElement = xmlDocument.CreateElement("VADiffsperre");
            VADiffsperreElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_VADiffsperre"));
            VADiffsperreElement.InnerText = tractor.VADiffsperre ?? string.Empty;
            tractorElement.AppendChild(VADiffsperreElement);

            XmlElement VierradbremseElement = xmlDocument.CreateElement("Vierradbremse");
            VierradbremseElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Vierradbremse"));
            VierradbremseElement.InnerText = tractor.Vierradbremse ?? string.Empty;
            tractorElement.AppendChild(VierradbremseElement);

            XmlElement DruckluftbremseElement = xmlDocument.CreateElement("Druckluftbremse");
            DruckluftbremseElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Druckluftbremse"));
            DruckluftbremseElement.InnerText = tractor.Druckluftbremse ?? string.Empty;
            tractorElement.AppendChild(DruckluftbremseElement);

            XmlElement VA_FederungElement = xmlDocument.CreateElement("VA_Federung");
            VA_FederungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_VA_Federung"));
            VA_FederungElement.InnerText = tractor.VA_Federung ?? string.Empty;
            tractorElement.AppendChild(VA_FederungElement);

            XmlElement AchsgewichtElement = xmlDocument.CreateElement("Achsgewicht");
            AchsgewichtElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Achsgewicht"));
            AchsgewichtElement.InnerText = string.Format("{0} {1}, {2} {3}", tractor.Achsgewichtvorne, StringManager.GetUnit(Units.kg), tractor.Achsgewichthinten, StringManager.GetUnit(Units.kg));
            tractorElement.AppendChild(AchsgewichtElement);

            XmlElement GewichtsverteilungElement = xmlDocument.CreateElement("Gewichtsverteilung");
            GewichtsverteilungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Gewichtsverteilung"));
            GewichtsverteilungElement.InnerText = string.Format("{0} {1}", tractor.Gewichtsverteilung, StringManager.GetUnit(Units.Percent));
            tractorElement.AppendChild(GewichtsverteilungElement);

            XmlElement AchslastElement = xmlDocument.CreateElement("Achslast");
            AchslastElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Achslast"));
            AchslastElement.InnerText = string.Format("{0} {1}, {2} {3}", tractor.Achslastvorne, StringManager.GetUnit(Units.kg), tractor.Achslasthinten, StringManager.GetUnit(Units.kg));
            tractorElement.AppendChild(AchslastElement);
            #endregion

            #region Gewicht
            XmlElement GesamtgewichtElement = xmlDocument.CreateElement("Gesamtgewicht");
            GesamtgewichtElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Gesamtgewicht"));
            GesamtgewichtElement.InnerText = string.Format("{0} {1}", tractor.Gesamtgewicht, StringManager.GetUnit(Units.kg));
            tractorElement.AppendChild(GesamtgewichtElement);

            XmlElement LeergewichtElement = xmlDocument.CreateElement("Leergewicht");
            LeergewichtElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Leergewicht"));
            LeergewichtElement.InnerText = string.Format("{0} {1}", tractor.Leergewicht, StringManager.GetUnit(Units.kg));
            tractorElement.AppendChild(LeergewichtElement);

            XmlElement ZulGesamtgewmaxElement = xmlDocument.CreateElement("ZulGesamtgewmax");
            ZulGesamtgewmaxElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ZulGesamtgewmax"));
            ZulGesamtgewmaxElement.InnerText = string.Format("{0} {1}", tractor.ZulGesamtgewmax, StringManager.GetUnit(Units.kg));
            tractorElement.AppendChild(ZulGesamtgewmaxElement);

            XmlElement NutzlastElement = xmlDocument.CreateElement("Nutzlast");
            NutzlastElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Nutzlast"));
            NutzlastElement.InnerText = string.Format("{0} {1}", tractor.Nutzlast, StringManager.GetUnit(Units.kg));
            tractorElement.AppendChild(NutzlastElement);

            XmlElement LaengeElement = xmlDocument.CreateElement("Laenge");
            LaengeElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Laenge"));
            LaengeElement.InnerText = string.Format("{0} {1}", tractor.Laenge, StringManager.GetUnit(Units.cm));
            tractorElement.AppendChild(LaengeElement);

            XmlElement BreiteElement = xmlDocument.CreateElement("Breite");
            BreiteElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Breite"));
            BreiteElement.InnerText = string.Format("{0} {1}", tractor.Breite, StringManager.GetUnit(Units.cm));
            tractorElement.AppendChild(BreiteElement);

            XmlElement HoeheElement = xmlDocument.CreateElement("Hoehe");
            HoeheElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Hoehe"));
            HoeheElement.InnerText = string.Format("{0} {1}", tractor.Hoehe, StringManager.GetUnit(Units.cm));
            tractorElement.AppendChild(HoeheElement);
            #endregion

            #region Wartung
            XmlElement OelMotorElement = xmlDocument.CreateElement("OelMotor");
            OelMotorElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_OelMotor"));
            OelMotorElement.InnerText = string.Format("{0} {1}", tractor.OelMotor, StringManager.GetUnit(Units.l));
            tractorElement.AppendChild(OelMotorElement);

            XmlElement OelwechselMotorStdElement = xmlDocument.CreateElement("OelwechselMotorStd");
            OelwechselMotorStdElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_OelwechselMotorStd"));
            OelwechselMotorStdElement.InnerText = string.Format("{0} {1}", tractor.OelwechselMotorStd, StringManager.GetUnit(Units.Hour));
            tractorElement.AppendChild(OelwechselMotorStdElement);

            XmlElement OelexternElement = xmlDocument.CreateElement("Oelextern");
            OelexternElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Oelextern"));
            OelexternElement.InnerText = string.Format("{0} {1}", tractor.Oelextern, StringManager.GetUnit(Units.l));
            tractorElement.AppendChild(OelexternElement);

            XmlElement OelGetriebeElement = xmlDocument.CreateElement("OelGetriebe");
            OelGetriebeElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_OelGetriebe"));
            OelGetriebeElement.InnerText = string.Format("{0} {1}", tractor.OelGetriebe, StringManager.GetUnit(Units.l));
            tractorElement.AppendChild(OelGetriebeElement);

            XmlElement OelwechselGetrStdElement = xmlDocument.CreateElement("OelwechselGetrStd");
            OelwechselGetrStdElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_OelwechselGetrStd"));
            OelwechselGetrStdElement.InnerText = string.Format("{0} {1}", tractor.OelwechselGetrStd, StringManager.GetUnit(Units.hours));
            tractorElement.AppendChild(OelwechselGetrStdElement);
            #endregion

            #region Kabine
            XmlElement KabineElement = xmlDocument.CreateElement("Kabine");
            KabineElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Kabine"));
            KabineElement.InnerText = tractor.Kabine ?? string.Empty;
            tractorElement.AppendChild(KabineElement);

            XmlElement Niedrig_KabineElement = xmlDocument.CreateElement("Niedrig_Kabine");
            Niedrig_KabineElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Niedrig_Kabine"));
            Niedrig_KabineElement.InnerText = tractor.Niedrig_Kabine ?? string.Empty;
            tractorElement.AppendChild(Niedrig_KabineElement);

            XmlElement KabinenfederungElement = xmlDocument.CreateElement("Kabinenfederung");
            KabinenfederungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Kabinenfederung"));
            KabinenfederungElement.InnerText = tractor.Kabinenfederung ?? string.Empty;
            tractorElement.AppendChild(KabinenfederungElement);

            XmlElement LautstaerkeElement = xmlDocument.CreateElement("Lautstaerke");
            LautstaerkeElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Lautstaerke"));
            LautstaerkeElement.InnerText = string.Format("{0} {1}", tractor.Lautstaerke, StringManager.GetUnit(Units.dB_A));
            tractorElement.AppendChild(LautstaerkeElement);

            XmlElement Klima_AnlageElement = xmlDocument.CreateElement("Klima_Anlage");
            Klima_AnlageElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Klima_Anlage"));
            Klima_AnlageElement.InnerText = tractor.Klima_Anlage ?? string.Empty;
            tractorElement.AppendChild(Klima_AnlageElement);

            XmlElement ISO_BusElement = xmlDocument.CreateElement("ISO_Bus");
            ISO_BusElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_ISO_Bus"));
            ISO_BusElement.InnerText = tractor.ISO_Bus ?? string.Empty;
            tractorElement.AppendChild(ISO_BusElement);

            XmlElement CAN_BusElement = xmlDocument.CreateElement("CAN_Bus");
            CAN_BusElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_CAN_Bus"));
            CAN_BusElement.InnerText = tractor.CAN_Bus ?? string.Empty;
            tractorElement.AppendChild(CAN_BusElement);

            XmlElement AutoLenksystemElement = xmlDocument.CreateElement("AutoLenksystem");
            AutoLenksystemElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_AutoLenksystem"));
            AutoLenksystemElement.InnerText = tractor.AutoLenksystem ?? string.Empty;
            tractorElement.AppendChild(AutoLenksystemElement);

            XmlElement AutomatAHKElement = xmlDocument.CreateElement("AutomatAHK");
            AutomatAHKElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_AutomatAHK"));
            AutomatAHKElement.InnerText = tractor.AutomatAHK ?? string.Empty;
            tractorElement.AppendChild(AutomatAHKElement);

            XmlElement AHKschnellverstElement = xmlDocument.CreateElement("AHKschnellverst");
            AHKschnellverstElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_AHKschnellverst"));
            AHKschnellverstElement.InnerText = tractor.AHKschnellverst ?? string.Empty;
            tractorElement.AppendChild(AHKschnellverstElement);

            XmlElement StuetzlastAHKElement = xmlDocument.CreateElement("StuetzlastAHK");
            StuetzlastAHKElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_StuetzlastAHK"));
            StuetzlastAHKElement.InnerText = string.Format("{0} {1}", tractor.StuetzlastAHK, StringManager.GetUnit(Units.kg));
            tractorElement.AppendChild(StuetzlastAHKElement);

            XmlElement RueckfahreinrchtgElement = xmlDocument.CreateElement("Rueckfahreinrchtg");
            RueckfahreinrchtgElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Rueckfahreinrchtg"));
            RueckfahreinrchtgElement.InnerText = tractor.Rueckfahreinrchtg ?? string.Empty;
            tractorElement.AppendChild(RueckfahreinrchtgElement);

            XmlElement ZugpendelElement = xmlDocument.CreateElement("Zugpendel");
            ZugpendelElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Zugpendel"));
            ZugpendelElement.InnerText = tractor.Zugpendel ?? string.Empty;
            tractorElement.AppendChild(ZugpendelElement);
            #endregion

            #region Preise
            XmlElement PreisvonEuroElement = xmlDocument.CreateElement("PreisvonEuro");
            PreisvonEuroElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_PreisvonEuro"));
            PreisvonEuroElement.InnerText = string.Format("{0} {1}", tractor.PreisvonEuro, StringManager.GetUnit(Units.euro));
            tractorElement.AppendChild(PreisvonEuroElement);

            XmlElement PreisbisEuroElement = xmlDocument.CreateElement("PreisbisEuro");
            PreisbisEuroElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_PreisbisEuro"));
            PreisbisEuroElement.InnerText = string.Format("{0} {1}", tractor.PreisbisEuro, StringManager.GetUnit(Units.euro));
            tractorElement.AppendChild(PreisbisEuroElement);
            #endregion

            #region Sonstiges
            XmlElement BesonderesElement = xmlDocument.CreateElement("Besonderes");
            BesonderesElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Besonderes"));
            BesonderesElement.InnerText = tractor.Besonderes ?? string.Empty;
            tractorElement.AppendChild(BesonderesElement);

            XmlElement AusstattungElement = xmlDocument.CreateElement("Ausstattung");
            AusstattungElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Ausstattung"));
            AusstattungElement.InnerText = tractor.Ausstattung ?? string.Empty;
            tractorElement.AppendChild(AusstattungElement);

            XmlElement PruefberichteElement = xmlDocument.CreateElement("Pruefberichte");
            PruefberichteElement.SetAttribute("ColumnName", ResourceReader.GetString("Comparison_Pruefberichte"));
            PruefberichteElement.InnerText = tractor.Pruefberichte ?? string.Empty;
            tractorElement.AppendChild(PruefberichteElement);
            #endregion

            return tractorElement;
        }

        protected void TransformToHTMLDocument(string xmlIntputFileName, string outputFileName)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));
            using (FileStream outputStream = new FileStream(outputFileName, FileMode.OpenOrCreate))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(outputStream))
                {
                    using (XmlReader xmlReader = XmlReader.Create(xmlIntputFileName))
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        Stream stream = assembly.GetManifestResourceStream("Enceladus.ComparisonTransformationSource.xslt");

                        XslCompiledTransform transformer = new XslCompiledTransform();
                        transformer.Load(XmlReader.Create(stream));
                        transformer.Transform(xmlReader, xmlWriter);
                    }
                }
            }
        }

        protected void OpenHTMLDocument(string outputFilePath)
        {
            bool succeeded = true;
            FileInfo htmlFile = new FileInfo(outputFilePath);
            try
            {
                Process ieProcess = new Process();
                ieProcess.StartInfo.FileName = "file://" + htmlFile.FullName;
                ieProcess.StartInfo.UseShellExecute = true;
                ieProcess.Start();
            }
            catch { succeeded = false; }

            if (succeeded == false)
            {
                Process.Start(htmlFile.FullName);
            }
        }
    }
}
