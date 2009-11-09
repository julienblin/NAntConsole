using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows
{
    [TaskName("remove-access-rule")]
    public class RemoveAccessRuleTask : BaseAccessRuleTask
    {
        protected override void ExecuteOnDir(DirectoryInfo dir)
        {
            DirectorySecurity dirSec = new DirectorySecurity(dir.FullName, AccessControlSections.Access);
            IList<FileSystemAccessRule> targetRules = FindAccessRules(dirSec);
            if (targetRules.Count == 0)
            {
                Log(Level.Info, Resources.RemoveAccessRuleEmpty, NTAccount, dir.FullName);
            }
            else
            {
                foreach (FileSystemAccessRule fileSystemAccessRule in targetRules)
                {
                    Log(Level.Info, Resources.RemoveAccessRuleRemoving, NTAccount, dir.FullName);
                    dirSec.RemoveAccessRule(fileSystemAccessRule);
                }
                dir.SetAccessControl(dirSec);
            }
        }

        protected override void ExecuteOnFile(FileInfo file)
        {
            FileSecurity fileSec = new FileSecurity(file.FullName, AccessControlSections.Access);
            IList<FileSystemAccessRule> targetRules = FindAccessRules(fileSec);
            if (targetRules.Count == 0)
            {
                Log(Level.Info, Resources.RemoveAccessRuleEmpty, NTAccount, file.FullName);
            }
            else
            {
                foreach (FileSystemAccessRule fileSystemAccessRule in targetRules)
                {
                    Log(Level.Info, Resources.RemoveAccessRuleRemoving, NTAccount, file.FullName);
                    fileSec.RemoveAccessRule(fileSystemAccessRule);
                }
                file.SetAccessControl(fileSec);
            }
        }

        protected override void ExecuteOnShare(string share)
        {
            Log(Level.Info, Resources.RemoveAccessRuleRemoving, NTAccount, share);
            Activeds.IADsSecurityUtility secUtility = new Activeds.ADsSecurityUtilityClass();
            Activeds.IADsSecurityDescriptor secDescriptor = (Activeds.IADsSecurityDescriptor)secUtility.GetSecurityDescriptor(share, (int)Activeds.ADS_PATHTYPE_ENUM.ADS_PATH_FILESHARE, (int)Activeds.ADS_SD_FORMAT_ENUM.ADS_SD_FORMAT_IID);
            Activeds.IADsAccessControlList dacl = (Activeds.IADsAccessControlList)secDescriptor.DiscretionaryAcl;

            List<Activeds.IADsAccessControlEntry> acesToRemove = new List<Activeds.IADsAccessControlEntry>();
            foreach (Activeds.IADsAccessControlEntry ace in dacl)
            {
                if (ace.Trustee.Equals(NTAccount, StringComparison.InvariantCultureIgnoreCase))
                {
                    acesToRemove.Add(ace);
                }
            }

            foreach (Activeds.IADsAccessControlEntry ace in acesToRemove)
            {
                dacl.RemoveAce(ace);
            }

            secDescriptor.DiscretionaryAcl = dacl;
            secUtility.SetSecurityDescriptor(share, (int)Activeds.ADS_PATHTYPE_ENUM.ADS_PATH_FILESHARE, secDescriptor, (int)Activeds.ADS_SD_FORMAT_ENUM.ADS_SD_FORMAT_IID);
        }
    }
}
