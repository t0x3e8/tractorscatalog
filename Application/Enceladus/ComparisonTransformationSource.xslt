<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="/">
    <HTML>
      <HEAD>
        <script src="jquery.js" type="text/javascript">
          // <![CDATA[ // ]]>
        </script>
        <script type="text/javascript">
          $(document).ready(function () {
          $("#comparisonTable tr:gt(1)").mouseover(function () {
          $(this).addClass("rowSelected");
          }).mouseout(function () {
          $(this).removeClass("rowSelected");
          });

          $("img").click(function () {
          var tableSector = $(this).parents("tr:first").parent().next();
          if (tableSector.is(":visible")) {
          tableSector.hide();
          $(this).attr("src", "expand.png");
          }
          else {
          tableSector.show();
          $(this).attr("src", "collapse.png");
          }
          });
          });
        </script>
        <style type="text/css">
          tr.header { background-color: #c3c3c3}
          td.rowHeader { font-weight:Bold; width: 250px; }
          .rowDataHeader { padding-left: 25px; }
          .rowSelected { background-color: #E0E0E0; }
          td.columnHeader {text-align: center;  font-weight:Bold; }
          td { vertical-align:middle; width: 200px; padding: 2px; }
          img.collabibleIconStyle { width:6%; float:left; }
          p.rowHeader { width:93%; }
          #documentTitle { font-size:x-large;  text-align: right;}
          #ProgramName { font-size:x-large;}
          #HeaderTable { width: 100%; }
          #HeaderTable td { width: 50%; }
        </style>
        <TITLE>Schlepper Vergleich</TITLE>
      </HEAD>
      <BODY>
        <xsl:element name="table">
          <xsl:attribute name="id">HeaderTable</xsl:attribute>
          <xsl:element name="tr">
            <xsl:element name="td">
              <xsl:attribute name="id">ProgramName</xsl:attribute>
              <xsl:value-of select="CompareInformation/ProgramName" />
            </xsl:element>
            <xsl:element name="td">
              <xsl:attribute name="id">DocumentTitle</xsl:attribute>
              <xsl:value-of select="//CompareInformation/DocumentTitle" />
            </xsl:element>
          </xsl:element>
        </xsl:element>

        <xsl:element name="br"></xsl:element>
        <xsl:element name="br"></xsl:element>

        <table border="0" cellSpacing="0" cellPadding="0" id="comparisonTable">
          <xsl:element name="tr">
            <xsl:element name="td" />
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td">
                <xsl:attribute name="class">columnHeader</xsl:attribute>
                <xsl:value-of select="TractorName" />
              </xsl:element>
            </xsl:for-each>
          </xsl:element>

          <!-- Each record on a seperate row -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">KatalogJahrImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('KatalogJahrBody', 'KatalogJahrImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/KatalogjahrPanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">KatalogJahrBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/LetzteAktualisierung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="LetzteAktualisierung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!--Motor-->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">MotorImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('MotorBody', 'MotorImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/MotorPanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">MotorBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/BauartMotor/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="BauartMotor" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Nennleistung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Nennleistung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/NenndrehzahlUmin/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="NenndrehzahlUmin" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/MaximalleistungKW/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="MaximalleistungKW" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Kuehlung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Kuehlung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Zylinderzahl/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Zylinderzahl" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Hubraumccm/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Hubraumccm" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/MDmax/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="MDmax" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/DrehmomentanstiegProzent/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="DrehmomentanstiegProzent" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/DrehzahlabfallProzent/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="DrehzahlabfallProzent" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/KonstantleistungProzent/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="KonstantleistungProzent" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/UberleistungkW/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="UberleistungkW" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/BohrungxHub/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="BohrungxHub" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Tanks/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Tanks" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/VerbrauchmaximalgkWh/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="VerbrauchmaximalgkWh" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Bestverbrauch/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Bestverbrauch" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Hubraumccm/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Hubraumccm" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/BestverbrauchbeiDrehzahl/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="BestverbrauchbeiDrehzahl" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/MittlererOECD_VerbrauchgkWh/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="MittlererOECD_VerbrauchgkWh" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!--Getriebe-->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">GetriebeImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('GetriebeBody', 'GetriebeImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/GetriebePanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">GetriebeBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Gaenge/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Gaenge" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Endgeschwindigkeitkmh/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Endgeschwindigkeitkmh" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Synchronisation/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Synchronisation" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/C30kmh/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="C30kmh" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/C40kmh/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="C40kmh" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/C50kmh/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="C50kmh" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/C60kmhundmehr/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="C60kmhundmehr" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/GetriebeWunsch/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="GetriebeWunsch" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZahlderSchalthebel/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZahlderSchalthebel" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Wendegetriebe/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Wendegetriebe" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/LS_Getriebe/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="LS_Getriebe" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/LSAnzahlStufen/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="LSAnzahlStufen" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/StufenlosesCVT/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="StufenlosesCVT" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Getriebevolllastschaltbar/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Getriebevolllastschaltbar" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Wendeschaltung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Wendeschaltung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/WGLastschaltbar/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="WGLastschaltbar" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/WGVorwahlbar/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="WGVorwahlbar" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Kriechgetriebe/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Kriechgetriebe" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Gaenge4bis12kmh/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Gaenge4bis12kmh" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Gaengeueber15kmh/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Gaengeueber15kmh" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Zapfwelle -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">ZapfwelleImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('ZapfwelleBody', 'ZapfwelleImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/ZapfwellePanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">ZapfwelleBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Drehzahlen/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Drehzahlen" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Uminmaximal/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Uminmaximal" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_kW/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_kW" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Bauart/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Bauart" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Spar_ZW/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Spar_ZW" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Weg_ZW/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Weg_ZW" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/BedienungHeckzapfwelle/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="BedienungHeckzapfwelle" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Stummelzahl/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Stummelzahl" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Profil/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Profil" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZWlastschaltbar/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZWlastschaltbar" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Mot_Umin_540/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Mot_Umin_540" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Mot_Umin_540E/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Mot_Umin_540E" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Mot_Umin_1000/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Mot_Umin_1000" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZW_Mot_Umin_1000E/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZW_Mot_Umin_1000E" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Front_ZWUmin/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Front_ZWUmin" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Hubwerk -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">HubwerkImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('HubwerkBody', 'HubwerkImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/HubwerkPanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">HubwerkBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/HubkraftmaximaldaN/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="HubkraftmaximaldaN" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/HubkraftdurchgehenddaN/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="HubkraftdurchgehenddaN" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/EHR/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="EHR" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/HubwerkKategorie/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="HubwerkKategorie" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Zusatz_Hubzylinder/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Zusatz_Hubzylinder" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/FernbedienungimHeck/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="FernbedienungimHeck" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Schnellkuppler/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Schnellkuppler" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Oberlenker_Regelung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Oberlenker_Regelung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Unterlenker_Regelung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Unterlenker_Regelung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Lageregelung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Lageregelung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Schwimmregelung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Schwimmregelung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Zugwiderstandsregelung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Zugwiderstandsregelung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Mischregelung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Mischregelung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Schlupfregelung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Schlupfregelung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/FronthubwerkundZW/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="FronthubwerkundZW" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/FronthubwerkHubkraftdaN/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="FronthubwerkHubkraftdaN" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Hydraulik -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">HydraulikImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('HydraulikBody', 'HydraulikImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/HydraulikPanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">HydraulikBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/HydraulikPumpenleistunglproMin/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="HydraulikPumpenleistunglproMin" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/HydraulikNenndruckbar/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="HydraulikNenndruckbar" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ArtHydrauliksystem/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ArtHydrauliksystem" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/AnzahlSteuerventile/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="AnzahlSteuerventile" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Abreisskupplungen/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Abreisskupplungen" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Achsen -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">AchsenImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('AchsenBody', 'AchsenImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/AchsenPanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">AchsenBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Lenkhilfe/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Lenkhilfe" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/HydrostatLenkung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="HydrostatLenkung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Lenkradverstellbar/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Lenkradverstellbar" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Wendekreis/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Wendekreis" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Radstand/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Radstand" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Bodenfreiheit/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Bodenfreiheit" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Spurweitevorne/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Spurweitevorne" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Spurweitehinten/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Spurweitehinten" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Bereifungvorne/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Bereifungvorne" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Bereifunghinten/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Bereifunghinten" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Differentialsp/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Differentialsp" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Allradantrieb/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Allradantrieb" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/VADiffsperre/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="VADiffsperre" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Vierradbremse/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Vierradbremse" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Druckluftbremse/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Druckluftbremse" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/VA_Federung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="VA_Federung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Achsgewicht/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Achsgewicht" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Gewichtsverteilung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Gewichtsverteilung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Achslast/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Achslast" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Gewicht -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">GewichtImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('GewichtBody', 'GewichtImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/GewichtPanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">GewichtBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Gesamtgewicht/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Gesamtgewicht" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Leergewicht/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Leergewicht" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ZulGesamtgewmax/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ZulGesamtgewmax" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Nutzlast/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Nutzlast" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Laenge/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Laenge" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Breite/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Breite" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Hoehe/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Hoehe" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Wartung -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">WartungImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('WartungBody', 'WartungImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/WartungPanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">WartungBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/OelMotor/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="OelMotor" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/OelwechselMotorStd/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="OelwechselMotorStd" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Oelextern/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Oelextern" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/OelGetriebe/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="OelGetriebe" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/OelwechselGetrStd/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="OelwechselGetrStd" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Kabine -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">KabineImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('KabineBody', 'KabineImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/KabinePanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">KabineBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Kabine/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Kabine" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Niedrig_Kabine/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Niedrig_Kabine" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Kabinenfederung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Kabinenfederung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Lautstaerke/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Lautstaerke" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Klima_Anlage/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Klima_Anlage" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/ISO_Bus/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="ISO_Bus" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/CAN_Bus/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="CAN_Bus" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/AutoLenksystem/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="AutoLenksystem" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/AutomatAHK/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="AutomatAHK" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/AHKschnellverst/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="AHKschnellverst" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/StuetzlastAHK/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="StuetzlastAHK" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Rueckfahreinrchtg/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Rueckfahreinrchtg" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Zugpendel/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Zugpendel" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Preise -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="img">
                <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                <xsl:attribute name="id">PreiseImg</xsl:attribute>
                <xsl:attribute name="src">collapse.png</xsl:attribute>
                <xsl:attribute name="onclick">toggleItem('PreiseBody', 'PreiseImg')</xsl:attribute>
              </xsl:element>
              <xsl:element name="p">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/PreisePanelTitle/@ColumnName"/>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">PreiseBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/PreisvonEuro/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="PreisvonEuro" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/PreisbisEuro/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="PreisbisEuro" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>

          <!-- Sonstiges -->
          <xsl:element name="tr">
            <xsl:attribute name="class">header</xsl:attribute>
            <xsl:element name="td">
              <xsl:attribute name="class">rowHeader</xsl:attribute>
              <xsl:element name="div">
                <xsl:element name="img">
                  <xsl:attribute name="class">collabibleIconStyle</xsl:attribute>
                  <xsl:attribute name="id">SonstigesImg</xsl:attribute>
                  <xsl:attribute name="src">collapse.png</xsl:attribute>
                  <xsl:attribute name="onclick">toggleItem('SonstigesBody', 'SonstigesImg')</xsl:attribute>
                </xsl:element>
                <xsl:element name="p">
                  <xsl:attribute name="class">rowHeader</xsl:attribute>
                  <xsl:value-of select="//CompareInformation/SonstigesPanelTitle/@ColumnName"/>
                </xsl:element>
              </xsl:element>
            </xsl:element>
            <xsl:for-each select="//CompareInformation/Tractor">
              <xsl:element name="td" />
            </xsl:for-each>
          </xsl:element>

          <xsl:element name="tbody">
            <xsl:attribute name="id">SonstigesBody</xsl:attribute>
            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Besonderes/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Besonderes" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Ausstattung/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Ausstattung" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>

            <xsl:element name="tr">
              <xsl:element name="td">
                <xsl:attribute name="class">rowHeader</xsl:attribute>
                <xsl:attribute name="class">rowDataHeader</xsl:attribute>
                <xsl:value-of select="//CompareInformation/Tractor/Pruefberichte/@ColumnName"/>
              </xsl:element>
              <xsl:for-each select="//CompareInformation/Tractor">
                <xsl:element name="td">
                  <xsl:value-of select="Pruefberichte" />
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
          </xsl:element>
        </table>

        <xsl:element name="br"></xsl:element>
        <xsl:element name="br"></xsl:element>
        <xsl:value-of select="//CompareInformation/CompanyData" />
      </BODY>
    </HTML>
  </xsl:template>
</xsl:stylesheet>