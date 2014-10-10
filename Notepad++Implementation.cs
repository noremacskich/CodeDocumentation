using System;
using System.Text;
using NppScripts;

public class Script : NppScript{
	
	
	/**@fun int GetCaretLine()
	 *  	^	This will get the current line number that the cursor is on.
	 * 
	 *	@return  | Integer
	 *		^	This will return the current line number that the cursor is on.
	 *
	 *	@author NoremacSkich | 2014/10/09
	 * 
	 */
	int GetCaretLine(){
        var sci = Npp.CurrentScintilla;
        int currentPos = (int)Win32.SendMessage(sci, SciMsg.SCI_GETCURRENTPOS, 0, 0);
        return (int)Win32.SendMessage(sci, SciMsg.SCI_LINEFROMPOSITION, currentPos, 0);
    }
	
	/**@fun public void printLine(string line)
	 *  	^	This will insert the input where the cursor is.
	 * 
	 *	@param line | string
	 *		^	This is the input that will be put where the current cursor.
	 *
	 *	@author NoremacSkich | 2014/10/09
	 * 
	 */
	public void printLine(string line){
		// Find the current cursor placement, replace the selection with the
		// dateTime string
		Win32.SendMessage(Npp.CurrentScintilla, SciMsg.SCI_REPLACESEL, 0, line);
	}
	

	/**@fun public int indentLevel(string line)
	*  	^	This will return how many levels of indentation there are.
	* 	N	This will be based on the number of tabs on the current line.
	* 
	*	@param line | string
	*		^	This is a copy of the line the function resides on
	*
	*	@return | Integer
	*		^	This is the number of tabs that is in the line
	*		N	It is assumed that all tabs are at the start of line.
	*
	*	@author NoremacSkich | 2014/10/09
	* 
	*/
	public int indentLevel(string line){
		
		return line.Split('\t').Length - 1;
	}
	
