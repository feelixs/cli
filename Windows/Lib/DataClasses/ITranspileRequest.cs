using System;
using System.ComponentModel;

namespace SSoTme.OST.Lib.DataClasses
{
    public interface ITranspileRequest
    {
        Guid AccountHolderId { get; set; }
        DateTime CreatedOn { get; set; }
        string JsonOutputFileSet { get; set; }
        BindingList<ProjectTranspiler> LastTranspilerRequestId_ProjectTranspilers { get; set; }
        BindingList<TranspileInputFile> TranspileInputFiles { get; set; }
        BindingList<TranspileOutputFile> TranspileOutputFiles { get; set; }
        Guid TranspileRequestId { get; set; }
        Guid TranspileRequestStatusId { get; set; }
        Guid TranspilerId { get; set; }
        Guid? TranspilerInstanceId { get; set; }
        byte[] ZippedInputFileSet { get; set; }
        byte[] ZippedOutputFileSet { get; set; }
    }
}