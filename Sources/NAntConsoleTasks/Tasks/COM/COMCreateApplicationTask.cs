using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Types;
using Comadmin;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using NAnt.DotNet.Tasks;

namespace CDS.Framework.Tools.NAntConsole.NAntConsoleTasks.Tasks.COM
{
    [TaskName("com-create-application")]
    public class COMCreateApplicationTask : BaseCOMTask
    {
        private string applicationName;
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        private readonly ArrayList comProperties = new ArrayList();
        [BuildElementArray("property", ElementType = typeof(PropertyTask))]
        public ArrayList COMProperties
        {
            get
            {
                return comProperties;
            }
        }

        private readonly ArrayList roles = new ArrayList();
        [BuildElementArray("role", ElementType = typeof(COMRoleElement))]
        public ArrayList Roles
        {
            get
            {
                return roles;
            }
        }

        private readonly ArrayList comInstallComponents = new ArrayList();
        [BuildElementArray("install-component", ElementType = typeof(COMInstallComponentElement))]
        public ArrayList ComInstallComponents
        {
            get
            {
                return comInstallComponents;
            }
        }

        private readonly ArrayList comComponentProperties = new ArrayList();
        [BuildElementArray("component-property", ElementType = typeof(COMComponentPropertyElement))]
        public ArrayList COMComponentProperties
        {
            get
            {
                return comComponentProperties;
            }
        }

        protected override void ExecuteCOMTask()
        {
            using (COMSearchResult searchResult = FindApplication(ApplicationName))
            {
                if (!searchResult.NotFound)
                {
                    Log(Level.Info, Resources.COMCreateApplicationDeleting, ApplicationName);
                    searchResult.CatalogCollection.Remove(searchResult.Index);
                    searchResult.CatalogCollection.SaveChanges();
                }

                Log(Level.Info, Resources.COMCreateApplicationCreating, ApplicationName);
                ICatalogObject newApplication = (ICatalogObject)searchResult.CatalogCollection.Add();
                newApplication.set_Value("Name", ApplicationName);

                ApplyProperties(COMProperties, newApplication);

                searchResult.CatalogCollection.SaveChanges();

                AddRoles(searchResult.CatalogCollection, newApplication);

                searchResult.CatalogCollection.SaveChanges();

                foreach (COMInstallComponentElement comComponentElement in ComInstallComponents)
                {
                    if(comComponentElement.File != null)
                    {
                        InstallComponent(comComponentElement, comComponentElement.File);
                    }

                    if (comComponentElement.FileSet != null)
                    {
                        foreach (string fileName in comComponentElement.FileSet.FileNames)
                        {
                            InstallComponent(comComponentElement, new FileInfo(fileName));
                        }
                    }
                }

                foreach (COMComponentPropertyElement comComponentPropertyElement in COMComponentProperties)
                {
                    using (COMSearchResult comComponentSearchResult = FindComponent(ApplicationName, comComponentPropertyElement.ComponentName))
                    {
                        if (!comComponentSearchResult.NotFound)
                        {
                            ApplyProperty(comComponentPropertyElement.PropertyName,
                                          comComponentPropertyElement.PropertyValue,
                                          comComponentSearchResult.CatalogObject);
                            comComponentSearchResult.CatalogCollection.SaveChanges();
                        }
                        else
                        {
                            Log(Level.Warning, Resources.COMCreateApplicationWarningComponentNotFound, comComponentPropertyElement.ComponentName, ApplicationName, comComponentPropertyElement.PropertyName, comComponentPropertyElement.PropertyValue);
                        }
                    }
                }
            }
        }

        private void InstallComponent(COMInstallComponentElement comComponentElement, FileInfo installComponentFile)
        {
            Log(Level.Info, Resources.COMCreateApplicationAddComponent, installComponentFile.FullName,
                ApplicationName);
            if (comComponentElement.Net)
            {
                RegsvcsTask regsvcsTask = new RegsvcsTask();
                CopyTo(regsvcsTask);
                regsvcsTask.Parent = this;
                regsvcsTask.Action = RegsvcsTask.ActionType.FindOrCreate;
                regsvcsTask.ApplicationName = ApplicationName;
                regsvcsTask.AssemblyFile = installComponentFile;
                regsvcsTask.Execute();
            }
            else
            {
                ComAdminCatalog.InstallComponent(ApplicationName, installComponentFile.FullName,
                                                 string.Empty, string.Empty);
            }
        }

        private void AddRoles(ICatalogCollection catalogCollection, ICatalogObject application)
        {
            ICatalogCollection rolesCatalog = (ICatalogCollection)catalogCollection.GetCollection(COMConstants.ROLES_CATALOG_NAME, application.Key);
            rolesCatalog.Populate();
            foreach (COMRoleElement comRole in Roles)
            {
                Log(Level.Info, Resources.COMCreateApplicationAddRole, comRole.RoleName, application.Name);
                ICatalogObject newRole = (ICatalogObject)rolesCatalog.Add();
                newRole.set_Value("Name", comRole.RoleName);
                rolesCatalog.SaveChanges();

                ICatalogCollection usersInRolesCatalog = (ICatalogCollection)rolesCatalog.GetCollection(COMConstants.USERS_IN_ROLES_CATALOG_NAME, newRole.Key);
                foreach (COMRoleUserElement comRoleUserElement in comRole.Users)
                {
                    ICatalogObject newUser = (ICatalogObject)usersInRolesCatalog.Add();
                    newUser.set_Value("User", comRoleUserElement.UserName);
                    usersInRolesCatalog.SaveChanges();
                }

            }
            rolesCatalog.SaveChanges();
        }
    }
}
