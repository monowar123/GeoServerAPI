using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServerAPI
{
    public class Layer
    {
        RestCommunication rc = new RestCommunication();
        public Layer()
        {
            
        }

        public string GetAllLayers()
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "layers.json");
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetAllLayers(string workspace, string datastore)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores", datastore, "featuretypes.json");
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetLayerDetails(string workspace, string layer)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "layers", workspace + ":" + layer + ".json");
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetLayerDetails(string workspace, string datastore, string layer)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores", datastore, "featuretypes", layer + ".json");
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string AddLayerFromShape(string workspace, string dataStore, string layerName, string nativeName, string styleName)
        {
            string response = string.Empty;
            try
            {
                string status = CheckDuplicate(workspace, dataStore, layerName);
                if (status == "duplicate")
                {
                    return status;
                }
                
                string jsonText = Utilities.GetJsonText("AddLayerFromShape.json", layerName: layerName, workspace: workspace, dataStore: dataStore, nativeName:nativeName);
                
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores", dataStore, "featuretypes");
                rc.Method = HttpVerb.POST;
                rc.PostData = jsonText;
                response = rc.MakeRequest();

                if (styleName != "")
                {
                    ChangeLayerStyle(workspace, dataStore, layerName, styleName);
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string AddLayerFromDb(string workspace, string dataStore, string layerName, string nativeName, string styleName)
        {
            string response = string.Empty;
            string jsonText = string.Empty;
            try
            {
                string status = CheckDuplicate(workspace, dataStore, layerName);
                if (status == "duplicate")
                {
                    return status;
                }
                
                jsonText = Utilities.GetJsonText("AddLayerFromDb.json", layerName: layerName, workspace: workspace, dataStore: dataStore, nativeName: nativeName);

                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores", dataStore, "featuretypes");
                rc.Method = HttpVerb.POST;
                rc.PostData = jsonText;
                response = rc.MakeRequest();

                if (styleName != "")
                {
                    ChangeLayerStyle(workspace, dataStore, layerName, styleName);
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string ChangeLayerStyle(string workspace, string dataStore, string layerName, string styleName)
        {
            string response = string.Empty;
            string jsonText = string.Empty;
            try
            {
                jsonText = Utilities.GetJsonText("ChangeLayerStyle.json", layerName: layerName, workspace: workspace, dataStore: dataStore, styleName: styleName);

                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "layers", workspace + ":" + layerName);
                rc.Method = HttpVerb.PUT;
                rc.PostData = jsonText;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string Edit()
        {
            return "";
        }

        public string Delete(string workspace, string layer)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "layers", workspace + ":" + layer + "?recurse=true");
                rc.Method = HttpVerb.DELETE;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        private string CheckDuplicate(string workspace, string datastore, string layer)
        {
            string message = GetLayerDetails(workspace, datastore, layer);

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
