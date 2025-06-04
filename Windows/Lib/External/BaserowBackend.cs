using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSoTme.OST.Lib.SassySDK.Derived;
using System.Collections.Generic;

namespace SSoTme.OST.Core.Lib.External
{
    public class BaserowBackend
    {
        private readonly string _baseUrl;
        private readonly string _username;
        private readonly string _password;
        private string _authToken;
        private DateTime _tokenExpiry;

        public BaserowBackend(string username, string password)
        {
            _baseUrl = "https://api.baserow.io/api";
            _username = username;
            _password = password;
        }
            
        public BaserowBackend(string runAs = "")
        {
            var key = SSOTMEKey.GetSSoTmeKey(runAs);
            if (!key.APIKeys.ContainsKey("baserow"))
            {
                throw new Exception("Baserow credentials not found. Run: ssotme -setAccountAPIKey=baserow/username/password");
            }

            var baserowConfigJson = key.APIKeys["baserow"];
            var baserowConfig = JsonConvert.DeserializeObject<dynamic>(baserowConfigJson);
            
            _baseUrl = "https://api.baserow.io/api";
            _username = baserowConfig.username;
            _password = baserowConfig.password;
            Console.WriteLine($"New baserowClient instance: {_username}");
        }

        private async Task<string> GetValidTokenAsync()
        {
            // Check if we have a valid token (expires after 60 minutes)
            if (!string.IsNullOrEmpty(_authToken) && DateTime.UtcNow < _tokenExpiry)
            {
                return _authToken;
            }

            // Request new JWT token
            using (var httpClient = new HttpClient())
            {
                var loginData = new { username = _username, password = _password };
                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"Requesting new BaseRow JWT Auth token for {_username}");
                var response = await httpClient.PostAsync($"{_baseUrl}/user/token-auth/", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to authenticate with Baserow: {response.StatusCode} - {responseContent}");
                }
                
                var authResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                _authToken = authResponse.token;
                _tokenExpiry = DateTime.UtcNow.AddMinutes(58); // Refresh 2 minutes before expiry
                
                return _authToken;
            }
        }

        public JToken FetchTablesForBase(string baseId)
        {
            var task = FetchTablesAsync(baseId);
            task.Wait();
            return task.Result;
        }

        private async Task<JToken> FetchTablesAsync(string baseid)
        {
            var token = await GetValidTokenAsync();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
        
                var response = await httpClient.GetAsync($"{_baseUrl}/database/tables/database/{baseid}/");
        
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to fetch Baserow tables: {response.StatusCode} - {errorContent}");
                }

                string strResp = await response.Content.ReadAsStringAsync();
                return JToken.Parse(strResp);
            }
        }

        public JToken GetField(string fieldId)
        {
            var task = GetFieldAsync(fieldId);
            task.Wait();
            return task.Result;
        }

        public async Task<JToken> GetFieldAsync(string fieldId)
        {
            var token = await GetValidTokenAsync();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
        
                var response = await httpClient.GetAsync($"{_baseUrl}/database/fields/{fieldId}/");
        
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to fetch Baserow field: {response.StatusCode} - {errorContent}");
                }

                string strResp = await response.Content.ReadAsStringAsync();
                return JToken.Parse(strResp);
            }
        }
        
        public JObject UpdateField(string fieldId, object schemaChanges)
        {
            var task = UpdateFieldAsync(fieldId, schemaChanges);
            task.Wait();
            return task.Result;
        }

        public async Task<JObject> UpdateFieldAsync(string fieldId, object schemaChanges)
        {
            var token = await GetValidTokenAsync();
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
                
                var json = JsonConvert.SerializeObject(schemaChanges);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await httpClient.PatchAsync($"{_baseUrl}/database/fields/{fieldId}/", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to update Baserow field: {response.StatusCode} - {errorContent}");
                }
                string strResp = await response.Content.ReadAsStringAsync();
                return JObject.Parse(strResp);
            }
        }

        public JToken CreateField(string tableId, string fieldName, string fieldType)
        {
            var task = CreateFieldAsync(tableId, fieldName, fieldType);
            task.Wait();
            return task.Result;
        }

        public async Task<JToken> CreateFieldAsync(string tableId, string fieldName, string fieldType)
        {
            var token = await GetValidTokenAsync();
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
                
                var fieldData = new { name = fieldName, type = fieldType };
                var json = JsonConvert.SerializeObject(fieldData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await httpClient.PostAsync($"{_baseUrl}/database/fields/table/{tableId}/", content);
                Console.WriteLine($"Response: {response.StatusCode} - {response.ReasonPhrase}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to create Baserow field: {response.StatusCode} - {errorContent}");
                }
                string strResp = await response.Content.ReadAsStringAsync();
                return JToken.Parse(strResp);
            }
        }

        public JToken GetTableSchema(string tableId)
        {
            var dataTask = GetTableSchemaAsync(tableId, false);
            dataTask.Wait();
            JToken tableData = dataTask.Result;
            
            var schemaTask = GetTableSchemaAsync(tableId, true);
            schemaTask.Wait();
            JToken tableSchema = schemaTask.Result;
            
            // Transform the data to include field objects with columnName, value, and id
            return TransformTableDataWithFields(tableData, tableSchema);
        }

        public async Task<JToken> GetTableSchemaAsync(string tableId, bool userReadable)
        {
            var token = await GetValidTokenAsync();
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
                var response = await httpClient.GetAsync($"{_baseUrl}/database/rows/table/{tableId}/?user_field_names={userReadable}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to get Baserow table schema: {response.StatusCode} - {errorContent}");
                }
                
                string strRes = await response.Content.ReadAsStringAsync();
                return JToken.Parse(strRes);
            }
        }
        
        private JToken TransformTableDataWithFields(JToken tableData, JToken tableSchema)
        {
            var transformedRows = new JArray();
            
            if (tableData != null && tableData["results"] != null)
            {
                var fields = tableSchema?["fields"]?.ToObject<JArray>();
                var fieldMap = new Dictionary<string, (string name, int id)>();
                
                // Build field mapping from schema
                if (fields != null)
                {
                    foreach (var field in fields)
                    {
                        var fieldId = field["id"]?.ToString();
                        var fieldName = field["name"]?.ToString();
                        if (!string.IsNullOrEmpty(fieldId) && !string.IsNullOrEmpty(fieldName))
                        {
                            fieldMap[$"field_{fieldId}"] = (fieldName, int.Parse(fieldId));
                        }
                    }
                }
                
                foreach (var row in tableData["results"])
                {
                    var transformedRow = new JObject();
                    
                    // Copy basic properties
                    if (row["id"] != null) transformedRow["id"] = row["id"];
                    if (row["order"] != null) transformedRow["order"] = row["order"];
                    
                    // Transform field data
                    foreach (var property in row.ToObject<JObject>().Properties())
                    {
                        if (property.Name.StartsWith("field_") && fieldMap.ContainsKey(property.Name))
                        {
                            var (columnName, fieldId) = fieldMap[property.Name];
                            transformedRow[property.Name] = new JObject
                            {
                                ["columnName"] = columnName,
                                ["value"] = property.Value,
                                ["id"] = fieldId
                            };
                        }
                        else if (!property.Name.Equals("id") && !property.Name.Equals("order"))
                        {
                            // Keep non-field properties as-is
                            transformedRow[property.Name] = property.Value;
                        }
                    }
                    
                    transformedRows.Add(transformedRow);
                }
            }
            
            return new JObject
            {
                ["results"] = transformedRows
            };
        }
    }
}