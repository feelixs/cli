using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoTme.OST.Lib.SassySDK.Derived
{
    public class SSOTMEKey
    {
        public string EmailAddress { get; set; }
        public string Secret { get; set; }
        public static SSOTMEKey CurrentKey
        {
            get { return GetSSoTmeKey(); }
            set { SetSSoTmeKey(value); }
        }

        public static void SetSSoTmeKey(SSOTMEKey value, string account = "")
        {
            FileInfo ssotmeKeyFI = GetKeyForAccount(account);
            String ssotmeJson = JsonConvert.SerializeObject(value, Formatting.Indented);
            File.WriteAllText(ssotmeKeyFI.FullName, ssotmeJson);
        }

        public static SSOTMEKey GetSSoTmeKey(string runAs = "")
        {
            FileInfo ssotmeKeyFI = GetKeyForAccount(runAs);
            var ssotmeKey = default(SSOTMEKey);
            if (ssotmeKeyFI.Exists) ssotmeKey = JsonConvert.DeserializeObject<SSOTMEKey>(File.ReadAllText(ssotmeKeyFI.FullName));

            else ssotmeKey = new SSOTMEKey();
            return ssotmeKey;
        }

        private static FileInfo GetKeyForAccount(string accountUsername)
        {

            var myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var ssotmeKeyFI = default(FileInfo);
            if (String.IsNullOrEmpty(accountUsername)) ssotmeKeyFI = new FileInfo(Path.Combine(myDocsPath, "SSOT.me", "ssotme.key"));
            else ssotmeKeyFI = new FileInfo(Path.Combine(myDocsPath, "SSOT.me", String.Format("ssotme_{0}.key", accountUsername)));

            if (!ssotmeKeyFI.Exists) throw new Exception(String.Format("Can't find key for SSoTme Account: {0}", accountUsername));
            else return ssotmeKeyFI;
        }
    }
}
