<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:nant="http://nant.sf.net/schemas/nant.xsd" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/nant:project">
    <html>
      <head>
        <style type="text/css">

          body {
          font-family:"ARIAL";
          font-size: 10pt;
          }

          h1 {
          border-bottom: 1px solid black;
          font-size: 14pt;
          }

          h2 {
          display: inline;
          font-size: 10pt;
          font-weight: normal;
          }

          h2 a {
          display: inline;
          font-size: 12pt;
          font-weight: bold;
          margin-right: 10px;
          }

          li {
          margin-bottom: 20px;
          }

          .targetBlock {
          margin-bottom:30px;
          }

          .argValue {
          font-family: Consolas, Courier New, Courier;
          }

        </style>
      </head>
      <body>
        <xsl:if test="count(nant:target[@name='install']) > 0">
          <div class="targetBlock">
            <h1>
              Install <xsl:value-of select="@name"/>
            </h1>

            <ol>
              <xsl:for-each select="nant:target[@name='install']">
                <xsl:call-template name="subTarget">
                  <xsl:with-param name="currentNode" select="."/>
                </xsl:call-template>
              </xsl:for-each>
            </ol>
          </div>
        </xsl:if>

        <xsl:if test="count(nant:target[@name='install']) > 0">
          <div class="targetBlock">
            <h1>
              Uninstall <xsl:value-of select="@name"/>
            </h1>

            <ol>
              <xsl:for-each select="nant:target[@name='uninstall']">
                <xsl:call-template name="subTarget">
                  <xsl:with-param name="currentNode" select="."/>
                </xsl:call-template>
              </xsl:for-each>
            </ol>
          </div>
        </xsl:if>

      </body>
    </html>
  </xsl:template>

  <xsl:template name="subTarget">
    <xsl:param name="currentNode"/>
    <xsl:for-each select="$currentNode/*">
      <li>
        <xsl:choose>
          <xsl:when test="name(..) = 'target'">
            <h2>
              <a href='/{name(.)}.html'>
                <xsl:value-of select="name(.)"/>
              </a>
            </h2>
          </xsl:when>
          <xsl:when test="name(..) = 'if'">
            <h2>
              <a href='/{name(.)}.html'>
                <xsl:value-of select="name(.)"/>
              </a>
            </h2>
          </xsl:when>
          <xsl:when test="name(..) = 'do'">
            <h2>
              <a href='/{name(.)}.html'>
                <xsl:value-of select="name(.)"/>
              </a>
            </h2>
          </xsl:when>
          <xsl:when test="name(..) = 'try'">
            <h2>
              <a href='/{name(.)}.html'>
                <xsl:value-of select="name(.)"/>
              </a>
            </h2>
          </xsl:when>
          <xsl:when test="name(..) = 'catch'">
            <h2>
              <a href='/{name(.)}.html'>
                <xsl:value-of select="name(.)"/>
              </a>
            </h2>
          </xsl:when>
          <xsl:when test="name(..) = 'finally'">
            <h2>
              <a href='/{name(.)}.html'>
                <xsl:value-of select="name(.)"/>
              </a>
            </h2>
          </xsl:when>
          <xsl:when test="name(..) = 'list-foreach'">
            <h2>
              <a href='/{name(.)}.html'>
                <xsl:value-of select="name(.)"/>
              </a>
            </h2>
          </xsl:when>
          <xsl:otherwise>
            <h2>
              <xsl:value-of select="name(.)"/>
            </h2>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:for-each select="@*">
          &#160;<xsl:value-of select="name(.)"/>=&quot;<span class="argValue"><xsl:value-of select="."/></span>&quot;
        </xsl:for-each>
        <xsl:if test="child::*">
          <ul>
            <xsl:call-template name="subTarget">
              <xsl:with-param name="currentNode" select="."/>
            </xsl:call-template>
          </ul>
        </xsl:if>
      </li>
    </xsl:for-each>
  </xsl:template>

</xsl:stylesheet>