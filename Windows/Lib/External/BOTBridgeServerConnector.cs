using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SSoTme.OST.Core.Lib.External;

public class BOTBridgeServerConnector
{
    private readonly BaserowBackend _baserowClient = new BaserowBackend();
    private readonly string _microSoftTenantId;
    private readonly string _baseId;
    
    public readonly string baseCopilotUri;
    public readonly string copilotReadUri;
    
    public BOTBridgeServerConnector(string microsoftUUID, string uri, string baseId)
    {
        // get baserow client from ~/.ssotme/ssotme.key file -> "baserow" api
        _baserowClient.InitFromHomeFile();
        _microSoftTenantId = microsoftUUID;
        _baseId = baseId;
        
        baseCopilotUri = uri;
        copilotReadUri = $"{baseCopilotUri}/check-req-actions?baseId={baseId}";;
    }
    
    public (bool, string, string) GetLastCopilotRequestedRead()
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("X-Microsoft-TenantID", _microSoftTenantId);
            string response = httpClient.GetStringAsync(copilotReadUri).Result;
            var json = JsonDocument.Parse(response);
            var changedVal = json.RootElement.GetProperty("changed").GetRawText();
            bool changed = changedVal == "true";
                
            string timestamp = "0";
            if (changed) {
                timestamp = json.RootElement.GetProperty("timestamp").GetString();
                if (string.IsNullOrEmpty(timestamp)) { Console.WriteLine($"WARN: Received null timestamp in response: {response}"); }
            }
            return (changed, response, timestamp);
        }
    }
    
    public JToken PostAvailableBases()
    {
        JToken availableBases = _baserowClient.GetAvailableBases();

        try
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Microsoft-TenantID", _microSoftTenantId);
                var payload = new JObject
                {
                    ["bases"] = availableBases
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine($"Posting available bases: {availableBases.ToString(Formatting.None)} for user: {_microSoftTenantId}");
                var response = httpClient.PostAsync($"{baseCopilotUri}/available-bases", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Failed to post bases. Status: {response.StatusCode}, Error: {errorContent}");
                    return null;
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return availableBases;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error posting available user bases to Bridge API: {ex.Message}");
            return null;
        }
    }

    public void SetIsBuilding(bool value)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var payload = new JObject
                {
                    ["val"] = value
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync($"{baseCopilotUri}/base-is-building?baseId={_baseId}", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Failed to post isBuilding. Status: {response.StatusCode}, Error: {errorContent}");
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"isBuilding posted successfully: {result}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error posting isBuilding to bridge: {ex.Message}");
        }
    }
    
    public void PostChange(string uri)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($"{uri}/mark?baseId={_baseId}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Failed to post change. Status: {response.StatusCode}, Error: {errorContent}");
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Change posted successfully: {result}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error posting change to listener API: {ex.Message}");
        }
    }
    
    public bool CurrentBaseIdIsAvailable(JToken userBases)
    {
        if (string.IsNullOrEmpty(_baseId) || userBases == null)
        {
            return false;
        }

        // userBases is expected to be a JArray or a JToken containing a "results" array
        JArray basesArray = userBases.Type == JTokenType.Array
            ? (JArray)userBases
            : userBases["results"] as JArray;

        if (basesArray == null)
        {
            Console.WriteLine("Could not find a list of bases in provided JToken.");
            return false;
        }

        foreach (var baseToken in basesArray)
        {
            var id = baseToken["id"]?.ToString();
            if (!string.IsNullOrEmpty(id) && id == _baseId)
            {
                return true;
            }
        }

        return false;
    }
    
    public string PostDataToBridge(JToken data, string dataTimestamp, bool success)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("X-Microsoft-TenantID", _microSoftTenantId);
            try
            {
                // Add timestamp to the data
                data["timestamp"] = dataTimestamp;
                data["success"] = success;
                
                var jsonString = data.ToString(Newtonsoft.Json.Formatting.None);
                // Console.WriteLine($"Posting to bridge: {uri} - {data}");
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync($"{baseCopilotUri}/put-action-result?baseId={_baseId}", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    Console.WriteLine($"Error posting data to bridge: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception posting data to bridge: {ex.Message}");
                return null;
            }
        }
    }
    
    private void ValidateRequiredParameters(string action, string param, string paramName, string[] actionsWhereParamIsRequired)
    {
        if (string.IsNullOrEmpty(param) && actionsWhereParamIsRequired.Contains(action))
        {
            throw new ArgumentException($"Action '{action}' requires a '{paramName}' but none was provided");
        }
    }
    
    public (JToken, bool, bool) RunCopilotAction(string commandData)
    // return: {data, was_success, was_changed}
    {  // todo you can make undo/redo actions using the baserow 'ClientSessionId' header
        
        var validActions = new[] { "list_tables", "update_cell", "get_cell", "get_field", "get_table_fields", "get_table_rows", "create_row", "move_row", "update_row", "create_field", "update_field", "delete_field" };
        try
        {
            var requestedChanges = JsonConvert.DeserializeObject<dynamic>(commandData);
            
            Console.WriteLine($"{requestedChanges.reason} (action: '{requestedChanges.action}')"); // copilot will provide this with every request
            
            string[] actionsRequireTableid = { "get_field", "update_field", "delete_field", "get_table_fields", "get_table_rows", "create_row" };
            string[] actionsRequireFieldid = { "get_field", "update_field", "delete_field", "update_cell", "get_cell" };
            string[] actionsRequireRowid = { "move_row", "update_row", "update_cell", "get_cell" };
            // match copilot's requested action to the right baserow endpoint
            string action = requestedChanges.action;
            string tableId = requestedChanges.tableId;
            string fieldId = requestedChanges.fieldId;
            string rowId = requestedChanges.rowId;
            ValidateRequiredParameters(action, tableId, "tableId", actionsRequireTableid);
            ValidateRequiredParameters(action, fieldId, "fieldId", actionsRequireFieldid);
            ValidateRequiredParameters(action, rowId, "rowId", actionsRequireRowid);
            
            // normalize incase they contain slashes
            try { tableId = tableId.Replace("/", ""); } catch {}
            try { fieldId = fieldId.Replace("/", ""); } catch {}
            try { rowId = rowId.Replace("/", ""); } catch {}
            
            // MARK START action definitions
            if (requestedChanges.action == "list_tables")
            {
                // table ids are globally unique across all databases
                // this.LogMessage("Fetching tables for base: {0}", baseId);
                var tablesData = _baserowClient.FetchTablesForBase(_baseId);
                return (new JObject
                {
                    ["content"] = tablesData,
                    ["msg"] = $"Successfully retrieved tables for base: {_baseId}"
                }, true, false);
            }
            else if (requestedChanges.action == "get_field")
            {
                JToken fieldSchema = _baserowClient.GetTableSingleField(tableId, fieldId);
                return (new JObject
                {
                    ["content"] = fieldSchema,
                    ["msg"] = $"Successfully retrieved table field {fieldId} for: {tableId}"
                }, true, false);
            }
            else if (requestedChanges.action == "get_table_fields")
            {
                JToken tableSchema = _baserowClient.GetTableFields(tableId);
                return (new JObject
                {
                    ["content"] = tableSchema,
                    ["msg"] = $"Successfully retrieved table fields for: {tableId}"
                }, true, false);
            }
            else if (requestedChanges.action == "get_table_rows")
            {
                JToken tableSchema = _baserowClient.GetTableRows(tableId);
                return (new JObject
                {
                    ["content"] = tableSchema,
                    ["msg"] = $"Successfully retrieved table rows for: {tableId}"
                }, true, false);
            }
            else if (requestedChanges.action == "create_field")
            {
                string name = requestedChanges.content.fieldName;
                string type = requestedChanges.content.fieldType;
                Console.WriteLine($"Creating new field: name: {name}, type: {type}");
                JToken resp = _baserowClient.CreateField(tableId, name, type);
                return (new JObject
                {
                    ["content"] = resp,
                    ["msg"] = $"Successfully created field on tableId: {tableId}"
                }, true, true);
            }
            else if (requestedChanges.action == "update_field")
            {
                string name = requestedChanges.content.fieldName;
                string type = requestedChanges.content.fieldType;
                // Console.WriteLine($"Updating field: id: {fieldId}, to type: {type}, name: {name}");
                JToken resp = _baserowClient.UpdateField(fieldId, name, type);
                return (new JObject
                {
                    ["content"] = resp,
                    ["msg"] = $"Successfully updated field: {fieldId}"
                }, true, true);
            }
            else if (requestedChanges.action == "delete_field")
            {
                Console.WriteLine($"Deleting field: id: {fieldId}");
                JToken resp = _baserowClient.DeleteField(fieldId);
                return (new JObject
                {
                    ["content"] = resp,
                    ["msg"] = $"Successfully deleted field: {fieldId}"
                }, true, true);
            }
            else if (requestedChanges.action == "create_row")
            {
                string fields = JsonConvert.SerializeObject(requestedChanges.content);
                Console.WriteLine($"Creating new row with fields: {fields}");
                JToken resp = _baserowClient.CreateRow(tableId, fields);
                return (new JObject
                {
                    ["content"] = resp,
                    ["msg"] = $"Successfully created row"
                }, true, true);
            }
            else if (requestedChanges.action == "move_row")
            {
                JToken resp;
                if (requestedChanges.ContainsKey("relativeRowId"))
                {
                    resp = _baserowClient.MoveRow(tableId, rowId, requestedChanges.relativeRowId);
                    Console.WriteLine($"Moved row {rowId} to position before {requestedChanges.relativeRowId}");
                    return (new JObject
                    {
                        ["content"] = resp,
                        ["msg"] = $"Successfully moved row {rowId} to position before {requestedChanges.relativeRowId}"
                    }, true, true);
                }
                resp = _baserowClient.MoveRow(tableId, rowId);
                Console.WriteLine($"Moved row {rowId} to the end of the table");
                return (new JObject
                {
                    ["content"] = resp,
                    ["msg"] = $"Successfully moved row {rowId} to the end of the table"
                }, true, true);
            }
            else if (requestedChanges.action == "update_row")
            {
                string fields = JsonConvert.SerializeObject(requestedChanges.content);
                JToken resp = _baserowClient.UpdateRow(tableId, rowId, fields);
                return (new JObject
                {
                    ["content"] = resp,
                    ["msg"] = $"Successfully updated row: {rowId}"
                }, true, true);
            }
            else if (requestedChanges.action == "get_cell")
            {
                // Console.WriteLine($"Fetching cell data for field x row: {fieldId}x{rowId}");
                JToken fieldResp = _baserowClient.GetCell(tableId, rowId, fieldId);
                return (new JObject
                {
                    ["content"] = fieldResp,
                    ["msg"] = $"Successfully retrieved cell: {fieldId}x{rowId}"
                }, true, false);
            }
            else if (requestedChanges.action == "update_cell")
            {
                string newValue = requestedChanges.content;
                // Console.WriteLine($"Updating cell data for field x row: {fieldId}x{rowId} to {newValue}");
                JToken resp = _baserowClient.UpdateCell(tableId, rowId, fieldId, newValue);
                return (new JObject
                {
                    ["content"] = resp,
                    ["msg"] = $"Successfully updated cell contents: {fieldId}x{rowId}"
                }, true, true);
            }
            
            return (new JObject
            {
                ["content"] = null,
                ["msg"] = $"Action was not matched to any of the following for base {_baseId}: {string.Join(", ", validActions)}"
            }, false, false);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running copilot action: {ex.Message}");
            return (new JObject
            {
                ["content"] = null,
                ["msg"] = $"Error running copilot action: {ex.Message}"
            }, false, false);
        }
    }
}