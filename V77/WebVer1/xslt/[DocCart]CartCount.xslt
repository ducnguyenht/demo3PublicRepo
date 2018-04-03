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
    <xsl:variable name="itemsInCart" select="DocCart:CartCount()"/>
    <span class="cartCount">
      <xsl:choose>
        <xsl:when test="$itemsInCart = 0">
          Your cart is empty.
        </xsl:when>
        <xsl:when test="$itemsInCart = 1">
          There is 1 item in your cart.
        </xsl:when>
        <xsl:otherwise>
          There are <xsl:value-of select="$itemsInCart"/> items in your cart.
        </xsl:otherwise>
      </xsl:choose>
    </span>
  </xsl:template>
</xsl:stylesheet>