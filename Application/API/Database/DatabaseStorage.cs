using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Threading;
using System.IO;
using Enceladus.Api.Properties;
using System.Diagnostics;

namespace Enceladus.Api
{
    public class DatabaseStorage : IDatabaseStorage
    {
        #region Fields and properties
        private object cacheLock = new object();
        protected static IDictionary<int, Tractor> cachedObjectSet = new Dictionary<int, Tractor>();
        protected static IDictionary<string, IList<TractorSearchResult>> cachedSearchResultSet = new Dictionary<string, IList<TractorSearchResult>>();
        private static readonly string TractorsTableName = "Superkatalog";
        private static readonly string SearchTableName = "Superkatalog_Search";        
        #endregion

        #region Constructors
        public DatabaseStorage()
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.ctor");
        }
        #endregion

        #region Methods
        #region Sync
        public Tractor Get(int id)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.Get", id.ToString());

            lock (cacheLock)
            {
                if (!cachedObjectSet.ContainsKey(id))
                {                    
                    SqlCeConnection connection = CreateConnection();
                    Logger.Instance.Log(LogType.Info, "Connection created");
                    try
                    {
                        Tractor tractor = null;

                        SqlCeCommand command = connection.CreateCommand();
                        command.CommandText = string.Format("SELECT * FROM [{0}] WHERE SATZ = {1}", DatabaseStorage.TractorsTableName, id);


                        Logger.Instance.Log(LogType.Info, "Connection openning");
                        connection.Open();
                        Logger.Instance.Log(LogType.Info, "Connection opened");                        

                        SqlCeDataReader rdr = command.ExecuteReader();
                        Logger.Instance.Log(LogType.Info, "Command executed");

                        while (rdr.Read())
                        {
                            tractor = new Tractor();
                            #region Tractor Structure
                            // Table Superkatalog
                            tractor.Satz = rdr.GetValue(0).ToString();
                            tractor.BauartMotor = rdr.GetValue(1).ToString();
                            tractor.Kuehlung = rdr.GetValue(2).ToString();
                            tractor.NenndrehzahlUmin = rdr.GetValue(3).ToString();
                            tractor.Zylinderzahl = rdr.GetValue(4).ToString();
                            tractor.Hubraumccm = rdr.GetValue(5).ToString();
                            tractor.NennleistungkW = rdr.GetValue(6).ToString();
                            tractor.VerbrauchmaximalgkWh = rdr.GetValue(7).ToString();
                            tractor.NennleistungPS = rdr.GetValue(8).ToString();
                            tractor.ECEoderISO = rdr.GetValue(9).ToString();
                            tractor.BestverbrauchgkWh = rdr.GetValue(10).ToString();
                            tractor.KonstantleistungProzent = rdr.GetValue(11).ToString();
                            tractor.Antriebsart = rdr.GetValue(12).ToString();
                            tractor.Endgeschwindigkeitkmh = rdr.GetValue(13).ToString();
                            tractor.Getriebetyp = rdr.GetValue(14).ToString();
                            tractor.Gaengevorwaerts = rdr.GetValue(15).ToString();
                            tractor.Gaengerueckwaerts = rdr.GetValue(16).ToString();
                            tractor.GetriebeWunsch = rdr.GetValue(17).ToString();
                            tractor.ZW_Drehzahlen = rdr.GetValue(18).ToString();
                            tractor.ZW_kW = rdr.GetValue(19).ToString();
                            tractor.HubwerkKategorie = rdr.GetValue(20).ToString();
                            tractor.HydraulikPumpenleistunglproMin = rdr.GetValue(21).ToString();
                            tractor.HubkraftmaximaldaN = rdr.GetValue(22).ToString();
                            tractor.HydraulikNenndruckbar = rdr.GetValue(23).ToString();
                            tractor.Gesamtgewicht = rdr.GetValue(24).ToString();
                            tractor.Wendekreis = rdr.GetValue(25).ToString();
                            tractor.Bereifungvorne = rdr.GetValue(26).ToString();
                            tractor.Bereifunghinten = rdr.GetValue(27).ToString();
                            tractor.PreisvonEuro = rdr.GetValue(28).ToString();
                            tractor.PreisbisEuro = rdr.GetValue(29).ToString();
                            tractor.Besonderes = rdr.GetValue(30).ToString();
                            tractor.Ausstattung = rdr.GetValue(31).ToString();
                            tractor.Pruefberichte = rdr.GetValue(32).ToString();
                            tractor.LetzteAktualisierung = rdr.GetValue(33).ToString();
                            tractor.Schlepperhersteller = rdr.GetValue(34).ToString();
                            tractor.Schleppertyp = rdr.GetValue(35).ToString();
                            tractor.Seitencodeprofi = rdr.GetValue(36).ToString();
                            tractor.Seitencodetop = rdr.GetValue(37).ToString();
                            tractor.Bild1 = rdr.GetValue(38).ToString();
                            tractor.HerstellerMotor = rdr.GetValue(39).ToString();
                            tractor.MotorTyp = rdr.GetValue(40).ToString();
                            tractor.MaximalleistungkW = rdr.GetValue(41).ToString();
                            tractor.MaxleistungbeiUmin = rdr.GetValue(42).ToString();
                            tractor.MDmaxNm = rdr.GetValue(43).ToString();
                            tractor.MDmaxbeiDrehzahl = rdr.GetValue(44).ToString();
                            tractor.PmaxabUminunten = rdr.GetValue(45).ToString();
                            tractor.DrehmomentanstiegProzent = rdr.GetValue(46).ToString();
                            tractor.DrehzahlabfallProzent = rdr.GetValue(47).ToString();
                            tractor.UberleistungkW = rdr.GetValue(48).ToString();
                            tractor.BoostleistungkW = rdr.GetValue(49).ToString();
                            tractor.BoostleistungbeiUmin = rdr.GetValue(50).ToString();
                            tractor.BohrungxHub = rdr.GetValue(51).ToString();
                            tractor.SCRKatalysator = rdr.GetValue(52).ToString();
                            tractor.Dieseloxydationskatalysator = rdr.GetValue(53).ToString();
                            tractor.Dieselpartikelfilter = rdr.GetValue(54).ToString();
                            tractor.Abgasnorm = rdr.GetValue(55).ToString();
                            tractor.Tankinhaltl = rdr.GetValue(56).ToString();
                            tractor.AnzahlTanks = rdr.GetValue(57).ToString();
                            tractor.Bestverbrauchlproh = rdr.GetValue(58).ToString();
                            tractor.BestverbrauchbeiDrehzahl = rdr.GetValue(59).ToString();
                            tractor.MittlererOECD_VerbrauchgkWh = rdr.GetValue(60).ToString();
                            tractor.PowermixMittel = rdr.GetValue(61).ToString();
                            tractor.AdBlueTankinhaltL = rdr.GetValue(62).ToString();
                            tractor.Getriebehersteller = rdr.GetValue(63).ToString();
                            tractor.Synchronisation = rdr.GetValue(64).ToString();
                            tractor.C30kmh = rdr.GetValue(65).ToString();
                            tractor.C40kmh = rdr.GetValue(66).ToString();
                            tractor.C50kmh = rdr.GetValue(67).ToString();
                            tractor.C60kmhundmehr = rdr.GetValue(68).ToString();
                            tractor.ZahlderSchalthebel = rdr.GetValue(69).ToString();
                            tractor.LS_Getriebe = rdr.GetValue(70).ToString();
                            tractor.Getriebevolllastschaltbar = rdr.GetValue(71).ToString();
                            tractor.LSAnzahlStufen = rdr.GetValue(72).ToString();
                            tractor.StufenlosesCVT = rdr.GetValue(73).ToString();
                            tractor.Wendegetriebe = rdr.GetValue(74).ToString();
                            tractor.Wendeschaltung = rdr.GetValue(75).ToString();
                            tractor.WGLastschaltbar = rdr.GetValue(76).ToString();
                            tractor.WGVorwahlbar = rdr.GetValue(77).ToString();
                            tractor.Kriechgetriebe = rdr.GetValue(78).ToString();
                            tractor.Kriechgetriebeab = rdr.GetValue(79).ToString();
                            tractor.AutomatikfunktionenGetriebe = rdr.GetValue(80).ToString();
                            tractor.Gaenge4bis12kmh = rdr.GetValue(81).ToString();
                            tractor.Gaengeueber15kmh = rdr.GetValue(82).ToString();
                            tractor.DrehzahlreduzierteMaxGeschwindigkeit = rdr.GetValue(83).ToString();
                            tractor.ZW_Uminmaximal = rdr.GetValue(84).ToString();
                            tractor.ZW_Bauart = rdr.GetValue(85).ToString();
                            tractor.Spar_ZW = rdr.GetValue(86).ToString();
                            tractor.Weg_ZW = rdr.GetValue(87).ToString();
                            tractor.ZWFernbedienung = rdr.GetValue(88).ToString();
                            tractor.ZW_Stummelzahl = rdr.GetValue(89).ToString();
                            tractor.ZW_Profil = rdr.GetValue(90).ToString();
                            tractor.ZWlastschaltbar = rdr.GetValue(91).ToString();
                            tractor.ZW_Mot_Umin_540 = rdr.GetValue(92).ToString();
                            tractor.ZW_Mot_Umin_540E = rdr.GetValue(93).ToString();
                            tractor.ZW_Mot_Umin_1000 = rdr.GetValue(94).ToString();
                            tractor.ZW_Mot_Umin_1000E = rdr.GetValue(95).ToString();
                            tractor.Front_ZWUmin = rdr.GetValue(96).ToString();
                            tractor.HubkraftdurchgehenddaN = rdr.GetValue(97).ToString();
                            tractor.EHR = rdr.GetValue(98).ToString();
                            tractor.Zusatz_Hubzylinder = rdr.GetValue(99).ToString();
                            tractor.FernbedienungimHeck = rdr.GetValue(100).ToString();
                            tractor.Schnellkuppler = rdr.GetValue(101).ToString();
                            tractor.Oberlenker_Regelung = rdr.GetValue(102).ToString();
                            tractor.Unterlenker_Regelung = rdr.GetValue(103).ToString();
                            tractor.Lageregelung = rdr.GetValue(104).ToString();
                            tractor.Schwimmregelung = rdr.GetValue(105).ToString();
                            tractor.Zugwiderstandsregelung = rdr.GetValue(106).ToString();
                            tractor.Mischregelung = rdr.GetValue(107).ToString();
                            tractor.Schlupfregelung = rdr.GetValue(108).ToString();
                            tractor.FronthubwerkundZW = rdr.GetValue(109).ToString();
                            tractor.FronthubwerkHubkraftdaN = rdr.GetValue(110).ToString();
                            tractor.ArtHydrauliksystem = rdr.GetValue(111).ToString();
                            tractor.AnzahlSteuerventile = rdr.GetValue(112).ToString();
                            tractor.Abreisskupplungen = rdr.GetValue(113).ToString();
                            tractor.Nutzlast = rdr.GetValue(114).ToString();
                            tractor.Lenkhilfe = rdr.GetValue(115).ToString();
                            tractor.HydrostatLenkung = rdr.GetValue(116).ToString();
                            tractor.Lenkradverstellbar = rdr.GetValue(117).ToString();
                            tractor.Radstand = rdr.GetValue(118).ToString();
                            tractor.Spurweitevorne = rdr.GetValue(119).ToString();
                            tractor.Spurweitehinten = rdr.GetValue(120).ToString();
                            tractor.Differentialsp = rdr.GetValue(121).ToString();
                            tractor.SchaltbarkeitHADiffsperre = rdr.GetValue(122).ToString();
                            tractor.Allradantrieb = rdr.GetValue(123).ToString();
                            tractor.VADiffsperre = rdr.GetValue(124).ToString();
                            tractor.Vierradbremse = rdr.GetValue(125).ToString();
                            tractor.Druckluftbremse = rdr.GetValue(126).ToString();
                            tractor.VA_Federung = rdr.GetValue(127).ToString();
                            tractor.Achsgewichtvorne = rdr.GetValue(128).ToString();
                            tractor.Achsgewichthinten = rdr.GetValue(129).ToString();
                            tractor.Gewichtsverteilung = rdr.GetValue(130).ToString();
                            tractor.Achslastvorne = rdr.GetValue(131).ToString();
                            tractor.Achslasthinten = rdr.GetValue(132).ToString();
                            tractor.Leergewicht = rdr.GetValue(133).ToString();
                            tractor.ZulGesamtgewmax = rdr.GetValue(134).ToString();
                            tractor.Laenge = rdr.GetValue(135).ToString();
                            tractor.Breite = rdr.GetValue(136).ToString();
                            tractor.Hoehe = rdr.GetValue(137).ToString();
                            tractor.OelMotor = rdr.GetValue(138).ToString();
                            tractor.OelwechselMotorStd = rdr.GetValue(139).ToString();
                            tractor.Oelextern = rdr.GetValue(140).ToString();
                            tractor.OelGetriebe = rdr.GetValue(141).ToString();
                            tractor.OelwechselGetrStd = rdr.GetValue(142).ToString();
                            tractor.OelHydraulikSerie = rdr.GetValue(143).ToString();
                            tractor.OelHydraulikOption = rdr.GetValue(144).ToString();
                            tractor.OelwechselHydrStd = rdr.GetValue(145).ToString();
                            tractor.Kabine = rdr.GetValue(146).ToString();
                            tractor.Niedrig_Kabine = rdr.GetValue(147).ToString();
                            tractor.Kabinenfederung = rdr.GetValue(148).ToString();
                            tractor.Lautstaerke = rdr.GetValue(149).ToString();
                            tractor.Rueckfahreinrchtg = rdr.GetValue(150).ToString();
                            tractor.Klima_Anlage = rdr.GetValue(151).ToString();
                            tractor.ISO_Bus = rdr.GetValue(152).ToString();
                            tractor.CAN_Bus = rdr.GetValue(153).ToString();
                            tractor.AutomatAHK = rdr.GetValue(154).ToString();
                            tractor.AHKschnellverst = rdr.GetValue(155).ToString();
                            tractor.StuetzlastAHK = rdr.GetValue(156).ToString();
                            tractor.Motor_Getriebe_Management = rdr.GetValue(157).ToString();
                            tractor.Zugpendel = rdr.GetValue(158).ToString();
                            tractor.AutoLenksystem = rdr.GetValue(159).ToString();
                            tractor.Bodenfreiheit = rdr.GetValue(160).ToString();
                            tractor.Katalogteil = rdr.GetValue(161).ToString();
                            tractor.BedienungHeckzapfwelle = rdr.GetValue(162).ToString();
                            #endregion
                        }

                        cachedObjectSet.Add(id, tractor);
                        Logger.Instance.Log(LogType.Info, "Tractor cached");
                    }
                    catch(Exception ex)
                    {
                        Logger.Instance.Log(LogType.Error, "DatabaseStorage.Get", "Exception while proceding Get method on database: " + ex.ToString());
                    }
                    finally
                    {
                        Logger.Instance.Log(LogType.Info, "Connection closing");
                        connection.Close();
                        Logger.Instance.Log(LogType.Info, "Connection closed");
                    }
                }
            }
            return cachedObjectSet[id];
        }
        
        public IList<TractorSearchResult> Search(string criteria, int start, int count)
        {
            string combineCachKey = string.Format("{0}_{1}_{2}", criteria, start, count);
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.Search", combineCachKey);

            if (!cachedSearchResultSet.ContainsKey(combineCachKey))
            {
                SqlCeConnection connection = CreateConnection();
                Logger.Instance.Log(LogType.Info, "Connection created");

                try
                {
                    SqlCeCommand command = connection.CreateCommand();
                    Logger.Instance.Log(LogType.Info, "Connection created");

                    if (!string.IsNullOrEmpty(criteria.Trim()))
                        command.CommandText = string.Format("SELECT * FROM [{0}] WHERE {1}", DatabaseStorage.SearchTableName, criteria);
                    else
                        command.CommandText = string.Format("SELECT * FROM [{0}]", DatabaseStorage.SearchTableName);
                    
                    Logger.Instance.Log(LogType.Info, "Connection openning");
                    connection.Open();
                    Logger.Instance.Log(LogType.Info, "Connection opened");

                    SqlCeDataReader rdr = command.ExecuteReader();
                    Logger.Instance.Log(LogType.Info, "Command executed");

                    IList<TractorSearchResult> result = new List<TractorSearchResult>();
                    //StringBuilder a = new StringBuilder();
                    while (rdr.Read())
                    {
                        //for (int i = 0; i < rdr.FieldCount; i++)
                        //{
                        //    a.AppendLine(i + " " + rdr.GetName(i));
                        //}
                        //string b = a.ToString();

                        TractorSearchResult tractorSearchResult = new TractorSearchResult();
                        // Table Superkatalog_Search
                        tractorSearchResult.Satz = rdr.GetValue(0).ToString();
                        tractorSearchResult.LetzteAktualisierung = rdr.GetValue(1).ToString();
                        tractorSearchResult.Schlepperhersteller = rdr.GetValue(2).ToString();
                        tractorSearchResult.Schleppertyp = rdr.GetValue(3).ToString();
                        tractorSearchResult.NennleistungkW = rdr.GetValue(4).ToString();
                        tractorSearchResult.NennleistungPS = rdr.GetValue(5).ToString();
                        tractorSearchResult.Gesamtgewicht = rdr.GetValue(6).ToString();
                        tractorSearchResult.Nutzlast = rdr.GetValue(7).ToString();
                        tractorSearchResult.Wendekreis = rdr.GetValue(8).ToString();
                        tractorSearchResult.Hoehe = rdr.GetValue(9).ToString();
                        tractorSearchResult.LS_Getriebe = rdr.GetValue(10).ToString();
                        tractorSearchResult.Kriechgetriebe = rdr.GetValue(11).ToString();
                        tractorSearchResult.FronthubwerkundZW = rdr.GetValue(12).ToString();
                        tractorSearchResult.HubkraftmaximaldaN = rdr.GetValue(13).ToString();
                        tractorSearchResult.PreisvonEuro = rdr.GetValue(14).ToString();
                        tractorSearchResult.Katalogteil = rdr.GetValue(15).ToString();

                        result.Add(tractorSearchResult);
                    }
                    cachedSearchResultSet.Add(combineCachKey, result);
                    Logger.Instance.Log(LogType.Info, "Result cached");
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(LogType.Error, "DatabaseStorage.Search", "Exception while proceding Search method on database: " + ex.ToString());
                }
                finally
                {
                    Logger.Instance.Log(LogType.Info, "Connection closing");
                    connection.Close();
                    Logger.Instance.Log(LogType.Info, "Connection closed");
                }
            }

            return cachedSearchResultSet[combineCachKey];
        }

        public bool IsTractorInCache(int tractorIndex)
        {
            bool result;
            lock (cacheLock)
            {
                result = cachedObjectSet.ContainsKey(tractorIndex);
            }
            return result;
        }

        public void CreateIndex(string indexName, string columnName)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.CreateIndex");

            SqlCeConnection connection = CreateConnection();
            Logger.Instance.Log(LogType.Info, "Connection created");
            try
            {
                SqlCeCommand command = connection.CreateCommand();
                command.CommandText = string.Format("CREATE UNIQUE INDEX {0} on [{1}] ([{2}])", indexName, DatabaseStorage.TractorsTableName, columnName);

                Logger.Instance.Log(LogType.Info, "Connection openning");
                connection.Open();
                Logger.Instance.Log(LogType.Info, "Connection opened");

                command.ExecuteNonQuery();
                Logger.Instance.Log(LogType.Info, "Command executed");
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "DatabaseStorage.CreateIndex", "Exception while creating index on database: " + ex.ToString());
            }
            finally
            {
                Logger.Instance.Log(LogType.Info, "Connection closing");
                connection.Close();
                Logger.Instance.Log(LogType.Info, "Connection closed");
            }
        }

        public void DropIndex(string indexName)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.DropIndex");

            SqlCeConnection connection = CreateConnection();
            Logger.Instance.Log(LogType.Info, "Connection created");

            try
            {
                SqlCeCommand command = connection.CreateCommand();

                command.CommandText = string.Format("DROP INDEX [{0}].[{1}]", DatabaseStorage.TractorsTableName, indexName);

                Logger.Instance.Log(LogType.Info, "Connection openning");
                connection.Open();
                Logger.Instance.Log(LogType.Info, "Connection opened");

                command.ExecuteNonQuery();
                Logger.Instance.Log(LogType.Info, "Command executed");
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "DatabaseStorage.DropIndex", "Exception while dropping index on database: " + ex.ToString());
            }
            finally
            {
                Logger.Instance.Log(LogType.Info, "Connection closing");
                connection.Close();
                Logger.Instance.Log(LogType.Info, "Connection closed");
            }
        }

        protected SqlCeConnection CreateConnection()
        {
            string connectionString = string.Format(Settings.Default.ProfiDbConnectionString, GlobalSettings.DatabaseFilePath);
            return new SqlCeConnection(connectionString);
        }
        #endregion

        #region Async
        public IAsyncResult BeginGet(AsyncCallback callback, object state, int tracorId)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.BeginGet", tracorId.ToString());
            AsyncResult<Tractor> ar = new AsyncResult<Tractor>(callback, state);
            ThreadPool.QueueUserWorkItem(new WaitCallback(GetAsyncWrapper), new AsyncResultAndIdEntity(ar, tracorId));
            return ar;
        }

        public Tractor EndGet(IAsyncResult result)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.EndGet", result.IsCompleted.ToString());
            AsyncResult<Tractor> ar = (result as AsyncResult<Tractor>);
            return ar.EndInvoke();
        }

        protected void GetAsyncWrapper(object argument)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.GetAsyncWrapper");
            AsyncResultAndIdEntity getEntity = (argument as AsyncResultAndIdEntity);
            IDatabaseStorage dbStorage = (getEntity.asyncResult.AsyncState as IDatabaseStorage);
            try
            {
                Tractor tractor = dbStorage.Get(getEntity.tractorId);
                getEntity.asyncResult.SetAsCompleted(tractor, false);
            }
            catch (Exception ex)
            {
                getEntity.asyncResult.SetAsCompleted(ex, false);
                Logger.Instance.Log(LogType.Error, "DatabaseStorage.GetAsyncWrapper", ex.ToString());
            }
        }

        public IAsyncResult BeginSearch(AsyncCallback callback, object state, string criteria, int start, int count)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.BeginSearch", "start: " + start.ToString() + ", count: " + count.ToString() +", criteria: " + criteria);
            AsyncResult<IList<TractorSearchResult>> ar = new AsyncResult<IList<TractorSearchResult>>(callback, state);
            ThreadPool.QueueUserWorkItem(new WaitCallback(SearchAsyncWrapper), new AsyncResultAndSearchCriterias(ar, criteria, start, count));
            return ar;
        }

        public IList<TractorSearchResult> EndSearch(IAsyncResult result)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.EndSearch", result.IsCompleted.ToString());
            AsyncResult<IList<TractorSearchResult>> ar = (result as AsyncResult<IList<TractorSearchResult>>);
            return ar.EndInvoke();
        }

        protected void SearchAsyncWrapper(object argument)
        {
            Logger.Instance.Log(LogType.Info, "DatabaseStorage.SearchAsyncWrapper");
            AsyncResultAndSearchCriterias searchEntity = (argument as AsyncResultAndSearchCriterias);
            IDatabaseStorage dbStorage = (searchEntity.asyncResult.AsyncState as IDatabaseStorage);
            try
            {
                IList<TractorSearchResult> resultSet = dbStorage.Search(searchEntity.searchCriteria, searchEntity.start, searchEntity.count);
                searchEntity.asyncResult.SetAsCompleted(resultSet, false);
            }
            catch (Exception ex)
            {
                searchEntity.asyncResult.SetAsCompleted(ex, false);
                Logger.Instance.Log(LogType.Error, "DatabaseStorage.SearchAsyncWrapper", ex.ToString());
            }
        }
        #endregion
        #endregion

        #region Structures
        private class AsyncResultAndIdEntity
        {
            public AsyncResult<Tractor> asyncResult { get; set; }
            public int tractorId { get; set; }
            public AsyncResultAndIdEntity(AsyncResult<Tractor> ar, int id)
            {
                this.asyncResult = ar;
                this.tractorId = id;
            }
        }

        private class AsyncResultAndSearchCriterias
        {
            public AsyncResult<IList<TractorSearchResult>> asyncResult { get; set; }
            public string searchCriteria { get; set; }
            public int start { get; set; }
            public int count { get; set; }

            public AsyncResultAndSearchCriterias(AsyncResult<IList<TractorSearchResult>> ar, string searchCriteria, int start, int count)
            {
                this.asyncResult = ar; 
                this.searchCriteria = searchCriteria;
                this.start = start;
                this.count = count;
            }
        }
        #endregion
    }
}
