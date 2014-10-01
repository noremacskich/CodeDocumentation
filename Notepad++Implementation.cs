using System;
using NppScripts;

public class Script : NppScript{
	
	/**@fun Run()
	 *  	^	This will generate the whole function documentation, and will insert 
	 *  		it where the current cursor is.
	 * 
	 * @author NoremacSkich | 2014/10/01
	 *
	 */
    public override void Run(){
		string documentation = getFunction();
		
		documentation += getAuthor();
				
		documentation += " */" + Environment.NewLine;
		
		// Find the current cursor placement, replace the selection with the
		// dateTime string
		Win32.SendMessage(Npp.CurrentScintilla, SciMsg.SCI_REPLACESEL, 0, documentation);
    }
	
	/**@fun getAuthor()
	 *  	^	This will insert the author line at the current cursor position.
	 *  	N	This will also replace any selected text
	 * 
	 * @return | String
	 *  	^	This is the string with the author information
	 *  	E	 * @author NoremacSkich | 2014/10/01
	 *   		 * 
	 * @author NoremacSkich | 2014/10/01
	 * 
	 */

	public string getAuthor(){
		// Create the date type
		DateTime currentDate = new DateTime();
		
		// Get the current date
		currentDate = DateTime.Now;
		
		// Convert the date into the YYYY/MM/DD format
		string finalDate = currentDate.ToString("yyyy/MM/dd");
		
		// Get the author information
		string author = "NoremacSkich";

		// Put it all together in the author line
        string dateTime = " *  	@author " + author + " | " + finalDate + Environment.NewLine;
        
		dateTime += " * " + Environment.NewLine;
		
		return dateTime;
		
	}
	
	/**@fun getFunction()
	 *  	^	This will parse the function header and return the @fun line with
	 *  		the description line.
	 * 
	 * @return | String
	 *  	^	This is the correctly formatted function string
	 *  	E	/**@fun getFunction()
	 *  		 *	^	
	 * 
	 * @author NoremacSkich | 2014/10/01
	 * 
	 */
	public string getFunction(){
		
		// Parse for the function line
		string functionLine = "foo()";
		
		// Put all the info together
		string finalString = " /**@fun " + functionLine + Environment.NewLine;
		
		// This is the description line
		finalString += " *  	^	" + Environment.NewLine;
		finalString += " * " + Environment.NewLine;
		// And return the function line
		return finalString;
	}
	
	/**@fun generateBasicParameter()
	 *  	^	This function will only return the description and the default
	 *  		value if there is any. The rest of the values are up to the 
	 *  		programmer to impliment.
	 * 
	 * @return | String
	 *  	^	The string will have all attributes needed for a parameter
	 *  	^	Refer to the @param documentation for the attributes that this
	 *  		will generate
	 * 
	 * @author NoremacSkich | 2014/10/01
	 *
	 */
	public string generateBasicParameter(){
		/**
		 * @param $var  | Var Type (| Optional)
		 * 	^	What does this variable represent?
		 *	^	If you exceed 80 characters on a line, go to the next line,
		 *		and omit the ^ symbol.
		 *	^	@param is short for @parameter
		 *	(F	This is the format of the string you expect for the function.)
		 *	(E	This will be an example of that format)
		 * 	(D	"This is the default value, if it's a string, use double quotes.")
		 *	(N	This is a special note about this parameter.)
		 */
		
		string optionalParam = "";
		string variable      = "testVar";
		string varType       = "Integer";
		string optional      = ""
		string defaultValue  = "This is the default value, if it's a string, use double quotes.";
		
		string spacer = "";
		
		string defaultLine     = ""
		string paramLine       = "";
		string descriptionLine = "";
		
		
		if(optionalParam){
			optional = " | Optional";
		}
		
		paramLine       = " * @param " + variable + " | " + varType + optional + Environment.NewLine;
		descriptionLine = " *  	^	" + Environment.NewLine;
		spacer          = " * " + Environment.NewLine;
		defaultLine     = " *  	D	" + defaultValue + Environment.NewLine;
		spacer          = " * " + Environment.Newline;
		
		string returnString = ""
		
		returnString += paramLine;
		returnString += descriptionLine;
		returnString += spacer;
		if(optionalParam){
			returnString += defaultLine;
			returnString += spacer;
		}
		
		
		return returnString; 
	}

	/**@fun generateBasicReturn()
	 *  	^	This function will generate a basic return document attribute.
	 *  		The properties of that attribute will be the description, and
	 *  		the default return type, if there is one.
	 * 
	 * @return | String
	 *  	^	The string will have all attributes needed for a basic return
	 * 
	 * @author NoremacSkich | 2014/10/01
	 *
	 */
	public string generateBasicReturn(){
		/**
		 * @return (Var Type)
		 *	^	This is optional.
		 *	(F	This is the format of the output string you expect for the function.)
		 *	(E	This will be an example of the return format.)
		 * 	(D	"This is the default return value, if it's a string, use double 
		 *		quotes.")
		 *	(N	This is a special note about the return type.)
		 */
		
		string defaultReturn = "";
		// This is the return type of the function
		string varType       = "Integer";
		// This will add the optional text after the var type
		string optional      = "";
		// This is the default return type for the function
		string defaultValue  = "This is the default value, if it's a string, use double quotes.";
		
		string spacer = "";
		
		// This is the " *  	D	" line
		string defaultLine     = ""
		// This is the " * @return (Var Type)" line
		string returnLine      = "";
		// This is the " *	^	This is optional." line
		string descriptionLine = "";
		
		
		if(optionalParam){
			optional = " | Optional";
		}
		
		returnLine      = " * @return | " + varType + optional + Environment.NewLine;
		descriptionLine = " *  	^	" + Environment.NewLine;
		spacer          = " * " + Environment.NewLine;
		defaultLine     = " *  	D	" + defaultValue + Environment.NewLine;
		spacer          = " * " + Environment.Newline;
		
		string returnString = ""
		
		returnString += returnLine;
		returnString += descriptionLine;
		returnString += spacer;
		if(optionalParam){
			returnString += defaultLine;
			returnString += spacer;
		}
		
		return returnString; 
	}
}