	/**@fun Run()
	 *  	^	This will generate the whole function documentation, and will insert 
	 *  		it where the current cursor is.
	 * 
	 * @author NoremacSkich | 2014/10/01
	 * @modified NoremacSkich | 2014/10/09
	 *
	 */
    public override void Run(){
		//int lineNum= 30;
		
		int indentToLevel = indentLevel( GetLine( GetCaretLine() ) );
		
		string indent = new String('\t', 5);
		
		string documentation = getFunction();
		
		
		// This is the process for getting parameters
		string parameters = getParameters( GetLine( GetCaretLine() ) );
		
		string [] paramArray = parameters.Split(new Char[] {','});
		
		foreach(string param in paramArray){
			
			// Determine the parameter name
			string paramName = "";
			
			// this determines the optional state
			bool optionalParam = false;
			
			// Determine the Parameter Type
			string paramType = "";
			
			
			if(param.IndexOf('=') >=0 ){
				
				optionalParam = true;
			}

			// Get rid of padding around text
			//param = param;
			
			// Then, lets split up the parameter with spaces
			string [] paramSplit = param.Trim().Split(new Char[] {' '});
			
			//printLine(paramSplit.Length.ToString());
			// First lets see if there is only one word
			if(paramSplit.Length == 1){
				
				// We will assume that this is the the variable name
				paramName = paramSplit[0];
				
			}else{
				
				// Assume the first one is refering to parameter type
				paramType = paramSplit[0];
				
				// And that the second one is the variable name
				paramName = paramSplit[1];
				
				//foreach(string subParam in paramSplit){
					// 
					
					// Seperate out the visibility, parameter name, and 
					// default value
					
				//}
			}
			
			documentation += generateBasicParameter( paramName, paramType, optionalParam);
		}
		
		//for(int i = 0; i<20; i++){
		//printLine(GetLine(GetCaretLine()));
		//}
			
		documentation += getAuthor();
				
		documentation += " */" + Environment.NewLine;
		
		documentation.Replace("\n\r", ("\n\r" + indent) );
		
		
		printLine(documentation);
		
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
	 * @modified NoremacSkich | 2014/10/09
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
        string dateTime = " *	@author " + author + " | " + finalDate + Environment.NewLine;
        
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
	 *	@param paramName | string
	 *		^	This is the name of the parameter
	 *
	 *	@param paramType | string
	 *		^	This is the parameter type
	 *		E	Integer, String, etc
	 *
	 *	@param optionalParam | bool
	 *		T	The string " | Optional" will be displayed
	 *		F	The string will not be displayed
	 * 
	 * @return | String
	 *  	^	The string will have all attributes needed for a parameter
	 *  	^	Refer to the @param documentation for the attributes that this
	 *  		will generate
	 * 
	 * @author NoremacSkich | 2014/10/01
	 *
	 */
	public string generateBasicParameter(string paramName, string paramType, bool optionalParam){
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
		
		//bool optionalParam = true;
		//string variable      = "testVar";
		//string varType       = "Integer";
		string optionalString      = "";
		string defaultValue        = "This is the default value, if it's a string, use double quotes.";
		
		string spacer          = "";
		
		string defaultLine     = "";
		string paramLine       = "";
		string descriptionLine = "";
		
		
		if(optionalParam){
			optionalString = " | Optional";
		}
		
		paramLine       = " *	@param " + paramName + " | " + paramType + optionalString + Environment.NewLine;
		descriptionLine = " *		^	" + Environment.NewLine;
		spacer          = " *" + Environment.NewLine;
		defaultLine     = " *		D	" + defaultValue + Environment.NewLine;
		spacer          = " *" + Environment.NewLine;
		
		string returnString = "";
		
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
		
		//string defaultReturn = "";
		// This is the return type of the function
		string varType       = "Integer";
		// This will add the optional text after the var type
		string optional      = "";
		// This is the default return type for the function
		string defaultValue  = "This is the default value, if it's a string, use double quotes.";
		
		string spacer = "";
		
		// This is the " *  	D	" line
		string defaultLine     = "";
		// This is the " * @return (Var Type)" line
		string returnLine      = "";
		// This is the " *	^	This is optional." line
		string descriptionLine = "";
				
		returnLine      = " * @return | " + varType + optional + Environment.NewLine;
		descriptionLine = " *  	^	" + Environment.NewLine;
		spacer          = " * " + Environment.NewLine;
		defaultLine     = " *  	D	" + defaultValue + Environment.NewLine;
		spacer          = " * " + Environment.NewLine;
		
		string returnString = "";
		
		returnString += returnLine;
		returnString += descriptionLine;
		returnString += spacer;
		
		return returnString; 
	}
	
	/**@fun GetLine(int line)
	 *  	^	This will grab the contents of the provided line number.
	 * 
	 * @param line | Integer
	 *  	^	This is the line number to grab the contents from
	 * 
	 * @return | String
	 *  	^	This will return the line contents
	 * 
	 * @author NoremacSkich | 2014/10/01
	 *
	 */
	string GetLine(int line){
		IntPtr sci = Npp.CurrentScintilla;
		
		int length = (int)Win32.SendMessage(sci, SciMsg.SCI_LINELENGTH, line, 0);
		var buffer = new StringBuilder(length + 1);
		Win32.SendMessage(sci, SciMsg.SCI_GETLINE, line, buffer);
		buffer.Length = length;        //NPP may inject some rubbish at the end of the line
		return buffer.ToString();
    }
	
	/**@fun getParameters(string parameterList)
	 *  	^	This will check for parameters the parameter list, and will return 
	 * 			the entire list if found
	 * 
	 * @param parameterList | String
	 *  	^	This is the string that will be parsed to find the parameter list
	 * 
	 * @return | String
	 *  	^	This will return the parameter list
	 *  	N	This function will return string.empty if the line doesn't have
	 *  		anything but whitespaces.  
	 *  	N	It will also return String.Empty if it can't find the starting
	 *  		parameter string or the ending parameter string.
	 * 
	 * @todo NoremacSkich | 2014/10/09
	 *  	^	Need to figure out how to deal with the exception when the 
	 *  		string.Substring(int1, int2) fails
	 * 
	 * @author NoremacSkich | 2014/10/09
	 *
	 */
	string getParameters(string parameterList){
		
		
		// Lets define the starting and ending strings of the parameter list
		string parameterStart = "(";
		string parameterEnd = ")";
		
		// Check to see if string is null, empty or has white space characters
		if(string.IsNullOrWhiteSpace(parameterList)){
		
			// The string is null or has whitspaces, return an empty string
			printLine("WhiteSpace");
			return String.Empty;
		}
		
		// Next check to see if the characters defining the starting and ending
		// of the parameter list is within the provided string
		if(!parameterList.Contains(parameterStart) && !parameterList.Contains(parameterEnd)){
			
			printLine("no " + parameterStart + " " + parameterEnd);
			// Then this has no parameters, return an empty string
			return String.Empty;
		}
		
		// Next, lets get the locations of both the parameter start and end 
		// end symbols
		
		// Get the starting number of the param list (zero based)
		int startList = parameterList.IndexOf(parameterStart);
		
		// Get the position of the closing parantheses, starting after the
		// position found in the start list. (zero based)
		int endList = parameterList.IndexOf(parameterEnd, startList);
		// Will return -1 if not found, but shouldn't happen since we already
		// checked for that
		
		
		// Now that we know where the parameter list is located, lets try to
		// find the number of parameters
		
		// First lets get everything between the start and end of the parameter
		// list, excluding those characters
		
		
		// This is the true start of the parameter list.  Adding how ever many 
		// characters compose the start of the parameter list
		int startParamList = startList + parameterStart.Length;
		
		// This is the true ending of the parameter list.  Don't need to subtract
		// anything because the indexOf returns the starting position of the 
		// string match
		int endParamList = endList;
		string fullParamList = String.Empty;
		try{
			// Lets grab that substring
			fullParamList = parameterList.Substring(startParamList, endParamList - startParamList);
			// returns empty if startParamList is equal to parameterList.Length and
			// endParamList is 0

		}catch(ArgumentOutOfRangeException outOfRange){
			// Throws ArgumentOutOfRangeException if startParamList + endParamList 
			// is longer than the string 
			// Or if either startParamList or EndParamList are less than zero
			printLine("out of range " + startParamList + " " + (endParamList - startParamList));
		}
		//printLine(fullParamList);
		// We can now return the parameter list
		return fullParamList;
	}
}