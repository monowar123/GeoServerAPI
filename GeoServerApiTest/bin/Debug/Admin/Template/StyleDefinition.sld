<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
  xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
  xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  
<NamedLayer>
<Name>style_bgttype</Name>
<UserStyle>
	<Title>Title: style_bgttype</Title>
<FeatureTypeStyle>
	 <Rule>
       <Name>PND</Name>
       <Title>PND</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>PND</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#B40000</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
	 <Rule>
       <Name>OBW</Name>
       <Title>OBW</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>OBW</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#800080</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
	 <Rule>
       <Name>BTD</Name>
       <Title>BTD</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>BTD</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#97B425</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
	<Rule>
       <Name>OTD</Name>
       <Title>OTD</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>OTD</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#E6BE78</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
   <Rule>
     <Name>WGD</Name>
     <Title>WGD</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>WGD</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#A5A5A5</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
     </PolygonSymbolizer>
  </Rule>
   <Rule>
       <Name>OWG</Name>
       <Title>OWG</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>OWG</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#D6D6D6</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
	 
	  <Rule>
       <Name>WTD</Name>
       <Title>WTD</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>WTD</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#528ED6</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
  
	  <Rule>
       <Name>OWT</Name>
       <Title>OWT</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>OWT</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#DEF7E7</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
	  <Rule>
       <Name>KWD</Name>
       <Title>KWD</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>KWD</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#804000</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
	   <Rule>
       <Name>SHD</Name>
       <Title>SHD</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>SHD</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#FC5227</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
	  <Rule>
       <Name>OBD</Name>
       <Title>OBD</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>OBD</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#000080</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
	 <Rule>
       <Name>OCO</Name>
       <Title>OCO</Title>
       <ogc:Filter>
         <ogc:PropertyIsEqualTo>
           <ogc:PropertyName>bgttype</ogc:PropertyName>
           <ogc:Literal>OCO</ogc:Literal>
         </ogc:PropertyIsEqualTo>
       </ogc:Filter>      
       <PolygonSymbolizer>
         <Fill>
           <CssParameter name="fill">#FFFFFF</CssParameter>
         </Fill>
         <Stroke>
           <CssParameter name="stroke">#000000</CssParameter>
           <CssParameter name="stroke-width">0.5</CssParameter>
         </Stroke>
       </PolygonSymbolizer>
     </Rule>
</FeatureTypeStyle>
</UserStyle>
</NamedLayer>
</StyledLayerDescriptor>