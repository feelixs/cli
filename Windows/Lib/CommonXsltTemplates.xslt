<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:msxml="urn:schemas-microsoft-com:xslt"
    xmlns:ms="http://www.microsoft.com/msxsl"
    exclude-result-prefixes="msxml msxsl ms"
    xmlns:date="urn:sample"
    xmlns:uscdl="uscdl.net">
<!--*****************************
    Project:    Codee42 (ODXML7)
    Created By: EJ Alexandra - 2017
                An Abstract Level, llc
    License:    Mozilla Public License 2.0
    *****************************  -->  


    <!-- example of escaping text elements

     <xsl:template match="/">
        <TestElement>
            <xsl:apply-templates mode="escape"/>
        </TestElement>
    </xsl:template>

    -->

    <msxml:script implements-prefix="ms">
        function range(min, max)
        {
            var dist = max - min + 1;
            return Math.floor(Math.random() * dist + min);
        }

        function uuid()
        {
            return System.Guid.NewGuid();
        }
    </msxml:script>


    <xsl:template name="object-def-type-to-mysql">
        <xsl:param name="object-def-type-name" />
        <xsl:param name="length" select="''" />
        <xsl:choose>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'GUID')">CHAR(36)</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'TEXT') or
                      contains(translate($object-def-type-name, $lcletters, $ucletters), 'STRING')">
                <xsl:choose>
                    <xsl:when test="normalize-space(translate($length, $ucletters, $lcletters)) = 'max'">LONGTEXT</xsl:when>
                    <xsl:when test="$length = -1">LONGTEXT</xsl:when>
                    <xsl:when test="$length = 0">LONGTEXT</xsl:when>
                    <xsl:when test="$length > 0">
                        NVARCHAR(<xsl:value-of select="$length" />)
                    </xsl:when>
                    <xsl:otherwise >NVARCHAR(100)</xsl:otherwise>
                </xsl:choose>
            </xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DATETIME')">DATETIME</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DATE')">DATE</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'TIME')">TIME</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BYTE[]')">VARBINARY(MAX)</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BYTE')">INT</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT16')">INT</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT32')">INT</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BOOLEAN')">BIT</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT64')">BIGINT</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DECIMAL')">DECIMAL(18,2)</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'MONEY')">DECIMAL(18,2)</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'REAL')">DECIMAL(18,2)</xsl:when>
            <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'FLOAT')">FLOAT</xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="object-def-type-name" />
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template match="*" mode="escape">
        <!-- Begin opening tag -->
        <xsl:text>&lt;</xsl:text>
        <xsl:value-of select="name()"/>

        <!-- Namespaces -->
        <xsl:for-each select="namespace::*[. != 'http://www.w3.org/XML/1998/namespace']">
            <xsl:text> xmlns</xsl:text>
            <xsl:if test="name() != ''">
                <xsl:text>:</xsl:text>
                <xsl:value-of select="name()"/>
            </xsl:if>
            <xsl:text>='</xsl:text>
            <xsl:call-template name="escape-xml">
                <xsl:with-param name="text" select="."/>
            </xsl:call-template>
            <xsl:text>'</xsl:text>
        </xsl:for-each>

        <!-- Attributes -->
        <xsl:for-each select="@*">
            <xsl:text> </xsl:text>
            <xsl:value-of select="name()"/>
            <xsl:text>='</xsl:text>
            <xsl:call-template name="escape-xml">
                <xsl:with-param name="text" select="."/>
            </xsl:call-template>
            <xsl:text>'</xsl:text>
        </xsl:for-each>

        <!-- End opening tag -->
        <xsl:text>&gt;</xsl:text>

        <!-- Content (child elements, text nodes, and PIs) -->
        <xsl:apply-templates select="node()" mode="escape" />

        <!-- Closing tag -->
        <xsl:text>&lt;/</xsl:text>
        <xsl:value-of select="name()"/>
        <xsl:text>&gt;</xsl:text>
    </xsl:template>

    <xsl:template match="text()" mode="escape">
        <xsl:call-template name="escape-xml">
            <xsl:with-param name="text" select="."/>
        </xsl:call-template>
    </xsl:template>

    <xsl:template name="upper-camel-case-identifier">
        <xsl:param name="identifier" />
        <xsl:value-of select="translate(substring($identifier, 1, 1), $lcletters, $ucletters)"/>
        <xsl:value-of select="substring($identifier, 2, string-length($identifier))"/>
    </xsl:template>

    <xsl:template name="camel-case-identifier">
        <xsl:param name="identifier" />
        <xsl:value-of select="translate(substring($identifier, 1, 1), $ucletters, $lcletters)"/>
        <xsl:value-of select="substring($identifier, 2, string-length($identifier))"/>
    </xsl:template>

    <xsl:template match="processing-instruction()" mode="escape">
        <xsl:text>&lt;?</xsl:text>
        <xsl:value-of select="name()"/>
        <xsl:text> </xsl:text>
        <xsl:call-template name="escape-xml">
            <xsl:with-param name="text" select="."/>
        </xsl:call-template>
        <xsl:text>?&gt;</xsl:text>
    </xsl:template>

    <xsl:template name="escape-xml">
        <xsl:param name="text"/>
        <xsl:if test="$text != ''">
            <xsl:variable name="head" select="substring($text, 1, 1)"/>
            <xsl:variable name="tail" select="substring($text, 2)"/>
            <xsl:choose>
                <xsl:when test="$head = '&amp;'">&amp;amp;</xsl:when>
                <xsl:when test="$head = '&lt;'">&amp;lt;</xsl:when>
                <xsl:when test="$head = '&gt;'">&amp;gt;</xsl:when>
                <xsl:when test="$head = '&quot;'">&amp;quot;</xsl:when>
                <xsl:when test="$head = &quot;&apos;&quot;">&amp;apos;</xsl:when>
                <xsl:otherwise><xsl:value-of select="$head"/></xsl:otherwise>
            </xsl:choose>
            <xsl:call-template name="escape-xml">
                <xsl:with-param name="text" select="$tail"/>
            </xsl:call-template>
        </xsl:if>
    </xsl:template>



  <xsl:output method="xml" indent="yes"/>

      <xsl:template name="bootstrap-links">
        <!-- Latest compiled and minified CSS -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
        <!-- Optional theme -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
        <!-- Latest compiled and minified JavaScript -->
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    </xsl:template>


    <xsl:template name="csharp-escape-string">
        <xsl:param name="text" />

        <xsl:choose>
            <xsl:when test="string-length(substring-before($text, '&quot;')) > 0 or string-length(substring-after($text, '&quot;')) > 0">
                <xsl:variable name="rest">
                    <xsl:call-template name="csharp-escape-string">
                        <xsl:with-param name="text" select="substring-after($text, '&quot;')" />
                    </xsl:call-template>
                </xsl:variable>
                <xsl:value-of select="concat(substring-before($text, '&quot;'), '\&quot;', $rest)"/>
            </xsl:when>
                <xsl:when test="string-length(substring-before($text, '\')) > 0 or string-length(substring-after($text, '\')) > 0">
                <xsl:variable name="rest">
                    <xsl:call-template name="csharp-escape-string">
                        <xsl:with-param name="text" select="substring-after($text, '\')" />
                    </xsl:call-template>
                </xsl:variable>
                <xsl:value-of select="concat(substring-before($text, '&quot;'), '\\', $rest)"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$text"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

  <xsl:template name="split-lines">
    <xsl:param name="text" />
    <xsl:param name="crlf" select="'&#xd;'" />
    <xsl:choose>
      <xsl:when test="string-length(substring-before($text, $crlf)) > 0">
        <item>
          <xsl:value-of select="substring-before($text, $crlf)" />
        </item>
      </xsl:when>
      <xsl:when test="normalize-space(substring-after($text, $crlf)) != ''" />
      <xsl:when test="normalize-space($text) != ''">
        <item>
          <xsl:value-of  select="$text" ></xsl:value-of>
        </item>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="substring-after($text, $crlf) != ''">
        <xsl:call-template name="split-lines">
          <xsl:with-param name="text" select="substring-after($text, $crlf)" />
        </xsl:call-template>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="get-balsamiq-label-text">
    <xsl:param name="mockup-inputs" />
    <xsl:param name="target-objectdef"  />
    <xsl:param name="target-propertydef" />
    <xsl:variable name="matching-label-text" select="$mockup-inputs//MockupInput[ControlType = 'Label' and TargetObjectDef = $target-objectdef and TargetPropertyDef = $target-propertydef]/Input" />
    <xsl:choose>
      <xsl:when test="normalize-space($matching-label-text) = ''">
        <xsl:value-of select="$target-propertydef"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="html-decode">
          <xsl:with-param name="text" select="$matching-label-text"/>
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>


  <xsl:template name="html-decode">
    <xsl:param name="text" select="''" />
    <xsl:variable name="apos">'</xsl:variable>
    <xsl:variable name="value1">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="$text" />
        <xsl:with-param name="replace" select="'%27'" />
        <xsl:with-param name="by" select="$apos" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="value2">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="$value1" />
        <xsl:with-param name="replace" select="'%20'" />
        <xsl:with-param name="by" select="'&#32;'" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="value3">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="$value2" />
        <xsl:with-param name="replace" select="'%3A'" />
        <xsl:with-param name="by" select="':'" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="value4">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="$value3" />
        <xsl:with-param name="replace" select="'%3F'" />
        <xsl:with-param name="by" select="'?'" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="value5">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="$value4" />
        <xsl:with-param name="replace" select="'%24'" />
        <xsl:with-param name="by" select="'$'" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="value6">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="$value5" />
        <xsl:with-param name="replace" select="'%28'" />
        <xsl:with-param name="by" select="'('" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="value7">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="$value6" />
        <xsl:with-param name="replace" select="'%29'" />
        <xsl:with-param name="by" select="')'" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:value-of select="$value7"/>
  </xsl:template>

  <xsl:template name="get-relationship-name">
    <xsl:param name="objectdef-name" />
    <xsl:param name="include-plural-related-suffix" select="1" />
    <xsl:variable name="related-name" select="../../../../Name" />
    <xsl:variable name="related-property-name" select="../../Name" />
    <xsl:variable name="plural-related">
      <xsl:call-template name="pluralize">
        <xsl:with-param name="word" select="$related-name" />
      </xsl:call-template>
    </xsl:variable>
      <xsl:choose>
        <xsl:when test="$related-property-name = $objectdef-name"></xsl:when>
        <xsl:when test="$related-property-name = concat($objectdef-name, 'Id')"></xsl:when>
        <xsl:when test="string-length(substring-before($related-property-name, $objectdef-name)) > 0">
          <xsl:value-of select="substring-before($related-property-name, $objectdef-name)"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$related-property-name"/>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:if test="$include-plural-related-suffix = 1">
        <xsl:value-of select="$plural-related"/>
      </xsl:if>
  </xsl:template>

  <xsl:template name="remove-spaces">
    <xsl:param name="text" />
    <xsl:if test="string-length($text) > 0">
      <xsl:choose>
        <xsl:when test="string-length($text) = 0"></xsl:when>
        <xsl:when test="substring($text, 1, 1) = ' '"></xsl:when>
        <xsl:when test="substring($text, 1, 1) = '.'"></xsl:when>
        <xsl:when test="substring($text, 1, 1) = '('"></xsl:when>
        <xsl:when test="substring($text, 1, 1) = ')'"></xsl:when>
        <xsl:when test="substring($text, 1, 1) = '-'"></xsl:when>
        <xsl:when test="substring($text, 1, 1) = '_'"></xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="substring($text, 1, 1)"/>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:call-template name="remove-spaces">
        <xsl:with-param name="text" select="substring($text, 2, string-length($text))" />
      </xsl:call-template>
    </xsl:if>
  </xsl:template>

  <xsl:template name="csharp-header">
    <xsl:param name="date" />
    <xsl:param name="author" />
    // ****************************************************
    // This file was derived from the <xsl:value-of select="../../../../Name" /> Specification.
    // Author: <xsl:value-of select="$author" />
    // DO NOT MODIFY THIS FILE - Changes may be overwritten in the future
    // ****************************************************
  </xsl:template>

  <xsl:template name="csharp-user-code-header">
    <xsl:param name="date" />
    <xsl:param name="author" />
    // ****************************************************
    // This file was derived from the <xsl:value-of select="Name" /> Specification.
    // Author: <xsl:value-of select="$author" />
    // ****************************************************
  </xsl:template>

  <xsl:template name="extract-cdata-text">
    <xsl:param name="cdata" />
    <xsl:choose>
      <xsl:when test="substring($cdata, 1, 9) = '&lt;![CDATA['">
        <xsl:value-of select="substring($cdata, 10, string-length($cdata) - 12)"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$cdata"/>
      </xsl:otherwise>
    </xsl:choose>
    
  </xsl:template>


  <xsl:template name="padd-output">
    <xsl:param name="depth" select="4" />
    <xsl:value-of select="' '"/>
    <xsl:if test="$depth - 1 > 0"><xsl:call-template name="padd-output"><xsl:with-param name="depth" select="$depth - 1" /></xsl:call-template></xsl:if>
  </xsl:template>

  <xsl:variable name="lcletters">abcdefghijklmnopqrstuvwxyz/\~!@#$%^&amp;*()_+`1234567890-=[]\{}|;':',.&lt;>?</xsl:variable>
  <xsl:variable name="ucletters">ABCDEFGHIJKLMNOPQRSTUVWXYZ/\~!@#$%^&amp;*()_+`1234567890-=[]\{}|;':',.&lt;>?</xsl:variable>


  <xsl:variable name="symbol-replacements">______________________________________</xsl:variable>
  <xsl:variable name="symbols"            > ~!@#$%^&amp;*()_-+=`,&lt;&gt;/?;:'"[{]}\|</xsl:variable>
  <xsl:variable name="symbols-with-dot"     ><xsl:value-of select="$symbols"  />.</xsl:variable>

  <xsl:template name="pluralize">
    <xsl:param name="word" />
    <xsl:choose>
      <xsl:when test="$word = 'calf'">calves</xsl:when>
      <xsl:when test="$word = 'Calf'">Calves</xsl:when>
      <xsl:when test="$word = 'elf'">elves</xsl:when>
      <xsl:when test="$word = 'Elf'">Elves</xsl:when>
      <xsl:when test="$word = 'half'">halves</xsl:when>
      <xsl:when test="$word = 'Half'">Halves</xsl:when>
      <xsl:when test="$word = 'hoof'">hooves</xsl:when>
      <xsl:when test="$word = 'Hoof'">Hooves</xsl:when>
      <xsl:when test="$word = 'knife'">knives</xsl:when>
      <xsl:when test="$word = 'Knife'">Knives</xsl:when>
      <xsl:when test="$word = 'leaf'">leaves</xsl:when>
      <xsl:when test="$word = 'Leaf'">Leaves</xsl:when>
      <xsl:when test="$word = 'life'">lives</xsl:when>
      <xsl:when test="$word = 'Life'">Lives</xsl:when>
      <xsl:when test="$word = 'loaf'">loaves</xsl:when>
      <xsl:when test="$word = 'Loaf'">Loaves</xsl:when>
      <xsl:when test="$word = 'shelf'">shelves</xsl:when>
      <xsl:when test="$word = 'Shelf'">Shelves</xsl:when>
      <xsl:when test="$word = 'thief'">thieves</xsl:when>
      <xsl:when test="$word = 'Thief'">Thieves</xsl:when>
      <xsl:when test="$word = 'wife'">wives</xsl:when>
      <xsl:when test="$word = 'Wife'">Wives</xsl:when>
      <xsl:when test="$word = 'wolf'">wolves</xsl:when>
      <xsl:when test="$word = 'Wolf'">Wolves</xsl:when>
      <xsl:when test="substring($word, string-length($word), 1) = 'y'"><xsl:value-of select="substring($word, 1, string-length($word) - 1)" />ies</xsl:when>
      <xsl:when test="substring($word, string-length($word), 1) = 's'"><xsl:value-of select="$word" />es</xsl:when>
      <xsl:when test="substring($word, string-length($word), 1) = 'x'"><xsl:value-of select="$word" />es</xsl:when>
      <xsl:otherwise><xsl:value-of select="$word"/>s</xsl:otherwise>
    </xsl:choose>
  </xsl:template>


  <xsl:template name="left-trim">
    <xsl:param name="s" />
    <xsl:choose>
      <xsl:when test="substring($s, 1, 1) = ''">
        <xsl:value-of select="$s"/>
      </xsl:when>
      <xsl:when test="normalize-space(substring($s, 1, 1)) = '' or 
                                      substring($s, 1, 1) = '&#xA;' or 
                                      substring($s, 1, 1) = '&#xD;' or
                                      substring($s, 1, 1) = '_'">
        <xsl:call-template name="left-trim">
          <xsl:with-param name="s" select="substring($s, 2)" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$s" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>


  <xsl:template name="clean-identifier">
    <xsl:param name="currentIdentifier" select="''" />
    <xsl:param name="removeDots" select="false" />
    <xsl:if test="normalize-space($removeDots) = 'false'">
    <xsl:call-template name="string-replace-all">
      <xsl:with-param name="text" select="translate($currentIdentifier, $symbols, $symbol-replacements)"/>
      <xsl:with-param name="replace" select="'_'" />
      <xsl:with-param name="by" select="''" />
    </xsl:call-template>
    </xsl:if>
    <xsl:if test="normalize-space($removeDots) != 'false'">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="translate($currentIdentifier, $symbols-with-dot, $symbol-replacements)"/>
        <xsl:with-param name="replace" select="'_'" />
        <xsl:with-param name="by" select="''" />
      </xsl:call-template>
    </xsl:if>
  </xsl:template>

  <xsl:template name="right-trim">
    <xsl:param name="s" />
    <xsl:choose>
      <xsl:when test="substring($s, 1, 1) = ''">
        <xsl:value-of select="$s"/>
      </xsl:when>
      <xsl:when test="normalize-space(substring($s, 1, 1)) = '' or 
                                      substring($s, 1, 1) = '&#xA;' or 
                                      substring($s, 1, 1) = '&#xD;' or
                                      substring($s, 1, 1) = '_'">
        <xsl:call-template name="right-trim">
          <xsl:with-param name="s" select="substring($s, 1, string-length($s) - 1)" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$s" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="trim">
    <xsl:param name="s" />
    <xsl:call-template name="right-trim">
      <xsl:with-param name="s">
        <xsl:call-template name="left-trim">
          <xsl:with-param name="s" select="$s" />
        </xsl:call-template>
      </xsl:with-param>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="sql-create-table">
    <xsl:param name="uscdlXml" />
    <xsl:param name="tableName" />
    <xsl:param name="channelId" />
    --ACTUAL CREATE TABLE <xsl:value-of select="$tableName"/> (channelId - <xsl:value-of select="$channelId"/>);
    <xsl:for-each select="$uscdlXml//uscdl:DataChannels/uscdl:DataChannel[@Id = $channelId]">
      <xsl:if test="not(uscdl:IsAbstract) or (uscdl:IsAbstract = 'false')">
        IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[<xsl:value-of select="$tableName"/>]') AND type in (N'U'))
        BEGIN
        DROP TABLE[<xsl:value-of select="$tableName"/>];
        END

        GO

        CREATE TABLE [<xsl:value-of select="$tableName"/>] (
        <xsl:for-each select="./uscdl:DataItem/uscdl:Columns/*[uscdl:IsDeclared = 'true']">

          [<xsl:value-of select="*[name() = 'Name']" />] <xsl:call-template name="uscdl-type-to-sql">
            <xsl:with-param name="uscdl-type-name" select="*[name() = 'DataTypeName']" />
          </xsl:call-template> <xsl:if test="* = 'Id'"> NOT NULL DEFAULT newid()</xsl:if>,
        </xsl:for-each>
        RowId INT,
        OptimisticLockField INT,
        CONSTRAINT [PK_<xsl:value-of select="$tableName"/>] PRIMARY KEY CLUSTERED
        (
        [Id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]

        GO
      </xsl:if>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="sql-to-object-def-type">
    <xsl:param name="sql-type-name" />
    <xsl:choose>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'UNIQUEIDENTIFIER')">GUID</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'BIGINT')">INT64</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'NVARCHAR')">NTEXT</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'VARBINARY')">BYTE[]</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'DATETIME')">DATETIME</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'VARCHAR')">TEXT</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'TINYINT')">BYTE</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'NCHAR')">NTEXT</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'TIME')">TIME</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'DATE')">DATE</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'INT')">INT32</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'BIT')">BOOLEAN</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'FLOAT')">FLOAT</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'REAL')">FLOAT</xsl:when>
      <xsl:when test="contains(translate($sql-type-name, $lcletters, $ucletters), 'CHAR')">TEXT</xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$sql-type-name" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="xsd-to-object-def-type">
    <xsl:param name="xsd-type-name" />
    <xsl:choose>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:unsignedbyte')">BYTE</xsl:when>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:unsignedshort')">SHORT</xsl:when>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:unsignedlong')">LONG</xsl:when>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:unsignedint')">INT32</xsl:when>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:guid')">GUID</xsl:when>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:string')">TEXT</xsl:when>
        <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:datetime')">DATETIME</xsl:when>
        <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:gYearMonth')">DATETIME</xsl:when>
        <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:byte')">INT16</xsl:when>
        <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:short')">INT16</xsl:when>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:int32')">INT32</xsl:when>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:int64')">INT64</xsl:when>
      <xsl:when test="contains(translate($xsd-type-name, $ucletters, $lcletters), 'xs:boolean')">BOOLEAN</xsl:when>
      <xsl:otherwise><xsl:value-of select="$xsd-type-name" /></xsl:otherwise>
    </xsl:choose>
  </xsl:template>


  <xsl:template name="object-def-type-to-sql">
    <xsl:param name="object-def-type-name" />
    <xsl:param name="length" select="''" />
    <xsl:choose>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'GUID')">UNIQUEIDENTIFIER</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'TEXT') or
                      contains(translate($object-def-type-name, $lcletters, $ucletters), 'STRING')">NVARCHAR<xsl:choose>
        <xsl:when test="$length = -1">(max)</xsl:when>
        <xsl:when test="$length = 0">(max)</xsl:when>
        <xsl:when test="translate($length, $lcletters, $ucletters) = 'MAX'">(max)</xsl:when>
        <xsl:when test="$length > 0">(<xsl:value-of select="$length" />)</xsl:when>
        <xsl:otherwise>(100)</xsl:otherwise>
      </xsl:choose></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DATETIME')">DATETIME</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DATE')">DATE</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'TIME')">TIME</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BYTE[]')">VARBINARY(MAX)</xsl:when>
        <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BYTE')">INT</xsl:when>
        <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'SHORT')">INT</xsl:when>
        <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT16')">INT</xsl:when>
        <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT32')">INT</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BOOLEAN')">BIT</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT64')">BIGINT</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DECIMAL')">DECIMAL(18,2)</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'MONEY')">DECIMAL(18,2)</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'REAL')">DECIMAL(18,2)</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'FLOAT')">FLOAT</xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="object-def-type-name" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="object-def-type-to-csharp">
    <xsl:param name="object-def-type-name" />
    <xsl:param name="is-nullable" select="'0'" />
      <xsl:param name="is-collection" select="'0'" />
    <xsl:variable name="nullable-symbol-pre"><xsl:if test="normalize-space($is-nullable) = '1'">Nullable&lt;</xsl:if></xsl:variable>
    <xsl:variable name="nullable-symbol"><xsl:if test="normalize-space($is-nullable) = '1'">></xsl:if></xsl:variable>
      <xsl:variable name="collection-symbol">
          <xsl:if test="normalize-space($is-collection) = 1">[]</xsl:if>
      </xsl:variable>
    <xsl:choose>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'GUID')"><xsl:value-of select="$nullable-symbol-pre"/>Guid<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'TEXT')">String<xsl:value-of select="$collection-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'CHAR')">String<xsl:value-of select="$collection-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'XML')">String<xsl:value-of select="$collection-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'NTEXT')">String<xsl:value-of select="$collection-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DATETIME')"><xsl:value-of select="$nullable-symbol-pre"/>DateTime<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DATE')"><xsl:value-of select="$nullable-symbol-pre"/>DateTime<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'TIME')"><xsl:value-of select="$nullable-symbol-pre"/>DateTime<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BYTE[]')">Byte[]</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BYTE')"><xsl:value-of select="$nullable-symbol-pre"/>Byte<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT16')"><xsl:value-of select="$nullable-symbol-pre"/>Int16<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'UBYTE')"><xsl:value-of select="$nullable-symbol-pre"/>UInt16<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT32')"><xsl:value-of select="$nullable-symbol-pre"/>Int32<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'SHORT')"><xsl:value-of select="$nullable-symbol-pre"/>Int32<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BOOLEAN')"><xsl:value-of select="$nullable-symbol-pre"/>Boolean<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT64')"><xsl:value-of select="$nullable-symbol-pre"/>Int64<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DECIMAL')"><xsl:value-of select="$nullable-symbol-pre"/>decimal<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'FLOAT')"><xsl:value-of select="$nullable-symbol-pre"/>float<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'REAL')"><xsl:value-of select="$nullable-symbol-pre"/>decimal<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'CHAR')"><xsl:value-of select="$nullable-symbol-pre"/>char<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'MONEY')"><xsl:value-of select="$nullable-symbol-pre"/>float<xsl:value-of select="$collection-symbol"/><xsl:value-of select="$nullable-symbol"/></xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'IMAGE')">byte[]</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'SQL_VARIANT')">object<xsl:value-of select="$collection-symbol"/></xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="object-def-type-name" />
          <xsl:value-of select="$collection-symbol"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="object-def-type-to-java">
    <xsl:param name="object-def-type-name" />
    <xsl:choose>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'GUID')">UUID</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'TEXT')">String</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'XML')">String</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'NTEXT')">String</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DATETIME')">Time</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BYTE[]')">Byte[]</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT16')">Short</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'UBYTE')">UInt16</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT32')">int</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'BOOLEAN')">Boolean</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'INT64')">Long</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'DECIMAL')">double</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'FLOAT')">float</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'CHAR')">char</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'MONEY')">float</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'IMAGE')">byte[]</xsl:when>
      <xsl:when test="contains(translate($object-def-type-name, $lcletters, $ucletters), 'SQL_VARIANT')">object</xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="object-def-type-name" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="csharp-to-object-def-type">
    <xsl:param name="csharp-type-name" />
    <xsl:choose>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'GUID')">GUID</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'STRING')">TEXT</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'STRING')">XML</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'STRING')">NTEXT</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'DATETIME')">DATETIME</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'BYTE[]')">BYTE[]</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'INT16')">INT16</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'UINT16')">UBYTE</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'INT32')">INT32</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'BOOLEAN')">BOOLEAN</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'INT64')">INT64</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'DECIMAL')">DECIMAL</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'FLOAT')">FLOAT</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'CHAR')">CHAR</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'FLOAT')">MONEY</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'BYTE[]')">IMAGE</xsl:when>
      <xsl:when test="contains(translate($csharp-type-name, $lcletters, $ucletters), 'OBJECT')">SQL_VARIANT</xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="object-def-type-name" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="uscdl-type-to-sql">
    <xsl:param name="uscdl-type-name" />
    <xsl:choose>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.GUID')">UNIQUEIDENTIFIER</xsl:when>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.STRING(MAX)')">NVARCHAR(MAX)</xsl:when>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.STRING')">NVARCHAR(500)</xsl:when>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.DATETIME')">DATETIME</xsl:when>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.BYTE[]')">VARBINARY(MAX)</xsl:when>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.BYTE')">INT</xsl:when>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.INT32')">INT</xsl:when>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.BOOLEAN')">BIT</xsl:when>
      <xsl:when test="contains(translate($uscdl-type-name, $lcletters, $ucletters), 'SYSTEM.INT64')">BIGINT</xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$uscdl-type-name" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="uscdl-type-to-csharp">
    <xsl:param name="uscdl-type-name" />
    <xsl:choose>
      <xsl:when test="contains($uscdl-type-name, 'System.Guid')">System.Guid</xsl:when>
      <xsl:when test="contains($uscdl-type-name, 'System.String(MAX)')">System.String</xsl:when>
      <xsl:when test="contains($uscdl-type-name, 'System.String')">System.String</xsl:when>
      <xsl:when test="contains($uscdl-type-name, 'System.DateTime')">System.DateTime</xsl:when>
      <xsl:when test="contains($uscdl-type-name, 'System.Byte[]')">System.Byte[]</xsl:when>
      <xsl:when test="contains($uscdl-type-name, 'System.Byte')">System.Byte</xsl:when>
      <xsl:when test="contains($uscdl-type-name, 'System.Int32')">System.Int32</xsl:when>
      <xsl:when test="contains($uscdl-type-name, 'System.Boolean')">System.Boolean</xsl:when>
      <xsl:when test="contains($uscdl-type-name, 'System.Int64')">System.Int64</xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$uscdl-type-name" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>


  <xsl:template name="string-replace-all">
    <xsl:param name="text"/>
    <xsl:param name="replace"/>
    <xsl:param name="by"/>
    <xsl:choose>
      <xsl:when test="contains($text,$replace)">
        <xsl:value-of select="substring-before($text,$replace)"/>
        <xsl:value-of select="$by"/>
        <xsl:call-template name="string-replace-all">
          <xsl:with-param name="text" select="substring-after($text,$replace)"/>
          <xsl:with-param name="replace" select="$replace"/>
          <xsl:with-param name="by" select="$by"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="sql-safe">
    <xsl:param name="sql"/>
    <xsl:variable name="replace">'</xsl:variable>
    <xsl:variable name="by">''</xsl:variable>
    <xsl:call-template name="string-replace-all">
      <xsl:with-param name="text" select="$sql" />
      <xsl:with-param name="replace" select="$replace" />
      <xsl:with-param name="by" select="$by" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="format-summary-comments">
    <xsl:param name="summaryComments" />
    <xsl:variable name="newline" select="'&#xa;'" />
    <xsl:variable name="replaceToken" select="concat($newline, '        /// ')" />
    <xsl:variable name="step1">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="concat($newline, $summaryComments)"/>
        <xsl:with-param name="replace" select="$newline" />
        <xsl:with-param name="by" select="$replaceToken" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:value-of select="$step1"/>
  </xsl:template>

  <xsl:template name="format-sql-comments">
    <xsl:param name="summaryComments" />
    <xsl:variable name="newline" select="'&#xa;'" />
    <xsl:variable name="replaceToken" select="concat($newline, '    -- ')" />
    <xsl:variable name="step1">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="concat($newline, $summaryComments)"/>
        <xsl:with-param name="replace" select="$newline" />
        <xsl:with-param name="by" select="$replaceToken" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:value-of select="$step1"/>
  </xsl:template>

  <xsl:template name="strip-dataitem-interface">
    <xsl:param name="type" />
    <xsl:choose>
      <xsl:when test="(substring($type, 1, 1) = 'I') and (substring($type, string-length($type) - 7, 8) = 'DataItem')">
        <xsl:value-of select="substring($type, 2, string-length($type) - 9)"/>
      </xsl:when>
      <xsl:when test="(substring($type, 1, 1) != 'I') and (substring($type, string-length($type) - 7, 8) = 'DataItem')">
        <xsl:value-of select="substring($type, 1, string-length($type) - 8)"/>
      </xsl:when>
      <xsl:when test="(substring($type, 1, 1) = 'I') and (substring($type, string-length($type) - 2, 3) = '128')">
        <xsl:value-of select="substring($type, 2, string-length($type) - 1)"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$type"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="getNamespace">
    <xsl:param name="fqn" />
    <xsl:variable name="firstBlock" select="substring-before($fqn, '.')" />
    <xsl:variable name="remainingNamespace" select="substring-after($fqn, '.')" />
    <xsl:value-of select="$firstBlock"/><xsl:if test="contains($remainingNamespace, '.')">.<xsl:call-template name="getNamespace"><xsl:with-param name="fqn" select="$remainingNamespace"></xsl:with-param></xsl:call-template></xsl:if>
  </xsl:template>

  <xsl:template name="getName">
    <xsl:param name="fqn" />
    <xsl:variable name="firstBlock" select="substring-before($fqn, '.')" />
    <xsl:variable name="remainingNamespace" select="substring-after($fqn, '.')" />
    <xsl:choose><xsl:when test="contains($remainingNamespace, '.')"><xsl:call-template name="getName"><xsl:with-param name="fqn" select="$remainingNamespace"></xsl:with-param></xsl:call-template></xsl:when><xsl:otherwise><xsl:value-of select="$remainingNamespace"/></xsl:otherwise></xsl:choose>
  </xsl:template>


  <xsl:template name="get-next-id">
    <xsl:param name="path-before-number"/>
    <xsl:param name="path-after-number" />
    <xsl:param name="root-path-to-check"/>
    <xsl:variable name="last-id">
      <xsl:call-template name="get-last-id">
        <xsl:with-param name="path-before-number" select="$path-before-number" />
        <xsl:with-param name="path-after-number" select="$path-after-number" />
        <xsl:with-param name="root-path-to-check" select="$root-path-to-check" />
      </xsl:call-template>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="string-length($last-id)>0">
        <xsl:value-of select="$last-id + 1"/>
      </xsl:when>
      <xsl:otherwise>1</xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="get-last-id">
    <xsl:param name="path-before-number"/>
    <xsl:param name="path-after-number" />
    <xsl:param name="root-path-to-check"/>
    <xsl:call-template name="get-last-id-recursive">
      <xsl:with-param name="path-before-number" select="$path-before-number" />
      <xsl:with-param name="path-after-number" select="$path-after-number" />
      <xsl:with-param name="root-path-to-check" select="$root-path-to-check" />
      <xsl:with-param name="id" select="1000" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="get-last-id-recursive">
    <xsl:param name="path-before-number"/>
    <xsl:param name="path-after-number" />
    <xsl:param name="root-path-to-check"/>
    <xsl:param name="id" />
    <xsl:variable name="file-name-to-check" select="concat($path-before-number, concat($id, $path-after-number))"/>
    <xsl:variable name="doc-check" select="document(concat($root-path-to-check,$file-name-to-check))" />
    <xsl:choose>
      <xsl:when test="count($doc-check) > 0">
        <xsl:value-of select="$id" />
      </xsl:when>
      <xsl:when test="$id = 0"></xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="get-last-id-recursive">
          <xsl:with-param name="path-before-number" select="$path-before-number" />
          <xsl:with-param name="path-after-number" select="$path-after-number" />
          <xsl:with-param name="root-path-to-check" select="$root-path-to-check" />
          <xsl:with-param name="id" select="$id - 1" />
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

    <xsl:template name="CamelCase">
        <xsl:param name="text"/>
        <xsl:choose>
            <xsl:when test="contains($text,' ')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,' ')"/>
                </xsl:call-template>
                <xsl:text> </xsl:text>
                <xsl:call-template name="CamelCase">
                    <xsl:with-param name="text" select="substring-after($text,' ')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="$text"/>
                </xsl:call-template>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="CamelCaseWord">
        <xsl:param name="text"/>
        <xsl:value-of select="translate(substring($text,1,1),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
        <xsl:value-of select="translate(substring($text,2,string-length($text)-1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')" />
    </xsl:template>


    <xsl:template name="camel-to-title-case">
      <xsl:param name="text" />
      <xsl:call-template name="title-case">
        <xsl:with-param name="text" select="normalize-space($text)" />
      </xsl:call-template>
    </xsl:template>
  
    <xsl:template name="title-case">
      <xsl:param name="text" />
      <xsl:if test="$text">
        <xsl:variable name="thisletter" select="substring($text,1,1)"/>
        <xsl:choose>
          <xsl:when test="string-length(substring-before(' ABCDEFGHIJKLMNOPQRSTUVWXYZ', $thisletter)) > 0">
            <xsl:value-of select="concat(' ', $thisletter)"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="$thisletter"/>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:call-template name="title-case">
          <xsl:with-param name="text" select="substring($text,2)"/>
        </xsl:call-template>
      </xsl:if>
    </xsl:template>


</xsl:stylesheet>
