using System;
using System.IO;
using System.Text;

namespace GeoServerAPI
{
    public class DataStore
    {
        RestCommunication rc = new RestCommunication();
        public DataStore()
        {           
        }

        public string Get(string workspace)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores.json");
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string Get(string workspace, string dataStore)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores", dataStore + ".json");
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string GetResources(string path)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "resource/" + path + "?format=json");
                rc.Method = HttpVerb.GET;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string AddStoreFromDb(string workspace, string dataStore, string dbName)
        {      
            string response = string.Empty;
            string jsonText = string.Empty;
            try
            {
                string status = CheckDuplicate(workspace, dataStore);
                if (status == "duplicate")
                {
                    return status;
                }

                string hostName = Utilities.GetAppConfigValue("Host");
                string port = Utilities.GetAppConfigValue("Port");
                string userId = Utilities.GetAppConfigValue("User_Id");
                string password = Utilities.GetAppConfigValue("Password");
                string namespace_uri = string.Empty;

                Workspace wpkObject = new Workspace();
                namespace_uri = wpkObject.GetNamespaceUri(workspace);

                jsonText = Utilities.GetJsonText("AddStoreFromDb.json", workspace, dataStore);
                jsonText = jsonText.Replace("#host_name#", hostName).Replace("#port#", port).Replace("#user_id#", userId).Replace("#password#", password).Replace("#db_name#", dbName);
                jsonText = jsonText.Replace("#namespace_uri#", namespace_uri);

                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores");
                rc.Method = HttpVerb.POST;
                rc.ContentType = "application/json";
                rc.PostData = jsonText;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;   
        }

        public string AddStoreFromShape(string workspace, string dataStore, string shapeFileName)
        {
            string response = string.Empty;
            try
            {
                string namespace_uri = string.Empty;
                string status = CheckDuplicate(workspace, dataStore);
                if (status == "duplicate")
                {
                    return status;
                }

                Workspace wpkObject = new Workspace();
                namespace_uri = wpkObject.GetNamespaceUri(workspace);

                string jsonText = Utilities.GetJsonText("AddStoreFromShape.json", workspace, dataStore, shapeFileName: shapeFileName);
                jsonText = jsonText.Replace("#namespace_uri#", namespace_uri);

                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores");
                rc.Method = HttpVerb.POST;
                rc.ContentType = "application/json";
                rc.PostData = jsonText;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string ChangeShapeFileName(string workspace, string dataStore, string shapeFileName)
        {
            string response = string.Empty;
            try
            {
                string jsonText = Utilities.GetJsonText("AddDataStore.json", workspace, dataStore, shapeFileName: shapeFileName);

                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores", dataStore);
                rc.Method = HttpVerb.PUT;
                rc.ContentType = "application/json";
                rc.PostData = jsonText;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string UploadShapeFile(string workspace, string dataStore, string filePath)
        {
            string response = string.Empty;
            try
            {
                string status = CheckDuplicate(workspace, dataStore);
                if (status == "duplicate")
                {
                    return status;
                }

                byte[] shapeFile = readLocalShapeFile(filePath);

                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores", dataStore, "file.shp");
                rc.Method = HttpVerb.PUT;
                rc.ContentType = "application/zip";
                response = rc.MakeRequest(shapeFile);

                if (response == "Created" || response == "Accepted")
                {
                    Delete(workspace, dataStore);
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        public string Edit() // PUT
        {
            // Not implemented yet.
            return "";
        }

        public string Delete(string workspace, string dataStore)
        {
            string response = string.Empty;
            try
            {
                rc.EndPoint = Path.Combine(Global.API_BASE_URL, "workspaces", workspace, "datastores", dataStore + "?recurse=true");
                rc.Method = HttpVerb.DELETE;
                response = rc.MakeRequest();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return response;
        }

        private string CheckDuplicate(string workspace, string dataStore)
        {
            string message = Get(workspace, dataStore);

            if (message.IndexOf("(404) Not Found") != -1)
            {
                return "unique";
            }
            else
            {
                return "duplicate";
            }
        }

        private byte[] readLocalShapeFile(string filePath)
        {
            byte[] buffer;
            char[] temp;
            FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fStream.Length;
                buffer = new byte[length];
                temp = new char[length];
                int count;
                int sum = 0;

                // Read until Read method returns 0 - End of stream reached
                while ((count = fStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;           
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                fStream.Close();
            }

            return buffer;
        }
    }
}
