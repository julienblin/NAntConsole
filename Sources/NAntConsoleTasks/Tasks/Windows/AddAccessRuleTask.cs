using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.Reflection;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.Windows
{
    [TaskName("add-access-rule")]
    public class AddAccessRuleTask : BaseAccessRuleTask
    {
        private FileSystemRights rights;
        [TaskAttribute("rights")]
        public FileSystemRights Rights
        {
            get { return rights; }
            set { rights = value; }
        }

        private Activeds.ADS_RIGHTS_ENUM accessMask = Activeds.ADS_RIGHTS_ENUM.ADS_RIGHT_GENERIC_ALL;
        [TaskAttribute("accessMask")]
        public Activeds.ADS_RIGHTS_ENUM AccessMask
        {
            get { return accessMask; }
            set { accessMask = value; }
        }

        private InheritanceFlags inheritanceFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
        [TaskAttribute("inheritance")]
        public InheritanceFlags InheritanceFlags
        {
            get { return inheritanceFlags; }
            set { inheritanceFlags = value; }
        }

        private PropagationFlags propagationFlags = PropagationFlags.None;
        [TaskAttribute("propagation")]
        public PropagationFlags PropagationFlags
        {
            get { return propagationFlags; }
            set { propagationFlags = value; }
        }

        private AccessControlType accessControlType = AccessControlType.Allow;
        [TaskAttribute("accessControlType")]
        public AccessControlType AccessControlType
        {
            get { return accessControlType; }
            set { accessControlType = value; }
        }

        protected override void ExecuteOnDir(DirectoryInfo dir)
        {
            DirectorySecurity dirSec = new DirectorySecurity(dir.FullName, AccessControlSections.Access);

            Log(Level.Info, Resources.AddAccessRuleAdding, Rights, NTAccount, dir.FullName);
            FileSystemAccessRule newRule = new FileSystemAccessRule(new NTAccount(NTAccount), Rights, InheritanceFlags, PropagationFlags, AccessControlType);
            dirSec.AddAccessRule(newRule);
            dir.SetAccessControl(dirSec);
        }

        protected override void ExecuteOnFile(FileInfo file)
        {
            FileSecurity fileSec = new FileSecurity(file.FullName, AccessControlSections.Access);

            Log(Level.Info, Resources.AddAccessRuleAdding, Rights, NTAccount, file.FullName);
            FileSystemAccessRule newRule = new FileSystemAccessRule(new NTAccount(NTAccount), Rights, AccessControlType);
            fileSec.AddAccessRule(newRule);
            file.SetAccessControl(fileSec);
        }

        protected override void ExecuteOnShare(string share)
        {
            Log(Level.Info, Resources.AddAccessRuleAdding, AccessMask, NTAccount, share);
            Activeds.IADsSecurityUtility secUtility = new Activeds.ADsSecurityUtilityClass();
            Activeds.IADsSecurityDescriptor secDescriptor = (Activeds.IADsSecurityDescriptor)secUtility.GetSecurityDescriptor(share, (int)Activeds.ADS_PATHTYPE_ENUM.ADS_PATH_FILESHARE, (int)Activeds.ADS_SD_FORMAT_ENUM.ADS_SD_FORMAT_IID);
            Activeds.IADsAccessControlList dacl = (Activeds.IADsAccessControlList)secDescriptor.DiscretionaryAcl;

            Activeds.IADsAccessControlEntry ace = new Activeds.AccessControlEntry();
            ace.Trustee = NTAccount;
            ace.AceType = (int)Activeds.ADS_ACETYPE_ENUM.ADS_ACETYPE_ACCESS_ALLOWED;
            ace.AceFlags = (int)Activeds.ADS_ACEFLAG_ENUM.ADS_ACEFLAG_INHERIT_ACE;
            ace.AccessMask = (int)AccessMask;
            dacl.AddAce(ace);
            secDescriptor.DiscretionaryAcl = dacl;
            secUtility.SetSecurityDescriptor(share, (int)Activeds.ADS_PATHTYPE_ENUM.ADS_PATH_FILESHARE, secDescriptor, (int)Activeds.ADS_SD_FORMAT_ENUM.ADS_SD_FORMAT_IID); 
        }
    }
}
