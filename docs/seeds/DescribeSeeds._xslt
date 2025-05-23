<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

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
                        <xsl:text>../seeds.md</xsl:text>
                    </RelativePath>
                    <xsl:element name="FileContents" xml:space="preserve"># SSoT.me offers the following <xsl:value-of select="count(//Seed)"/> seeds.

### [Home](./)

<xsl:for-each select="//Seed">
## <xsl:value-of select="DisplayName"/>

[Github Repo](https://github.com/SSoTme/<xsl:value-of select="RepoName"/>)

<xsl:value-of select="Description"/>

</xsl:for-each>
</xsl:element>
                </FileSetFile>
            </FileSetFiles>
        </FileSet>
    </xsl:template>
</xsl:stylesheet>
