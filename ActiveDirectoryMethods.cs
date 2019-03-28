using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ActiveDirectory
{
    public class ActiveDirectoryMethods
    {
        // private void LoadDTSVariable()
        // {
        //     var tableColumns = CreateColumns(new string[] { "GuidId", "DistinguishedName", "SAMAccountName", "DisplayName", "EmployeeId", "FirstName", "LastName", "EmailAddress", "Telephone", "AccountDescription", "LastLogon", "IsAccountLockedOut", "IsEnabled" });
        //     var dataTable = CreateTable(tableColumns);
        //     GetAccounts().ForEach(x =>
        //     {
        //         LoadDataTable(dataTable, x.GetListOfValues());
        //     });

        //     Dts.Variables["User::ADDataTable"].Value = dataTable;
        // }
    
    
        private List<ActiveDirectoryUser> GetAccounts(string domainName)
        {
            var result = new List<ActiveDirectoryUser>();
            using (var context = new PrincipalContext(ContextType.Domain, domainName)) //"dcs.azdcs.gov"
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (UserPrincipal u in searcher.FindAll())
                    {
                        
                        result.Add(new ActiveDirectoryUser
                        {
                            GuidId = u.Guid,
                            DistinguishedName = u.DistinguishedName,
                            SAMAccountName = u.SamAccountName,
                            DisplayName = u.DisplayName,
                            EmployeeId = u.EmployeeId,
                            FirstName = u.GivenName,
                            LastName = u.Surname,
                            EmailAddress = u.EmailAddress,
                            Telephone = u.VoiceTelephoneNumber,
                            AccountDescription = u.Description,
                            LastLogon = u.LastLogon,
                            IsAccountLockedOut = u.IsAccountLockedOut(),
                            IsEnabled = u.Enabled, 
                            Manager = GetActiveDirectoryProperty(u.SamAccountName, domainName, "manager"),
                            Title = GetActiveDirectoryProperty(u.SamAccountName, domainName, "title")
                        });
                    }
                }
            }
            return result;
        }

        private void LoadDataTable(DataTable table, string[] data)
        {
            table.Rows.Add(data);
        }

        private DataTable CreateTable(List<DataColumn> dataColumns)
        {
            var dataTable = new DataTable();
            dataTable.Columns.AddRange(dataColumns.ToArray());
            return dataTable;
        }

        private List<DataColumn> CreateColumns(string[] columnNames)
        {
            return columnNames.Select(x => new DataColumn
            {
                ColumnName = x,
                AllowDBNull = true
            }).ToList();
        }


        //TODO: Clean up these helper methods pre deployment

        public string GetActiveDirectoryProperty(string samAccountName, string domainName, string propertyName){

            DirectoryEntry de = new DirectoryEntry(domainName);

            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + samAccountName + "))";
            ds.SearchScope = SearchScope.Subtree;

            SearchResult rs = ds.FindOne();

            var result = rs.Properties[propertyName][0].ToString(); //see AdUser object for available properties (this is tacky, but I don't want to serialize the entire object)
            
            //clean up when done. 
            //TODO: (change to using block before pushing to PROD!)
            ds.Dispose();
            de.Dispose();

            return result; //any consumer of this method will have to know that this is a temp fix, and you must cast/serialize complex objects where necessary
        }
    }
}