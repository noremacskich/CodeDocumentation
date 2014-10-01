using System;
using NppScripts;

public class Script : NppScript
{
	
	/**@fun Run()
	 * 	^	This is what will run the script
	 * 
	 *	@author NoremacSkich | 2014/10/01
	 *
	 */
    public override void Run()
    {
		getAuthor();
    }
	
	/**@fun getAuthor()
	 * 	^	This will insert the author line at the current cursor position.
	 * 	N	This will also replace any selected text
	 * 
	 * @author NoremacSkich | 2014/10/01
	 * 
	 */

	public void getAuthor(){
		// Create the date type
		DateTime currentDate = new DateTime();
		
		// Get the current date
		currentDate = DateTime.Now;
		
		// Convert the date into the YYYY/MM/DD format
		string finalDate = currentDate.ToString("yyyy/MM/dd");
		
		// Get the author information
		string author = "NoremacSkich";

		// Put it all together in the author line
        string dateTime = " *	@author " + author + " | " + finalDate;
        
		// Find the current cursor placement, replace the selection with the
		// dateTime string
		Win32.SendMessage(Npp.CurrentScintilla, SciMsg.SCI_REPLACESEL, 0, dateTime);
	}
}