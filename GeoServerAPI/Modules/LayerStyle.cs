using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace GeoServerAPI
{
    public class LayerStyle
    {
        RestCommunication rc = new RestCommunication();
        public LayerStyle()
        {
                  
        }

        public string GetCommonStyle()
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "styles.json");
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetStyles(string workspace)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "styles.json");
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetAllStyle()
        {
            string response = string.Empty;

            try
            {
                //get the object of common style json
                response = GetCommonStyle();
                var commonStyleObject = JsonConvert.DeserializeObject<dynamic>(response);
                foreach (var record in commonStyleObject.styles.style)
                {
                    record.href = "";
                }
        
                //get all workspace and iterate to find their style
                Workspace wpk = new Workspace();
                string wpkJson = wpk.Get();
                var wpkObject = JsonConvert.DeserializeObject<dynamic>(wpkJson);
                foreach (var record in wpkObject.workspaces.workspace)
                {
                    string temp = GetStyles(Convert.ToString(record.name));
                    if (temp != "{\"styles\":\"\"}")
                    {
                        var styleObject = JsonConvert.DeserializeObject<dynamic>(temp);
                        foreach (var style in styleObject.styles.style)
                        {
                            style.name = record.name + ":" + style.name;
                            style.href = record.name;
                            commonStyleObject.styles.style.Add(style);
                        }                     
                    }
                }
                response = JsonConvert.SerializeObject(commonStyleObject);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetStyleDetails(string workspace, string styleName)
        {
            string response = string.Empty;
            try
            {             
                if (workspace != "")
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "styles", styleName + ".json");
                }
                else
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "styles", styleName + ".json");
                }
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetStyleFile(string workspace, string styleName)
        {
            string fileAddress = string.Empty;

            if (workspace != "")
            {
                fileAddress = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "styles", styleName + ".sld");
            }
            else
            {
                fileAddress = Path.Combine(Global.API_BASE_URL, "styles", styleName + ".sld");
            }

            return fileAddress;
        }

        public string GetStyleFileContent(string workspace, string styleName)
        {
            string fileContent = string.Empty;
            try
            {
                if (workspace != "")
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "styles", styleName + ".sld");
                }
                else
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "styles", styleName + ".sld");
                }
                rc.Method = HttpVerb.GET;
                fileContent = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return fileContent;
        }

        public string AddNewStyle(string workspace, string styleName, string styleFileName, string styleWorkspace)
        {
            string response = string.Empty;
            string jsonText = string.Empty;          
            try
            {
                string status = CheckDuplicate(workspace, styleName);
                if (status == "duplicate")
                {
                    return status;
                }

                jsonText = Utilities.GetJsonText("AddStyle.json", workspace: workspace, styleName: styleName);

                if (workspace != "")
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "styles");
                }
                else
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "styles");
                }
                rc.Method = HttpVerb.POST;
                rc.PostData = jsonText;

                response = rc.MakeRequest();  

                if (styleFileName != "")
                {
                    string fileContent = GetStyleFileContent(styleWorkspace, styleFileName);
                    string secondResponse = EditStyle(workspace, styleName, fileContent);
                    if (secondResponse != "OK")
                    {
                        return "Style created but error in sld file creation.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }      

        public string AddStyleFromLocalFile(string workspace, string styleName, string styleFileContent)
        {
            string response = string.Empty;
            string jsonText = string.Empty;
            try
            {
                string status = CheckDuplicate(workspace, styleName);
                if (status == "duplicate")
                {
                    return status;
                }

                jsonText = Utilities.GetJsonText("AddStyle.json", workspace: workspace, styleName: styleName);

                if (workspace != "")
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "styles");
                }
                else
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "styles");
                }
                rc.Method = HttpVerb.POST;
                rc.PostData = jsonText;

                response = rc.MakeRequest();

                if (styleFileContent != "")
                {
                    string secondResponse = EditStyle(workspace, styleName, styleFileContent);
                    if (secondResponse != "OK")
                    {
                        return "Style created but sld file is not valid";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string EditStyle(string workspace, string styleName, string styleFileContent)
        {
            string response = string.Empty;
            try
            {              
                if (workspace != "")
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "styles", styleName);
                }
                else
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "styles", styleName);
                }
                rc.Method = HttpVerb.PUT;
                rc.PostData = styleFileContent;
                rc.ContentType = "application/vnd.ogc.sld+xml";
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string CopyStyle(string workspace, string styleName, string styleFileName, string styleWorkspace)
        {
            string response = string.Empty;
            try
            {
                if (styleFileName != "")
                {
                    string fileContent = GetStyleFileContent(styleWorkspace, styleFileName);
                    response = EditStyle(workspace, styleName, fileContent);
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string DeleteStyle(string workspace, string styleName)
        {
            string response = string.Empty;
            try
            {
                if (workspace != "")
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "styles", styleName);
                }
                else
                {
                    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "styles", styleName);
                }
                rc.Method = HttpVerb.DELETE;
             
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        private string CheckDuplicate(string workspace, string styleName)
        {
            string message = GetStyleDetails(workspace, styleName);
            if (message.IndexOf("(404) Not Found") != -1)
            {
                return "unique";
            }
            else
            {
                return "duplicate";
            }
        }
    }
}
