<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE xsl:stylesheet [
  <!ENTITY nbsp "&#x00A0;">
]>
<xsl:stylesheet
  version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxml="urn:schemas-microsoft-com:xslt"
  xmlns:umbraco.library="urn:umbraco.library" xmlns:DocCart="urn:DocCart"
  exclude-result-prefixes="msxml umbraco.library DocCart">

  <xsl:output method="xml" omit-xml-declaration="yes"/>

  <xsl:param name="currentPage"/>

  <xsl:template match="/">
    <xsl:variable name="formAction" select="umbraco.library:NiceUrl($currentPage/@id)"/>

    <xsl:variable name="items" select="DocCart:CartAsXml()/Items/Item"/>

    <table>
      <thead>
        <th>

        </th>
        <th>
          Stock Code
        </th>
        <th>
          Description
        </th>
        <th>
          Quantity
        </th>
        <th>
          Price
        </th>
      </thead>
      <xsl:choose>
        <xsl:when test="count($items) &gt; 0">
          <xsl:for-each select="$items">
            <tr>
              <td>
                <form action="{$formAction}" method="post">
                  <input type="submit" name="submit" value="Delete"/>
                  <input type="hidden" name="nodeId" value="{@NodeId}"/>
                </form>
              </td>
              <td>
                <xsl:value-of select="DisplayId"/>
              </td>
              <td>
                <xsl:value-of select="Description"/>
              </td>
              <td>
                <form action="{$formAction}" method="post">
                  <input type="text" name="quantity" style="width: 20px;">
                    <xsl:attribute name="value">
                      <xsl:value-of select="Quantity"/>
                    </xsl:attribute>
                  </input>
                  <input type="hidden" name="nodeId" value="{@NodeId}"/>
                  <input type="submit" name="submit" value="Update"/>
                </form>
              </td>
              <td>
                £<xsl:value-of select="format-number(Price, '###,##0.00')"/>
              </td>
            </tr>
          </xsl:for-each>
        </xsl:when>
        <xsl:otherwise>
          <td colspan="5">Your basket is empty</td>
        </xsl:otherwise>
      </xsl:choose>
      <tr>
        <td colspan="4" style="text-align: right;">
          Sub-Total:
        </td>
        <td>
          £<xsl:value-of select="format-number(DocCart:TotalPrice(), '###,##0.00')"/>
        </td>
      </tr>
    </table>
  </xsl:template>
</xsl:stylesheet>