using System;
using NppScripts;

public class Script : NppScript
{
	
	/**@fun Run()
	 * 	^	This will generate the whole function documentation, and will insert 
	 * 		it where the current cursor is.
	 * 
	 *	@author NoremacSkich | 2014/10/01
	 *
	 */
    public override void Run()
    {
		string documentation = getFunction();
		
		documentation += getAuthor();
				
		documentation += " */" + Environment.NewLine;
		
		// Find the current cursor placement, replace the selection with the
		// dateTime string
		Win32.SendMessage(Npp.CurrentScintilla, SciMsg.SCI_REPLACESEL, 0, documentation);
    }
	
	/**@fun getAuthor()
	 * 	^	This will insert the author line at the current cursor position.
	 * 	N	This will also replace any selected text
	 * 
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
        string dateTime = " *	@author " + author + " | " + finalDate + Environment.NewLine;
        
		dateTime += " * " + Environment.NewLine;
		
		return dateTime;
		
	}
	
	/**@fun getFunction()
	 * 	^	This will parse the function header and return the @fun line with
	 * 		the description line.
	 * 
	 * @return | String
	 * 	^	This is the correctly formatted function string
	 * 	E	/**@fun getFunction()
	 * 		 *	^	
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
		finalString += " *	^	" + Environment.NewLine;
		finalString += " * " + Environment.NewLine;
		// And return the function line
		return finalString;
	}
	
}