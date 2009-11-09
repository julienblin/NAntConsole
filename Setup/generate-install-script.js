// Generates the WiX XML necessary to install a directory tree.
var g_shell = new ActiveXObject("WScript.Shell");
var g_fs = new ActiveXObject("Scripting.FileSystemObject");
if (WScript.Arguments.length != 2)
{
 WScript.Echo("Usage: cscript.exe generate-install-script.js <rootFolder> <outputXMLFile>");
 WScript.Quit(1);
}
var rootDir = WScript.Arguments.Item(0);
var outFile = WScript.Arguments.Item(1);
var baseFolder = g_fs.GetFolder(rootDir);
var componentIds = new Array();

WScript.Echo("Generating " + outFile + "...");

var f = g_fs.CreateTextFile(outFile, true);
f.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
f.WriteLine("<Include>");
f.WriteLine("  <Directory Id=\"TARGETDIR\" Name=\"SourceDir\">");
f.WriteLine("     <Component Id=\"C__6891C999C7354CF2B057F8D038790C04\" Guid=\"6891C999-C735-4CF2-B057-F8D038790C04\">");
f.WriteLine("        <File Id=\"_4CA72857426543EC976535ECB58E4736\" Name=\"CDS.Framework.Tools.NAntConsole.exe\" Source=\"$(var.redist_folder)\\CDS.Framework.Tools.NAntConsole.exe\" Vital=\"yes\" DiskId=\"1\"/>");
f.WriteLine("        <Shortcut Id=\"_AA9547A466CD4DC39E0BB823EA668A7E\" Directory=\"_DF7029BC17464838862EB73755A85474\" Name=\"NAntConsole\" Icon=\"_D707CE1C009F1381803C2C.exe\" IconIndex=\"0\" Show=\"normal\" WorkingDirectory=\"TARGETDIR\" Advertise=\"yes\" />");
f.WriteLine("        <ProgId Id=\"NAntConsole.nantfile\" Description=\"NAntConsole build file\">");
f.WriteLine("          <Extension Id=\"nant\" ContentType=\"application/nant\">");
f.WriteLine("            <Verb Id=\"open\" Command=\"Open\" TargetFile=\"_4CA72857426543EC976535ECB58E4736\" Argument='\"%1\"' />");
f.WriteLine("          </Extension>");
f.WriteLine("        </ProgId>");
f.WriteLine("        <ProgId Id=\"NAntConsole.deployfile\" Description=\"NAntConsole deploy file\">");
f.WriteLine("          <Extension Id=\"deploy\" ContentType=\"application/deploy\">");
f.WriteLine("            <Verb Id=\"install\" Command=\"Install\" TargetFile=\"_4CA72857426543EC976535ECB58E4736\" Argument='\"%1\"' />");
f.WriteLine("            <Verb Id=\"view-install\" Command=\"View install\" TargetFile=\"_4CA72857426543EC976535ECB58E4736\" Argument='\"%1\" \"ViewInstall\"' />");
f.WriteLine("            <Verb Id=\"uninstall\" Command=\"Uninstall\" TargetFile=\"_4CA72857426543EC976535ECB58E4736\" Argument='\"%1\" \"uninstall\"' />");
f.WriteLine("          </Extension>");
f.WriteLine("        </ProgId>");
f.WriteLine("     </Component>");
f.Write(getDirTree(rootDir, "", 1, baseFolder, componentIds));
f.WriteLine("<Component Id=\"C__746C69616E6711D38E0D00C04F6837D0\" Guid=\"{4ECA0796-2D83-4199-B052-4E1D51BC36D0}\" KeyPath=\"yes\"><RemoveFolder Id=\"_DC81F11EF90E4A9099243795C16E83FE\" Directory=\"_DF7029BC17464838862EB73755A85474\" On=\"uninstall\" /></Component><Directory Id=\"ProgramMenuFolder\" SourceName=\"User's Programs Menu\"><Directory Id=\"_DF7029BC17464838862EB73755A85474\" Name=\"CDS\" /></Directory>");
f.WriteLine("  </Directory>");
f.WriteLine("  <Feature Id=\"DefaultFeature\" Level=\"1\" ConfigurableDirectory=\"TARGETDIR\">");
for (var i=0; i<componentIds.length; i++)
{
 f.WriteLine("    <ComponentRef Id=\"C__" + componentIds[i] + "\" />");
}
f.WriteLine("<ComponentRef Id=\"C__6891C999C7354CF2B057F8D038790C04\" />");
f.WriteLine("<ComponentRef Id=\"C__746C69616E6711D38E0D00C04F6837D0\" />");
f.WriteLine("  </Feature>");
f.WriteLine("</Include>");
f.Close();

// recursive method to extract information for a folder
function getDirTree(root, xml, indent, baseFolder, componentIds)
{
 var fdrFolder = null;
 try
 {
  fdrFolder = g_fs.GetFolder(root);
 }
 catch (e)
 {
  return;
 }

 // indent the xml
 var space = "";
 for (var i=0; i<indent; i++)
  space = space + "  ";

 if (fdrFolder != baseFolder)
 {
  var directoryId = "_" + FlatFormat(GetGuid());

  xml = xml + space + "<Directory Id=\"" + directoryId +"\"";
  xml = xml + " Name=\"" + fdrFolder.Name + "\">\r\n";
 }

 var componentGuid = GetGuid();
 var componentId = FlatFormat(componentGuid);


 xml = xml + space + "  <Component Id=\"C__" + componentId + "\""
     + " Guid=\"" + componentGuid + "\">\r\n";

 if (fdrFolder.Files.Count > 0)
 {
  var enumFiles = new Enumerator(fdrFolder.Files);

  for (;!enumFiles.atEnd();enumFiles.moveNext())
  {

   var file = enumFiles.item();
   if(file.Name != "CDS.Framework.Tools.NAntConsole.exe")
   {
	   var fId = "_" + FlatFormat(GetGuid());
	   xml = xml + space + "    <File Id=\"" + fId +
				"\" Name=\"" + file.Name +
				"\" Source=\"$(var.redist_folder)" + file.Path.substring(baseFolder.Path.length) +
				"\" Vital=\"yes" +
				"\" DiskId=\"1\"/>\r\n";
   }
  }
 }
 else
 {
  xml = xml + space + "    <CreateFolder />\r\n";
 }

 xml = xml + space + "  </Component>\r\n";

 componentIds[componentIds.length] = componentId;


 var enumSubFolders = new Enumerator(fdrFolder.SubFolders);

 var depth = indent + 1;
 for (;!enumSubFolders.atEnd();enumSubFolders.moveNext())
 {
  var subfolder = enumSubFolders.item();
  xml = getDirTree(enumSubFolders.item().Path, xml, depth, baseFolder, componentIds);
 }

 if (fdrFolder != baseFolder)
 {
  xml = xml + space + "</Directory>\r\n";
 }

 return xml;
}

// Generate a new GUID
function GetGuid()
{
 return new ActiveXObject("Scriptlet.Typelib").Guid.substr(1,36).toUpperCase();
}

// Convert a GUID from this format
//   7E70E5E5-CE19-4270-A740-223A09796433
// to this format:
//   7E70E5E5CE194270A740223A09796433
function FlatFormat(guid)
{
 return guid.replace(/-/g, "");
}