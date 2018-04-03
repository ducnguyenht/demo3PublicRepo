<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE xsl:stylesheet [ <!ENTITY nbsp "&#x00A0;"> ]>
<xsl:stylesheet 
  version="1.0" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
  xmlns:msxml="urn:schemas-microsoft-com:xslt"
  xmlns:umbraco.library="urn:umbraco.library" xmlns:DocCart="urn:DocCart" 
  exclude-result-prefixes="msxml umbraco.library DocCart">
    
<xsl:output method="xml" omit-xml-declaration="yes"/>

<xsl:param name="currentPage"/>

<xsl:template match="/">

  <xsl:variable name="docId" select="number(umbraco.library:Request('docId'))"/>
  <xsl:variable name="action" select="umbraco.library:Request('action')"/>
  
  <xsl:if test="(($docId &gt; 0) and ($action = 'addToCartXslt'))">
    <xsl:value-of select="DocCart:AddToCart($docId)"/>  
  </xsl:if>
  
</xsl:template>

</xsl:stylesheet>