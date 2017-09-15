/************************************************
 CODEE42 - AUTO GENERATED FILE - DO NOT OVERWRITE
 ************************************************
 Created By: EJ Alexandra - 2016
             An Abstract Level, llc
 License:    Mozilla Public License 2.0
 ************************************************
 CODEE42 - AUTO GENERATED FILE - DO NOT OVERWRITE
 ************************************************/
using System;
using SSoTme.OST.Lib.DataClasses;
using System.Collections.Generic;

namespace EnumList
{
    public class TranspileRequestStatusList
    {
        public static Dictionary<Guid, TranspileRequestStatus> ById = new Dictionary<Guid, TranspileRequestStatus>();
        public static Dictionary<String, TranspileRequestStatus> ByName = new Dictionary<String, TranspileRequestStatus>();
        public static List<TranspileRequestStatus> Items = new List<TranspileRequestStatus>();

        
          public static TranspileRequestStatus Requested { get; private set; }
        
          public static TranspileRequestStatus Recieved { get; private set; }
        
          public static TranspileRequestStatus InProcess { get; private set; }
        
          public static TranspileRequestStatus Completed { get; private set; }
        

        static TranspileRequestStatusList()
        { // test
            TranspileRequestStatusList.Requested = new TranspileRequestStatus()
            {
                TranspileRequestStatusId = new Guid("0B76A0B8-53A9-4B10-8768-19E451BBC826"),
                Name = @"Requested",
                Description = @"The request has been submitted",
                SortOrder = 1
            };
            
            TranspileRequestStatusList.Items.Add(TranspileRequestStatusList.Requested);
            TranspileRequestStatusList.ById[TranspileRequestStatusList.Requested.TranspileRequestStatusId] = TranspileRequestStatusList.Requested;
            TranspileRequestStatusList.ByName[TranspileRequestStatusList.Requested.Name] = TranspileRequestStatusList.Requested;
            TranspileRequestStatusList.Recieved = new TranspileRequestStatus()
            {
                TranspileRequestStatusId = new Guid("16512F23-E49F-4BEC-9EDC-F9BA5531F85A"),
                Name = @"Recieved",
                Description = @"The request has been received",
                SortOrder = 5
            };
            
            TranspileRequestStatusList.Items.Add(TranspileRequestStatusList.Recieved);
            TranspileRequestStatusList.ById[TranspileRequestStatusList.Recieved.TranspileRequestStatusId] = TranspileRequestStatusList.Recieved;
            TranspileRequestStatusList.ByName[TranspileRequestStatusList.Recieved.Name] = TranspileRequestStatusList.Recieved;
            TranspileRequestStatusList.InProcess = new TranspileRequestStatus()
            {
                TranspileRequestStatusId = new Guid("27392D0C-041C-481A-B6E0-F3C66872ECC9"),
                Name = @"InProcess",
                Description = @"The request is being processed",
                SortOrder = 10
            };
            
            TranspileRequestStatusList.Items.Add(TranspileRequestStatusList.InProcess);
            TranspileRequestStatusList.ById[TranspileRequestStatusList.InProcess.TranspileRequestStatusId] = TranspileRequestStatusList.InProcess;
            TranspileRequestStatusList.ByName[TranspileRequestStatusList.InProcess.Name] = TranspileRequestStatusList.InProcess;
            TranspileRequestStatusList.Completed = new TranspileRequestStatus()
            {
                TranspileRequestStatusId = new Guid("A131AD0F-D050-4C37-9AD8-B9B8538AEF66"),
                Name = @"Completed",
                Description = @"The request is finished",
                SortOrder = 100
            };
            
            TranspileRequestStatusList.Items.Add(TranspileRequestStatusList.Completed);
            TranspileRequestStatusList.ById[TranspileRequestStatusList.Completed.TranspileRequestStatusId] = TranspileRequestStatusList.Completed;
            TranspileRequestStatusList.ByName[TranspileRequestStatusList.Completed.Name] = TranspileRequestStatusList.Completed;
            
        }
    }
}
            