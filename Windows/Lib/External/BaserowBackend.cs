using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSoTme.OST.Lib.SassySDK.Derived;

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
            Console.WriteLine($"New baserowClient instance: {_username}: {_password}");
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

        public string FetchTablesForBase(string baseId)
        {
            var task = FetchTablesAsync(baseId);
            task.Wait();
            return task.Result;
        }

        private async Task<string> FetchTablesAsync(string baseid)
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

                return await response.Content.ReadAsStringAsync(); // Return the response content
            }
        }
        
        public void UpdateTable(string tableId, object schemaChanges)
        {
            var task = UpdateTableAsync(tableId, schemaChanges);
            task.Wait();
        }

        public async Task UpdateTableAsync(string tableId, object schemaChanges)
        {
            var token = await GetValidTokenAsync();
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
                
                var json = JsonConvert.SerializeObject(schemaChanges);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await httpClient.PatchAsync($"{_baseUrl}/database/tables/{tableId}/", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to update Baserow table: {response.StatusCode} - {errorContent}");
                }
            }
        }

        public void CreateField(string tableId, string fieldName, string fieldType)
        {
            var task = CreateFieldAsync(tableId, fieldName, fieldType);
            task.Wait();
        }

        public async Task CreateFieldAsync(string tableId, string fieldName, string fieldType)
        {
            var token = await GetValidTokenAsync();
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
                
                var fieldData = new { name = fieldName, type = fieldType };
                var json = JsonConvert.SerializeObject(fieldData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await httpClient.PostAsync($"{_baseUrl}/database/fields/table/{tableId}/", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to create Baserow field: {response.StatusCode} - {errorContent}");
                }
            }
        }

        public string GetTableSchema(string tableId)
        {
            var task = GetTableSchemaAsync(tableId);
            task.Wait();
            return task.Result;
        }

        public async Task<string> GetTableSchemaAsync(string tableId)
        {
            var token = await GetValidTokenAsync();
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
                
                var response = await httpClient.GetAsync($"{_baseUrl}/database/tables/{tableId}/");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to get Baserow table schema: {response.StatusCode} - {errorContent}");
                }
                
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}