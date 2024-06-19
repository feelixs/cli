<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                >
    <xsl:output method="xml" indent="yes"/>
    <xsl:include href="../CommonXsltTemplates.xslt"/>
    <!--
    
    Access this using:
    
    > ssotme xml-xslt-transform -i ../SSoT/AICPublic.xml -i CreateDesigners.xslt
    
    -->
    <xsl:param name="output-filename" select="'output.txt'" />

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/*">
        <FileSet>
            <FileSetFiles>
                <FileSetFile>
                    <RelativePath>
                        <xsl:text>AICSkills.designer.cs</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve">using System;
using System.Collections.Generic;
using System.Text;

namespace AICapture.OST.Lib.AICapture.DataClasses
{
    public partial class AICSkills
    {
        public enum Enum {
            <xsl:for-each select="//AICSkills/AICSkill"><xsl:if test="position() > 1">,
            </xsl:if><xsl:value-of select="CleanName"/></xsl:for-each>
        }
    // Create an Enum here
    }
}
</xsl:element>
                </FileSetFile>
            </FileSetFiles>
        </FileSet>
    </xsl:template>
</xsl:stylesheet>
