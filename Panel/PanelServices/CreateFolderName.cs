using System;
using System.Text.RegularExpressions;

namespace Panel.PanelServices;

public class CreateFolderName
{
    public string Name { get; set; }
     public string  GenerateName(){
                Name =Name.Trim();
                Name=Name.Replace(" ",string.Empty);
                string pattern=@"[<>:""/\\?*]";
                Name=Regex.Replace(Name,pattern,string.Empty);
                return Name;
     }

}
