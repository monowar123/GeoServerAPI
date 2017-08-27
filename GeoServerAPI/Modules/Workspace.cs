using Newtonsoft.Json;
using System;
using System.IO;

namespace GeoServerAPI
{
    public class Workspace
    {
        RestCommunication rc = new RestCommunication();
        public Workspace()
        {         
        }

        public string Get()
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces.json");
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string Get(string workspace)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace + ".json");
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetNamespaceUri(string workspace)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "namespaces", workspace + ".json");
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();

                var namespaceObject = JsonConvert.DeserializeObject<dynamic>(response);
                if (Global.IsPropertyExists(namespaceObject, "namespace"))
                {
                    response = namespaceObject["namespace"].uri;
                }
                else
                {
                    throw new Exception("Namespace not found");
                }
            }
            catch (JsonException)
            {
                throw new Exception("Problem in deserialize json object from GetNamespaceUri()");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return response;
        }
        
        public string Add(string workspace) // POST
        {
            string response = string.Empty;
            try
            {
                string status = CheckDuplicate(workspace);
                if (status == "duplicate")
                {
                    return status;
                }

                string jsonText = Utilities.GetJsonText("AddWorkspace.json", workspace);

                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "namespaces.json");
                rc.Method = HttpVerb.POST;
                rc.PostData = jsonText;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string EditName(string workspace, string newWorkspace) // PUT
        {
            string response = string.Empty;
            //try
            //{
            //    string jsonText = Utilities.GetJsonText("abc.json", newWorkspace);

            //    rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace + ".json");
            //    rc.Method = HttpVerb.PUT;
            //    rc.PostData = jsonText;
            //    response = rc.MakeRequest();
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message.ToString();
            //}

            return response;
        }

        public string Delete(string workspace)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace + "?recurse=true");
                rc.Method = HttpVerb.DELETE;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        private string CheckDuplicate(string workspace)
        {
            string message = Get(workspace);
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
