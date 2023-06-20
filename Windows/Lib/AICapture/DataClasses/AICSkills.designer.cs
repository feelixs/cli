using System;
using System.Collections.Generic;
using System.Text;

namespace AICapture.OST.Lib.AICapture.DataClasses
{
    public partial class AICSkills
    {
        public enum Enum {
            FindSSoT,
            UpdateSSoT,
            SaveTranscript,
            UpdateTranscript,
            GetProjectList,
            SwitchProject,
            CreateProject,
            GetCurrentPath,
            PromptLLM,
            GetBackupList,
            GetConversationList,
            GetConversationDetails,
            ChangeProject,
            SaveBackup,
            RestoreBackup,
            GetFileList,
            UpdateFile,
            RequestReplay,
            RequestReplayAll,
            Steren,
            PromptChatGPT35,
            PromptChatGPT40,
            PromptBard,
            MathQuestion
        }
    // Create an Enum here
    }
}
