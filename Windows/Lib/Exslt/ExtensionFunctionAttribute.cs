using System;
using System.Reflection;

namespace GotDotNet.Exslt {
	/// <summary>
	/// ExtensionFunctionAttribute for annotating XSLT/XPath extension functions.
	/// </summary>
	public class ExtensionFunctionAttribute : Attribute {
	    private ExsltFunctionNamespace _moduleNS;	    	  	    
	    private string _conformantName;
	    private string _descriptionURL;
	    
		public ExtensionFunctionAttribute(ExsltFunctionNamespace moduleNS) {		
            _moduleNS = moduleNS;            		    			
		}
		
        public ExtensionFunctionAttribute() {}            
		
		/// <summary>
		/// Indicates to which namespace (EXSLT module) function belongs to.
		/// </summary>
		public ExsltFunctionNamespace ModuleNamespace {
		    get { return _moduleNS; }
		    set { _moduleNS = value;}
		}
		
		/// <summary>
		/// Conformant (as per EXSLT definition) function name.
		/// </summary>
		public string ConformantName {
		    get { return _conformantName; }
		    set { _conformantName = value; }
		}
		
		/// <summary>
		/// URL address where the function description can be found.
		/// </summary>
		public string DescriptopnURL {
		    get { return _descriptionURL; }
		    set { _descriptionURL = value; }
		}
	}
}
